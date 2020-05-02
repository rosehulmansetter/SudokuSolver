using FluentAssertions;
using NUnit.Framework;
using SudokuRulesEngine;
using System.Collections.Generic;
using System.Linq;

namespace SudokuRulesEngineTests
{
    [TestFixture]
    public class BoardTests
    {
        private Board board;

        [SetUp]
        public void Setup()
        {
            board = new Board();
        }

        [Test]
        public void TestCopyConstructor()
        {
            board.SetCell(7, 1);
            board.SetCell(18, 6);
            board.SetCell(64, 9);
            board.SetCell(77, 3);

            Board newBoard = new Board(board);

            newBoard.Should().NotBe(board);

            for(int index = 0; index < GridMath.TotalNumberOfCells; index++)
            {
                board.GetPossibleValues(index).Should().Equal(newBoard.GetPossibleValues(index));
            }

            newBoard.SetCell(0, 2);

            newBoard.GetPossibleValues(0).Should().NotEqual(board.GetPossibleValues(0));
        }

        [Test]
        public void TestSetCell()
        {
            board.SetCell(7, 1);

            board.GetPossibleValues(7).Count.Should().Be(1);
            board.GetPossibleValues(7).First().Should().Be(1);

            board.SetCell(7, 7);

            board.GetPossibleValues(7).Count.Should().Be(1);
            board.GetPossibleValues(7).First().Should().Be(7);

            board.SetCell(56, 3);

            board.GetPossibleValues(56).Count.Should().Be(1);
            board.GetPossibleValues(56).First().Should().Be(3);
        }

        [Test]
        public void TestRemoveValueFromCell()
        {
            int index = 70;

            var possibleValues = board.GetPossibleValues(index);

            possibleValues.Count.Should().Be(9);

            board.RemoveValueFromCell(index, 2);

            possibleValues = board.GetPossibleValues(index);

            possibleValues.Count.Should().Be(8);
            possibleValues.Should().NotContain(2);

            board.RemoveValueFromCell(index, 2);

            possibleValues.Count.Should().Be(8);
            possibleValues.Should().NotContain(2);

            board.RemoveValueFromCell(index, 5);

            possibleValues.Count.Should().Be(7);
            possibleValues.Should().NotContain(2);
            possibleValues.Should().NotContain(5);
        }

        [Test]
        public void TestIsValid()
        {
            board.IsValid().Should().BeTrue();

            board.SetCell(7, 3);
            board.IsValid().Should().BeTrue();

            board.RemoveValueFromCell(7, 3);
            board.IsValid().Should().BeFalse();
        }

        [Test]
        public void TestIsSolved()
        {
            for (int i = 0; i < GridMath.TotalNumberOfCells; i++)
            {
                board.IsSolved().Should().BeFalse();
                board.SetCell(i, 1);
            }

            board.IsSolved().Should().BeTrue();
        }

        [Test]
        public void TestGetCellDataForRow()
        {
            board.SetCell(27, 4);
            board.RemoveValueFromCell(29, 8);
            board.SetCell(31, 7);
            board.SetCell(32, 1);
            board.RemoveValueFromCell(35, 5);

            CellData cellData = board.GetCellDataForRow(3);

            cellData[27].Count.Should().Be(1);
            cellData[27].Should().Contain(4);

            cellData[28].Count.Should().Be(9);

            cellData[29].Count.Should().Be(8);
            cellData[29].Should().NotContain(8);

            cellData[30].Count.Should().Be(9);

            cellData[31].Count.Should().Be(1);
            cellData[31].Should().Contain(7);

            cellData[32].Count.Should().Be(1);
            cellData[32].Should().Contain(1);

            cellData[33].Count.Should().Be(9);

            cellData[34].Count.Should().Be(9);

            cellData[35].Count.Should().Be(8);
            cellData[35].Should().NotContain(5);
        }

        [Test]
        public void TestGetCellDataForColumn()
        {
            board.RemoveValueFromCell(6, 9);
            board.RemoveValueFromCell(15, 8);
            board.RemoveValueFromCell(42, 7);
            board.SetCell(51, 1);
            board.RemoveValueFromCell(78, 5);

            CellData cellData = board.GetCellDataForColumn(6);

            cellData[6].Count.Should().Be(8);
            cellData[6].Should().NotContain(9);

            cellData[15].Count.Should().Be(8);
            cellData[15].Should().NotContain(8);

            cellData[24].Count.Should().Be(9);

            cellData[33].Count.Should().Be(9);

            cellData[42].Count.Should().Be(8);
            cellData[42].Should().NotContain(7);

            cellData[51].Count.Should().Be(1);
            cellData[51].Should().Contain(1);

            cellData[60].Count.Should().Be(9);

            cellData[69].Count.Should().Be(9);

            cellData[78].Count.Should().Be(8);
            cellData[78].Should().NotContain(5);
        }

        [Test]
        public void TestGetCellDataForSquare()
        {
            board.RemoveValueFromCell(57, 1);
            board.RemoveValueFromCell(59, 6);
            board.SetCell(66, 1);
            board.SetCell(67, 4);
            board.SetCell(76, 6);

            CellData cellData = board.GetCellDataForSquare(7);

            cellData[57].Count.Should().Be(8);
            cellData[57].Should().NotContain(1);

            cellData[58].Count.Should().Be(9);

            cellData[59].Count.Should().Be(8);
            cellData[59].Should().NotContain(6);

            cellData[66].Count.Should().Be(1);
            cellData[66].Should().Contain(1);

            cellData[67].Count.Should().Be(1);
            cellData[67].Should().Contain(4);

            cellData[68].Count.Should().Be(9);

            cellData[75].Count.Should().Be(9);

            cellData[76].Count.Should().Be(1);
            cellData[76].Should().Contain(6);

            cellData[77].Count.Should().Be(9);
        }

        [Test]
        public void TestGetPossibleValues()
        {
            board.RemoveValueFromCell(44, 1);
            board.RemoveValueFromCell(44, 3);
            board.RemoveValueFromCell(44, 7);
            board.RemoveValueFromCell(11, 6);
            board.SetCell(2, 4);
            board.SetCell(76, 6);

            board.GetPossibleValues(44).Should().Equal(new List<int> { 2, 4, 5, 6, 8, 9 });
            board.GetPossibleValues(11).Should().Equal(new List<int> { 1, 2, 3, 4, 5, 7, 8, 9 });
            board.GetPossibleValues(2).Should().Equal(new List<int> { 4 });
            board.GetPossibleValues(76).Should().Equal(new List<int> { 6 });
            board.GetPossibleValues(55).Should().Equal(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        }

        [Test]
        public void TestIsCellSolved()
        {
            board.IsCellSolved(55).Should().BeFalse();

            board.RemoveValueFromCell(55, 6);
            board.IsCellSolved(55).Should().BeFalse();

            board.RemoveValueFromCell(55, 3);
            board.IsCellSolved(55).Should().BeFalse();

            board.RemoveValueFromCell(55, 8);
            board.IsCellSolved(55).Should().BeFalse();

            board.RemoveValueFromCell(55, 4);
            board.IsCellSolved(55).Should().BeFalse();

            board.RemoveValueFromCell(55, 1);
            board.IsCellSolved(55).Should().BeFalse();

            board.RemoveValueFromCell(55, 5);
            board.IsCellSolved(55).Should().BeFalse();

            board.RemoveValueFromCell(55, 2);
            board.IsCellSolved(55).Should().BeFalse();

            board.RemoveValueFromCell(55, 7);
            board.IsCellSolved(55).Should().BeTrue();

            board.RemoveValueFromCell(55, 9);
            board.IsCellSolved(55).Should().BeFalse();

            board.IsCellSolved(60).Should().BeFalse();
            board.SetCell(60, 1);

            board.IsCellSolved(60).Should().BeTrue();
        }
    }
}
