using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TamoCRM.Core.Commons.Extensions
{
    public static class ListExtensions
    {
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }

        public static List<T> Clone<T>(this List<T> list) where T : ICloneable
        {
            return list.IsNullOrEmpty() ? null : list.Where(c => c != null).Select(c => (T)c.Clone()).ToList();
        }

        public static bool IsNullOrEmpty<T>(this HashSet<T> hash)
        {
            return hash == null || hash.Count == 0;
        }

        public static bool IsNullOrEmpty(this Hashtable hash)
        {
            return hash == null || hash.Count == 0;
        }
    }
}