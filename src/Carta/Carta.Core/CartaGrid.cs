using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carta.Core
{
    public class CartaGrid : ObservableObject
    {
        public Cell[,] Cells { get; private set; }

        private readonly List<CartaLine> _columns = new List<CartaLine>();
        public IReadOnlyList<CartaLine> Columns => _columns;

        private readonly List<CartaLine> _rows = new List<CartaLine>();
        public IReadOnlyList<CartaLine> Rows => _rows;

        private readonly List<CartaLine> _allLines = new List<CartaLine>();
        internal IEnumerable<CartaLine> AllLines => _allLines;

        private bool _completed;
        public bool Completed
        {
            get { return _completed; }
            private set { Set(ref _completed, value); }
        }

        public CartaGrid(bool[,] grid)
        {
            BuildCellGrid(grid);
        }

        private void BuildCellGrid(bool[,] grid)
        {
            Cells = new Cell[grid.GetLength(0), grid.GetLength(1)];

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                NewLine(x, _columns);
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (x == 0)
                    {
                        NewLine(y, _rows);
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

        private void NewLine(int index, List<CartaLine> group)
        {
            var line = new CartaLine(index);
            line.PropertyChanged += Line_PropertyChanged;
            group.Add(line);
            _allLines.Add(line);
        }

        private void Line_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(CartaLine.Completed))
            {
                CheckCompleted();
            }
        }

        private void CheckCompleted()
        {
            if(AllLines.All(l => l.Completed))
            {
                Completed = true;
            }
        }
    }
}
