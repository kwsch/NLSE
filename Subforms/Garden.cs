// I have to say that most of the code from the functions I added have a bad code, I know it's not good if we want to maintain the project, or if you want to learn how some stuff are working, but at least stuff work as it should. I'll propably fix those bad codes when i'll have more experience.
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
        private ushort[] TownAcreTiles, IslandAcreTiles, aeTownAcreTiles, aeIslandAcreTiles;
        private PictureBox[] TownAcres, IslandAcres, aeTownAcres, aeIslandAcres, PlayerPics, VillagersPict;
        private ComboBox[] TownVillagers, PlayerBadges;
        private TextBox[] TownVillagersCatch;
        private CheckBox[] TownVillagersBoxed;

        private Player[] Players;
        private PlayerExterior[] PlayersExterior;
        private Building[] Buildings;
        private Villager[] Villagers;
        private Item[] TownItems, IslandItems;

        #region badgepict
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

        Image[] PlayerHair =
        {
            Properties.Resources.hair_M_1, Properties.Resources.hair_M_2, Properties.Resources.hair_M_3, Properties.Resources.hair_M_4,
            Properties.Resources.hair_M_5, Properties.Resources.hair_M_6, Properties.Resources.hair_M_7, Properties.Resources.hair_M_8,
            Properties.Resources.hair_M_9, Properties.Resources.hair_M_10, Properties.Resources.hair_M_11, Properties.Resources.hair_M_12,
            Properties.Resources.hair_M_13, Properties.Resources.hair_M_14, Properties.Resources.hair_M_15, Properties.Resources.hair_M_16,
            Properties.Resources.nothing,
            Properties.Resources.hair_F_1, Properties.Resources.hair_F_2, Properties.Resources.hair_F_3, Properties.Resources.hair_F_4,
            Properties.Resources.hair_F_5, Properties.Resources.hair_F_6, Properties.Resources.hair_F_7, Properties.Resources.hair_F_8,
            Properties.Resources.hair_F_9, Properties.Resources.hair_F_10, Properties.Resources.hair_F_11, Properties.Resources.hair_F_12,
            Properties.Resources.hair_F_13, Properties.Resources.hair_F_14, Properties.Resources.hair_F_15, Properties.Resources.hair_F_16,
            Properties.Resources.nothing,

        };

        Image[] PlayerFaceMale =
        {
            Properties.Resources.face_M_1, Properties.Resources.face_M_2, Properties.Resources.face_M_3, Properties.Resources.face_M_4,
            Properties.Resources.face_M_5, Properties.Resources.face_M_6, Properties.Resources.face_M_7, Properties.Resources.face_M_8,
            Properties.Resources.face_M_9, Properties.Resources.face_M_10, Properties.Resources.face_M_11, Properties.Resources.face_M_12,
        };

        Image[] PlayerFaceFemale =
        {
            Properties.Resources.face_F_1, Properties.Resources.face_F_2, Properties.Resources.face_F_3, Properties.Resources.face_F_4,
            Properties.Resources.face_F_5, Properties.Resources.face_F_6, Properties.Resources.face_F_7, Properties.Resources.face_F_8,
            Properties.Resources.face_F_9, Properties.Resources.face_F_10, Properties.Resources.face_F_11, Properties.Resources.face_F_12,
        };
        #endregion

        //bool
        private bool loaded;


        // Form Handling
        public Garden()
        {
            InitializeComponent();
            CB_Choice.SelectedIndex = 0;
            CB_WantedBadge.SelectedIndex = 0;
            DisableControl();
            loaded = false;
        }

        private void DisableControl()
        {
            tabControl1.Enabled = false;
            L_Info.Enabled = false;
            PB_JPEG0.Enabled = false;
            PB_JPEG1.Enabled = false;
            PB_JPEG2.Enabled = false;
            PB_JPEG3.Enabled = false;
            CB_Item.Enabled = false;
            TB_Flag1.Enabled = false;
            TB_Flag2.Enabled = false;
            L_CurrentItem.Enabled = false;
            L_ItemHover.Enabled = false;
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
        }

        private void EnableControl()
        {
            tabControl1.Enabled = true;
            L_Info.Enabled = true;
            PB_JPEG0.Enabled = true;
            PB_JPEG1.Enabled = true;
            PB_JPEG2.Enabled = true;
            PB_JPEG3.Enabled = true;
            CB_Item.Enabled = true;
            TB_Flag1.Enabled = true;
            TB_Flag2.Enabled = true;
            L_CurrentItem.Enabled = true;
            L_ItemHover.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            CB_Flag.SelectedIndex = 0;
        }

        uint GetDecryptedValue(uint player, int offset)
        {
            return (Util.DecryptACNLMoney(BitConverter.ToUInt64(Players[player].Data, offset)));
        }

        // Garden Save Editing
        private GardenData Save;
        class GardenData
        {
            public byte[] Data;
            public byte[] TownBytes;

            public string TownName;
            public int TownHallColor;
            public int TrainStationColor;
            public int GrassType;
            public int NativeFruit;
            public uint SecondsPlayed;
            public ushort PlayDays;
            public uint TRN_MondayAM;
            public uint TRN_MondayPM;
            public uint TRN_TuesdayAM;
            public uint TRN_TuesdayPM;
            public uint TRN_WednesdayAM;
            public uint TRN_WednesdayPM;
            public uint TRN_ThursdayAM;
            public uint TRN_ThursdayPM;
            public uint TRN_FridayAM;
            public uint TRN_FridayPM;
            public uint TRN_SaturdayAM;
            public uint TRN_SaturdayPM;
            public int Nookling;
            public int Leif;
            public int Kicks;
            public int ClubLOL;
            public int Fortune;
            public int DreamSuite;
            public int Shampoodle;
            public int SewingMachine;
            public int MuseumShop;
            public uint TreeSize;

            public GardenData(byte[] data)
            {
                Data = data;
                TownBytes = getTownBytes();
                TownName = Encoding.Unicode.GetString(Data.Skip(0x621BA).Take(0x12).ToArray()).Trim('\0');
                GrassType = Data[0x53481];
                TownHallColor = Data[0x621B8] & 3;
                TrainStationColor = Data[0x621B9] & 3;
                NativeFruit = Data[0x6223A];
                TreeSize = Data[0x4BE86];

                Nookling = Data[0x62264];
                Leif = Data[0x666F4];
                Kicks = Data[0x6682C];
                ClubLOL = Data[0x6AD82];
                Fortune = Data[0x6ADA4];
                DreamSuite = Data[0x6ADA2];
                Shampoodle = Data[0x6ADB4];
                MuseumShop = Data[0x6ACBC];
                SewingMachine = Data[0x621D5];

                SecondsPlayed = BitConverter.ToUInt32(Data, 0x621B0);
                PlayDays = BitConverter.ToUInt16(Data, 0x6223E);

                TRN_MondayAM = Util.DecryptACNLMoney(BitConverter.ToUInt64(Data, 0x6ADE0));
                TRN_MondayPM = Util.DecryptACNLMoney(BitConverter.ToUInt64(Data, 0x6ADE8));
                TRN_TuesdayAM = Util.DecryptACNLMoney(BitConverter.ToUInt64(Data, 0x6ADF0));
                TRN_TuesdayPM = Util.DecryptACNLMoney(BitConverter.ToUInt64(Data, 0x6ADF8));
                TRN_WednesdayAM = Util.DecryptACNLMoney(BitConverter.ToUInt64(Data, 0x6AE00));
                TRN_WednesdayPM = Util.DecryptACNLMoney(BitConverter.ToUInt64(Data, 0x6AE08));
                TRN_ThursdayAM = Util.DecryptACNLMoney(BitConverter.ToUInt64(Data, 0x6AE10));
                TRN_ThursdayPM = Util.DecryptACNLMoney(BitConverter.ToUInt64(Data, 0x6AE18));
                TRN_FridayAM = Util.DecryptACNLMoney(BitConverter.ToUInt64(Data, 0x6AE20));
                TRN_FridayPM = Util.DecryptACNLMoney(BitConverter.ToUInt64(Data, 0x6AE28));
                TRN_SaturdayAM = Util.DecryptACNLMoney(BitConverter.ToUInt64(Data, 0x6AE30));
                TRN_SaturdayPM = Util.DecryptACNLMoney(BitConverter.ToUInt64(Data, 0x6AE38));
            }
            public byte[] Write()
            {
                Data[0x621B8] = (byte)(Data[0x621B8] & 0xFC | TownHallColor);
                Data[0x621B9] = (byte)(Data[0x621B9] & 0xFC | TrainStationColor);
                Array.Copy(Encoding.Unicode.GetBytes(TownName.PadRight(9, '\0')), 0, Data, 0x621BA, 0x12);

                Data[0x4BE86] = (byte)TreeSize;

                Data[0x53481] = (byte)GrassType;
                Data[0x6223A] = (byte)NativeFruit;

                Data[0x62264] = (byte)Nookling;
                Data[0x62265] = (byte)Nookling;

                Data[0x666F4] = (byte)Leif;

                Data[0x6682C] = (byte)Kicks;
                Data[0x6AD82] = (byte)ClubLOL;
                Data[0x6ADA4] = (byte)Fortune;
                Data[0x6ADA2] = (byte)DreamSuite;
                Data[0x6ADB4] = (byte)Shampoodle;
                Data[0x6ACBC] = (byte)MuseumShop;
                Data[0x621D5] = (byte)SewingMachine;


                Array.Copy(BitConverter.GetBytes(SecondsPlayed), 0, Data, 0x621B0, 4);
                Array.Copy(BitConverter.GetBytes(PlayDays), 0, Data, 0x6223E, 2);

                Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney(TRN_MondayAM)), 0, Data, 0x6ADE0, 8);
                Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney(TRN_MondayPM)), 0, Data, 0x6ADE8, 8);
                Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney(TRN_TuesdayAM)), 0, Data, 0x6ADF0, 8);
                Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney(TRN_TuesdayPM)), 0, Data, 0x6ADF8, 8);
                Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney(TRN_WednesdayAM)), 0, Data, 0x6AE00, 8);
                Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney(TRN_WednesdayPM)), 0, Data, 0x6AE08, 8);
                Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney(TRN_ThursdayAM)), 0, Data, 0x6AE10, 8);
                Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney(TRN_ThursdayPM)), 0, Data, 0x6AE18, 8);
                Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney(TRN_FridayAM)), 0, Data, 0x6AE20, 8);
                Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney(TRN_FridayPM)), 0, Data, 0x6AE28, 8);
                Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney(TRN_SaturdayAM)), 0, Data, 0x6AE30, 8);
                Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney(TRN_SaturdayPM)), 0, Data, 0x6AE38, 8);

                return Data;
            }
            public byte[] getTownBytes()
            {
                return Data.Skip(0x621B8).Take(0x14).ToArray();
            }
        }
        class Player
        {
            public byte[] Data;
            public byte[] PlayerBytes;

            private uint U32;
            public byte Hair, HairColor,
                Face, EyeColor,
                Tan, U9;

            public string Name;
            public string Comment;
            public int Gender;
            public int BirthdayDay;
            public int BirthdayMonth;
            public int RegDay;
            public int RegMonth;
            public uint RegYear;
            public string HomeTown;

            public Image JPEG;
            public byte[] Badges;
            public byte[] Letters;
            public Item[] Pockets = new Item[16];
            public Item[] IslandBox = new Item[5 * 8];
            public Item[] Dressers = new Item[5 * 36];

            public Player(byte[] data)
            {
                Data = data;
                PlayerBytes = getPlayerBytes();

                U32 = BitConverter.ToUInt32(data, 0);
                Hair = Data[4];
                HairColor = Data[5];
                Face = Data[6];
                EyeColor = Data[7];
                Tan = Data[8];
                U9 = Data[9];
                Name = Encoding.Unicode.GetString(Data.Skip(0x55A8).Take(0x12).ToArray()).Trim('\0');
                Comment = Encoding.Unicode.GetString(Data.Skip(0x6B38).Take(0x50).ToArray()).Trim('\0');

                Gender = Data[0x55BA];

                BirthdayMonth = Data[0x55D4];
                BirthdayDay = Data[0x55D5];

                RegDay = Data[0x55D9];
                RegMonth = Data[0x55D8];
                RegYear = BitConverter.ToUInt16(data, 0x55D6);

                HomeTown = Encoding.Unicode.GetString(Data.Skip(0x55BE).Take(0x12).ToArray()).Trim('\0');

                try { JPEG = Image.FromStream(new MemoryStream(Data.Skip(0x5738).Take(0x1400).ToArray())); }
                catch { JPEG = null; }

                Badges = Data.Skip(0x573C - 0xA0).Take(24).ToArray();

                Letters = Data.Skip(0x70A8 - 0xA0).Take(0x1900).ToArray();

                for (int i = 0; i < Pockets.Length; i++)
                    Pockets[i] = new Item(Data.Skip(0x6BD0 + i * 4).Take(4).ToArray());

                for (int i = 0; i < IslandBox.Length; i++)
                    IslandBox[i] = new Item(Data.Skip(0x6F10 + i * 4).Take(4).ToArray());

                for (int i = 0; i < Dressers.Length; i++)
                    Dressers[i] = new Item(Data.Skip(0x92F0 + i * 4).Take(4).ToArray());
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

                Array.Copy(Encoding.Unicode.GetBytes(Name.PadRight(9, '\0')), 0, Data, 0x55A8, 0x12);
                Array.Copy(Encoding.Unicode.GetBytes(Comment.PadRight(40, '\0')), 0, Data, 0x6B38, 0x50);

                Data[0x55BA] = (byte)Gender;

                Data[0x55D4] = (byte)BirthdayMonth;
                Data[0x55D5] = (byte)BirthdayDay;

                Data[0x55D9] = (byte)RegDay;
                Data[0x55D8] = (byte)RegMonth;
                Data[0x55D6] = (byte)RegYear;

                Array.Copy(Encoding.Unicode.GetBytes(HomeTown.PadRight(9, '\0')), 0, Data, 0x55BE, 0x12);

                Array.Copy(Badges, 0, Data, 0x569C, Badges.Length);

                Array.Copy(Letters, 0, Data, 0x70A8 - 0xA0, Letters.Length);

                for (int i = 0; i < Pockets.Length; i++)
                    Array.Copy(Pockets[i].Write(), 0, Data, 0x6BD0 + i * 4, 4);

                for (int i = 0; i < IslandBox.Length; i++)
                    Array.Copy(IslandBox[i].Write(), 0, Data, 0x6F10 + i * 4, 4);

                for (int i = 0; i < Dressers.Length; i++)
                    Array.Copy(Dressers[i].Write(), 0, Data, 0x92F0 + i * 4, 4);

                return Data;
            }

            public byte[] getPlayerBytes()
            {
                return Data.Skip(0x55A6).Take(0x2E).ToArray();
            }
        }

        class PlayerExterior
        {
            public byte[] Data;

            private uint U32;
            public int HouseStyle;
            public int HouseBrick;
            public int HouseRoof;
            public int HouseDoorForm;
            public int HouseDoor;
            public int HouseFence;
            public int HousePavement;
            public int HouseMailBox;

            public int MainRoomSize;
            public int UpstairsSize;
            public int BasementSize;
            public int RightRoomSize;
            public int LeftRoomSize;
            public int BackRoomSize;
            public int HouseSize;

            public PlayerExterior(byte[] data)
            {
                Data = data;

                U32 = BitConverter.ToUInt32(data, 0);

                HouseStyle = Data[0x5D905 - 0x5D904];
                HouseDoorForm = Data[0x5D906 - 0x5D904];
                HouseBrick = Data[0x5D907 - 0x5D904];
                HouseRoof = Data[0x5D908 - 0x5D904];
                HouseDoor = Data[0x5D909 - 0x5D904];
                HouseFence = Data[0x5D90A - 0x5D904];
                HousePavement = Data[0x5D90B - 0x5D904];
                HouseMailBox = Data[0x5D90C - 0x5D904];

                MainRoomSize = Data[0x5D936 - 0x5D904];
                UpstairsSize = Data[0x5DC38 - 0x5D904];
                BasementSize = Data[0x5DF3A - 0x5D904];
                RightRoomSize = Data[0x5E23C - 0x5D904];
                LeftRoomSize = Data[0x5E53E - 0x5D904];
                BackRoomSize = Data[0x5E840 - 0x5D904];
                HouseSize = Data[0x5D904 - 0x5D904];
            }
            public byte[] Write()
            {
                Array.Copy(BitConverter.GetBytes(U32), 0, Data, 0, 4);

                Data[0x5D905 - 0x5D904] = (byte)HouseStyle;
                Data[0x5D90E - 0x5D904] = (byte)HouseStyle;

                Data[0x5D906 - 0x5D904] = (byte)HouseDoorForm;
                Data[0x5D90F - 0x5D904] = (byte)HouseDoorForm;

                Data[0x5D907 - 0x5D904] = (byte)HouseBrick;
                Data[0x5D910 - 0x5D904] = (byte)HouseBrick;

                Data[0x5D908 - 0x5D904] = (byte)HouseRoof;
                Data[0x5D911 - 0x5D904] = (byte)HouseRoof;

                Data[0x5D909 - 0x5D904] = (byte)HouseDoor;
                Data[0x5D912 - 0x5D904] = (byte)HouseDoor;

                Data[0x5D90A - 0x5D904] = (byte)HouseFence;
                Data[0x5D913 - 0x5D904] = (byte)HouseFence;

                Data[0x5D90B - 0x5D904] = (byte)HousePavement;
                Data[0x5D914 - 0x5D904] = (byte)HousePavement;

                Data[0x5D90C - 0x5D904] = (byte)HouseMailBox;
                Data[0x5D915 - 0x5D904] = (byte)HouseMailBox;

                Data[0x5D904 - 0x5D904] = (byte)HouseSize;
                Data[0x5D90D - 0x5D904] = (byte)HouseSize;

                Data[0x5D936 - 0x5D904] = (byte)MainRoomSize;
                Data[0x5DC38 - 0x5D904] = (byte)UpstairsSize;
                Data[0x5DF3A - 0x5D904] = (byte)BasementSize;
                Data[0x5E23C - 0x5D904] = (byte)RightRoomSize;
                Data[0x5E53E - 0x5D904] = (byte)LeftRoomSize;
                Data[0x5E840 - 0x5D904] = (byte)BackRoomSize;

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
                using (var ms = new MemoryStream())
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
            public bool Boxed;
            public Villager(byte[] data, int offset, int size)
            {
                Data = data.Skip(offset).Take(size).ToArray();

                ID = BitConverter.ToInt16(Data, 0);
                Type = Data[2];
                Boxed = (Data[0x24E4] & 1) == 1;
                CatchPhrase = Encoding.Unicode.GetString(Data.Skip(0x24C6).Take(22).ToArray()).Trim('\0');
                HomeTown1 = Encoding.Unicode.GetString(Data.Skip(0x24F0).Take(0x12).ToArray()).Trim('\0');
                HomeTown2 = Encoding.Unicode.GetString(Data.Skip(0x2504).Take(0x12).ToArray()).Trim('\0');
            }
            public byte[] Write()
            {
                Array.Copy(BitConverter.GetBytes(ID), 0, Data, 0, 2);
                Data[2] = Type;
                Data[0x24E4] = (byte)((Data[0x24E4] & ~1) | (Boxed ? 1 : 0));
                Array.Copy(Encoding.Unicode.GetBytes(CatchPhrase.PadRight(11, '\0')), 0, Data, 0x24C6, 22);
                Array.Copy(Encoding.Unicode.GetBytes(HomeTown1.PadRight(9, '\0')), 0, Data, 0x24F0, 0x12);
                Array.Copy(Encoding.Unicode.GetBytes(HomeTown2.PadRight(9, '\0')), 0, Data, 0x2504, 0x12);
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
                    // Flag2 = (byte)((Flag2 & 0x3F)
                    //    | (Watered ? 1 << 6 : 0)
                    //    | (Buried ? 1 << 7 : 0));
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
                Players[i] = new Player(Save.Data.Skip(0xA0 + i * 0xA480).Take(0xA480).ToArray());
            for (int i = 0; i < Players.Length; i++)
                PlayerPics[i].Image = Players[i].JPEG;

            PlayersExterior = new PlayerExterior[4];
            for (int i = 0; i < PlayersExterior.Length; i++)
                PlayersExterior[i] = new PlayerExterior(Save.Data.Skip(0x5D904 + i * 0x1228).Take(0x1228).ToArray());

            loadPlayer(0);

            // Load Town
            TownAcreTiles = new ushort[TownAcres.Length];
            for (int i = 0; i < TownAcreTiles.Length; i++)
                TownAcreTiles[i] = BitConverter.ToUInt16(Save.Data, 0x53484 + i * 2);
            fillMapAcres(TownAcreTiles, TownAcres);
            aeTownAcreTiles = (ushort[])TownAcreTiles.Clone();
            fillMapAcres(aeTownAcreTiles, aeTownAcres);
            TownItems = getMapItems(Save.Data.Skip(0x534D8).Take(0x5000).ToArray());
            fillTownItems(TownItems, TownAcres);

            // Load Island
            IslandAcreTiles = new ushort[IslandAcres.Length];
            for (int i = 0; i < IslandAcreTiles.Length; i++)
                IslandAcreTiles[i] = BitConverter.ToUInt16(Save.Data, 0x6FEB8 + i * 2);
            fillMapAcres(IslandAcreTiles, IslandAcres);
            aeIslandAcreTiles = (ushort[])IslandAcreTiles.Clone();
            fillMapAcres(aeIslandAcreTiles, aeIslandAcres);
            IslandItems = getMapItems(Save.Data.Skip(0x6FED8).Take(0x1000).ToArray());
            fillIslandItems(IslandItems, IslandAcres);

            // Load Buildings
            Buildings = new Building[58];
            for (int i = 0; i < Buildings.Length; i++)
                Buildings[i] = new Building(Save.Data.Skip(0x4BE88 + i * 4).Take(4).ToArray());

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
                NUD_TreeSize.Value = Save.TreeSize;

                CB_Nookling.SelectedIndex = Save.Nookling;

                NUD_Seconds.Value = Save.SecondsPlayed % 60;
                NUD_Minutes.Value = (Save.SecondsPlayed / 60) % 60;
                NUD_Hours.Value = (Save.SecondsPlayed / 3600) % 24;
                NUD_Days.Value = (Save.SecondsPlayed / 86400) % 25000;

                NUD_OverallDays.Value = Save.PlayDays;

                NUD_MonAM.Value = Save.TRN_MondayAM;
                NUD_MonPM.Value = Save.TRN_MondayPM;
                NUD_TueAM.Value = Save.TRN_TuesdayAM;
                NUD_TuePM.Value = Save.TRN_TuesdayPM;
                NUD_WedAM.Value = Save.TRN_WednesdayAM;
                NUD_WedPM.Value = Save.TRN_WednesdayPM;
                NUD_ThursAM.Value = Save.TRN_ThursdayAM;
                NUD_ThursPM.Value = Save.TRN_ThursdayPM;
                NUD_FriAM.Value = Save.TRN_FridayAM;
                NUD_FriPM.Value = Save.TRN_FridayPM;
                NUD_SatAM.Value = Save.TRN_SaturdayAM;
                NUD_SatPM.Value = Save.TRN_SaturdayPM;

                CB_StreetKicks.Checked = Save.Kicks == 2;
                CB_StreetClubLOL.Checked = Save.ClubLOL == 2;
                CB_StreetDream.Checked = Save.DreamSuite == 1;
                CB_StreetFortune.Checked = Save.Fortune == 1;
                CB_StreetShampoodle.Checked = Save.Shampoodle == 2;
                CB_StreetMuseum.Checked = Save.MuseumShop == 1;
                CB_StreetSewing.Checked = Save.SewingMachine == 0x80;

                checkPlayer();
            }
        }
        private byte[] saveData()
        {
            savePlayer(currentPlayer);
            // Write Players
            for (int i = 0; i < Players.Length; i++)
                Array.Copy(Players[i].Write(), 0, Save.Data, 0xA0 + i * 0xA480, 0xA480);

            // Write Players Exterior
            for (int i = 0; i < Players.Length; i++)
                Array.Copy(PlayersExterior[i].Write(), 0, Save.Data, 0x5D904 + i * 0x1228, 0x1228);

            // Write Town
            for (int i = 0; i < TownAcreTiles.Length; i++) // Town Acres
                Array.Copy(BitConverter.GetBytes(TownAcreTiles[i]), 0, Save.Data, 0x53484 + i * 2, 2);
            for (int i = 0; i < TownItems.Length; i++) // Town Items
                Array.Copy(TownItems[i].Write(), 0, Save.Data, 0x534D8 + i * 4, 4);

            // Write Island
            for (int i = 0; i < IslandAcreTiles.Length; i++) // Island Acres
                Array.Copy(BitConverter.GetBytes(IslandAcreTiles[i]), 0, Save.Data, 0x6FEB8 + i * 2, 2);
            for (int i = 0; i < IslandItems.Length; i++) // Island Items
                Array.Copy(IslandItems[i].Write(), 0, Save.Data, 0x6FED8 + i * 4, 4);

            saveBuildingList();
            // Write Buildings
            for (int i = 0; i < Buildings.Length; i++)
                Array.Copy(Buildings[i].Write(), 0, Save.Data, 0x4BE88 + i * 4, 4);

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
                Save.Nookling = CB_Nookling.SelectedIndex;
                Save.SecondsPlayed = 0;
                Save.SecondsPlayed += (uint)NUD_Seconds.Value;
                Save.SecondsPlayed += (uint)NUD_Minutes.Value * 60;
                Save.SecondsPlayed += (uint)NUD_Hours.Value * 3600;
                Save.SecondsPlayed += (uint)NUD_Days.Value * 86400;

                Save.TreeSize = (uint)NUD_TreeSize.Value;

                Save.PlayDays = (ushort)NUD_OverallDays.Value;

                Save.TRN_MondayAM = (uint)NUD_MonAM.Value;
                Save.TRN_MondayPM = (uint)NUD_MonPM.Value;

                Save.TRN_TuesdayAM = (uint)NUD_TueAM.Value;
                Save.TRN_TuesdayPM = (uint)NUD_TuePM.Value;

                Save.TRN_WednesdayAM = (uint)NUD_WedAM.Value;
                Save.TRN_WednesdayPM = (uint)NUD_WedPM.Value;

                Save.TRN_ThursdayAM = (uint)NUD_ThursAM.Value;
                Save.TRN_ThursdayPM = (uint)NUD_ThursPM.Value;

                Save.TRN_FridayAM = (uint)NUD_FriAM.Value;
                Save.TRN_FridayPM = (uint)NUD_FriPM.Value;

                Save.TRN_SaturdayAM = (uint)NUD_SatAM.Value;
                Save.TRN_SaturdayPM = (uint)NUD_SatPM.Value;

                Save.Leif = CB_Nookling.SelectedIndex;
                if (Save.Leif == 1) Save.Leif += 1;

                Save.Kicks = CB_StreetKicks.Checked == true ? 2 : 0;
                Save.ClubLOL = CB_StreetClubLOL.Checked == true ? 2 : 0;
                Save.DreamSuite = CB_StreetDream.Checked == true ? 1 : 0;
                Save.Fortune = CB_StreetFortune.Checked == true ? 1 : 0;
                Save.Shampoodle = CB_StreetShampoodle.Checked == true ? 2 : 0;
                Save.MuseumShop = CB_StreetMuseum.Checked == true ? 1 : 0;
                Save.SewingMachine = CB_StreetSewing.Checked == true ? 0x80 : 0;
            }

            byte[] finalData = (byte[])Save.Write().Clone();
            {

                // Update ID references with their current values
                for (int i = 0; i < 4; i++)
                    Util.ReplaceAllBytes(finalData, Players[i].PlayerBytes, Players[i].getPlayerBytes());

                Util.ReplaceAllBytes(finalData, Save.TownBytes, Save.getTownBytes());
            }
            return finalData;
        }

        private int currentPlayer = -1;
        private void loadPlayer(int i)
        {
            currentPlayer = i;
            PB_LPlayer0.Image = PlayerPics[i].Image;
            PB_Pocket.Image = getItemPic(16, 16, Players[i].Pockets);
            PB_Dresser1.Image = getItemPic(16, 5, Players[i].Dressers.Take(Players[i].Dressers.Length / 2).ToArray());
            PB_Dresser2.Image = getItemPic(16, 5, Players[i].Dressers.Skip(Players[i].Dressers.Length / 2).ToArray());
            PB_Island.Image = getItemPic(16, 5, Players[i].IslandBox);


            TB_Name.Text = Players[i].Name;
            TB_Comment.Text = Players[i].Comment;

            for (int j = 0; j < PlayerBadges.Length; j++)
                PlayerBadges[j].SelectedIndex = Players[i].Badges[j];
            loadBadge();

            CB_HairStyle.SelectedIndex = Players[i].Hair;
            CB_HairColor.SelectedIndex = Players[i].HairColor;
            CB_FaceShape.SelectedIndex = Players[i].Face;
            CB_EyeColor.SelectedIndex = Players[i].EyeColor;
            CB_SkinColor.SelectedIndex = Players[i].Tan;
            CB_Gender.SelectedIndex = Players[i].Gender;

            NUD_BirthdayDay.Value = Players[i].BirthdayDay;
            CB_BirthdayMonth.SelectedIndex = Players[i].BirthdayMonth;

            NUD_RegDay.Value = Players[i].RegDay;
            CB_RegMonth.SelectedIndex = Players[i].RegMonth;
            NUD_RegYear.Value = Players[i].RegYear;

            CB_HouseStyle.SelectedIndex = PlayersExterior[i].HouseStyle;
            CB_HouseBrick.SelectedIndex = PlayersExterior[i].HouseBrick;
            CB_HousePavement.SelectedIndex = PlayersExterior[i].HousePavement;
            CB_HouseRoof.SelectedIndex = PlayersExterior[i].HouseRoof;
            CB_HouseFence.SelectedIndex = PlayersExterior[i].HouseFence;
            CB_HouseDoor.SelectedIndex = PlayersExterior[i].HouseDoor;
            CB_HouseDoorForm.SelectedIndex = PlayersExterior[i].HouseDoorForm;
            CB_HouseMailBox.SelectedIndex = PlayersExterior[i].HouseMailBox;

            CB_HouseSize.SelectedIndex = PlayersExterior[i].HouseSize;
            CB_MainRoomSize.SelectedIndex = PlayersExterior[i].MainRoomSize - 1;
            CB_UpstairsSize.SelectedIndex = PlayersExterior[i].UpstairsSize - 1;
            CB_BasementSize.SelectedIndex = PlayersExterior[i].BasementSize - 1;
            CB_RightRoomSize.SelectedIndex = PlayersExterior[i].RightRoomSize - 1;
            CB_LeftRoomSize.SelectedIndex = PlayersExterior[i].LeftRoomSize - 1;
            CB_BackRoomSize.SelectedIndex = PlayersExterior[i].BackRoomSize - 1;

            // Load encrypted $ value
            NUD_Money.Value = GetDecryptedValue((uint)i, 0x6B8C);
            NUD_PocketMoney.Value = GetDecryptedValue((uint)i, 0x6FA8 - 0xA0);
            NUD_IslandMedals.Value = GetDecryptedValue((uint)i, 0x6B9C);
            NUD_MEOW.Value = GetDecryptedValue((uint)i, 0x8DBC - 0xA0);

            // Load encrypted achievement value (can be improved)
            NUD_Badge00.Value = GetDecryptedValue((uint)i, 0x567C - 0xA0);
            NUD_Badge01.Value = GetDecryptedValue((uint)i, 0x5684 - 0xA0);
            NUD_Badge02.Value = GetDecryptedValue((uint)i, 0x568C - 0xA0);
            NUD_Badge03.Value = GetDecryptedValue((uint)i, 0x5694 - 0xA0);
            NUD_Badge04.Value = GetDecryptedValue((uint)i, 0x569C - 0xA0);
            NUD_Badge05.Value = GetDecryptedValue((uint)i, 0x56A4 - 0xA0);
            NUD_Badge06.Value = GetDecryptedValue((uint)i, 0x56AC - 0xA0);
            NUD_Badge07.Value = GetDecryptedValue((uint)i, 0x56B4 - 0xA0);
            NUD_Badge08.Value = GetDecryptedValue((uint)i, 0x56BC - 0xA0);
            NUD_Badge09.Value = GetDecryptedValue((uint)i, 0x56C4 - 0xA0);
            NUD_Badge10.Value = GetDecryptedValue((uint)i, 0x56CC - 0xA0);
            NUD_Badge11.Value = GetDecryptedValue((uint)i, 0x6C44 - 0xA0);
            NUD_Badge12.Value = GetDecryptedValue((uint)i, 0x56DC - 0xA0);
            NUD_Badge13.Value = GetDecryptedValue((uint)i, 0x56E4 - 0xA0);
            NUD_Badge14.Value = GetDecryptedValue((uint)i, 0x56EC - 0xA0);
            NUD_Badge15.Value = GetDecryptedValue((uint)i, 0x56F4 - 0xA0);
            NUD_Badge16.Value = GetDecryptedValue((uint)i, 0x56FC - 0xA0);
            NUD_Badge17.Value = GetDecryptedValue((uint)i, 0x5704 - 0xA0);
            NUD_Badge18.Value = GetDecryptedValue((uint)i, 0x570C - 0xA0);
            NUD_Badge19.Value = GetDecryptedValue((uint)i, 0x5714 - 0xA0);
            NUD_Badge20.Value = GetDecryptedValue((uint)i, 0x571C - 0xA0);
            NUD_Badge21.Value = GetDecryptedValue((uint)i, 0x5724 - 0xA0);
            NUD_Badge22.Value = GetDecryptedValue((uint)i, 0x572C - 0xA0);
            NUD_Badge23.Value = GetDecryptedValue((uint)i, 0x5734 - 0xA0);
        }
        private void savePlayer(int i)
        {
            Players[i].Name = TB_Name.Text;
            Players[i].Comment = TB_Comment.Text;

            for (int j = 0; j < PlayerBadges.Length; j++)
                Players[i].Badges[j] = (byte)PlayerBadges[j].SelectedIndex;

            Players[i].Hair = (byte)CB_HairStyle.SelectedIndex;
            Players[i].HairColor = (byte)CB_HairColor.SelectedIndex;
            Players[i].Face = (byte)CB_FaceShape.SelectedIndex;
            Players[i].EyeColor = (byte)CB_EyeColor.SelectedIndex;
            Players[i].Tan = (byte)CB_SkinColor.SelectedIndex;
            Players[i].Gender = (byte)CB_Gender.SelectedIndex;

            Players[i].BirthdayDay = (byte)NUD_BirthdayDay.Value;
            Players[i].BirthdayMonth = (byte)CB_BirthdayMonth.SelectedIndex;

            Players[i].RegDay = (byte)NUD_RegDay.Value;
            Players[i].RegMonth = (byte)CB_RegMonth.SelectedIndex;
            Players[i].RegYear = (uint)NUD_RegYear.Value;

            PlayersExterior[currentPlayer].HouseStyle = (byte)CB_HouseStyle.SelectedIndex;
            PlayersExterior[currentPlayer].HouseRoof = (byte)CB_HouseRoof.SelectedIndex;
            PlayersExterior[currentPlayer].HouseBrick = (byte)CB_HouseBrick.SelectedIndex;
            PlayersExterior[currentPlayer].HouseFence = (byte)CB_HouseFence.SelectedIndex;
            PlayersExterior[currentPlayer].HouseDoorForm = (byte)CB_HouseDoorForm.SelectedIndex;
            PlayersExterior[currentPlayer].HouseDoor = (byte)CB_HouseDoor.SelectedIndex;
            PlayersExterior[currentPlayer].HouseMailBox = (byte)CB_HouseMailBox.SelectedIndex;
            PlayersExterior[currentPlayer].HousePavement = (byte)CB_HousePavement.SelectedIndex;

            PlayersExterior[currentPlayer].HouseSize = (byte)CB_HouseSize.SelectedIndex;
            PlayersExterior[currentPlayer].MainRoomSize = (byte)CB_MainRoomSize.SelectedIndex + 1;
            PlayersExterior[currentPlayer].UpstairsSize = (byte)CB_UpstairsSize.SelectedIndex + 1;
            PlayersExterior[currentPlayer].BasementSize = (byte)CB_BasementSize.SelectedIndex + 1;
            PlayersExterior[currentPlayer].RightRoomSize = (byte)CB_RightRoomSize.SelectedIndex + 1;
            PlayersExterior[currentPlayer].LeftRoomSize = (byte)CB_LeftRoomSize.SelectedIndex + 1;
            PlayersExterior[currentPlayer].BackRoomSize = (byte)CB_BackRoomSize.SelectedIndex + 1;


            // save encrypted $ value
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Money.Value)), 0, Players[i].Data, 0x6B8C, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_PocketMoney.Value)), 0, Players[i].Data, 0x6FA8 - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_IslandMedals.Value)), 0, Players[i].Data, 0x6B9C, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_MEOW.Value)), 0, Players[i].Data, 0x8DBC - 0xA0, 8);

            // save encrypted achievement value
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge00.Value)), 0, Players[i].Data, 0x567C - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge01.Value)), 0, Players[i].Data, 0x5684 - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge02.Value)), 0, Players[i].Data, 0x568C - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge03.Value)), 0, Players[i].Data, 0x5694 - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge04.Value)), 0, Players[i].Data, 0x569C - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge05.Value)), 0, Players[i].Data, 0x56A4 - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge06.Value)), 0, Players[i].Data, 0x56AC - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge07.Value)), 0, Players[i].Data, 0x56B4 - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge08.Value)), 0, Players[i].Data, 0x56BC - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge09.Value)), 0, Players[i].Data, 0x56C4 - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge10.Value)), 0, Players[i].Data, 0x56CC - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge11.Value)), 0, Players[i].Data, 0x6C44 - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge12.Value)), 0, Players[i].Data, 0x56DC - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge13.Value)), 0, Players[i].Data, 0x56E4 - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge14.Value)), 0, Players[i].Data, 0x56EC - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge15.Value)), 0, Players[i].Data, 0x56F4 - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge16.Value)), 0, Players[i].Data, 0x56FC - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge17.Value)), 0, Players[i].Data, 0x5704 - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge18.Value)), 0, Players[i].Data, 0x570C - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge19.Value)), 0, Players[i].Data, 0x5714 - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge20.Value)), 0, Players[i].Data, 0x571C - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge21.Value)), 0, Players[i].Data, 0x5724 - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge22.Value)), 0, Players[i].Data, 0x572C - 0xA0, 8);
            Array.Copy(BitConverter.GetBytes(Util.EncryptACNLMoney((uint)NUD_Badge23.Value)), 0, Players[i].Data, 0x5734 - 0xA0, 8);
        }

        private void legalBadgeCheck(object sender, EventArgs e)
        {
            if (CB_Badge00.SelectedIndex == 1) // fish
                NUD_Badge00.BackColor = NUD_Badge00.Value < 500 ? Color.Firebrick : Color.White;
            else if (CB_Badge00.SelectedIndex == 2)
                NUD_Badge00.BackColor = NUD_Badge00.Value < 2000 ? Color.Firebrick : Color.White;
            else if (CB_Badge00.SelectedIndex == 3)
                NUD_Badge00.BackColor = NUD_Badge00.Value < 5000 ? Color.Firebrick : Color.White;
            else if (CB_Badge00.SelectedIndex == 0)
                NUD_Badge00.BackColor = NUD_Badge00.Value < 0 ? Color.Firebrick : Color.White;

            if (CB_Badge01.SelectedIndex == 1) // insect
                NUD_Badge01.BackColor = NUD_Badge01.Value < 500 ? Color.Firebrick : Color.White;
            else if (CB_Badge01.SelectedIndex == 2)
                NUD_Badge01.BackColor = NUD_Badge01.Value < 2000 ? Color.Firebrick : Color.White;
            else if (CB_Badge01.SelectedIndex == 3)
                NUD_Badge01.BackColor = NUD_Badge01.Value < 5000 ? Color.Firebrick : Color.White;
            else if (CB_Badge01.SelectedIndex == 0)
                NUD_Badge01.BackColor = NUD_Badge01.Value < 0 ? Color.Firebrick : Color.White;

            if (CB_Badge02.SelectedIndex == 1) // sea food
                NUD_Badge02.BackColor = NUD_Badge02.Value < 100 ? Color.Firebrick : Color.White;
            else if (CB_Badge03.SelectedIndex == 2)
                NUD_Badge02.BackColor = NUD_Badge02.Value < 200 ? Color.Firebrick : Color.White;
            else if (CB_Badge03.SelectedIndex == 3)
                NUD_Badge02.BackColor = NUD_Badge02.Value < 1000 ? Color.Firebrick : Color.White;
            else if (CB_Badge02.SelectedIndex == 0)
                NUD_Badge02.BackColor = NUD_Badge02.Value < 0 ? Color.Firebrick : Color.White;

            if (CB_Badge06.SelectedIndex == 1) // ballons
                NUD_Badge06.BackColor = NUD_Badge06.Value < 50 ? Color.Firebrick : Color.White;
            else if (CB_Badge06.SelectedIndex == 2)
                NUD_Badge06.BackColor = NUD_Badge06.Value < 100 ? Color.Firebrick : Color.White;
            else if (CB_Badge06.SelectedIndex == 3)
                NUD_Badge06.BackColor = NUD_Badge06.Value < 200 ? Color.Firebrick : Color.White;
            else if (CB_Badge06.SelectedIndex == 0)
                NUD_Badge06.BackColor = NUD_Badge06.Value < 0 ? Color.Firebrick : Color.White;

            if (CB_Badge07.SelectedIndex == 1) // visit
                NUD_Badge07.BackColor = NUD_Badge07.Value < 50 ? Color.Firebrick : Color.White;
            else if (CB_Badge07.SelectedIndex == 2)
                NUD_Badge07.BackColor = NUD_Badge07.Value < 200 ? Color.Firebrick : Color.White;
            else if (CB_Badge07.SelectedIndex == 3)
                NUD_Badge07.BackColor = NUD_Badge07.Value < 500 ? Color.Firebrick : Color.White;
            else if (CB_Badge07.SelectedIndex == 0)
                NUD_Badge07.BackColor = NUD_Badge07.Value < 0 ? Color.Firebrick : Color.White;

            if (CB_Badge08.SelectedIndex == 1) // helper
                NUD_Badge08.BackColor = NUD_Badge08.Value < 50 ? Color.Firebrick : Color.White;
            else if (CB_Badge08.SelectedIndex == 2)
                NUD_Badge08.BackColor = NUD_Badge08.Value < 100 ? Color.Firebrick : Color.White;
            else if (CB_Badge08.SelectedIndex == 3)
                NUD_Badge08.BackColor = NUD_Badge08.Value < 300 ? Color.Firebrick : Color.White;
            else if (CB_Badge08.SelectedIndex == 0)
                NUD_Badge08.BackColor = NUD_Badge08.Value < 0 ? Color.Firebrick : Color.White;

            if (CB_Badge09.SelectedIndex == 1) // tree
                NUD_Badge09.BackColor = NUD_Badge09.Value < 100 ? Color.Firebrick : Color.White;
            else if (CB_Badge09.SelectedIndex == 2)
                NUD_Badge09.BackColor = NUD_Badge09.Value < 250 ? Color.Firebrick : Color.White;
            else if (CB_Badge09.SelectedIndex == 3)
                NUD_Badge09.BackColor = NUD_Badge09.Value < 500 ? Color.Firebrick : Color.White;
            else if (CB_Badge09.SelectedIndex == 0)
                NUD_Badge09.BackColor = NUD_Badge09.Value < 0 ? Color.Firebrick : Color.White;

            if (CB_Badge11.SelectedIndex == 1) // turnip
                NUD_Badge11.BackColor = NUD_Badge11.Value < 500000 ? Color.Firebrick : Color.White;
            else if (CB_Badge11.SelectedIndex == 2)
                NUD_Badge11.BackColor = NUD_Badge11.Value < 3000000 ? Color.Firebrick : Color.White;
            else if (CB_Badge11.SelectedIndex == 3)
                NUD_Badge11.BackColor = NUD_Badge11.Value < 10000000 ? Color.Firebrick : Color.White;
            else if (CB_Badge11.SelectedIndex == 0)
                NUD_Badge11.BackColor = NUD_Badge11.Value < 0 ? Color.Firebrick : Color.White;

            if (CB_Badge12.SelectedIndex == 1) // island medals
                NUD_Badge12.BackColor = NUD_Badge12.Value < 300 ? Color.Firebrick : Color.White;
            else if (CB_Badge12.SelectedIndex == 2)
                NUD_Badge12.BackColor = NUD_Badge12.Value < 1500 ? Color.Firebrick : Color.White;
            else if (CB_Badge12.SelectedIndex == 3)
                NUD_Badge12.BackColor = NUD_Badge12.Value < 5000 ? Color.Firebrick : Color.White;
            else if (CB_Badge12.SelectedIndex == 0)
                NUD_Badge12.BackColor = NUD_Badge12.Value < 0 ? Color.Firebrick : Color.White;

            if (CB_Badge13.SelectedIndex == 1) // street pass
                NUD_Badge13.BackColor = NUD_Badge13.Value < 100 ? Color.Firebrick : Color.White;
            else if (CB_Badge13.SelectedIndex == 2)
                NUD_Badge13.BackColor = NUD_Badge13.Value < 300 ? Color.Firebrick : Color.White;
            else if (CB_Badge13.SelectedIndex == 3)
                NUD_Badge13.BackColor = NUD_Badge13.Value < 1000 ? Color.Firebrick : Color.White;
            else if (CB_Badge13.SelectedIndex == 0)
                NUD_Badge13.BackColor = NUD_Badge13.Value < 0 ? Color.Firebrick : Color.White;

            if (CB_Badge14.SelectedIndex == 1) // grass
                NUD_Badge14.BackColor = NUD_Badge14.Value < 500 ? Color.Firebrick : Color.White;
            else if (CB_Badge14.SelectedIndex == 2)
                NUD_Badge14.BackColor = NUD_Badge14.Value < 2000 ? Color.Firebrick : Color.White;
            else if (CB_Badge14.SelectedIndex == 3)
                NUD_Badge14.BackColor = NUD_Badge14.Value < 5000 ? Color.Firebrick : Color.White;
            else if (CB_Badge14.SelectedIndex == 0)
                NUD_Badge14.BackColor = NUD_Badge14.Value < 0 ? Color.Firebrick : Color.White;

            if (CB_Badge15.SelectedIndex == 1) // shopping
                NUD_Badge15.BackColor = NUD_Badge15.Value < 500000 ? Color.Firebrick : Color.White;
            else if (CB_Badge15.SelectedIndex == 2)
                NUD_Badge15.BackColor = NUD_Badge15.Value < 2000000 ? Color.Firebrick : Color.White;
            else if (CB_Badge15.SelectedIndex == 3)
                NUD_Badge15.BackColor = NUD_Badge15.Value < 5000000 ? Color.Firebrick : Color.White;
            else if (CB_Badge15.SelectedIndex == 0)
                NUD_Badge15.BackColor = NUD_Badge15.Value < 0 ? Color.Firebrick : Color.White;

            if (CB_Badge16.SelectedIndex == 1) // letter
                NUD_Badge16.BackColor = NUD_Badge16.Value < 50 ? Color.Firebrick : Color.White;
            else if (CB_Badge16.SelectedIndex == 2)
                NUD_Badge16.BackColor = NUD_Badge16.Value < 100 ? Color.Firebrick : Color.White;
            else if (CB_Badge16.SelectedIndex == 3)
                NUD_Badge16.BackColor = NUD_Badge16.Value < 200 ? Color.Firebrick : Color.White;
            else if (CB_Badge16.SelectedIndex == 0)
                NUD_Badge16.BackColor = NUD_Badge16.Value < 0 ? Color.Firebrick : Color.White;

            if (CB_Badge17.SelectedIndex == 1) // serge
                NUD_Badge17.BackColor = NUD_Badge17.Value < 50 ? Color.Firebrick : Color.White;
            else if (CB_Badge17.SelectedIndex == 2)
                NUD_Badge17.BackColor = NUD_Badge17.Value < 100 ? Color.Firebrick : Color.White;
            else if (CB_Badge17.SelectedIndex == 3)
                NUD_Badge17.BackColor = NUD_Badge17.Value < 250 ? Color.Firebrick : Color.White;
            else if (CB_Badge17.SelectedIndex == 0)
                NUD_Badge17.BackColor = NUD_Badge17.Value < 0 ? Color.Firebrick : Color.White;

            if (CB_Badge19.SelectedIndex == 1) // KK
                NUD_Badge19.BackColor = NUD_Badge19.Value < 20 ? Color.Firebrick : Color.White;
            else if (CB_Badge19.SelectedIndex == 2)
                NUD_Badge19.BackColor = NUD_Badge19.Value < 50 ? Color.Firebrick : Color.White;
            else if (CB_Badge19.SelectedIndex == 3)
                NUD_Badge19.BackColor = NUD_Badge19.Value < 100 ? Color.Firebrick : Color.White;
            else if (CB_Badge19.SelectedIndex == 0)
                NUD_Badge19.BackColor = NUD_Badge19.Value < 0 ? Color.Firebrick : Color.White;

            if (CB_Badge22.SelectedIndex == 1) // visited
                NUD_Badge22.BackColor = NUD_Badge22.Value < 50 ? Color.Firebrick : Color.White;
            else if (CB_Badge22.SelectedIndex == 2)
                NUD_Badge22.BackColor = NUD_Badge22.Value < 200 ? Color.Firebrick : Color.White;
            else if (CB_Badge22.SelectedIndex == 3)
                NUD_Badge22.BackColor = NUD_Badge22.Value < 500 ? Color.Firebrick : Color.White;
            else if (CB_Badge22.SelectedIndex == 0)
                NUD_Badge22.BackColor = NUD_Badge22.Value < 0 ? Color.Firebrick : Color.White;

            if (CB_Badge23.SelectedIndex == 1) // dream
                NUD_Badge23.BackColor = NUD_Badge23.Value < 50 ? Color.Firebrick : Color.White;
            else if (CB_Badge23.SelectedIndex == 2)
                NUD_Badge23.BackColor = NUD_Badge23.Value < 200 ? Color.Firebrick : Color.White;
            else if (CB_Badge23.SelectedIndex == 3)
                NUD_Badge23.BackColor = NUD_Badge23.Value < 500 ? Color.Firebrick : Color.White;
            else if (CB_Badge23.SelectedIndex == 0)
                NUD_Badge23.BackColor = NUD_Badge23.Value < 0 ? Color.Firebrick : Color.White;
            if (!loaded) return;
            loadBadge();
        }

        private void checkPlayer()
        {
            if (BitConverter.ToUInt32(Save.Data, 0x10C) == 0x00000000)
            {
                PB_JPEG0.Visible = false;
                PB_JPEG0.Image = Properties.Resources.no_tpc;
            }

            if (BitConverter.ToUInt32(Save.Data, 0xA58C) == 0x00000000)
            {
                PB_JPEG1.Visible = false;
                PB_JPEG1.Image = Properties.Resources.no_tpc;
            }

            if (BitConverter.ToUInt32(Save.Data, 0x14A0C) == 0x00000000)
            {
                PB_JPEG2.Visible = false;
                PB_JPEG2.Image = Properties.Resources.no_tpc;
            }

            if (BitConverter.ToUInt32(Save.Data, 0x1EE8C) == 0x00000000)
            {
                PB_JPEG3.Visible = false;
                PB_JPEG3.Image = Properties.Resources.no_tpc;
            }

            if (BitConverter.ToUInt32(Save.Data, 0x57D8) == 0x00000000)
            {
                PB_JPEG0.Image = Properties.Resources.no_tpc;
            }

            if (BitConverter.ToUInt32(Save.Data, 0xFC58) == 0x00000000)
            {
                PB_JPEG1.Image = Properties.Resources.no_tpc;
            }

            if (BitConverter.ToUInt32(Save.Data, 0x1A0D8) == 0x00000000)
            {
                PB_JPEG2.Image = Properties.Resources.no_tpc;
            }

            if (BitConverter.ToUInt32(Save.Data, 0x24558) == 0x00000000)
            {
                PB_JPEG3.Image = Properties.Resources.no_tpc;
            }
        }

        private void loadVillager(int i)
        {
            Villagers[i] = new Villager(Save.Data, 0x0292D0 + 0x2518 * i, 0x2518);
            TownVillagers[i].Enabled = TownVillagersCatch[i].Enabled = TownVillagersBoxed[i].Enabled = (Villagers[i].ID != -1);
            TownVillagers[i].SelectedValue = (int)Villagers[i].ID;
            TownVillagersCatch[i].Text = Villagers[i].CatchPhrase;
            TownVillagersBoxed[i].Checked = Villagers[i].Boxed;
            VillagersPict[i].Image = (Image)Properties.Resources.ResourceManager.GetObject("villager_" + Villagers[i].ID);
        }

        private void loadBadge() // Can be improved
        {
            PB_Badge00.Image = BadgePCH[CB_Badge00.SelectedIndex];
            PB_Badge01.Image = BadgeFLT[CB_Badge01.SelectedIndex];
            PB_Badge02.Image = BadgePLG[CB_Badge02.SelectedIndex];
            PB_Badge03.Image = BadgePSO[CB_Badge03.SelectedIndex];
            PB_Badge04.Image = BadgeINS[CB_Badge04.SelectedIndex];
            PB_Badge05.Image = BadgePLP[CB_Badge05.SelectedIndex];
            PB_Badge06.Image = BadgeBLO[CB_Badge06.SelectedIndex];
            PB_Badge07.Image = BadgeTRN[CB_Badge07.SelectedIndex];
            PB_Badge08.Image = BadgeVST[CB_Badge08.SelectedIndex];
            PB_Badge09.Image = BadgeARB[CB_Badge09.SelectedIndex];
            PB_Badge10.Image = BadgeCLO[CB_Badge10.SelectedIndex];
            PB_Badge11.Image = BadgeNVT[CB_Badge11.SelectedIndex];
            PB_Badge12.Image = BadgeISL[CB_Badge12.SelectedIndex];
            PB_Badge13.Image = BadgeSTP[CB_Badge13.SelectedIndex];
            PB_Badge14.Image = BadgeMVH[CB_Badge14.SelectedIndex];
            PB_Badge15.Image = BadgeCDY[CB_Badge15.SelectedIndex];
            PB_Badge16.Image = BadgeLTR[CB_Badge16.SelectedIndex];
            PB_Badge17.Image = BadgeRNV[CB_Badge17.SelectedIndex];
            PB_Badge18.Image = BadgeCTL[CB_Badge18.SelectedIndex];
            PB_Badge19.Image = BadgeKKG[CB_Badge19.SelectedIndex];
            PB_Badge20.Image = BadgeMSN[CB_Badge20.SelectedIndex];
            PB_Badge21.Image = BadgeCRN[CB_Badge21.SelectedIndex];
            PB_Badge22.Image = BadgeHRT[CB_Badge22.SelectedIndex];
            PB_Badge23.Image = BadgeFLR[CB_Badge23.SelectedIndex];
        }

        private void saveVillager(int i)
        {
            Villagers[i].ID = (TownVillagers[i].SelectedItem == null)
                    ? (short)-1
                    : (short)Util.getIndex(TownVillagers[i]);
            Villagers[i].CatchPhrase = TownVillagersCatch[i].Text;
            Villagers[i].Boxed = TownVillagersBoxed[i].Checked;
            Array.Copy(Villagers[i].Write(), 0, Save.Data, 0x0292D0 + 0x2518 * i, 0x2518);
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
                if (i % 7 == 0 || i / 7 == 0 || i % 7 == 6 || i / 36 > 0) continue;
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
                items[i] = new Item(itemData.Skip(4 * i).Take(4).ToArray());
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

        private Item currentItem = new Item(new byte[] { 0xFE, 0x7F, 0, 0 });
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
            const int width = 64 * mapScale, height = 64 * mapScale;
            byte[] bmpData = new byte[4 * ((width) * (height))];
            for (int i = 0; i < 0x100; i++) // loop over acre data
            {
                int X = i % 16;
                int Y = i / 16;

                var item = items[quadrant * 0x100 + i];
                if (item.ID == 0x7FFE)
                    continue; // skip this one.

                string itemType = getItemType(item.ID);
                uint itemColor = getItemColor(itemType);

                // Plop into image
                for (int x = 0; x < itemsize * itemsize; x++)
                {
                    Buffer.BlockCopy(BitConverter.GetBytes(itemColor), 0, bmpData,
                        ((Y * itemsize + x / itemsize) * width * 4) + ((X * itemsize + x % itemsize) * 4), 4);
                }
                // Buried
                if (item.Buried)
                {
                    for (int z = 2; z < itemsize - 1; z++)
                    {
                        Buffer.BlockCopy(BitConverter.GetBytes(0xFF000000), 0, bmpData,
                            ((Y * itemsize + z) * width * 4) + ((X * itemsize + z) * 4), 4);
                        Buffer.BlockCopy(BitConverter.GetBytes(0xFF000000), 0, bmpData,
                            ((Y * itemsize + z) * width * 4) + ((X * itemsize + itemsize - z) * 4), 4);
                    }
                }
            }
            for (int i = 0; i < width * height; i++) // slap on a grid
                if (i % (itemsize) == 0 || (i / (16 * itemsize)) % (itemsize) == 0)
                    Buffer.BlockCopy(BitConverter.GetBytes(0x41FFFFFF), 0, bmpData,
                        ((i / (16 * itemsize)) * width * 4) + ((i % (16 * itemsize)) * 4), 4);

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
            if (ID == 0x009d) return "pattern";
            if (ID >= 0x9f && ID <= 0xca) return "flower";
            if (ID >= 0x20ac && ID <= 0x2117) return "money";
            if (ID >= 0x98 && ID <= 0x9c) return "rock";
            if (ID >= 0x212B && ID <= 0x223e) return "song";
            if (ID >= 0x223f && ID <= 0x2282) return "paper";
            if (ID >= 0x2283 && ID <= 0x228D) return "turnip";
            if (ID >= 0x228e && ID <= 0x234b) return "catchable";
            if ((ID >= 0x234c && ID <= 0x2469) || ID == 0x211e || ID == 0x211f) return "wallfloor";
            if (ID >= 0x2446 && ID <= 0x28b1) return "clothes";
            if (ID >= 0x295c && ID <= 0x29de) return "gyroids";
            if (ID >= 0x30cc && ID <= 0x30cf) return "mannequin";
            if (ID >= 0x30d0 && ID <= 0x3108) return "art";
            if (ID >= 0x3130 && ID <= 0x3172) return "fossil";
            if (ID >= 0x334c && ID <= 0x338b) return "tool";
            if (ID != 0x7ffe) return "furniture";

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
                        ((Y * itemsize + x % itemsize) * width * 4) + ((X * itemsize + x / itemsize) * 4), 4);
                }
                // Buried
                if (item.Buried)
                {
                    for (int z = 2; z < itemsize - 1; z++)
                    {
                        Buffer.BlockCopy(BitConverter.GetBytes(0xFF000000), 0, bmpData,
                            ((Y * itemsize + z) * width * 4) + ((X * itemsize + z) * 4), 4);
                        Buffer.BlockCopy(BitConverter.GetBytes(0xFF000000), 0, bmpData,
                            ((Y * itemsize + z) * width * 4) + ((X * itemsize + itemsize - z) * 4), 4);
                    }
                }
            }
            for (int i = 0; i < width * height; i++) // slap on a grid
                if (i % (itemsize) == 0 || (i / (itemsize * itemsPerRow)) % (itemsize) == 0)
                    Buffer.BlockCopy(BitConverter.GetBytes(0x17000000), 0, bmpData,
                        ((i / (itemsize * itemsPerRow)) * width * 4) + ((i % (itemsize * itemsPerRow)) * 4), 4);

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
            tabControl2.SelectedIndex = 0;
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

        private void changeVillager(object sender, EventArgs e)
        {
            if (!loaded) return;
            int index = Array.IndexOf(TownVillagers, sender as ComboBox);
            int value = Util.getIndex(sender as ComboBox);
            if (index == -1 || value == -1) return;
            Villagers[index].Type = Main.villagerList[value].Type;
            VillagersPict[index].Image = (Image)Properties.Resources.ResourceManager.GetObject("villager_" + TownVillagers[index].SelectedIndex);

            if (DialogResult.Yes != Util.Prompt(MessageBoxButtons.YesNoCancel, String.Format("Do you want to reset villager {0}'s data? (furniture, clothes...)", index + 1)))
                return;

            Array.Copy(Main.villagerList[value].DefaultBytes, 0, Villagers[index].Data, 0x246E, 88);
            TownVillagersCatch[index].Text = Main.villagerList[value].CatchPhrase;
        }

        private void saveBuildingList()
        {
            const int offset = 0x4BE84;

            int itemcount = dataGridView1.Rows.Count;
            for (int i = 0; i < itemcount; i++)
            {
                int ID = (int)dataGridView1.Rows[i].Cells[0].Value;
                if (ID > 0xFC || ID < 0)
                {
                    Buildings[i].ID = 0xFC;
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
                byte[] data = {0x3A};
                Array.Copy(data, 0, Save.Data, offset, data.Length); // change 0x4BE84 to 0x3A so adding buildings will always work.
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

        // Leaftools ported cheats, thanks to NeoKamek for hosting the source code for his tools.
        // https://bitbucket.org/neokamek/leaftools/src
        private void B_PWP_Click(object sender, EventArgs e)
        {
            const int offset = 0x50328;
            byte[] PWPUnlock =
            {
                0xFF, 0xFF, 0xFF, 0xFF, // 0
                0xFF, 0xFF, 0xFF, 0xFF, // 1
                0xFF, 0xFF, 0xFF, 0xFF, // 2
                0xFF, 0xFF, 0xFF, 0xFF, // 3
                0xFF, 0xFF, 0xFF, 0xFF, // 4
                0x2A, 0xD6, 0xE4, 0x58, // 5
            };

            Array.Copy(PWPUnlock, 0, Save.Data, offset, PWPUnlock.Length);

            Util.Alert("All Public Works Projects unlocked!");
        }
        private void B_Grass_Click(object sender, EventArgs e)
        {
            const byte tileValue = 0xFF;
            const int offset = 0x59900;

            for (int i = 0; i < 0x2800; i++)
                Save.Data[offset + i] = tileValue;

            Util.Alert("All Map Tiles have had their grass refreshed!!");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "garden_plus.dat|*.dat" +
            "|All Files|*.*";
            if (ofd.ShowDialog() != DialogResult.OK)
                return; // no file loaded

            string path = ofd.FileName;
            if (loaded == true)
            {
                DialogResult dialogResult = MessageBox.Show("A savegame is already opened, would you like to reload an other savegame and lost your change ?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    loaded = false;
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
            long length = new FileInfo(path).Length;
            if (length == 0x7FA00) 
            {
                MessageBox.Show("Look like you tried to load the old format of the ACNL savegame.\n\n" +
                    "garden_plus.dat is the new format of the savegame after you did the WA update. \n\n" +
                    "If you want to modifiy your old garden.dat, use the web editor instead or the old version of NLSE");
                return;
            }
            else if (length != 0x89B00 && length != 0x89A80)
            {
                MessageBox.Show("Unsupported file.\nPlease choose a valid garden_plus.dat!");
                return;
            }

            Invisible.Text = path;
            FileInfo fi = new FileInfo(path);
            string filename = Path.GetFileName(path);
            saveToolStripMenuItem.Text = ("Save " + filename);

            Main.SaveData = File.ReadAllBytes(path);

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
            aeTownAcres = new[]
            {
                PB_aeT00, PB_aeT10, PB_aeT20, PB_aeT30, PB_aeT40, PB_aeT50, PB_aeT60,
                PB_aeT01, PB_aeT11, PB_aeT21, PB_aeT31, PB_aeT41, PB_aeT51, PB_aeT61,
                PB_aeT02, PB_aeT12, PB_aeT22, PB_aeT32, PB_aeT42, PB_aeT52, PB_aeT62,
                PB_aeT03, PB_aeT13, PB_aeT23, PB_aeT33, PB_aeT43, PB_aeT53, PB_aeT63,
                PB_aeT04, PB_aeT14, PB_aeT24, PB_aeT34, PB_aeT44, PB_aeT54, PB_aeT64,
                PB_aeT05, PB_aeT15, PB_aeT25, PB_aeT35, PB_aeT45, PB_aeT55, PB_aeT65,
            };
            aeIslandAcres = new[]
            {
                PB_aeI00, PB_aeI10, PB_aeI20, PB_aeI30,
                PB_aeI01, PB_aeI11, PB_aeI21, PB_aeI31,
                PB_aeI02, PB_aeI12, PB_aeI22, PB_aeI32,
                PB_aeI03, PB_aeI13, PB_aeI23, PB_aeI33,
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

            VillagersPict = new[]
            {
                PB_Villager1, PB_Villager2, PB_Villager3, PB_Villager4, PB_Villager5,
                PB_Villager6, PB_Villager7, PB_Villager8, PB_Villager9, PB_Villager10
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
            TownVillagersBoxed = new[]
            {
                CHK_V01, CHK_V02, CHK_V03, CHK_V04, CHK_V05, CHK_V06, CHK_V07, CHK_V08, CHK_V09, CHK_V10
            };
            #endregion
            #region Load Event Methods to Controls
            foreach (PictureBox p in TownAcres) { p.MouseMove += mouseTown; p.MouseClick += clickTown; }
            foreach (PictureBox p in IslandAcres) { p.MouseMove += mouseIsland; p.MouseClick += clickIsland; }
            foreach (PictureBox p in aeTownAcres) { p.MouseMove += mouseTownAcre; p.MouseClick += clickTownAcre; }
            foreach (PictureBox p in aeIslandAcres) { p.MouseMove += mouseIslandAcre; p.MouseClick += clickIslandAcre; }
            { PB_Pocket.MouseMove += mouseCustom; PB_Pocket.MouseClick += clickCustom; }
            { PB_Dresser1.MouseMove += mouseCustom; PB_Dresser1.MouseClick += clickCustom; }
            { PB_Dresser2.MouseMove += mouseCustom; PB_Dresser2.MouseClick += clickCustom; }
            { PB_Island.MouseMove += mouseCustom; PB_Island.MouseClick += clickCustom; }
            #endregion
            loadData();
            EnableControl();
            reloadCurrentItem(currentItem);
            loaded = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!loaded)
                return;

            if (File.Exists(Invisible.Text + ".bak"))
            {
                File.Delete(Invisible.Text + ".bak");
            }

            File.Copy(Invisible.Text, Invisible.Text + ".bak");

            Main.SaveData = saveData();
            Verification.fixChecksums(ref Main.SaveData);
            File.WriteAllBytes(Invisible.Text, Main.SaveData);
        }

        private void exhibitiondatEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void frienddatEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Util.Alert(String.Format("NLSE - By Kaphotics{0}" +
                                     "{0}" +
                                     "Rework by Mega-Mew, support for WA and new features{0}" +
                                     "{0}" +
                                     "Credits:{0}" +
                                     "{0}" +
                                     "Big thanks to marc_max (ACNL Save Editor), NeoKamek (LeafTools), SciresM (ACNL crypto functions),{0}and the many other contributors to the scene!",
                Environment.NewLine));
        }

        private void CB_HairStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            PB_Hair.Image = PlayerHair[CB_HairStyle.SelectedIndex];
        }

        private void CB_FaceShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_Gender.SelectedIndex == 0)
            {
                PB_Face.Image = PlayerFaceMale[CB_FaceShape.SelectedIndex];
            }
            else if (CB_Gender.SelectedIndex == 1)
            {
                PB_Face.Image = PlayerFaceFemale[CB_FaceShape.SelectedIndex];
            }
        }

        private void CB_Gender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_Gender.SelectedIndex == 0)
            {
                PB_Face.Image = PlayerFaceMale[CB_FaceShape.SelectedIndex];
            }
            else if (CB_Gender.SelectedIndex == 1)
            {
                PB_Face.Image = PlayerFaceFemale[CB_FaceShape.SelectedIndex];
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!loaded)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "garden_plus.dat|*.dat";
            if (sfd.ShowDialog() != DialogResult.OK)
                return; // not saved

            string path = sfd.FileName;

            Main.SaveData = saveData();
            Verification.fixChecksums(ref Main.SaveData);
            File.WriteAllBytes(path, Main.SaveData);
        }

        private void BTN_TPCInject_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPEG Picture|*.jpg;*.jpeg";
            if (ofd.ShowDialog() != DialogResult.OK)
                return; // no file loaded

            string path = ofd.FileName;

            long length = new FileInfo(path).Length;
            if (length > 0x1400) // Check file size
            {
                MessageBox.Show("Your TCP Picture is too big !\nMax: 4kb (0x1400)");
                return;
            }
            byte[] CheckHeader = File.ReadAllBytes(path).Skip(0).Take(2).ToArray();

            if (BitConverter.ToUInt16(CheckHeader, 0x0) != 0xD8FF) // Check if JPEG
            {
                MessageBox.Show("The picture must be a JPEG !");
                return;
            }
            Image img = Image.FromFile(path);

            if (img.Height > 104 && img.Width > 64) // Check Dimension
            {
                MessageBox.Show("The dimensions of the picture must be 64x104.");
                return;
            }

            if (img.Height != 104 && img.Width != 64)
            {
                MessageBox.Show("The dimensions of the picture are ok, but the maximum here is 64x104.\nThe dimensions of your picture are: " + img.Width + "x" + img.Height + "");
            }
            byte[] CorrectFile = File.ReadAllBytes(path); // All check passed, write image.
            Array.Copy(CorrectFile, 0, Players[currentPlayer].Data, 0x57D8 - 0xA0 + (currentPlayer * 0xA480), length);
            PlayerPics[currentPlayer].Image = img;
            PB_LPlayer0.Image = img;
            Util.Alert("Succesfully injected the new picture.");
        }

        private void BTN_DMPInvo_Click(object sender, EventArgs e)
        {
            if (CB_Choice.SelectedIndex == 0)
            {
                for (int i = 0; i < Players.Length; i++)
                    Array.Copy(Players[i].Write(), 0, Save.Data, 0xA0 + i * 0xA480, 0xA480);

                if (!Directory.Exists("Players"))
                    Directory.CreateDirectory("Players");

                if (!Directory.Exists("Players/Inventory Data"))
                    Directory.CreateDirectory("Players/Inventory Data");

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = TB_Name.Text + " Inventory Data";
                sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + Path.Combine("Players", "Inventory Data");
                sfd.Filter = "Inventory Data (Player " + (currentPlayer + 1) + ")|*.bin";
                if (sfd.ShowDialog() != DialogResult.OK)
                    return; // not saved

                byte[] Pocket = (Save.Data.Skip(0x6C70 + currentPlayer * 0xA480).Take(0x40).ToArray());
                byte[] Dresser = (Save.Data.Skip(0x9390 + currentPlayer * 0xA480).Take(0x2D0).ToArray());
                byte[] IslandInv = (Save.Data.Skip(0x6FB0 + currentPlayer * 0xA480).Take(0xA0).ToArray());
                byte[] Storage = (Save.Data.Skip(0x7A778 + (0x5A0 * (currentPlayer))).Take(0x5A0).ToArray());
                byte[] Inv = Pocket.Concat(Dresser).Concat(IslandInv).Concat(Storage).ToArray();

                File.WriteAllBytes(sfd.FileName, Inv);
            }
            else if (CB_Choice.SelectedIndex == 1)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Inventory|*.bin";
                if (ofd.ShowDialog() != DialogResult.OK)
                    return; // no file loaded

                string path = ofd.FileName;

                long length = new FileInfo(path).Length;
                if (length != 0x950) // Check file size
                {
                    MessageBox.Show("Invalid file.");
                    return;
                }
                for (int i = 0; i < Players.Length; i++)
                    Array.Copy(Players[i].Write(), 0, Save.Data, 0xA0 + i * 0xA480, 0xA480);

                byte[] PocketR = File.ReadAllBytes(path).Skip(0x0).Take(0x40).ToArray();
                byte[] DresserR = File.ReadAllBytes(path).Skip(0x40).Take(0x2D0).ToArray();
                byte[] IslandInvR = File.ReadAllBytes(path).Skip(0x310).Take(0xA0).ToArray();
                byte[] StorageR = File.ReadAllBytes(path).Skip(0x390).Take(0x5A0).ToArray();

                Array.Copy(PocketR, 0, Save.Data, (0x6C70 + currentPlayer * 0xA480), 0x40);
                Array.Copy(DresserR, 0, Save.Data, (0x9390 + currentPlayer * 0xA480), 0x2D0);
                Array.Copy(IslandInvR, 0, Save.Data, (0x6FB0 + currentPlayer * 0xA480), 0xA0);
                Array.Copy(StorageR, 0, Save.Data, (0x7A778 + (0x5A0 * (currentPlayer))), 0x5A0);

                for (int i = 0; i < Players.Length; i++) // load
                    Players[i] = new Player(Save.Data.Skip(0xA0 + i * 0xA480).Take(0xA480).ToArray());

                loadPlayer(currentPlayer);
            }
        }



        private void BTN_HouseData_Click(object sender, EventArgs e)
        {
            if (CB_Choice.SelectedIndex == 0)
            {
                savePlayer(currentPlayer);

                for (int i = 0; i < Players.Length; i++)
                    Array.Copy(PlayersExterior[i].Write(), 0, Save.Data, 0x5D904 + i * 0x1228, 0x1228);

                if (!Directory.Exists("Players"))
                    Directory.CreateDirectory("Players");

                if (!Directory.Exists("Players/House Data"))
                    Directory.CreateDirectory("Players/House Data");

                SaveFileDialog sfd = new SaveFileDialog();

                sfd.FileName = TB_Name.Text + " House Data";
                sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + Path.Combine("Players", "House Data");

                sfd.Filter = "Save ACNL House Data (Player " + (currentPlayer + 1) + ")|*.bin";
                if (sfd.ShowDialog() != DialogResult.OK)
                    return; // not saved

                byte[] House = (Save.Data.Skip(0x5D904 + currentPlayer * 0x1228).Take(0x1228).ToArray());


                File.WriteAllBytes(sfd.FileName, House);
            }
            else if (CB_Choice.SelectedIndex == 1)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Open a ACNL House Data file|*.bin";
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

                Array.Copy(House, 0, Save.Data, 0x5D904 + currentPlayer * 0x1228, length);

                for (int i = 0; i < PlayersExterior.Length; i++)
                    PlayersExterior[i] = new PlayerExterior(Save.Data.Skip(0x5D904 + i * 0x1228).Take(0x1228).ToArray());

                loadPlayer(currentPlayer);
            }
        }

        private void BTN_DMPBadge_Click(object sender, EventArgs e)
        {
            if (CB_Choice.SelectedIndex == 0)
            {
                for (int i = 0; i < Players.Length; i++)
                    Array.Copy(Players[i].Write(), 0, Save.Data, 0xA0 + i * 0xA480, 0xA480);

                if (!Directory.Exists("Players"))
                    Directory.CreateDirectory("Players");

                if (!Directory.Exists("Players/Badge Data"))
                    Directory.CreateDirectory("Players/Badge Data");

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Badges Data (Player " + (currentPlayer + 1) + ")|*.bin";
                sfd.FileName = TB_Name.Text + " Badge Data";
                sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + Path.Combine("Players", "Badge Data");

                if (sfd.ShowDialog() != DialogResult.OK)
                    return; // not saved

                byte[] BadgesData = (Save.Data.Skip(0x567C + currentPlayer * 0xA480).Take(0xD8).ToArray());
                byte[] Turnip = (Save.Data.Skip(0x6C44 + currentPlayer * 0xA480).Take(0x8).ToArray());
                byte[] Final = BadgesData.Concat(Turnip).ToArray();

                File.WriteAllBytes(sfd.FileName, Final);
            }
            else if (CB_Choice.SelectedIndex == 1)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Badge Data|*.bin";
                if (ofd.ShowDialog() != DialogResult.OK)
                    return; // no file loaded

                string path = ofd.FileName;

                long length = new FileInfo(path).Length;
                if (length != 0xE0) // Check file size
                {
                    MessageBox.Show("Invalid file.");
                    return;
                }
                for (int i = 0; i < Players.Length; i++)
                    Array.Copy(Players[i].Write(), 0, Save.Data, 0xA0 + i * 0xA480, 0xA480);

                byte[] BadgeData = File.ReadAllBytes(path).Skip(0x0).Take(0xD8).ToArray();
                byte[] Turnip = File.ReadAllBytes(path).Skip(0xD8).Take(0x8).ToArray();

                Array.Copy(BadgeData, 0, Save.Data, (0x567C + currentPlayer * 0xA480), 0xD8);
                Array.Copy(Turnip, 0, Save.Data, (0x6C44 + currentPlayer * 0xA480), 0x8);

                for (int i = 0; i < Players.Length; i++) // load
                    Players[i] = new Player(Save.Data.Skip(0xA0 + i * 0xA480).Take(0xA480).ToArray());
                loadPlayer(currentPlayer);
            }
        }

        private void BTN_DMPEncyclo_Click(object sender, EventArgs e)
        {
            if (CB_Choice.SelectedIndex == 0)
            {
                for (int i = 0; i < Players.Length; i++)
                    Array.Copy(Players[i].Write(), 0, Save.Data, 0xA0 + i * 0xA480, 0xA480);

                if (!Directory.Exists("Players"))
                    Directory.CreateDirectory("Players");

                if (!Directory.Exists("Players/Encyclopedia Data"))
                    Directory.CreateDirectory("Players/Encyclopedia Data");


                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Encyclopedia Data (Player " + (currentPlayer + 1) + ")|*.bin";
                sfd.FileName = TB_Name.Text + " Encyclopedia Data";
                sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + Path.Combine("Players", "Encyclopedia Data");

                if (sfd.ShowDialog() != DialogResult.OK)
                    return; // not saved

                byte[] Encyclo = (Save.Data.Skip(0x6D10 + currentPlayer * 0xA480).Take(0x1C).ToArray()); // What you caught
                byte[] BeastSize = (Save.Data.Skip(0xA328 + currentPlayer * 0xA480).Take(0x15C).ToArray()); // Size of everything you caught
                byte[] Final = Encyclo.Concat(BeastSize).ToArray();

                File.WriteAllBytes(sfd.FileName, Final);
            }
            else if (CB_Choice.SelectedIndex == 1)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Encyclopedia|*.bin";
                if (ofd.ShowDialog() != DialogResult.OK)
                    return; // no file loaded

                string path = ofd.FileName;

                long length = new FileInfo(path).Length;
                if (length != 0x178) // Check file size
                {
                    MessageBox.Show("Invalid file.");
                    return;
                }
                for (int i = 0; i < Players.Length; i++)
                    Array.Copy(Players[i].Write(), 0, Save.Data, 0xA0 + i * 0xA480, 0xA480);

                byte[] Encyclo = File.ReadAllBytes(path).Skip(0x0).Take(0x1C).ToArray();
                byte[] BeastSize = File.ReadAllBytes(path).Skip(0x1C).Take(0x15C).ToArray();

                Array.Copy(Encyclo, 0, Save.Data, (0x6D10 + currentPlayer * 0xA480), 0x1C);
                Array.Copy(BeastSize, 0, Save.Data, (0xA328 + currentPlayer * 0xA480), 0x15C);

                for (int i = 0; i < Players.Length; i++) // load
                    Players[i] = new Player(Save.Data.Skip(0xA0 + i * 0xA480).Take(0xA480).ToArray());
                loadPlayer(currentPlayer);
            }
        }

        private void BTN_DMPCatalog_Click(object sender, EventArgs e)
        {
            if (CB_Choice.SelectedIndex == 0)
            {
                if (!Directory.Exists("Players"))
                    Directory.CreateDirectory("Players");

                if (!Directory.Exists("Players/Catalog Data"))
                    Directory.CreateDirectory("Players/Catalog Data");

                for (int i = 0; i < Players.Length; i++)
                    Array.Copy(Players[i].Write(), 0, Save.Data, 0xA0 + i * 0xA480, 0xA480);

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Catalog Data (Player " + (currentPlayer + 1) + ")|*.bin";
                sfd.FileName = TB_Name.Text + " Catalog Data";
                sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + Path.Combine("Players", "Catalog Data");
                if (sfd.ShowDialog() != DialogResult.OK)
                    return; // not saved

                byte[] Catalog = (Save.Data.Skip(0x6D30 + currentPlayer * 0xA480).Take(0x1A8).ToArray()); 

                File.WriteAllBytes(sfd.FileName, Catalog);
            }
            else if (CB_Choice.SelectedIndex == 1)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Catalog|*.bin";
                if (ofd.ShowDialog() != DialogResult.OK)
                    return; // no file loaded

                string path = ofd.FileName;

                long length = new FileInfo(path).Length;
                if (length != 0x1A8) // Check file size
                {
                    MessageBox.Show("Invalid file.");
                    return;
                }
                for (int i = 0; i < Players.Length; i++)
                    Array.Copy(Players[i].Write(), 0, Save.Data, 0xA0 + i * 0xA480, 0xA480);

                byte[] Catalog = File.ReadAllBytes(path).Skip(0x0).Take(0x23C).ToArray();

                Array.Copy(Catalog, 0, Save.Data, (0x6D30 + currentPlayer * 0xA480), 0x23C);

                for (int i = 0; i < Players.Length; i++) // load
                    Players[i] = new Player(Save.Data.Skip(0xA0 + i * 0xA480).Take(0xA480).ToArray());
                loadPlayer(currentPlayer);
            }
        }

        private void BTN_DMPKK_Click(object sender, EventArgs e)
        {
            if (CB_Choice.SelectedIndex == 0)
            {
                if (!Directory.Exists("Players"))
                    Directory.CreateDirectory("Players");

                if (!Directory.Exists("Players/K.K. Data"))
                    Directory.CreateDirectory("Players/K.K. Data");

                for (int i = 0; i < Players.Length; i++)
                    Array.Copy(Players[i].Write(), 0, Save.Data, 0xA0 + i * 0xA480, 0xA480);

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "KK Music (Player " + (currentPlayer + 1) + ")|*.bin";
                sfd.FileName = TB_Name.Text + " K.K. Data";
                sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + Path.Combine("Players", "K.K. Data");

                if (sfd.ShowDialog() != DialogResult.OK)
                    return; // not saved

                byte[] Catalog = (Save.Data.Skip(0x903C + currentPlayer * 0xA480).Take(0xC).ToArray());

                File.WriteAllBytes(sfd.FileName, Catalog);
            }
            else if (CB_Choice.SelectedIndex == 1)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "KK Music|*.bin";
                if (ofd.ShowDialog() != DialogResult.OK)
                    return; // no file loaded

                string path = ofd.FileName;

                long length = new FileInfo(path).Length;
                if (length != 0xC) // Check file size
                {
                    MessageBox.Show("Invalid file.");
                    return;
                }
                for (int i = 0; i < Players.Length; i++)
                    Array.Copy(Players[i].Write(), 0, Save.Data, 0xA0 + i * 0xA480, 0xA480);

                byte[] KK = File.ReadAllBytes(path).Skip(0x0).Take(0xC).ToArray();

                Array.Copy(KK, 0, Save.Data, (0x6D30 + currentPlayer * 0xA480), 0xC);

                for (int i = 0; i < Players.Length; i++) // load
                    Players[i] = new Player(Save.Data.Skip(0xA0 + i * 0xA480).Take(0xA480).ToArray());
                loadPlayer(currentPlayer);
            }
        }

        private void BTN_DMPMus_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("Museum Contribution"))
                Directory.CreateDirectory("Museum Contribution");

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Museum Contribution|*.bin";
            sfd.FileName = TB_TownName.Text + " Museum Contribution";
            sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + @"Museum Contribution";
            if (sfd.ShowDialog() != DialogResult.OK)
                  return; // not saved

              byte[] Museum = (Save.Data.Skip(0x6AEB8).Take(0x55A).ToArray());

              File.WriteAllBytes(sfd.FileName, Museum);
        }

        private void BTN_INJMus_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Open a Museum Data file|*.bin";
            if (ofd.ShowDialog() != DialogResult.OK)
                return; // no file loaded

            string path = ofd.FileName;

            long length = new FileInfo(path).Length;
            if (length != 0x55A) // Check file size
            {
                MessageBox.Show("Invalid file.");
                return;
            }
            byte[] Museum = File.ReadAllBytes(path);
            Array.Copy(Museum, 0, Save.Data, 0x6AEB8, length);
        }

        private void BTN_CompleteMus_Click(object sender, EventArgs e)
        {
            byte[] Museum = Properties.Resources.Museum;
            Array.Copy(Museum, 0, Save.Data, 0x6AEB8, 0x55A);
        }

        private void BTN_DMPlayer_Click(object sender, EventArgs e)
        {
            if (CB_Choice.SelectedIndex == 0)
            {
                if (!Directory.Exists("Players"))
                    Directory.CreateDirectory("Players");

                if (!Directory.Exists("Players/Full Data"))
                    Directory.CreateDirectory("Players/Full Data");

                for (int i = 0; i < Players.Length; i++)
                    Array.Copy(Players[i].Write(), 0, Save.Data, 0xA0 + i * 0xA480, 0xA480);

                savePlayer(currentPlayer);

                for (int i = 0; i < Players.Length; i++)
                    Array.Copy(PlayersExterior[i].Write(), 0, Save.Data, 0x5D904 + i * 0x1228, 0x1228);

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Player Data (Player " + (currentPlayer + 1) + ")|*.bin";
                sfd.FileName = TB_Name.Text + " Full Data";
                sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + Path.Combine("Players", "Full Data");
                if (sfd.ShowDialog() != DialogResult.OK)
                    return; // not saved

                byte[] Player = (Save.Data.Skip(0xA0 + currentPlayer * 0xA480).Take(0xA480).ToArray());
                byte[] House = (Save.Data.Skip(0x5D904 + currentPlayer * 0x1228).Take(0x1228).ToArray());
                byte[] Storage = (Save.Data.Skip(0x7A778 + (0x5A0 * (currentPlayer))).Take(0x5A0).ToArray());
                byte[] Final = Player.Concat(House).Concat(Storage).ToArray();

                File.WriteAllBytes(sfd.FileName, Final);
            }
            else if (CB_Choice.SelectedIndex == 1)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Player Data|*.bin";
                if (ofd.ShowDialog() != DialogResult.OK)
                    return; // no file loaded

                string path = ofd.FileName;

                long length = new FileInfo(path).Length;
                if (length != 0xBC48) // Check file size
                {
                    MessageBox.Show("Invalid file.");
                    return;
                }
                for (int i = 0; i < Players.Length; i++) // save
                    Array.Copy(Players[i].Write(), 0, Save.Data, 0xA0 + i * 0xA480, 0xA480);

                byte[] Player = File.ReadAllBytes(path).Skip(0x0).Take(0xA480).ToArray();
                byte[] House = File.ReadAllBytes(path).Skip(0xA480).Take(0x1228).ToArray();
                byte[] Storage = File.ReadAllBytes(path).Skip(0xB6A8).Take(0x5A0).ToArray();

                Array.Copy(Player, 0, Save.Data, (0xA0 + currentPlayer * 0xA480), 0xA480);
                Array.Copy(House, 0, Save.Data, 0x5D904 + currentPlayer * 0x1228, 0x1228);
                Array.Copy(Storage, 0, Save.Data, (0x7A778 + (0x5A0 * (currentPlayer))), 0x5A0);

                for (int i = 0; i < Players.Length; i++) // load
                    Players[i] = new Player(Save.Data.Skip(0xA0 + i * 0xA480).Take(0xA480).ToArray());
                for (int i = 0; i < Players.Length; i++)
                    PlayerPics[i].Image = Players[i].JPEG;

                loadPlayer(currentPlayer);

                for (int i = 0; i < PlayersExterior.Length; i++)
                    PlayersExterior[i] = new PlayerExterior(Save.Data.Skip(0x5D904 + i * 0x1228).Take(0x1228).ToArray());

                CB_HouseStyle.SelectedIndex = PlayersExterior[currentPlayer].HouseStyle;
                CB_HouseBrick.SelectedIndex = PlayersExterior[currentPlayer].HouseBrick;
                CB_HousePavement.SelectedIndex = PlayersExterior[currentPlayer].HousePavement;
                CB_HouseRoof.SelectedIndex = PlayersExterior[currentPlayer].HouseRoof;
                CB_HouseFence.SelectedIndex = PlayersExterior[currentPlayer].HouseFence;
                CB_HouseDoor.SelectedIndex = PlayersExterior[currentPlayer].HouseDoor;
                CB_HouseDoorForm.SelectedIndex = PlayersExterior[currentPlayer].HouseDoorForm;
                CB_HouseMailBox.SelectedIndex = PlayersExterior[currentPlayer].HouseMailBox;
            }
        }

        private void BTN_GetEmoticon_Click(object sender, EventArgs e)
        {
            byte[] emotions =
            {
                0x01, 0x02, 0x03, 0x04,
                0x05, 0x06, 0x07, 0x08,
                0x09, 0x0A, 0x0B, 0x0C,
                0x0D, 0x0E, 0x10, 0x11,
                0x12, 0x13, 0x14, 0x15,
                0x16, 0x17, 0x18, 0x19,
                0x1A, 0x1B, 0x1C, 0x1D,
                0x1E, 0x20, 0x21, 0x24,
                0x26, 0x27, 0x28, 0x29,
                0x2A, 0x2B, 0x2C, 0x2E,
            };

            for (int i = 0; i < Players.Length; i++) // save
                Array.Copy(Players[i].Write(), 0, Save.Data, 0xA0 + i * 0xA480, 0xA480);

            Array.Copy(emotions, 0, Save.Data, 0x8A70 + currentPlayer * 0xA480, emotions.Length);

            for (int i = 0; i < Players.Length; i++) // load
                Players[i] = new Player(Save.Data.Skip(0xA0 + i * 0xA480).Take(0xA480).ToArray());
            Util.Alert("Added all emoticons for " + TB_Name.Text + ".");
        }

        private void BTN_GetKKSong_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Players.Length; i++) // save
                Array.Copy(Players[i].Write(), 0, Save.Data, 0xA0 + i * 0xA480, 0xA480);

            for (int i = 0; i < 0xC; i++)
                Save.Data[0x903C + currentPlayer * 0xA480 + i] = 0xFF;

            for (int i = 0; i < Players.Length; i++) // load
                Players[i] = new Player(Save.Data.Skip(0xA0 + i * 0xA480).Take(0xA480).ToArray());
            Util.Alert("All K.K. song has been added to " + TB_Name.Text + " music player list.");
        }

        private void BTN_FillCatalog_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Players.Length; i++) // save
                Array.Copy(Players[i].Write(), 0, Save.Data, 0xA0 + i * 0xA480, 0xA480);

            for (int i = 0; i < 0x1A8; i++)
                Save.Data[0x6D30 + currentPlayer * 0xA480 + i] = 0xFF;

            for (int i = 0; i < Players.Length; i++) // load
                Players[i] = new Player(Save.Data.Skip(0xA0 + i * 0xA480).Take(0xA480).ToArray());
            Util.Alert(TB_Name.Text + " catalog has been filled.");
        }

        private void BTN_FillEncyclo_Click(object sender, EventArgs e)
        {
            byte[] encyclo =
            {
                0xCC, 0xFC, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xD0, 0x9A,
            };

            byte[] BeastSize = Properties.Resources.encyclo;

            for (int i = 0; i < Players.Length; i++) // save
                Array.Copy(Players[i].Write(), 0, Save.Data, 0xA0 + i * 0xA480, 0xA480);

            Array.Copy(encyclo, 0, Save.Data, 0x6D10 + currentPlayer * 0xA480, encyclo.Length);
            Array.Copy(BeastSize, 0, Save.Data, 0xA328 + currentPlayer * 0xA480, BeastSize.Length);

            for (int i = 0; i < Players.Length; i++) // load
                Players[i] = new Player(Save.Data.Skip(0xA0 + i * 0xA480).Take(0xA480).ToArray());
            Util.Alert(TB_Name.Text + " encyclopedia has been filled.");
        }

        private void BTN_Badges_Click(object sender, EventArgs e)
        {
                foreach (Control value in PlayerBadges)
                {
                    if (value is ComboBox)
                    {
                        ComboBox cb = (ComboBox)value;
                        cb.SelectedIndex = CB_WantedBadge.SelectedIndex;
                    }
                }
                if (CB_FixBadge.Checked == true)
                {
                    BTN_FixBadges.PerformClick();
                }
        }

        private void BTN_FixBadges_Click(object sender, EventArgs e)
        {
            if (CB_Badge00.SelectedIndex == 1) // fish
            {
                if (NUD_Badge00.Value < 500)
                {
                    NUD_Badge00.Value = 500;
                }
            }
            else if (CB_Badge00.SelectedIndex == 2)
            {
                if (NUD_Badge00.Value < 2000)
                    {
                        NUD_Badge00.Value = 2000;
                    }
            }
            else if (CB_Badge00.SelectedIndex == 3)
            {
                if (NUD_Badge00.Value < 5000)
                    {
                        NUD_Badge00.Value = 5000;
                    }
            }

            if (CB_Badge01.SelectedIndex == 1) // insect
            {
                if (NUD_Badge01.Value < 500)
                {
                    NUD_Badge01.Value = 500;
                }
            }
            else if (CB_Badge01.SelectedIndex == 2)
            {
                if (NUD_Badge01.Value < 2000)
                {
                    NUD_Badge01.Value = 2000;
                }
            }
            else if (CB_Badge01.SelectedIndex == 3)
            {
                if (NUD_Badge01.Value < 5000)
                {
                    NUD_Badge01.Value = 5000;
                }
            }

            if (CB_Badge02.SelectedIndex == 1) // sea food
            {
                if (NUD_Badge02.Value < 100)
                {
                    NUD_Badge02.Value = 100;
                }
            }
            else if (CB_Badge02.SelectedIndex == 2)
            {
                if (NUD_Badge02.Value < 200)
                {
                    NUD_Badge02.Value = 200;
                }
            }
            else if (CB_Badge02.SelectedIndex == 3)
            {
                if (NUD_Badge02.Value < 1000)
                {
                    NUD_Badge02.Value = 1000;
                }
            }

            if (CB_Badge06.SelectedIndex == 1) // balloon
            {
                if (NUD_Badge06.Value < 50)
                {
                    NUD_Badge06.Value = 50;
                }
            }
            else if (CB_Badge06.SelectedIndex == 2)
            {
                if (NUD_Badge06.Value < 100)
                {
                    NUD_Badge06.Value = 100;
                }
            }
            else if (CB_Badge06.SelectedIndex == 3)
            {
                if (NUD_Badge06.Value < 200)
                {
                    NUD_Badge06.Value = 200;
                }
            }

            if (CB_Badge07.SelectedIndex == 1) // visit
            {
                if (NUD_Badge07.Value < 50)
                {
                    NUD_Badge07.Value = 50;
                }
            }
            else if (CB_Badge07.SelectedIndex == 2)
            {
                if (NUD_Badge07.Value < 200)
                {
                    NUD_Badge07.Value = 200;
                }
            }
            else if (CB_Badge07.SelectedIndex == 3)
            {
                if (NUD_Badge07.Value < 500)
                {
                    NUD_Badge07.Value = 500;
                }
            }

            if (CB_Badge08.SelectedIndex == 1) // helper
            {
                if (NUD_Badge08.Value < 50)
                {
                    NUD_Badge08.Value = 50;
                }
            }
            else if (CB_Badge08.SelectedIndex == 2)
            {
                if (NUD_Badge08.Value < 100)
                {
                    NUD_Badge08.Value = 100;
                }
            }
            else if (CB_Badge08.SelectedIndex == 3)
            {
                if (NUD_Badge08.Value < 300)
                {
                    NUD_Badge08.Value = 300;
                }
            }

            if (CB_Badge09.SelectedIndex == 1) // tree
            {
                if (NUD_Badge09.Value < 100)
                {
                    NUD_Badge09.Value = 100;
                }
            }
            else if (CB_Badge09.SelectedIndex == 2)
            {
                if (NUD_Badge09.Value < 250)
                {
                    NUD_Badge09.Value = 250;
                }
            }
            else if (CB_Badge09.SelectedIndex == 3)
            {
                if (NUD_Badge09.Value < 500)
                {
                    NUD_Badge09.Value = 500;
                }
            }

            if (CB_Badge11.SelectedIndex == 1) // turnip
            {
                if (NUD_Badge11.Value < 500000)
                {
                    NUD_Badge11.Value = 500000;
                }
            }
            else if (CB_Badge11.SelectedIndex == 2)
            {
                if (NUD_Badge11.Value < 3000000)
                {
                    NUD_Badge11.Value = 3000000;
                }
            }
            else if (CB_Badge11.SelectedIndex == 3)
            {
                if (NUD_Badge11.Value < 10000000)
                {
                    NUD_Badge11.Value = 10000000;
                }
            }

            if (CB_Badge12.SelectedIndex == 1) // island medals
            {
                if (NUD_Badge12.Value < 300)
                {
                    NUD_Badge12.Value = 300;
                }
            }
            else if (CB_Badge12.SelectedIndex == 2)
            {
                if (NUD_Badge12.Value < 1500)
                {
                    NUD_Badge12.Value = 1500;
                }
            }
            else if (CB_Badge12.SelectedIndex == 3)
            {
                if (NUD_Badge12.Value < 5000)
                {
                    NUD_Badge12.Value = 5000;
                }
            }

            if (CB_Badge13.SelectedIndex == 1) // street pass
            {
                if (NUD_Badge13.Value < 100)
                {
                    NUD_Badge13.Value = 100;
                }
            }
            else if (CB_Badge13.SelectedIndex == 2)
            {
                if (NUD_Badge13.Value < 300)
                {
                    NUD_Badge13.Value = 300;
                }
            }
            else if (CB_Badge13.SelectedIndex == 3)
            {
                if (NUD_Badge13.Value < 1000)
                {
                    NUD_Badge13.Value = 1000;
                }
            }

            if (CB_Badge14.SelectedIndex == 1) // weed
            {
                if (NUD_Badge13.Value < 500)
                {
                    NUD_Badge13.Value = 500;
                }
            }
            else if (CB_Badge14.SelectedIndex == 2)
            {
                if (NUD_Badge13.Value < 2000)
                {
                    NUD_Badge13.Value = 2000;
                }
            }
            else if (CB_Badge14.SelectedIndex == 3)
            {
                if (NUD_Badge13.Value < 5000)
                {
                    NUD_Badge13.Value = 5000;
                }
            }

            if (CB_Badge15.SelectedIndex == 1) // shopping
            {
                if (NUD_Badge15.Value < 500000)
                {
                    NUD_Badge15.Value = 500000;
                }
            }
            else if (CB_Badge15.SelectedIndex == 2)
            {
                if (NUD_Badge15.Value < 2000000)
                {
                    NUD_Badge15.Value = 2000000;
                }
            }
            else if (CB_Badge15.SelectedIndex == 3)
            {
                if (NUD_Badge15.Value < 5000000)
                {
                    NUD_Badge15.Value = 5000000;
                }
            }

            if (CB_Badge15.SelectedIndex == 1) // shopping
            {
                if (NUD_Badge15.Value < 500000)
                {
                    NUD_Badge15.Value = 500000;
                }
            }
            else if (CB_Badge15.SelectedIndex == 2)
            {
                if (NUD_Badge15.Value < 2000000)
                {
                    NUD_Badge15.Value = 2000000;
                }
            }
            else if (CB_Badge15.SelectedIndex == 3)
            {
                if (NUD_Badge15.Value < 5000000)
                {
                    NUD_Badge15.Value = 5000000;
                }
            }

            if (CB_Badge16.SelectedIndex == 1) // letter
            {
                if (NUD_Badge16.Value < 50)
                {
                    NUD_Badge16.Value = 50;
                }
            }
            else if (CB_Badge16.SelectedIndex == 2)
            {
                if (NUD_Badge16.Value < 100)
                {
                    NUD_Badge16.Value = 100;
                }
            }
            else if (CB_Badge16.SelectedIndex == 3)
            {
                if (NUD_Badge16.Value < 200)
                {
                    NUD_Badge16.Value = 200;
                }
            }

            if (CB_Badge17.SelectedIndex == 1) // serge
            {
                if (NUD_Badge17.Value < 50)
                {
                    NUD_Badge17.Value = 50;
                }
            }
            else if (CB_Badge17.SelectedIndex == 2)
            {
                if (NUD_Badge17.Value < 100)
                {
                    NUD_Badge17.Value = 100;
                }
            }
            else if (CB_Badge17.SelectedIndex == 3)
            {
                if (NUD_Badge17.Value < 250)
                {
                    NUD_Badge17.Value = 250;
                }
            }

            if (CB_Badge19.SelectedIndex == 1) // KK
            {
                if (NUD_Badge19.Value < 20)
                {
                    NUD_Badge19.Value = 20;
                }
            }
            else if (CB_Badge19.SelectedIndex == 2)
            {
                if (NUD_Badge19.Value < 50)
                {
                    NUD_Badge19.Value = 50;
                }
            }
            else if (CB_Badge19.SelectedIndex == 3)
            {
                if (NUD_Badge19.Value < 100)
                {
                    NUD_Badge19.Value = 100;
                }
            }

            if (CB_Badge22.SelectedIndex == 1) // visited
            {
                if (NUD_Badge22.Value < 50)
                {
                    NUD_Badge22.Value = 50;
                }
            }
            else if (CB_Badge22.SelectedIndex == 2)
            {
                if (NUD_Badge22.Value < 200)
                {
                    NUD_Badge22.Value = 200;
                }
            }
            else if (CB_Badge22.SelectedIndex == 3)
            {
                if (NUD_Badge22.Value < 500)
                {
                    NUD_Badge22.Value = 500;
                }
            }

            if (CB_Badge23.SelectedIndex == 1) // dream
            {
                if (NUD_Badge23.Value < 50)
                {
                    NUD_Badge23.Value = 50;
                }
            }
            else if (CB_Badge23.SelectedIndex == 2)
            {
                if (NUD_Badge23.Value < 200)
                {
                    NUD_Badge23.Value = 200;
                }
            }
            else if (CB_Badge23.SelectedIndex == 3)
            {
                if (NUD_Badge23.Value < 500)
                {
                    NUD_Badge23.Value = 500;
                }
            }
        }

        private void BTN_FillMap_Click(object sender, EventArgs e)
        {

            if (DialogResult.Yes != Util.Prompt(MessageBoxButtons.YesNo, "Fill the map with " + CB_Item.Text + "?"))
                return;
            int ctr = filler(ref TownItems);


            fillTownItems(TownItems, TownAcres);

            for (int i = 0; i < TownItems.Length; i++)
                Array.Copy(TownItems[i].Write(), 0, Save.Data, 0x534D8 + i * 4, 4);

            TownItems = getMapItems(Save.Data.Skip(0x534D8).Take(0x5000).ToArray());
            fillTownItems(TownItems, TownAcres); // Reload to remove the "X" from buried flag

            Util.Alert(String.Format("{0} items changed !", ctr));

        }

        private int filler(ref Item[] items)
        {
            int a = Int32.Parse(CB_Item.SelectedValue.ToString());
            ushort item = Convert.ToUInt16(a);

            string flag1 = TB_Flag1.Text;
            byte f1out = Convert.ToByte(flag1);

            string flag2 = TB_Flag2.Text;
            byte f2out = Convert.ToByte(flag1);

            int ctr = 0;
            foreach (Item i in TownItems)
            {
                ctr++;
                i.ID = item;
                i.Flag1 = f1out;
                i.Flag2 = f2out;
            }
            return ctr;
        }

        private void CB_Flag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_Flag.SelectedIndex == 0)
            {
                TB_Flag1.Enabled = true;
                TB_Flag2.Enabled = true;
                TB_Flag1.Text = "00";
                TB_Flag2.Text = "00";
            }
            else if (CB_Flag.SelectedIndex == 1)
            {
                TB_Flag1.Enabled = false;
                TB_Flag2.Enabled = false;
                TB_Flag1.Text = "00";
                TB_Flag2.Text = "80";
            }
            else if (CB_Flag.SelectedIndex == 2)
            {
                TB_Flag1.Enabled = false;
                TB_Flag2.Enabled = false;
                TB_Flag1.Text = "00";
                TB_Flag2.Text = "40";
            }
        }

        private void BTN_ExportMapItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("Map Items"))
                Directory.CreateDirectory("Map Items");

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = TB_TownName.Text + " Map Items";
            sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + @"Map Items";
            sfd.Filter = "Map Item Data|*.bin";
            if (sfd.ShowDialog() != DialogResult.OK)
                return; // not saved

            for (int i = 0; i < TownItems.Length; i++)
                Array.Copy(TownItems[i].Write(), 0, Save.Data, 0x534D8 + i * 4, 4);

            byte[] MapItem = (Save.Data.Skip(0x534D8).Take(0x5000).ToArray());

            File.WriteAllBytes(sfd.FileName, MapItem);
        }

        private void BTN_ImportMapItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Open a map item data file|*.bin";
            if (ofd.ShowDialog() != DialogResult.OK)
                return; // no file loaded

            string path = ofd.FileName;

            long length = new FileInfo(path).Length;
            if (length != 0x5000) // Check file size
            {
                MessageBox.Show("Invalid file.");
                return;
            }
            byte[] MapItem = File.ReadAllBytes(path);
            Array.Copy(MapItem, 0, Save.Data, (0x534D8), length);

            TownItems = getMapItems(Save.Data.Skip(0x534D8).Take(0x5000).ToArray());
            fillTownItems(TownItems, TownAcres); 
        }

        private void B_ExportIslandItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("Island Items"))
                Directory.CreateDirectory("Island Items");

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = TB_TownName.Text + " Island Items";
            sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + @"Island Items";
            sfd.Filter = "Island Item Data|*.bin";
            if (sfd.ShowDialog() != DialogResult.OK)
                return; // not saved

            for (int i = 0; i < IslandItems.Length; i++) 
                Array.Copy(IslandItems[i].Write(), 0, Save.Data, 0x6FED8 + i * 4, 4);

            byte[] IslandItem = (Save.Data.Skip(0x6FED8).Take(0x1000).ToArray());

            File.WriteAllBytes(sfd.FileName, IslandItem);
        }

        private void B_ImportIslandItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Open a island item data file|*.bin";
            if (ofd.ShowDialog() != DialogResult.OK)
                return; // no file loaded

            string path = ofd.FileName;

            long length = new FileInfo(path).Length;
            if (length != 0x1000) // Check file size
            {
                MessageBox.Show("Invalid file.");
                return;
            }
            byte[] IslandItem = File.ReadAllBytes(path);
            Array.Copy(IslandItem, 0, Save.Data, (0x6FED8), length);

            IslandItems = getMapItems(Save.Data.Skip(0x6FED8).Take(0x1000).ToArray());
            fillIslandItems(IslandItems, IslandAcres);
        }

        private void BTN_ExportVillager_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("Villagers"))
                Directory.CreateDirectory("Villagers");

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Villager Data|*.bin";
            sfd.FileName = TB_TownName.Text + " Villagers";
            sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + @"Villagers";

            if (sfd.ShowDialog() != DialogResult.OK)
                return; // not saved

            for (int i = 0; i < Villagers.Length; i++)
                saveVillager(i);

            byte[] Villager = (Save.Data.Skip(0x292D0).Take(0x172F0).ToArray());

            File.WriteAllBytes(sfd.FileName, Villager);
        }

        private void BTN_ImportVillager_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Open a villager data file|*.bin";
            if (ofd.ShowDialog() != DialogResult.OK)
                return; // no file loaded

            string path = ofd.FileName;

            long length = new FileInfo(path).Length;
            if (length != 0x172F0) // Check file size
            {
                MessageBox.Show("Invalid file.");
                return;
            }
            byte[] Villager = File.ReadAllBytes(path);
            Array.Copy(Villager, 0, Save.Data, (0x292D0), length);

            Villagers = new Villager[10];
            for (int i = 0; i < Villagers.Length; i++)
                loadVillager(i);
        }

        private void BTN_ExportBuildings_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("Map Buildings"))
                Directory.CreateDirectory("Map Buildings");

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Buildings Data|*.bin";
            sfd.FileName = TB_TownName.Text + " Map Buildings";
            sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + @"Map Buildings";
            if (sfd.ShowDialog() != DialogResult.OK)
                return; // not saved

            saveBuildingList();
            for (int i = 0; i < Buildings.Length; i++)
                Array.Copy(Buildings[i].Write(), 0, Save.Data, 0x4BE88 + i * 4, 4);

            byte[] Building = (Save.Data.Skip(0x4BE88).Take(0xE8).ToArray());

            File.WriteAllBytes(sfd.FileName, Building);
        }

        private void BTN_ImportBuildings_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Open a buildings data file|*.bin";
            if (ofd.ShowDialog() != DialogResult.OK)
                return; // no file loaded

            string path = ofd.FileName;

            long length = new FileInfo(path).Length;
            if (length != 0xE8) // Check file size
            {
                MessageBox.Show("Invalid file.");
                return;
            }
            byte[] Building = File.ReadAllBytes(path);
            Array.Copy(Building, 0, Save.Data, (0x4BE88), length);

            Buildings = new Building[58];
            for (int i = 0; i < Buildings.Length; i++)
                Buildings[i] = new Building(Save.Data.Skip(0x4BE88 + i * 4).Take(4).ToArray());
            populateBuildingList();
        }

        private void NUD_TreeSize_ValueChanged(object sender, EventArgs e)
        {
            if (!loaded) return;
            DialogResult dialogResult = MessageBox.Show("Do you want to fix your town play time to match with your tree size ?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (NUD_TreeSize.Value == 0)
                {
                    NUD_Days.Value = 0;
                    NUD_Hours.Value = 1;
                    NUD_Minutes.Value = 0;
                    NUD_Seconds.Value = 0;
                }
                else if (NUD_TreeSize.Value == 1)
                {
                    NUD_Days.Value = 0;
                    NUD_Hours.Value = 5;
                    NUD_Minutes.Value = 0;
                    NUD_Seconds.Value = 0;
                }
                else if (NUD_TreeSize.Value == 2)
                {
                    NUD_Days.Value = 0;
                    NUD_Hours.Value = 20;
                    NUD_Minutes.Value = 0;
                    NUD_Seconds.Value = 0;
                }
                else if (NUD_TreeSize.Value == 3)
                {
                    NUD_Days.Value = 2;
                    NUD_Hours.Value = 2;
                    NUD_Minutes.Value = 0;
                    NUD_Seconds.Value = 0;
                }
                else if (NUD_TreeSize.Value == 4)
                {
                    NUD_Days.Value = 4;
                    NUD_Hours.Value = 4;
                    NUD_Minutes.Value = 0;
                    NUD_Seconds.Value = 0;
                }
                else if (NUD_TreeSize.Value == 5)
                {
                    NUD_Days.Value = 7;
                    NUD_Hours.Value = 12;
                    NUD_Minutes.Value = 0;
                    NUD_Seconds.Value = 0;
                }
                else if (NUD_TreeSize.Value == 6)
                {
                    NUD_Days.Value = 12;
                    NUD_Hours.Value = 12;
                    NUD_Minutes.Value = 0;
                    NUD_Seconds.Value = 0;
                }
                else if (NUD_TreeSize.Value == 7)
                {
                    NUD_Days.Value = 20;
                    NUD_Hours.Value = 20;
                    NUD_Minutes.Value = 0;
                    NUD_Seconds.Value = 0;
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void BTN_DMPattern_Click(object sender, EventArgs e)
        {
            if (CB_Choice.SelectedIndex == 0)
            {
                if (!Directory.Exists("Players"))
                    Directory.CreateDirectory("Players");

                if (!Directory.Exists("Players/Pattern Data"))
                    Directory.CreateDirectory("Players/Pattern Data");

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = TB_Name.Text + " Pattern Data";
                sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + Path.Combine("Players", "Pattern Data");
                sfd.Filter = "Save ACNL Pattern Data (Player "+ (currentPlayer + 1) +")|*.bin";
                if (sfd.ShowDialog() != DialogResult.OK)
                return; // not saved

                byte[] Pattern = (Save.Data.Skip(0xCC + currentPlayer * 0xA480).Take(0x870 * 0xA).ToArray());

                File.WriteAllBytes(sfd.FileName, Pattern);
            }
            else if (CB_Choice.SelectedIndex == 1)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Open a ACNL Pattern Data file|*.bin";
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
                Array.Copy(Pattern, 0, Players[currentPlayer].Data, 0xCC - 0xA0, length);
                Array.Copy(Pattern, 0, Save.Data, (0xCC + currentPlayer * 0xA480), length);
            }
        }

        private void B_Desert_Click(object sender, EventArgs e)
        {
            const byte tileValue = 0x00;
            const int offset = 0x59900;

            for (int i = 0; i < 0x2800; i++)
                Save.Data[offset + i] = tileValue;

            Util.Alert("All Map Tiles have had their grass removed (desertified)!");
        }

        private void mouseTownAcre(object sender, MouseEventArgs e)
        {
            int acre = Array.IndexOf(aeTownAcres, sender as PictureBox);
            if (acre < 0) return;

            L_AcreInfo.Text = String.Format("Acre Info:{0}X: {1}{0}Y: {2}{0}Index: {3}",
                Environment.NewLine, acre % 7, acre / 7, aeTownAcreTiles[acre]);
        }
        private void mouseIslandAcre(object sender, MouseEventArgs e)
        {
            int acre = Array.IndexOf(aeIslandAcres, sender as PictureBox);
            if (acre < 0) return;

            L_AcreInfo.Text = String.Format("Acre Info:{0}X: {1}{0}Y: {2}{0}Index: {3}",
                Environment.NewLine, acre % 4, acre / 4, aeIslandAcreTiles[acre]);
        }
        private void clickTownAcre(object sender, MouseEventArgs e)
        {
            int acre = Array.IndexOf(aeTownAcres, sender as PictureBox);
            if (acre < 0) return;

            int newAcre = aeTownAcreTiles[acre] + (e.Button == MouseButtons.Right ? -1 : 1);
            if (newAcre < 0)
                newAcre = CHK_IslandAcresOnTown.Checked ? 205 : 176;
            else if (newAcre > 205 || (newAcre > 176 && !CHK_IslandAcresOnTown.Checked))
                newAcre = 1;

            aeTownAcreTiles[acre] = (ushort)newAcre;
            aeTownAcres[acre].BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("acre_" + aeTownAcreTiles[acre]);
        }
        private void clickIslandAcre(object sender, MouseEventArgs e)
        {
            int acre = Array.IndexOf(aeIslandAcres, sender as PictureBox);
            if (acre < 0) return;

            if (e.Button == MouseButtons.Right) // Decrement (180-203 allowed)
            {
                aeIslandAcreTiles[acre]--;
                if (aeIslandAcreTiles[acre] < 169) aeIslandAcreTiles[acre] = 214;
            }
            else // Increment (180-203 allowed)
            {
                aeIslandAcreTiles[acre]++;
                if (aeIslandAcreTiles[acre] < 169) aeIslandAcreTiles[acre] = 214;
            }
            aeIslandAcres[acre].BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("acre_" + aeIslandAcreTiles[acre]);
        }

        private void B_ApplyAcres_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes !=
                Util.Prompt(MessageBoxButtons.YesNo,
                    String.Format("Applying acres will copy the Acre Editor's {0} map to the Save File's {0} map.", Tab_AcreTown.Text),
                    "Continue?"))
                return;
            if (TC_AcreEditor.SelectedIndex == 0)
            {
                Array.Copy(aeTownAcreTiles, TownAcreTiles, TownAcreTiles.Length);
                fillMapAcres(TownAcreTiles, TownAcres);
            }
            else
            {
                Array.Copy(aeIslandAcreTiles, IslandAcreTiles, IslandAcreTiles.Length);
                fillMapAcres(IslandAcreTiles, IslandAcres);
            }
        }
        private void B_ResetAcres_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes !=
                Util.Prompt(MessageBoxButtons.YesNo,
                    String.Format("Applying acres will copy the Save File's {0} map to the Acre Editor's {0} map.", Tab_AcreTown.Text),
                    "Continue?"))
                return;
            if (TC_AcreEditor.SelectedIndex == 0)
            {
                Array.Copy(TownAcreTiles, aeTownAcreTiles, TownAcreTiles.Length);
                fillMapAcres(aeTownAcreTiles, aeTownAcres);
            }
            else
            {
                Array.Copy(IslandAcreTiles, aeIslandAcreTiles, IslandAcreTiles.Length);
                fillMapAcres(aeIslandAcreTiles, aeIslandAcres);
            }
        }

        private void B_ImportAcres_Click(object sender, EventArgs e)
        {

            var ofd = new OpenFileDialog
            {
                FileName = ((TC_AcreEditor.SelectedIndex == 0) ? "Town" : "Island") + "_AcreData.acnlmap"
            };
            if (DialogResult.OK != ofd.ShowDialog())
                return;

            byte[] data = File.ReadAllBytes(ofd.FileName);
            if (data.Length != 2 * 6 * 7 && data.Length != 2 * 4 * 4)
            {
                Util.Error("Input file length is not a valid acre map.",
                    String.Format("Data Size: {1}{0}Acre Count: {2}", Environment.NewLine, data.Length, data.Length / 2));
                return;
            }

            ushort[] uA = new ushort[data.Length / 2];
            using (MemoryStream ms = new MemoryStream(data))
            using (BinaryReader br = new BinaryReader(ms))
            {
                for (int i = 0; i < uA.Length; i++)
                    uA[i] = br.ReadUInt16();
            }

            if (uA.Length == 6 * 7) // Town Map
            {
                Array.Copy(uA, aeTownAcreTiles, uA.Length);
                fillMapAcres(aeTownAcreTiles, aeTownAcres);
            }
            else if (uA.Length == 4 * 4) // Island Map
            {
                Array.Copy(uA, aeIslandAcreTiles, uA.Length);
                fillMapAcres(aeIslandAcreTiles, aeIslandAcres);
            }
            else
            return; // unreachable.
        }

        private void B_ExportAcres_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("Map Acres"))
                Directory.CreateDirectory("Map Acres");

            var sfd = new SaveFileDialog
            {
                FileName = ((TC_AcreEditor.SelectedIndex == 0) ? TB_TownName.Text + " Town" : TB_TownName.Text + " Island") + "AcreData.acnlmap"
            };
            sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + @"Map Acres";
            if (DialogResult.OK != sfd.ShowDialog())
                return;

            byte[] data;
            ushort[] usD = (TC_AcreEditor.SelectedIndex == 0) ? aeTownAcreTiles : aeIslandAcreTiles;
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                foreach (ushort u in usD)
                    bw.Write(u);
                data = ms.ToArray();
            }
            File.WriteAllBytes(sfd.FileName, data);
        }
    }
}
