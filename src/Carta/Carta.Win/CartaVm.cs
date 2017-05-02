using System;
using System.Collections.Generic;
using System.Windows.Media;
using Carta.Core;

namespace Carta.Win
{
    public class CartaVm : ObservableObject
    {
        private CellVisualState _selectedCellVisualState;
        public CartaGrid Grid { get; set; }

        public IEnumerable<Cell> Cells { get; set; }

        public CellVisualState CellVisualStateMode
        {
            get { return _selectedCellVisualState; }
            private set { Set(ref _selectedCellVisualState, value); }
        }

        private CellVisualState? _currentCellState;
        public CellVisualState? CurrentCellState
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
            CellVisualStateMode = CellVisualState.MarkedAsFilled;
        }

        public void Toggle(Cell cell)
        {
            if (CurrentCellState == null)
            {
                return;
            }
            cell.VisualState = CurrentCellState.Value;
        }

        public void ChangeMode()
        {
            if (CellVisualStateMode == CellVisualState.MarkedAsFilled)
            {
                CellVisualStateMode = CellVisualState.MarkedAsEmpty;
            }
            else
            {
                CellVisualStateMode = CellVisualState.MarkedAsFilled;
            }
        }

        internal void SetCurrentCelleState(Cell cell)
        {
            if (cell.VisualState == CellVisualState.None)
            {
                CurrentCellState = CellVisualStateMode;
            }
            else
            {
                CurrentCellState = CellVisualState.None;
            }
            Toggle(cell);
        }
    }
}
