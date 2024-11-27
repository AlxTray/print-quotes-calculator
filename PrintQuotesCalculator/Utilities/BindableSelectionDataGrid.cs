using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace PrintQuotesCalculator.Utilities
{
    public class BindableSelectionDataGrid : DataGrid
    {
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register(nameof(SelectedItems), typeof(IList), typeof(BindableSelectionDataGrid), new PropertyMetadata(default(IList)));

        public new IEnumerable SelectedItems
        {
            get => (IList)GetValue(SelectedItemsProperty);
            set => new Exception("This property is read-only. To bind to it you must use 'Mode=OneWayToSource'.");
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            SetValue(SelectedItemsProperty, base.SelectedItems);
        }
    }
}
