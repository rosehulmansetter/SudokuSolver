using System.Collections.Generic;
using System.Linq;

namespace SudokuRulesEngine.Rules
{
    public class SolveForUniqueValuesInARowColumnOrSquare : Rule
    {
        public bool ApplyRule(ref Board board)
        {
            bool ruleSucceededForRows = ApplyRuleForAllRows(ref board);
            bool ruleSucceededForColumns = ApplyRuleForAllColumns(ref board);
            bool ruleSucceededSquares = ApplyRuleForAllSquares(ref board);

            return ruleSucceededForRows || ruleSucceededForColumns || ruleSucceededSquares;
        }

        private bool ApplyRuleForAllRows(ref Board board)
        {
            bool ruleSucceeded = false;

            for (int i = 0; i < 9; i++)
            {
                bool ruleSucceededForCells = SolveUniquePossibilitiesInCells(ref board, board.GetCellDataForRow(i));
                ruleSucceeded |= ruleSucceededForCells;
            }
            return ruleSucceeded;
        }

        private bool ApplyRuleForAllColumns(ref Board board)
        {
            bool ruleSucceeded = false;

            for (int i = 0; i < 9; i++)
            {
                bool ruleSucceededForCells = SolveUniquePossibilitiesInCells(ref board, board.GetCellDataForColumn(i));
                ruleSucceeded |= ruleSucceededForCells;
            }
            return ruleSucceeded;
        }

        private bool ApplyRuleForAllSquares(ref Board board)
        {
            bool ruleSucceeded = false;

            for (int i = 0; i < 9; i++)
            {
                bool ruleSucceededForCells = SolveUniquePossibilitiesInCells(ref board, board.GetCellDataForSquare(i));
                ruleSucceeded |= ruleSucceededForCells;
            }
            return ruleSucceeded;
        }

        private bool SolveUniquePossibilitiesInCells(ref Board board, Dictionary<int, List<int>> cellData)
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
