using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NbAreaMaker._Common;

namespace NbAreaMaker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private const string templateName = "Xxx";
        private string dirPath;
        private void MainForm_Load(object sender, EventArgs e)
        {
            bool isOk = CheckXxxDir();
            if (!isOk)
            {
                MessageBox.Show("没有找到模板项目");
                this.Close();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string areaName = this.txtAreaName.Text.Trim();
            if (string.IsNullOrWhiteSpace(areaName))
            {
                MessageBox.Show("模块名称不能为空");
                return;
            }
            if (!IsAllEnglish(areaName))
            {
                MessageBox.Show("模块名称必须全部是英文字母");
                return;
            }
            
            CreateNewArea(areaName);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateNewArea(string areaName)
        {
            string outPutDir = dirPath.Replace(templateName, areaName);
            if (Directory.Exists(outPutDir))
            {
                MessageBox.Show(string.Format("要创建的路径{0}\r\n处已经有一个同名模块，请确认！", outPutDir));
                return; 
            }
            try
            {
                //生成思路：
                //1 将Xxx文件夹Copy一份，重命名为新的Area名称，以FTC为例子
                //2 检测并保证有几个空目录
                //    Content
                //    Controllers
                //3 修改如下：
                //    替换Properties\AssemblyInfo.cs
                //        替换Web.Areas.Xxx为新的Web.Areas.FTC
                //        替换{新的GUID}为新的guid
                //    替换XxxAreaRegistration.cs
                //        替换Xxx为FTC
                //    替换Yyy.Web.Areas.Xxx.csproj
                //        替换{新的GUID}为NewGUID
                //        替换Xxx为FTC
                //        替换Yyy为ZQNB
                //    改名
                //        修改文件名XxxAreaRegistration.cs为TTCAreaRegistration.cs
                //        修改文件名Yyy.Web.Areas.Xxx.csproj为ZQNB.Web.Areas.FTC.csproj

                //4 如果启用主题，替换 Views\web.config namespace <pages pageBaseType="ZQNB.Common.Web.Mvc.WebViewPage">

                const string guidPlaceHolder = "新的GUID";
                string message = "";

                MyIOHelper.CopyFolder(dirPath, outPutDir);
                
                string contentFolder = string.Format("{0}\\{1}", outPutDir, "Content");
                MyIOHelper.TryCreateFolder(contentFolder);
                string controllersFolder = string.Format("{0}\\{1}", outPutDir, "Controllers");
                MyIOHelper.TryCreateFolder(controllersFolder);
                //string viewModelsFolder = string.Format("{0}\\{1}", outPutDir, "ViewModels");
                //MyIOHelper.TryCreateFolder(viewModelsFolder);


                var prefix = MyConfigHelper.Resolve().GetAppSettingValue("AreaPrex", "ZQNB");
                //Properties\AssemblyInfo.cs
                string assemblyInfoFilePath = string.Format("{0}\\Properties\\AssemblyInfo.cs", outPutDir);
                string assemblyInfoFileContent = MyIOHelper.ReadAllText(assemblyInfoFilePath);
                string newAssemblyInfoFileContent = assemblyInfoFileContent
                    .Replace(templateName, areaName)
                    .Replace(guidPlaceHolder, Guid.NewGuid().ToString("D").ToUpper())
                    .Replace("Yyy", prefix);
                if (!MyIOHelper.TrySaveFileWithAddAccessRule(assemblyInfoFilePath, newAssemblyInfoFileContent, null,
                    out message))
                {
                    MessageBox.Show(message);
                    return;
                }

                //XxxAreaRegistration.cs 内容
                string xxxAreaRegistrationFilePath = string.Format("{0}\\XxxAreaRegistration.cs", outPutDir);
                string xxxAreaRegistrationFileContent = MyIOHelper.ReadAllText(xxxAreaRegistrationFilePath);
                string newXxxAreaRegistrationFileContent = xxxAreaRegistrationFileContent
                    .Replace(templateName, areaName)
                    .Replace("Yyy", prefix);

                if (!MyIOHelper.TrySaveFileWithAddAccessRule(xxxAreaRegistrationFilePath, newXxxAreaRegistrationFileContent, null,out message))
                {
                    MessageBox.Show(message);
                    return;
                }
                //XxxAreaRegistration.cs 改名
                string newXxxAreaRegistrationFilePath = xxxAreaRegistrationFilePath.Replace(templateName, areaName);
                if (!MyIOHelper.TryChangeFileName(xxxAreaRegistrationFilePath, newXxxAreaRegistrationFilePath))
                {
                    MessageBox.Show("");
                    return;
                }

                //Yyy.Web.Areas.Xxx.csproj 内容
                string csprojFilePath = string.Format("{0}\\Yyy.Web.Areas.Xxx.csproj", outPutDir);
                string csprojFilePathContent = MyIOHelper.ReadAllText(csprojFilePath);

                string newCsprojFilePathContent = csprojFilePathContent
                    .Replace(templateName, areaName)
                    .Replace(guidPlaceHolder, Guid.NewGuid().ToString("D").ToUpper())
                    .Replace("Yyy", prefix);

                if (!MyIOHelper.TrySaveFileWithAddAccessRule(csprojFilePath, newCsprojFilePathContent, null, out message))
                {
                    MessageBox.Show(message);
                    return;
                }

                //Yyy.Web.Areas.Xxx.csproj 改名
                string newCsprojFilePath = csprojFilePath.Replace(templateName, areaName).Replace("Yyy", prefix);
                if (!MyIOHelper.TryChangeFileName(csprojFilePath, newCsprojFilePath))
                {
                    MessageBox.Show(@"改名失败:" + newCsprojFilePath);
                    return;
                }

                //Views\web.config
                var themeEnabled = MyConfigHelper.Resolve().GetAppSettingValueAsBool("ThemeEnabled", true);
                if (themeEnabled)
                {
                    string viewWebConfigFilePath = string.Format("{0}\\Views\\web.config", outPutDir);
                    string viewWebConfigFilePathContent = MyIOHelper.ReadAllText(viewWebConfigFilePath);
                    
                    var ns = "Yyy.Common.Web.Mvc.WebViewPage".Replace("Yyy", prefix);
                    string newViewWebConfigFilePathContent = viewWebConfigFilePathContent
                        .Replace("System.Web.Mvc.WebViewPage", ns);
                    if (!MyIOHelper.TrySaveFileWithAddAccessRule(viewWebConfigFilePath, newViewWebConfigFilePathContent, null, out message))
                    {
                        MessageBox.Show(message);
                        return;
                    }
                }

                MessageBox.Show("创建成功！");
                if (checkOpenDir.Checked)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(outPutDir);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private bool IsAllEnglish(string input)
        {
            string pattern = @"^[A-Za-z]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        private bool CheckXxxDir()
        {
            dirPath = string.Format("{0}\\{1}", Application.StartupPath, templateName);
            if (!Directory.Exists(dirPath))
            {
                return false;
            }

            return true;
        }
    }
}
