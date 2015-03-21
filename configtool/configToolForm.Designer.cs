﻿namespace configtool
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConfig)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewConfig
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewConfig.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewConfig.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewConfig.Location = new System.Drawing.Point(0, 27);
            this.dataGridViewConfig.Name = "dataGridViewConfig";
            this.dataGridViewConfig.Size = new System.Drawing.Size(435, 386);
            this.dataGridViewConfig.TabIndex = 0;
            this.dataGridViewConfig.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewConfig_CellValueChanged);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(440, 144);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(143, 44);
            this.buttonSend.TabIndex = 1;
            this.buttonSend.Text = "Send to device";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(440, 96);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(143, 42);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(440, 51);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(143, 39);
            this.buttonOpen.TabIndex = 3;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.FormattingEnabled = true;
            this.comboBoxPorts.Location = new System.Drawing.Point(440, 309);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(97, 21);
            this.comboBoxPorts.TabIndex = 4;
            this.comboBoxPorts.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(596, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.buttonFromDevice.Location = new System.Drawing.Point(440, 195);
            this.buttonFromDevice.Name = "buttonFromDevice";
            this.buttonFromDevice.Size = new System.Drawing.Size(143, 45);
            this.buttonFromDevice.TabIndex = 6;
            this.buttonFromDevice.Text = "Load from device";
            this.buttonFromDevice.UseVisualStyleBackColor = true;
            this.buttonFromDevice.Click += new System.EventHandler(this.buttonFromDevice_Click);
            // 
            // buttonErase
            // 
            this.buttonErase.Location = new System.Drawing.Point(442, 247);
            this.buttonErase.Name = "buttonErase";
            this.buttonErase.Size = new System.Drawing.Size(141, 41);
            this.buttonErase.TabIndex = 7;
            this.buttonErase.Text = "Erase device";
            this.buttonErase.UseVisualStyleBackColor = true;
            this.buttonErase.Click += new System.EventHandler(this.buttonErase_Click);
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 475);
            this.Controls.Add(this.buttonErase);
            this.Controls.Add(this.buttonFromDevice);
            this.Controls.Add(this.comboBoxPorts);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.dataGridViewConfig);
            this.Controls.Add(this.menuStrip1);
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

    }
}

