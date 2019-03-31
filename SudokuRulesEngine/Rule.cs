namespace SudokuRulesEngine
{
    public interface Rule
    {
        void ApplyRule(ref Board board);
    }
}
