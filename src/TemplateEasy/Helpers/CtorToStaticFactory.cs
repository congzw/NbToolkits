using System;
using System.Linq;
using System.Text;

namespace TemplateEasy
{
    public class CtorToStaticFactory
    {
        public static string Type = "构造函数 -> 静态工厂";
        public static string Transfer(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "fail";
            }
            var inputFix = input.Trim();
            var appendPrefix = "\t";

            var lines = inputFix.Trim().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var stringBuilder = new StringBuilder();
            var lineNo = 0;
            var className = "";
            var propStartLine = 3;
            var propEndLine = lines.Count -1;
            foreach (var line in lines)
            {
                lineNo++;
                if (lineNo == 1)
                {
                    var lineSplits = line.Replace("public", "").Split(new[] { '(' }, StringSplitOptions.RemoveEmptyEntries);
                    className = lineSplits.FirstOrDefault().Trim();
                    var lastAppend = lineSplits.LastOrDefault();
                    if (string.IsNullOrWhiteSpace(className))
                    {
                        return "fail";
                    }

                    var newLine = "public static {className} Create({lastAppend}"
                        .Replace("{className}", className)
                        .Replace("{lastAppend}", lastAppend);

                    stringBuilder.AppendLine(newLine);
                }
                else if (lineNo == propStartLine)
                {
                    var newLine2 = "var item = new {className}();"
                        .Replace("{className}", className);
                    stringBuilder.AppendLine(appendPrefix + newLine2);
                }
                else
                {
                    var lineSplits = line.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    if (lineSplits.Length == 2)
                    {
                        var propName = lineSplits.FirstOrDefault().Trim();
                        var propValue = lineSplits.LastOrDefault();

                        var newLine = "item.{propName} = {propValue}"
                            .Replace("{propName}", propName)
                            .Replace("{propValue}", propValue);

                        stringBuilder.AppendLine(appendPrefix + newLine);

                        if (lineNo == propEndLine)
                        {
                            stringBuilder.AppendLine(appendPrefix + "return item;");
                        }
                    }
                    else
                    {
                        stringBuilder.AppendLine(line.TrimStart());
                    }
                }
            }

            return stringBuilder.ToString();
        }
    }
}
