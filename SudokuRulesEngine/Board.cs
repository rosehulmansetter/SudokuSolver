using System;
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
            foreach(List<int> cell in cellData)
            {
                if(cell.Count > 1)
                {
                    return false;
                }
            }
            return true;
        }

        internal Dictionary<int, List<int>> GetCellDataForSquare(int i)
        {
            return GetCellDataForIndices(GridMath.GetIndicesInSquare(i));
        }

        internal Dictionary<int, List<int>> GetCellDataForColumn(int i)
        {
            return GetCellDataForIndices(GridMath.GetIndicesInColumn(i));
        }

        internal Dictionary<int, List<int>> GetCellDataForRow(int i)
        {
            return GetCellDataForIndices(GridMath.GetIndicesInRow(i));
        }

        private Dictionary<int, List<int>> GetCellDataForIndices(List<int> indices)
        {
            Dictionary<int, List<int>> result = new Dictionary<int, List<int>>();
            foreach (int index in indices)
            {
                result.Add(index, cellData[index]);
            }
            return result;
        }

        public bool RemoveValueFromCell(int index, int value)
        {
            bool actionTaken = false;

            if(cellData[index].Contains(value))
            {
                cellData[index].Remove(value);
                actionTaken = true;
            }

            return actionTaken;
        }

        public List<List<int>> GetCellData()
        {
            return cellData;
        }

        public override bool Equals(object obj)
        {
            var board = obj as Board;
            return board != null &&
                   EqualityComparer<List<List<int>>>.Default.Equals(cellData, board.cellData);
        }

        public override int GetHashCode()
        {
            return 334095517 + EqualityComparer<List<List<int>>>.Default.GetHashCode(cellData);
        }

        public static bool operator ==(Board board1, Board board2)
        {
            List<List<int>> cellData1 = board1.GetCellData();
            List<List<int>> cellData2 = board2.GetCellData();
            for(int i = 0; i < cellData1.Count; i++)
            {
                List<int> possibleNumbers1 = cellData1[i];
                List<int> possibleNumbers2 = cellData2[i];

                if (possibleNumbers1.Count != possibleNumbers2.Count || 
                    possibleNumbers1.Intersect(possibleNumbers2).Count() != possibleNumbers1.Count)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator !=(Board board1, Board board2)
        {
            return !(board1 == board2);
        }
    }
}
