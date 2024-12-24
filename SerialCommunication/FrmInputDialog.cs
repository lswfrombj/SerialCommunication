using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tt
{
    public partial class FrmInputDialog : Form
    {
        public FrmInputDialog()
        {
            InitializeComponent();
        }

        public delegate void TextEventHandler(string strText);

        public TextEventHandler TextHandler;

        private void btnok_Click(object sender, EventArgs e)
        {
            if (null != TextHandler)
            {
                TextHandler.Invoke(txtbeginnum.Text);
                DialogResult = DialogResult.OK;
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            TextHandler.Invoke(txtbeginnum.Text);
            DialogResult = DialogResult.Cancel;
        }

        private void txtString_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Keys.Enter == (Keys)e.KeyChar)
            {
                if (null != TextHandler)
                {
                    TextHandler.Invoke(txtbeginnum.Text);
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
