using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLSE
{
    public class Data
    {
        internal static string[] getItemStrings(string l)
        {
            string[] storage = new string[0x8000];
            object txt = Properties.Resources.ResourceManager.GetObject("item_" + l); // Fetch File, \n to list.
            List<string> rawlist = ((string)txt).Split(new[] { '\n' }).ToList();
            string[] input = rawlist.ToArray();

            for (int i = 0; i < input.Length; i++)
            {
            try {
                string line = input[i];
                string[] t = line.Split('\t');
                if (t.Length < 2 || t[0].Length != 4 || t[0][0] == '\\')
                    continue;

                int index = Convert.ToInt32(t[0], 16);
                storage[index] = t[1];
            }
            catch { }
            }
            return storage;
        }

        internal static List<cbItem> getCBItems(string[] storage)
        {
            List<cbItem> cbList = new List<cbItem>();
            try
            {
            for (int i = 0; i < storage.Length; i++)
                if (storage[i] != null && storage[i].Length > 0)
                    cbList.Add(new cbItem
                    {
                        Text = storage[i],
                        Value = i
                    });
                }
            catch { }
            return cbList;
        }
    }
    public class cbItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
    }
    public class DataRef
    {
        public int Offset, Length;
        public DataRef(int offset, int length)
        {
            Offset = offset;
            Length = length;
        }

        public byte[] getData(byte[] Save)
        {
            return Save.Skip(Offset).Take(Length).ToArray();
        }
        public void setData(ref byte[] Save, byte[] Inject)
        {
            if (Length != Inject.Length)
                throw new Exception(String.Format(
                    "Data lengths do not match.{0}" +
                    "Expected: 0x{1}{0}" +
                    "Received: 0x{2}",
                    Environment.NewLine, Length.ToString("X5"), Inject.Length.ToString("X5")));
            Array.Copy(Save, Offset, Inject, 0, Length);
        }

        public string getString(byte[] Save)
        {
            return Encoding.Unicode.GetString(Save.Skip(Offset).Take(Length).ToArray()).Trim('\0');
        }
        public void setString(ref byte[] Save, string s)
        {
            if (Length > s.Length*2)
                throw new Exception(String.Format(
                    "String byte lengths do not match.{0}" +
                    "Expected: 0x{1}{0}" +
                    "Received: 0x{2}",
                    Environment.NewLine, Length.ToString("X5"), s.Length.ToString("X5")));
            byte[] newARR = Encoding.Unicode.GetBytes(s.PadRight(Length/2));
            Array.Copy(newARR, 0, Save, 0, Length);
        }
    }
}
