using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NLSE
{
    public partial class Exhibition : Form
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
        public Exhibition()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "exhibition.dat|*.dat" +
            "|All Files|*.*";
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                Close();
                return; // no file loaded
            }
            else
            {
                InitializeComponent();
                CB_Choi.SelectedIndex = 0;
                path = ofd.FileName;
                long length = new FileInfo(path).Length;
                if (length != 0x17BE10)
                {
                    MessageBox.Show("Not a valid exhibition.dat file !");
                    Close();
                }
                else
                {
                    Main.SaveData = File.ReadAllBytes(path);
                    Save = new ExhibitionData(Main.SaveData);
                    loadData();
                }
            }
        }
        private void BlockControl()
        {
            tabControl1.Enabled = false;
            B_SavePlayer.Enabled = false;
            B_Del.Enabled = false;
            B_Export.Enabled = false;
        }

        private void UnblockControl()
        {
            tabControl1.Enabled = true;
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

        // Exhibition Save Editing
        private ExhibitionData Save;

        class ExhibitionData
        {
            public byte[] Data;
            public ExhibitionData(byte[] data)
            {
                Data = data;
            }
            public byte[] Write()
            {
                return Data;
            }
        }

        class PlayerList
        {
            public byte[] Data;
            public int PlayerSID;
            public string PlayerName;
            public int TownSID;
            public string TownName;

            public PlayerList(byte[] data)
            {
                Data = data;

                PlayerSID = BitConverter.ToUInt16(data, 0);
                PlayerName = Encoding.Unicode.GetString(Data.Skip(0x2).Take(0x12).ToArray()).Trim('\0');
                TownSID = BitConverter.ToUInt16(data, 0x15);
                TownName = Encoding.Unicode.GetString(Data.Skip(0x17).Take(0x12).ToArray()).Trim('\0');
            }
        }

        class PlayerData
        {
            public byte[] Data;
            public byte Hair, HairColor,
                Face, EyeColor,
                Tan;

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
            public int MetDay;
            public int MetMonth;
            public uint MetYear;
            public int PrivateDC;
            public int DCPart1;
            public int DCPart2;
            public int DCPart3;
            public int DCPart4;
            public int Visited;
            public int Favorite;
            public int HoldItem;
            public Image JPEG;

            public PlayerData(byte[] data)
            {
                Data = data;

                Hair = Data[0x1234];
                HairColor = Data[0x1235];
                Face = Data[0x1236];
                EyeColor = Data[0x1237];
                Tan = Data[0x1238];

                PlayerSID = BitConverter.ToUInt16(data, 0x67D6);
                PlayerName = Encoding.Unicode.GetString(Data.Skip(0x67D8).Take(0x12).ToArray()).Trim('\0');
                TownSID = BitConverter.ToUInt16(data, 0x67EC);
                TownName = Encoding.Unicode.GetString(Data.Skip(0x67EE).Take(0x12).ToArray()).Trim('\0');
                Comment = Encoding.Unicode.GetString(Data.Skip(0x7D68).Take(0x40).ToArray());

                Badges = Data.Skip(0x68CC).Take(24).ToArray();
                Gender = Data[0x67EA];
                Visited = Data[0x7DC9];
                Favorite = Data[0x7DCF];
                HoldItem = Data[0x7DCE];

                BirthdayMonth = Data[0x6804];
                BirthdayDay = Data[0x6805];

                RegDay = Data[0x6808];
                RegMonth = Data[0x6807];
                RegYear = BitConverter.ToUInt16(data, 0x6806);

                MetDay = Data[0x7DC7];
                MetMonth = Data[0x7DC6];
                MetYear = BitConverter.ToUInt16(data, 0x7DC4);

                DCPart1 = BitConverter.ToUInt16(data, 0x6920);
                DCPart2 = BitConverter.ToUInt16(data, 0x6922);
                DCPart3 = Data[0x6924];
                DCPart4 = Data[0x6929];
                PrivateDC = Data[0x6942];

                try { JPEG = Image.FromStream(new MemoryStream(Data.Skip(0x6968).Take(0x1400).ToArray())); }
                catch { JPEG = null; }
            }
            public byte[] Write()
            {
                Data[0x1234] = Hair;
                Data[0x1235] = HairColor;
                Data[0x1236] = Face;
                Data[0x1237] = EyeColor;
                Data[0x1238] = Tan;

                Array.Copy(Encoding.Unicode.GetBytes(PlayerName.PadRight(9, '\0')), 0, Data, 0x67D8, 0x12);
                Array.Copy(Encoding.Unicode.GetBytes(TownName.PadRight(9, '\0')), 0, Data, 0x67EE, 0x12);
                Array.Copy(Encoding.Unicode.GetBytes(Comment.PadRight(32, '\0')), 0, Data, 0x7D68, 0x40);

                Array.Copy(Badges, 0, Data, 0x569C, Badges.Length);
                Data[0x67EA] = (byte)Gender;
                Data[0x7DC9] = (byte)Visited;
                Data[0x7DCF] = (byte)Favorite;
                Data[0x7DCE] = (byte)HoldItem;
                Data[0x6942] = (byte)PrivateDC;

                Data[0x6804] = (byte)BirthdayMonth;
                Data[0x6805] = (byte)BirthdayDay;

                Data[0x6808] = (byte)RegDay;
                Data[0x6807] = (byte)RegMonth;
                Array.Copy(BitConverter.GetBytes(RegYear), 0, Data, 0x6806, 2);

                Data[0x7DC7] = (byte)MetDay;
                Data[0x7DC6] = (byte)MetMonth;
                Array.Copy(BitConverter.GetBytes(MetYear), 0, Data, 0x7DC4, 2);

                return Data;
            }
        }

        private void loadData()
        {
            Save = new ExhibitionData(Main.SaveData);

            PlayersData = new PlayerData[48];
            for (int i = 0; i < PlayersData.Length; i++)
                PlayersData[i] = new PlayerData(Save.Data.Skip(0x2590 + i * 0x7DD8).Take(0x7DD8).ToArray());
            PopulateList();
        }

        private void saveData()
        {
            int i = PeopleList.SelectedIndex;
            savePlayer(i);

            for (int g = 0; g < PlayersData.Length; g++)
                Array.Copy(PlayersData[g].Write(), 0, Save.Data, 0x2590 + g * 0x7DD8, 0x7DD8);

            Main.SaveData = Save.Write();

            Verification.fixChecksums(ref Main.SaveData);
            File.WriteAllBytes(path, Main.SaveData);
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
            CB_HairStyle.SelectedIndex = PlayersData[i].Hair;
            CB_FaceShape.SelectedIndex = PlayersData[i].Face;
            CB_SkinColor.SelectedIndex = PlayersData[i].EyeColor;
            CB_EyeColor.SelectedIndex = PlayersData[i].EyeColor;
            CB_HairColor.SelectedIndex = PlayersData[i].HairColor;

            NUD_RegDay.Value = PlayersData[i].RegDay;
            CB_RegMonth.SelectedIndex = PlayersData[i].RegMonth;
            NUD_RegYear.Value = PlayersData[i].RegYear;

            NUD_BirthDay.Value = PlayersData[i].BirthdayDay;
            CB_BirthMonth.SelectedIndex = PlayersData[i].BirthdayMonth;

            loadBadge();

            NUD_MetDay.Value = PlayersData[i].MetDay;
            CB_MetMonth.SelectedIndex = PlayersData[i].MetMonth;
            NUD_MetYear.Value = PlayersData[i].MetYear;

            CB_Visited.Checked = PlayersData[i].Visited == 0;
            CB_PrivateDC.Checked = PlayersData[i].PrivateDC == 0xE7;
            CB_Item.Checked = PlayersData[i].HoldItem == 1;

            if (PlayersData[i].Favorite == 0)
            {
                CB_Favorite.Checked = true;
                CB_FavoriteLocked.Enabled = false;
                CB_Favorite.Enabled = true;
            }
            else if (PlayersData[i].Favorite == 1)
            {
                CB_FavoriteLocked.Checked = true;
                CB_Favorite.Enabled = false;
                CB_FavoriteLocked.Enabled = true;
            }
            else if (PlayersData[i].Favorite == 4)
            {
                CB_FavoriteLocked.Checked = false;
                CB_Favorite.Checked = false;
                CB_Favorite.Enabled = true;
                CB_FavoriteLocked.Enabled = true;
            }

            if (PlayersData[i].DCPart1 != 0)
            {
                label5.Text = $"Dream Code: {PlayersData[i].DCPart4:X02}{PlayersData[i].DCPart3:X02}-{PlayersData[i].DCPart2:X04}-{PlayersData[i].DCPart1:X04}";
            }
            
            PB_PlayerPict.Image = PlayersData[i].JPEG;
        }
        private void savePlayer(int i)
        {
            PlayersData[i].PlayerName = TB_Name.Text;
            PlayersData[i].TownName = TB_Town.Text;
            PlayersData[i].Comment = TB_Comment.Text;

            PlayersData[i].PrivateDC = CB_PrivateDC.Checked == true ? 0xE7 : 0xEF;

            PlayersData[i].Hair = (byte)CB_HairStyle.SelectedIndex;
            PlayersData[i].HairColor = (byte)CB_HairColor.SelectedIndex;
            PlayersData[i].Face = (byte)CB_FaceShape.SelectedIndex;
            PlayersData[i].EyeColor = (byte)CB_EyeColor.SelectedIndex;
            PlayersData[i].Tan = (byte)CB_SkinColor.SelectedIndex;
            PlayersData[i].Gender = (byte)CB_Gender.SelectedIndex;

            PlayersData[i].BirthdayDay = (byte)NUD_BirthDay.Value;
            PlayersData[i].BirthdayMonth = (byte)CB_BirthMonth.SelectedIndex;

            PlayersData[i].RegDay = (byte)NUD_RegDay.Value;
            PlayersData[i].RegMonth = (byte)CB_RegMonth.SelectedIndex;
            PlayersData[i].RegYear = (uint)NUD_RegYear.Value;

            PlayersData[i].MetDay = (byte)NUD_MetDay.Value;
            PlayersData[i].MetMonth = (byte)CB_MetMonth.SelectedIndex;
            PlayersData[i].MetYear = (uint)NUD_MetYear.Value;

            if (CB_Favorite.Checked == false && CB_FavoriteLocked.Checked == false)
            {
                PlayersData[i].Favorite = 4;
            }
            if (CB_Favorite.Checked == true && CB_FavoriteLocked.Checked == false)
            {
                PlayersData[i].Favorite = 0;
            }
            if (CB_Favorite.Checked == false && CB_FavoriteLocked.Checked == true)
            {
                PlayersData[i].Favorite = 1;
            }

            if (CB_Visited.Checked == true)
            {
                PlayersData[i].Visited = 0;
            }
            else if (CB_Visited.Checked == false)
            {
                PlayersData[i].Visited = 3;
            }

            if (CB_Item.Checked == true)
            {
                PlayersData[i].HoldItem = 1;
            }
            else if (CB_Item.Checked == false)
            {
                PlayersData[i].HoldItem = 3;
            }
        }

        private void loadBadge()
        {
            int i = PeopleList.SelectedIndex;

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

        private void PopulateList()
        {
            PeopleList.Items.Clear();
            for (int j = 0; j < PlayersData.Length; j++)
            {
                if (PlayersData[j].PlayerSID == 0 && PlayersData[j].TownSID == 0)
                {
                    int slot = j + 1;
                    string empty = "Slot " + slot.ToString() + ": empty"; 
                    PeopleList.Items.Add(empty);
                }
                else
                {
                    PeopleList.Items.Add(PlayersData[j].PlayerName);
                }
                PeopleList.SelectedIndex = 0;
            }
        }
        private void PeopleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = PeopleList.SelectedIndex;
            loadPlayer(i);
        }

        private void B_SavePlayer_Click(object sender, EventArgs e)
        {
            int i = PeopleList.SelectedIndex;
            savePlayer(i);
            for (int g = 0; g < PlayersData.Length; g++)
                Array.Copy(PlayersData[g].Write(), 0, Save.Data, 0x2590 + g * 0x7DD8, 0x7DD8);
            PopulateList();
            PeopleList.SelectedIndex = i;
        }

        private void B_Patterns_Click(object sender, EventArgs e)
        {
            int i = PeopleList.SelectedIndex;

            if (CB_Choi.SelectedIndex == 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = TB_Name.Text + " Pattern Data.bin";
                if (sfd.ShowDialog() != DialogResult.OK)
                    return; // not saved

                byte[] Pattern = (PlayersData[i].Data.Skip(0x125C).Take(0x870 * 0xA).ToArray());

                File.WriteAllBytes(sfd.FileName, Pattern);
            }
            else if (CB_Choi.SelectedIndex == 1)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() != DialogResult.OK)
                    return; // no file loaded

                string path = ofd.FileName;

                long length = new FileInfo(path).Length;
                if (length != 0x5460) // Check file size
                {
                    MessageBox.Show("Invalid file.");
                    return;
                }
                byte[] Pattern = File.ReadAllBytes(path);

                Array.Copy(Pattern, 0, PlayersData[i].Data, 0x125C, length);
            }
        }

        private void B_HouseData_Click(object sender, EventArgs e)
        {
            int i = PeopleList.SelectedIndex;

            if (CB_Choi.SelectedIndex == 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = TB_Name.Text + " House Data.bin";
                if (sfd.ShowDialog() != DialogResult.OK)
                    return; // not saved

                byte[] House = (PlayersData[i].Data.Skip(0xC).Take(0x1228).ToArray());

                File.WriteAllBytes(sfd.FileName, House);
            }
            else if (CB_Choi.SelectedIndex == 1)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() != DialogResult.OK)
                    return; // no file loaded

                string path = ofd.FileName;

                long length = new FileInfo(path).Length;
                if (length != 0x1228) // Check file size
                {
                    MessageBox.Show("Invalid file.");
                    return;
                }
                byte[] House = File.ReadAllBytes(path);

                Array.Copy(House, 0, PlayersData[i].Data, 0xC, length);
            }
        }

        private void B_Export_Click(object sender, EventArgs e)
        {
            int i = PeopleList.SelectedIndex;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = TB_Name.Text + " exi Data.bin";
            if (sfd.ShowDialog() != DialogResult.OK)
                return; // not saved

            byte[] Data = (PlayersData[i].Data.Take(0x7DD8).ToArray());

            File.WriteAllBytes(sfd.FileName, Data);
        }

        private void B_Import_Click(object sender, EventArgs e)
        {
            int i = PeopleList.SelectedIndex;

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return; // no file loaded

            string path = ofd.FileName;

            long length = new FileInfo(path).Length;
            if (length != 0x7DD8) // Check file size
            {
                MessageBox.Show("Invalid file.");
                return;
            }
            byte[] Data = File.ReadAllBytes(path);

            Array.Copy(Data, 0, Save.Data, (0x2590 + i * 0x7DD8), 0x7DD8);

            PlayersData = new PlayerData[48];
            for (int j = 0; j < PlayersData.Length; j++)
                PlayersData[j] = new PlayerData(Save.Data.Skip(0x2590 + j * 0x7DD8).Take(0x7DD8).ToArray());
            PopulateList();

            PeopleList.SelectedIndex = i;
        }

        private void B_Del_Click(object sender, EventArgs e)
        {
            int i = PeopleList.SelectedIndex;

            DialogResult dialogResult = MessageBox.Show("Do you really want to remove " + TB_Name.Text + " ?", "Delete Player", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                byte[] Empty = Properties.Resources.exi_empty_data;

                Array.Copy(Empty, 0, Save.Data, (0x2590 + i * 0x7DD8), 0x7DD8);

                PlayersData = new PlayerData[48];
                for (int j = 0; j < PlayersData.Length; j++)
                    PlayersData[j] = new PlayerData(Save.Data.Skip(0x2590 + j * 0x7DD8).Take(0x7DD8).ToArray());
                PopulateList();

                PeopleList.SelectedIndex = i;
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void FavoriteChanged(object sender, EventArgs e)
        {
            if (CB_Favorite.Checked == true)
            {
                CB_FavoriteLocked.Checked = false;
                CB_FavoriteLocked.Enabled = false;
            }
            if (CB_Favorite.Checked == false)
            {
                CB_FavoriteLocked.Enabled = true;
            }
            if (CB_FavoriteLocked.Checked == true)
            {
                CB_Favorite.Checked = false;
                CB_Favorite.Enabled = false;
            }
            if (CB_FavoriteLocked.Checked == false)
            {
                CB_Favorite.Enabled = true;
            }
        }
    }
}
