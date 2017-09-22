using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NbCopyToFolders.Common;

namespace NbCopyToFolders
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var copyToFolders = MyConfigHelper.GetAppSettingValue(ConstHelper.CopyToFolders, @"D:\WS_NbCloud\NbCloud\Master\lib\Shared, D:\WS_NbCloud\NbCloud\Master\src\ZQNB.Web\bin");
            var copyToFoldersArray = MyStringHelper.SplitString(copyToFolders).Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            var copyToFoldersFix = string.Join(",", copyToFoldersArray);
            var copyFromFolder = MyConfigHelper.GetAppSettingValue(ConstHelper.CopyFromFolder, @".\CopyFromFolder");
            this.txtCopyToFolders.Text = copyToFoldersFix;
            this.txtCopyFromFolder.Text = copyFromFolder;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                var copyToFolders = this.txtCopyToFolders.Text.Trim();
                var copyToFoldersArray = MyStringHelper.SplitString(copyToFolders).Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                var copyFromFolder = this.txtCopyFromFolder.Text.Trim();

                if (!TryCheckFolder(copyFromFolder))
                {
                    return;
                }

                foreach (var copyToFolder in copyToFoldersArray)
                {
                    if (!TryCheckFolder(copyToFolder))
                    {
                        return;
                    }
                }


                foreach (var copyToFolder in copyToFoldersArray)
                {
                    MyIOHelper.CopyFolder(copyFromFolder, copyToFolder);
                }
                MessageBox.Show(@"Copy Complete!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool TryCheckFolder(string folder)
        {
            if (!Directory.Exists(folder))
            {
                MessageBox.Show(@"路径不存在:" + folder);
                return false;
            }
            return true;
        }
    }

}