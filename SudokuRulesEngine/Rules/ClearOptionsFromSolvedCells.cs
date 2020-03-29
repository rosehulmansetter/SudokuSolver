using System.Collections.Generic;
using System.Linq;

namespace SudokuRulesEngine.Rules
{
    class ClearOptionsFromSolvedCells : Rule
    {
        public bool ApplyRule(ref Board board)
        {
            bool ruleSuccess = false;

            List<List<int>> cells = board.GetCellData();
            //go through cells
            for(int i = 0; i < cells.Count; i++)
            {
                var currentCellValues = cells[i];
                if(currentCellValues.Count == 1)
                {
                    HashSet<int> relatedCellIndices = GridMath.GetRelatedCellIndices(i);
                    IEnumerator<int> enumerator = relatedCellIndices.GetEnumerator();
                    do
                    {
                        ruleSuccess |= board.RemoveValueFromCell(enumerator.Current, currentCellValues.First());
                    }
                    while (enumerator.MoveNext());
                }
            }

            return ruleSuccess;
        }
    }
}
