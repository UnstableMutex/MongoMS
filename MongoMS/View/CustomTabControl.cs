using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MongoMS.View
{
    internal class CustomTabControl : TabControl
    {
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            if (e.NewItems != null)
                if (e.NewItems.Count > 0)
                {
                    SelectedIndex = e.NewStartingIndex;
                }

        }
    }
}