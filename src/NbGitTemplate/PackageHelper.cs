using System;
using System.IO;
using System.IO.Compression;

namespace NbGitTemplate
{
    public class PackageHelper
    {
        public void UnPackage(string zipPath, string extractFolder)
        {
            ZipFile.ExtractToDirectory(zipPath, extractFolder);
        }

        public void UnPackage(Stream zipStream, string extractFolder)
        {
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Read))
            {
                archive.ExtractToDirectory(extractFolder);
            } 
        }

        /// <summary>
        /// 修改文件夹的名称
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="destName"></param>
        /// <returns></returns>
        public bool TryChangeFolderName(string sourceName, string destName)
        {
            if (!Directory.Exists(sourceName))
            {
                return false;
            }
            try
            {
                Directory.Move(sourceName, destName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 修改文件名
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        /// <returns></returns>
        public bool TryChangeFileName(string sourceFileName, string destFileName)
        {
            if (!File.Exists(sourceFileName))
            {
                return false;
            }
            try
            {
                File.Move(sourceFileName, destFileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>     
        /// C# 删除文件夹        
        /// </summary>     
        /// <param name="dir">删除的文件夹，全路径格式</param>     
        public void DeleteFolder(string dir)
        {
            if (!Directory.Exists(dir))
            {
                return;
            }
            // 循环文件夹里面的内容     
            foreach (string f in Directory.GetFileSystemEntries(dir))
            {
                // 如果是文件存在     
                if (File.Exists(f))
                {
                    FileInfo fi = new FileInfo(f);
                    if (fi.Attributes.ToString().IndexOf("Readonly", StringComparison.OrdinalIgnoreCase) != 1)
                    {
                        fi.Attributes = FileAttributes.Normal;
                    }
                    // 直接删除其中的文件     
                    File.Delete(f);
                }
                else
                {
                    // 如果是文件夹存在     
                    // 递归删除子文件夹     
                    DeleteFolder(f);
                }
            }
            // 删除已空文件夹     
            Directory.Delete(dir);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="file"></param>
        public void DeleteFile(string file)
        {
            // 如果是文件存在     
            if (File.Exists(file))
            {
                FileInfo fi = new FileInfo(file);
                if (fi.Attributes.ToString().IndexOf("Readonly", StringComparison.OrdinalIgnoreCase) != 1)
                {
                    fi.Attributes = FileAttributes.Normal;
                }
                // 直接删除其中的文件     
                File.Delete(file);
            }
        }

    }
}