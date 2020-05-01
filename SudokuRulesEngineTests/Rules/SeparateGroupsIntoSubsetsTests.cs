using FluentAssertions;
using NUnit.Framework;
using SudokuRulesEngine;
using SudokuRulesEngine.Rules;
using System.Collections.Generic;

namespace SudokuRulesEngineTests.Rules
{
    [TestFixture]
    public class SeparateGroupsIntoSubsetsTests
    {
        private Board board;
        private TestSeparateGroupsIntoSubsets Rule;

        [SetUp]
        public void Setup()
        {
            board = new Board();
            Rule = new TestSeparateGroupsIntoSubsets();
        }

        [Test]
        public void RuleDoesNotApplyIfNoDistinctGroupsExist()
        {
            board.RemoveValueFromCell(6, 3);
            board.RemoveValueFromCell(6, 4);
            board.RemoveValueFromCell(6, 5);
            board.RemoveValueFromCell(6, 6);
            board.RemoveValueFromCell(6, 7);
            board.RemoveValueFromCell(6, 8);
            board.RemoveValueFromCell(6, 9);

            board.RemoveValueFromCell(15, 4);
            board.RemoveValueFromCell(15, 5);
            board.RemoveValueFromCell(15, 6);
            board.RemoveValueFromCell(15, 7);
            board.RemoveValueFromCell(15, 8);
            board.RemoveValueFromCell(15, 9);
            board.RemoveValueFromCell(15, 1);

            board.RemoveValueFromCell(24, 5);
            board.RemoveValueFromCell(24, 6);
            board.RemoveValueFromCell(24, 7);
            board.RemoveValueFromCell(24, 8);
            board.RemoveValueFromCell(24, 9);
            board.RemoveValueFromCell(24, 1);
            board.RemoveValueFromCell(24, 2);

            board.RemoveValueFromCell(33, 6);
            board.RemoveValueFromCell(33, 7);
            board.RemoveValueFromCell(33, 8);
            board.RemoveValueFromCell(33, 9);
            board.RemoveValueFromCell(33, 1);
            board.RemoveValueFromCell(33, 2);
            board.RemoveValueFromCell(33, 3);

            board.RemoveValueFromCell(42, 7);
            board.RemoveValueFromCell(42, 8);
            board.RemoveValueFromCell(42, 9);
            board.RemoveValueFromCell(42, 1);
            board.RemoveValueFromCell(42, 2);
            board.RemoveValueFromCell(42, 3);
            board.RemoveValueFromCell(42, 4);

            board.RemoveValueFromCell(51, 8);
            board.RemoveValueFromCell(51, 9);
            board.RemoveValueFromCell(51, 1);
            board.RemoveValueFromCell(51, 2);
            board.RemoveValueFromCell(51, 3);
            board.RemoveValueFromCell(51, 4);
            board.RemoveValueFromCell(51, 5);

            board.RemoveValueFromCell(60, 9);
            board.RemoveValueFromCell(60, 1);
            board.RemoveValueFromCell(60, 2);
            board.RemoveValueFromCell(60, 3);
            board.RemoveValueFromCell(60, 4);
            board.RemoveValueFromCell(60, 5);
            board.RemoveValueFromCell(60, 6);

            board.RemoveValueFromCell(69, 1);
            board.RemoveValueFromCell(69, 2);
            board.RemoveValueFromCell(69, 3);
            board.RemoveValueFromCell(69, 4);
            board.RemoveValueFromCell(69, 5);
            board.RemoveValueFromCell(69, 6);
            board.RemoveValueFromCell(69, 7);

            board.RemoveValueFromCell(78, 2);
            board.RemoveValueFromCell(78, 3);
            board.RemoveValueFromCell(78, 4);
            board.RemoveValueFromCell(78, 5);
            board.RemoveValueFromCell(78, 6);
            board.RemoveValueFromCell(78, 7);
            board.RemoveValueFromCell(78, 8);

            Board oldBoard = new Board(board);

            Rule.Solve(2, ref board, board.GetCellDataForColumn(6)).Should().BeFalse();

            for (int index = 0; index < GridMath.TotalNumberOfCells; index++)
            {
                List<int> expectedValues = oldBoard.GetPossibleValues(index);
                List<int> actualValues = board.GetPossibleValues(index);
                expectedValues.Should().Equal(actualValues);
            }
        }

        [Test]
        public void RuleAppliesIfTwoValuesAreConstrainedToTwoCells()
        {
            board.RemoveValueFromCell(27, 7);
            board.RemoveValueFromCell(27, 2);
            board.RemoveValueFromCell(28, 7);
            board.RemoveValueFromCell(28, 2);
            board.RemoveValueFromCell(29, 7);
            board.RemoveValueFromCell(29, 2);
            board.RemoveValueFromCell(36, 7);
            board.RemoveValueFromCell(36, 2);
            board.RemoveValueFromCell(38, 7);
            board.RemoveValueFromCell(38, 2);
            board.RemoveValueFromCell(46, 7);
            board.RemoveValueFromCell(46, 2);
            board.RemoveValueFromCell(47, 7);
            board.RemoveValueFromCell(47, 2);

            Rule.Solve(2, ref board, board.GetCellDataForSquare(3)).Should().BeTrue();

            List<int> valuesFor37 = board.GetPossibleValues(37);
            valuesFor37.Count.Should().Be(2);
            valuesFor37.Should().Contain(2);
            valuesFor37.Should().Contain(7);

            List<int> valuesFor45 = board.GetPossibleValues(45);
            valuesFor45.Count.Should().Be(2);
            valuesFor45.Should().Contain(2);
            valuesFor45.Should().Contain(7);
        }

        [Test]
        public void RuleAppliesIfTwoCellsOnlyHaveTwoValues()
        {
            board.RemoveValueFromCell(74, 1);
            board.RemoveValueFromCell(74, 2);
            board.RemoveValueFromCell(74, 5);
            board.RemoveValueFromCell(74, 6);
            board.RemoveValueFromCell(74, 7);
            board.RemoveValueFromCell(74, 8);
            board.RemoveValueFromCell(74, 9);

            board.RemoveValueFromCell(80, 1);
            board.RemoveValueFromCell(80, 2);
            board.RemoveValueFromCell(80, 5);
            board.RemoveValueFromCell(80, 6);
            board.RemoveValueFromCell(80, 7);
            board.RemoveValueFromCell(80, 8);
            board.RemoveValueFromCell(80, 9);

            Rule.Solve(2, ref board, board.GetCellDataForRow(8)).Should().BeTrue();

            List<int> valuesFor72 = board.GetPossibleValues(72);
            valuesFor72.Count.Should().Be(7);
            valuesFor72.Should().NotContain(3);
            valuesFor72.Should().NotContain(4);

            List<int> valuesFor73 = board.GetPossibleValues(73);
            valuesFor73.Count.Should().Be(7);
            valuesFor73.Should().NotContain(3);
            valuesFor73.Should().NotContain(4);

            List<int> valuesFor75 = board.GetPossibleValues(75);
            valuesFor75.Count.Should().Be(7);
            valuesFor75.Should().NotContain(3);
            valuesFor75.Should().NotContain(4);

            List<int> valuesFor76 = board.GetPossibleValues(76);
            valuesFor76.Count.Should().Be(7);
            valuesFor76.Should().NotContain(3);
            valuesFor76.Should().NotContain(4);

            List<int> valuesFor77 = board.GetPossibleValues(77);
            valuesFor77.Count.Should().Be(7);
            valuesFor77.Should().NotContain(3);
            valuesFor77.Should().NotContain(4);

            List<int> valuesFor78 = board.GetPossibleValues(78);
            valuesFor78.Count.Should().Be(7);
            valuesFor78.Should().NotContain(3);
            valuesFor78.Should().NotContain(4);

            List<int> valuesFor79 = board.GetPossibleValues(79);
            valuesFor79.Count.Should().Be(7);
            valuesFor79.Should().NotContain(3);
            valuesFor79.Should().NotContain(4);
        }

        [Test]
        public void RuleAppliesToMultipleSetsOfValuesInTheSameCells()
        {
            board.RemoveValueFromCell(4, 9);
            board.RemoveValueFromCell(4, 1);
            board.RemoveValueFromCell(13, 9);
            board.RemoveValueFromCell(13, 1);
            board.RemoveValueFromCell(22, 9);
            board.RemoveValueFromCell(22, 1);
            board.RemoveValueFromCell(40, 9);
            board.RemoveValueFromCell(40, 1);
            board.RemoveValueFromCell(58, 9);
            board.RemoveValueFromCell(58, 1);
            board.RemoveValueFromCell(67, 9);
            board.RemoveValueFromCell(67, 1);
            board.RemoveValueFromCell(76, 9);
            board.RemoveValueFromCell(76, 1);

            board.RemoveValueFromCell(13, 5);
            board.RemoveValueFromCell(13, 4);
            board.RemoveValueFromCell(22, 5);
            board.RemoveValueFromCell(22, 4);
            board.RemoveValueFromCell(31, 5);
            board.RemoveValueFromCell(31, 4);
            board.RemoveValueFromCell(49, 5);
            board.RemoveValueFromCell(49, 4);
            board.RemoveValueFromCell(58, 5);
            board.RemoveValueFromCell(58, 4);
            board.RemoveValueFromCell(67, 5);
            board.RemoveValueFromCell(67, 4);
            board.RemoveValueFromCell(76, 5);
            board.RemoveValueFromCell(76, 4);

            Rule.Solve(2, ref board, board.GetCellDataForColumn(4)).Should().BeTrue();

            board.GetPossibleValues(4).Should().Equal(new List<int> { 4, 5 });
            board.GetPossibleValues(13).Should().Equal(new List<int> { 2, 3, 6, 7, 8 });
            board.GetPossibleValues(22).Should().Equal(new List<int> { 2, 3, 6, 7, 8 });
            board.GetPossibleValues(31).Should().Equal(new List<int> { 1, 9 });
            board.GetPossibleValues(40).Should().Equal(new List<int> { 4, 5 });
            board.GetPossibleValues(49).Should().Equal(new List<int> { 1, 9 });
            board.GetPossibleValues(58).Should().Equal(new List<int> { 2, 3, 6, 7, 8 });
            board.GetPossibleValues(67).Should().Equal(new List<int> { 2, 3, 6, 7, 8 });
            board.GetPossibleValues(76).Should().Equal(new List<int> { 2, 3, 6, 7, 8 });
        }

        [Test]
        public void RuleAppliesToMultipleSetsOfCellsWithTheSameValues()
        {
            board.RemoveValueFromCell(4, 1);
            board.RemoveValueFromCell(4, 3);
            board.RemoveValueFromCell(4, 4);
            board.RemoveValueFromCell(4, 5);
            board.RemoveValueFromCell(4, 6);
            board.RemoveValueFromCell(4, 8);
            board.RemoveValueFromCell(4, 9);

            board.RemoveValueFromCell(22, 1);
            board.RemoveValueFromCell(22, 3);
            board.RemoveValueFromCell(22, 4);
            board.RemoveValueFromCell(22, 5);
            board.RemoveValueFromCell(22, 6);
            board.RemoveValueFromCell(22, 8);
            board.RemoveValueFromCell(22, 9);

            board.RemoveValueFromCell(13, 1);
            board.RemoveValueFromCell(13, 2);
            board.RemoveValueFromCell(13, 3);
            board.RemoveValueFromCell(13, 4);
            board.RemoveValueFromCell(13, 5);
            board.RemoveValueFromCell(13, 7);
            board.RemoveValueFromCell(13, 8);

            board.RemoveValueFromCell(14, 1);
            board.RemoveValueFromCell(14, 2);
            board.RemoveValueFromCell(14, 3);
            board.RemoveValueFromCell(14, 4);
            board.RemoveValueFromCell(14, 5);
            board.RemoveValueFromCell(14, 7);
            board.RemoveValueFromCell(14, 8);

            Rule.Solve(2, ref board, board.GetCellDataForSquare(1)).Should().BeTrue();

            board.GetPossibleValues(3).Should().Equal(new List<int> { 1, 3, 4, 5, 8 });
            board.GetPossibleValues(4).Should().Equal(new List<int> { 2, 7 });
            board.GetPossibleValues(5).Should().Equal(new List<int> { 1, 3, 4, 5, 8 });
            board.GetPossibleValues(12).Should().Equal(new List<int> { 1, 3, 4, 5, 8 });
            board.GetPossibleValues(13).Should().Equal(new List<int> { 6, 9 });
            board.GetPossibleValues(14).Should().Equal(new List<int> { 6, 9 });
            board.GetPossibleValues(21).Should().Equal(new List<int> { 1, 3, 4, 5, 8 });
            board.GetPossibleValues(22).Should().Equal(new List<int> { 2, 7 });
            board.GetPossibleValues(23).Should().Equal(new List<int> { 1, 3, 4, 5, 8 });
        }

        [Test]
        public void RuleAppliesToEverySetOfCellsOnTheBoard()
        {
            board.RemoveValueFromCell(21, 1);
            board.RemoveValueFromCell(21, 2);
            board.RemoveValueFromCell(21, 3);
            board.RemoveValueFromCell(21, 4);
            board.RemoveValueFromCell(21, 5);
            board.RemoveValueFromCell(21, 6);
            board.RemoveValueFromCell(21, 7);

            board.RemoveValueFromCell(48, 1);
            board.RemoveValueFromCell(48, 2);
            board.RemoveValueFromCell(48, 3);
            board.RemoveValueFromCell(48, 4);
            board.RemoveValueFromCell(48, 5);
            board.RemoveValueFromCell(48, 6);
            board.RemoveValueFromCell(48, 7);

            board.RemoveValueFromCell(36, 2);
            board.RemoveValueFromCell(36, 6);
            board.RemoveValueFromCell(37, 2);
            board.RemoveValueFromCell(37, 6);
            board.RemoveValueFromCell(39, 2);
            board.RemoveValueFromCell(39, 6);
            board.RemoveValueFromCell(40, 2);
            board.RemoveValueFromCell(40, 6);
            board.RemoveValueFromCell(41, 2);
            board.RemoveValueFromCell(41, 6);
            board.RemoveValueFromCell(43, 2);
            board.RemoveValueFromCell(43, 6);
            board.RemoveValueFromCell(44, 2);
            board.RemoveValueFromCell(44, 6);

            Rule.ApplyRule(ref board).Should().BeTrue();

            board.GetPossibleValues(3).Should().Equal(new List<int> { 1, 2, 3, 4, 5, 6, 7 });
            board.GetPossibleValues(12).Should().Equal(new List<int> { 1, 2, 3, 4, 5, 6, 7 });
            board.GetPossibleValues(30).Should().Equal(new List<int> { 1, 2, 3, 4, 5, 6, 7 });
            board.GetPossibleValues(39).Should().Equal(new List<int> { 1, 3, 4, 5, 7 });
            board.GetPossibleValues(57).Should().Equal(new List<int> { 1, 2, 3, 4, 5, 6, 7 });
            board.GetPossibleValues(66).Should().Equal(new List<int> { 1, 2, 3, 4, 5, 6, 7 });
            board.GetPossibleValues(75).Should().Equal(new List<int> { 1, 2, 3, 4, 5, 6, 7 });

            board.GetPossibleValues(38).Should().Equal(new List<int> { 2 ,6 });
            board.GetPossibleValues(42).Should().Equal(new List<int> { 2, 6 });
        }

        [Test]
        public void RuleAppliesIfThreeValuesAreContainedInThreeCells()
        {
            board.RemoveValueFromCell(9, 1);
            board.RemoveValueFromCell(9, 3);
            board.RemoveValueFromCell(9, 5);
            board.RemoveValueFromCell(11, 1);
            board.RemoveValueFromCell(11, 3);
            board.RemoveValueFromCell(11, 5);
            board.RemoveValueFromCell(12, 1);
            board.RemoveValueFromCell(12, 3);
            board.RemoveValueFromCell(12, 5);
            board.RemoveValueFromCell(13, 1);
            board.RemoveValueFromCell(13, 3);
            board.RemoveValueFromCell(13, 5);
            board.RemoveValueFromCell(15, 1);
            board.RemoveValueFromCell(15, 3);
            board.RemoveValueFromCell(15, 5);
            board.RemoveValueFromCell(16, 1);
            board.RemoveValueFromCell(16, 3);
            board.RemoveValueFromCell(16, 5);

            Rule.Solve(3, ref board, board.GetCellDataForRow(1)).Should().BeTrue();

            board.GetPossibleValues(10).Should().Equal(new List<int> { 1, 3, 5 });
            board.GetPossibleValues(14).Should().Equal(new List<int> { 1, 3, 5 });
            board.GetPossibleValues(17).Should().Equal(new List<int> { 1, 3, 5 });
        }

        [Test]
        public void RuleAppliesIfThreeValuesAreContainedInOnlyThreeCells()
        {

        }

        [Test]
        public void RuleAppliesIfFourValuesAreContainedInFourCells()
        {

        }

        [Test]
        public void RuleAppliesIfFourValuesAreContainedInOnlyFourCells()
        {

        }

        [Test]
        public void RuleAppliesIfFiveValuesAreContainedInFiveCells()
        {

        }

        [Test]
        public void RuleAppliesIfFiveValuesAreContainedInOnlyFiveCells()
        {

        }

        [Test]
        public void RuleDoesNotApplyIfSubgroupsAreAlreadySeparated()
        {

        }
    }

    public class TestSeparateGroupsIntoSubsets : SeparateGroupsIntoSubsets
    {
        public bool Solve(int groupSize, ref Board board, CellData cellData)
        {
            return SolveForCells(groupSize, ref board, cellData);
        }
    }
}
