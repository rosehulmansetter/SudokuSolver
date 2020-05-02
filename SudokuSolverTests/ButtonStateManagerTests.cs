using FluentAssertions;
using NUnit.Framework;
using SudokuSolver;
using System.Windows;
using System.Windows.Controls;

namespace SudokuSolverTests
{
    [TestFixture]
    public class ButtonStateManagerTests
    {
        ButtonStateManager Manager;
        Button button;

        [SetUp]
        public void Setup()
        {
            Manager = new ButtonStateManager();
            button = new Button();
        }

        [TestCase(ButtonStateManager.All, GameMode.Edit, true)]
        [TestCase(ButtonStateManager.All, GameMode.Solved, true)]
        [TestCase(ButtonStateManager.All, GameMode.Solving, true)]
        [TestCase(ButtonStateManager.EditAndSolved, GameMode.Edit, true)]
        [TestCase(ButtonStateManager.EditAndSolved, GameMode.Solved, true)]
        [TestCase(ButtonStateManager.EditAndSolved, GameMode.Solving, false)]
        [TestCase(ButtonStateManager.EditAndSolving, GameMode.Edit, true)]
        [TestCase(ButtonStateManager.EditAndSolving, GameMode.Solved, false)]
        [TestCase(ButtonStateManager.EditAndSolving, GameMode.Solving, true)]
        [TestCase(ButtonStateManager.SolvedAndSolving, GameMode.Edit, false)]
        [TestCase(ButtonStateManager.SolvedAndSolving, GameMode.Solved, true)]
        [TestCase(ButtonStateManager.SolvedAndSolving, GameMode.Solving, true)]
        [TestCase(ButtonStateManager.Edit, GameMode.Edit, true)]
        [TestCase(ButtonStateManager.Edit, GameMode.Solved, false)]
        [TestCase(ButtonStateManager.Edit, GameMode.Solving, false)]
        [TestCase(ButtonStateManager.Solved, GameMode.Edit, false)]
        [TestCase(ButtonStateManager.Solved, GameMode.Solved, true)]
        [TestCase(ButtonStateManager.Solved, GameMode.Solving, false)]
        [TestCase(ButtonStateManager.Solving, GameMode.Edit, false)]
        [TestCase(ButtonStateManager.Solving, GameMode.Solved, false)]
        [TestCase(ButtonStateManager.Solving, GameMode.Solving, true)]
        public void TestContains(int modes, GameMode mode, bool contains)
        {
            modes.Contains(mode).Should().Be(contains);
        }

        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        public void AddButtonDoesNotChangeButtonsInitialState(bool visible, bool enabled)
        {
            Visibility initalVisibility = visible ? Visibility.Visible : Visibility.Hidden;
            button.Visibility = initalVisibility;
            button.IsEnabled = enabled;

            Manager.AddButton(button, ButtonStateManager.All, ButtonStateManager.All);

            button.Visibility.Should().Be(initalVisibility);
            button.IsEnabled.Should().Be(enabled);
        }

        [Test]
        public void WorksForButtonAlwaysVisibleAndEnabled()
        {
            Manager.AddButton(button, ButtonStateManager.All, ButtonStateManager.All);

            Manager.GoToGameMode(GameMode.Edit);
            button.Visibility.Should().Be(Visibility.Visible);
            button.IsEnabled.Should().BeTrue();

            Manager.GoToGameMode(GameMode.Solved);
            button.Visibility.Should().Be(Visibility.Visible);
            button.IsEnabled.Should().BeTrue();

            Manager.GoToGameMode(GameMode.Solving);
            button.Visibility.Should().Be(Visibility.Visible);
            button.IsEnabled.Should().BeTrue();
        }

        [Test]
        public void WorksForButtonSometimesVisibleAndEnabled()
        {
            Manager.AddButton(button, ButtonStateManager.SolvedAndSolving, ButtonStateManager.Solved);

            Manager.GoToGameMode(GameMode.Edit);
            button.Visibility.Should().Be(Visibility.Hidden);

            Manager.GoToGameMode(GameMode.Solved);
            button.Visibility.Should().Be(Visibility.Visible);
            button.IsEnabled.Should().BeTrue();

            Manager.GoToGameMode(GameMode.Solving);
            button.Visibility.Should().Be(Visibility.Visible);
            button.IsEnabled.Should().BeFalse();

            Manager.GoToGameMode(GameMode.Solved);
            button.Visibility.Should().Be(Visibility.Visible);
            button.IsEnabled.Should().BeTrue();

            Manager.GoToGameMode(GameMode.Edit);
            button.Visibility.Should().Be(Visibility.Hidden);
        }

        [Test]
        public void WorksForMultipleButtons()
        {
            Manager.AddButton(button, ButtonStateManager.Edit, ButtonStateManager.Edit);
            Button otherButton = new Button();
            Manager.AddButton(otherButton, ButtonStateManager.All, ButtonStateManager.Solving);

            Manager.GoToGameMode(GameMode.Edit);
            button.Visibility.Should().Be(Visibility.Visible);
            button.IsEnabled.Should().BeTrue();
            otherButton.Visibility.Should().Be(Visibility.Visible);
            otherButton.IsEnabled.Should().BeFalse();

            Manager.GoToGameMode(GameMode.Solved);
            button.Visibility.Should().Be(Visibility.Hidden);
            otherButton.Visibility.Should().Be(Visibility.Visible);
            otherButton.IsEnabled.Should().BeFalse();

            Manager.GoToGameMode(GameMode.Solving);
            button.Visibility.Should().Be(Visibility.Hidden);
            otherButton.Visibility.Should().Be(Visibility.Visible);
            otherButton.IsEnabled.Should().BeTrue();
        }
    }
}