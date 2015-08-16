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
        private PictureBox[] mapAcres;
        private PictureBox[] islandAcres;
        private PictureBox[] playerPics;
        private Player[] Players;
        private Building[] Buildings;
        private Villager[] Villagers;

        // Form Handling
        public Garden()
        {
            InitializeComponent();
            Save = new GardenData(Main.SaveData);
            mapAcres = new[]
            {
                PB_acre00, PB_acre10, PB_acre20, PB_acre30, PB_acre40, PB_acre50, PB_acre60,
                PB_acre01, PB_acre11, PB_acre21, PB_acre31, PB_acre41, PB_acre51, PB_acre61,
                PB_acre02, PB_acre12, PB_acre22, PB_acre32, PB_acre42, PB_acre52, PB_acre62,
                PB_acre03, PB_acre13, PB_acre23, PB_acre33, PB_acre43, PB_acre53, PB_acre63,
                PB_acre04, PB_acre14, PB_acre24, PB_acre34, PB_acre44, PB_acre54, PB_acre64,
                PB_acre05, PB_acre15, PB_acre25, PB_acre35, PB_acre45, PB_acre55, PB_acre65,
            };
            islandAcres = new[]
            {
                PB_island00, PB_island10, PB_island20, PB_island30,
                PB_island01, PB_island11, PB_island21, PB_island31,
                PB_island02, PB_island12, PB_island22, PB_island32,
                PB_island03, PB_island13, PB_island23, PB_island33,
            };
            playerPics = new[]
            {
                PB_JPEG1, PB_JPEG2, PB_JPEG3, PB_JPEG4
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

        // Data Usage
        private void loadData()
        {
            // Load Players
            Players = new Player[4];
            for (int i = 0; i < Players.Length; i++)
                Players[i] = new Player(Save.Data.Skip(0xA0 + i * 0x9F10).Take(0x9F10).ToArray());

            for (int i = 0; i < Players.Length; i++)
                playerPics[i].Image = Players[i].JPEG;

            string Town = Save.TownName.getString(Save.Data);
            L_Info.Text = String.Format("{1}{0}{0}Inhabitants:{0}{2}{0}{3}{0}{4}{0}{5}", Environment.NewLine, 
                Town, 
                Players[0].Name, Players[1].Name, Players[2].Name, Players[3].Name);

            // Load Maps
            fillMap(Save.Data, 0x4DA84, mapAcres);
            fillMap(Save.Data, 0x6A488, islandAcres);

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
        private void fillMap(byte[] acreData, int offset, PictureBox[] Tiles)
        {
            for (int i = 0; i < Tiles.Length; i++)
            {
                int file = BitConverter.ToUInt16(acreData, offset + i*2);
                Tiles[i].Image = (Image)Properties.Resources.ResourceManager.GetObject("acre_" + file);
            }
        }
    }
}
