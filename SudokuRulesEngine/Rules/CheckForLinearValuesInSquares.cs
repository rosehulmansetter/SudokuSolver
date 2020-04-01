using System.Collections.Generic;
using System.Linq;

namespace SudokuRulesEngine.Rules
{
    public class CheckForLinearValuesInSquares : Rule
    {
        public bool ApplyRule(ref Board board)
        {
            bool ruleSucceeded = false;

            for(int squareNumber = 0; squareNumber < 9; squareNumber++)
            {
                CellDataStatus status = board.GetSquareStatus(squareNumber);
                foreach(int unsolvedValue in status.UnsolvedValues)
                {
                    List<int> cellIndicesWithValue = status.GetCellIndicesWithValue(unsolvedValue);
                    if (InSameRow(cellIndicesWithValue))
                    {
                        Dictionary<int, List<int>> allCellsInRow = board.GetCellDataForRow(GridMath.GetRowForIndex(cellIndicesWithValue.First()));
                        foreach(int cellIndex in allCellsInRow.Keys)
                        {
                            if(!cellIndicesWithValue.Contains(cellIndex) && allCellsInRow[cellIndex].Contains(unsolvedValue))
                            {
                                ruleSucceeded = true;
                                board.RemoveValueFromCell(cellIndex, unsolvedValue);
                            }
                        }
                    }
                    else if (InSameColumn(cellIndicesWithValue))
                    {
                        Dictionary<int, List<int>> allCellsInColumn = board.GetCellDataForColumn(GridMath.GetColumnForIndex(cellIndicesWithValue.First()));
                        foreach (int cellIndex in allCellsInColumn.Keys)
                        {
                            if (!cellIndicesWithValue.Contains(cellIndex) && allCellsInColumn[cellIndex].Contains(unsolvedValue))
                            {
                                ruleSucceeded = true;
                                board.RemoveValueFromCell(cellIndex, unsolvedValue);
                            }
                        }
                    }
                }
            }

            return ruleSucceeded;
        }

        private bool InSameRow(List<int> cellIndicesWithValue)
        {
            var row = GridMath.GetRowForIndex(cellIndicesWithValue.First());
            return cellIndicesWithValue.All(x => GridMath.GetRowForIndex(x) == row);
        }

        private bool InSameColumn(List<int> cellIndicesWithValue)
        {
            var column = GridMath.GetColumnForIndex(cellIndicesWithValue.First());
            return cellIndicesWithValue.All(x => GridMath.GetColumnForIndex(x) == column);
        }
    }
}
