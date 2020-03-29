using System.Collections.Generic;
using System.Linq;

namespace SudokuRulesEngine.Rules
{
    public class SolveForUniqueValuesInARowColumnOrSquare : Rule
    {
        public bool ApplyRule(ref Board board)
        {
            bool ruleSucceeded = false;

            ruleSucceeded |= ApplyRuleForAllRows(board);
            ruleSucceeded |= ApplyRuleForAllColumns(board);
            ruleSucceeded |= ApplyRuleForAllSquares(board);

            return ruleSucceeded;
        }

        private bool ApplyRuleForAllSquares(Board board)
        {
            bool ruleSucceeded = false;

            for (int i = 0; i < 9; i++)
            {
                ruleSucceeded |= SolveUniquePossibilitiesInCells(board, board.GetCellDataForSquare(i));
            }
            return ruleSucceeded;
        }

        private bool ApplyRuleForAllColumns(Board board)
        {
            bool ruleSucceeded = false;

            for (int i = 0; i < 9; i++)
            {
                ruleSucceeded |= SolveUniquePossibilitiesInCells(board, board.GetCellDataForColumn(i));
            }
            return ruleSucceeded;
        }

        private bool ApplyRuleForAllRows(Board board)
        {
            bool ruleSucceeded = false;

            for (int i = 0; i < 9; i++)
            {
                ruleSucceeded |= SolveUniquePossibilitiesInCells(board, board.GetCellDataForRow(i));
            }
            return ruleSucceeded;
        }

        private bool SolveUniquePossibilitiesInCells(Board board, Dictionary<int, List<int>> cellData)
        {
            bool ruleSucceeded = false;
            Dictionary<int, List<int>> unsolvedCells = new Dictionary<int, List<int>>();
            List<int> unsolvedValues = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            foreach (int cellIndex in cellData.Keys)
            {
                if(cellData[cellIndex].Count == 1)
                {
                    unsolvedValues.Remove(cellData[cellIndex].First());
                }
                else
                {
                    unsolvedCells.Add(cellIndex, cellData[cellIndex]);
                }
            }
            foreach (int unsolvedValue in unsolvedValues)
            {
                if (unsolvedCells.Values.Count(cell => cell.Contains(unsolvedValue)) == 1)
                {
                    foreach (KeyValuePair<int, List<int>> cell in unsolvedCells)
                    {
                        if (cell.Value.Contains(unsolvedValue))
                        {
                            board.SetCell(cell.Key, unsolvedValue);
                            ruleSucceeded = true;
                        }
                    }
                }
            }

            return ruleSucceeded;
        }
    }
}
