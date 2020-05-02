using System;
using System.Collections.Generic;
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
        GameMode Mode;
        private int SolutionIndexShown = 1;
        List<Board> Solutions;
        ButtonStateManager Buttons;

        public MainWindow()
        {
            InitializeComponent();
            cellGrid = new CellGrid();
            Solutions = new List<Board>();
            Mode = GameMode.Edit;
            InitializeRulesEngine();
            SetUpSquares();
            InitializeButtonStates();
            Buttons.GoToGameMode(GameMode.Edit);
        }

        private void InitializeRulesEngine()
        {
            rulesEngine = new RulesEngine();
            rulesEngine.AddRule(new ClearOptionsFromSolvedCells());
            rulesEngine.AddRule(new SolveForUniqueValuesInARowColumnOrSquare());
            rulesEngine.AddRule(new CheckForLinearValuesInSquares());
            rulesEngine.AddRule(new SeparateGroupsIntoSubsets());
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

        private void InitializeButtonStates()
        {
            Buttons = new ButtonStateManager();

            Buttons.AddButton(SolveButton, ButtonStateManager.EditAndSolved, ButtonStateManager.Edit);
            Buttons.AddButton(EditButton, ButtonStateManager.All, ButtonStateManager.Solved);
            Buttons.AddButton(ResetButton, ButtonStateManager.All, ButtonStateManager.EditAndSolved);
            Buttons.AddButton(CancelButton, ButtonStateManager.Solving, ButtonStateManager.Solving);
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
            if (Mode != GameMode.Edit)
            {
                return;
            }

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

        private void SolvePuzzle(object sender, RoutedEventArgs e)
        {
            GoToGameMode(GameMode.Solving);
            rulesEngine.Solve(cellGrid.GetSolvedCellData());
        }

        private void EditPuzzle(object sender, RoutedEventArgs e)
        {
            MultipleSolutionsGrid.Visibility = Visibility.Hidden;
            GoToGameMode(GameMode.Edit);
        }

        private void ResetPuzzle(object sender, RoutedEventArgs e)
        {
            MultipleSolutionsGrid.Visibility = Visibility.Hidden;
            GoToGameMode(GameMode.Edit);
            cellGrid.ClearAllCellValues();
        }

        private void GoToGameMode(GameMode mode)
        {
            Buttons.GoToGameMode(mode);
            cellGrid.GoToGameMode(mode);
        }

        private void CancelSolution(object sender, RoutedEventArgs e)
        {
            rulesEngine.Cancel();
            Buttons.GoToGameMode(GameMode.Edit);
        }

        void HandleSolutionComplete(object sender, EventArgs args)
        {
            BoardEventArgs boardArgs = (BoardEventArgs)args;
            Solutions = boardArgs.Boards;
            SetUpBoardForSolutions();
        }

        private void SetUpBoardForSolutions()
        {
            SolutionIndexShown = 0;
            ShowCurrentSolution();

            if (Solutions.Count == 1)
            {
                MultipleSolutionsGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                MultipleSolutionsGrid.Visibility = Visibility.Visible;
                MessageBox.Show($"Puzzle has {Solutions.Count} solutions.");
            }

            Buttons.GoToGameMode(GameMode.Solved);
        }

        private void ShowPreviousSolution(object sender, RoutedEventArgs e)
        {
            SolutionIndexShown = (--SolutionIndexShown).Mod(Solutions.Count);
            ShowCurrentSolution();
        }

        private void ShowNextSolution(object sender, RoutedEventArgs e)
        {
            SolutionIndexShown = (++SolutionIndexShown).Mod(Solutions.Count);
            ShowCurrentSolution();
        }

        private void ShowCurrentSolution()
        {
            Board board = Solutions[SolutionIndexShown];

            for(int index = 0; index < GridMath.TotalNumberOfCells; index++)
            {
                cellGrid.UpdateCell(index, board.GetPossibleValues(index));
            }
            MultipleSolutionText.Text = $"{SolutionIndexShown + 1} of {Solutions.Count}";
        }
    }
}
