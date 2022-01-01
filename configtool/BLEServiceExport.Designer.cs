namespace configtool
{
    partial class BLEServiceExportDlg
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
            System.Windows.Forms.Button buttonFolder;
            this.textBoxServiceName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxServiceUUID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCharFirstUuid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxBaseName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.labelFolder = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelUuidsFileName = new System.Windows.Forms.Label();
            this.labelServiceFileName = new System.Windows.Forms.Label();
            this.buttonGenerateCode = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            buttonFolder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonFolder
            // 
            buttonFolder.Location = new System.Drawing.Point(75, 248);
            buttonFolder.Name = "buttonFolder";
            buttonFolder.Size = new System.Drawing.Size(29, 23);
            buttonFolder.TabIndex = 9;
            buttonFolder.Text = "...";
            buttonFolder.UseVisualStyleBackColor = true;
            buttonFolder.Click += new System.EventHandler(this.buttonFolder_Click);
            // 
            // textBoxServiceName
            // 
            this.textBoxServiceName.Location = new System.Drawing.Point(192, 30);
            this.textBoxServiceName.Name = "textBoxServiceName";
            this.textBoxServiceName.Size = new System.Drawing.Size(171, 22);
            this.textBoxServiceName.TabIndex = 0;
            this.textBoxServiceName.TextChanged += new System.EventHandler(this.textBoxServiceName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Service name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Service UUID";
            // 
            // textBoxServiceUUID
            // 
            this.textBoxServiceUUID.Location = new System.Drawing.Point(192, 60);
            this.textBoxServiceUUID.Name = "textBoxServiceUUID";
            this.textBoxServiceUUID.Size = new System.Drawing.Size(170, 22);
            this.textBoxServiceUUID.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Characteristics first UUID";
            // 
            // textBoxCharFirstUuid
            // 
            this.textBoxCharFirstUuid.Location = new System.Drawing.Point(192, 95);
            this.textBoxCharFirstUuid.Name = "textBoxCharFirstUuid";
            this.textBoxCharFirstUuid.Size = new System.Drawing.Size(100, 22);
            this.textBoxCharFirstUuid.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Filename base";
            // 
            // textBoxBaseName
            // 
            this.textBoxBaseName.Location = new System.Drawing.Point(192, 128);
            this.textBoxBaseName.Name = "textBoxBaseName";
            this.textBoxBaseName.Size = new System.Drawing.Size(165, 22);
            this.textBoxBaseName.TabIndex = 7;
            this.textBoxBaseName.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 248);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Folder";
            // 
            // labelFolder
            // 
            this.labelFolder.AutoSize = true;
            this.labelFolder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelFolder.Location = new System.Drawing.Point(192, 246);
            this.labelFolder.MinimumSize = new System.Drawing.Size(400, 15);
            this.labelFolder.Name = "labelFolder";
            this.labelFolder.Size = new System.Drawing.Size(400, 19);
            this.labelFolder.TabIndex = 10;
            this.labelFolder.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 169);
            this.label7.MinimumSize = new System.Drawing.Size(100, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "Generated files:";
            // 
            // labelUuidsFileName
            // 
            this.labelUuidsFileName.AutoSize = true;
            this.labelUuidsFileName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelUuidsFileName.Location = new System.Drawing.Point(192, 169);
            this.labelUuidsFileName.MinimumSize = new System.Drawing.Size(200, 0);
            this.labelUuidsFileName.Name = "labelUuidsFileName";
            this.labelUuidsFileName.Size = new System.Drawing.Size(200, 19);
            this.labelUuidsFileName.TabIndex = 12;
            this.labelUuidsFileName.Click += new System.EventHandler(this.labelUuidsFileName_Click);
            // 
            // labelServiceFileName
            // 
            this.labelServiceFileName.AutoSize = true;
            this.labelServiceFileName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelServiceFileName.Location = new System.Drawing.Point(192, 198);
            this.labelServiceFileName.MinimumSize = new System.Drawing.Size(200, 0);
            this.labelServiceFileName.Name = "labelServiceFileName";
            this.labelServiceFileName.Size = new System.Drawing.Size(200, 19);
            this.labelServiceFileName.TabIndex = 12;
            this.labelServiceFileName.Click += new System.EventHandler(this.labelUuidsFileName_Click);
            // 
            // buttonGenerateCode
            // 
            this.buttonGenerateCode.Location = new System.Drawing.Point(192, 306);
            this.buttonGenerateCode.Name = "buttonGenerateCode";
            this.buttonGenerateCode.Size = new System.Drawing.Size(163, 44);
            this.buttonGenerateCode.TabIndex = 13;
            this.buttonGenerateCode.Text = "Generate code";
            this.buttonGenerateCode.UseVisualStyleBackColor = true;
            this.buttonGenerateCode.Click += new System.EventHandler(this.buttonGenerateCode_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(361, 306);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 44);
            this.button1.TabIndex = 14;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BLEServiceExportDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 367);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonGenerateCode);
            this.Controls.Add(this.labelServiceFileName);
            this.Controls.Add(this.labelUuidsFileName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.labelFolder);
            this.Controls.Add(buttonFolder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxBaseName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxCharFirstUuid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxServiceUUID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxServiceName);
            this.Name = "BLEServiceExportDlg";
            this.Text = "BLEServiceExport";
            this.Load += new System.EventHandler(this.BLEServiceExport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxServiceName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxServiceUUID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxCharFirstUuid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxBaseName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelFolder;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelUuidsFileName;
        private System.Windows.Forms.Label labelServiceFileName;
        private System.Windows.Forms.Button buttonGenerateCode;
        private System.Windows.Forms.Button button1;
    }
}