using System;

namespace NbMd2Html
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentFolder = ".\\";
            var argsHelper = new ArgsHelper();
            var isAppendOnly = argsHelper.IsAppendOnly(args);
            var convertHelper = new ConvertHelper();
            convertHelper.ProcessFiles(currentFolder, isAppendOnly);
            WaitForExit();
        }

        private static void WaitForExit()
        {
            Console.WriteLine("process any key exit...");
            Console.Read();
        }
    }
}
