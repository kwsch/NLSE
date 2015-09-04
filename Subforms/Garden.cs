using System;
using System.Drawing;
using System.Drawing.Imaging;
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
        private PictureBox[] TownAcres, IslandAcres, PlayerPics;
        private ComboBox[] TownVillagers;
        private ComboBox[]PlayerBadges;
        private TextBox[] TownVillagersCatch;

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
            PlayerBadges = new[]
            {
                CB_Badge00, CB_Badge01, CB_Badge02, CB_Badge03, CB_Badge04,
                CB_Badge05, CB_Badge06, CB_Badge07, CB_Badge08, CB_Badge09,
                CB_Badge10, CB_Badge11, CB_Badge12, CB_Badge13, CB_Badge14,
                CB_Badge15, CB_Badge16, CB_Badge17, CB_Badge18, CB_Badge19,
                CB_Badge20, CB_Badge21, CB_Badge22, CB_Badge23,
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
            { PB_Pocket.MouseMove += mouseCustom; PB_Pocket.MouseClick += clickCustom; }
            { PB_Dresser1.MouseMove += mouseCustom; PB_Dresser1.MouseClick += clickCustom; }
            { PB_Dresser2.MouseMove += mouseCustom; PB_Dresser2.MouseClick += clickCustom; }
            { PB_Island.MouseMove += mouseCustom; PB_Island.MouseClick += clickCustom; }
            #endregion
            // Load
            loadData();
            reloadCurrentItem(currentItem);
        }
        private void B_Save_Click(object sender, EventArgs e)
        {
            Main.SaveData = saveData();
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
            loaded = false;
            loadData();
        }
        private void B_Export_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                FileName = "acnlram.bin",
                Filter = "RAM Dump|*.bin"
            };
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            byte[] RAM = saveData().Skip(0x80).ToArray();
            if (RAM.Length < 0x80000)
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

                Name = Encoding.Unicode.GetString(Data.Skip(0x55A8).Take(0x12).ToArray()).Trim('\0');
                Gender = Data[0x6F4C];
                HomeTown = Encoding.Unicode.GetString(Data.Skip(0x55BE).Take(0x12).ToArray()).Trim('\0');

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

                Array.Copy(Encoding.Unicode.GetBytes(Name.PadRight(9, '\0')), 0, Data, 0x55A8, 0x12);
                Array.Copy(Encoding.Unicode.GetBytes(HomeTown.PadRight(9, '\0')), 0, Data, 0x55BE, 0x12);

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
            Save = new GardenData(Main.SaveData);

            // Load Players
            Players = new Player[4];
            for (int i = 0; i < Players.Length; i++)
                Players[i] = new Player(Save.Data.Skip(0xA0 + i * 0x9F10).Take(0x9F10).ToArray());
            for (int i = 0; i < Players.Length; i++)
                PlayerPics[i].Image = Players[i].JPEG;

            loadPlayer(0);

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

            populateBuildingList();

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
            loaded = true;
        }
        private byte[] saveData()
        {
            savePlayer(currentPlayer);
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

            saveBuildingList();
            // Write Buildings
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
            return Save.Write();
        }

        private int currentPlayer = -1;
        private void loadPlayer(int i)
        {
            currentPlayer = i;
            PB_LPlayer0.Image = Players[i].JPEG;
            PB_Pocket.Image = getItemPic(16, 16, Players[i].Pockets);
            PB_Dresser1.Image = getItemPic(16, 5, Players[i].Dressers.Take(Players[i].Dressers.Length / 2).ToArray());
            PB_Dresser2.Image = getItemPic(16, 5, Players[i].Dressers.Skip(Players[i].Dressers.Length / 2).ToArray());
            PB_Island.Image = getItemPic(16, 5, Players[i].IslandBox);

            TB_Name.Text = Players[i].Name;
            for (int j = 0; j < PlayerBadges.Length; j++)
                PlayerBadges[j].SelectedIndex = Players[i].Badges[j];

            CB_HairStyle.SelectedIndex = Players[i].Hair;
            CB_HairColor.SelectedIndex = Players[i].HairColor;
            CB_FaceShape.SelectedIndex = Players[i].Face;
            CB_EyeColor.SelectedIndex = Players[i].EyeColor;
            CB_SkinColor.SelectedIndex = Players[i].Tan;
            CB_Gender.SelectedIndex = Players[i].Gender;
        }
        private void savePlayer(int i)
        {
            Players[i].Name = TB_Name.Text;
            for (int j = 0; j < PlayerBadges.Length; j++)
                Players[i].Badges[j] = (byte)PlayerBadges[j].SelectedIndex;

            Players[i].Hair = (byte)CB_HairStyle.SelectedIndex;
            Players[i].HairColor = (byte)CB_HairColor.SelectedIndex;
            Players[i].Face = (byte)CB_FaceShape.SelectedIndex;
            Players[i].EyeColor = (byte)CB_EyeColor.SelectedIndex;
            Players[i].Tan = (byte)CB_SkinColor.SelectedIndex;
            Players[i].Gender = (byte)CB_Gender.SelectedIndex;
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
            Villagers[i].ID = (TownVillagers[i].SelectedItem == null) 
                    ? (short)-1 
                    : (short)Util.getIndex(TownVillagers[i]);
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
                TownItems[index] = copyCurrentItem();
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
                IslandItems[index] = copyCurrentItem();
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

            var s = (sender as PictureBox);
            bool pocket = s == PB_Pocket;
            bool dress1 = s == PB_Dresser1;
            bool dress2 = s == PB_Dresser2;
            bool island = s == PB_Island;
            bool dress = dress1 | dress2;
            
            if (dress2) index += Players[currentPlayer].Dressers.Length / 2;

            Item item = null;
            if (pocket)
                item = Players[currentPlayer].Pockets[index];
            else if (dress)
                item = Players[currentPlayer].Dressers[index];
            else if (island)
                item = Players[currentPlayer].IslandBox[index];

            hoverItem(item, X, Y);
        }
        private void clickCustom(object sender, MouseEventArgs e)
        {
            int width = (sender as PictureBox).Width / 16; // 16pixels per item

            int X = e.X / (16);
            int Y = e.Y / (16);

            // Get Base Acre
            int index = width * Y + X;

            var s = (sender as PictureBox);
            bool pocket = s == PB_Pocket;
            bool dress1 = s == PB_Dresser1;
            bool dress2 = s == PB_Dresser2;
            bool island = s == PB_Island;
            bool dress = dress1 | dress2;
            
            if (dress2) index += Players[currentPlayer].Dressers.Length / 2;

            if (e.Button == MouseButtons.Right) // Read
            {
                if (pocket)
                    reloadCurrentItem(Players[currentPlayer].Pockets[index]);
                else if (dress)
                    reloadCurrentItem(Players[currentPlayer].Dressers[index]);
                else if (island)
                    reloadCurrentItem(Players[currentPlayer].IslandBox[index]);
            }
            else // Write
            {
                if (pocket)
                {
                    Players[currentPlayer].Pockets[index] = copyCurrentItem();
                    PB_Pocket.Image = getItemPic(16, 16, Players[currentPlayer].Pockets);
                }
                else if (dress)
                {
                    Players[currentPlayer].Dressers[index] = copyCurrentItem();
                    if (dress1)
                        PB_Dresser1.Image = getItemPic(16, 5, Players[currentPlayer].Dressers.Take(Players[currentPlayer].Dressers.Length / 2).ToArray());
                    else
                        PB_Dresser2.Image = getItemPic(16, 5, Players[currentPlayer].Dressers.Skip(Players[currentPlayer].Dressers.Length / 2).ToArray());
                }
                else if (island)
                {
                    Players[currentPlayer].IslandBox[index] = copyCurrentItem();
                    PB_Island.Image = getItemPic(16, 5, Players[currentPlayer].IslandBox);
                }
            }
        }
        private void reloadCurrentItem(Item item)
        {
            currentItem = new Item(item.Write());
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
        private Item copyCurrentItem()
        {
            return new Item(currentItem.Write());
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
            const int width = 64 * mapScale, height = 64*mapScale;
            byte[] bmpData = new byte[4 * ((width) * (height))];
            for (int i = 0; i < 0x100; i++) // loop over acre data
            {
                int X = i % 16;
                int Y = i / 16;

                var item = items[quadrant*0x100 + i];
                if (item.ID == 0x7FFE)
                    continue; // skip this one.
                
                string itemType = getItemType(item.ID);
                uint itemColor = getItemColor(itemType);

                // Plop into image
                for (int x = 0; x < itemsize*itemsize; x++)
                {
                    Buffer.BlockCopy(BitConverter.GetBytes(itemColor), 0, bmpData,
                        ((Y*itemsize + x/itemsize)*width*4) + ((X*itemsize + x%itemsize)*4), 4);
                }
                // Buried
                if (item.Buried)
                {
                    for (int z = 2; z < itemsize - 1; z++)
                    {
                        Buffer.BlockCopy(BitConverter.GetBytes(0xFF000000), 0, bmpData,
                            ((Y*itemsize + z)*width*4) + ((X*itemsize + z)*4), 4);
                        Buffer.BlockCopy(BitConverter.GetBytes(0xFF000000), 0, bmpData,
                            ((Y*itemsize + z)*width*4) + ((X*itemsize + itemsize - z)*4), 4);
                    }
                }
            }
            for (int i = 0; i < width * height; i++) // slap on a grid
                if (i % (itemsize) == 0 || (i / (16 * itemsize)) % (itemsize) == 0)
                    Buffer.BlockCopy(BitConverter.GetBytes(0x41FFFFFF), 0, bmpData,
                        ((i/(16*itemsize))*width*4) + ((i%(16*itemsize))*4), 4);

            Bitmap b = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData bData = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            System.Runtime.InteropServices.Marshal.Copy(bmpData, 0, bData.Scan0, bmpData.Length);
            b.UnlockBits(bData);
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
        private uint getItemColor(string itemType)
        {
            switch (itemType)
            {
                case "furniture": return 0xc83cde30;
                case "flower": return 0xc8ec67b8;
                case "wiltedflower": return 0xc8ac2778;
                case "pattern": return 0xc8877861;
                case "money": return 0xc8ffff00;
                case "rock": return 0xc8000000;
                case "song": return 0xc8a4ecb8;
                case "paper": return 0xc8a4ece8;
                case "turnip": return 0xc8bbac9d;
                case "catchable": return 0xc8bae33e;
                case "wallfloor": return 0xc8994040;
                case "clothes": return 0xc82874aa;
                case "gyroids": return 0xc8d48324;
                case "mannequin": return 0xc82e5570;
                case "art": return 0xc8cf540a;
                case "fossil": return 0xc8868686;
                case "tool": return 0xc8818181;
                case "tree": return 0xc8ffffff;
                case "weed": return 0xc8008000;
            }
            return 0xc8ff0000;
        }

        private Image getItemPic(int itemsize, int itemsPerRow, Item[] items)
        {
            int width = itemsize * itemsPerRow, height = itemsize * items.Length / itemsPerRow;
            byte[] bmpData = new byte[4 * ((width) * (height))];
            for (int i = 0; i < items.Length; i++) // loop over acre data
            {
                int X = i % itemsPerRow;
                int Y = i / itemsPerRow;

                var item = items[i];
                if (item.ID == 0x7FFE)
                    continue; // skip this one.

                string itemType = getItemType(item.ID);
                uint itemColor = getItemColor(itemType);

                // Plop into image
                for (int x = 0; x < itemsize * itemsize; x++)
                {
                    Buffer.BlockCopy(BitConverter.GetBytes(itemColor), 0, bmpData,
                        ((Y*itemsize + x%itemsize)*width*4) + ((X*itemsize + x/itemsize)*4), 4);
                }
                // Buried
                if (item.Buried)
                {
                    for (int z = 2; z < itemsize - 1; z++)
                    {
                        Buffer.BlockCopy(BitConverter.GetBytes(0xFF000000), 0, bmpData,
                            ((Y*itemsize + z)*width*4) + ((X*itemsize + z)*4), 4);
                        Buffer.BlockCopy(BitConverter.GetBytes(0xFF000000), 0, bmpData,
                            ((Y*itemsize + z)*width*4) + ((X*itemsize + itemsize - z)*4), 4);
                    }
                }
            }
            for (int i = 0; i < width * height; i++) // slap on a grid
                if (i%(itemsize) == 0 || (i/(itemsize*itemsPerRow))%(itemsize) == 0)
                    Buffer.BlockCopy(BitConverter.GetBytes(0x17000000), 0, bmpData,
                        ((i/(itemsize*itemsPerRow))*width*4) + ((i%(itemsize*itemsPerRow))*4), 4);

            Bitmap b = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData bData = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            System.Runtime.InteropServices.Marshal.Copy(bmpData, 0, bData.Scan0, bmpData.Length);
            b.UnlockBits(bData);
            return b;
        }

        private void clickPlayerPic(object sender, EventArgs e)
        {
            int index = Array.IndexOf(PlayerPics, sender as PictureBox);
            if (currentPlayer > -1)
                savePlayer(currentPlayer);

            loadPlayer(index);
            tabControl1.SelectedIndex = 2;
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
                Width = 215,
                FlatStyle = FlatStyle.Flat
            };
            DataGridViewColumn dgvX = new DataGridViewTextBoxColumn
            {
                HeaderText = "X",
                DisplayIndex = 1,
                Width = 35,
            };
            DataGridViewColumn dgvY = new DataGridViewTextBoxColumn
            {
                HeaderText = "Y",
                DisplayIndex = 2,
                Width = 35,
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

        private void reloadOverviewLabel()
        {
            L_Info.Text = String.Format("{1}{0}{0}Players:{0}{2}{0}{3}{0}{4}{0}{5}", Environment.NewLine,
                Save.TownName,
                Players[0].Name, Players[1].Name, Players[2].Name, Players[3].Name);
        }
        private void changePlayerName(object sender, EventArgs e)
        {
            Players[currentPlayer].Name = TB_Name.Text;
            reloadOverviewLabel();
        }
        private void changeTownName(object sender, EventArgs e)
        {
            Save.TownName = TB_TownName.Text;
            reloadOverviewLabel();
        }

        private void B_PWP_Click(object sender, EventArgs e)
        {
            byte[] PWPUnlock =
            {
                0xFF, 0xFF, 0xFF, 0xFF, // 0
                0xFF, 0xFF, 0xFF, 0xFF, // 1
                0xFF, 0xFF, 0xFF, 0xFF, // 2
                0xFF, 0xFF, 0xFF, 0xFF, // 3
                0xFF, 0xFF, 0xFF, 0xFF, // 4
                0x2A, 0xD6, 0xE4, 0x58, // 5
            };
            Array.Copy(PWPUnlock, 0, Save.Data, 0x4D9C8 + 0x80, PWPUnlock.Length);
            Util.Alert("All Public Works Projects unlocked!");
        }
    }
}
