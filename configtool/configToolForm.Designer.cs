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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfig));
            this.dataGridViewConfig = new System.Windows.Forms.DataGridView();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.comboBoxPorts = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyConfigurationStructureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopEditingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSelectedFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonFromDevice = new System.Windows.Forms.Button();
            this.buttonErase = new System.Windows.Forms.Button();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConfig)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewConfig
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewConfig.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewConfig.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewConfig.Location = new System.Drawing.Point(12, 27);
            this.dataGridViewConfig.Name = "dataGridViewConfig";
            this.dataGridViewConfig.Size = new System.Drawing.Size(682, 386);
            this.dataGridViewConfig.TabIndex = 0;
            this.dataGridViewConfig.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewConfig_CellEnter);
            this.dataGridViewConfig.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewConfig_CellLeave);
            this.dataGridViewConfig.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewConfig_CellValueChanged);
            this.dataGridViewConfig.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewConfig_DataError);
            // 
            // buttonSend
            // 
            this.buttonSend.Image = ((System.Drawing.Image)(resources.GetObject("buttonSend.Image")));
            this.buttonSend.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSend.Location = new System.Drawing.Point(708, 124);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(165, 39);
            this.buttonSend.TabIndex = 1;
            this.buttonSend.Text = "Send to device";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSave.Location = new System.Drawing.Point(708, 76);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(165, 39);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpen.Image")));
            this.buttonOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOpen.Location = new System.Drawing.Point(708, 27);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(165, 39);
            this.buttonOpen.TabIndex = 3;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.FormattingEnabled = true;
            this.comboBoxPorts.Location = new System.Drawing.Point(708, 309);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(94, 21);
            this.comboBoxPorts.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(887, 24);
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
            this.createTemplateToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // modifyConfigurationStructureToolStripMenuItem
            // 
            this.modifyConfigurationStructureToolStripMenuItem.Name = "modifyConfigurationStructureToolStripMenuItem";
            this.modifyConfigurationStructureToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.modifyConfigurationStructureToolStripMenuItem.Text = "Edit fields";
            this.modifyConfigurationStructureToolStripMenuItem.Click += new System.EventHandler(this.modifyConfigurationStructureToolStripMenuItem_Click);
            // 
            // stopEditingToolStripMenuItem
            // 
            this.stopEditingToolStripMenuItem.Name = "stopEditingToolStripMenuItem";
            this.stopEditingToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.stopEditingToolStripMenuItem.Text = "Stop editing";
            this.stopEditingToolStripMenuItem.Click += new System.EventHandler(this.stopEditingToolStripMenuItem_Click);
            // 
            // removeSelectedFieldToolStripMenuItem
            // 
            this.removeSelectedFieldToolStripMenuItem.Name = "removeSelectedFieldToolStripMenuItem";
            this.removeSelectedFieldToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.removeSelectedFieldToolStripMenuItem.Text = "Remove selected parameter";
            this.removeSelectedFieldToolStripMenuItem.Click += new System.EventHandler(this.removeSelectedFieldToolStripMenuItem_Click);
            // 
            // createTemplateToolStripMenuItem
            // 
            this.createTemplateToolStripMenuItem.Name = "createTemplateToolStripMenuItem";
            this.createTemplateToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.createTemplateToolStripMenuItem.Text = "Create template";
            this.createTemplateToolStripMenuItem.Click += new System.EventHandler(this.createTemplateToolStripMenuItem_Click);
            // 
            // buttonFromDevice
            // 
            this.buttonFromDevice.Image = ((System.Drawing.Image)(resources.GetObject("buttonFromDevice.Image")));
            this.buttonFromDevice.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonFromDevice.Location = new System.Drawing.Point(708, 174);
            this.buttonFromDevice.Name = "buttonFromDevice";
            this.buttonFromDevice.Size = new System.Drawing.Size(165, 39);
            this.buttonFromDevice.TabIndex = 6;
            this.buttonFromDevice.Text = "Load from device";
            this.buttonFromDevice.UseVisualStyleBackColor = true;
            this.buttonFromDevice.Click += new System.EventHandler(this.buttonFromDevice_Click);
            // 
            // buttonErase
            // 
            this.buttonErase.Image = ((System.Drawing.Image)(resources.GetObject("buttonErase.Image")));
            this.buttonErase.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonErase.Location = new System.Drawing.Point(708, 223);
            this.buttonErase.Name = "buttonErase";
            this.buttonErase.Size = new System.Drawing.Size(165, 39);
            this.buttonErase.TabIndex = 7;
            this.buttonErase.Text = "Erase device";
            this.buttonErase.UseVisualStyleBackColor = true;
            this.buttonErase.Click += new System.EventHandler(this.buttonErase_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 421);
            this.Controls.Add(this.buttonErase);
            this.Controls.Add(this.buttonFromDevice);
            this.Controls.Add(this.comboBoxPorts);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.dataGridViewConfig);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormConfig";
            this.Text = "Configuration Tool";
            this.Load += new System.EventHandler(this.FormConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConfig)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
    }
}

