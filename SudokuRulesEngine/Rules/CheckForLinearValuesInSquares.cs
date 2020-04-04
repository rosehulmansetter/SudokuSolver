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
                CellData cellData = board.GetCellDataForSquare(squareNumber);

                foreach(int unsolvedValue in cellData.GetUnsolvedValues())
                {
                    List<int> cellIndicesWithUnsolvedValue = cellData.GetCellIndicesWithValue(unsolvedValue);
                    if (InSameRow(cellIndicesWithUnsolvedValue))
                    {
                        CellData allCellsInRow = board.GetCellDataForRow(GridMath.GetRowForIndex(cellIndicesWithUnsolvedValue.First()));
                        bool removalSucceeded = RemoveUnsolvedValueFromCellsNotInSquare(ref board, unsolvedValue, squareNumber, allCellsInRow);
                        ruleSucceeded |= removalSucceeded;
                    }
                    else if (InSameColumn(cellIndicesWithUnsolvedValue))
                    {
                        CellData allCellsInColumn = board.GetCellDataForColumn(GridMath.GetColumnForIndex(cellIndicesWithUnsolvedValue.First()));
                        bool removalSucceeded = RemoveUnsolvedValueFromCellsNotInSquare(ref board, unsolvedValue, squareNumber, allCellsInColumn);
                        ruleSucceeded |= removalSucceeded;
                    }
                }
            }

            return ruleSucceeded;
        }

        private bool InSameRow(List<int> cellIndicesWithValue)
        {
            return cellIndicesWithValue.Select(x => GridMath.GetRowForIndex(x)).Distinct().Count() == 1;
        }

        private bool InSameColumn(List<int> cellIndicesWithValue)
        {
            return cellIndicesWithValue.Select(x => GridMath.GetColumnForIndex(x)).Distinct().Count() == 1;
        }

        private bool RemoveUnsolvedValueFromCellsNotInSquare(ref Board board, int unsolvedValue, int squareNumber, CellData cells)
        {
            bool success = false;

            foreach (int cellIndex in cells.Keys)
            {
                if (GridMath.GetSquareForIndex(cellIndex) != squareNumber)
                {
                    bool removalSucceeded = board.RemoveValueFromCell(cellIndex, unsolvedValue);
                    success |= removalSucceeded;
                }
            }

            return success;
        }
    }
}
