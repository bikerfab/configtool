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
    public partial class templateDlg : Form
    {
        string productId;
        string versionId;

        public templateDlg()
        {
            InitializeComponent();
        }

        public byte getProductID()
        {
            return Convert.ToByte(productId);
        }

        public byte getVersionID()
        {
            return Convert.ToByte(versionId);
        }

        private void templateDlg_Load(object sender, EventArgs e)
        {
            buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            productId = textBoxProdId.Text;
            versionId = textBoxVerId.Text;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
