using SudokuRulesEngine.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace SudokuRulesEngine
{
    public class SubsetIterator<T>
    {
        private List<T> PossibleValues;
        readonly int SetSize;
        List<int> TrackingIndices;

        public SubsetIterator(int setSize, List<T> values)
        {
            if(setSize > values.Count)
            {
                throw new Exception();
            }
            SetSize = setSize;
            PossibleValues = values;
            TrackingIndices = new List<int>();
        }

        private void InitializeTrackingIndices()
        {
            for(int i = 0; i < SetSize; i++)
            {
                TrackingIndices.Add(i);
            }
        }

        public bool MoveNext()
        {
            if (TrackingIndices.Count == 0)
            {
                InitializeTrackingIndices();
                return true;
            }

            for(int index = TrackingIndices.Count - 1; index >= 0; index--)
            {
                int nextIllegalIndex = index == TrackingIndices.Count - 1 ? PossibleValues.Count : TrackingIndices[index + 1];

                if (TrackingIndices[index] + 1 < nextIllegalIndex)
                {
                    TrackingIndices[index] += 1;
                    for(int j = 1; j < SetSize - index; j++)
                    {
                        TrackingIndices[index + j] = TrackingIndices[index] + j;
                    }
                    return true;
                }
            }

            return false;
        }

        public List<T> Current
        {
            get
            {
                var result = new List<T>();
                foreach(int index in TrackingIndices)
                {
                    result.Add(PossibleValues[index]);
                }
                return result;
            }
        }
    }
}
