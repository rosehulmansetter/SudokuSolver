using SudokuRulesEngine.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace SudokuRulesEngine
{
    public static class GridMath
    {
        public static int GetIndexByRowAndColumnIndices(int rowIndex, int columnIndex)
        {
            return rowIndex * 9 + columnIndex;
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

        private static List<int> GetIndicesInSameRow(int cellIndex)
        {
            List<int> indicesInSameRow = new List<int>();
            int rowIndex = cellIndex / 9;
            for(int i = 0; i < 9; i++)
            {
                indicesInSameRow.Add(GetIndexByRowAndColumnIndices(rowIndex, i));
            }
            return indicesInSameRow;
        }

        private static List<int> GetIndicesInSameColumn(int cellIndex)
        {
            List<int> indicesInSameColumn = new List<int>();
            int columnIndex = cellIndex % 9;
            for (int i = 0; i < 9; i++)
            {
                indicesInSameColumn.Add(GetIndexByRowAndColumnIndices(i, columnIndex));
            }
            return indicesInSameColumn;
        }

        private static List<int> GetIndicesInSameSquare(int cellIndex)
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
