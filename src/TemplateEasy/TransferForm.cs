using System;
using System.Windows.Forms;

namespace TemplateEasy
{
    public partial class TransferForm : Form
    {
        public TransferForm()
        {
            InitializeComponent();
            this.splitContainer1.FixedPanel = FixedPanel.Panel1;
            this.cbxType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbxType.Items.Add(CtorToStaticFactory.Type);
            this.cbxType.SelectedIndex = 0;
        }

        private void TransferForm_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.cbxType.SelectedItem.ToString() == CtorToStaticFactory.Type)
            {
                this.textBox2.Text = CtorToStaticFactory.Transfer(this.textBox1.Text);
            }
        }
    }
}
