using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using SwaggerGenerator.Libs;

namespace SwaggerGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var jsonFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApiDoc/demo.json");
            var content = File.ReadAllText(jsonFile);
            var swaggerApiInfo = SwaggerApiDoc.ParseJson(content);
            this.txtValue.Text = JsonConvert.SerializeObject(swaggerApiInfo, Formatting.Indented);
        }
    }
}
