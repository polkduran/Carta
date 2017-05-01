using System.Collections.Generic;

namespace Carta.Core
{
    public class CartaLine : ObservableObject
    {
        public int Index { get; }

        private readonly List<int> _blocks;
        public IReadOnlyList<int> Blocks => _blocks;

        internal List<Cell> Cells { get; }

        private bool _completed;
        public bool Completed
        {
            get { return _completed; }
            private set { Set(ref _completed, value); }
        }

        public CartaLine(int index)
        {
            Index = index;
            Cells = new List<Cell>();

            // Initialization with 1 item to signal an empty line.
            _blocks = new List<int> { 0 };
        }

        internal void OnBuilt()
        {
            RemoveLastIfNeeded();
            CheckCompleted();
        }

        internal void AddCell(Cell cell)
        {
            Cells.Add(cell);
            cell.PropertyChanged += Cell_PropertyChanged;
            var lastIndex = _blocks.Count - 1;
            if (cell.Filled)
            {
                _blocks[lastIndex]++;
            }
            else
            {

                if (_blocks[lastIndex] > 0)
                {
                    _blocks.Add(0);
                }
            }
        }

        private void Cell_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Cell.VisualState))
            {
                CheckCompleted();
            }
        }

        private void RemoveLastIfNeeded()
        {
            var lastIndex = _blocks.Count - 1;
            if (lastIndex > 0 && _blocks[lastIndex] == 0)
            {
                _blocks.RemoveAt(lastIndex);
            }
        }

        private void CheckCompleted()
        {
            var cellsBlocks = new List<int>();
            var prevCellFilled = false;
            foreach (var cell in Cells)
            {
                if (cell.VisualState == CellVisualState.MarkedAsFilled)
                {
                    if (prevCellFilled)
                    {
                        cellsBlocks[cellsBlocks.Count - 1]++;
                    }
                    else
                    {
                        cellsBlocks.Add(1);
                    }
                    prevCellFilled = true;
                }
                else
                {
                    prevCellFilled = false;
                }
            }

            // for empty lines.
            if(cellsBlocks.Count == 0)
            {
                cellsBlocks.Add(0);
            }
            if (cellsBlocks.Count != Blocks.Count)
            {
                Completed = false;
                return;
            }
            for (var i = 0; i < cellsBlocks.Count; i++)
            {
                if (cellsBlocks[i] != Blocks[i])
                {
                    Completed = false;
                    return;
                }
            }
            Completed = true;
        }

        public void Over()
        {
            foreach (var cell in Cells)
            {
                cell.PropertyChanged -= Cell_PropertyChanged;
            }
        }
    }
}
