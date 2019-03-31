using System;

namespace SudokuRulesEngine
{
    public class BoardEventArgs : EventArgs
    {
        public Board BoardData { get; set; }
    }
}
