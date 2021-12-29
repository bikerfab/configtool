namespace configtool
{
    partial class FormConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfig));
            this.dataGridViewConfig = new System.Windows.Forms.DataGridView();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.comboBoxPorts = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyConfigurationStructureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopEditingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSelectedFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addParameterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonFromDevice = new System.Windows.Forms.Button();
            this.buttonErase = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.labelCfgName = new System.Windows.Forms.Label();
            this.labelIdentifiers = new System.Windows.Forms.Label();
            this.buttonQR = new System.Windows.Forms.Button();
            this.textBoxData = new System.Windows.Forms.TextBox();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportStructureFiletxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportBLEProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConfig)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewConfig
            // 
            this.dataGridViewConfig.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewConfig.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewConfig.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewConfig.Location = new System.Drawing.Point(13, 91);
            this.dataGridViewConfig.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewConfig.Name = "dataGridViewConfig";
            this.dataGridViewConfig.RowHeadersWidth = 51;
            this.dataGridViewConfig.Size = new System.Drawing.Size(839, 448);
            this.dataGridViewConfig.TabIndex = 0;
            this.dataGridViewConfig.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewConfig_CellEnter);
            this.dataGridViewConfig.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewConfig_CellLeave);
            this.dataGridViewConfig.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewConfig_CellValueChanged);
            this.dataGridViewConfig.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewConfig_DataError);
            this.dataGridViewConfig.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridViewConfig_UserAddedRow);
            // 
            // buttonSend
            // 
            this.buttonSend.Image = ((System.Drawing.Image)(resources.GetObject("buttonSend.Image")));
            this.buttonSend.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSend.Location = new System.Drawing.Point(863, 150);
            this.buttonSend.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(220, 48);
            this.buttonSend.TabIndex = 1;
            this.buttonSend.Text = "Send to device";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSave.Location = new System.Drawing.Point(863, 91);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(220, 48);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpen.Image")));
            this.buttonOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOpen.Location = new System.Drawing.Point(863, 32);
            this.buttonOpen.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(220, 48);
            this.buttonOpen.TabIndex = 3;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.FormattingEnabled = true;
            this.comboBoxPorts.Location = new System.Drawing.Point(863, 409);
            this.comboBoxPorts.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(124, 24);
            this.comboBoxPorts.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1096, 28);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.newToolStripMenuItem,
            this.modifyConfigurationStructureToolStripMenuItem,
            this.stopEditingToolStripMenuItem,
            this.removeSelectedFieldToolStripMenuItem,
            this.addParameterToolStripMenuItem,
            this.createTemplateToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(278, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(278, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // modifyConfigurationStructureToolStripMenuItem
            // 
            this.modifyConfigurationStructureToolStripMenuItem.Name = "modifyConfigurationStructureToolStripMenuItem";
            this.modifyConfigurationStructureToolStripMenuItem.Size = new System.Drawing.Size(278, 26);
            this.modifyConfigurationStructureToolStripMenuItem.Text = "Edit fields";
            this.modifyConfigurationStructureToolStripMenuItem.Click += new System.EventHandler(this.modifyConfigurationStructureToolStripMenuItem_Click);
            // 
            // stopEditingToolStripMenuItem
            // 
            this.stopEditingToolStripMenuItem.Name = "stopEditingToolStripMenuItem";
            this.stopEditingToolStripMenuItem.Size = new System.Drawing.Size(278, 26);
            this.stopEditingToolStripMenuItem.Text = "Stop editing";
            this.stopEditingToolStripMenuItem.Click += new System.EventHandler(this.stopEditingToolStripMenuItem_Click);
            // 
            // removeSelectedFieldToolStripMenuItem
            // 
            this.removeSelectedFieldToolStripMenuItem.Name = "removeSelectedFieldToolStripMenuItem";
            this.removeSelectedFieldToolStripMenuItem.Size = new System.Drawing.Size(278, 26);
            this.removeSelectedFieldToolStripMenuItem.Text = "Remove selected parameter";
            this.removeSelectedFieldToolStripMenuItem.Click += new System.EventHandler(this.removeSelectedFieldToolStripMenuItem_Click);
            // 
            // addParameterToolStripMenuItem
            // 
            this.addParameterToolStripMenuItem.Name = "addParameterToolStripMenuItem";
            this.addParameterToolStripMenuItem.Size = new System.Drawing.Size(278, 26);
            this.addParameterToolStripMenuItem.Text = "Add parameter";
            this.addParameterToolStripMenuItem.Click += new System.EventHandler(this.addParameterToolStripMenuItem_Click);
            // 
            // createTemplateToolStripMenuItem
            // 
            this.createTemplateToolStripMenuItem.Name = "createTemplateToolStripMenuItem";
            this.createTemplateToolStripMenuItem.Size = new System.Drawing.Size(278, 26);
            this.createTemplateToolStripMenuItem.Text = "Create template";
            this.createTemplateToolStripMenuItem.Click += new System.EventHandler(this.createTemplateToolStripMenuItem_Click);
            // 
            // buttonFromDevice
            // 
            this.buttonFromDevice.Image = ((System.Drawing.Image)(resources.GetObject("buttonFromDevice.Image")));
            this.buttonFromDevice.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonFromDevice.Location = new System.Drawing.Point(863, 212);
            this.buttonFromDevice.Margin = new System.Windows.Forms.Padding(4);
            this.buttonFromDevice.Name = "buttonFromDevice";
            this.buttonFromDevice.Size = new System.Drawing.Size(220, 48);
            this.buttonFromDevice.TabIndex = 6;
            this.buttonFromDevice.Text = "Load from device";
            this.buttonFromDevice.UseVisualStyleBackColor = true;
            this.buttonFromDevice.Click += new System.EventHandler(this.buttonFromDevice_Click);
            // 
            // buttonErase
            // 
            this.buttonErase.Image = ((System.Drawing.Image)(resources.GetObject("buttonErase.Image")));
            this.buttonErase.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonErase.Location = new System.Drawing.Point(863, 272);
            this.buttonErase.Margin = new System.Windows.Forms.Padding(4);
            this.buttonErase.Name = "buttonErase";
            this.buttonErase.Size = new System.Drawing.Size(220, 48);
            this.buttonErase.TabIndex = 7;
            this.buttonErase.Text = "Erase device";
            this.buttonErase.UseVisualStyleBackColor = true;
            this.buttonErase.Click += new System.EventHandler(this.buttonErase_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 548);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(1096, 29);
            this.statusStrip.TabIndex = 9;
            this.statusStrip.Text = "statusStrip1";
            this.statusStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip_ItemClicked);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.AutoSize = false;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(250, 23);
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.AutoSize = false;
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(293, 21);
            // 
            // labelCfgName
            // 
            this.labelCfgName.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelCfgName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelCfgName.Location = new System.Drawing.Point(16, 33);
            this.labelCfgName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCfgName.Name = "labelCfgName";
            this.labelCfgName.Size = new System.Drawing.Size(836, 30);
            this.labelCfgName.TabIndex = 10;
            this.labelCfgName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelIdentifiers
            // 
            this.labelIdentifiers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelIdentifiers.Location = new System.Drawing.Point(16, 64);
            this.labelIdentifiers.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelIdentifiers.Name = "labelIdentifiers";
            this.labelIdentifiers.Size = new System.Drawing.Size(836, 28);
            this.labelIdentifiers.TabIndex = 11;
            this.labelIdentifiers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelIdentifiers.Click += new System.EventHandler(this.labelIdentifiers_Click);
            // 
            // buttonQR
            // 
            this.buttonQR.Location = new System.Drawing.Point(863, 337);
            this.buttonQR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonQR.Name = "buttonQR";
            this.buttonQR.Size = new System.Drawing.Size(220, 48);
            this.buttonQR.TabIndex = 12;
            this.buttonQR.Text = "QR Code";
            this.buttonQR.UseVisualStyleBackColor = true;
            this.buttonQR.Click += new System.EventHandler(this.buttonQRCode_Click);
            // 
            // textBoxData
            // 
            this.textBoxData.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBoxData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxData.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxData.Location = new System.Drawing.Point(494, 114);
            this.textBoxData.Multiline = true;
            this.textBoxData.Name = "textBoxData";
            this.textBoxData.Size = new System.Drawing.Size(345, 414);
            this.textBoxData.TabIndex = 13;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.saveToolStripMenuItem,
            this.exportStructureFiletxtToolStripMenuItem,
            this.exportBLEProfileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(252, 26);
            this.openToolStripMenuItem1.Text = "Open";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exportStructureFiletxtToolStripMenuItem
            // 
            this.exportStructureFiletxtToolStripMenuItem.Name = "exportStructureFiletxtToolStripMenuItem";
            this.exportStructureFiletxtToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.exportStructureFiletxtToolStripMenuItem.Text = "Export structure file (txt)";
            this.exportStructureFiletxtToolStripMenuItem.Click += new System.EventHandler(this.exportStructureFiletxtToolStripMenuItem_Click);
            // 
            // exportBLEProfileToolStripMenuItem
            // 
            this.exportBLEProfileToolStripMenuItem.Name = "exportBLEProfileToolStripMenuItem";
            this.exportBLEProfileToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.exportBLEProfileToolStripMenuItem.Text = "Export BLE profile";
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1096, 577);
            this.Controls.Add(this.textBoxData);
            this.Controls.Add(this.buttonQR);
            this.Controls.Add(this.labelIdentifiers);
            this.Controls.Add(this.labelCfgName);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.buttonErase);
            this.Controls.Add(this.buttonFromDevice);
            this.Controls.Add(this.comboBoxPorts);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.dataGridViewConfig);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormConfig";
            this.Text = "Configuration Tool";
            this.Load += new System.EventHandler(this.FormConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConfig)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewConfig;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonOpen;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.ComboBox comboBoxPorts;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyConfigurationStructureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedFieldToolStripMenuItem;
        private System.Windows.Forms.Button buttonFromDevice;
        private System.Windows.Forms.ToolStripMenuItem stopEditingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createTemplateToolStripMenuItem;
        private System.Windows.Forms.Button buttonErase;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.Label labelCfgName;
        private System.Windows.Forms.Label labelIdentifiers;
        private System.Windows.Forms.Button buttonQR;
        private System.Windows.Forms.TextBox textBoxData;
        private System.Windows.Forms.ToolStripMenuItem addParameterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportStructureFiletxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportBLEProfileToolStripMenuItem;
    }
}

