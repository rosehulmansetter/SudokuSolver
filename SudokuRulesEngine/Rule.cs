namespace SudokuRulesEngine
{
    interface Rule
    {
        void ApplyRule(ref Board board);
    }
}
