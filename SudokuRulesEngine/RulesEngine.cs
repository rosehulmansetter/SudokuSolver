using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuRulesEngine
{
    public class RulesEngine
    {
        private List<Rule> rules;
        public event EventHandler SolutionComplete;

        public RulesEngine()
        {
            rules = new List<Rule>();
        }

        public void AddRule(Rule rule)
        {
            rules.Add(rule);
        }

        public void Solve(Dictionary<int, int> solvedCells)
        {
            Board board = InitializeBoard(solvedCells);

            int numberOfSolutions = SolveInternal(ref board);

            OnSolutionComplete(new BoardEventArgs { BoardData = board, TotalSolutions = numberOfSolutions });
        }

        private int SolveInternal(ref Board board)
        {
            bool rulesFoundNewInfo = false;

            do
            {
                rulesFoundNewInfo = false;
                var enumerator = rules.GetEnumerator();

                while (!rulesFoundNewInfo && enumerator.MoveNext())
                {
                    rulesFoundNewInfo = enumerator.Current.ApplyRule(ref board);
                }

            } while (!board.IsSolved() && rulesFoundNewInfo);

            if (board.IsSolved())
            {
                return 1;
            }
            else if (board.GetCellData().All(c => c.Count > 0))
            {
                int numberOfSolutions = 0;
                List<List<int>> boardData = board.GetCellData();
                int firstUnsolvedCellIndex = 0;
                while (boardData[firstUnsolvedCellIndex].Count == 1)
                {
                    firstUnsolvedCellIndex++;
                }

                List<int> possibleValues = boardData[firstUnsolvedCellIndex];

                foreach (int value in possibleValues)
                {
                    Board newBoard = new Board(board);
                    newBoard.SetCell(firstUnsolvedCellIndex, value);
                    int solutions = SolveInternal(ref newBoard);
                    if(solutions == 1)
                    {
                        board = new Board(newBoard);
                    }
                    numberOfSolutions += solutions;
                }

                return numberOfSolutions;
            }
            else
            {
                return 0;
            }
        }

        private Board InitializeBoard(Dictionary<int, int> solvedCells)
        {
            Board board = new Board();

            foreach(int index in solvedCells.Keys)
            {
                board.SetCell(index, solvedCells[index]);
            }

            return board;
        }

        public virtual void OnSolutionComplete(BoardEventArgs e)
        {
            EventHandler handler = SolutionComplete;
            handler?.Invoke(this, e);
        }
    }
}
