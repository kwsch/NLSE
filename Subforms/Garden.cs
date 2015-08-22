using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NLSE
{
    public partial class Garden : Form
    {
        // Form Variables
        private ushort[] TownAcreTiles, IslandAcreTiles;
        private PictureBox[] TownAcres, IslandAcres, PlayerPics, PlayerPicsLarge, PlayerPockets, PlayerDressers1, PlayerDressers2, PlayerIslandBox;
        private ComboBox[] PlayerHairStyles, PlayerHairColors, PlayerEyeColors, PlayerFaces, PlayerTans, PlayerGenders, TownVillagers;
        private ComboBox[][] PlayerBadges;
        private TextBox[] PlayerNames, TownVillagersCatch;

        private Player[] Players;
        private Building[] Buildings;
        private Villager[] Villagers;
        private Item[] TownItems, IslandItems;

        // Form Handlingc
        public Garden()
        {
            InitializeComponent();
            CB_Item.DisplayMember = "Text";
            CB_Item.ValueMember = "Value";
            CB_Item.DataSource = new BindingSource(Main.itemList, null);
            TB_Flag1.KeyPress += EnterKey;
            TB_Flag2.KeyPress += EnterKey;
            Save = new GardenData(Main.SaveData);
            #region Array Initialization
            TownAcres = new[]
            {
                PB_acre00, PB_acre10, PB_acre20, PB_acre30, PB_acre40, PB_acre50, PB_acre60,
                PB_acre01, PB_acre11, PB_acre21, PB_acre31, PB_acre41, PB_acre51, PB_acre61,
                PB_acre02, PB_acre12, PB_acre22, PB_acre32, PB_acre42, PB_acre52, PB_acre62,
                PB_acre03, PB_acre13, PB_acre23, PB_acre33, PB_acre43, PB_acre53, PB_acre63,
                PB_acre04, PB_acre14, PB_acre24, PB_acre34, PB_acre44, PB_acre54, PB_acre64,
                PB_acre05, PB_acre15, PB_acre25, PB_acre35, PB_acre45, PB_acre55, PB_acre65,
            };
            IslandAcres = new[]
            {
                PB_island00, PB_island10, PB_island20, PB_island30,
                PB_island01, PB_island11, PB_island21, PB_island31,
                PB_island02, PB_island12, PB_island22, PB_island32,
                PB_island03, PB_island13, PB_island23, PB_island33,
            };
            PlayerPics = new[]
            {
                PB_JPEG0, PB_JPEG1, PB_JPEG2, PB_JPEG3
            };
            PlayerPicsLarge = new[]
            {
                PB_LPlayer0, /* PB_LPlayer1, PB_LPlayer2, PB_LPlayer3 */
            };
            PlayerPockets = new[]
            {
                PB_P0Pocket, /* PB_P1Pocket, PB_P2Pocket, PB_P3Pocket */
            };
            PlayerDressers1 = new[]
            {
                PB_P0Dresser1, /* PB_P1Dresser1, PB_P2Dresser1, PB_P3Dresser1 */
            };
            PlayerDressers2 = new[]
            {
                PB_P0Dresser2, /* PB_P1Dresser2, PB_P2Dresser2, PB_P3Dresser2 */
            };
            PlayerIslandBox = new[]
            {
                PB_P0Island, /* PB_P1Island, PB_P2Island, PB_P3Island */
            };
            PlayerBadges = new[]
            {
                new []
                {
                    CB_P0Badge00, CB_P0Badge01, CB_P0Badge02, CB_P0Badge03, CB_P0Badge04,
                    CB_P0Badge05, CB_P0Badge06, CB_P0Badge07, CB_P0Badge08, CB_P0Badge09,
                    CB_P0Badge10, CB_P0Badge11, CB_P0Badge12, CB_P0Badge13, CB_P0Badge14,
                    CB_P0Badge15, CB_P0Badge16, CB_P0Badge17, CB_P0Badge18, CB_P0Badge19,
                    CB_P0Badge20, CB_P0Badge21, CB_P0Badge22, CB_P0Badge23,
                }, /*
                new []
                {
                    CB_P1Badge00, CB_P1Badge01, CB_P1Badge02, CB_P1Badge03, CB_P1Badge04,
                    CB_P1Badge05, CB_P1Badge06, CB_P1Badge07, CB_P1Badge08, CB_P1Badge09,
                    CB_P1Badge10, CB_P1Badge11, CB_P1Badge12, CB_P1Badge13, CB_P1Badge14,
                    CB_P1Badge15, CB_P1Badge16, CB_P1Badge17, CB_P1Badge18, CB_P1Badge19,
                    CB_P1Badge20, CB_P1Badge21, CB_P1Badge22, CB_P1Badge23,
                },
                new []
                {
                    CB_P2Badge00, CB_P2Badge01, CB_P2Badge02, CB_P2Badge03, CB_P2Badge04,
                    CB_P2Badge05, CB_P2Badge06, CB_P2Badge07, CB_P2Badge08, CB_P2Badge09,
                    CB_P2Badge10, CB_P2Badge11, CB_P2Badge12, CB_P2Badge13, CB_P2Badge14,
                    CB_P2Badge15, CB_P2Badge16, CB_P2Badge17, CB_P2Badge18, CB_P2Badge19,
                    CB_P2Badge20, CB_P2Badge21, CB_P2Badge22, CB_P2Badge23,
                },
                new []
                {
                    CB_P3Badge00, CB_P3Badge01, CB_P3Badge02, CB_P3Badge03, CB_P3Badge04,
                    CB_P3Badge05, CB_P3Badge06, CB_P3Badge07, CB_P3Badge08, CB_P3Badge09,
                    CB_P3Badge10, CB_P3Badge11, CB_P3Badge12, CB_P3Badge13, CB_P3Badge14,
                    CB_P3Badge15, CB_P3Badge16, CB_P3Badge17, CB_P3Badge18, CB_P3Badge19,
                    CB_P3Badge20, CB_P3Badge21, CB_P3Badge22, CB_P3Badge23,
                } */
            };
            PlayerNames = new[]
            {
                TB_P0Name, /* TB_P1Name, TB_P2Name, TB_P3Name */ 
            };
            PlayerHairStyles = new[]
            {
                CB_P0HairStyle, /* CB_P1HairStyle, CB_P2HairStyle, CB_P3HairStyle */ 
            };
            PlayerHairColors = new[]
            {
                CB_P0HairColor, /* CB_P1HairColor, CB_P2HairColor, CB_P3HairColor */ 
            };
            PlayerEyeColors = new[]
            {
                CB_P0EyeColor, /* CB_P1EyeColor, CB_P2EyeColor, CB_P3EyeColor */ 
            };
            PlayerFaces = new[]
            {
                CB_P0FaceShape, /* CB_P1FaceShape, CB_P2FaceShape, CB_P3FaceShape */ 
            };
            PlayerTans = new[]
            {
                CB_P0SkinColor, /* CB_P1SkinColor, CB_P2SkinColor, CB_P3SkinColor */ 
            };
            PlayerGenders = new[]
            {
                CB_P0Gender, /* CB_P1Gender, CB_P2Gender, CB_P3Gender */ 
            };
            TownVillagers = new[]
            {
                CB_Villager1, CB_Villager2, CB_Villager3, CB_Villager4, CB_Villager5,
                CB_Villager6, CB_Villager7, CB_Villager8, CB_Villager9, CB_Villager10
            };
            foreach (ComboBox id in TownVillagers)
            {
                id.DisplayMember = "Text";
                id.ValueMember = "Value";
                id.DataSource = new BindingSource(Main.vList, null);
            }

            TownVillagersCatch = new[]
            {
                TB_VillagerCatch1, TB_VillagerCatch2, TB_VillagerCatch3, TB_VillagerCatch4, TB_VillagerCatch5,
                TB_VillagerCatch6, TB_VillagerCatch7, TB_VillagerCatch8, TB_VillagerCatch9, TB_VillagerCatch10
            };
            #endregion
            #region Load Event Methods to Controls
            foreach (PictureBox p in TownAcres) { p.MouseMove += mouseTown; p.MouseClick += clickTown; }
            foreach (PictureBox p in IslandAcres) { p.MouseMove += mouseIsland; p.MouseClick += clickIsland; }
            foreach (PictureBox p in PlayerPockets) { p.MouseMove += mouseCustom; p.MouseClick += clickCustom; }
            foreach (PictureBox p in PlayerDressers1) { p.MouseMove += mouseCustom; p.MouseClick += clickCustom; }
            foreach (PictureBox p in PlayerDressers2) { p.MouseMove += mouseCustom; p.MouseClick += clickCustom; }
            foreach (PictureBox p in PlayerIslandBox) { p.MouseMove += mouseCustom; p.MouseClick += clickCustom; }
            #endregion
            // Load
            loadData();
            populateBuildingList();
            reloadCurrentItem(currentItem);
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
        private void B_Import_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                FileName = "acnlram.bin",
                Filter = "RAM Dump|*.bin"
            };
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            var len = new FileInfo(ofd.FileName).Length;
            if (new FileInfo(ofd.FileName).Length != 0x80000)
            {
                Util.Error(String.Format(
                    "Data lengths do not match.{0}" +
                    "Expected: 0x{1}{0}" +
                    "Received: 0x{2}",
                    Environment.NewLine, 0x80000.ToString("X5"), len.ToString("X5")));
                return;
            }

            byte[] data = File.ReadAllBytes(ofd.FileName);
            Array.Copy(data, 0, Save.Data, 0x80, Save.Data.Length - 0x80);
        }
        private void B_Export_Click(object sender, EventArgs e)
        {
            saveData();
            var sfd = new SaveFileDialog
            {
                FileName = "acnlram.bin",
                Filter = "RAM Dump|*.bin"
            };
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            byte[] RAM = Save.Data.Skip(0x80).ToArray();
            Array.Resize(ref RAM, 0x80000);

            File.WriteAllBytes(sfd.FileName, RAM);
        }

        // Garden Save Editing
        private GardenData Save;
        class GardenData
        {
            public string TownName;
            public int TownHallColor;
            public int TrainStationColor;
            public int GrassType;
            public int NativeFruit;
            public uint SecondsPlayed;
            public ushort PlayDays;
            public byte[] Data;
            public GardenData(byte[] data)
            {
                Data = data;
                TownName = Encoding.Unicode.GetString(Data.Skip(0x5C7BA).Take(0x12).ToArray()).Trim('\0');
                GrassType = Data[0x4DA81];
                TownHallColor = Data[0x5C7B8] & 3;
                TrainStationColor = Data[0x5C7B9] & 3;
                NativeFruit = Data[0x5C836];
                SecondsPlayed = BitConverter.ToUInt32(Data, 0x5C7B0);
                PlayDays = BitConverter.ToUInt16(Data, 0x5C83A);
            }
            public byte[] Write()
            {
                Data[0x4DA81] = (byte)GrassType;
                Data[0x5C7B8] = (byte)((Data[0x5C7B8] & 0xFC) | TownHallColor);
                Data[0x5C7B9] = (byte)((Data[0x5C7B9] & 0xFC) | TrainStationColor);
                Data[0x5C836] = (byte)NativeFruit;

                Array.Copy(BitConverter.GetBytes(SecondsPlayed), 0, Data, 0x5C7B0, 4);
                Array.Copy(BitConverter.GetBytes(PlayDays), 0, Data, 0x5C83A, 2);
                return Data;
            }
        }
        class Player
        {
            public byte[] Data;
            private uint U32;
            public byte Hair, HairColor, 
                Face, EyeColor, 
                Tan, U9;

            public string Name;
            public int Gender;
            public string HomeTown;

            public Image JPEG;
            public byte[] Badges;
            public Item[] Pockets = new Item[16];
            public Item[] IslandBox = new Item[5 * 8];
            public Item[] Dressers = new Item[5 * 36];
            public Player(byte[] data)
            {
                Data = data;

                U32 = BitConverter.ToUInt32(data, 0);
                Hair = Data[4];
                HairColor = Data[5];
                Face = Data[6];
                EyeColor = Data[7];
                Tan = Data[8];
                U9 = Data[9];

                Name = Encoding.Unicode.GetString(Data.Skip(0x6F3A).Take(0x12).ToArray()).Trim('\0');
                Gender = Data[0x6F4C];
                HomeTown = Encoding.Unicode.GetString(Data.Skip(0x6F50).Take(0x12).ToArray()).Trim('\0');

                try { JPEG = Image.FromStream(new MemoryStream(Data.Skip(0x5724).Take(0x1400).ToArray())); }
                catch { JPEG = null; }

                Badges = Data.Skip(0x569C).Take(24).ToArray();

                for (int i = 0; i < Pockets.Length; i++)
                    Pockets[i] = new Item(Data.Skip(0x6BB0 + i*4).Take(4).ToArray());

                for (int i = 0; i < IslandBox.Length; i++)
                    IslandBox[i] = new Item(Data.Skip(0x6E60 + i*4).Take(4).ToArray());

                for (int i = 0; i < Dressers.Length; i++)
                    Dressers[i] = new Item(Data.Skip(0x8E18 + i*4).Take(4).ToArray());
            }
            public byte[] Write()
            {
                Array.Copy(BitConverter.GetBytes(U32), 0, Data, 0, 4);
                Data[4] = Hair;
                Data[5] = HairColor;
                Data[6] = Face;
                Data[7] = EyeColor;
                Data[8] = Tan;
                Data[9] = U9;
                Data[0x6F4C] = (byte)Gender;

                Array.Copy(Encoding.Unicode.GetBytes(Name.PadRight(9, '\0')), 0, Data, 0x6F3A, 0x12);
                Array.Copy(Encoding.Unicode.GetBytes(HomeTown.PadRight(9, '\0')), 0, Data, 0x6F50, 0x12);

                Array.Copy(Badges, 0, Data, 0x569C, Badges.Length);

                for (int i = 0; i < Pockets.Length; i++)
                    Array.Copy(Pockets[i].Write(), 0, Data, 0x6BB0 + i*4, 4);

                for (int i = 0; i < IslandBox.Length; i++)
                    Array.Copy(IslandBox[i].Write(), 0, Data, 0x6E60 + i*4, 4);

                for (int i = 0; i < Dressers.Length; i++)
                    Array.Copy(Dressers[i].Write(), 0, Data, 0x8E18 + i*4, 4);

                return Data;
            }
        }
        class Building
        {
            public int X, Y, ID;

            public Building(byte[] data)
            {
                if (data.Length != 4) return;
                ID = BitConverter.ToUInt16(data, 0);
                X = data[2];
                Y = data[3];
            }
            public byte[] Write()
            {
                using(var ms = new MemoryStream())
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write((ushort)ID);
                    bw.Write((byte)X);
                    bw.Write((byte)Y);
                    return ms.ToArray();
                }
            }
        }
        class Villager
        {
            // Fetch from raw data
            public byte[] Data;
            public short ID;
            public byte Type;
            public string CatchPhrase;
            private string HomeTown1;
            private string HomeTown2;
            public Villager(byte[] data, int offset, int size)
            {
                Data = data.Skip(offset).Take(size).ToArray();

                ID = BitConverter.ToInt16(Data, 0);
                Type = Data[2];
                CatchPhrase = Encoding.Unicode.GetString(Data.Skip(0x24A6).Take(22).ToArray()).Trim('\0');
                HomeTown1 = Encoding.Unicode.GetString(Data.Skip(0x24CE).Take(0x12).ToArray()).Trim('\0');
                HomeTown2 = Encoding.Unicode.GetString(Data.Skip(0x24E4).Take(0x12).ToArray()).Trim('\0');
            }
            public byte[] Write()
            {
                Array.Copy(BitConverter.GetBytes(ID), 0, Data, 0, 2);
                Data[2] = Type;
                Array.Copy(Encoding.Unicode.GetBytes(CatchPhrase.PadRight(11, '\0')), 0, Data, 0x24A6, 22);
                Array.Copy(Encoding.Unicode.GetBytes(HomeTown1.PadRight(9, '\0')), 0, Data, 0x24CE, 0x12);
                Array.Copy(Encoding.Unicode.GetBytes(HomeTown2.PadRight(9, '\0')), 0, Data, 0x24E4, 0x12);
                return Data;
            }
        }
        class Item
        {
            public byte Flag1, Flag2;
            public bool Buried, Watered;
            public ushort ID;
            public Item(byte[] data)
            {
                ID = BitConverter.ToUInt16(data, 0);
                Flag1 = data[2];
                Flag2 = data[3];

                Watered = Flag2 >> 6 == 1;
                Buried = Flag2 >> 7 == 1;
            }
            public byte[] Write()
            {
                using (var ms = new MemoryStream())
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(ID);
                    bw.Write(Flag1);
                    Flag2 = (byte)((Flag2 & 0x3F) 
                        | (Watered ? 1 << 6 : 0) 
                        | (Buried ? 1 << 7 : 0));
                    bw.Write(Flag2);
                    return ms.ToArray();
                }
            }
        }

        // Data Usage
        private void loadData()
        {
            // Load Players
            Players = new Player[4];
            for (int i = 0; i < Players.Length; i++)
                Players[i] = new Player(Save.Data.Skip(0xA0 + i * 0x9F10).Take(0x9F10).ToArray());
            for (int i = 0; i < Players.Length; i++)
                PlayerPics[i].Image = Players[i].JPEG;

            // Temporary
            for (int i = 0; i < 1 /*Players.Length */; i++)
                loadPlayer(i);

            // Load Town
            TownAcreTiles = new ushort[TownAcres.Length];
            for (int i = 0; i < TownAcreTiles.Length; i++)
                TownAcreTiles[i] = BitConverter.ToUInt16(Save.Data, 0x4DA84 + i * 2);
            fillMapAcres(TownAcreTiles, TownAcres);
            TownItems = getMapItems(Save.Data.Skip(0x4DAD8).Take(0x5000).ToArray());
            fillTownItems(TownItems, TownAcres);

            // Load Island
            IslandAcreTiles = new ushort[IslandAcres.Length];
            for (int i = 0; i < IslandAcreTiles.Length; i++)
                IslandAcreTiles[i] = BitConverter.ToUInt16(Save.Data, 0x6A488 + i * 2);
            fillMapAcres(IslandAcreTiles, IslandAcres);
            IslandItems = getMapItems(Save.Data.Skip(0x6A4A8).Take(0x1000).ToArray());
            fillIslandItems(IslandItems, IslandAcres);

            // Load Buildings
            Buildings = new Building[58];
            for (int i = 0; i < Buildings.Length; i++)
                Buildings[i] = new Building(Save.Data.Skip(0x0495A8 + i * 4).Take(4).ToArray());

            // Load Villagers
            Villagers = new Villager[10];
            for (int i = 0; i < Villagers.Length; i++)
                loadVillager(i);

            // Load Overall
            {
                TB_TownName.Text = Save.TownName;
                CB_GrassShape.SelectedIndex = Save.GrassType;
                CB_NativeFruit.SelectedIndex = Save.NativeFruit;
                CB_TownHallColor.SelectedIndex = Save.TownHallColor;
                CB_TrainStationColor.SelectedIndex = Save.TrainStationColor;

                NUD_Seconds.Value = Save.SecondsPlayed % 60;
                NUD_Minutes.Value = (Save.SecondsPlayed / 60) % 60;
                NUD_Hours.Value = (Save.SecondsPlayed / 3600) % 24;
                NUD_Days.Value = (Save.SecondsPlayed / 86400) % 25000;

                NUD_OverallDays.Value = Save.PlayDays;
            }
            L_Info.Text = String.Format("{1}{0}{0}Inhabitants:{0}{2}{0}{3}{0}{4}{0}{5}", Environment.NewLine,
                Save.TownName,
                Players[0].Name, Players[1].Name, Players[2].Name, Players[3].Name);
            loaded = true;
        }
        private void saveData()
        {
            // Temporary
            for (int i = 0; i < 1 /*Players.Length */; i++)
                savePlayer(i);
            // Write Players
            for (int i = 0; i < Players.Length; i++)
                Array.Copy(Players[i].Write(), 0, Save.Data, 0xA0 + i * 0x9F10, 0x9F10);

            // Write Town
            for (int i = 0; i < TownAcreTiles.Length; i++) // Town Acres
                Array.Copy(BitConverter.GetBytes(TownAcreTiles[i]), 0, Save.Data, 0x4DA84 + i * 2, 2);
            for (int i = 0; i < TownItems.Length; i++) // Town Items
                Array.Copy(TownItems[i].Write(), 0, Save.Data, 0x4DAD8 + i * 4, 4);

            // Write Island
            for (int i = 0; i < IslandAcreTiles.Length; i++) // Island Acres
                Array.Copy(BitConverter.GetBytes(IslandAcreTiles[i]), 0, Save.Data, 0x6A488 + i * 2, 2);
            for (int i = 0; i < IslandItems.Length; i++) // Island Items
                Array.Copy(IslandItems[i].Write(), 0, Save.Data, 0x6A4A8 + i * 4, 4);

            // Write Buildings
            saveBuildingList();
            for (int i = 0; i < Buildings.Length; i++)
                Array.Copy(Buildings[i].Write(), 0, Save.Data, 0x0495A8 + i * 4, 4);

            // Write Villagers
            for (int i = 0; i < Villagers.Length; i++)
                saveVillager(i);

            // Write Overall
            {
                Save.TownName = TB_TownName.Text;
                Save.GrassType = CB_GrassShape.SelectedIndex;
                Save.NativeFruit = CB_NativeFruit.SelectedIndex;
                Save.TownHallColor = CB_TownHallColor.SelectedIndex;
                Save.TrainStationColor = CB_TrainStationColor.SelectedIndex;

                Save.SecondsPlayed = 0;
                Save.SecondsPlayed += (uint)NUD_Seconds.Value;
                Save.SecondsPlayed += (uint)NUD_Minutes.Value * 60;
                Save.SecondsPlayed += (uint)NUD_Hours.Value * 3600;
                Save.SecondsPlayed += (uint)NUD_Days.Value * 86400;

                Save.PlayDays = (ushort)NUD_OverallDays.Value;
            }
            // Finish
            Main.SaveData = Save.Write();
        }
        private void loadPlayer(int i)
        {
            PlayerPicsLarge[i].Image = Players[i].JPEG;
            PlayerPockets[i].Image = getItemPic(16, 16, Players[i].Pockets);
            PlayerDressers1[i].Image = getItemPic(16, 5, Players[i].Dressers.Take(Players[i].Dressers.Length / 2).ToArray());
            PlayerDressers2[i].Image = getItemPic(16, 5, Players[i].Dressers.Skip(Players[i].Dressers.Length / 2).ToArray());
            PlayerIslandBox[i].Image = getItemPic(16, 5, Players[i].IslandBox);
            PlayerNames[i].Text = Players[i].Name;
            for (int j = 0; j < PlayerBadges[i].Length; j++)
                PlayerBadges[i][j].SelectedIndex = Players[i].Badges[j];

            PlayerHairStyles[i].SelectedIndex = Players[i].Hair;
            PlayerHairColors[i].SelectedIndex = Players[i].HairColor;
            PlayerFaces[i].SelectedIndex = Players[i].Face;
            PlayerEyeColors[i].SelectedIndex = Players[i].EyeColor;
            PlayerTans[i].SelectedIndex = Players[i].Tan;
            PlayerGenders[i].SelectedIndex = Players[i].Gender;
        }
        private void savePlayer(int i)
        {
            Players[i].Name = PlayerNames[i].Text;
            for (int j = 0; j < PlayerBadges[i].Length; j++)
                Players[i].Badges[j] = (byte)PlayerBadges[i][j].SelectedIndex;

            Players[i].Hair = (byte)PlayerHairStyles[i].SelectedIndex;
            Players[i].HairColor = (byte)PlayerHairColors[i].SelectedIndex;
            Players[i].Face = (byte)PlayerFaces[i].SelectedIndex;
            Players[i].EyeColor = (byte)PlayerEyeColors[i].SelectedIndex;
            Players[i].Tan = (byte)PlayerTans[i].SelectedIndex;
            Players[i].Gender = (byte)PlayerGenders[i].SelectedIndex;
        }

        private void loadVillager(int i)
        {
            Villagers[i] = new Villager(Save.Data, 0x027D10 + 0x24F8 * i, 0x24F8);
            TownVillagers[i].Enabled = TownVillagersCatch[i].Enabled = (Villagers[i].ID != -1);
            TownVillagers[i].SelectedValue = (int)Villagers[i].ID;
            TownVillagersCatch[i].Text = Villagers[i].CatchPhrase;
        }
        private void saveVillager(int i)
        {
            Villagers[i].ID = (short)Util.getIndex(TownVillagers[i]);
            Villagers[i].CatchPhrase = TownVillagersCatch[i].Text;
            Array.Copy(Villagers[i].Write(), 0, Save.Data, 0x027D10 + 0x24F8 * i, 0x24F8);
        }

        // Utility
        private void fillMapAcres(ushort[] Acres, PictureBox[] Tiles)
        {
            for (int i = 0; i < Tiles.Length; i++)
                Tiles[i].BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("acre_" + Acres[i]);
        }
        private void fillTownItems(Item[] items, PictureBox[] Tiles)
        {
            int ctr = 0;
            for (int i = 0; i < Tiles.Length; i++)
            {
                if (i%7 == 0 || i/7 == 0 || i%7 == 6 || i/36 > 0) continue;
                Tiles[i].Image = getAcreItemPic(ctr, items);
                ctr++;
            }
        }
        private void fillIslandItems(Item[] items, PictureBox[] Tiles)
        {
            int ctr = 0;
            for (int i = 0; i < Tiles.Length; i++)
            {
                if (i % 4 == 0 || i / 4 == 0 || i % 4 == 3 || i / 12 > 0) continue;
                Tiles[i].Image = getAcreItemPic(ctr, items);
                ctr++;
            }
        }

        private Item[] getMapItems(byte[] itemData)
        {
            var items = new Item[itemData.Length / 4];
            for (int i = 0; i < items.Length; i++)
                items[i] = new Item(itemData.Skip(4*i).Take(4).ToArray());
            return items;
        }
        private bool getIsWeed(uint item)
        {
            return (item >= 0x7c && item <= 0x7f) || (item >= 0xcb && item <= 0xcd) || (item == 0xf8);
        }
        private bool getIsWilted(ushort item)
        {
            return (item >= 0xce && item <= 0xfb);
        }

        // Quick Cheats
        private void B_WaterFlowers_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != Util.Prompt(MessageBoxButtons.YesNo, "Water all flowers?"))
                return;
            int ctr = waterFlowers(ref TownItems);
            fillTownItems(TownItems, TownAcres);
            Util.Alert(String.Format("{0} flowers were watered!", ctr));
        }
        private void B_RemoveWeeds_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != Util.Prompt(MessageBoxButtons.YesNo, "Clear all weeds?"))
                return;
            int ctr = clearWeeds(ref TownItems);
            fillTownItems(TownItems, TownAcres);
            Util.Alert(String.Format("{0} weeds were cleared!", ctr));
        }
        private int waterFlowers(ref Item[] items)
        {
            int ctr = 0;
            foreach (Item i in items.Where(t => getIsWilted(t.ID)))
            {
                ctr++;
                i.Watered = true;
            }
            return ctr;
        }
        private int clearWeeds(ref Item[] items)
        {
            int ctr = 0;
            foreach (Item i in items.Where(t => getIsWeed(t.ID)))
            {
                ctr++;
                i.ID = 0x7FFE;
                i.Flag1 = 0;
                i.Flag2 = 0;
            }
            return ctr;
        }

        private Item currentItem = new Item(new byte[] {0xFE, 0x7F, 0, 0});
        private const int mapScale = 2;
        private void mouseTown(object sender, MouseEventArgs e)
        {
            int acre = Array.IndexOf(TownAcres, sender as PictureBox);
            int baseX = acre % 7;
            int baseY = acre / 7;

            int X = baseX * 16 + e.X / (4 * mapScale);
            int Y = baseY * 16 + e.Y / (4 * mapScale);

            // Get Base Acre

            int index = getItemIndex(X, Y, 5);
            Item item = TownItems[index];

            hoverItem(item, X, Y);
        }
        private void mouseIsland(object sender, MouseEventArgs e)
        {
            int acre = Array.IndexOf(IslandAcres, sender as PictureBox);
            int baseX = acre % 4;
            int baseY = acre / 4;

            int X = baseX * 16 + e.X / (4 * mapScale);
            int Y = baseY * 16 + e.Y / (4 * mapScale);

            // Get Base Acre
            int index = getItemIndex(X, Y, 2);
            Item item = IslandItems[index];

            hoverItem(item, X, Y);
        }
        private void clickTown(object sender, MouseEventArgs e)
        {
            int acre = Array.IndexOf(TownAcres, sender as PictureBox);
            int baseX = acre % 7;
            int baseY = acre / 7;

            int X = baseX * 16 + e.X / (4 * mapScale);
            int Y = baseY * 16 + e.Y / (4 * mapScale);

            // Get Base Acre
            int index = getItemIndex(X, Y, 5);

            if (e.Button == MouseButtons.Right) // Read
                reloadCurrentItem(TownItems[index]);
            else // Write
            {
                TownItems[index] = currentItem;
                int zX = (X - 16) / 16;
                int zY = (Y - 16) / 16;
                int zAcre = zX + zY * 5;
                TownAcres[acre].Image = getAcreItemPic(zAcre, TownItems);
            }
        }
        private void clickIsland(object sender, MouseEventArgs e)
        {
            int acre = Array.IndexOf(IslandAcres, sender as PictureBox);
            int baseX = acre % 4;
            int baseY = acre / 4;

            int X = baseX * 16 + e.X / (4 * mapScale);
            int Y = baseY * 16 + e.Y / (4 * mapScale);

            // Get Base Acre
            int index = getItemIndex(X, Y, 2);

            if (e.Button == MouseButtons.Right) // Read
                reloadCurrentItem(IslandItems[index]);
            else // Write
            {
                IslandItems[index] = currentItem;
                int zX = (X - 16) / 16;
                int zY = (Y - 16) / 16;
                int zAcre = zX + zY * 2;
                IslandAcres[acre].Image = getAcreItemPic(zAcre, IslandItems);
            }
        }
        private void mouseCustom(object sender, MouseEventArgs e)
        {
            int width = (sender as PictureBox).Width / 16; // 16pixels per item
            int X = e.X / (16);
            int Y = e.Y / (16);
            if (e.X == (sender as PictureBox).Width - 1 - 2) // tweak because the furthest pixel is unused for transparent effect, and 2 px are used for border
                X -= 1;
            if (e.Y == (sender as PictureBox).Height - 1 - 2)
                Y -= 1;

            // Get Base Acre
            int index = width * Y + X;

            bool pocket = Array.IndexOf(PlayerPockets, sender as PictureBox) > -1;
            bool dress1 = Array.IndexOf(PlayerDressers1, sender as PictureBox) > -1;
            bool dress2 = Array.IndexOf(PlayerDressers2, sender as PictureBox) > -1;
            bool island = Array.IndexOf(PlayerIslandBox, sender as PictureBox) > -1;
            bool dress = dress1 | dress2;

            int player = -1;
            if (pocket)
                player = Array.IndexOf(PlayerPockets, sender as PictureBox);
            else if (dress1)
                player = Array.IndexOf(PlayerDressers1, sender as PictureBox);
            else if (dress2)
                player = Array.IndexOf(PlayerDressers2, sender as PictureBox);
            else if (island)
                player = Array.IndexOf(PlayerIslandBox, sender as PictureBox);

            if (dress2) index += Players[player].Dressers.Length / 2;

            Item item = null;
            if (pocket)
                item = Players[player].Pockets[index];
            else if (dress)
                item = Players[player].Dressers[index];
            else if (island)
                item = Players[player].IslandBox[index];

            hoverItem(item, X, Y);
        }
        private void clickCustom(object sender, MouseEventArgs e)
        {
            int width = (sender as PictureBox).Width / 16; // 16pixels per item

            int X = e.X / (16);
            int Y = e.Y / (16);

            // Get Base Acre
            int index = width * Y + X;

            bool pocket = Array.IndexOf(PlayerPockets, sender as PictureBox) > -1;
            bool dress1 = Array.IndexOf(PlayerDressers1, sender as PictureBox) > -1;
            bool dress2 = Array.IndexOf(PlayerDressers2, sender as PictureBox) > -1;
            bool island = Array.IndexOf(PlayerIslandBox, sender as PictureBox) > -1;
            bool dress = dress1 | dress2;

            int player = -1;
            if (pocket)
                player = Array.IndexOf(PlayerPockets, sender as PictureBox);
            else if (dress1)
                player = Array.IndexOf(PlayerDressers1, sender as PictureBox);
            else if (dress2)
                player = Array.IndexOf(PlayerDressers2, sender as PictureBox);
            else if (island)
                player = Array.IndexOf(PlayerIslandBox, sender as PictureBox);

            if (dress2) index += Players[player].Dressers.Length / 2;

            if (e.Button == MouseButtons.Right) // Read
            {
                if (pocket)
                    reloadCurrentItem(Players[player].Pockets[index]);
                else if (dress)
                    reloadCurrentItem(Players[player].Dressers[index]);
                else if (island)
                    reloadCurrentItem(Players[player].IslandBox[index]);
            }
            else // Write
            {
                if (pocket)
                {
                    Players[player].Pockets[index] = currentItem;
                    PlayerPockets[player].Image = getItemPic(16, 16, Players[player].Pockets);
                }
                else if (dress)
                {
                    Players[player].Dressers[index] = currentItem;
                    if (dress1)
                        PlayerDressers1[player].Image = getItemPic(16, 5, Players[player].Dressers.Take(Players[player].Dressers.Length / 2).ToArray());
                    else
                        PlayerDressers2[player].Image = getItemPic(16, 5, Players[player].Dressers.Skip(Players[player].Dressers.Length / 2).ToArray());
                }
                else if (island)
                {
                    Players[player].IslandBox[index] = currentItem;
                    PlayerIslandBox[player].Image = getItemPic(16, 5, Players[player].IslandBox);
                }
            }
        }
        private void reloadCurrentItem(Item item)
        {
            currentItem = item;
            CB_Item.SelectedValue = (int)item.ID;
            TB_Flag1.Text = item.Flag1.ToString("X2");
            TB_Flag2.Text = item.Flag2.ToString("X2");
        }
        private void hoverItem(Item item, int X, int Y)
        {
            string itemName = Main.itemNames[item.ID];
            L_ItemHover.Text = String.Format("[0x{0}{1}{2}] {3}x{4}: {5}",
                item.Flag2.ToString("X2"), item.Flag1.ToString("X2"), item.ID.ToString("X4"),
                X.ToString("00"), Y.ToString("00"),
                itemName);
        }

        private int getItemIndex(int X, int Y, int width)
        {
            int zX = (X - 16) / 16;
            int zY = (Y - 16) / 16;
            int zAcre = zX + zY * width;
            int index = zAcre * 0x100 + (X % 16) + (Y % 16) * 0x10;
            return index;
        }
        private Image getAcreItemPic(int quadrant, Item[] items)
        {
            const int itemsize = 4 * mapScale;
            Bitmap b = new Bitmap(64 * mapScale, 64 * mapScale);
            for (int i = 0; i < 0x100; i++) // loop over acre data
            {
                int X = i % 16;
                int Y = i / 16;

                var item = items[quadrant*0x100 + i];
                if (item.ID == 0x7FFE)
                    continue; // skip this one.
                
                string itemType = getItemType(item.ID);
                Color itemColor = getItemColor(itemType);
                itemColor = Color.FromArgb(200, itemColor.R, itemColor.G, itemColor.B);

                // Plop into image
                for (int x = 0; x < itemsize*itemsize; x++)
                {
                    int rX = (X * itemsize + x % itemsize);
                    int rY = (Y * itemsize + x / itemsize);
                    b.SetPixel(rX, rY, itemColor);
                }
                // Buried
                if (item.Buried)
                {
                    for (int z = 2; z < itemsize - 1; z++)
                    {
                        b.SetPixel(X * itemsize + z, Y * itemsize + z, Color.Black);
                        b.SetPixel(X * itemsize + itemsize - z, Y * itemsize + z, Color.Black);
                    }
                }
            }
            for (int i = 0; i < b.Width * b.Height; i++) // slap on a grid
                if (i % (itemsize) == 0 || (i / (16 * itemsize)) % (itemsize) == 0)
                    b.SetPixel(i % (16 * itemsize), i / (16 * itemsize), Color.FromArgb(65, 0xFF, 0xFF, 0xFF));

            return b;
        }

        private string getItemType(ushort ID)
        {
            if (getIsWilted(ID)) return "wiltedflower";
            if (getIsWeed(ID)) return "weed";
            if (ID==0x009d) return "pattern";
	        if (ID>=0x9f && ID<=0xca) return "flower";
	        if (ID>=0x20a7 && ID<=0x2112) return "money";
	        if (ID>=0x98 && ID<=0x9c) return "rock";
	        if (ID>=0x2126 && ID<=0x2239) return "song";
	        if (ID>=0x223a && ID<=0x227a) return "paper";
	        if (ID>=0x227b && ID<=0x2285) return "turnip";
	        if (ID>=0x2286 && ID<=0x2341) return "catchable";
	        if ((ID>=0x2342 && ID<=0x2445) || ID==0x2119 || ID==0x211a) return "wallfloor";
	        if (ID>=0x2446 && ID<=0x28b1) return "clothes";
	        if (ID>=0x28b2 && ID<=0x2934) return "gyroids";
	        if (ID>=0x2e2c && ID<=0x2e2f) return "mannequin";
	        if (ID>=0x2e30 && ID<=0x2e8f) return "art";
	        if (ID>=0x2e90 && ID<=0x2ed2) return "fossil";
	        if (ID>=0x303b && ID<=0x307a) return "tool";
	        if (ID!=0x7ffe) return "furniture";

            return "unknown";
        }
        private Color getItemColor(string itemType)
        {
            switch (itemType)
            {
                case "furniture": return ColorTranslator.FromHtml("#3cde30");
                case "flower": return ColorTranslator.FromHtml("#ec67b8");
                case "wiltedflower": return ColorTranslator.FromHtml("#ac2778");
                case "pattern": return ColorTranslator.FromHtml("#877861");
                case "money": return Color.Yellow;
                case "rock": return Color.Black;
                case "song": return ColorTranslator.FromHtml("#a4ecb8");
                case "paper": return ColorTranslator.FromHtml("#a4ece8");
                case "turnip": return ColorTranslator.FromHtml("#bbac9d");
                case "catchable": return ColorTranslator.FromHtml("#bae33e");
                case "wallfloor": return ColorTranslator.FromHtml("#994040");
                case "clothes": return ColorTranslator.FromHtml("#2874aa");
                case "gyroids": return ColorTranslator.FromHtml("#d48324");
                case "mannequin": return ColorTranslator.FromHtml("#2e5570");
                case "art": return ColorTranslator.FromHtml("#cf540a");
                case "fossil": return ColorTranslator.FromHtml("#868686");
                case "tool": return ColorTranslator.FromHtml("#818181");
                case "tree": return Color.White;
                case "weed": return Color.Green;
            }
            return Color.Red;
        }

        private Image getItemPic(int itemsize, int itemsPerRow, Item[] items)
        {
            Bitmap b = new Bitmap(itemsize * itemsPerRow, itemsize * items.Length / itemsPerRow);
            for (int i = 0; i < items.Length; i++) // loop over acre data
            {
                int X = i % itemsPerRow;
                int Y = i / itemsPerRow;

                var item = items[i];
                if (item.ID == 0x7FFE)
                    continue; // skip this one.

                string itemType = getItemType(item.ID);
                Color itemColor = getItemColor(itemType);
                itemColor = Color.FromArgb(200, itemColor.R, itemColor.G, itemColor.B);

                // Plop into image
                for (int x = 0; x < itemsize * itemsize; x++)
                {
                    int rX = (X * itemsize + x / itemsize);
                    int rY = (Y * itemsize + x % itemsize);
                    b.SetPixel(rX, rY, itemColor);
                }
                // Buried
                if (item.Buried)
                {
                    for (int z = 2; z < itemsize - 1; z++)
                    {
                        b.SetPixel(X * itemsize + z, Y * itemsize + z, Color.Black);
                        b.SetPixel(X * itemsize + itemsize - z, Y * itemsize + z, Color.Black);
                    }
                }
            }
            for (int i = 0; i < b.Width * b.Height; i++) // slap on a grid
                if (i % (itemsize) == 0 || (i / (itemsize * itemsPerRow)) % (itemsize) == 0)
                    b.SetPixel(i % (itemsize * itemsPerRow), i / (itemsize * itemsPerRow), Color.FromArgb(25, 0x0, 0x0, 0x0));

            return b;
        }

        private void clickPlayerPic(object sender, EventArgs e)
        {
            int index = Array.IndexOf(PlayerPics, sender as PictureBox);
            index = 0; // mayor only right now
            tabControl1.SelectedIndex = 2 + index;
        }

        private void changeItemID(object sender, EventArgs e)
        {
            int index = Util.getIndex(CB_Item);
            currentItem.ID = (ushort)((index == 0) ? 0x7FFE : index);

            if (currentItem.ID == 0x7FFE)
            {
                TB_Flag1.Text = 0.ToString("X2");
                TB_Flag2.Text = 0.ToString("X2");
            }
            else
            L_CurrentItem.Text = String.Format("Current Item: [0x{0}{1}{2}]",
                currentItem.Flag2.ToString("X2"),
                currentItem.Flag1.ToString("X2"),
                currentItem.ID.ToString("X4"));
        }
        private void EnterKey(object sender, KeyPressEventArgs e)
        {
            // this will only allow valid hex values [0-9][a-f][A-F] to be entered. See ASCII table
            char c = e.KeyChar;
            if (!(c == '\b' || ('0' <= c && c <= '9') || ('A' <= c && c <= 'F'))) // et cetera
            {
                e.Handled = true;
            }
        }
        private void changeItemFlag(object sender, EventArgs e)
        {
            currentItem.Flag1 = Convert.ToByte(TB_Flag1.Text, 16);
            currentItem.Flag2 = Convert.ToByte(TB_Flag2.Text, 16);
            L_CurrentItem.Text = String.Format("Current Item: [0x{0}{1}{2}]", 
                currentItem.Flag2.ToString("X2"),
                currentItem.Flag1.ToString("X2"), 
                currentItem.ID.ToString("X4"));
        }

        private bool loaded;
        private void changeVillager(object sender, EventArgs e)
        {
            if (!loaded) return;
            int index = Array.IndexOf(TownVillagers, sender as ComboBox);
            int value = Util.getIndex(sender as ComboBox);
            if (index == -1 || value == -1) return;
            Villagers[index].Type = Main.villagerList[value].Type;

            if (DialogResult.Yes != Util.Prompt(MessageBoxButtons.YesNoCancel, String.Format("Do you want to reset villager {0}'s data? (furniture, clothes...)", index+1)))
                return;

            Array.Copy(Main.villagerList[value].DefaultBytes, 0, Villagers[index].Data, 0x244E, 88);
            TownVillagersCatch[index].Text = Main.villagerList[value].CatchPhrase;
        }

        private void saveBuildingList()
        {
            int itemcount = dataGridView1.Rows.Count;
            for (int i = 0; i < itemcount; i++)
            {
                int ID = (int)dataGridView1.Rows[i].Cells[0].Value;
                if (ID > 0xF8 || ID < 0)
                {
                    Buildings[i].ID = 0xF8;
                    Buildings[i].X = 0;
                    Buildings[i].Y = 0;
                }
                else
                {
                    Buildings[i].ID = ID;
                    try
                    { Buildings[i].X = Convert.ToUInt16(dataGridView1.Rows[i].Cells[1].Value.ToString()); }
                    catch { }
                    try
                    { Buildings[i].Y = Convert.ToUInt16(dataGridView1.Rows[i].Cells[2].Value.ToString()); }
                    catch { }
                }
            }
        }
        private void populateBuildingList()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            DataGridViewComboBoxColumn dgvItemVal = new DataGridViewComboBoxColumn
            {
                DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
                DisplayIndex = 0,
                DisplayMember = "Text",
                ValueMember = "Value",
                DataSource = Main.buildingList,
                Width = 150,
                FlatStyle = FlatStyle.Flat
            };
            DataGridViewColumn dgvX = new DataGridViewTextBoxColumn
            {
                HeaderText = "X",
                DisplayIndex = 1,
                Width = 45,
            };
            DataGridViewColumn dgvY = new DataGridViewTextBoxColumn
            {
                HeaderText = "Y",
                DisplayIndex = 2,
                Width = 45,
            };
            dgvX.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvY.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns.Add(dgvItemVal);
            dataGridView1.Columns.Add(dgvX);
            dataGridView1.Columns.Add(dgvY);

            dataGridView1.Rows.Add(Buildings.Length);
            dataGridView1.CancelEdit();

            string itemname = "";
            for (int i = 0; i < Buildings.Length; i++)
            {
                int itemvalue = Buildings[i].ID;
                try { itemname = Main.buildingNames[itemvalue]; }
                catch
                {
                    Util.Error("Unknown building detected.", "Building ID: " + itemvalue, "Building is after: " + itemname);
                    continue;
                }
                int itemarrayval = Array.IndexOf(Main.buildingNames, itemname);
                if (itemarrayval == -1)
                {
                    Buildings[i].ID = 0xFE;
                    Buildings[i].X = 0;
                    Buildings[i].Y = 0;
                    Util.Alert(itemname + " removed from Building List.", "If you save changes to Garden, the Building will no longer be in the Town.");
                }

                dataGridView1.Rows[i].Cells[0].Value = Buildings[i].ID;
                dataGridView1.Rows[i].Cells[1].Value = Buildings[i].X;
                dataGridView1.Rows[i].Cells[2].Value = Buildings[i].Y;
            }
        }
        private void dropclick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0) return;

            ComboBox comboBox = (ComboBox)dataGridView1.EditingControl;
            comboBox.DroppedDown = true;
        }
    }
}
