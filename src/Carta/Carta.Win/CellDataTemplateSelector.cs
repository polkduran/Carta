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
            var visualState = (CellVisualState)item;

            switch (visualState)
            {
                case CellVisualState.None:
                    return NotMarkedTemplate;
                case CellVisualState.MarkedAsFilled:
                    return FilledMarkedTemplate;
                case CellVisualState.MarkedAsEmpty:
                    return EmptyMarkedTemplate;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
