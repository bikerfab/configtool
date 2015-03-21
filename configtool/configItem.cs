using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace configtool
{
    [Serializable]
    public class configItem
    {
        public String tag;
        public String value;
        public int type;
        public byte numBytes;

        public configItem(String t, String v, int ty)
        {
            tag = t;
            value = v;
            type = ty;

            switch(type)
            {
                case 1:
                case 4:
                case 5:
                    numBytes = 4;
                    break;

                case 2:
                case 3:
                    numBytes = 2;
                    break;

                default:
                    numBytes = 0;
                    break;
            }
        }

        public byte getSize()
        {
            return numBytes;
        }

        public byte[] getRawData()
        {
            byte[] raw = new byte[4];

            switch (type)
            {
                case 1:     // float
                    raw = BitConverter.GetBytes(Convert.ToSingle(value));
                    break;

                case 2:     // 16 bit uint
                    raw = BitConverter.GetBytes(Convert.ToUInt16(value));
                    break;

                case 3:     // 16 bit int
                    raw = BitConverter.GetBytes(Convert.ToInt16(value));
                    break;

                case 4:     // 32 bit uint
                    raw = BitConverter.GetBytes(Convert.ToUInt32(value));
                    break;

                case 5:     // 32 bit int
                    raw = BitConverter.GetBytes(Convert.ToInt32(value));
                    break;

                default:
                    raw = null;
                    break;
            }
            return raw;
        }

        public void setValueFromBuffer(byte[] buffer, int offset)
        {
            switch (type)
            {
                case 1:     // float
                    value = BitConverter.ToSingle(buffer, offset).ToString();
                    break;

                case 2:     // 16 bit uint
                    value = BitConverter.ToUInt16(buffer, offset).ToString();
                    break;

                case 3:     // 16 bit int
                    value = BitConverter.ToInt16(buffer, offset).ToString();
                    break;

                case 4:     // 32 bit uint
                    value = BitConverter.ToUInt32(buffer, offset).ToString();
                    break;

                case 5:     // 32 bit int
                    value = BitConverter.ToInt32(buffer, offset).ToString();
                    break;

                default:
                    value = "undef";
                    break;
            }
        }
    }
}
