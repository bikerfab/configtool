using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace configtool
{
    [Serializable]
    public class configHeader
    {
        public byte size;
        public byte numItems;
        public byte prodId;
        public byte versionId;
    }

    [Serializable]
    public class Configuration
    {
        configHeader header;
        List<configItem> data = new List<configItem>();

        public static int CFG_LOAD_OK = 1;
        public static int CFG_LOAD_WRONGCHECK = 2;
        public static int CFG_LOAD_PROCEED = 8;
        public static int CFG_NOT_CONFIGURED = 9;
        public static int CFG_ERASED = 10;
        public static int CFG_PRESENT = 0xA5;

        public Configuration()
        {
            header = new configHeader();
        }
               
        public void updateHeader()
        {
            header.numItems = (byte)data.Count();

            foreach(configItem item in data)
            {
                header.size += item.getSize();
            }
        }

        public void loadData(String filename)
        {
            header = new configHeader();

            using (Stream stream = File.Open(filename, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                header.size = (byte)bformatter.Deserialize(stream);
                header.numItems = (byte)bformatter.Deserialize(stream);
                header.prodId = (byte)bformatter.Deserialize(stream);
                header.versionId = (byte)bformatter.Deserialize(stream);

                data = (List<configItem>)bformatter.Deserialize(stream);                
            }            
        }

        // the template is an empty configuration (i.e. all values are null strings) with the                            
        // same product id and version used to get the parameters tags and data types. 
        // Load this config then fill values with the data read from the device.
        // Templates are stored in the application folder (or a user folder)
        public bool loadTemplate(byte prodId, byte verId, string folder)
        {
            // search for all templates in app folder
            byte pid, vid;

            string[] filePaths = Directory.GetFiles(folder, "*.ctp");

            // open each template
            foreach(string template in filePaths)
            {
                using (Stream stream = File.Open(template, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    bformatter.Deserialize(stream);
                    bformatter.Deserialize(stream);
                    pid = (byte)bformatter.Deserialize(stream);
                    vid = (byte)bformatter.Deserialize(stream);
                }
                // check for prodId,verId
                if (prodId == pid && verId == vid)
                {
                    // if found, load as a config and return true
                    loadData(template);
                    return true;
                }                
            }
                                   
            // if not found return false
            return false;
        }

        public void fromBuffer(byte[] buffer)  // fill the values of the template
        {
            int i;
            int j = 0;

            header.size = (byte)buffer[1];
            header.numItems = (byte)buffer[2];
            header.prodId = (byte)buffer[3];
            header.versionId = (byte)buffer[4];

            for(i=0; i< header.numItems; i++)
            {
                configItem item = data.ElementAt(i);
                item.setValueFromBuffer(buffer, j+5);
                j += item.numBytes;                
            }
        }

        public void saveData(String filename)
        {
            using (Stream stream = File.Open(filename, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, header.size);
                bformatter.Serialize(stream, header.numItems);
                bformatter.Serialize(stream, header.prodId);
                bformatter.Serialize(stream, header.versionId);
                bformatter.Serialize(stream, data);
            }
        }

        public void addItem(configItem item)
        {
            data.Add(item);
        }

        public void clear()
        {
            header.size = 0;
            header.numItems = 0;
            header.prodId = 0;
            header.versionId = 0;

            data.Clear();
        }

        public void clearDataOnly()
        {
            data.Clear();
        }

        public List<configItem> getData()
        {
            return data;
        }

        public bool isEmpty()
        {
            return data.Count == 0;
        }

        public byte getSize()
        {
            return header.size;
        }

        public byte getNumItems()
        {
            return header.numItems;
        }

        public void setNumItems(int n)
        {
            header.numItems = (byte)n;
        }

        public byte getProductId()
        {
            return header.prodId;
        }

        public byte getVersionId()
        {
            return header.versionId;
        }

        public void setProductId(byte id)
        {
            header.prodId = id;
        }

        public void setVersionId(byte id)
        {
            header.versionId = id;
        }

        public configItem getItem(int i)
        {
            return data.ElementAt(i);
        }

        public byte checksum()
        {
            int chksum = 0;
            int i, j;
            configItem item;

            chksum = header.size + header.numItems +header.prodId + header.versionId;

            for (i = 0; i < header.numItems; i++)
            {
                item = data.ElementAt(i);

                for (j = 0; j < item.numBytes; j++)
                {
                    chksum += data.ElementAt(i).getRawData()[j];
                }
            }

            return BitConverter.GetBytes(chksum)[0];
        }
    }

}
