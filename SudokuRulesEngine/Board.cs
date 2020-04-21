using System.Collections.Generic;
using System.Linq;

namespace SudokuRulesEngine
{
    public class Board
    {
        private List<List<int>> cellData;

        public Board()
        {
            cellData = new List<List<int>>();
            InitializeBoardValues();
        }

        public Board(Board other)
        {
            cellData = new List<List<int>>();
            foreach(List<int> possibleValues in other.cellData)
            {
                var newValues = new List<int>();
                newValues.AddRange(possibleValues);
                cellData.Add(newValues);
            }
        }

        private void InitializeBoardValues()
        {
            for (int i = 0; i < 81; i++)
            {
                cellData.Add(GridMath.AllPossibleValues());
            }
        }

        internal void SetCell(int index, int value)
        {
            cellData[index] = new List<int> { value };
        }

        internal bool RemoveValueFromCell(int index, int value)
        {
            return cellData[index].Remove(value);
        }

        internal bool IsSolved()
        {
            return cellData.All(c => c.Count == 1);
        }

        public CellData GetCellDataForSquare(int i)
        {
            return GetCellDataForIndices(GridMath.GetIndicesInSquare(i));
        }

        public CellData GetCellDataForColumn(int i)
        {
            return GetCellDataForIndices(GridMath.GetIndicesInColumn(i));
        }

        public CellData GetCellDataForRow(int i)
        {
            return GetCellDataForIndices(GridMath.GetIndicesInRow(i));
        }

        private CellData GetCellDataForIndices(List<int> indices)
        {
            CellData result = new CellData();
            foreach (int index in indices)
            {
                result.Add(index, cellData[index]);
            }
            return result;
        }

        public List<List<int>> GetCellData()
        {
            return cellData;
        }
    }
}
