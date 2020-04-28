using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuRulesEngine
{
    public class RulesEngine
    {
        private static bool Cancelled;
        private List<Rule> rules;
        public event EventHandler SolutionComplete;

        public RulesEngine()
        {
            rules = new List<Rule>();
        }

        public void Cancel()
        {
            Cancelled = true;
        }

        public void AddRule(Rule rule)
        {
            rules.Add(rule);
        }

        public void Solve(Dictionary<int, int> solvedCells)
        {
            Cancelled = false;
            Board board = InitializeBoard(solvedCells);

            List<Board> solutions = SolveInternal(ref board);

            if (!Cancelled)
            {
                OnSolutionComplete(new BoardEventArgs { Boards = solutions });
            }
        }

        private List<Board> SolveInternal(ref Board board)
        {
            if(Cancelled)
            {
                return new List<Board>();
            }

            List<Board> solutions = new List<Board>();
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
                solutions.Add(board);
                return solutions;
            }
            else if (board.IsValid())
            {
                int firstUnsolvedCellIndex = 0;
                while (board.GetPossibleValues(firstUnsolvedCellIndex).Count == 1)
                {
                    firstUnsolvedCellIndex++;
                }

                List<int> possibleValues = board.GetPossibleValues(firstUnsolvedCellIndex);

                foreach (int value in possibleValues)
                {
                    Board newBoard = new Board(board);
                    newBoard.SetCell(firstUnsolvedCellIndex, value);
                    solutions.AddRange(SolveInternal(ref newBoard));
                }

                return solutions;
            }
            else
            {
                return new List<Board>();
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
