namespace SudokuRulesEngine
{
    public interface Rule
    {
        bool ApplyRule(ref Board board);
    }
}
