using System;
using System.Collections.Generic;

namespace SudokuRulesEngine
{
    public class BoardEventArgs : EventArgs
    {
        public List<Board> Boards { get; set; }
    }
}
