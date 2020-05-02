using SudokuRulesEngine;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Linq;

namespace SudokuSolver
{
    public partial class Cell : UserControl
    {
        private List<int> possibleNumbers;
        private bool showHints;
        private Dictionary<int, Label> HintLabels;

        public Cell()
        {
            InitializeComponent();
            SetUpHintLabels();
            SetInitialPotentialValues();
            SetHintsVisible(false);
        }

        private void SetUpHintLabels()
        {
            HintLabels = new Dictionary<int, Label>
            {
                { 1, OneLabel },
                { 2, TwoLabel },
                { 3, ThreeLabel },
                { 4, FourLabel },
                { 5, FiveLabel },
                { 6, SixLabel },
                { 7, SevenLabel },
                { 8, EightLabel },
                { 9, NineLabel }
            };
        }

        private void SetInitialPotentialValues()
        {
            PossibleNumbers = GridMath.AllPossibleValues();
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
            PossibleNumbers = new List<int>() { number };
        }

        private void UpdateUI()
        {
            SolvedLabel.Visibility = IsSolved() ? Visibility.Visible : Visibility.Hidden;
            SolvedLabel.Content = possibleNumbers.First().ToString();

            foreach(int possibleValue in HintLabels.Keys)
            {
                bool showHint = showHints && !IsSolved() && possibleNumbers.Contains(possibleValue);
                HintLabels[possibleValue].Visibility = showHint ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public bool IsSolved()
        {
            return possibleNumbers.Count == 1;
        }

        public void Unset()
        {
            SetInitialPotentialValues();
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
