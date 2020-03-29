using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SudokuRulesEngine;
using SudokuRulesEngine.Rules;

namespace SudokuSolver
{
    public partial class MainWindow : Window
    {
        private const int SQUARE_SIZE = 3;
        private CellGrid cellGrid;
        private RulesEngine rulesEngine;
        bool solving;

        public MainWindow()
        {
            InitializeComponent();
            cellGrid = new CellGrid();
            InitializeRulesEngine();
            solving = false;
            SetUpSquares();
            SetButtonsToEditMode();
        }

        private void InitializeRulesEngine()
        {
            rulesEngine = new RulesEngine();
            rulesEngine.AddRule(new ClearOptionsFromSolvedCells());
            rulesEngine.AddRule(new SolveForUniqueValuesInARowColumnOrSquare());
            rulesEngine.BoardChanged += HandleBoardChanged;
            rulesEngine.SolutionComplete += HandleSolutionComplete;
        }

        private void SetUpSquares()
        {
            SetUpSquare(0, 0, TopLeftGrid);
            SetUpSquare(0, 1, TopMiddleGrid);
            SetUpSquare(0, 2, TopRightGrid);
            SetUpSquare(1, 0, MiddleLeftGrid);
            SetUpSquare(1, 1, MiddleMiddleGrid);
            SetUpSquare(1, 2, MiddleRightGrid);
            SetUpSquare(2, 0, BottomLeftGrid);
            SetUpSquare(2, 1, BottomMiddleGrid);
            SetUpSquare(2, 2, BottomRightGrid);
        }

        private void SetUpSquare(int gridRowIndex, int gridColumnIndex, Grid grid)
        {
            for (int row = 0; row < SQUARE_SIZE; row++)
            {
                for(int column = 0; column < SQUARE_SIZE; column++)
                {
                    Cell c = cellGrid.GetCellByIndices(3 * gridRowIndex + row, 3 * gridColumnIndex + column);

                    Grid.SetRow(c, row);
                    Grid.SetColumn(c, column);

                    double bottomBorderThickness = row < 2 ? 0.5 : 0;
                    double rightBorderThickness = column < 2 ? 0.5 : 0;

                    c.BorderThickness = new Thickness(0, 0, rightBorderThickness, bottomBorderThickness);
                    c.BorderBrush = Brushes.Gray;

                    grid.Children.Add(c);
                }
            }
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            if(!solving)
            {
                if (e.Key >= Key.D1 && e.Key <= Key.D9)
                {
                    int numberPressed = e.Key - Key.D0;
                    cellGrid.EnterNumber(numberPressed);
                }
                else if (e.Key == Key.Space)
                {
                    cellGrid.MoveToNext();
                }
                else if (e.Key == Key.Right)
                {
                    cellGrid.MoveRight();
                }
                else if (e.Key == Key.Left)
                {
                    cellGrid.MoveLeft();
                }
                else if (e.Key == Key.Up)
                {
                    cellGrid.MoveUp();
                }
                else if (e.Key == Key.Down)
                {
                    cellGrid.MoveDown();
                }
                else if (e.Key == Key.Back)
                {
                    cellGrid.ClearSelectedCell();
                }
            }
        }

        private void SolvePuzzle(object sender, RoutedEventArgs e)
        {
            solving = true;
            SolveButton.IsEnabled = false;
            EditButton.IsEnabled = false;
            ResetButton.IsEnabled = false;
            cellGrid.UnselectSelectedCell();
            cellGrid.SetHintsVisible(true);
            rulesEngine.Solve(cellGrid.GetSolvedCellData());
        }

        private void EditPuzzle(object sender, RoutedEventArgs e)
        {
            SetButtonsToEditMode();
            cellGrid.SetHintsVisible(false);
            cellGrid.ResetSelectedCell();
        }

        private void ResetPuzzle(object sender, RoutedEventArgs e)
        {
            SetButtonsToEditMode();
            cellGrid.SetHintsVisible(false);
            cellGrid.ResetSelectedCell();
            cellGrid.ClearAllCellValues();
        }

        private void SetButtonsToEditMode()
        {
            SolveButton.IsEnabled = true;
            EditButton.IsEnabled = false;
            ResetButton.IsEnabled = true;
        }

        void HandleBoardChanged(object sender, EventArgs boardArgs)
        {
            Board board = ((BoardEventArgs)boardArgs).BoardData;
            cellGrid.UpdateBoard(board.GetCellData());
        }

        void HandleSolutionComplete(object sender, EventArgs args)
        {
            SolveButton.IsEnabled = true;
            EditButton.IsEnabled = true;
            ResetButton.IsEnabled = true;
            solving = false;
        }
    }
}
