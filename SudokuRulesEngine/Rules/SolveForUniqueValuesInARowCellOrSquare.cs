using SudokuRulesEngine.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuRulesEngine.Rules
{
    public class SolveForUniqueValuesInARowCellOrSquare : Rule
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
            Dictionary<int, List<int>> unsolvedCells = cellData.Where(cell => cell.Value.Count > 1)
                    .ToDictionary(cell => cell.Key, cell => cell.Value);
            HashSet<int> unsolvedValues = new HashSet<int>();
            foreach (List<int> unsolvedCellValues in unsolvedCells.Values)
            {
                unsolvedValues.AddRange(unsolvedCellValues);
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
