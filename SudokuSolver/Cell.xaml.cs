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
            HintLabels = new Dictionary<int, Label>();
            HintLabels.Add(1, OneLabel);
            HintLabels.Add(2, TwoLabel);
            HintLabels.Add(3, ThreeLabel);
            HintLabels.Add(4, FourLabel);
            HintLabels.Add(5, FiveLabel);
            HintLabels.Add(6, SixLabel);
            HintLabels.Add(7, SevenLabel);
            HintLabels.Add(8, EightLabel);
            HintLabels.Add(9, NineLabel);
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
