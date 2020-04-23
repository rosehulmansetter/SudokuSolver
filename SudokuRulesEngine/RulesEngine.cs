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

            List<Board> solutions = SolveInternal(ref board);

            OnSolutionComplete(new BoardEventArgs { Boards = solutions });
        }

        private List<Board> SolveInternal(ref Board board)
        {
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
            else if (board.GetCellData().All(c => c.Count > 0))
            {
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
