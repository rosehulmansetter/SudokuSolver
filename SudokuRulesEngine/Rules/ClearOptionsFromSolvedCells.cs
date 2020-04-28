﻿using System.Collections.Generic;
using System.Linq;

namespace SudokuRulesEngine.Rules
{
    public class ClearOptionsFromSolvedCells : Rule
    {
        public bool ApplyRule(ref Board board)
        {
            bool ruleSuccess = false;

            for(int i = 0; i < GridMath.TotalNumberOfCells; i++)
            {
                var currentCellValues = board.GetPossibleValues(i);
                if(currentCellValues.Count == 1)
                {
                    List<int> relatedCellIndices = GridMath.GetRelatedCellIndices(i);

                    foreach(int relatedCellIndex in relatedCellIndices)
                    {
                        bool removalSuccessful = board.RemoveValueFromCell(relatedCellIndex, currentCellValues.First());
                        ruleSuccess |= removalSuccessful;
                    }
                }
            }

            return ruleSuccess;
        }
    }
}
