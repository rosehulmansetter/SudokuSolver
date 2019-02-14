namespace SudokuRulesEngine
{
    public static class GridMath
    {
        public static int GetIndexByRowAndColumnIndexes(int rowIndex, int columnIndex)
        {
            return rowIndex * 9 + columnIndex;
        }
    }
}
