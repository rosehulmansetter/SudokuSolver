using SudokuRulesEngine;
using System.Collections.Generic;

namespace SudokuSolver
{
    public class CellGrid
    {
        private const int TOTAL_NUMBER_OF_CELLS = 81;
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
            for (int i = 0; i < TOTAL_NUMBER_OF_CELLS; i++)
            {
                Cell c = new Cell();
                cells.Add(c);
            }
        }

        public Cell GetCellByIndices(int rowIndex, int columnIndex)
        {
            return cells[GridMath.GetIndexByRowAndColumnIndexes(rowIndex, columnIndex)];
        }

        public Cell GetCellByIndex(int index)
        {
            return cells[index];
        }

        public void EnterNumber(int numberPressed)
        {
            cells[selectedCell].Set(numberPressed);
            SetSelectedIndex(selectedCell + 1);
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
            if(selectedCell == TOTAL_NUMBER_OF_CELLS)
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
            foreach(Cell c in cells)
            {
                c.Unset();
            }
        }

        public Dictionary<int, int> GetSolvedCellData()
        {
            Dictionary<int, int> solvedCells = new Dictionary<int, int>();
            for(int i = 0; i < TOTAL_NUMBER_OF_CELLS; i++)
            {
                Cell cell = cells[i];
                if(cell.PossibleNumbers.Count == 1)
                {
                    solvedCells.Add(i, (int)cell.PossibleNumbers[0]);
                }
            }

            return solvedCells;
        }

        public void UpdateBoard(List<List<int>> allPossibleCellValues)
        {
            for(int i = 0; i < allPossibleCellValues.Count; i++)
            {
                cells[i].PossibleNumbers = allPossibleCellValues[i];
            }
        }
    }
}
