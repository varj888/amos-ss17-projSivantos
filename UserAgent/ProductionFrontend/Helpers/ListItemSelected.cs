using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Frontend.Helpers
{
    /// <summary>
    /// detect selected item(s) in the Frontend-List
    /// </summary>
    public class ListItemSelected
    {
        public static object GetSelectedItem(DependencyObject o)
        {
            return o.GetValue(SelectedItemProp);
        }

        public static void SetSelectedItem(DependencyObject o, object value)
        {
            o.SetValue(SelectedItemProp, value);
        }

        public static ICommand GetSelectedItemChanged(DependencyObject o)
        {
            return (ICommand)o.GetValue(SelectedItemProp);
        }

        public static void SetSelectedItemChanged(DependencyObject o, ICommand value)
        {
            o.SetValue(SelectedItemProp, value);
        }

        public static readonly DependencyProperty SelectedItemProp =
            DependencyProperty.RegisterAttached("SelectedItem",
                typeof(object), typeof(ListItemSelected),
                new UIPropertyMetadata(null, SelectedItemChanged));

        private static void SelectedItemChanged(DependencyObject o,
            DependencyPropertyChangedEventArgs e)
        {
            if (!(o is ListView) || e.NewValue == null)
                return;

            var view = o as ListView;

            view.SelectionChanged += (sender, eNew) =>
            SetSelectedItem(view, e.NewValue);

            var cmd = (ICommand)(view as DependencyObject).GetValue
                (SelectedItemChangedProperty);
            if (cmd != null)
            {
                if (cmd.CanExecute(null))
                    cmd.Execute(new DepPropEventArgs(e));
            }
        }

        public static readonly DependencyProperty SelectedItemChangedProperty =
            DependencyProperty.RegisterAttached("SelectedItemChanged",
                typeof(ICommand), typeof(ListItemSelected));

        public class DepPropEventArgs : EventArgs
        {
            public DependencyPropertyChangedEventArgs
                DependencyPropertyChangedEventArgs
            { get; private set; }

            public DepPropEventArgs
                (DependencyPropertyChangedEventArgs eventArgs)
            {
                this.DependencyPropertyChangedEventArgs = eventArgs;
            }
        }
    }
}
