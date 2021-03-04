using System;
using System.Linq;
using System.Text;

namespace configtool
{
    [Serializable]
    public class configItem
    {
        public String tag;
        public String value;
        public String type;
        public String descript;
        public int typeCode;
        public byte numBytes;
        public static String[] dataTypeNames = { "Float", "UInt16", "Int16", "UInt32", "Int32", "Int8", "String8" };
        static byte[] itemNumBytes = { 4, 2, 2, 4, 4, 1, 8 };

        public configItem(String t, String v, String ty, String desc)
        {
            tag = t;
            value = v;
            type = ty;
            descript = desc;
            typeCode = getTypeCode(ty);
            numBytes = configItem.itemNumBytes[typeCode];
        }

        public int getTypeCode(String typeName)
        {
            for(int i = 0; i < dataTypeNames.Count(); i++)
            {
                if (dataTypeNames[i] == typeName)
                    return i;
            }

            return -1;
        }

        public byte getSize()
        {
            return numBytes;
        }

        public byte[] getRawData()
        {
            byte[] raw = new byte[4];

            switch (typeCode)
            {
                case 0:     // float
                    raw = BitConverter.GetBytes(Convert.ToSingle(value));
                    break;

                case 1:     // 16 bit uint
                    raw = BitConverter.GetBytes(Convert.ToUInt16(value));
                    break;

                case 2:     // 16 bit int
                    raw = BitConverter.GetBytes(Convert.ToInt16(value));
                    break;

                case 3:     // 32 bit uint
                    raw = BitConverter.GetBytes(Convert.ToUInt32(value));
                    break;

                case 4:     // 32 bit int
                    raw = BitConverter.GetBytes(Convert.ToInt32(value));
                    break;

                case 5: // Uint8
                    raw = BitConverter.GetBytes(Convert.ToUInt16(value) & 0xFF);
                    break;

                case 6: // String 8
                    byte[] str8 = Encoding.ASCII.GetBytes(value);
                    return str8;

                default:
                    raw = null;
                    break;
            }
            return raw;
        }

        public void setValueFromBuffer(byte[] buffer, int offset)
        {
            switch (typeCode)
            {
                case 0:     // float
                    value = BitConverter.ToSingle(buffer, offset).ToString();
                    break;

                case 1:     // 16 bit uint
                    value = BitConverter.ToUInt16(buffer, offset).ToString();
                    break;

                case 2:     // 16 bit int
                    value = BitConverter.ToInt16(buffer, offset).ToString();
                    break;

                case 3:     // 32 bit uint
                    value = BitConverter.ToUInt32(buffer, offset).ToString();
                    break;

                case 4:     // 32 bit int
                    value = BitConverter.ToInt32(buffer, offset).ToString();
                    break;

                case 5: // 8 bit uint
                    value = BitConverter.ToChar(buffer, offset).ToString();
                    break;

                case 6: // String 8
                    value = System.Text.Encoding.Default.GetString(buffer);
                    break;

                default:
                    value = "undef";
                    break;
            }
        }
    }
}
