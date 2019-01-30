using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SudokuRulesEngine;

namespace SudokuSolver
{
    public partial class MainWindow : Window
    {
        private List<Cell> cells;
        private const int SQUARE_SIZE = 3;
        private const int TOTAL_NUMBER_OF_CELLS = 81;

        public MainWindow()
        {
            InitializeComponent();
            cells = new List<Cell>();
            GenerateCells();
            SetUpSquares();

            cells[0].SetAsSelected();
        }

        private void GenerateCells()
        {
            for(int i = 0; i < TOTAL_NUMBER_OF_CELLS; i++)
            {
                Cell c = new Cell();
                cells.Add(c);
            }
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
                    Cell c = cells[GridMath.GetIndexByGridAndSquareIndexes(gridRowIndex, gridColumnIndex, row, column)];

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
    }
}
