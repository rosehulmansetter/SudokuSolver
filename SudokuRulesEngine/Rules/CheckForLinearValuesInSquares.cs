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
                        List<int> allCellIndicesInRow = GridMath.GetIndicesInSameRow(cellIndicesWithValue.First());
                        foreach(int cellIndex in allCellIndicesInRow)
                        {
                            if(!cellIndicesWithValue.Contains(cellIndex) && status.UnsolvedCells[cellIndex].Contains(unsolvedValue))
                            {
                                ruleSucceeded = true;
                                board.RemoveValueFromCell(cellIndex, unsolvedValue);
                            }
                        }
                    }
                    else if (InSameColumn(cellIndicesWithValue))
                    {
                        List<int> allCellIndicesInColumn = GridMath.GetIndicesInSameColumn(cellIndicesWithValue.First());
                        foreach (int cellIndex in allCellIndicesInColumn)
                        {
                            if (!cellIndicesWithValue.Contains(cellIndex) && status.UnsolvedCells[cellIndex].Contains(unsolvedValue))
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
