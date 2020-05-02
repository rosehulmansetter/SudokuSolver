using SudokuRulesEngine.ExtensionMethods;
using System.Collections.Generic;
using System.Linq;

namespace SudokuRulesEngine
{
    public static class GridMath
    {
        public const int TotalNumberOfCells = 81;
        public const int CellsInRow = 9, CellsInColumn = 9, CellsInSquare = 9,
            SquareSize = 3;

        public static List<int> AllPossibleValues()
        {
            return new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }

        public static int GetIndexByRowAndColumnIndices(int rowIndex, int columnIndex)
        {
            return rowIndex * 9 + columnIndex;
        }

        public static int GetRowForIndex(int cellIndex)
        {
            return cellIndex / 9;
        }

        public static int GetColumnForIndex(int cellIndex)
        {
            return cellIndex % 9;
        }

        public static int GetSquareForIndex(int cellIndex)
        {
            return 3 * (GetRowForIndex(cellIndex) / 3) + GetColumnForIndex(cellIndex) / 3;
        }

        public static List<int> GetRelatedCellIndices(int cellIndex)
        {
            HashSet<int> relatedCellIndices = new HashSet<int>();

            relatedCellIndices.AddRange(GetIndicesInRow(GetRowForIndex(cellIndex)));
            relatedCellIndices.AddRange(GetIndicesInColumn(GetColumnForIndex(cellIndex)));
            relatedCellIndices.AddRange(GetIndicesInSquare(GetSquareForIndex(cellIndex)));
            relatedCellIndices.Remove(cellIndex);

            return relatedCellIndices.ToList();
        }

        public static List<int> GetIndicesInRow(int i)
        {
            List<int> indicesInRow = new List<int>();
            for (int cellIndex = 9 * i; indicesInRow.Count < 9; cellIndex++)
            {
                indicesInRow.Add(cellIndex);
            }
            return indicesInRow;
        }

        public static List<int> GetIndicesInColumn(int i)
        {
            List<int> indicesInColumn = new List<int>();
            for (int cellIndex = i; indicesInColumn.Count < 9; cellIndex += 9)
            {
                indicesInColumn.Add(cellIndex);
            }
            return indicesInColumn;
        }

        public static List<int> GetIndicesInSquare(int squareIndex)
        {
            List<int> indicesInSquare = new List<int>();
            int squareRowIndex = squareIndex / 3;
            int squareColumnIndex = squareIndex % 3;
            for (int row = squareRowIndex * 3; row < (squareRowIndex + 1) * 3; row++)
            {
                for (int column = squareColumnIndex * 3; column < (squareColumnIndex + 1) * 3; column++)
                {
                    indicesInSquare.Add(GetIndexByRowAndColumnIndices(row, column));
                }
            }
            return indicesInSquare;
        }

        public static int Mod(this int num, int modNum)
        {
            if(num < 0)
            {
                return (num + modNum).Mod(modNum);
            }

            return num % modNum;
        }
    }
}
