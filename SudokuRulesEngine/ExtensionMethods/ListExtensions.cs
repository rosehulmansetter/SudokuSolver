using System.Collections.Generic;

namespace SudokuRulesEngine.ExtensionMethods
{
    public static class ListExtensions
    {
        public static bool IsSolved<T>(this List<T> list)
        {
            return list.Count == 1;
        }
    }
}
