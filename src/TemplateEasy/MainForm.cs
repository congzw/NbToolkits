using System;
using System.Windows.Forms;

namespace TemplateEasy
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var values = this.textBox1.Text;
            var result = TemplateHelper.ReplaceWithDoubleQuotes(values);
            this.textBox2.Text = result;
        }
    }
}
