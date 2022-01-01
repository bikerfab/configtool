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
        public BLEServiceExportInfo info;
        public Configuration cfg;
        
        public BLEServiceExportDlg(BLEServiceExportInfo exportInfo)
        {
            info = exportInfo;

            InitializeComponent();

            textBoxServiceName.Text = info.srvName;
            textBoxServiceUUID.Text = info.srvUUID;
            textBoxCharFirstUuid.Text = info.charFirstUUID;
            textBoxBaseName.Text = info.baseName;
            textBoxFolder.Text = info.folder;
        }
        void selectFolder(String title, String startingFolder)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = startingFolder;
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                info.folder = fbd.SelectedPath;
            }
        }

        private void buttonFolder_Click(object sender, EventArgs e)
        {
            selectFolder("Export folder", textBoxFolder.Text);
            textBoxFolder.Text = info.folder;
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
            info.srvName = textBoxServiceName.Text;
            info.srvUUID = textBoxServiceUUID.Text;
            info.charFirstUUID = textBoxCharFirstUuid.Text;
            info.baseName = textBoxBaseName.Text;
            info.folder = textBoxFolder.Text;

            cfg.saveData();

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
