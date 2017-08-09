using System.Configuration;
using System.Linq;

namespace NbExePackage
{
    class Program
    {
        static void Main(string[] args)
        {
            string packageBinFolder = ".\\";
            string archiveName = "Archive.zip";
            string[] excludes = new string[3] { "ExePackage.exe", "ExePackage.exe.config", "\\[Area]_[Version]" };
            
            //try fix by config
            FixByConfig(ref packageBinFolder, ref archiveName, ref excludes);

            //try fix by args
            PackageBinFolder(args, ref packageBinFolder, ref archiveName, ref excludes);

            var packageHelper = new PackageHelper();
            packageHelper.Create(packageBinFolder, archiveName, excludes);
        }

        private static string PackageBinFolder(string[] args, ref string packageBinFolder, ref string archiveName, ref string[] excludes)
        {
            if (args.Length > 0)
            {
                packageBinFolder = args[0];
            }

            if (args.Length > 1)
            {
                archiveName = args[1];
            }

            if (args.Length > 2)
            {
                excludes = new string[args.Length - 2];
                args.CopyTo(excludes, 2);
            }

            return packageBinFolder;
        }

        private static void FixByConfig(ref string packageBinFolder, ref string archiveName, ref string[] excludes)
        {
            var appSettingForPackageBinFolder = ConfigurationManager.AppSettings[PackageHelper.PackageBinFolder];
            if (!string.IsNullOrWhiteSpace(appSettingForPackageBinFolder))
            {
                packageBinFolder = appSettingForPackageBinFolder;
            }

            var appSettingForArchiveName = ConfigurationManager.AppSettings[PackageHelper.ArchiveName];
            if (!string.IsNullOrWhiteSpace(appSettingForArchiveName))
            {
                archiveName = appSettingForArchiveName;
            }

            var appSettingForExcludeFoldersOrFiles = ConfigurationManager.AppSettings[PackageHelper.ExcludeFoldersOrFiles];
            if (!string.IsNullOrWhiteSpace(appSettingForExcludeFoldersOrFiles))
            {
                var names = appSettingForExcludeFoldersOrFiles.Split(',','，',';','；').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                if (names.Length > 0)
                {
                    excludes = new string[names.Length];
                    names.CopyTo(excludes, 0);
                }
            }
        }
    }
}
