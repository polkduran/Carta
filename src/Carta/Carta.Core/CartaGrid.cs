using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carta.Core
{
    public class CartaGrid
    {
        private Cell[,] _cells;

        public IReadOnlyCollection<CartaLine> Columns { get; private set; }

        public IReadOnlyCollection<CartaLine> Rows { get; private set; }

        public CartaGrid(bool[,] grid)
        {
            BuildCellGrid(grid);
        }

        private void BuildCellGrid(bool[,] grid)
        {
            _cells = new Cell[grid.GetLength(0), grid.GetLength(1)];
            var cols = new List<int>[grid.GetLength(0)];
            var rows = new List<int>[grid.GetLength(1)];

            for (int x = 0; x < grid.GetLength(1); x++)
            {
                cols[x] = new List<int> { 0 };
                for (int y = 0; y < grid.GetLength(0); y++)
                {
                    if (x == 0)
                    {
                        rows[y] = new List<int> { 0 };
                    }
                    var filled = grid[x, y];
                    FillLine(filled, cols[x]);
                    FillLine(filled, rows[y]);
                    _cells[x, y] = new Cell(x, y, filled);

                    if (x == grid.GetLength(1) - 1)
                    {
                        RemoveLastIfNeeded(rows[y]);
                    }
                }
                RemoveLastIfNeeded(cols[x]);
            }
        }

        private void RemoveLastIfNeeded(List<int> blocks)
        {
            var lastIndex = blocks.Count - 1;
            if (lastIndex > 0 && blocks[lastIndex] == 0)
            {
                blocks.RemoveAt(lastIndex);
            }
        }

        private void FillLine(bool filled, List<int> blocks)
        {
            if (filled)
            {
                blocks[blocks.Count - 1]++;
            }
            else
            {
                if (blocks.Count > 1)
                {
                    blocks.Add(0);
                }
            }
        }

        public Cell GetCell(int x, int y)
        {
            return _cells[x, y];
        }
    }

    public class Cell : ObservableObject
    {
        public int X { get; }
        public int Y { get; }
        public bool Filled { get; }

        private CellVisualState _visualState;
        public CellVisualState VisualState
        {
            get
            {
                return _visualState;
            }
            set
            {
                Set(ref _visualState, value);
            }
        }

        public Cell(int x, int y, bool filled)
        {
            X = x;
            Y = y;
            Filled = filled;
        }
    }

    public enum CellVisualState
    {
        None,
        MarkedAsFilled,
        MarkedAsEmpty
    }

    public class CartaLine
    {
        public int Index { get; }
        public IReadOnlyList<int> Blocks { get; }
        public bool Completed { get; set; }

        public CartaLine(int index, IReadOnlyList<int> blocks)
        {
            Index = index;
            Blocks = blocks;
        }
    }
}
