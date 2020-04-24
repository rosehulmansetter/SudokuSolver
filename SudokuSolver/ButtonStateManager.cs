using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SudokuSolver
{
    public class ButtonStateManager
    {
        private Button ManagedButton { get; set; }
        private List<GameMode> VisibleStates { get; set; }
        private List<GameMode> EnabledStates { get; set; }

        public ButtonStateManager(Button button)
        {
            ManagedButton = button;
            VisibleStates = new List<GameMode>();
            EnabledStates = new List<GameMode>();
        }

        public void HandleNewGameMode(GameMode mode)
        {
            if(VisibleStates.Contains(mode))
            {
                ManagedButton.Visibility = Visibility.Visible;
                ManagedButton.IsEnabled = EnabledStates.Contains(mode);
            }
            else
            {
                ManagedButton.Visibility = Visibility.Hidden;
            }
        }

        public void SetVisibleStates(params GameMode[] modes)
        {
            VisibleStates.Clear();
            VisibleStates.AddRange(modes);
        }

        public void SetEnabledStates(params GameMode[] modes)
        {
            EnabledStates.Clear();
            EnabledStates.AddRange(modes);
        }
    }
}
