using System;
using System.Collections.Generic;
using System.Windows.Media;
using Carta.Core;

namespace Carta.Win
{
    public class CartaVm : ObservableObject
    {
        private CellState _selectedCellState;
        public CartaGrid Grid { get; set; }

        public IEnumerable<Cell> Cells { get; set; }

        public CellState CellStateMode
        {
            get { return _selectedCellState; }
            private set { Set(ref _selectedCellState, value); }
        }

        private CellState? _currentCellState;
        public CellState? CurrentCellState
        {
            get { return _currentCellState; }
            set { Set(ref _currentCellState, value); }
        }

        public CartaVm(CartaGrid grid)
        {
            Grid = grid;
            var cells = new List<Cell>(grid.Cells.Length);
            for (var y = 0; y < grid.Cells.GetLength(1); y++)
            {
                for (var x = 0; x < grid.Cells.GetLength(0); x++)
                {

                    cells.Add(grid.Cells[x, y]);
                }
            }

            Cells = cells;
            CellStateMode = CellState.MarkedAsFilled;
        }

        public void Toggle(Cell cell)
        {
            if (CurrentCellState == null)
            {
                return;
            }
            cell.State = CurrentCellState.Value;
        }

        public void ChangeMode()
        {
            if (CellStateMode == CellState.MarkedAsFilled)
            {
                CellStateMode = CellState.MarkedAsEmpty;
            }
            else
            {
                CellStateMode = CellState.MarkedAsFilled;
            }
        }

        internal void SetCurrentCellState(Cell cell)
        {
            if (cell.State == CellState.None)
            {
                CurrentCellState = CellStateMode;
            }
            else
            {
                CurrentCellState = CellState.None;
            }
            Toggle(cell);
        }
    }
}
