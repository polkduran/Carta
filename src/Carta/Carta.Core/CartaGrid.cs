using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carta.Core
{
    public class CartaGrid
    {
        public Cell[,] Cells { get; private set; }

        private readonly List<CartaLine> _columns = new List<CartaLine>();
        public IReadOnlyList<CartaLine> Columns => _columns;

        private readonly List<CartaLine> _rows = new List<CartaLine>();
        public IReadOnlyList<CartaLine> Rows => _rows;

        public CartaGrid(bool[,] grid)
        {
            BuildCellGrid(grid);
        }

        private void BuildCellGrid(bool[,] grid)
        {
            Cells = new Cell[grid.GetLength(0), grid.GetLength(1)];

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                _columns.Add(new CartaLine(x));
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (x == 0)
                    {
                        _rows.Add(new CartaLine(y));
                    }
                    var cell = new Cell(x, y, grid[x, y]); 
                    Cells[x, y] = cell;

                    FillLines(cell);

                    if (x == grid.GetLength(0) - 1)
                    {
                        RemoveLastIfNeeded(_rows[y].BlocksInternal);
                    }
                }
                RemoveLastIfNeeded(_columns[x].BlocksInternal);
            }
        }

        private void FillLines(Cell cell)
        {
            FillLine(cell, _columns[cell.X]);
            FillLine(cell, _rows[cell.Y]);
        }

        private void RemoveLastIfNeeded(List<int> blocks)
        {
            var lastIndex = blocks.Count - 1;
            if (lastIndex > 0 && blocks[lastIndex] == 0)
            {
                blocks.RemoveAt(lastIndex);
            }
        }

        private void FillLine(Cell cell, CartaLine line)
        {
            line.Cells.Add(cell);
            var lastIndex = line.BlocksInternal.Count - 1;
            if (cell.Filled)
            {
                line.BlocksInternal[lastIndex]++;
            }
            else
            {
                
                if (line.BlocksInternal[lastIndex] > 0)
                {
                    line.BlocksInternal.Add(0);
                }
            }
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

    public class CartaLine : ObservableObject
    {
        public int Index { get; }

        internal List<int> BlocksInternal { get; }
        public IReadOnlyList<int> Blocks => BlocksInternal;

        internal List<Cell> Cells { get; }

        private bool _completed;
        public bool Completed
        {
            get { return _completed; }
            set
            {
                Set(ref _completed, value);
            }
        }

        public CartaLine(int index)
        {
            Index = index;
            Cells = new List<Cell>();

            // Initialization with 1 item to signal an empty line.
            BlocksInternal = new List<int> { 0 };
        }

        private void CheckCompleted()
        {
            // Empty row
            if(Blocks.Count == 0 && Blocks[0] == 0)
            {
                Completed = true;
                return;
            }
        }
    }
}
