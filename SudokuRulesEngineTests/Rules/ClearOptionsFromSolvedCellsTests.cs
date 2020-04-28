using FluentAssertions;
using NUnit.Framework;
using SudokuRulesEngine;
using SudokuRulesEngine.Rules;
using System.Collections.Generic;

namespace SudokuRulesEngineTests.Rules
{
    [TestFixture]
    public class ClearOptionsFromSolvedCellsTests
    {
        private Board board;
        private ClearOptionsFromSolvedCells Rule;

        [SetUp]
        public void Setup()
        {
            board = new Board();
            Rule = new ClearOptionsFromSolvedCells();
        }

        [Test]
        public void RuleDoesNothingWhenNoCellsAreSolved()
        {
            board.RemoveValueFromCell(20, 5);
            board.RemoveValueFromCell(35, 6);
            board.RemoveValueFromCell(35, 1);

            Board oldBoard = new Board(board);

            Rule.ApplyRule(ref board);

            for(int index = 0; index < GridMath.TotalNumberOfCells; index++)
            {
                List<int> expectedValues = oldBoard.GetPossibleValues(index);
                List<int> actualValues = board.GetPossibleValues(index);
                expectedValues.Should().Equal(actualValues);
            }
        }

        [Test]
        public void RuleRemovesValuesFromCellsWhenCellsAreSolved()
        {
            board.SetCell(48, 1);
            board.SetCell(9, 7);

            Rule.ApplyRule(ref board);

            List<int> relatedIndicesTo48 = new List<int> { 3, 12, 21, 30, 39, 57, 66, 75, 45, 46, 47, 49, 50, 51, 52, 53, 31, 32, 40, 41 };
            
            foreach(int index in relatedIndicesTo48)
            {
                board.GetPossibleValues(index).Should().NotContain(1);
            }

            List<int> relatedIndicesTo9 = new List<int> { 0, 18, 27, 36, 45, 54, 63, 72, 10, 11, 12, 13, 14, 15, 16, 17, 1, 2, 19, 20 };

            foreach (int index in relatedIndicesTo9)
            {
                board.GetPossibleValues(index).Should().NotContain(7);
            }
        }

        [Test]
        public void RuleDoesNotApplyWhenNoNewCellsAreSolved()
        {
            board.SetCell(20, 5);
            board.SetCell(35, 6);
            board.SetCell(35, 1);

            Rule.ApplyRule(ref board).Should().BeTrue();
            Rule.ApplyRule(ref board).Should().BeFalse();
        }

    }
}
