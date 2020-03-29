using System.Collections.Generic;

namespace SudokuRulesEngine.ExtensionMethods
{
    public static class HashSetExtensions
    {
        public static void AddRange<T>(this HashSet<T> set, List<T> list)
        {
            foreach(T item in list)
            {
                set.Add(item);
            }
        }
    }
}
