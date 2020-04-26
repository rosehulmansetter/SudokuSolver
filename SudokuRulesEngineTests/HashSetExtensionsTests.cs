using FluentAssertions;
using NUnit.Framework;
using SudokuRulesEngine.ExtensionMethods;
using System.Collections.Generic;

namespace SudokuRulesEngineTests
{
    [TestFixture]
    public class HashSetExtensionsTests
    {
        [Test]
        public void TestAddRangeAddsElements()
        {
            var elementsToBeAdded = new List<int> { 1, 3, 5 };

            HashSet<int> hash = new HashSet<int>();

            hash.AddRange(elementsToBeAdded);

            foreach(int elementToBeAdded in elementsToBeAdded)
            {
                hash.Should().Contain(elementToBeAdded);
            }

            var moreElementsToBeAdded = new List<int> { 5, 7, 9 };

            hash.AddRange(moreElementsToBeAdded);

            foreach (int elementToBeAdded in moreElementsToBeAdded)
            {
                hash.Should().Contain(elementToBeAdded);
            }

            hash.Count.Should().Be(5);
        }
    }
}
