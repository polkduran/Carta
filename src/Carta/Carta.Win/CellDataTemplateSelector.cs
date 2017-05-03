using System;
using System.Windows;
using System.Windows.Controls;
using Carta.Core;

namespace Carta.Win
{
    public class CellDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NotMarkedTemplate { get; set; }

        public DataTemplate FilledMarkedTemplate { get; set; }

        public DataTemplate EmptyMarkedTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var state = (CellState)item;

            switch (state)
            {
                case CellState.None:
                    return NotMarkedTemplate;
                case CellState.MarkedAsFilled:
                    return FilledMarkedTemplate;
                case CellState.MarkedAsEmpty:
                    return EmptyMarkedTemplate;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
