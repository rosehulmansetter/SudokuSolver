using FluentAssertions;
using NUnit.Framework;
using SudokuRulesEngine;
using SudokuRulesEngine.Rules;
using System.Collections.Generic;

namespace SudokuRulesEngineTests.Rules
{
    [TestFixture]
    public class CheckForLinearValuesInSquaresTests
    {
        private Board board;
        private CheckForLinearValuesInSquares Rule;

        [SetUp]
        public void Setup()
        {
            board = new Board();
            Rule = new CheckForLinearValuesInSquares();
        }

        [Test]
        public void RuleDoesNotApplyIfNoPossibleValuesAreColinear()
        {
            board.RemoveValueFromCell(0, 1);
            board.RemoveValueFromCell(1, 1);
            board.RemoveValueFromCell(9, 1);
            board.RemoveValueFromCell(11, 1);
            board.RemoveValueFromCell(18, 1);
            board.RemoveValueFromCell(19, 1);
            board.RemoveValueFromCell(20, 1);

            board.RemoveValueFromCell(34, 6);
            board.RemoveValueFromCell(35, 6);
            board.RemoveValueFromCell(42, 6);
            board.RemoveValueFromCell(43, 6);
            board.RemoveValueFromCell(44, 6);
            board.RemoveValueFromCell(51, 6);
            board.RemoveValueFromCell(52, 6);

            Board oldBoard = new Board(board);

            Rule.ApplyRule(ref board).Should().BeFalse();

            for(int index = 0; index < GridMath.TotalNumberOfCells; index++)
            {
                List<int> expectedValues = oldBoard.GetPossibleValues(index);
                List<int> actualValues = board.GetPossibleValues(index);
                expectedValues.Should().Equal(actualValues);
            }
        }

        [Test]
        public void RuleAppliesIfAllPossibleValuesInASquareAreInTheSameRow()
        {
            board.RemoveValueFromCell(6, 3);
            board.RemoveValueFromCell(7, 3);
            board.RemoveValueFromCell(8, 3);
            board.RemoveValueFromCell(24, 3);
            board.RemoveValueFromCell(25, 3);
            board.RemoveValueFromCell(26, 3);

            board.RemoveValueFromCell(57, 8);
            board.RemoveValueFromCell(66, 8);
            board.RemoveValueFromCell(67, 8);
            board.RemoveValueFromCell(68, 8);
            board.RemoveValueFromCell(75, 8);
            board.RemoveValueFromCell(76, 8);
            board.RemoveValueFromCell(77, 8);

            Rule.ApplyRule(ref board).Should().BeTrue();
            
            board.GetPossibleValues(9).Should().NotContain(3);
            board.GetPossibleValues(10).Should().NotContain(3);
            board.GetPossibleValues(11).Should().NotContain(3);
            board.GetPossibleValues(12).Should().NotContain(3);
            board.GetPossibleValues(13).Should().NotContain(3);
            board.GetPossibleValues(14).Should().NotContain(3);

            board.GetPossibleValues(54).Should().NotContain(8);
            board.GetPossibleValues(55).Should().NotContain(8);
            board.GetPossibleValues(56).Should().NotContain(8);
            board.GetPossibleValues(60).Should().NotContain(8);
            board.GetPossibleValues(61).Should().NotContain(8);
            board.GetPossibleValues(62).Should().NotContain(8);
        }

        [Test]
        public void RuleAppliesIfAllPossibleValuesInASquareAreInTheSameColumn()
        {
            board.RemoveValueFromCell(54, 4);
            board.RemoveValueFromCell(55, 4);
            board.RemoveValueFromCell(63, 4);
            board.RemoveValueFromCell(64, 4);
            board.RemoveValueFromCell(65, 4);
            board.RemoveValueFromCell(72, 4);
            board.RemoveValueFromCell(73, 4);

            board.RemoveValueFromCell(6, 2);
            board.RemoveValueFromCell(7, 2);
            board.RemoveValueFromCell(15, 2);
            board.RemoveValueFromCell(16, 2);
            board.RemoveValueFromCell(24, 2);
            board.RemoveValueFromCell(25, 2);

            Rule.ApplyRule(ref board).Should().BeTrue();

            board.GetPossibleValues(2).Should().NotContain(4);
            board.GetPossibleValues(11).Should().NotContain(4);
            board.GetPossibleValues(20).Should().NotContain(4);
            board.GetPossibleValues(29).Should().NotContain(4);
            board.GetPossibleValues(38).Should().NotContain(4);
            board.GetPossibleValues(47).Should().NotContain(4);

            board.GetPossibleValues(35).Should().NotContain(2);
            board.GetPossibleValues(44).Should().NotContain(2);
            board.GetPossibleValues(53).Should().NotContain(2);
            board.GetPossibleValues(62).Should().NotContain(2);
            board.GetPossibleValues(71).Should().NotContain(2);
            board.GetPossibleValues(80).Should().NotContain(2);
        }

        [Test]
        public void RuleDoesNotApplyIfSquaresHaveSetsOfValuesInTheSameRowAndColumn()
        {
            board.RemoveValueFromCell(30, 9);
            board.RemoveValueFromCell(31, 9);
            board.RemoveValueFromCell(32, 9);
            board.RemoveValueFromCell(39, 9);
            board.RemoveValueFromCell(41, 9);
            board.RemoveValueFromCell(48, 9);
            board.RemoveValueFromCell(50, 9);

            board.RemoveValueFromCell(30, 6);
            board.RemoveValueFromCell(31, 6);
            board.RemoveValueFromCell(32, 6);
            board.RemoveValueFromCell(39, 6);
            board.RemoveValueFromCell(40, 6);
            board.RemoveValueFromCell(41, 6);
            board.RemoveValueFromCell(50, 6);

            Rule.ApplyRule(ref board).Should().BeTrue();

            board.GetPossibleValues(4).Should().NotContain(9);
            board.GetPossibleValues(13).Should().NotContain(9);
            board.GetPossibleValues(22).Should().NotContain(9);
            board.GetPossibleValues(58).Should().NotContain(9);
            board.GetPossibleValues(67).Should().NotContain(9);
            board.GetPossibleValues(76).Should().NotContain(9);

            board.GetPossibleValues(45).Should().NotContain(6);
            board.GetPossibleValues(46).Should().NotContain(6);
            board.GetPossibleValues(47).Should().NotContain(6);
            board.GetPossibleValues(51).Should().NotContain(6);
            board.GetPossibleValues(52).Should().NotContain(6);
            board.GetPossibleValues(53).Should().NotContain(6);
        }

        [Test]
        public void RuleDoesNotApplyRuleHasAlreadyBeenApplied()
        {
            board.RemoveValueFromCell(6, 3);
            board.RemoveValueFromCell(7, 3);
            board.RemoveValueFromCell(8, 3);
            board.RemoveValueFromCell(24, 3);
            board.RemoveValueFromCell(25, 3);
            board.RemoveValueFromCell(26, 3);

            board.RemoveValueFromCell(57, 8);
            board.RemoveValueFromCell(66, 8);
            board.RemoveValueFromCell(67, 8);
            board.RemoveValueFromCell(68, 8);
            board.RemoveValueFromCell(75, 8);
            board.RemoveValueFromCell(76, 8);
            board.RemoveValueFromCell(77, 8);

            Rule.ApplyRule(ref board).Should().BeTrue();
            Rule.ApplyRule(ref board).Should().BeFalse();
        }
    }
}
