using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Ports;
using System.Data;
using System.Globalization;

namespace configtool
{
    public partial class FormConfig : Form
    {
        Configuration cfg;
        bool bIsComboBox = false;
        delegate void SetComboBoxCellType(int iRowIndex);
        bool editMode = false;
        bool adminMode = false;
        int languageCode;

        Timeout tout;
        public const int MSG_SENT = 0;
        public const int MSG_CHKSUM_ERR = 1;
        public const int MSG_NOT_RESP = 2;
        public const int MSG_NO_CFG = 3;
        public const int MSG_NO_TEMPL = 4;
        public const int MSG_LOADED = 5;
        public const int MSG_EMPTY = 6;
        public const int MSG_ERASED = 7;
        public const int MSG_MISSING_DATA = 8;
        public const int MSG_FORMAT = 9;

        String[] msgBoxStrings = new String[10];

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
            dataGridViewConfig.Columns.Add("description", "Description");

            DataGridViewColumn colDesc = dataGridViewConfig.Columns[3];
            colDesc.Width = 300;
            dataGridViewConfig.AllowUserToAddRows = false;
            dataGridViewConfig.AllowUserToDeleteRows = false;
            dataGridViewConfig.Columns["dataType"].Visible = false;
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            initTable();

            String[] args = Environment.GetCommandLineArgs();
            if (args.Length == 1)   // no arguments
                languageCode =-1;    // system default language
            else
                languageCode = int.Parse(args[1]);  // force language 0-1 (EN-IT)

            setLanguage(languageCode);

            if (args.Length == 3)   // with admin mode argument, lang is mandatory
            {
                if (int.Parse(args[2]) == 1)
                    adminMode = true;
                else
                    adminMode = false;
            }
            else
            {
                adminMode = false;
            }

            setAdmin(adminMode);    // true shows admin menu items

            stopEditing();      // no edit mode at startup

            cfg = new Configuration();

            string[] ports = SerialPort.GetPortNames();

            if (ports.Length > 0)
            {
                for (int i = 0; i < ports.Length; i++)
                {
                    comboBoxPorts.Items.Add(ports[i]);
                }

                comboBoxPorts.SelectedIndex = 0;
            }
        }

        // lang code = -1 sets system language (IT/EN)
        // lang code = 0 sets EN
        // lang code = 1 sets IT
        private void setLanguage(int langCode)
        {
            String[] langs = { "en", "it" };
            String language;

            CultureInfo ci = CultureInfo.CurrentUICulture;

            if (langCode == -1)  // use system default
                language = ci.Name.Substring(0, 2);
            else
                language = langs[langCode];

            System.Reflection.Assembly cfgAssembly;
            cfgAssembly = this.GetType().Assembly;
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("configtool.Lang.langres_" + language, cfgAssembly);
            buttonOpen.Text = rm.GetString("open");
            buttonSave.Text = rm.GetString("save");
            buttonSend.Text = rm.GetString("send");
            buttonFromDevice.Text = rm.GetString("load");
            buttonErase.Text = rm.GetString("erase");
            dataGridViewConfig.Columns["param"].HeaderText = rm.GetString("tblParamCol");
            dataGridViewConfig.Columns["val"].HeaderText = rm.GetString("tblValueCol");
            dataGridViewConfig.Columns["description"].HeaderText = rm.GetString("tblDescCol");

            msgBoxStrings[MSG_SENT] = rm.GetString("msgSent");
            msgBoxStrings[MSG_CHKSUM_ERR] = rm.GetString("msgChksum");
            msgBoxStrings[MSG_NOT_RESP] = rm.GetString("msgNotResp");
            msgBoxStrings[MSG_NO_CFG] = rm.GetString("msgNotCfg");
            msgBoxStrings[MSG_NO_TEMPL] = rm.GetString("msgNoTempl");
            msgBoxStrings[MSG_LOADED] = rm.GetString("msgCfgLoaded");
            msgBoxStrings[MSG_EMPTY] = rm.GetString("msgCfgEmpty");
            msgBoxStrings[MSG_ERASED] = rm.GetString("msgErased");
            msgBoxStrings[MSG_MISSING_DATA] = rm.GetString("msgMissingData");
            msgBoxStrings[MSG_FORMAT] = rm.GetString("msgFormat");
        }

        // hides admin menu items if no admin user
        private void setAdmin(bool mode)
        {
            if(mode == false)
            {
                modifyConfigurationStructureToolStripMenuItem.Visible = false;
                stopEditingToolStripMenuItem.Visible = false;
                removeSelectedFieldToolStripMenuItem.Visible = false;
                createTemplateToolStripMenuItem.Visible = false;
            }

        }

        private void initGridView()
        {                                
            int i = 0;

            initTable();

            dataGridViewConfig.Columns["param"].ReadOnly = true;

            for (i = 0; i < cfg.getNumItems(); i++)
            {
                String[] row = new String[] { cfg.getItem(i).tag, 
                                              cfg.getItem(i).value, 
                                              Convert.ToString(cfg.getItem(i).type),
                                              cfg.getItem(i).descript};
                dataGridViewConfig.Rows.Add(row);
            }

        }

        private void waitTargetProceed()
        {
            int rxdata = 0;

            do
            {
                rxdata = serialPort.ReadChar();
                tout.check();
            } while (rxdata != Configuration.CFG_LOAD_PROCEED);
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {            
            int j = 0;
            byte[] rx = new byte[16];
            int rxdata = 0;

            if (cfg.checkData())
            {
                tout = new Timeout(10000);
                Cursor.Current = Cursors.WaitCursor;

                gridViewToData(true);

                int chk = cfg.checksum();

                rx[0] = 0;

                serialPort.PortName = comboBoxPorts.GetItemText(comboBoxPorts.SelectedItem);
                serialPort.BaudRate = 115200;
                serialPort.WriteTimeout = 5000;
                serialPort.ReadTimeout = 5000;
                serialPort.Open();

                try
                {
                    serialPort.Write("c");

                    waitTargetProceed();

                    Debug.Print("Proceed received");

                    serialPort.Write(BitConverter.GetBytes(Convert.ToByte(cfg.getSize())), 0, 1);
                    Debug.Print(cfg.getSize().ToString());
                    waitTargetProceed();

                    serialPort.Write(BitConverter.GetBytes(Convert.ToByte(cfg.getNumItems())), 0, 1);
                    Debug.Print(cfg.getNumItems().ToString());
                    waitTargetProceed();

                    serialPort.Write(BitConverter.GetBytes(Convert.ToByte(cfg.getProductId())), 0, 1);
                    Debug.Print(cfg.getProductId().ToString());
                    waitTargetProceed();

                    serialPort.Write(BitConverter.GetBytes(Convert.ToByte(cfg.getVersionId())), 0, 1);
                    Debug.Print(cfg.getVersionId().ToString());
                    waitTargetProceed();

                    Debug.Print("header written");


                    foreach (configItem item in cfg.getData())
                    {
                        byte[] raw = item.getRawData();

                        for (j = 0; j < item.numBytes; j++)
                        {
                            String s2 = String.Format("{0}", raw[j]);
                            Debug.Print(s2);
                            serialPort.Write(raw, j, 1);
                            Debug.Print("data written");
                            waitTargetProceed();
                            Debug.Print("Proceed received");
                            tout.check();
                        }
                    }

                    Debug.Print("data written");

                    serialPort.Write(BitConverter.GetBytes(cfg.checksum()), 0, 1);
                    
                    Debug.Print("checksum written");

                    do
                    {
                        rxdata = serialPort.ReadChar();
                        tout.check();
                    } while (rxdata != Configuration.CFG_LOAD_OK && rxdata != Configuration.CFG_LOAD_WRONGCHECK);

                    Debug.Print("reply received");


                    if (rxdata == Configuration.CFG_LOAD_OK)
                        MessageBox.Show(msgBoxStrings[MSG_SENT], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (rxdata == Configuration.CFG_LOAD_WRONGCHECK)
                        MessageBox.Show(msgBoxStrings[MSG_CHKSUM_ERR], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    serialPort.Close();
                    Cursor.Current = Cursors.Default;

                }
                catch (TimeoutException)
                {
                    Cursor.Current = Cursors.Default;

                    Debug.Print("Timeout");
                    MessageBox.Show(msgBoxStrings[MSG_NOT_RESP], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tout.stop();
                    serialPort.Close();
                }
            }
            else
                MessageBox.Show(msgBoxStrings[MSG_MISSING_DATA], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
            if (dataGridViewConfig.Rows.Count == 0)
            {
                MessageBox.Show(msgBoxStrings[MSG_EMPTY], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                cfg.clearDataOnly();
                gridViewToData(false);

                if (cfg.checkData())
                {
                    SaveFileDialog cfgSave = new SaveFileDialog();
                    cfgSave.Title = "Save Configuration";
                    cfgSave.Filter = "cfg files|*.cfg";

                    if (cfgSave.ShowDialog() == DialogResult.OK)
                    {
                        cfg.saveData(cfgSave.FileName.ToString());
                    }
                }
                else
                    MessageBox.Show(msgBoxStrings[MSG_MISSING_DATA], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void configFileOpen()
        {
            OpenFileDialog cfgSelect = new OpenFileDialog();

                        // http://stackoverflow.com/questions/6751188/openfiledialog-c-slow-on-any-file-better-solution
                        cfgSelect.AutoUpgradeEnabled = false;
                        cfgSelect.DereferenceLinks = false;
            
                        cfgSelect.Title = "Open Configuration";
                        cfgSelect.Filter = "cfg files|*.cfg";
     
                        if (cfgSelect.ShowDialog() == DialogResult.OK)
                        {                
                            if (!cfg.isEmpty())
                                cfg.clear();

                            if (cfg.loadData(cfgSelect.FileName.ToString()))
                            {
                                initGridView();
                                setLanguage(languageCode);
                            }
                            else
                                MessageBox.Show(msgBoxStrings[MSG_FORMAT], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }            
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            configFileOpen();
        }

        private void modifyConfigurationStructureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridViewConfig.AllowUserToAddRows = true;
            dataGridViewConfig.AllowUserToDeleteRows = true;
            dataGridViewConfig.Columns["param"].ReadOnly = false;
            dataGridViewConfig.Columns["dataType"].Visible = true;

            allowEditing();

            editMode = true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            templateDlg dlg;

            cfg.clear();
            initGridView();
            setLanguage(languageCode);

            dlg = new templateDlg();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                cfg.setProductId(dlg.getProductID());
                cfg.setVersionId(dlg.getVersionID());

                if (cfg.loadTemplate(cfg.getProductId(), cfg.getVersionId(), Application.StartupPath))
                {
                    initGridView();
                    setLanguage(languageCode);
                }
                else
                {
                    String msg = String.Format(msgBoxStrings[MSG_NO_TEMPL],
                                                dlg.getProductID(),
                                                dlg.getVersionID());
                    MessageBox.Show(msg,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
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
            int i=0;
            byte[] buffer = new byte[256];
            configHeader cfgHeader = new configHeader();
            tout = new Timeout(50000);

            serialPort.PortName = comboBoxPorts.GetItemText(comboBoxPorts.SelectedItem);
            serialPort.BaudRate = 115200;
            serialPort.WriteTimeout = 5000;
            serialPort.ReadTimeout = 5000;
            serialPort.Open();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                serialPort.Write("d");

                do
                {
                    buffer[i] = (byte)serialPort.ReadByte();
                    Debug.Print(Convert.ToString(buffer[i]));
                    i++;
                    tout.check();
                } while (serialPort.BytesToRead > 0);

                Debug.Print("received");

                if (buffer[0] == Configuration.CFG_NOT_CONFIGURED)
                    MessageBox.Show(msgBoxStrings[MSG_NO_CFG], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (buffer[0] == Configuration.CFG_PRESENT)
                {
                    Debug.Print("load configuration from device");
                    // load config dump
                    cfgHeader.size = (byte)buffer[1]; //size
                    cfgHeader.numItems = (byte)buffer[2]; //num items
                    cfgHeader.prodId = (byte)buffer[3]; //prod id
                    cfgHeader.versionId = (byte)buffer[4]; //version id                              

                    if (cfg.loadTemplate(cfgHeader.prodId, cfgHeader.versionId, Application.StartupPath))
                    {
                        Cursor.Current = Cursors.Default;
                        cfg.fromBuffer(buffer);
                        initGridView();
                        setLanguage(languageCode);
                        MessageBox.Show(msgBoxStrings[MSG_LOADED], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        String msg = String.Format(msgBoxStrings[MSG_NO_TEMPL],
                                                    cfgHeader.prodId,
                                                    cfgHeader.versionId);
                        MessageBox.Show(msg,
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }

                }

                serialPort.Close();
            }
            catch(TimeoutException)
            {
                Cursor.Current = Cursors.WaitCursor;
                Debug.Print("Timeout");
                MessageBox.Show(msgBoxStrings[MSG_NOT_RESP], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                serialPort.Close();
            }
 
        }

        void stopEditing()
        {
            dataGridViewConfig.AllowUserToAddRows = false;
            dataGridViewConfig.AllowUserToDeleteRows = false;
            dataGridViewConfig.Columns["param"].ReadOnly = true;
            dataGridViewConfig.Columns["dataType"].Visible = false;
            stopEditingToolStripMenuItem.Enabled = false;
            modifyConfigurationStructureToolStripMenuItem.Enabled = true;
            newToolStripMenuItem.Enabled = true;
            removeSelectedFieldToolStripMenuItem.Enabled = false;
            createTemplateToolStripMenuItem.Enabled = false;

            editMode = false;
        }

        void allowEditing()
        {
            dataGridViewConfig.AllowUserToAddRows = true;
            dataGridViewConfig.AllowUserToDeleteRows = true;
            dataGridViewConfig.Columns["param"].ReadOnly = false;
            dataGridViewConfig.Columns["dataType"].Visible = true;
            stopEditingToolStripMenuItem.Enabled = true;
            modifyConfigurationStructureToolStripMenuItem.Enabled = false;
            newToolStripMenuItem.Enabled = false;
            removeSelectedFieldToolStripMenuItem.Enabled = true;
            createTemplateToolStripMenuItem.Enabled = true;

            editMode = true;
        }

        private void stopEditingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stopEditing();
        }

        void gridViewToData(bool clr)
        {
            configItem item;
            String sParam;
            String dataType;
            String sValue;
            String description;
            byte size = 0;

            if(clr == true)
                cfg.clearDataOnly();

            foreach (DataGridViewRow row in dataGridViewConfig.Rows)
            {
                if (row.Index < dataGridViewConfig.RowCount)
                {
                    sParam = row.Cells["param"].Value.ToString();
                    dataType = row.Cells["dataType"].Value.ToString();
                    if (row.Cells["val"].Value != null)
                        sValue = row.Cells["val"].Value.ToString();
                    else
                        sValue = "";

                    description = row.Cells["description"].Value.ToString();
                    item = new configItem(sParam, sValue, dataType, description);
                    size += item.getSize();

                    cfg.addItem(item);
                }
            }

            cfg.updateHeader();
        }

        private void createTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            configItem item;
            String sParam = "";
            String sDataType = "";
            String desc = "";
            byte size = 0;
            templateDlg dlg;
            bool inputError = false;

            if (dataGridViewConfig.Rows.Count == 0 || dataGridViewConfig.Rows[0].Cells["param"].Value == null)
            {
                MessageBox.Show(msgBoxStrings[MSG_EMPTY], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (editMode)
                {
                    stopEditing();
                }

                cfg.setNumItems(dataGridViewConfig.RowCount);
                cfg.clear();

                foreach (DataGridViewRow row in dataGridViewConfig.Rows)
                {
                    if (row.Index < dataGridViewConfig.RowCount)
                    {
                        if (row.Cells["param"].Value != null)
                            sParam = row.Cells["param"].Value.ToString();
                        else
                            inputError = true;

                        if (row.Cells["dataType"].Value != null)
                            sDataType = row.Cells["dataType"].Value.ToString();
                        else
                            inputError = true;

                        if (row.Cells["description"].Value != null)
                            desc = row.Cells["description"].Value.ToString();
                        else
                            inputError = true;

                        if (!inputError)
                        {
                            item = new configItem(sParam, "", sDataType, desc);
                            size += item.getSize();

                            cfg.addItem(item);
                        }
                        else
                            MessageBox.Show("Mandatory data missing", "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (!inputError)
                {
                    dlg = new templateDlg();

                    cfg.updateHeader();

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        cfg.setProductId(dlg.getProductID());
                        cfg.setVersionId(dlg.getVersionID());
                        SaveFileDialog cfgSelect = new SaveFileDialog();

                        cfgSelect.Title = "Save template";
                        cfgSelect.Filter = "Configuration template files|*.cft";
                        cfgSelect.InitialDirectory = Application.StartupPath;
                        if (cfgSelect.ShowDialog() == DialogResult.OK)
                        {
                            //       gridViewToData(false);
                            cfg.saveData(cfgSelect.FileName.ToString());
                        }
                    }
                    else
                        allowEditing();
                }
                else
                    allowEditing();
            }
        }

        private void buttonErase_Click(object sender, EventArgs e)
        {
            int rxdata = 0;
            tout = new Timeout(5000);
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                serialPort.PortName = comboBoxPorts.GetItemText(comboBoxPorts.SelectedItem);
                serialPort.BaudRate = 115200;
                serialPort.Open();
                serialPort.Write("e");

                do
                {
                    rxdata = serialPort.ReadChar();
                    tout.check();
                } while (rxdata != Configuration.CFG_ERASED);

                serialPort.Close();
                Cursor.Current = Cursors.Default;
                MessageBox.Show(msgBoxStrings[MSG_ERASED], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (TimeoutException)
            {
                Debug.Print("Timeout");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(msgBoxStrings[MSG_NOT_RESP], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                serialPort.Close();
            }
        }

        private void ChangeCellToComboBox(int iRowIndex)
        {
            if (bIsComboBox == false)
            {
                DataGridViewComboBoxCell dgComboCell = new DataGridViewComboBoxCell();

                dgComboCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                DataTable dt = new DataTable();              
                dt.Columns.Add("dataType", typeof(string));

                for (int i = 0; i < 5; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["dataType"] = configItem.dataTypeNames[i];
                    dt.Rows.Add(dr);
                }

                dgComboCell.DataSource = dt;
                dgComboCell.ValueMember = "dataType";
                dgComboCell.DisplayMember = "dataType";
                dataGridViewConfig.Rows[iRowIndex].Cells[dataGridViewConfig.CurrentCell.ColumnIndex] = dgComboCell;

                bIsComboBox = true;
            }
        }

        private void dataGridViewConfig_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            SetComboBoxCellType objChangeCellType = new SetComboBoxCellType(ChangeCellToComboBox);

            if (e.ColumnIndex == this.dataGridViewConfig.Columns["dataType"].Index)
            {
                this.dataGridViewConfig.BeginInvoke(objChangeCellType, e.RowIndex);
                bIsComboBox = false;
            } 
        }

        private void dataGridViewConfig_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataGridViewConfig.Columns["dataType"].Index)
            {              
                DataGridViewRow row = dataGridViewConfig.Rows[e.RowIndex];
                row.Cells["dataType"].Value = configItem.dataTypeNames[e.ColumnIndex];
            }
        }

        private void dataGridViewConfig_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Debug.Print(e.ToString());
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            configFileOpen();
        }
    }
}
