using FluentAssertions;
using NUnit.Framework;
using SudokuRulesEngine;
using SudokuRulesEngine.Rules;
using System.Linq;

namespace SudokuRulesEngineTests.Rules
{
    [TestFixture]
    public class SolveForUniqueValuesInARowColumnOrSquareTests
    {
        private Board board;
        private SolveForUniqueValuesInARowColumnOrSquare Rule;

        [SetUp]
        public void Setup()
        {
            board = new Board();
            Rule = new SolveForUniqueValuesInARowColumnOrSquare();
        }

        [Test]
        public void RuleDoesNotApplyIfNoPossibleValuesAreUniqueInAnyRowColumnOrSquare()
        {
            //Remove same value from seven cells in row
            board.RemoveValueFromCell(0, 6);
            board.RemoveValueFromCell(1, 6);
            board.RemoveValueFromCell(2, 6);
            board.RemoveValueFromCell(4, 6);
            board.RemoveValueFromCell(6, 6);
            board.RemoveValueFromCell(7, 6);
            board.RemoveValueFromCell(8, 6);

            //Remove same value from seven cells in column
            board.RemoveValueFromCell(5, 3);
            board.RemoveValueFromCell(14, 3);
            board.RemoveValueFromCell(32, 3);
            board.RemoveValueFromCell(41, 3);
            board.RemoveValueFromCell(59, 3);
            board.RemoveValueFromCell(68, 3);
            board.RemoveValueFromCell(77, 3);

            //Remove same value from seven cells in square
            board.RemoveValueFromCell(32, 1);
            board.RemoveValueFromCell(34, 1);
            board.RemoveValueFromCell(41, 1);
            board.RemoveValueFromCell(42, 1);
            board.RemoveValueFromCell(43, 1);
            board.RemoveValueFromCell(51, 1);
            board.RemoveValueFromCell(52, 1);

            Board oldBoard = new Board(board);

            Rule.ApplyRule(ref board).Should().BeFalse();

            for(int index = 0; index < GridMath.TotalNumberOfCells; index++)
            {
                board.GetCellData()[index].Should().Equal(oldBoard.GetCellData()[index]);
            }
        }

        [Test]
        public void RuleAppliesIfAValueIsUniqueInAnyRow()
        {
            board.RemoveValueFromCell(18, 9);
            board.RemoveValueFromCell(19, 9);
            board.RemoveValueFromCell(21, 9);
            board.RemoveValueFromCell(22, 9);
            board.RemoveValueFromCell(23, 9);
            board.RemoveValueFromCell(24, 9);
            board.RemoveValueFromCell(25, 9);
            board.RemoveValueFromCell(26, 9);

            Rule.ApplyRule(ref board).Should().BeTrue();

            board.GetCellData()[20].Count.Should().Be(1);
            board.GetCellData()[20].First().Should().Be(9);
        }

        [Test]
        public void RuleAppliesIfAValueIsUniqueInAnyColumn()
        {
            board.RemoveValueFromCell(3, 2);
            board.RemoveValueFromCell(12, 2);
            board.RemoveValueFromCell(21, 2);
            board.RemoveValueFromCell(30, 2);
            board.RemoveValueFromCell(39, 2);
            board.RemoveValueFromCell(48, 2);
            board.RemoveValueFromCell(57, 2);
            board.RemoveValueFromCell(66, 2);

            Rule.ApplyRule(ref board).Should().BeTrue();

            board.GetCellData()[75].Count.Should().Be(1);
            board.GetCellData()[75].First().Should().Be(2);
        }

        [Test]
        public void RuleAppliesIfAValueIsUniqueInAnySquare()
        {
            board.RemoveValueFromCell(57, 4);
            board.RemoveValueFromCell(58, 4);
            board.RemoveValueFromCell(66, 4);
            board.RemoveValueFromCell(67, 4);
            board.RemoveValueFromCell(68, 4);
            board.RemoveValueFromCell(75, 4);
            board.RemoveValueFromCell(76, 4);
            board.RemoveValueFromCell(77, 4);

            Rule.ApplyRule(ref board).Should().BeTrue();

            board.GetCellData()[59].Count.Should().Be(1);
            board.GetCellData()[59].First().Should().Be(4);
        }

        [Test]
        public void RuleAppliesIfUniqueValuesExistForRowsColumnsAndSquares()
        {
            board.RemoveValueFromCell(18, 7);
            board.RemoveValueFromCell(19, 7);
            board.RemoveValueFromCell(20, 7);
            board.RemoveValueFromCell(21, 7);
            board.RemoveValueFromCell(23, 7);
            board.RemoveValueFromCell(24, 7);
            board.RemoveValueFromCell(25, 7);
            board.RemoveValueFromCell(26, 7);

            board.RemoveValueFromCell(55, 2);
            board.RemoveValueFromCell(56, 2);
            board.RemoveValueFromCell(57, 2);
            board.RemoveValueFromCell(58, 2);
            board.RemoveValueFromCell(59, 2);
            board.RemoveValueFromCell(60, 2);
            board.RemoveValueFromCell(61, 2);
            board.RemoveValueFromCell(62, 2);

            board.RemoveValueFromCell(7, 5);
            board.RemoveValueFromCell(16, 5);
            board.RemoveValueFromCell(25, 5);
            board.RemoveValueFromCell(43, 5);
            board.RemoveValueFromCell(52, 5);
            board.RemoveValueFromCell(61, 5);
            board.RemoveValueFromCell(70, 5);
            board.RemoveValueFromCell(79, 5);

            board.RemoveValueFromCell(3, 9);
            board.RemoveValueFromCell(4, 9);
            board.RemoveValueFromCell(5, 9);
            board.RemoveValueFromCell(12, 9);
            board.RemoveValueFromCell(13, 9);
            board.RemoveValueFromCell(21, 9);
            board.RemoveValueFromCell(22, 9);
            board.RemoveValueFromCell(23, 9);

            board.RemoveValueFromCell(27, 6);
            board.RemoveValueFromCell(28, 6);
            board.RemoveValueFromCell(29, 6);
            board.RemoveValueFromCell(36, 6);
            board.RemoveValueFromCell(37, 6);
            board.RemoveValueFromCell(38, 6);
            board.RemoveValueFromCell(45, 6);
            board.RemoveValueFromCell(47, 6);
            
            Rule.ApplyRule(ref board).Should().BeTrue();

            board.GetCellData()[22].Count.Should().Be(1);
            board.GetCellData()[22].First().Should().Be(7);

            board.GetCellData()[54].Count.Should().Be(1);
            board.GetCellData()[54].First().Should().Be(2);

            board.GetCellData()[34].Count.Should().Be(1);
            board.GetCellData()[34].First().Should().Be(5);

            board.GetCellData()[14].Count.Should().Be(1);
            board.GetCellData()[14].First().Should().Be(9);

            board.GetCellData()[46].Count.Should().Be(1);
            board.GetCellData()[46].First().Should().Be(6);
        }

    }
}
