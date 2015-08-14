using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NLSE
{
    public partial class Garden : Form
    {
        public Garden(byte[] data)
        {
            InitializeComponent();
            Save = new GardenData(data);

            // Load
            loadData();
        }
        private void B_Save_Click(object sender, EventArgs e)
        {
            saveData();
            Close();
        }
        private void B_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Garden Save Editing
        private GardenData Save;
        class GardenData
        {
            // public int TOWNMAGIC = 0x84; // Offset of the Town Data
            // public int Villagers = 0x180 + 0x84;

            // Player Name is 0x16 bytes long.
            // Town Name is 0x12 bytes long.
            public DataRef JPEG = new DataRef(0x57C4, 0x1400);
            public DataRef PlayerName = new DataRef(0xF8, 0x14);
            public DataRef TownName = new DataRef(0x10E, 0x12);

            public byte[] Data;
            public GardenData(byte[] data)
            {
                Data = data;
            }
        }
        private void loadData()
        {
            PB_JPEG.Image = Image.FromStream(new MemoryStream(Save.JPEG.getData(Save.Data)));
            string Name = Save.PlayerName.getString(Save.Data);
            string Town = Save.TownName.getString(Save.Data);
            L_Info.Text = Name + Environment.NewLine + Town;
        }
        private void saveData()
        {

        }
        
        // Unused
        private int getTownOffset()
        {
            return 0x84;
        }

        private void lt_bank()
        {
            int offset = getTownOffset();
            const int BANK_OFFSET = 0x6B8C;

            offset += BANK_OFFSET;
            Array.Copy(BitConverter.GetBytes(0x8CF95678), 0, Save.Data, offset, 4);
            Array.Copy(BitConverter.GetBytes(0x0D118636), 0, Save.Data, offset, 4);
        }

    }
}
