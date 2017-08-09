using System;

namespace NbMd2Html
{
    public class ArgsHelper
    {
        public bool IsAppendOnly(string[] args)
        {
            var appendOnly = true;
            if (args.Length > 0)
            {
                var append = args[0];
                if (append.StartsWith("append", StringComparison.OrdinalIgnoreCase))
                {
                    var splits = append.Split(new[] { ':', '-', ':', '/' });
                    if (splits.Length == 1)
                    {
                        return true;
                    }
                    bool.TryParse(splits[1], out appendOnly);
                }
            }

            return appendOnly;
        }
    }
}