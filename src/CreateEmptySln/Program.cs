using System;
using System.IO;

namespace CreateEmptySln
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var templateDir = @".\template\";
            var appDir = @".\app\";
            if (Directory.Exists(appDir))
            {
                Console.WriteLine($"delete dir: {appDir}");
                Directory.Delete(appDir, true);
            }
            CopyDirectory(templateDir, appDir, true);

            var slnFilePath = Path.Combine(appDir, "src", "app", "app.sln");
            ReplaceSlnFile(slnFilePath);

            Console.WriteLine("####################");
            Console.WriteLine("Completed!, Press any key to exit!");
            Console.Read();
        }

        static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            if (!Directory.Exists(destinationDir))
            {
                Directory.CreateDirectory(destinationDir);
            }

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }

        static void ReplaceSlnFile(string slnFilePath)
        {
            if (File.Exists(slnFilePath))
            {
                var content = File.ReadAllText(slnFilePath).Replace("{GUID}", Guid.NewGuid().ToString("D").ToUpper());
                File.WriteAllText(slnFilePath, content);
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine($"file not found: {slnFilePath}");
            }
        }
    }
}