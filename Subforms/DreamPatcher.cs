using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using PackageIO;
using System.Windows.Forms;
using System.IO;

namespace NLSE.Subforms
{
    public partial class DreamPatcher : Form
    {
        OpenFileDialog op = new OpenFileDialog();
        public string filepath;
        public string fileoutput;

        public DreamPatcher()
        {
            InitializeComponent();
        }

        private void B_OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofddr = new OpenFileDialog();
            ofddr.Filter = "garden_plus.dat(*.*)|*.*";
            if (ofddr.ShowDialog() != DialogResult.OK)
                return; // no file loaded
            filepath = ofddr.FileName;
            TB_FilePath.Text = filepath;
        }

        private void B_PatchFile_Click(object sender, EventArgs e)
        {
            if (TB_FilePath.Text == "")
            {
                MessageBox.Show("Please open a file first.");
                return;
            }
            else
            {
                checkGarden();
                checkDream();
                cleanDream();
            }
        }
        public void checkGarden()
        {
            Reader read = new Reader(filepath, Endian.Big);
            long length = new FileInfo(filepath).Length;
            read.Close();
            if (length == 0x89A80)
            {
                byte[] array = new Byte[0x80];
                Array.Clear(array, 0, array.Length);

                using (BinaryWriter file = new BinaryWriter(File.Open(filepath, FileMode.Open)))
                {
                    file.Seek(0, SeekOrigin.End);
                    file.Write(array);
                    file.Close();
                }
            }
            else if (length != 0x89B00)
            {
                MessageBox.Show("Not a valid ACNL savegame !");
                return;
            }
        }

        private void checkDream()
        {
            Reader read = new Reader(filepath, Endian.Big);
            read.Seek(0x4BF68);
            int check = read.ReadInt8();
            if (check != 0x67)
            {
                MessageBox.Show("Invalid dream dump !\nMake sure it was dumped in the dream.");
                return;
            }
        }
        public void cleanDream() // shitty but working code
        {
            Writer write = new Writer(filepath, Endian.Big);
            for (int i = 0; i < 4; i++) // remove dream code
            {
                write.Seek(0x5790 + (0xA480 * i));
                write.WriteInt48(0);
                write.Seek(0x5798 + (0xA480 * i));
                write.WriteInt8(0);
            }
            write.Seek(0x4BF68); // clean dream buildings
            write.WriteUInt32(0xFC000000);
            write.Seek(0x4BF6C);
            write.WriteUInt32(0xFC000000);

            for (int i = 0; i < 0x188; i++) // clean town info board
            {
                write.Seek(0x6E2DC + i); 
                write.WriteInt8(0);
            }

            write.Seek(0x7250C);
            write.WriteUInt32(0xFFFFFFFF);
            for (int i = 0; i < 0x1444; i++) // clean "unknow" checksum algo data
            {
                write.Seek(0x72510 + i);
                write.WriteInt8(0);
            }

            for (int i = 0; i < 0x6E18; i++) // clean additionnal mail
            {
                write.Seek(0x73958 + i);
                write.WriteInt8(0);
            }

            for (int i = 0; i < 0xCCC0; i++) // clean additionnal mail
            {
                write.Seek(0x7BDF8 + i);
                write.WriteInt8(0);
            }
            write.Close();
            byte[] Data = File.ReadAllBytes(filepath);
            Verification.fixChecksums(ref Data);
            File.WriteAllBytes(filepath, Data);
            Util.Alert("Dream succefully modified, you can now use it as a normal savegame !");
        }
    }
}

