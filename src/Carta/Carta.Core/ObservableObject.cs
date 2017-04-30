using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Carta.Core
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool Set<T>(ref T old, T current, [CallerMemberName] string propertyName = null)
        {
            if(EqualityComparer<T>.Default.Equals(old, current))
            {
                return true;
            }
            old = current;
            RaisePropertyChanged(propertyName);
            return true;
        }
    }
}
