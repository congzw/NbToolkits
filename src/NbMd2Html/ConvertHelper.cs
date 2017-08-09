using System;
using System.IO;

namespace NbMd2Html
{
    public class ConvertHelper
    {
        public void ProcessFiles(string currentFolder, bool appendOnly)
        {
            var mdFiles = Directory.GetFiles(currentFolder, "*.*", SearchOption.TopDirectoryOnly);
            Console.WriteLine("total find: {0}", mdFiles.Length);
            Console.WriteLine("=================");

            foreach (var mdFile in mdFiles)
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(mdFile);
                var fileName = appendOnly ? mdFile : fileNameWithoutExtension;
                var htmlFile = fileName + ".html";
                using (var reader = new StreamReader(mdFile))
                using (var writer = new StreamWriter(htmlFile))
                {
                    CommonMark.CommonMarkConverter.Convert(reader, writer);
                }
                Console.WriteLine("convert complete: {0}", htmlFile);
            }
        }

    }
}