using System;
using System.Collections.Generic;

namespace SudokuRulesEngine.Rules
{
    class SeparateGroupsIntoSubsets : Rule
    {
        public bool ApplyRule(ref Board board)
        {
            bool ruleSucceeded = false;

            int groupSize = 2;
            while(groupSize <= 5 && !ruleSucceeded)
            {
                ruleSucceeded |= CheckRuleForGroupSizeOf(groupSize, ref board);
                groupSize++;
            }

            return ruleSucceeded;
        }

        private bool CheckRuleForGroupSizeOf(int groupSize, ref Board board)
        {
            bool ruleSucceededForRows = SeparateRowsIntoSubsets(groupSize, ref board);
            bool ruleSucceededForColumns = SeparateColumnsIntoSubsets(groupSize, ref board);
            bool ruleSucceededForSquares = SeparateSquaresIntoSubsets(groupSize, ref board);

            return ruleSucceededForRows || ruleSucceededForColumns || ruleSucceededForSquares;
        }

        private bool SeparateRowsIntoSubsets(int groupSize, ref Board board)
        {
            bool ruleSucceeded = false;

            for (int i = 0; i < 9; i++)
            {
                bool ruleSucceededForCells = SolveForCells(groupSize, ref board, board.GetCellDataForRow(i));
                ruleSucceeded |= ruleSucceededForCells;
            }

            return ruleSucceeded;
        }

        private bool SeparateColumnsIntoSubsets(int groupSize, ref Board board)
        {
            bool ruleSucceeded = false;

            for (int i = 0; i < 9; i++)
            {
                bool ruleSucceededForCells = SolveForCells(groupSize, ref board, board.GetCellDataForColumn(i));
                ruleSucceeded |= ruleSucceededForCells;
            }

            return ruleSucceeded;
        }

        private bool SeparateSquaresIntoSubsets(int groupSize, ref Board board)
        {
            bool ruleSucceeded = false;

            for (int i = 0; i < 9; i++)
            {
                bool ruleSucceededForCells = SolveForCells(groupSize, ref board, board.GetCellDataForSquare(i));
                ruleSucceeded |= ruleSucceededForCells;
            }

            return ruleSucceeded;
        }

        private bool SolveForCells(int groupSize, ref Board board, Dictionary<int, List<int>> cellData)
        {
            List<int> unsolvedValues = 
        }
    }
}
