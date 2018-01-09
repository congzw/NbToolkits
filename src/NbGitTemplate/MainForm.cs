using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using NbGitTemplate.Properties;

namespace NbGitTemplate
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.txtSln.Text = "NbCloud";
        }

        private static string templateName = "SlnTemplate";

        private async void btnSwitch_Click(object sender, EventArgs e)
        {
            await CreateSln();
        }
        
        private async Task CreateSln()
        {
            var slnName = this.txtSln.Text.Trim();
            if (string.IsNullOrWhiteSpace(slnName))
            {
                PrintMessage("解决方案名称不能为空");
                return;
            }
            PrintMessage("开始创建...");
            this.btnSwitch.Enabled = false;
            this.txtSln.Enabled = false;
            await Creating(slnName, this.cbxNestMaster.Checked);
            this.btnSwitch.Enabled = true;
            this.txtSln.Enabled = true;
            await Task.FromResult(0);
        }

        private async Task Creating(string slnName, bool nestedMaster)
        {
            //unzip file
            var packageHelper = new PackageHelper();
            var currentFolder = Path.GetFullPath(".\\");
            
            if (nestedMaster)
            {
                //currentFolder = currentFolder + slnName + "\\master\\" + slnName + "\\";

                currentFolder = currentFolder + slnName;
                packageHelper.DeleteFolder(currentFolder);
                Directory.CreateDirectory(currentFolder);

                currentFolder = currentFolder + "\\master";
                packageHelper.DeleteFolder(currentFolder);
                Directory.CreateDirectory(currentFolder);
                currentFolder = currentFolder.TrimEnd('\\') + "\\";
            }


            var extractFolder = currentFolder + templateName;
            if (Directory.Exists(extractFolder))
            {
                packageHelper.DeleteFolder(extractFolder);
            }
            var targetFolder = currentFolder + slnName;
            if (Directory.Exists(targetFolder))
            {
                packageHelper.DeleteFolder(targetFolder);
            }

            //packageHelper.UnPackage(templateFilePath, currentFolder);

            var memoryStream = new MemoryStream(Resources.SlnTemplate);
            packageHelper.UnPackage(memoryStream, currentFolder);
            
            //change NbTemplate.sln name
            var slnFileNameTemplate = currentFolder + @"SlnTemplate\src\" + templateName + ".sln";
            var slnFileName = currentFolder + @"SlnTemplate\src\" + slnName + ".sln";
            PrintMessage(string.Format("Change sln: {0} => {1}", slnFileNameTemplate, slnFileName));
            packageHelper.TryChangeFileName(slnFileNameTemplate, slnFileName);

            //change folder name
            PrintMessage(string.Format("Change Folder: {0} => {1}", currentFolder + templateName, currentFolder+slnName));
            packageHelper.TryChangeFolderName(currentFolder + templateName, currentFolder + slnName);


            PrintMessage(slnName + " => 创建完成");
        }

        private void PrintMessage(string message)
        {
            this.txtMessage.AppendText(string.Format("{0} => {1}\r\n", DateTime.Now, message));
        }
    }
}
