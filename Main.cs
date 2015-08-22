using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace NLSE
{
    public partial class Main : Form
    {
        internal static string root;
        internal static string[] itemNames = Data.getItemStrings("en");
        internal static List<cbItem> itemList = Data.getCBItems(itemNames);
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
            string path = files[0]; // open first D&D
            if (Directory.Exists(path))
            {
                root = path;
                L_IO.Text = root;
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
                            Util.Alert("File checksums were not updated (already valid):" + Environment.NewLine + file);
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
                MessageBox.Show("Error:" + Environment.NewLine + ex);
            }
        }
        private void clickGarden(object sender, EventArgs e)
        {
            string dataPath = Path.Combine(root, "garden.dat");
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
                MessageBox.Show("Error:" + Environment.NewLine + ex);
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
                MessageBox.Show("Error:" + Environment.NewLine + ex);
            }
        }
    }
}
