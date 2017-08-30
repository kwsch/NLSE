using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace NLSE
{
    public partial class Main : Form
    {
        internal static string root;
        internal static string[] itemNames = Data.getIndexStrings("item", "en");
        internal static string[] buildingNames = Data.getIndexStrings("building", "en");
        internal static string[] villagerNames = Data.getStrings("name", "en");
        internal static List<cbItem> itemList = Data.getCBItems(itemNames);
        internal static List<cbItem> vList = Data.getCBList(villagerNames, null);
        internal static List<cbItem> buildingList = Data.getCBItems(buildingNames);
        internal static ACNLVillager[] villagerList = Data.GetVillagers();

        public Main()
        {
            // Set up.
            InitializeComponent();
            CB_Friend.Enabled = B_Exhibition.Enabled = B_Friend.Enabled = B_Garden.Enabled = false;

            // Allow D&D
            AllowDrop = true;
            DragEnter += tabMain_DragEnter;
            DragDrop += tabMain_DragDrop;

            // Find the save files.
            scanLoop();
            // mine();
        }
        // Drag & Drop Events
        private void tabMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        private void tabMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (Directory.Exists(files[0]))
            {
                findLoop = false;
                root = files[0];
                L_IO.Text = root;
                return;
            }

            long len = new FileInfo(files[0]).Length;
            if (len == 0x80000 || len == 0xC0000 || len == 0x121000 || len == 0x130000) // RAM
            {
                if (Util.Prompt(MessageBoxButtons.YesNo, "Edit RAM Dump?" + Environment.NewLine + files[0]) == DialogResult.Yes)
                {
                    byte[] data = File.ReadAllBytes(files[0]);
                    SaveData = new byte[data.Length + 0x80]; // shift 0x80 bytes
                    Array.Copy(data, 0, SaveData, 0x80, SaveData.Length - 0x80);
                    new Garden().ShowDialog();
                }
            }
            else // Try fix checksums?
            {
                foreach (string file in files)
                {
                    try
                    {
                        byte[] data = File.ReadAllBytes(file);
                        byte[] data2 = (byte[])data.Clone();
                        Verification.fixChecksums(ref data);
                        if (!data.SequenceEqual(data2))
                        {
                            if (Util.Prompt(MessageBoxButtons.YesNo, "Update checksums?" + Environment.NewLine + file) == DialogResult.Yes)
                            {
                                File.WriteAllBytes(file, data);
                                Util.Alert("File checksums were updated:" + Environment.NewLine + file);
                            }
                            Util.Alert("File checksums were not updated (chose not to):" + Environment.NewLine + file);
                        }
                        else
                        {
                            Util.Alert("File checksums were not updated (already valid):" + Environment.NewLine + file, "If you were trying to load your save file, drop the folder that has garden.dat instead!");
                        }
                    }
                    catch (Exception ex) { Util.Error("File error:" + Environment.NewLine + file, ex.ToString()); }
                }
            }
        }

        // Find Files on Load Loop
        private bool findLoop = true;
        private void scanLoop(int ms = 400)
        {
            new Thread(() =>
            {
                while (findLoop && (root = Util.GetSDFLocation()) == null)
                    Thread.Sleep(ms);

                // Trigger update
                if (InvokeRequired)
                    Invoke((MethodInvoker)delegate { L_IO.Text = root; });
                else L_IO.Text = root;
            }).Start();
        }

        private void L_IO_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog ofd = new FolderBrowserDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            findLoop = false;
            root = ofd.SelectedPath;
            L_IO.Text = root;
        }
        private void updatePath(object sender, EventArgs e)
        {
            // Scan for files to enable.
            string[] files = Directory.GetFiles(root);

            B_Exhibition.Enabled = File.Exists(Path.Combine(root, "exhibition.dat"));
            B_Garden.Enabled = File.Exists(Path.Combine(root, "garden.dat"));

            CB_Friend.Items.Clear();
            foreach (string file in files.Where(file => file.Contains("friend")))
                CB_Friend.Items.Add(Path.GetFileName(file));
            CB_Friend.Enabled = B_Friend.Enabled = CB_Friend.Items.Count > 0;
            if (CB_Friend.Items.Count > 0) 
                CB_Friend.SelectedIndex = 0;
        }

        // Editing Windows
        internal static byte[] SaveData;

        private void clickExhibition(object sender, EventArgs e)
        {
            string dataPath = Path.Combine(root, "exhibition.dat");
            if (!File.Exists(dataPath)) return;

            // Load Data
            try
            {
                SaveData = File.ReadAllBytes(dataPath);

                // Open Form
                new Exhibition().ShowDialog();

                // Form closed, write data.
                Verification.fixChecksums(ref SaveData);
                File.WriteAllBytes(dataPath, SaveData);
            }
            catch (Exception ex)
            {
                // Error
                Util.Error("Error:" + Environment.NewLine + ex);
            }
        }
        private void clickGarden(object sender, EventArgs e)
        {
            string dataPath = Path.Combine(root, "garden_plus.dat");
            if (!File.Exists(dataPath)) return;

            // Load Data
            try
            {
                SaveData = File.ReadAllBytes(dataPath);

                // Open Form
                new Garden().ShowDialog();

                // Form closed, write data.
                Verification.fixChecksums(ref SaveData);
                File.WriteAllBytes(dataPath, SaveData);
            }
            catch (Exception ex)
            {
                // Error
                Util.Error("Error:" + Environment.NewLine + ex);
            }
        }
        private void clickFriend(object sender, EventArgs e)
        {
            string dataPath = Path.Combine(root, CB_Friend.Text);
            if (!File.Exists(dataPath)) return;

            // Load Data
            try
            {
                SaveData = File.ReadAllBytes(dataPath);

                // Open Form
                new Friend().ShowDialog();

                // Form closed, write data.
                Verification.fixChecksums(ref SaveData);
                File.WriteAllBytes(dataPath, SaveData);
            }
            catch (Exception ex)
            {
                // Error
                Util.Error("Error:" + Environment.NewLine + ex);
            }
        }

        private void clickHelp(object sender, CancelEventArgs e)
        {
            Util.Alert(String.Format("NLSE - By Kaphotics{0}" +
                                     "{0}" +
                                     "To begin, drag the folder that contains your exported save file (garden.dat) onto the program window.{0}" +
                                     "{0}" +
                                     "Credits:{0}" +
                                     "Big thanks to marc_max (ACNL Save Editor), NeoKamek (LeafTools), and the many other contributors to the scene!",
                Environment.NewLine));
            e.Cancel = true; // remove ? cursor
        }
    }
}
