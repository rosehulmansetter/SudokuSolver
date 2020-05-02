using System.Collections.Generic;
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
                if(board.IsCellSolved(i))
                {
                    int solvedValue = board.GetPossibleValues(i).First();
                    List<int> relatedCellIndices = GridMath.GetRelatedCellIndices(i);

                    foreach(int relatedCellIndex in relatedCellIndices)
                    {
                        bool removalSuccessful = board.RemoveValueFromCell(relatedCellIndex, solvedValue);
                        ruleSuccess |= removalSuccessful;
                    }
                }
            }

            return ruleSuccess;
        }
    }
}
