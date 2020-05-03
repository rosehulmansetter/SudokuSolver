using SudokuRulesEngine;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    public class CellGrid
    {
        private List<Cell> cells;
        private int selectedCell;

        public CellGrid()
        {
            cells = Enumerable.Repeat(new Cell(), GridMath.TotalNumberOfCells).ToList();
            ResetSelectedCell();
        }

        public Cell GetCellByIndices(int rowIndex, int columnIndex)
        {
            return cells[GridMath.GetIndexByRowAndColumnIndices(rowIndex, columnIndex)];
        }

        public void EnterNumber(int numberPressed)
        {
            cells[selectedCell].Set(numberPressed);
            if(selectedCell + 1 < GridMath.TotalNumberOfCells)
            {
                SetSelectedIndex(selectedCell + 1);
            }
        }

        public void MoveRight()
        {
            if(GridMath.GetColumnForIndex(selectedCell) < 8)
            {
                SetSelectedIndex(selectedCell + 1);
            }
        }

        public void MoveLeft()
        {
            if (GridMath.GetColumnForIndex(selectedCell) > 0)
            {
                SetSelectedIndex(selectedCell - 1);
            }
        }

        public void MoveUp()
        {
            if (GridMath.GetRowForIndex(selectedCell) > 0)
            {
                SetSelectedIndex(selectedCell - 9);
            }
        }

        public void MoveDown()
        {
            if (GridMath.GetRowForIndex(selectedCell) < 8)
            {
                SetSelectedIndex(selectedCell + 9);
            }
        }

        public void MoveToNext()
        {
            SetSelectedIndex((selectedCell + 1) % GridMath.TotalNumberOfCells);
        }

        public void ClearSelectedCell()
        {
            cells[selectedCell].Unset();
        }

        public void ClearAllCellValues()
        {
            cells.ForEach(c => c.Unset());
        }

        public void GoToGameMode(GameMode mode)
        {
            SetHintsVisible(mode != GameMode.Edit);
            if (mode == GameMode.Edit)
            {
                ResetSelectedCell();
            }
            else
            {
                UnselectSelectedCell();
            }
        }

        public Dictionary<int, int> GetSolvedCellData()
        {
            Dictionary<int, int> solvedCells = new Dictionary<int, int>();
            for(int i = 0; i < GridMath.TotalNumberOfCells; i++)
            {
                Cell cell = cells[i];
                if(cell.IsSolved())
                {
                    solvedCells.Add(i, cell.PossibleNumbers.First());
                }
            }

            return solvedCells;
        }

        public void UpdateCell(int index, List<int> possibleValues)
        {
            cells[index].PossibleNumbers = possibleValues;
        }

        private void SetSelectedIndex(int selectedIndex)
        {
            cells[selectedCell].SetAsUnselected();
            selectedCell = selectedIndex;
            cells[selectedCell].SetAsSelected();
        }

        private void SetHintsVisible(bool visible)
        {
            cells.ForEach(c => c.SetHintsVisible(visible));
        }

        private void UnselectSelectedCell()
        {
            cells[selectedCell].SetAsUnselected();
        }

        private void ResetSelectedCell()
        {
            UnselectSelectedCell();
            selectedCell = 0;
            cells[selectedCell].SetAsSelected();
        }
    }
}
