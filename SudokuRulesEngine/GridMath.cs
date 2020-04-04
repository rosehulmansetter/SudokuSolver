using SudokuRulesEngine.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace SudokuRulesEngine
{
    public static class GridMath
    {
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

        public static HashSet<int> GetRelatedCellIndices(int cellIndex)
        {
            HashSet<int> relatedCellIndices = new HashSet<int>();

            relatedCellIndices.AddRange(GetIndicesInSameRow(cellIndex));
            relatedCellIndices.AddRange(GetIndicesInSameColumn(cellIndex));
            relatedCellIndices.AddRange(GetIndicesInSameSquare(cellIndex));
            relatedCellIndices.Remove(cellIndex);

            return relatedCellIndices;
        }

        internal static List<int> GetIndicesInColumn(int i)
        {
            List<int> indicesInColumn = new List<int>();
            for (int cellIndex = i; indicesInColumn.Count < 9; cellIndex += 9)
            {
                indicesInColumn.Add(cellIndex);
            }
            return indicesInColumn;
        }

        internal static List<int> GetIndicesInRow(int i)
        {
            List<int> indicesInRow = new List<int>();
            for(int cellIndex = 9*i; indicesInRow.Count < 9; cellIndex++)
            {
                indicesInRow.Add(cellIndex);
            }
            return indicesInRow;
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

        public static List<int> GetIndicesInSameRow(int cellIndex)
        {
            List<int> indicesInSameRow = new List<int>();
            int rowIndex = cellIndex / 9;
            for (int i = 0; i < 9; i++)
            {
                indicesInSameRow.Add(GetIndexByRowAndColumnIndices(rowIndex, i));
            }
            return indicesInSameRow;
        }

        public static List<int> GetIndicesInSameColumn(int cellIndex)
        {
            List<int> indicesInSameColumn = new List<int>();
            int columnIndex = cellIndex % 9;
            for (int i = 0; i < 9; i++)
            {
                indicesInSameColumn.Add(GetIndexByRowAndColumnIndices(i, columnIndex));
            }
            return indicesInSameColumn;
        }

        public static List<int> GetIndicesInSameSquare(int cellIndex)
        {
            List<int> indicesInSameSquare = new List<int>();
            int rowIndex = cellIndex / 9;
            int squareRowIndex = rowIndex / 3;
            int columnIndex = cellIndex % 9;
            int squareColumnIndex = columnIndex / 3;
            for (int row = squareRowIndex*3; row < (squareRowIndex+1)*3; row++)
            {
                for(int column = squareColumnIndex*3; column < (squareColumnIndex+1)*3; column++)
                {
                    indicesInSameSquare.Add(GetIndexByRowAndColumnIndices(row, column));
                }
            }
            return indicesInSameSquare;
        }
    }
}
