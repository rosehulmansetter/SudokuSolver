using System.Collections.Generic;

namespace SudokuRulesEngine.ExtensionMethods
{
    public static class ListExtensions
    {
        public static T Last<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }

        public static List<T> SubtractRange<T>(this List<T> list, List<T> valuesToBeRemoved)
        {
            list.RemoveAll(val => valuesToBeRemoved.Contains(val));
            return list;
        }
    }
}
