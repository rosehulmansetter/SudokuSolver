using System.Collections.Generic;

namespace SudokuRulesEngine
{
    public class Board
    {
        private List<List<int>> cellData;

        public Board()
        {
            cellData = new List<List<int>>();

            for(int i = 0; i < 81; i++)
            {
                cellData.Add(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }
        }

        public void SetCell(int index, int value)
        {
            cellData[index] = new List<int> { value };
        }

        public bool IsSolved()
        {
            return false;
        }

        public List<List<int>> GetCellData()
        {
            return cellData;
        }
    }
}
