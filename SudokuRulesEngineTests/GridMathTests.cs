using FluentAssertions;
using NUnit.Framework;
using SudokuRulesEngine;
using System.Collections.Generic;

namespace SudokuRulesEngineTests
{
    [TestFixture]
    public class GridMathTests
    {
        private const int EXPECTED_NUMBER_OF_RELATED_INDICES = 20;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckTotalNumberOfCellsValue()
        {
            GridMath.TotalNumberOfCells.Should().Be(81);
        }

        [Test]
        public void CheckAllPossibleValues()
        {
            var actual = GridMath.AllPossibleValues();

            actual.Count.Should().Be(9);
            actual.Should().Contain(1);
            actual.Should().Contain(2);
            actual.Should().Contain(3);
            actual.Should().Contain(4);
            actual.Should().Contain(5);
            actual.Should().Contain(6);
            actual.Should().Contain(7);
            actual.Should().Contain(8);
            actual.Should().Contain(9);
        }

        [TestCase(0, 7, 7)]
        [TestCase(1, 3, 12)]
        [TestCase(2, 0, 18)]
        [TestCase(4, 8, 44)]
        [TestCase(6, 1, 55)]
        [TestCase(8, 2, 74)]
        public void TestGetIndexByRowAndColumn(int row, int column, int expectedIndex)
        {
            GridMath.GetIndexByRowAndColumnIndices(row, column).Should().Be(expectedIndex);
        }

        [TestCase(66, 7)]
        [TestCase(59, 6)]
        [TestCase(40, 4)]
        [TestCase(1, 0)]
        [TestCase(48, 5)]
        [TestCase(11, 1)]
        [TestCase(77, 8)]
        [TestCase(21, 2)]
        [TestCase(30, 3)]
        public void TestRowForIndex(int index, int expectedRow)
        {
            GridMath.GetRowForIndex(index).Should().Be(expectedRow);
        }

        [TestCase(7, 7)]
        [TestCase(15, 6)]
        [TestCase(22, 4)]
        [TestCase(27, 0)]
        [TestCase(32, 5)]
        [TestCase(37, 1)]
        [TestCase(44, 8)]
        [TestCase(74, 2)]
        [TestCase(57, 3)]
        public void TestColumnForIndex(int index, int expectedColumn)
        {
            GridMath.GetColumnForIndex(index).Should().Be(expectedColumn);
        }

        [TestCase(59, 7)]
        [TestCase(72, 6)]
        [TestCase(41, 4)]
        [TestCase(19, 0)]
        [TestCase(53, 5)]
        [TestCase(13, 1)]
        [TestCase(60, 8)]
        [TestCase(8, 2)]
        [TestCase(46, 3)]
        public void TestSquareForIndex(int index, int expectedSquare)
        {
            GridMath.GetSquareForIndex(index).Should().Be(expectedSquare);
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
            List<int> actualRelatedIndices = GridMath.GetRelatedCellIndices(index);

            actualRelatedIndices.Should().NotBeEmpty()
                .And.HaveCount(EXPECTED_NUMBER_OF_RELATED_INDICES);

            foreach(int expectedIndex in expectedRelatedIndices)
            {
                actualRelatedIndices.Should().Contain(expectedIndex);
            }
        }

        [TestCase(0, new int[] { 0, 1, 2, 3, 5, 6, 7, 8 })]
        [TestCase(1, new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 17 })]
        [TestCase(2, new int[] { 18, 19, 20, 21, 22, 23, 24, 25, 26 })]
        [TestCase(3, new int[] { 27, 28, 29, 30, 31, 32, 33, 34, 35 })]
        [TestCase(4, new int[] { 36, 37, 38, 39, 40, 41, 42, 43, 44 })]
        [TestCase(5, new int[] { 45, 46, 47, 48, 49, 50, 51, 52, 53 })]
        [TestCase(6, new int[] { 54, 55, 56, 57, 58, 59, 60, 61, 62 })]
        [TestCase(7, new int[] { 63, 64, 65, 66, 67, 68, 69, 70, 71 })]
        [TestCase(8, new int[] { 72, 73, 74, 75, 76, 77, 78, 79, 80 })]
        public void TestIndicesInRow(int row, int[] expectedIndices)
        {
            List<int> actualIndicesInRow = GridMath.GetIndicesInRow(row);

            actualIndicesInRow.Should().NotBeEmpty()
                .And.HaveCount(GridMath.CellsInRow);

            foreach (int expectedIndex in expectedIndices)
            {
                actualIndicesInRow.Should().Contain(expectedIndex);
            }
        }

        [TestCase(0, new int[] { 0, 9, 18, 27, 36, 45, 54, 63, 72 })]
        [TestCase(1, new int[] { 1, 10, 19, 28, 37, 46, 55, 64, 73 })]
        [TestCase(2, new int[] { 2, 11, 20, 29, 38, 47, 56, 65, 74 })]
        [TestCase(3, new int[] { 3, 12, 21, 30, 39, 48, 57, 66, 75 })]
        [TestCase(4, new int[] { 4, 13, 22, 31, 40, 49, 58, 67, 76 })]
        [TestCase(5, new int[] { 5, 14, 23, 32, 41, 50, 59, 68, 77 })]
        [TestCase(6, new int[] { 6, 15, 24, 33, 42, 51, 60, 69, 78 })]
        [TestCase(7, new int[] { 7, 16, 25, 34, 43, 52, 61, 70, 79 })]
        [TestCase(8, new int[] { 8, 17, 26, 35, 44, 53, 62, 71, 80 })]
        public void TestIndicesInColumn(int column, int[] expectedIndices)
        {
            List<int> actualIndicesInColumn = GridMath.GetIndicesInColumn(column);

            actualIndicesInColumn.Should().NotBeEmpty()
                .And.HaveCount(GridMath.CellsInColumn);

            foreach (int expectedIndex in expectedIndices)
            {
                actualIndicesInColumn.Should().Contain(expectedIndex);
            }
        }

        [TestCase(0, new int[] { 0, 1, 2, 9, 10, 11, 18, 19, 20 })]
        [TestCase(1, new int[] { 3, 4, 5, 12, 13, 14, 21, 22, 23 })]
        [TestCase(2, new int[] { 6, 7, 8, 15, 16, 17, 24, 25, 26 })]
        [TestCase(3, new int[] { 27, 28, 29, 36, 37, 38, 45, 46, 47 })]
        [TestCase(4, new int[] { 30, 31, 32, 39, 40, 41, 48, 49, 50 })]
        [TestCase(5, new int[] { 33, 34, 35, 42, 43, 44, 51, 52, 53 })]
        [TestCase(6, new int[] { 54, 55, 56, 63, 64, 65, 72, 73, 74 })]
        [TestCase(7, new int[] { 57, 58, 59, 66, 67, 68, 75, 76, 77 })]
        [TestCase(8, new int[] { 60, 61, 62, 69, 70, 71, 78, 79, 80 })]
        public void TestIndicesInSquare(int square, int[] expectedIndices)
        {
            List<int> actualIndicesInSquare = GridMath.GetIndicesInSquare(square);

            actualIndicesInSquare.Should().NotBeEmpty()
                .And.HaveCount(GridMath.CellsInSquare);

            foreach (int expectedIndex in expectedIndices)
            {
                actualIndicesInSquare.Should().Contain(expectedIndex);
            }
        }

        [TestCase(13, 7, 6)]
        [TestCase(98, 4, 2)]
        [TestCase(26, 13, 0)]
        [TestCase(9, 9, 0)]
        [TestCase(-47, 5, 3)]
        [TestCase(0, 15, 0)]
        public void TestMod(int number, int mod, int expectedResult)
        {
            number.Mod(mod).Should().Be(expectedResult);
        }
    }
}