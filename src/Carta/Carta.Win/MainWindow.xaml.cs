using System.Windows;
using System.Windows.Input;
using Carta.Core;

namespace Carta.Win
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private CartaVm _vm;
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var grid = new bool[4, 3] {
                { true, true, false },
                { false, false, false },
                { true, true, true},
                { false, true, false },
            };

            var cartaGrid = new CartaGrid(grid);
            _vm = new CartaVm(cartaGrid);
            DataContext = _vm;
        }

        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var cell = (Cell)((FrameworkElement) sender).DataContext;
                _vm.Toggle(cell);
            }
        }

        private void ChangeMode_OnClick(object sender, RoutedEventArgs e)
        {
            _vm.ChangeMode();
        }

        private void StackPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _vm.CurrentCellState = null;
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var cell = (Cell)((FrameworkElement)sender).DataContext;
            _vm.SetCurrentCellState(cell);
        }
    }
}
