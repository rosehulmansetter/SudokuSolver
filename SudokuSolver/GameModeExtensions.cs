namespace SudokuSolver
{
    public static class GameModeExtensions
    {
        public static bool Contains(this int states, GameMode mode)
        {
            return (states & ((int)mode)) > 0;
        }
    }
}
