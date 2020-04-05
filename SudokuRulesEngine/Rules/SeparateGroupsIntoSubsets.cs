using SudokuRulesEngine.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

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
            bool ruleSucceeded = false;

            var unsolvedValues = cellData.GetUnsolvedValues();
            var unsolvedCells = cellData.GetUnsolvedCells();

            if(groupSize >= unsolvedCells.Count)
            {
                return false;
            }

            var valueSetGenerator = new SubsetIterator<int>(groupSize, unsolvedValues);

            while(valueSetGenerator.MoveNext())
            {
                List<int> setOfValues = valueSetGenerator.Current;
                HashSet<KeyValuePair<int, List<int>>> cellsWithValues = new HashSet<KeyValuePair<int, List<int>>>();

                foreach(int value in setOfValues)
                {
                    cellsWithValues.AddRange(unsolvedCells.Where(kv => kv.Value.Contains(value)).ToList());
                }

                if(cellsWithValues.Count == groupSize)
                {
                    List<int> otherValues = unsolvedValues.SubtractRange(setOfValues);

                    foreach(KeyValuePair<int, List<int>> cell in cellsWithValues)
                    {
                        foreach(int otherValue in otherValues)
                        {
                            if(cell.Value.Contains(otherValue))
                            {
                                board.RemoveValueFromCell(cell.Key, otherValue);
                                ruleSucceeded = true;
                            }
                        }
                    }
                }
            }

            var cellSetGenerator = new SubsetIterator<KeyValuePair<int, List<int>>>(groupSize, unsolvedCells.ToList());

            while(cellSetGenerator.MoveNext())
            {
                List<KeyValuePair<int, List<int>>> setOfCells = cellSetGenerator.Current;
                HashSet<int> valuesInCells = new HashSet<int>();

                foreach(KeyValuePair<int, List<int>> cell in setOfCells)
                {
                    valuesInCells.AddRange(cell.Value);
                }

                if(valuesInCells.Count == groupSize)
                {
                    foreach(KeyValuePair<int, List<int>> cell in unsolvedCells)
                    {
                        if (!setOfCells.Select(c => c.Key).Contains(cell.Key))
                        {
                            foreach(int valueInCell in valuesInCells)
                            {
                                if(cell.Value.Contains(valueInCell))
                                {
                                    board.RemoveValueFromCell(cell.Key, valueInCell);
                                    ruleSucceeded = true;
                                }
                            }
                        }
                    }
                }
            }

            return ruleSucceeded;
        }
    }
}
