using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carta.Core
{
    public class CartaGrid
    {
        public CartaGrid(bool[,] grid)
        {

        }


        private Cell[][] _cells;
        public Cell GetCell(int x, int y)
        {
            return _cells[x][y];
        }

    }

    public class Cell
    {
        public int X { get; }
        public int Y { get; }
        public bool Filled { get; }
        public CellVisualState VisualState { get; set; }
    }

    public enum CellVisualState
    {
        None,
        MarkedAsFilled,
        MarkedAsEmpty
    }

    public class CartaLine
    {
        public int Index { get; set; }
        public IReadOnlyList<int> Blocks { get; set; }
        public bool Completed { get; set; }
    }


}
