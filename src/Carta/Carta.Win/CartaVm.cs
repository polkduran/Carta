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

        public CellVisualState SelectedCellVisualState
        {
            get { return _selectedCellVisualState; }
            set { Set(ref _selectedCellVisualState , value); }
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
            SelectedCellVisualState = CellVisualState.MarkedAsFilled;
        }

        public void Toggle(Cell cell)
        {
            if (cell.VisualState != CellVisualState.None)
            {
                cell.VisualState = CellVisualState.None;
            }
            else
            {
                cell.VisualState = SelectedCellVisualState;
            }
        }
    }
}
