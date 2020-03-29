using FluentAssertions;
using NUnit.Framework;
using SudokuRulesEngine;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class GridMathTests
    {
        private const int EXPECTED_NUMBER_OF_EXPECTED_INDICES = 20;

        [SetUp]
        public void Setup()
        {
        }

        [TestCase(4, new int[] { 0, 1, 2, 3, 5, 6, 7, 8, 13, 22, 31, 40, 49, 58, 67, 76, 12, 14, 21, 23 })]
        [TestCase(17, new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 8, 26, 35, 44, 53, 62, 71, 80, 6, 7, 24, 25 })]
        [TestCase(19, new int[] { 18, 20, 21, 22, 23, 24, 25, 26, 1, 10, 28, 37, 46, 55, 64, 73, 0, 2, 9, 11 })]
        [TestCase(30, new int[] { 27, 28, 29, 31, 32, 33, 34, 35, 3, 12, 21, 39, 48, 57, 66, 75, 40, 41, 49, 50 })]
        [TestCase(43, new int[] { 36, 37, 38, 39, 40, 41, 42, 44, 7, 16, 25, 34, 52, 61, 70, 79, 33, 35, 51, 53 })]
        [TestCase(45, new int[] { 46, 47, 48, 49, 50, 51, 52, 53, 0, 9, 18, 27, 36, 54, 63, 72, 28, 29, 37, 38 })]
        [TestCase(60, new int[] { 54, 55, 56, 57, 58, 59, 61, 62, 6, 15, 24, 33, 42, 51, 69, 78, 70, 71, 79, 80 })]
        [TestCase(65, new int[] { 63, 64, 66, 67, 68, 69, 70, 71, 2, 11, 20, 29, 38, 47, 56, 74, 54, 55, 72, 73 })]
        [TestCase(77, new int[] { 72, 73, 74, 75, 76, 78, 79, 80, 5, 14, 23, 32, 41, 50, 59, 68, 57, 58, 66, 67 })]
        public void TestRelatedCellIndices(int index, int[] expectedRelatedIndices)
        {
            HashSet<int> actualRelatedIndices = GridMath.GetRelatedCellIndices(index);

            actualRelatedIndices.Should().NotBeEmpty()
                .And.HaveCount(EXPECTED_NUMBER_OF_EXPECTED_INDICES);

            foreach(int expectedIndex in expectedRelatedIndices)
            {
                actualRelatedIndices.Should().Contain(expectedIndex);
            }
        }
    }
}