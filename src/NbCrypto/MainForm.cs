using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            this.cbxType.SelectedIndex = 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

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
            
            var encrypt = this.cbxEncrypt.Checked;
            var selectedItem = this.cbxType.SelectedItem;
            var theType = selectedItem.ToString();
            if (theType == "Text")
            {
                var raw = this.txtValue.Text;
                var result = process(password, raw);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    this.txtValue.Text = result;
                    MessageBox.Show("Complete!");
                }
                return;
            }
            
            var filePath = encrypt ? "raw.txt" : "encrypt.txt";

            if (!File.Exists(filePath))
            {
                MessageBox.Show("File should exist: " + filePath);
                return;
            }

            var content = File.ReadAllText(filePath);
            var fileResult = process(password, content);
            if (!string.IsNullOrWhiteSpace(fileResult))
            {
                var saveFilePath = !encrypt ? "raw.txt" : "encrypt.txt";
                File.WriteAllText(saveFilePath, fileResult);
                MessageBox.Show("Complete!");
            }
        }


        private string process(string password, string raw)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password should not null!");
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(raw))
            {
                MessageBox.Show("Text should not null!");
                return string.Empty;
            }

            try
            {
                var encrypt = this.cbxEncrypt.Checked;
                var result =  encrypt ? AESHelper.EncryptToBase64(raw, password) : AESHelper.DecryptFromBase64(raw, password);
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid operation");
                return string.Empty;
            }
        }

    }
}
