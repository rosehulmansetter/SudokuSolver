namespace SudokuRulesEngine
{
    public static class GridMath
    {
        public static int GetIndexByGridAndSquareIndexes(int gridRowIndex, int gridColumnIndex, int squareRowIndex, int squareColumnIndex)
        {
            return 9 * (3 * gridRowIndex + squareRowIndex) + 3 * gridColumnIndex + squareColumnIndex;
        }
    }
}
