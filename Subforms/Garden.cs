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
        private PictureBox[] TownAcres;
        private PictureBox[] IslandAcres;
        private PictureBox[] PlayerPics;
        private Player[] Players;
        private Building[] Buildings;
        private Villager[] Villagers;
        private Item[] TownItems, IslandItems;

        // Form Handling
        public Garden()
        {
            InitializeComponent();
            Save = new GardenData(Main.SaveData);
            TownAcres = new[]
            {
                PB_acre00, PB_acre10, PB_acre20, PB_acre30, PB_acre40, PB_acre50, PB_acre60,
                PB_acre01, PB_acre11, PB_acre21, PB_acre31, PB_acre41, PB_acre51, PB_acre61,
                PB_acre02, PB_acre12, PB_acre22, PB_acre32, PB_acre42, PB_acre52, PB_acre62,
                PB_acre03, PB_acre13, PB_acre23, PB_acre33, PB_acre43, PB_acre53, PB_acre63,
                PB_acre04, PB_acre14, PB_acre24, PB_acre34, PB_acre44, PB_acre54, PB_acre64,
                PB_acre05, PB_acre15, PB_acre25, PB_acre35, PB_acre45, PB_acre55, PB_acre65,
            };
            foreach (PictureBox p in TownAcres)
            {
                p.MouseMove += mouseTown;
                p.MouseClick += clickTown;
            }
            IslandAcres = new[]
            {
                PB_island00, PB_island10, PB_island20, PB_island30,
                PB_island01, PB_island11, PB_island21, PB_island31,
                PB_island02, PB_island12, PB_island22, PB_island32,
                PB_island03, PB_island13, PB_island23, PB_island33,
            };
            foreach (PictureBox p in IslandAcres)
            {
                p.MouseMove += mouseIsland;
                p.MouseClick += clickIsland;
            }
            PlayerPics = new[]
            {
                PB_JPEG0, PB_JPEG1, PB_JPEG2, PB_JPEG3
            };

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
            public DataRef TownName = new DataRef(0x5C7BA, 0x12);

            public byte[] Data;
            public GardenData(byte[] data)
            {
                Data = data;
            }
            public byte[] Write()
            {
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
            public string HomeTown;

            public Image JPEG;
            public byte[] Badges;
            public int[] Pockets = new int[16];
            public int[] IslandBox = new int[5 * 8];
            public int[] Dressers = new int[5 * 36];
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
                HomeTown = Encoding.Unicode.GetString(Data.Skip(0x6F50).Take(0x12).ToArray()).Trim('\0');

                try { JPEG = Image.FromStream(new MemoryStream(Data.Skip(0x5724).Take(0x1400).ToArray())); }
                catch { JPEG = null; }

                Badges = Data.Skip(0x569C).Take(20).ToArray();

                for (int i = 0; i < Pockets.Length; i++)
                    Pockets[i] = BitConverter.ToInt32(Data, 0x6BB0 + i*4);

                for (int i = 0; i < IslandBox.Length; i++)
                    IslandBox[i] = BitConverter.ToInt32(data, 0x6DC0 + i*4);

                for (int i = 0; i < Dressers.Length; i++)
                    Dressers[i] = BitConverter.ToInt32(Data, 0x8E10 + i*4);
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

                Array.Copy(Badges, 0, Data, 0x569C, Badges.Length);

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
            public int ID;
            public Villager(byte[] data, int offset, int size)
            {
                Data = data.Skip(offset).Take(size).ToArray();

                ID = BitConverter.ToUInt16(Data, 0);
            }
            public byte[] Write()
            {
                Array.Copy(BitConverter.GetBytes((ushort)ID), 0, Data, 0, 2);
                return Data;
            }
        }
        class Item
        {
            public byte Flag1, Flag2;
            public ushort ID;
            public Item(byte[] data)
            {
                ID = BitConverter.ToUInt16(data, 0);
                Flag1 = data[2];
                Flag2 = data[3];
            }
            public byte[] Write()
            {
                using (var ms = new MemoryStream())
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(ID);
                    bw.Write(Flag1);
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

            string Town = Save.TownName.getString(Save.Data);
            L_Info.Text = String.Format("{1}{0}{0}Inhabitants:{0}{2}{0}{3}{0}{4}{0}{5}", Environment.NewLine, 
                Town, 
                Players[0].Name, Players[1].Name, Players[2].Name, Players[3].Name);

            // Load Maps
            fillMapAcres(Save.Data, 0x4DA84, TownAcres);
            TownItems = getMapItems(Save.Data.Skip(0x4DAD8).Take(0x5000).ToArray());
            fillTownItems(TownItems, TownAcres);
            fillMapAcres(Save.Data, 0x6A488, IslandAcres);
            IslandItems = getMapItems(Save.Data.Skip(0x6A4A8).Take(0x1000).ToArray());
            fillIslandItems(IslandItems, IslandAcres);

            // Load Buildings
            Buildings = new Building[58];
            for (int i = 0; i < Buildings.Length; i++)
                Buildings[i] = new Building(Save.Data.Skip(0x0495A8 + i * 4).Take(4).ToArray());

            // Load Villagers
            Villagers = new Villager[10];
            for (int i = 0; i < Villagers.Length; i++)
                Villagers[i] = new Villager(Save.Data, 0x027D10 + 0x24F8 * i, 0x24F8);
        }
        private void saveData()
        {
            Main.SaveData = Save.Write();
        }

        // Utility
        private void fillMapAcres(byte[] acreData, int offset, PictureBox[] Tiles)
        {
            for (int i = 0; i < Tiles.Length; i++)
            {
                int file = BitConverter.ToUInt16(acreData, offset + i*2);
                Tiles[i].BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("acre_" + file);
            }
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
                i.Flag1 = 0x40;
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

            L_TownCoord.Text = String.Format("X: {1}{0}Y: {2}{0}Item: {3}", Environment.NewLine, X, Y, item.ID.ToString("X4"));
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

            L_IslandCoord.Text = String.Format("X: {1}{0}Y: {2}{0}Item: {3}", Environment.NewLine, X, Y, item.ID.ToString("X4"));
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
                choiceTownItem = TownItems[index]; // replace this with updating the item view
            else // Write
            {
                if (choiceTownItem == null) return;
                TownItems[index] = choiceTownItem;
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
                choiceIslandItem = IslandItems[index]; // replace this with updating the item view
            else // Write
            {
                if (choiceIslandItem == null) return;
                IslandItems[index] = choiceIslandItem;
                int zX = (X - 16) / 16;
                int zY = (Y - 16) / 16;
                int zAcre = zX + zY * 2;
                IslandAcres[acre].Image = getAcreItemPic(zAcre, IslandItems);
            }
        }

        private Item choiceTownItem = new Item(new byte[] {0xFE, 0x7F, 0, 0});
        private Item choiceIslandItem = new Item(new byte[] {0xFE, 0x7F, 0, 0});
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

                int index = quadrant*0x100 + X + Y*0x10;

                var item = items[index];
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
                case "song": return ColorTranslator.FromHtml("#a4ecb8)");
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
    }
}
