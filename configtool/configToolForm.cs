using System;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Ports;
using System.Data;
using System.Globalization;
using System.Reflection;
using QRCoder;
using System.IO;

namespace configtool
{
    public partial class FormConfig : Form
    {
        Configuration cfg;
        Configuration tempCfg;       // for template loading

        bool bIsComboBox = false;
        delegate void SetComboBoxCellType(int iRowIndex);
        bool editMode = false;
        bool adminMode = false;
        int languageCode;
        bool qrShown;
        String exportTxtData;

        PictureBox pbQR;

        String dataFolder;
        String version;

        public static int TIMEOUT_LEN = 5000;

        Timeout tout;

        enum msgStrings
        {
            MSG_SENT = 0,
            MSG_CHKSUM_ERR,
            MSG_NOT_RESP,
            MSG_NO_CFG,
            MSG_NO_TEMPL,
            MSG_LOADED,
            MSG_EMPTY,
            MSG_ERASED,
            MSG_MISSING_DATA,
            MSG_FORMAT

        };

        String[] msgBoxStrings = new String[10];

        public FormConfig()
        {
            InitializeComponent();

            toolStripStatusLabel.Width = (this.Width - 40) / 2;
            toolStripProgressBar.Width = (this.Width - 40) / 2;

            dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ConfigurationTool";
            version = Assembly.GetEntryAssembly().GetName().Version.ToString();

            this.Text = "Configuration Tool " + version;
            pbQR = new PictureBox();
            qrShown = false;
        }

        private void initTable()
        {
            dataGridViewConfig.Columns.Clear();
            dataGridViewConfig.Columns.Add("param", "Parameter");
            dataGridViewConfig.Columns.Add("val", "Value");
            dataGridViewConfig.Columns.Add("dataType", "Type");
            dataGridViewConfig.Columns.Add("description", "Description");

            // BLE data
            dataGridViewConfig.Columns.Add("uuid", "UUID");
            dataGridViewConfig.Columns.Add("r", "Read");
            dataGridViewConfig.Columns.Add("w", "Write");
            dataGridViewConfig.Columns.Add("n", "Notify");

            DataGridViewColumn colDesc = dataGridViewConfig.Columns[3];
            colDesc.Width = 300;
            dataGridViewConfig.AllowUserToAddRows = false;
            dataGridViewConfig.AllowUserToDeleteRows = false;
            dataGridViewConfig.Columns["dataType"].Visible = false;
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            initTable();
            textBoxData.Visible = false;

            String[] args = Environment.GetCommandLineArgs();
            if (args.Length == 1)   // no arguments
                languageCode = -1;    // system default language
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

            msgBoxStrings[(int)msgStrings.MSG_SENT] = rm.GetString("msgSent");
            msgBoxStrings[(int)msgStrings.MSG_CHKSUM_ERR] = rm.GetString("msgChksum");
            msgBoxStrings[(int)msgStrings.MSG_NOT_RESP] = rm.GetString("msgNotResp");
            msgBoxStrings[(int)msgStrings.MSG_NO_CFG] = rm.GetString("msgNotCfg");
            msgBoxStrings[(int)msgStrings.MSG_NO_TEMPL] = rm.GetString("msgNoTempl");
            msgBoxStrings[(int)msgStrings.MSG_LOADED] = rm.GetString("msgCfgLoaded");
            msgBoxStrings[(int)msgStrings.MSG_EMPTY] = rm.GetString("msgCfgEmpty");
            msgBoxStrings[(int)msgStrings.MSG_ERASED] = rm.GetString("msgErased");
            msgBoxStrings[(int)msgStrings.MSG_MISSING_DATA] = rm.GetString("msgMissingData");
            msgBoxStrings[(int)msgStrings.MSG_FORMAT] = rm.GetString("msgFormat");
        }

        // hides admin menu items if no admin user
        private void setAdmin(bool mode)
        {
            if (mode == false)
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
            String[] row;
            initTable();

            dataGridViewConfig.Columns["param"].ReadOnly = true;

            for (i = 0; i < cfg.getNumItems(); i++)
            {

                if(cfg.getItem(i).ble != null)
                {
                    row = new String[] { cfg.getItem(i).tag,
                                              cfg.getItem(i).value,
                                              Convert.ToString(cfg.getItem(i).type),
                                              cfg.getItem(i).descript,
                                              cfg.getItem(i).ble.uuid,
                                              cfg.getItem(i).ble.r.ToString(),
                                              cfg.getItem(i).ble.w.ToString(),
                                              cfg.getItem(i).ble.n.ToString()
                                            };
                }
                else
                {
                    row = new String[] { cfg.getItem(i).tag,
                                              cfg.getItem(i).value,
                                              Convert.ToString(cfg.getItem(i).type),
                                              cfg.getItem(i).descript,
                                              "",
                                              "",
                                              "",
                                              ""
                                            };
                }
                
                dataGridViewConfig.Rows.Add(row);
            }

        }

        private void waitTargetReply(Configuration.cfgReply reply)
        {
            int rxdata = 0;

            do
            {
                rxdata = serialPort.ReadByte();
                tout.check();
            } while (rxdata != (int)reply);

            Debug.Print("waitTargetReply -> " + reply.ToString());
        }

        private void sendCommand(Configuration.cfgCommand cmd)
        {
            byte[] buff = new byte[1];

            buff[0] = (byte)cmd;
            serialPort.Write(buff, 0, 1);
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            int j = 0;
            byte[] rx = new byte[16];
            int rxdata = 0;

            if (cfg.checkData() && validateGridData())
            {
                tout = new Timeout(TIMEOUT_LEN);
                Cursor.Current = Cursors.WaitCursor;

                gridViewToData(true);

                toolStripProgressBar.Maximum = cfg.getSize();
                toolStripProgressBar.Step = 1;
                toolStripProgressBar.Value = 0;
                statusStrip.Refresh();

                int chk = cfg.checksum();

                rx[0] = 0;

                serialPort.PortName = comboBoxPorts.GetItemText(comboBoxPorts.SelectedItem);
                serialPort.BaudRate = 115200;
                serialPort.WriteTimeout = 5000;
                serialPort.ReadTimeout = 5000;
                serialPort.Open();

                try
                {
                    sendCommand(Configuration.cfgCommand.CFG_CONFIG);

                    waitTargetReply(Configuration.cfgReply.CFG_LOAD_PROCEED);

                    Debug.Print("Proceed received");

                    serialPort.Write(BitConverter.GetBytes(Convert.ToByte(cfg.getSize())), 0, 1);
                    Debug.Print(cfg.getSize().ToString());
                    waitTargetReply(Configuration.cfgReply.CFG_LOAD_PROCEED);

                    serialPort.Write(BitConverter.GetBytes(Convert.ToByte(cfg.getNumItems())), 0, 1);
                    Debug.Print(cfg.getNumItems().ToString());
                    waitTargetReply(Configuration.cfgReply.CFG_LOAD_PROCEED);

                    serialPort.Write(BitConverter.GetBytes(Convert.ToByte(cfg.getProductId())), 0, 1);
                    Debug.Print(cfg.getProductId().ToString());
                    waitTargetReply(Configuration.cfgReply.CFG_LOAD_PROCEED);

                    serialPort.Write(BitConverter.GetBytes(Convert.ToByte(cfg.getVersionId())), 0, 1);
                    Debug.Print(cfg.getVersionId().ToString());
                    waitTargetReply(Configuration.cfgReply.CFG_LOAD_PROCEED);

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
                            waitTargetReply(Configuration.cfgReply.CFG_LOAD_PROCEED);
                            Debug.Print("Proceed received");
                            tout.check();

                            toolStripProgressBar.PerformStep();
                            statusStrip.Refresh();
                        }
                    }

                    Debug.Print("data written");

                    serialPort.Write(BitConverter.GetBytes(cfg.checksum()), 0, 1);

                    Debug.Print("checksum written");

                    do
                    {
                        rxdata = serialPort.ReadChar();
                        tout.check();
                    } while (rxdata != (int)Configuration.cfgReply.CFG_LOAD_OK && rxdata != (int)Configuration.cfgReply.CFG_LOAD_WRONGCHECK);

                    Debug.Print("reply received");


                    if (rxdata == (int)Configuration.cfgReply.CFG_LOAD_OK)
                        toolStripStatusLabel.Text = msgBoxStrings[(int)msgStrings.MSG_SENT];

                    if (rxdata == (int)Configuration.cfgReply.CFG_LOAD_WRONGCHECK)
                        MessageBox.Show(msgBoxStrings[(int)msgStrings.MSG_CHKSUM_ERR], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    statusStrip.Refresh();

                    serialPort.Close();
                    Cursor.Current = Cursors.Default;

                }
                catch (TimeoutException)
                {
                    Cursor.Current = Cursors.Default;

                    Debug.Print("Timeout");
                    MessageBox.Show(msgBoxStrings[(int)msgStrings.MSG_NOT_RESP], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tout.stop();
                    serialPort.Close();
                }
            }
            else
                MessageBox.Show(msgBoxStrings[(int)msgStrings.MSG_MISSING_DATA], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
            saveConfigurationFile();
        }

        string configFileOpen()
        {
            OpenFileDialog cfgSelect = new OpenFileDialog();

            // http://stackoverflow.com/questions/6751188/openfiledialog-c-slow-on-any-file-better-solution
            cfgSelect.AutoUpgradeEnabled = false;
            cfgSelect.DereferenceLinks = false;

            cfgSelect.InitialDirectory = dataFolder + "\\cfg";
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
                    return cfgSelect.FileName.ToString();
                }
                else
                {
                    return "";
                }
            }
            else
                return "";
        }

        void openConfigurationFile()
        {
            cfg.name = configFileOpen();

            if (cfg.name != "")
            {
                labelCfgName.Text = cfg.name;
                //      labelIdentifiers.Text = "Product ID:"+cfg.getProductId().ToString() + " Version ID:" + cfg.getVersionId().ToString();

                updateLabels(cfg);
            }
            else
                MessageBox.Show(msgBoxStrings[(int)msgStrings.MSG_FORMAT], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void saveConfigurationFile()
        {
            if (dataGridViewConfig.Rows.Count == 0)
            {
                MessageBox.Show(msgBoxStrings[(int)msgStrings.MSG_EMPTY], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                cfg.clearDataOnly();
                gridViewToData(false);

                if (cfg.checkData() && validateGridData())
                {
                    SaveFileDialog cfgSave = new SaveFileDialog();
                    cfgSave.Title = "Save Configuration";
                    cfgSave.Filter = "cfg files|*.cfg";

                    cfgSave.FileName = cfg.name;

                    if (cfgSave.ShowDialog() == DialogResult.OK)
                    {
                        cfg.saveData(cfgSave.FileName.ToString());
                    }
                }
                else
                    MessageBox.Show(msgBoxStrings[(int)msgStrings.MSG_MISSING_DATA], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            showDataGrid();

            openConfigurationFile();
        }

        private void updateLabels(Configuration cfg)
        {
            if (cfg.name == "" || cfg.name == null)
                labelCfgName.Text = "Read from target";
            else
                labelCfgName.Text = cfg.name;

            labelIdentifiers.Text = "Product ID:" + cfg.getProductId().ToString() + " Version ID:" + cfg.getVersionId().ToString();
        }

        private void modifyConfigurationStructureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allowEditing();

            editMode = true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            templateDlg dlg;
            String templateName = "";

            cfg.clear();
            initGridView();
            setLanguage(languageCode);

            dlg = new templateDlg();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                cfg.setProductId(dlg.getProductID());
                cfg.setVersionId(dlg.getVersionID());

                if (cfg.loadTemplate(cfg.getProductId(), cfg.getVersionId(), Application.StartupPath, ref templateName))
                {
                    initGridView();
                    setLanguage(languageCode);
                }
                else
                {
                    String msg = String.Format(msgBoxStrings[(int)msgStrings.MSG_NO_TEMPL],
                                                dlg.getProductID(),
                                                dlg.getVersionID());
                    MessageBox.Show(msg,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    initGridView();
                    setLanguage(languageCode);
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
            String templateName = "";
            int i = 0;
            byte[] buffer = new byte[256];
            configHeader cfgHeader = new configHeader();
            tout = new Timeout(TIMEOUT_LEN);

            serialPort.PortName = comboBoxPorts.GetItemText(comboBoxPorts.SelectedItem);
            serialPort.BaudRate = 115200;
            serialPort.WriteTimeout = 5000;
            serialPort.ReadTimeout = 5000;
            try
            {
                serialPort.Open();

                toolStripProgressBar.Maximum = cfg.getSize();
                toolStripProgressBar.Step = 1;
                toolStripProgressBar.Value = 0;

                statusStrip.Refresh();

                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    sendCommand(Configuration.cfgCommand.CFG_DUMP);

                    do
                    {
                        buffer[i] = (byte)serialPort.ReadByte();
                        Debug.Print(Convert.ToString(buffer[i]));
                        i++;
                        tout.check();

                        toolStripProgressBar.PerformStep();
                        statusStrip.Refresh();

                    } while (serialPort.BytesToRead > 0);

                    statusStrip.Refresh();
                    Debug.Print("received");

                    if (buffer[0] == (int)Configuration.cfgReply.CFG_NOT_CONFIGURED)
                    {
                        toolStripProgressBar.Value = 0;
                        statusStrip.Refresh();
                        MessageBox.Show(msgBoxStrings[(int)msgStrings.MSG_NO_CFG], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (buffer[0] == Configuration.CFG_PRESENT)
                    {
                        Debug.Print("load configuration from device");
                        // load config dump
                        cfgHeader.size = (byte)buffer[1]; //size
                        cfgHeader.numItems = (byte)buffer[2]; //num items
                        cfgHeader.prodId = (byte)buffer[3]; //prod id
                        cfgHeader.versionId = (byte)buffer[4]; //version id                              

                        if (cfg.loadTemplate(cfgHeader.prodId, cfgHeader.versionId, dataFolder + "\\cft", ref templateName))
                        {
                            Cursor.Current = Cursors.Default;
                            cfg.fromBuffer(buffer);
                            initGridView();
                            setLanguage(languageCode);
                            //    MessageBox.Show(msgBoxStrings[(int)msgStrings.MSG_LOADED], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            toolStripStatusLabel.Text = msgBoxStrings[(int)msgStrings.MSG_LOADED];
                            updateLabels(cfg);
                        }
                        else
                        {
                            Cursor.Current = Cursors.Default;
                            String msg = String.Format(msgBoxStrings[(int)msgStrings.MSG_NO_TEMPL],
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
                catch (TimeoutException)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Debug.Print("Timeout");
                    MessageBox.Show(msgBoxStrings[(int)msgStrings.MSG_NOT_RESP], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    serialPort.Close();
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("COM port ERR", "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void setGridBackgroundColor(System.Drawing.Color clr)
        {
            int i;

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.BackColor = clr;
            style.ForeColor = System.Drawing.Color.Black;
            foreach (DataGridViewRow row in dataGridViewConfig.Rows)
            {
                for (i = 0; i < dataGridViewConfig.ColumnCount; i++)
                    row.Cells[i].Style = style;
            }
        }

        void stopEditing()
        {
            dataGridViewConfig.AllowUserToAddRows = false;
            dataGridViewConfig.AllowUserToDeleteRows = false;
            dataGridViewConfig.Columns["param"].ReadOnly = true;
            dataGridViewConfig.Columns["dataType"].Visible = false;
            dataGridViewConfig.Columns["val"].ReadOnly = false;

            setGridBackgroundColor(System.Drawing.Color.White);

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
            dataGridViewConfig.Columns["val"].ReadOnly = true;

            setGridBackgroundColor(System.Drawing.Color.Yellow);

            stopEditingToolStripMenuItem.Enabled = true;
            modifyConfigurationStructureToolStripMenuItem.Enabled = false;
            newToolStripMenuItem.Enabled = false;
            removeSelectedFieldToolStripMenuItem.Enabled = true;
            createTemplateToolStripMenuItem.Enabled = true;

            editMode = true;
        }


        bool validateGridData()
        {
            bool inputError = false;
            Int32 i32;
            Int16 i16;
            UInt32 ui32;
            UInt16 ui16;
            Double d;
            Byte b;

            foreach (DataGridViewRow row in dataGridViewConfig.Rows)
            {
                if (row.Index < dataGridViewConfig.RowCount)
                {
                    if (row.Cells["param"].Value == null)
                        inputError = true;

                    if (row.Cells["dataType"].Value == null)
                        inputError = true;

                    if (row.Cells["description"].Value == null)
                        inputError = true;

                    if (row.Cells["val"].Value == null)
                        inputError = true;

                    if (row.Cells["dataType"].Value.ToString() == configItem.getTypeName(configItem.TYPES.INT32) && !Int32.TryParse(row.Cells["val"].Value.ToString(), out i32))
                        inputError = true;

                    if (row.Cells["dataType"].Value.ToString() == configItem.getTypeName(configItem.TYPES.UINT32) && !UInt32.TryParse(row.Cells["val"].Value.ToString(), out ui32))
                        inputError = true;

                    if (row.Cells["dataType"].Value.ToString() == configItem.getTypeName(configItem.TYPES.INT16) && !Int16.TryParse(row.Cells["val"].Value.ToString(), out i16))
                        inputError = true;

                    if (row.Cells["dataType"].Value.ToString() == configItem.getTypeName(configItem.TYPES.UINT16) && !UInt16.TryParse(row.Cells["val"].Value.ToString(), out ui16))
                        inputError = true;

                    if (row.Cells["dataType"].Value.ToString() == configItem.getTypeName(configItem.TYPES.FLOAT) && !Double.TryParse(row.Cells["val"].Value.ToString(), out d))
                        inputError = true;

                    if (row.Cells["dataType"].Value.ToString() == configItem.getTypeName(configItem.TYPES.UINT8) && !Byte.TryParse(row.Cells["val"].Value.ToString(), out b))
                        inputError = true;
                }
            }

            return !inputError;
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

            String uuidValue;
            bool rValue;
            bool wValue;
            bool nValue;

            bleData ble;

            byte size = 0;

            if (clr == true)
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

                    if (row.Cells["description"].Value != null)
                        description = row.Cells["description"].Value.ToString();
                    else
                        description = "";

                    if (row.Cells["uuid"].Value != null)
                        uuidValue = row.Cells["uuid"].Value.ToString();
                    else
                        uuidValue = "";

                    if (row.Cells["r"].Value != null)
                        rValue = row.Cells["r"].Value.Equals("True") ? true : false;
                    else
                        rValue = false;

                    if (row.Cells["w"].Value != null)
                        wValue = row.Cells["w"].Value.Equals("True") ? true : false;
                    else
                        wValue = false;

                    if (row.Cells["n"].Value != null)
                        nValue = row.Cells["n"].Value.Equals("True") ? true : false;
                    else
                        nValue = false;

                    ble = new bleData(uuidValue, rValue, wValue, nValue);

                    item = new configItem(sParam, sValue, dataType, description, ble);
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
                MessageBox.Show(msgBoxStrings[(int)msgStrings.MSG_EMPTY], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (editMode)
                {
                    stopEditing();
                }

                cfg.setNumItems(dataGridViewConfig.RowCount);

                if (cfg.getProductId() == 0)     // if product/version ID existing, just update
                    cfg.clear();
                else
                    cfg.clearDataOnly();

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

                        if (inputError == false)
                        {
                            item = new configItem(sParam, "", sDataType, desc, new bleData("", false, false, false));
                            size += item.getSize();

                            cfg.addItem(item);
                        }
                        else
                            MessageBox.Show("Mandatory data missing - Row : " + row.Index, "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (inputError == false)
                {
                    dlg = new templateDlg();

                    cfg.updateHeader();

                    // show current values
                    dlg.setProductID(cfg.getProductId());
                    dlg.setVersionID(cfg.getVersionId());

                    dlg.Refresh();

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        cfg.setProductId(dlg.getProductID());
                        cfg.setVersionId(dlg.getVersionID());
                        SaveFileDialog cfgSelect = new SaveFileDialog();

                        cfgSelect.Title = "Save template";
                        cfgSelect.Filter = "Configuration template files|*.cft";
                        cfgSelect.InitialDirectory = dataFolder;
                        if (cfgSelect.ShowDialog() == DialogResult.OK)
                        {
                            //       gridViewToData(false);
                            cfg.saveData(cfgSelect.FileName.ToString());

                            String tmplName = "";

                            tempCfg = new Configuration();
                            tempCfg.setProductId(cfg.getProductId());
                            tempCfg.setVersionId(cfg.getVersionId());

                            tempCfg.loadTemplate(cfg.getProductId(), cfg.getVersionId(), dataFolder, ref tmplName);
                            cfg.saveData(tmplName);

                            updateLabels(cfg);
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
            tout = new Timeout(TIMEOUT_LEN);
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                serialPort.PortName = comboBoxPorts.GetItemText(comboBoxPorts.SelectedItem);
                serialPort.BaudRate = 115200;
                serialPort.Open();

                sendCommand(Configuration.cfgCommand.CFG_ERASE);
                Debug.Print("erase command sent");

                waitTargetReply(Configuration.cfgReply.CFG_ASK_CONFIRM_ERASE);
                Debug.Print("CFG_ASK_CONFIRM_ERASE recv");


                sendCommand(Configuration.cfgCommand.CFG_CONFIRM_ERASE);
                Debug.Print("CFG_CONFIRM_ERASE sent");
                waitTargetReply(Configuration.cfgReply.CFG_ERASED);

                Debug.Print("CFG_ERASED recv");
                serialPort.Close();
                Cursor.Current = Cursors.Default;
                toolStripStatusLabel.Text = msgBoxStrings[(int)msgStrings.MSG_ERASED];

            }
            catch (TimeoutException)
            {
                Debug.Print("Timeout");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(msgBoxStrings[(int)msgStrings.MSG_NOT_RESP], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                for (int i = 0; i < configItem.dataTypeNames.Length; i++)
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
            openConfigurationFile();
        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridViewConfig_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            setGridBackgroundColor(System.Drawing.Color.Yellow);
        }

        private void showDataGrid()
        {
            this.Controls.Remove(pbQR);
            dataGridViewConfig.Visible = true;
            qrShown = false;

            buttonQR.Text = "QR Code";
        }

        private void hideDataGrid()
        {
            this.Controls.Add(pbQR);
            dataGridViewConfig.Visible = false;
            qrShown = true;

            buttonQR.Text = "Data grid";
        }

        private void buttonQRCode_Click(object sender, EventArgs e)
        {
            int j = 0;
            int i = 0;
            String data = "@0000\r\n";
            String qrData = "";
            String qrParams = "";

            if (qrShown)
            {
                showDataGrid();
                textBoxData.Visible = false;
            }
            else
            {
                textBoxData.Visible = true;

                if (cfg.checkData() && validateGridData())
                {
                    gridViewToData(true);

                    foreach (configItem item in cfg.getData())
                    {
                        byte[] raw = item.getRawData();

                        if (item.typeCode == 6)
                        {
                            item.numBytes = 8;
                            if (item.value.Length < 8)
                                item.value += new string(' ', 8 - item.value.Length);

                            raw = Encoding.ASCII.GetBytes(item.value);
                        }

                        for (j = 0; j < item.numBytes; j++)
                        {
                            i++;
                            String s2 = String.Format("{0:X2}", raw[j]) + " ";
                            qrData += s2;
                            data += s2;

                            if (i % 16 == 0)
                                data += "\r\n";
                        }

                        qrParams += (item.tag + "," + item.typeCode + ";");
                    }

                    exportTxtData = qrParams;

                    qrData += ";" + qrParams;

                    Debug.Print("qrData = " + qrData);
                    Debug.Print(data);

                    dataGridViewConfig.Visible = false;

                    System.Drawing.Point location = dataGridViewConfig.Location;
                    location.Offset(10, 10);

                    pbQR.Location = location;


                    pbQR.Size = new System.Drawing.Size(300, 300);

                    // https://github.com/codebude/QRCoder
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.L);
                    QRCode qrCode = new QRCode(qrCodeData);
                    System.Drawing.Bitmap qrCodeImage = qrCode.GetGraphic(20);

                    pbQR.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbQR.Image = qrCodeImage;
                    hideDataGrid();

                    textBoxData.Text = qrData + "\r\n\r\nsize: " + i;
                }
                else
                    MessageBox.Show(msgBoxStrings[(int)msgStrings.MSG_MISSING_DATA], "Configuration Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void addParameterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int32 selectedRowIndex = dataGridViewConfig.CurrentRow.Index;

            String[] row = new String[] { "",
                                              "",
                                              "",
                                              ""};

            dataGridViewConfig.Rows.Insert(selectedRowIndex, row);

        }

        private void labelIdentifiers_Click(object sender, EventArgs e)
        {

        }

        String getConfigItemsDescription()
        {
            String description = "";

            foreach (configItem item in cfg.getData())
            {
                description += (item.tag + "," + item.typeCode + ";");
            }

            return description;
        }

        private void exportStructureFiletxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog exportStructureFileDialog = new SaveFileDialog();
            exportStructureFileDialog.Title = "Export structure file";
            exportStructureFileDialog.Filter = "txt files|*.txt";
            exportTxtData = "";
            exportStructureFileDialog.FileName = cfg.name.Split('.')[0];

            if (exportStructureFileDialog.ShowDialog() == DialogResult.OK)
            {
                exportTxtData = getConfigItemsDescription();

                StreamWriter stream = new StreamWriter(exportStructureFileDialog.FileName, false);

                using (stream)
                {
                    stream.WriteLine(exportTxtData);
                }

                stream.Close();
            }
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showDataGrid();

            openConfigurationFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveConfigurationFile();
        }

        private void exportCSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String listing = @"// code generated by configtool, DO NOT EDIT

#ifndef INCLUDE_CONFIG_H_ 
#define INCLUDE_CONFIG_H_
#include ""platform_types.h""
#define CONFIG_KEY      0xA5
#define CONFIG_VERSION  "+ cfg.getVersionId()+@"
#define CFG_ITEMS_DESCRIPTION ";

            listing += @"""";
            listing += getConfigItemsDescription();
            listing += @"""";

            listing += @"
// configuration
typedef struct
{
";
            // list struct fields

            foreach (configItem item in cfg.getData())
            {
                listing += "    " + item.type;
                listing += " ";
                listing += item.tag;
                listing += ";   // " + item.descript + "\r\n";

            }

            listing += @"} CFG_PARAMS;

CFG_PARAMS* getConfig();
void loadConfig(void);
void saveConfig(void);
uint8_t isConfigValid(CFG_PARAMS* cfg);
void eraseConfig(void);

#endif /* INCLUDE_CONFIG_H_ */";

            // TODO save file dialog scelta nome "config.h" default
            exportSource("config.h", listing);
        }

        void exportSource(String filename, String listing)
        {
            StreamWriter stream = new StreamWriter(filename, false);

            using (stream)
            {
                stream.WriteLine(listing);
            }

            stream.Close();
        }

        private void exportBLEProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BLEServiceExportDlg bLEServiceExportDlg = new BLEServiceExportDlg(cfg.serviceExportInfo);
      //      bLEServiceExportDlg.info = cfg.serviceExportInfo;
            bLEServiceExportDlg.cfg = cfg;

            bLEServiceExportDlg.ShowDialog();
            exportBLEProfile(bLEServiceExportDlg.info.srvName,
                             bLEServiceExportDlg.info.srvUUID,
                             bLEServiceExportDlg.info.charFirstUUID,
                             bLEServiceExportDlg.info.folder,
                             bLEServiceExportDlg.info.baseName);


        }

        void exportBLEProfile(String srvName, String srvUUID, String charFirstUUID, String folder, String baseName)
        { 
            int index = 0;
            int charFirstUUIDVal = Convert.ToInt32(charFirstUUID, 16); 

            String srvUuid = srvUUID;


            String srvNameTag = "BLE_" + srvName + "_";
            String listing = @"// code generated by configtool, DO NOT EDIT

// service UUID

#define "+srvNameTag+"UUID    "+ srvUuid+@"

// characteristics UUIDs

";

            foreach (configItem item in cfg.getData())
            {
                listing += "#define " + srvNameTag + item.tag+"  " + index+ "\r\n";
                listing += "#define " + srvNameTag + item.tag + "_UUID   "+ string.Format("0x{0:x16}", (charFirstUUIDVal + index)) +"\r\n";
                listing += "#define " + srvNameTag + item.tag + "_LEN    " + item.getSize()+ "\r\n";
                listing += "\r\n";
                index++;
            }

            exportSource(folder+"\\"+baseName+"_uuids.h", listing);


            listing = @"typedef struct
{
    uint8_t paramID;
    gattAttribute_t *gattAttr;
    gattAttribute_t *config;
    uint8_t size;

} CHARACTERISTIC_DATA;

/*********************************************************************
* GLOBAL VARIABLES
*/
CONST uint8_t serviceUUID[ATT_UUID_SIZE] =
{
  TI_BASE_UUID_128("+srvNameTag+@"UUID)
};

";

            foreach (configItem item in cfg.getData())
            {
                listing += "CONST uint8_t " + (srvNameTag + item.tag).ToUpper() + "_UUID[ATT_UUID_SIZE] = \r\n{\r\n";
                listing += "TI_BASE_UUID_128("+ srvNameTag + item.tag + "_UUID)\r\n";
                listing += "};\r\n";
                listing += "\r\n";
            }

            listing += @"
/*********************************************************************
* Profile Attributes - variables
*/

/* properties GATT_PROP_READ/WRITE are from host point of view
   READ is a data read by the host and transferred from the target
   WRITE is a data written by the host and tranferred to the target

*/
// Service declaration
";

            listing += "static CONST gattAttrType_t serviceDecl = { ATT_UUID_SIZE, serviceUUID };\r\n";

            foreach (configItem item in cfg.getData())
            {
                listing += @"// Characteristic """+item.tag+@""" Properties (for declaration) "+item.numBytes+ "byte read by host\r\n";
                listing += @"static uint8_t "+ srvNameTag + item.tag + "Props = ";
                if (item.ble.r)
                    listing += "GATT_PROP_READ ";

                if (item.ble.r && (item.ble.w || item.ble.n))
                    listing += " | ";

                if (item.ble.w)
                    listing += "GATT_PROP_WRITE ";

                if (item.ble.w && item.ble.n)
                    listing += " | ";

                if (item.ble.n)
                    listing += "GATT_PROP_NOTIFY ";

                listing += ";\r\n";

                listing +=@"// Characteristic """+item.tag+@""" Value variable
";
                listing += @"static uint8_t "+srvNameTag + item.tag + "Val["+ srvNameTag + item.tag + "_LEN];\r\n";
                //  static uint8_t demoService_data1rVal[DEMO_SERVICE_DATA1R_LEN] = { 0 };

                if (item.ble.n)
                    listing += @"static gattCharCfg_t *" + srvNameTag + item.tag + "Config;\r\n";

            }


            listing += @"/*********************************************************************
* Profile Attributes - Table
*/";

            listing += @"static gattAttribute_t serviceAttrTbl[] =
{
    //  Service Declaration      0
    {
        { ATT_BT_UUID_SIZE, primaryServiceUUID },
            GATT_PERMIT_READ,
            0,
            (uint8_t *)&serviceDecl
    },
";
            foreach (configItem item in cfg.getData())
            {
                listing += @"

    // " + item.tag + @" Characteristic declaration
    {  
        { ATT_BT_UUID_SIZE, characterUUID },
        GATT_PERMIT_READ,
        0,
        &"+srvNameTag + item.tag + @"Props 
    },
    // " + item.tag + @" Characteristic value
    {
        { ATT_UUID_SIZE, " + (srvNameTag + item.tag).ToUpper() + @"_UUID },
        ";

                if (item.ble.r)
                    listing += "GATT_PROP_READ ";

                if (item.ble.r && item.ble.w)
                    listing += " | ";

                if (item.ble.w)
                    listing += "GATT_PROP_WRITE ";

                listing += @",
        0,
        " + srvNameTag + item.tag + @"Val
    },";

                if (item.ble.n)
                {
                    listing += @"   // " + item.tag + @" CCCD
    {
        { ATT_BT_UUID_SIZE, clientCharCfgUUID },
        GATT_PERMIT_READ | GATT_PERMIT_WRITE,
        0,
        (uint8*)&" + srvNameTag + item.tag + @"Config
    },";
                }
            }

            listing += @"
};

";
            // attribute table
            index = 2;
            listing += @"CHARACTERISTIC_DATA charactData[] = {";

            foreach (configItem item in cfg.getData())
            {

                listing += @"   
                                        {
                                            "+srvNameTag + item.tag + @",  
                                            &serviceAttrTbl[" + index + @"],
                                            ";
                if (item.ble.n)
                {
                    listing += "&serviceAttrTbl[" + (++index) + @"],
                                ";
                }
                else
                {
                    listing += @"NULL,
                                ";
                }

                listing += "            " + srvNameTag + item.tag + @"_LEN
                                        },";

                index += 2;
            }

            listing += @"
                                    };";



            exportSource(folder + "\\" + baseName +"_service.c", listing);


        }
    }
}
