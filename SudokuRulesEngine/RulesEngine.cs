using System;
using System.Collections.Generic;

namespace SudokuRulesEngine
{
    public class RulesEngine
    {
        private List<Rule> rules;
        public event EventHandler BoardChanged;
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

            do
            {
                Board newBoard = board;

                foreach (Rule r in rules)
                {
                    r.ApplyRule(ref newBoard);
                }

                if (newBoard.IsSolved())
                {
                    OnBoardChanged(new BoardEventArgs { BoardData = newBoard });
                    OnSolutionComplete(new EventArgs());
                    break;
                }
                else if (newBoard != board)
                {
                    OnBoardChanged(new BoardEventArgs { BoardData = newBoard });
                    board = newBoard;
                }
                else
                {
                    OnSolutionComplete(new EventArgs());
                    break;
                }
            } while (true);
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

        public virtual void OnBoardChanged(BoardEventArgs e)
        {
            EventHandler handler = BoardChanged;
            handler?.Invoke(this, e);
        }

        public virtual void OnSolutionComplete(EventArgs e)
        {
            EventHandler handler = SolutionComplete;
            handler?.Invoke(this, e);
        }
    }
}
