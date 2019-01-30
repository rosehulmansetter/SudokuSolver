using System;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Media;

namespace SudokuSolver
{
    public partial class Cell : UserControl
    {
        private ArrayList possibleNumbers;

        public Cell()
        {
            InitializeComponent();
            possibleNumbers = new ArrayList();
            UpdateUI();
        }

        public ArrayList PossibleNumbers
        {
            set
            {
                possibleNumbers = value;
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            if (possibleNumbers.Contains(1))
                OneLabel.Visibility = System.Windows.Visibility.Visible;
            else
                OneLabel.Visibility = System.Windows.Visibility.Hidden;

            if (possibleNumbers.Contains(2))
                TwoLabel.Visibility = System.Windows.Visibility.Visible;
            else
                TwoLabel.Visibility = System.Windows.Visibility.Hidden;

            if (possibleNumbers.Contains(3))
                ThreeLabel.Visibility = System.Windows.Visibility.Visible;
            else
                ThreeLabel.Visibility = System.Windows.Visibility.Hidden;

            if (possibleNumbers.Contains(4))
                FourLabel.Visibility = System.Windows.Visibility.Visible;
            else
                FourLabel.Visibility = System.Windows.Visibility.Hidden;

            if (possibleNumbers.Contains(5))
                FiveLabel.Visibility = System.Windows.Visibility.Visible;
            else
                FiveLabel.Visibility = System.Windows.Visibility.Hidden;

            if (possibleNumbers.Contains(6))
                SixLabel.Visibility = System.Windows.Visibility.Visible;
            else
                SixLabel.Visibility = System.Windows.Visibility.Hidden;

            if (possibleNumbers.Contains(7))
                SevenLabel.Visibility = System.Windows.Visibility.Visible;
            else
                SevenLabel.Visibility = System.Windows.Visibility.Hidden;

            if (possibleNumbers.Contains(8))
                EightLabel.Visibility = System.Windows.Visibility.Visible;
            else
                EightLabel.Visibility = System.Windows.Visibility.Hidden;

            if (possibleNumbers.Contains(9))
                NineLabel.Visibility = System.Windows.Visibility.Visible;
            else
                NineLabel.Visibility = System.Windows.Visibility.Hidden;
        }

        public void SetAsSelected()
        {
            Background = Brushes.LightCyan;
        }

        public void SetAsUnselected()
        {
            Background = Brushes.White;
        }
    }
}
