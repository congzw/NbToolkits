using System;
using System.Windows.Forms;

namespace TemplateEasy
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
            this.cbxJsonFormat.Items.Add("Json.NoAction");
            this.cbxJsonFormat.Items.Add("Json.Format.None");
            this.cbxJsonFormat.Items.Add("Json.Format.Indented");
            this.cbxJsonFormat.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var values = this.textBox1.Text;

            try
            {
                values = TemplateHelper.FormatJson(values, cbxJsonFormat.SelectedIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (cbxQuotes.Checked)
            {
                values = TemplateHelper.ReplaceWithDoubleQuotes(values);
            }

            this.textBox2.Text = values;
        }
    }
}
