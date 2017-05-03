namespace Carta.Core
{
    public class Cell : ObservableObject
    {
        public int X { get; }
        public int Y { get; }
        public bool Filled { get; }

        private CellState _state;
        public CellState State
        {
            get
            {
                return _state;
            }
            set
            {
                Set(ref _state, value);
            }
        }

        public Cell(int x, int y, bool filled)
        {
            X = x;
            Y = y;
            Filled = filled;
        }

        public override string ToString()
        {
            return $"({X}, {Y}):{State}:{Filled}";
        }
    }
}
