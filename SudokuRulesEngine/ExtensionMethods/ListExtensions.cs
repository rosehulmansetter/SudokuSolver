using System.Collections.Generic;

namespace SudokuRulesEngine.ExtensionMethods
{
    public static class ListExtensions
    {
        public static T Last<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }
    }
}
