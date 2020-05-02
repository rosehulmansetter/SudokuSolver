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
            cells = new List<Cell>();
            GenerateCells();
            ResetSelectedCell();
        }

        private void GenerateCells()
        {
            for (int i = 0; i < GridMath.TotalNumberOfCells; i++)
            {
                Cell c = new Cell();
                cells.Add(c);
            }
        }

        public Cell GetCellByIndices(int rowIndex, int columnIndex)
        {
            return cells[GridMath.GetIndexByRowAndColumnIndices(rowIndex, columnIndex)];
        }

        public Cell GetCellByIndex(int index)
        {
            return cells[index];
        }

        public void EnterNumber(int numberPressed)
        {
            cells[selectedCell].Set(numberPressed);
            if(selectedCell + 1 < GridMath.TotalNumberOfCells)
            {
                SetSelectedIndex(selectedCell + 1);
            }
        }

        private void SetSelectedIndex(int selectedIndex)
        {
            cells[selectedCell].SetAsUnselected();
            selectedCell = selectedIndex;
            cells[selectedCell].SetAsSelected();
        }

        public void MoveRight()
        {
            if(selectedCell % 9 != 8)
            {
                SetSelectedIndex(selectedCell + 1);
            }
        }

        public void MoveLeft()
        {
            if (selectedCell % 9 != 0)
            {
                SetSelectedIndex(selectedCell - 1);
            }
        }

        public void MoveUp()
        {
            if (selectedCell > 8)
            {
                SetSelectedIndex(selectedCell - 9);
            }
        }

        public void MoveDown()
        {
            if (selectedCell < 72)
            {
                SetSelectedIndex(selectedCell + 9);
            }
        }

        public void MoveToNext()
        {
            int newSelectedIndex = selectedCell + 1;
            if(selectedCell == GridMath.TotalNumberOfCells)
            {
                newSelectedIndex = 0;
            }
            SetSelectedIndex(newSelectedIndex);
        }

        public void ClearSelectedCell()
        {
            cells[selectedCell].Unset();
        }

        public void SetHintsVisible(bool visible)
        {
            cells.ForEach(c => c.SetHintsVisible(visible));
        }

        public void UnselectSelectedCell()
        {
            cells[selectedCell].SetAsUnselected();
        }

        public void ResetSelectedCell()
        {
            UnselectSelectedCell();
            selectedCell = 0;
            cells[selectedCell].SetAsSelected();
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
    }
}
