using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
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
        public String name;
        configHeader header;
        List<configItem> data = new List<configItem>();

        public enum cfgReply
        {
            CFG_LOAD_OK = 1,
            CFG_LOAD_WRONGCHECK,
            CFG_LOAD_PROCEED,
            CFG_NOT_CONFIGURED,
            CFG_ERASED,
            CFG_ASK_CONFIRM_ERASE,
            CFG_CMD_ERR
        };

        public enum cfgCommand
        {
            CFG_CONFIG = 0xA0,
            CFG_DUMP,
            CFG_ERASE,
            CFG_CONFIRM_ERASE
        };

        public static int CFG_PRESENT = 0xA5;
        public Configuration()
        {
            header = new configHeader();
        }
               
        public void updateHeader()
        {
            header.numItems = (byte)data.Count();
            header.size = 0;

            foreach (configItem item in data)
            {
                header.size += item.getSize();
            }
        }

        public bool loadData(String filename)
        {
            header = new configHeader();

            try
            {
                using (Stream stream = File.Open(filename, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    header.size = (byte)bformatter.Deserialize(stream);
                    header.numItems = (byte)bformatter.Deserialize(stream);
                    header.prodId = (byte)bformatter.Deserialize(stream);
                    header.versionId = (byte)bformatter.Deserialize(stream);

                    data = (List<configItem>)bformatter.Deserialize(stream);

                    return true;
                }
            }
            catch(SerializationException e)
            {
                return false;
            }
        }

        // the template is an empty configuration (i.e. all values are null strings) with the                            
        // same product id and version used to get the parameters tags and data types. 
        // Load this config then fill values with the data read from the device.
        // Templates are stored in the application folder (or a user folder)
        public bool loadTemplate(byte prodId, byte verId, string folder, ref String name)
        {
            // search for all templates in app folder
            byte pid, vid;

            try
            {
                string[] filePaths = Directory.GetFiles(folder, "*.cft");

                name = "";

                // open each template
                foreach (string template in filePaths)
                {
                    try
                    {
                        using (Stream stream = File.Open(template, FileMode.Open))
                        {
                            var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                            bformatter.Deserialize(stream);
                            bformatter.Deserialize(stream);
                            pid = (byte)bformatter.Deserialize(stream);
                            vid = (byte)bformatter.Deserialize(stream);
                        }
                    }
                    catch (SerializationException e)
                    {
                        return false;
                    }

                    // check for prodId,verId
                    if (prodId == pid && verId == vid)
                    {
                        // if found, load as a config and return true
                        loadData(template);
                        name = template;
                        return true;
                    }
                }
            }
            catch(Exception e)
            {
                return false;
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

        public bool checkData()
        {
            foreach (configItem item in data)
            {
                if (item.value.Length == 0)
                    return false;
            }

            return true;
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
