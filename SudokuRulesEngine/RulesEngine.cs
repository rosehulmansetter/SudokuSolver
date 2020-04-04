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
                bool rulesFoundNewInfo = false;
                var enumerator = rules.GetEnumerator();

                while(!rulesFoundNewInfo && enumerator.MoveNext())
                {
                    rulesFoundNewInfo = enumerator.Current.ApplyRule(ref board);
                }

                if (board.IsSolved())
                {
                    OnBoardChanged(new BoardEventArgs { BoardData = board });
                    OnSolutionComplete(new EventArgs());
                    break;
                }
                else if (rulesFoundNewInfo)
                {
                    OnBoardChanged(new BoardEventArgs { BoardData = board });
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
