using System.Collections.Generic;
using System.Linq;

namespace SudokuRulesEngine
{
    public class CellData : Dictionary<int, List<int>>
    {
        public List<int> GetUnsolvedValues()
        {
            var result = GridMath.AllPossibleValues();

            foreach(int index in Keys)
            {
                if(this[index].Count == 1)
                {
                    result.Remove(this[index].First());
                }
            }

            return result;
        }

        public Dictionary<int, List<int>> GetUnsolvedCells()
        {
            return this.Where(kv => kv.Value.Count > 1).ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public List<int> GetCellIndicesWithValue(int unsolvedValue)
        {
            return Keys.Where(k => this[k].Contains(unsolvedValue)).ToList<int>();
        }
    }
}
