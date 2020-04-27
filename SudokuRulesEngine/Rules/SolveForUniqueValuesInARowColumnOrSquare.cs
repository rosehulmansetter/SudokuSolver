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

            for (int i = 0; i < GridMath.CellsInRow; i++)
            {
                bool ruleSucceededForCells = SolveUniquePossibilitiesInCells(ref board, board.GetCellDataForRow(i));
                ruleSucceeded |= ruleSucceededForCells;
            }
            return ruleSucceeded;
        }

        private bool ApplyRuleForAllColumns(ref Board board)
        {
            bool ruleSucceeded = false;

            for (int i = 0; i < GridMath.CellsInColumn; i++)
            {
                bool ruleSucceededForCells = SolveUniquePossibilitiesInCells(ref board, board.GetCellDataForColumn(i));
                ruleSucceeded |= ruleSucceededForCells;
            }
            return ruleSucceeded;
        }

        private bool ApplyRuleForAllSquares(ref Board board)
        {
            bool ruleSucceeded = false;

            for (int i = 0; i < GridMath.CellsInSquare; i++)
            {
                bool ruleSucceededForCells = SolveUniquePossibilitiesInCells(ref board, board.GetCellDataForSquare(i));
                ruleSucceeded |= ruleSucceededForCells;
            }
            return ruleSucceeded;
        }

        private bool SolveUniquePossibilitiesInCells(ref Board board, CellData cellData)
        {
            bool ruleSucceeded = false;
            Dictionary<int, List<int>> unsolvedCells = cellData.GetUnsolvedCells();
            List<int> unsolvedValues = cellData.GetUnsolvedValues();

            foreach (int unsolvedValue in unsolvedValues)
            {
                if (unsolvedCells.Values.Count(cell => cell.Contains(unsolvedValue)) == 1)
                {
                    int index = unsolvedCells.First(cell => cell.Value.Contains(unsolvedValue)).Key;
                    board.SetCell(index, unsolvedValue);
                    ruleSucceeded = true;
                }
            }

            return ruleSucceeded;
        }
    }
}
