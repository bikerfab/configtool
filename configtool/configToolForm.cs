using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Ports;

namespace configtool
{
    public partial class FormConfig : Form
    {
        Configuration cfg;

        public FormConfig()
        {
            InitializeComponent();            
        }

        private void initTable()
        {
            dataGridViewConfig.Columns.Clear();
            dataGridViewConfig.Columns.Add("param", "Parameter");
            dataGridViewConfig.Columns.Add("val", "Value");
            dataGridViewConfig.Columns.Add("dataType", "Type");
            dataGridViewConfig.AllowUserToAddRows = false;
            dataGridViewConfig.AllowUserToDeleteRows = false;
            dataGridViewConfig.Columns["dataType"].Visible = false;
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            cfg = new Configuration();
            initTable();

            string[] ports = SerialPort.GetPortNames();
            for (int i = 0; i < ports.Length; i++)
            {
                comboBoxPorts.Items.Add(ports[i]);
            }                       
        }


        private void initGridView()
        {                                
            int i = 0;

            initTable();

            dataGridViewConfig.Columns["param"].ReadOnly = true;

            for (i = 0; i < cfg.getNumItems(); i++)
            {
                String[] row = new String[] { cfg.getItem(i).tag, cfg.getItem(i).value, Convert.ToString(cfg.getItem(i).type) };
                dataGridViewConfig.Rows.Add(row);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            int i = 0;
            int j = 0;
            byte[] rx = new byte[16];
            int rxdata = 0;

            gridViewToData();

            int chk = cfg.checksum();

            rx[0] = 0;

            serialPort.PortName = comboBoxPorts.GetItemText(comboBoxPorts.SelectedItem);
            serialPort.BaudRate = 115200;
            serialPort.Open();
            serialPort.Write("c");

            do
            {
                rxdata = serialPort.ReadChar();
            } while (rxdata != 8);

            
            Debug.Print("Proceed received");

            serialPort.Write(BitConverter.GetBytes(Convert.ToByte(cfg.getSize())),0,1);
            serialPort.Write(BitConverter.GetBytes(Convert.ToByte(cfg.getNumItems())), 0, 1);
            serialPort.Write(BitConverter.GetBytes(Convert.ToByte(cfg.getProductId())), 0, 1);
            serialPort.Write(BitConverter.GetBytes(Convert.ToByte(cfg.getVersionId())), 0, 1);

            foreach (configItem item in cfg.getData())
            {
                byte[] raw = item.getRawData();

                for (j = 0; j < item.numBytes; j++)
                {
                    String s2 = String.Format("{0}", raw[j]);
                    Debug.Print(s2);
                    serialPort.Write(raw, j, 1);
                }
            }       
           
            serialPort.Write(BitConverter.GetBytes(cfg.checksum()), 0, 1);

            do
            {
                rxdata = serialPort.ReadChar();
            } while (rxdata != 1 && rxdata != 2);

            if(rxdata == 1)
                Debug.Print("download ok");

            if(rxdata == 2)
                Debug.Print("wrong checksum");

            serialPort.Close();   
        }

        private void dataGridViewConfig_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
    //        Debug.Print("edit");
    ////        Debug.Print(cfg.getItem(e.RowIndex).tag);
    //        if (dataGridViewConfig.Rows[e.RowIndex].Cells["val"].Value != null)
    //            cfg.getItem(e.RowIndex).value = Convert.ToString(dataGridViewConfig.Rows[e.RowIndex].Cells["val"].Value);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            gridViewToData();

            SaveFileDialog cfgSave = new SaveFileDialog();
            cfgSave.Title = "Save Configuration";
            cfgSave.Filter = "cfg files|*.cfg";

            if (cfgSave.ShowDialog() == DialogResult.OK)
            {
                cfg.saveData(cfgSave.FileName.ToString());
            }            
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog cfgSelect = new OpenFileDialog();
            cfgSelect.Title = "Open Configuration";
            cfgSelect.Filter = "cfg files|*.cfg";
     
            if (cfgSelect.ShowDialog() == DialogResult.OK)
            {                
                if (!cfg.isEmpty())
                    cfg.clear();

                cfg.loadData(cfgSelect.FileName.ToString());
                initGridView();             
            }

            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void modifyConfigurationStructureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridViewConfig.AllowUserToAddRows = true;
            dataGridViewConfig.AllowUserToDeleteRows = true;
            dataGridViewConfig.Columns["param"].ReadOnly = false;
            dataGridViewConfig.Columns["dataType"].Visible = true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void removeSelectedFieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = dataGridViewConfig.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    dataGridViewConfig.Rows.RemoveAt(dataGridViewConfig.SelectedRows[0].Index);
                }
            }
        }

        private void buttonFromDevice_Click(object sender, EventArgs e)
        {
            int rxdata = 0;
            int i=0;
            byte[] buffer = new byte[256];
            configHeader cfgHeader = new configHeader();

            serialPort.PortName = comboBoxPorts.GetItemText(comboBoxPorts.SelectedItem);
            serialPort.BaudRate = 115200;
            serialPort.Open();
            serialPort.Write("d");

            do
            {
                buffer[i] = (byte)serialPort.ReadByte();
                Debug.Print(Convert.ToString(buffer[i]));
                i++;
            } while (serialPort.BytesToRead > 0);

            Debug.Print("received");

            Debug.Print("rxdata = " + Convert.ToString(rxdata));

            if (buffer[0] == 9)
                Debug.Print("not configured");
            else if(buffer[0] == 0xA5)
            {
                Debug.Print("load configuration from device");
                // load config dump
                cfgHeader.size = (byte)buffer[1]; //size
                cfgHeader.numItems = (byte)buffer[2]; //num items
                cfgHeader.prodId = (byte)buffer[3]; //prod id
                cfgHeader.versionId = (byte)buffer[4]; //version id                              

                if (cfg.loadTemplate(cfgHeader.prodId, cfgHeader.versionId, Application.StartupPath))
                {
                    cfg.fromBuffer(buffer);
                    initGridView();
                }
                else
                    MessageBox.Show("No template for version ID = product ID =",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error); 

            }

            serialPort.Close();
 
        }



        private void stopEditingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridViewConfig.AllowUserToAddRows = false;
            dataGridViewConfig.AllowUserToDeleteRows = false;
            dataGridViewConfig.Columns["param"].ReadOnly = true;
            dataGridViewConfig.Columns["dataType"].Visible = false;
        }

        void gridViewToData()
        {
            configItem item;
            String sParam;
            int dataType;
            String sValue;
            byte size = 0;

            cfg.clear();

            foreach (DataGridViewRow row in dataGridViewConfig.Rows)
            {
                if (row.Index < dataGridViewConfig.RowCount)
                {
                    sParam = row.Cells["param"].Value.ToString();
                    dataType = Convert.ToInt32(row.Cells["dataType"].Value);
                    sValue = row.Cells["val"].Value.ToString();
                    item = new configItem(sParam, sValue, dataType);
                    size += item.getSize();

                    cfg.addItem(item);
                }
            }

            cfg.updateHeader(0, 0);
        }

        private void createTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            configItem item;
            String sParam;
            int sDataType;
            byte size = 0;
            templateDlg dlg;

            cfg.setNumItems(dataGridViewConfig.RowCount-1);

            foreach (DataGridViewRow row in dataGridViewConfig.Rows)
            {
                if (row.Index < dataGridViewConfig.RowCount-1)
                {
                    sParam = row.Cells["param"].Value.ToString();
                    sDataType = Convert.ToInt32(row.Cells["dataType"].Value);
                    item = new configItem(sParam, "", sDataType);
                    size += item.getSize();

                    cfg.addItem(item);
                }
            }

            dlg = new templateDlg();
                        
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                cfg.setProductId(dlg.getProductID());
                cfg.setVersionId(dlg.getVersionID());
                SaveFileDialog cfgSelect = new SaveFileDialog();

                cfgSelect.Title = "Save template";
                cfgSelect.Filter = "ctp files|*.ctp";
                cfgSelect.InitialDirectory = Application.StartupPath;
                if (cfgSelect.ShowDialog() == DialogResult.OK)
                {
                    cfg.saveData(cfgSelect.FileName.ToString());
                }
            }
        }

        private void buttonErase_Click(object sender, EventArgs e)
        {
            serialPort.PortName = comboBoxPorts.GetItemText(comboBoxPorts.SelectedItem);
            serialPort.BaudRate = 115200;
            serialPort.Open();
            serialPort.Write("e");
            serialPort.Close();
        }
    }
}
