using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;


namespace NbExePackage
{
    public class PackageHelper
    {
        public static readonly string PackageBinFolder = "PackageBinFolder";
        public static readonly string ArchiveName = "ArchiveName";
        public static readonly string ExcludeFoldersOrFiles = "ExcludeFoldersOrFiles";
        public static readonly string[] InitExcludes = {
            ".vshost.exe",
            ".vshost.exe.config",
            ".vshost.exe.manifest",
            ".pdb",
            ".user",
            ".suo",
            ".vssscc",
            ".vspscc",
            "\\bin",
            "\\obj",
            "\\packages"
        };

        public void Create(string packageFolder, string archiveName = "Archive.zip", params string[] addExcludeFoldersOrFiles)
        {
            //find bin folder
            var excludes = InitExcludes.ToList();
            foreach (var addExcludeFoldersOrFile in addExcludeFoldersOrFiles)
            {
                if (!excludes.Contains(addExcludeFoldersOrFile, StringComparer.OrdinalIgnoreCase))
                {
                    excludes.Add(addExcludeFoldersOrFile);
                }
            }

            int filesAdded = CreateArchive(packageFolder, excludes, archiveName);
            Console.WriteLine("共计添加打包文件：{0}个", filesAdded);
            Console.ReadLine();
        }

        public int CreateArchive(string folder, IList<string> exceptions, string archiveName)
        {
            int filesCount = 0;
            string folderFullPath = Path.GetFullPath(folder);
            string archivePath = Path.Combine(folderFullPath, archiveName);
            if (File.Exists(archivePath))
            {
                Console.WriteLine(@"文件'{0}'已经存在，是否覆盖？ 确定输入 'y'", archiveName);
                string read = Console.ReadLine();
                if (read != null && read.ToLower() == "y")
                {
                    File.Delete(archivePath);
                }
                else
                {
                    Console.WriteLine(@"文件'{0}'已经存在，选择跳过", archivePath);
                    return 0;
                }
            }

            var files = Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories).ToList();
            using (ZipArchive archive = ZipFile.Open(archivePath, ZipArchiveMode.Create))
            {
                foreach (string file in files)
                {
                    if (!Excluded(file, exceptions))
                    {
                        try
                        {
                            var addFile = Path.GetFullPath(file);
                            if (addFile != archivePath)
                            {
                                addFile = addFile.Substring(folderFullPath.Length);
                                Console.WriteLine("添加： " + addFile);
                                archive.CreateEntryFromFile(file, addFile);
                                filesCount++;
                            }
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine(@"文件{0}添加出错: 
                            {1} \n 忽略！", file, ex.Message);
                        }
                    }
                }
            }

            if (filesCount == 0)
            {
                if (File.Exists(archivePath))
                {
                    File.Delete(archivePath);
                }
            }

            return filesCount;
        }

        private bool Excluded(string file, IList<string> exceptions)
        {
            List<string> fileNames = exceptions.Where(fileName => !fileName.StartsWith(@"\") && !fileName.StartsWith(@"/")).ToList();
            var name = Path.GetFileName(file);
            if (fileNames.Any(x => name.EndsWith(x, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            List<string> folderNames = exceptions.Where(folder => folder.StartsWith(@"\") || folder.StartsWith(@"/")).ToList();
            if (!exceptions.Contains(Path.GetExtension(file)))
            {
                foreach (string folderException in folderNames)
                {
                    var directoryName = Path.GetDirectoryName(file);
                    if (directoryName != null && directoryName.Contains(folderException))
                    {
                        return true;
                    }
                }
                return false;
            }
            return true;
        }
    }
}