using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using GenericNumbers;
using SimpleMonads;

namespace MoreCollections
{
    public static class Extensions
    {
        public static IEnumerable<T> Take<T>(this IEnumerable<T> source, INumberRange<int> take)
        {
            return source.Skip(take.LowerBound.ChangeStrictness(false).Value).Take(take.Size);
        }

        public static string GetCommonBeginning(this string str1, string str2)
        {
            int i;
            for (i = 0; i < Math.Min(str1.Length, str2.Length) && str1[i] == str2[i]; i++) ;
            if (i == str1.Length)
                return str1;
            return str1.Substring(0, i);
        }

        public static string GetCommonEnding(this string str1, string str2)
        {
            int i;
            for (i = 0; i < Math.Min(str1.Length, str2.Length) && str1[str1.Length - i - 1] == str2[str2.Length - i - 1]; i++) ;
            if (i == str1.Length)
                return str1;
            return str1.Substring(str1.Length - i);
        }

        public static bool RemoveRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey[] keys)
        {
            return dictionary.RemoveRange(keys.AsEnumerable());
;        }

        public static bool RemoveRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey> keys)
        {
            bool removedAll = true;
            foreach (var key in keys)
            {
                removedAll = removedAll && dictionary.Remove(key);
            }
            return removedAll;
        }

        /// <summary>
        /// Converts the specified ObservableCollection into an IReadOnlyObservableList,
        /// which can be chained using Select and Where extension methods.
        /// </summary>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            return new ObservableCollection<T>(source);
        }
    }
}
