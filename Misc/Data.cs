using System;
using System.Linq;
using System.Text;

namespace NLSE
{
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
                    "Expected:{1}{0}" +
                    "Received:{2}", 
                    Environment.NewLine, Length, Inject.Length));
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
                    "Expected:{1}{0}" +
                    "Received:{2}",
                    Environment.NewLine, Length, s.Length));
            byte[] newARR = Encoding.Unicode.GetBytes(s.PadRight(Length/2));
            Array.Copy(newARR, 0, Save, 0, Length);
        }
    }
}
