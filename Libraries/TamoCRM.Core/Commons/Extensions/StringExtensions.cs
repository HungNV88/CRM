using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TamoCRM.Core.Commons.Extensions
{
    public static class StringExtensions
    {
        public static string SubStringExtend(this string source, int length)
        {
            if (source.IsStringNullOrEmpty()) return string.Empty;
            if (source.Length >= length) return source.Substring(0, length) + "...";
            return source;
        }

        public static string GetElement(this string source, string pattern)
        {
            if (source.IsStringNullOrEmpty() || pattern.IsStringNullOrEmpty())
                return string.Empty;

            var mt = (new Regex(pattern)).Match(source);
            var result = mt.Success
                                ? (pattern.Contains("(?<text>") ? mt.Groups["text"].Value : mt.Value)
                                : string.Empty;
            return result;
        }

        public static List<string> GetElements(this string source, string pattern)
        {
            if (source.IsStringNullOrEmpty() || pattern.IsStringNullOrEmpty())
                return null;

            var mtCol = (new Regex(pattern)).Matches(source);
            var result = mtCol.Count > 0
                              ? (mtCol.Cast<Match>().Select(mt => pattern.Contains("(?<text>") ? mt.Groups["text"].Value : mt.Value)).ToList()
                              : null;
            return result;
        }
    }
}

