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
            SetInitialPotentialValues();
            UpdateUI();
        }

        private void SetInitialPotentialValues()
        {
            possibleNumbers = new ArrayList() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }

        public ArrayList PossibleNumbers
        {
            set
            {
                possibleNumbers = value;
                UpdateUI();
            }
        }

        public void Set(int number)
        {
            possibleNumbers = new ArrayList() { number };
            UpdateUI();
        }

        private void UpdateUI()
        {
            if(possibleNumbers.Count == 1)
            {
                SolvedLabel.Visibility = System.Windows.Visibility.Visible;
                SolvedLabel.Content = ((int)possibleNumbers[0]).ToString();
                OneLabel.Visibility = System.Windows.Visibility.Hidden;
                TwoLabel.Visibility = System.Windows.Visibility.Hidden;
                ThreeLabel.Visibility = System.Windows.Visibility.Hidden;
                FourLabel.Visibility = System.Windows.Visibility.Hidden;
                FiveLabel.Visibility = System.Windows.Visibility.Hidden;
                SixLabel.Visibility = System.Windows.Visibility.Hidden;
                SevenLabel.Visibility = System.Windows.Visibility.Hidden;
                EightLabel.Visibility = System.Windows.Visibility.Hidden;
                NineLabel.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                SolvedLabel.Visibility = System.Windows.Visibility.Hidden;
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
        }

        internal void Unset()
        {
            SetInitialPotentialValues();
            UpdateUI();
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
