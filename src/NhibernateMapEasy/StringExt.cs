using System;

namespace NhibernateMapEasy
{
    public static class StringExt
    {
        public static bool NbContains(this string source, string toCheck, StringComparison comp = StringComparison.OrdinalIgnoreCase)
        {
            if (source == null)
            {
                return false;
            }
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
