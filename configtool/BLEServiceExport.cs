using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace configtool
{
    public partial class BLEServiceExportDlg : Form
    {
        public String folder;
        public String srvName;
        public String srvUUID;
        public String charFirstUUID;
        public String baseName;
        
        public BLEServiceExportDlg()
        {
            InitializeComponent();
        }
        void selectFolder(String title, String startingFolder)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = startingFolder;
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                folder = fbd.SelectedPath;
            }
        }

        private void buttonFolder_Click(object sender, EventArgs e)
        {
            selectFolder("Export folder", textBoxFolder.Text);
            textBoxFolder.Text = folder;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void BLEServiceExport_Load(object sender, EventArgs e)
        {

        }

        private void labelUuidsFileName_Click(object sender, EventArgs e)
        {

        }

        private void buttonGenerateCode_Click(object sender, EventArgs e)
        {
            srvName = textBoxServiceName.Text;
            srvUUID = textBoxServiceUUID.Text;
            charFirstUUID = textBoxCharFirstUuid.Text;
            baseName = textBoxBaseName.Text;
            folder = textBoxFolder.Text;

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            labelUuidsFileName.Text = textBoxBaseName.Text + "_uuids.h";
            labelServiceFileName.Text = textBoxBaseName.Text + "_service.c";
        }

        private void textBoxServiceName_TextChanged(object sender, EventArgs e)
        {
            textBoxBaseName.Text = textBoxServiceName.Text;
        }
    }
}
