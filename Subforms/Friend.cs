using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NLSE
{
    public partial class Friend : Form
    {
        Image[] BadgePCH = { Properties.Resources.empty, Properties.Resources.PCH_1, Properties.Resources.PCH_2, Properties.Resources.PCH_3 };
        Image[] BadgeFLT = { Properties.Resources.empty, Properties.Resources.FLT_1, Properties.Resources.FLT_2, Properties.Resources.FLT_3 };
        Image[] BadgePLG = { Properties.Resources.empty, Properties.Resources.PLG_1, Properties.Resources.PLG_2, Properties.Resources.PLG_3 };
        Image[] BadgePSO = { Properties.Resources.empty, Properties.Resources.PSO_1, Properties.Resources.PSO_2, Properties.Resources.PSO_3 };
        Image[] BadgeINS = { Properties.Resources.empty, Properties.Resources.INS_1, Properties.Resources.INS_2, Properties.Resources.INS_3 };
        Image[] BadgePLP = { Properties.Resources.empty, Properties.Resources.PLP_1, Properties.Resources.PLP_2, Properties.Resources.PLP_3 };
        Image[] BadgeBLO = { Properties.Resources.empty, Properties.Resources.BLO_1, Properties.Resources.BLO_2, Properties.Resources.BLO_3 };
        Image[] BadgeTRN = { Properties.Resources.empty, Properties.Resources.TRN_1, Properties.Resources.TRN_2, Properties.Resources.TRN_3 };
        Image[] BadgeHRT = { Properties.Resources.empty, Properties.Resources.HRT_1, Properties.Resources.HRT_2, Properties.Resources.HRT_3 };
        Image[] BadgeARB = { Properties.Resources.empty, Properties.Resources.ARB_1, Properties.Resources.ARB_2, Properties.Resources.ARB_3 };
        Image[] BadgeCLO = { Properties.Resources.empty, Properties.Resources.CLO_1, Properties.Resources.CLO_2, Properties.Resources.CLO_3 };
        Image[] BadgeISL = { Properties.Resources.empty, Properties.Resources.ISL_1, Properties.Resources.ISL_2, Properties.Resources.ISL_3 };
        Image[] BadgeSTP = { Properties.Resources.empty, Properties.Resources.STP_1, Properties.Resources.STP_2, Properties.Resources.STP_3 };
        Image[] BadgeMVH = { Properties.Resources.empty, Properties.Resources.MVH_1, Properties.Resources.MVH_2, Properties.Resources.MVH_3 };
        Image[] BadgeCDY = { Properties.Resources.empty, Properties.Resources.CDY_1, Properties.Resources.CDY_2, Properties.Resources.CDY_3 };
        Image[] BadgeRNV = { Properties.Resources.empty, Properties.Resources.RNV_1, Properties.Resources.RNV_2, Properties.Resources.RNV_3 };
        Image[] BadgeLTR = { Properties.Resources.empty, Properties.Resources.LTR_1, Properties.Resources.LTR_2, Properties.Resources.LTR_3 };
        Image[] BadgeCTL = { Properties.Resources.empty, Properties.Resources.CTL_1, Properties.Resources.CTL_2, Properties.Resources.CTL_3 };
        Image[] BadgeKKG = { Properties.Resources.empty, Properties.Resources.KKG_1, Properties.Resources.KKG_2, Properties.Resources.KKG_3 };
        Image[] BadgeCRN = { Properties.Resources.empty, Properties.Resources.CRN_1, Properties.Resources.CRN_2, Properties.Resources.CRN_3 };
        Image[] BadgeNVT = { Properties.Resources.empty, Properties.Resources.NVT_1, Properties.Resources.NVT_2, Properties.Resources.NVT_3 };
        Image[] BadgeMSN = { Properties.Resources.empty, Properties.Resources.MSN_1, Properties.Resources.MSN_2, Properties.Resources.MSN_3 };
        Image[] BadgeFLR = { Properties.Resources.empty, Properties.Resources.FLR_1, Properties.Resources.FLR_2, Properties.Resources.FLR_3 };
        Image[] BadgeVST = { Properties.Resources.empty, Properties.Resources.VST_1, Properties.Resources.VST_2, Properties.Resources.VST_3 };

        private PlayerData[] PlayersData;

        public string path;
        public Friend()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "friend#.dat|*.dat" +
            "|All Files|*.*";
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                Close();
                return; // no file loaded
            }
            else
            {
                InitializeComponent();
                path = ofd.FileName;
                long length = new FileInfo(path).Length;
                if (length != 0x29608)
                {
                    MessageBox.Show("Not a valid friend#.dat file !");
                    Close();
                }
                else
                {
                    Main.SaveData = File.ReadAllBytes(path);
                    Save = new FriendData(Main.SaveData);
                    loadData();
                }
            }
        }
        private void BlockControl()
        {
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            B_SavePlayer.Enabled = false;
            B_Del.Enabled = false;
            B_Export.Enabled = false;
        }

        private void UnblockControl()
        {
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            B_SavePlayer.Enabled = true;
            B_Del.Enabled = true;
            B_Export.Enabled = true;
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

        // Friend Save Editing
        private FriendData Save;
        class FriendData
        {
            public byte[] Data;
            public FriendData(byte[] data)
            {
                Data = data;
            }
            public byte[] Write()
            {
                return Data;
            }
        }
        class PlayerData
        {
            public byte[] Data;

            public int PlayerSID;
            public string PlayerName;
            public string Comment;
            public int TownSID;
            public string TownName;
            public byte[] Badges;
            public int Gender;
            public int BirthdayDay;
            public int BirthdayMonth;
            public int RegDay;
            public int RegMonth;
            public uint RegYear;
            public int DCPart1;
            public int DCPart2;
            public int DCPart3;
            public int DCPart4;
            public Image JPEG;

            public PlayerData(byte[] data)
            {
                Data = data;

                PlayerSID = BitConverter.ToUInt16(data, 0x1424);
                PlayerName = Encoding.Unicode.GetString(Data.Skip(0x1426).Take(0x12).ToArray()).Trim('\0');
                TownSID = BitConverter.ToUInt16(data, 0x143A);
                TownName = Encoding.Unicode.GetString(Data.Skip(0x143C).Take(0x12).ToArray()).Trim('\0');
                Comment = Encoding.Unicode.GetString(Data.Skip(0x1470).Take(0x40).ToArray());

                Badges = Data.Skip(0x1452).Take(24).ToArray();
                Gender = Data[0x1438];

                BirthdayMonth = Data[0x146A];
                BirthdayDay = Data[0x146B];

                RegDay = Data[0x146F];
                RegMonth = Data[0x146E];
                RegYear = BitConverter.ToUInt16(data, 0x146C);

                DCPart1 = BitConverter.ToUInt16(data, 0x0);
                DCPart2 = BitConverter.ToUInt16(data, 0x0);
                DCPart3 = Data[0x0];
                DCPart4 = Data[0x0];

                try { JPEG = Image.FromStream(new MemoryStream(Data.Skip(0x8).Take(0x1400).ToArray())); }
                catch { JPEG = null; }
            }
            public byte[] Write()
            {
                Array.Copy(Encoding.Unicode.GetBytes(PlayerName.PadRight(9, '\0')), 0, Data, 0x1426, 0x12);
                Array.Copy(Encoding.Unicode.GetBytes(TownName.PadRight(9, '\0')), 0, Data, 0x143C, 0x12);
                Array.Copy(Encoding.Unicode.GetBytes(Comment.PadRight(32, '\0')), 0, Data, 0x1470, 0x40);

                Data[0x1438] = (byte)Gender;

                Data[0x146A] = (byte)BirthdayMonth;
                Data[0x146B] = (byte)BirthdayDay;

                Data[0x146F] = (byte)RegDay;
                Data[0x146E] = (byte)RegMonth;
                Array.Copy(BitConverter.GetBytes(RegYear), 0, Data, 0x146C, 2);

                return Data;
            }
        }

        private void loadData()
        {
            Save = new FriendData(Main.SaveData);

            PlayersData = new PlayerData[20];
            for (int i = 0; i < PlayersData.Length; i++)
                PlayersData[i] = new PlayerData(Save.Data.Skip(0x4 + i * 0x14B0).Take(0x14B0).ToArray());
            PopulateList();
        }
        private void saveData()
        {
            int i = friendList.SelectedIndex;
            savePlayer(i);

            for (int g = 0; g < PlayersData.Length; g++)
                Array.Copy(PlayersData[g].Write(), 0, Save.Data, 0x4 + g * 0x14B0, 0x14B0);

            Main.SaveData = Save.Write();

            Verification.fixChecksums(ref Main.SaveData);
            File.WriteAllBytes(path, Main.SaveData);
        }
        private void PopulateList()
        {
            friendList.Items.Clear();
            for (int j = 0; j < PlayersData.Length; j++)
            {
                if (PlayersData[j].PlayerSID == 0 && PlayersData[j].TownSID == 0)
                {
                    int slot = j + 1;
                    string empty = "Slot " + slot.ToString() + ": empty";
                    friendList.Items.Add(empty);
                }
                else
                {
                    friendList.Items.Add(PlayersData[j].PlayerName);
                }
                friendList.SelectedIndex = 0;
            }
        }

        private void friendList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = friendList.SelectedIndex;
            loadPlayer(i);
        }
        private void loadBadge()
        {
            int i = friendList.SelectedIndex;

            PB_Badge00.Image = BadgePCH[PlayersData[i].Badges[0]];
            PB_Badge01.Image = BadgeFLT[PlayersData[i].Badges[1]];
            PB_Badge02.Image = BadgePLG[PlayersData[i].Badges[2]];
            PB_Badge03.Image = BadgePSO[PlayersData[i].Badges[3]];
            PB_Badge04.Image = BadgeINS[PlayersData[i].Badges[4]];
            PB_Badge05.Image = BadgePLP[PlayersData[i].Badges[5]];
            PB_Badge06.Image = BadgeBLO[PlayersData[i].Badges[6]];
            PB_Badge07.Image = BadgeTRN[PlayersData[i].Badges[7]];
            PB_Badge08.Image = BadgeVST[PlayersData[i].Badges[22]];
            PB_Badge09.Image = BadgeARB[PlayersData[i].Badges[9]];
            PB_Badge10.Image = BadgeCLO[PlayersData[i].Badges[10]];
            PB_Badge11.Image = BadgeNVT[PlayersData[i].Badges[11]];
            PB_Badge12.Image = BadgeISL[PlayersData[i].Badges[12]];
            PB_Badge13.Image = BadgeSTP[PlayersData[i].Badges[13]];
            PB_Badge14.Image = BadgeMVH[PlayersData[i].Badges[14]];
            PB_Badge15.Image = BadgeCDY[PlayersData[i].Badges[15]];
            PB_Badge16.Image = BadgeLTR[PlayersData[i].Badges[16]];
            PB_Badge17.Image = BadgeRNV[PlayersData[i].Badges[17]];
            PB_Badge18.Image = BadgeCTL[PlayersData[i].Badges[18]];
            PB_Badge19.Image = BadgeKKG[PlayersData[i].Badges[19]];
            PB_Badge20.Image = BadgeMSN[PlayersData[i].Badges[20]];
            PB_Badge21.Image = BadgeCRN[PlayersData[i].Badges[21]];
            PB_Badge22.Image = BadgeHRT[PlayersData[i].Badges[8]];
            PB_Badge23.Image = BadgeFLR[PlayersData[i].Badges[23]];
        }

        private void loadPlayer(int i)
        {
            if (PlayersData[i].PlayerSID == 0 && PlayersData[i].TownSID == 0)
            {
                BlockControl();
            }
            else
            UnblockControl();
            TB_Name.Text = PlayersData[i].PlayerName;
            TB_Town.Text = PlayersData[i].TownName;
            TB_Comment.Text = PlayersData[i].Comment;
            CB_Gender.SelectedIndex = PlayersData[i].Gender;

            NUD_RegDay.Value = PlayersData[i].RegDay;
            CB_RegMonth.SelectedIndex = PlayersData[i].RegMonth;
            NUD_RegYear.Value = PlayersData[i].RegYear;

            NUD_BirthDay.Value = PlayersData[i].BirthdayDay;
            CB_BirthMonth.SelectedIndex = PlayersData[i].BirthdayMonth;

            PB_PlayerPict.Image = PlayersData[i].JPEG;
            loadBadge();
        }
        private void savePlayer(int i)
        {
            PlayersData[i].PlayerName = TB_Name.Text;
            PlayersData[i].TownName = TB_Town.Text;
            PlayersData[i].Comment = TB_Comment.Text;

            PlayersData[i].Gender = (byte)CB_Gender.SelectedIndex;

            PlayersData[i].BirthdayDay = (byte)NUD_BirthDay.Value;
            PlayersData[i].BirthdayMonth = (byte)CB_BirthMonth.SelectedIndex;

            PlayersData[i].RegDay = (byte)NUD_RegDay.Value;
            PlayersData[i].RegMonth = (byte)CB_RegMonth.SelectedIndex;
            PlayersData[i].RegYear = (uint)NUD_RegYear.Value;
        }

        private void B_SavePlayer_Click(object sender, EventArgs e)
        {
            int i = friendList.SelectedIndex;
            savePlayer(i);
            for (int g = 0; g < PlayersData.Length; g++)
                Array.Copy(PlayersData[g].Write(), 0, Save.Data, 0x4 + g * 0x14B0, 0x14B0);
            PopulateList();
            friendList.SelectedIndex = i;
        }

        private void B_Export_Click(object sender, EventArgs e)
        {
            int i = friendList.SelectedIndex;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = TB_Name.Text + " friend Data.bin";
            if (sfd.ShowDialog() != DialogResult.OK)
                return; // not saved

            byte[] Data = (PlayersData[i].Data.Take(0x14B0).ToArray());

            File.WriteAllBytes(sfd.FileName, Data);
        }

        private void B_Import_Click(object sender, EventArgs e)
        {
            int i = friendList.SelectedIndex;

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return; // no file loaded

            string path = ofd.FileName;

            long length = new FileInfo(path).Length;
            if (length != 0x14B0) // Check file size
            {
                MessageBox.Show("Invalid file.");
                return;
            }
            byte[] Data = File.ReadAllBytes(path);

            Array.Copy(Data, 0, Save.Data, (0x4 + i * 0x14B0), 0x14B0);

            PlayersData = new PlayerData[20];
            for (int j = 0; j < PlayersData.Length; j++)
                PlayersData[j] = new PlayerData(Save.Data.Skip(0x4 + j * 0x14B0).Take(0x14B0).ToArray());
            PopulateList();

            friendList.SelectedIndex = i;
        }

        private void B_Del_Click(object sender, EventArgs e)
        {
            int i = friendList.SelectedIndex;

            DialogResult dialogResult = MessageBox.Show("Do you really want to remove " + TB_Name.Text + " ?", "Delete Player", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                byte[] Empty = Properties.Resources.friend_empty_data;

                Array.Copy(Empty, 0, Save.Data, (0x4 + i * 0x14B0), 0x14B0);

                PlayersData = new PlayerData[20];
                for (int j = 0; j < PlayersData.Length; j++)
                    PlayersData[j] = new PlayerData(Save.Data.Skip(0x4 + j * 0x14B0).Take(0x14B0).ToArray());
                PopulateList();

                friendList.SelectedIndex = i;
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void B_ReplaceTCP_Click(object sender, EventArgs e)
        {
            int i = friendList.SelectedIndex;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPEG Picture|*.jpg;*.jpeg";
            if (ofd.ShowDialog() != DialogResult.OK)
                return; // no file loaded

            string path = ofd.FileName;

            long length = new FileInfo(path).Length;
            if (length > 0x1400)
            {
                MessageBox.Show("Your TCP Picture is too big !\nMax: 4kb (0x1400)");
                return;
            }
            byte[] CheckHeader = File.ReadAllBytes(path).Skip(0).Take(2).ToArray();

            if (BitConverter.ToUInt16(CheckHeader, 0x0) != 0xD8FF)
            {
                MessageBox.Show("The picture must be a JPEG !");
                return;
            }
            Image img = Image.FromFile(path);

            if (img.Height > 104 && img.Width > 64)
            {
                MessageBox.Show("The dimensions of the picture must be 64x104.");
                return;
            }

            if (img.Height != 104 && img.Width != 64)
            {
                MessageBox.Show("The dimensions of the picture are ok, but the maximum here is 64x104.\nThe dimensions of your picture are: " + img.Width + "x" + img.Height + "");
            }
            byte[] CorrectFile = File.ReadAllBytes(path);
            Array.Copy(CorrectFile, 0, Save.Data, 0xC + (i * 0x14B0), length);
            PlayersData = new PlayerData[20];
            for (int j = 0; j < PlayersData.Length; j++)
                PlayersData[j] = new PlayerData(Save.Data.Skip(0x4 + j * 0x14B0).Take(0x14B0).ToArray());
            PopulateList();

            friendList.SelectedIndex = i;

            PB_PlayerPict.Image = img;
            Util.Alert("Succesfully injected the new picture.");
        }
    }
}
