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

        private bool SolveForCells(int groupSize, ref Board board, CellData cellData)
        {
            var unsolvedValues = cellData.GetUnsolvedValues();
            var unsolvedCells = cellData.GetUnsolvedCells();

            if(groupSize >= unsolvedCells.Count)
            {
                return false;
            }


            //for each set of groupSize values
            //- check cells to see if only groupSize cells contain these values
            //- if values are contained within groupSize cells
            //  - cells containing values should ONLY contain those values
            
            //for each set of groupSize cells
            //- check values in cells to see if only groupSize values are contained in these cells
            //- if values are contained within groupSize cells
            //  - cells not in this group should NOT contain these values
            return false;
        }
    }
}
