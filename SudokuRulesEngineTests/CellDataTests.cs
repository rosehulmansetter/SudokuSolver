using FluentAssertions;
using NUnit.Framework;
using SudokuRulesEngine;
using System.Collections.Generic;
using System.Linq;

namespace SudokuRulesEngineTests
{
    [TestFixture]
    public class CellDataTests
    {
        [Test]
        public void TestWhenAllValuesAreSolved()
        {
            CellData cellData = new CellData
            {
                { 1, new List<int> { 1 } },
                { 2, new List<int> { 2 } },
                { 3, new List<int> { 3 } },
                { 4, new List<int> { 4 } },
                { 5, new List<int> { 5 } },
                { 6, new List<int> { 6 } },
                { 7, new List<int> { 7 } },
                { 8, new List<int> { 8 } },
                { 9, new List<int> { 9 } }
            };

            cellData.GetUnsolvedValues().Should().BeEmpty();
            cellData.GetUnsolvedCells().Should().BeEmpty();
        }

        [Test]
        public void TestUnsolvedValuesWhenSomeValuesAreSolved()
        {
            CellData cellData = new CellData
            {
                { 1, new List<int> { 1 } },
                { 2, new List<int> { 2, 3 } },
                { 3, new List<int> { 3, 7, 9 } },
                { 4, new List<int> { 4, 8 } },
                { 5, new List<int> { 5 } },
                { 6, new List<int> { 6 } },
                { 7, new List<int> { 7, 9 } },
                { 8, new List<int> { 8, 4 } },
                { 9, new List<int> { 9, 2 } }
            };

            var expectedUnsolvedValues = new List<int> { 2, 3, 4, 7, 8, 9 };
            var unsolvedValues = cellData.GetUnsolvedValues();

            unsolvedValues.Count.Should().Be(expectedUnsolvedValues.Count);

            foreach(int expectedUnsolvedValue in expectedUnsolvedValues)
            {
                unsolvedValues.Should().Contain(expectedUnsolvedValue);
            }
        }

        [Test]
        public void TestUnsolvedCellsWhenSomeCellsAreSolved()
        {
            CellData cellData = new CellData
            {
                { 1, new List<int> { 1 } },
                { 2, new List<int> { 2, 3 } },
                { 3, new List<int> { 3, 7, 9 } },
                { 4, new List<int> { 4, 8 } },
                { 5, new List<int> { 5 } },
                { 6, new List<int> { 6 } },
                { 7, new List<int> { 7, 9 } },
                { 8, new List<int> { 8, 4 } },
                { 9, new List<int> { 9, 2 } }
            };

            var expectedUnsolvedCellIndices = new List<int> { 2, 3, 4, 7, 8, 9 };
            var unsolvedCells = cellData.GetUnsolvedCells();

            unsolvedCells.Count.Should().Be(expectedUnsolvedCellIndices.Count);

            foreach (int expectedUnsolvedCellIndex in expectedUnsolvedCellIndices)
            {
                unsolvedCells.Keys.Should().Contain(expectedUnsolvedCellIndex);
                unsolvedCells[expectedUnsolvedCellIndex].Count
                    .Should().Be(cellData[expectedUnsolvedCellIndex].Count);

                foreach(int possibleValue in unsolvedCells[expectedUnsolvedCellIndex])
                {
                    cellData[expectedUnsolvedCellIndex].Should().Contain(possibleValue);
                }
            }
        }

        [Test]
        public void TestUnsolvedValuesWhenNoValuesAreSolved()
        {
            CellData cellData = new CellData
            {
                { 1, new List<int> { 1, 2 } },
                { 2, new List<int> { 2, 3 } },
                { 3, new List<int> { 3, 4 } },
                { 4, new List<int> { 4, 5 } },
                { 5, new List<int> { 5, 6 } },
                { 6, new List<int> { 6, 7 } },
                { 7, new List<int> { 7, 8 } },
                { 8, new List<int> { 8, 9 } },
                { 9, new List<int> { 9, 1 } }
            };

            cellData.GetUnsolvedValues().Count.Should().Be(GridMath.AllPossibleValues().Count);
            var unsolvedValues = cellData.GetUnsolvedValues();

            foreach(int possibleValue in GridMath.AllPossibleValues())
            {
                unsolvedValues.Should().Contain(possibleValue);
            }
        }

        [Test]
        public void TestUnsolvedCellsWhenNoCellsAreSolved()
        {
            CellData cellData = new CellData
            {
                { 1, new List<int> { 1, 2 } },
                { 2, new List<int> { 2, 3 } },
                { 3, new List<int> { 3, 4 } },
                { 4, new List<int> { 4, 5 } },
                { 5, new List<int> { 5, 6 } },
                { 6, new List<int> { 6, 7 } },
                { 7, new List<int> { 7, 8 } },
                { 8, new List<int> { 8, 9 } },
                { 9, new List<int> { 9, 1 } }
            };

            var unsolvedCells = cellData.GetUnsolvedCells();

            unsolvedCells.Count.Should().Be(cellData.Keys.Count);

            foreach (int expectedUnsolvedCellIndex in cellData.Keys)
            {
                unsolvedCells.Keys.Should().Contain(expectedUnsolvedCellIndex);
                unsolvedCells[expectedUnsolvedCellIndex].Count
                    .Should().Be(cellData[expectedUnsolvedCellIndex].Count);

                foreach (int possibleValue in unsolvedCells[expectedUnsolvedCellIndex])
                {
                    cellData[expectedUnsolvedCellIndex].Should().Contain(possibleValue);
                }
            }
        }

        [Test]
        public void TestGetCellsWithValue()
        {
            CellData cellData = new CellData
            {
                { 1, new List<int> { 5 } },
                { 2, new List<int> { 2, 3 } },
                { 3, new List<int> { 3, 7, 9 } },
                { 4, new List<int> { 4, 8 } },
                { 5, new List<int> { 5 } },
                { 6, new List<int> { 6, 9 } },
                { 7, new List<int> { 7, 9 } },
                { 8, new List<int> { 8, 4 } },
                { 9, new List<int> { 9, 2 } }
            };

            cellData.GetCellIndicesWithValue(1).Should().BeEmpty();

            var cellsWithSix = cellData.GetCellIndicesWithValue(6);

            cellsWithSix.Should().HaveCount(1);
            cellsWithSix.First().Should().Be(6);

            var cellsWithNine = cellData.GetCellIndicesWithValue(9);

            cellsWithNine.Should().HaveCount(4);

            cellsWithNine.Should().Contain(3);
            cellsWithNine.Should().Contain(6);
            cellsWithNine.Should().Contain(7);
            cellsWithNine.Should().Contain(9);
        }
    }
}
