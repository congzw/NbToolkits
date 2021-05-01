using System.Linq;

namespace SwaggerGenerator.Libs
{
    public static class StringExt
    {
        public static string FirstLower(this string value)
        {
            var first = value.First();
            var lower = first.ToString().ToLower();
            return lower + value.TrimStart(first);
        }
    }
}
