using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace SudokuSolver
{
    public partial class Cell : UserControl
    {
        private List<int> possibleNumbers;
        private bool showHints;

        public Cell()
        {
            InitializeComponent();
            SetInitialPotentialValues();
            showHints = false;
            UpdateUI();
        }

        private void SetInitialPotentialValues()
        {
            possibleNumbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }

        public List<int> PossibleNumbers
        {
            set
            {
                possibleNumbers = value;
                UpdateUI();
            }
            get
            {
                return possibleNumbers;
            }
        }

        public void Set(int number)
        {
            possibleNumbers = new List<int>() { number };
            UpdateUI();
        }

        private void UpdateUI()
        {
            if(!showHints || possibleNumbers.Count == 1)
            {
                OneLabel.Visibility = System.Windows.Visibility.Hidden;
                TwoLabel.Visibility = System.Windows.Visibility.Hidden;
                ThreeLabel.Visibility = System.Windows.Visibility.Hidden;
                FourLabel.Visibility = System.Windows.Visibility.Hidden;
                FiveLabel.Visibility = System.Windows.Visibility.Hidden;
                SixLabel.Visibility = System.Windows.Visibility.Hidden;
                SevenLabel.Visibility = System.Windows.Visibility.Hidden;
                EightLabel.Visibility = System.Windows.Visibility.Hidden;
                NineLabel.Visibility = System.Windows.Visibility.Hidden;

                if(possibleNumbers.Count == 1)
                {
                    SolvedLabel.Visibility = System.Windows.Visibility.Visible;
                    SolvedLabel.Content = ((int)possibleNumbers[0]).ToString();
                }
                else
                {
                    SolvedLabel.Visibility = System.Windows.Visibility.Hidden;
                }
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

        public void SetHintsVisible(bool visible)
        {
            showHints = visible;
            UpdateUI();
        }
    }
}
