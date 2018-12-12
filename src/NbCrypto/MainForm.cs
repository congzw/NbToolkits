using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NbCrypto.Helpers;

namespace NbCrypto
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.cbxEncrypt.Checked = true;
        }

        private void cbxEncrypt_CheckedChanged(object sender, EventArgs e)
        {
            this.btnOk.Text = this.cbxEncrypt.Checked ? "Encrypt" : "Decrypt";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var password = txtPassword.Text.Trim();
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password should not null!");
                return;
            }

            var raw = this.txtValue.Text;
            if (string.IsNullOrWhiteSpace(raw))
            {
                MessageBox.Show("Text should not null!");
                return;
            }

            try
            {
                var encrypt = this.cbxEncrypt.Checked;
                if (encrypt)
                {
                    var value = AESHelper.EncryptToBase64(raw, password);
                    this.txtValue.Text = value;
                }
                else
                {
                    var value = AESHelper.DecryptFromBase64(raw, password);
                    this.txtValue.Text = value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid operation");
            }
        }
    }
}
