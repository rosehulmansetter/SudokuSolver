using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SudokuSolver
{
    public class ButtonStateManager
    {
        public const int All = 0b_111;
        public const int EditAndSolved = 0b_110;
        public const int EditAndSolving = 0b_101;
        public const int SolvedAndSolving = 0b_011;
        public const int Edit = 0b_100;
        public const int Solved = 0b_010;
        public const int Solving = 0b_001;

        private List<ButtonManager> Buttons;

        public ButtonStateManager()
        {
            Buttons = new List<ButtonManager>();
        }

        public void AddButton(Button button, int visibleStates, int enabledStates)
        {
            Buttons.Add(new ButtonManager(button, visibleStates, enabledStates));
        }

        public void GoToGameMode(GameMode mode)
        {
            foreach(ButtonManager button in Buttons)
            {
                button.HandleNewGameMode(mode);
            }
        }

        private class ButtonManager
        {
            private Button ManagedButton { get; }
            private int VisibleStates { get; }
            private int EnabledStates { get; }

            public ButtonManager(Button button, int visibleStates, int enabledStates)
            {
                ManagedButton = button;
                VisibleStates = visibleStates;
                EnabledStates = enabledStates;
            }

            public void HandleNewGameMode(GameMode mode)
            {
                if (VisibleStates.Contains(mode))
                {
                    ManagedButton.Visibility = Visibility.Visible;
                    ManagedButton.IsEnabled = EnabledStates.Contains(mode);
                }
                else
                {
                    ManagedButton.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
