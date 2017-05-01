namespace Carta.Core
{
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
}
