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

        private readonly List<CartaLine> _allLines = new List<CartaLine>();
        internal IEnumerable<CartaLine> AllLines => _allLines;

        public CartaGrid(bool[,] grid)
        {
            BuildCellGrid(grid);
        }

        private void BuildCellGrid(bool[,] grid)
        {
            Cells = new Cell[grid.GetLength(0), grid.GetLength(1)];

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                NewColumn(x);
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (x == 0)
                    {
                        NewRow(y);
                    }
                    var cell = new Cell(x, y, grid[x, y]);
                    Cells[x, y] = cell;

                    _columns[cell.X].AddCell(cell);
                    _rows[cell.Y].AddCell(cell);

                    if (x == grid.GetLength(0) - 1)
                    {
                        _rows[y].OnBuilt();
                    }
                }
                _columns[x].OnBuilt();
            }
        }

        private void NewRow(int y)
        {
            var row = new CartaLine(y);
            _rows.Add(row);
            _allLines.Add(row);
        }

        private void NewColumn(int x)
        {
            var column = new CartaLine(x);
            _columns.Add(column);
            _allLines.Add(column);
        }
    }
}
