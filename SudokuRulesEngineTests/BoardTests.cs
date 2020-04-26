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

            List<List<int>> boardData = board.GetCellData();
            List<List<int>> newBoardData = newBoard.GetCellData();

            for(int index = 0; index < GridMath.TotalNumberOfCells; index++)
            {
                boardData[index].Count.Should().Be(newBoardData[index].Count);

                foreach(int value in boardData[index])
                {
                    newBoardData[index].Should().Contain(value);
                }
            }

            newBoard.SetCell(0, 2);

            newBoard.GetCellData()[0].Should().NotEqual(board.GetCellData()[0]);
        }

        [Test]
        public void TestSetCell()
        {
            board.SetCell(7, 1);

            var cellData = board.GetCellData();

            cellData[7].Count.Should().Be(1);
            cellData[7].First().Should().Be(1);

            board.SetCell(7, 7);

            cellData = board.GetCellData();

            cellData[7].Count.Should().Be(1);
            cellData[7].First().Should().Be(7);

            board.SetCell(56, 3);

            cellData[56].Count.Should().Be(1);
            cellData[56].First().Should().Be(3);
        }

        [Test]
        public void TestRemoveValueFromCell()
        {
            int index = 70;

            var possibleValues = board.GetCellData()[index];

            possibleValues.Count.Should().Be(9);

            board.RemoveValueFromCell(index, 2);

            possibleValues = board.GetCellData()[index];

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
        public void TestIsSolved()
        {
            for(int i = 0; i < GridMath.TotalNumberOfCells; i++)
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
    }
}
