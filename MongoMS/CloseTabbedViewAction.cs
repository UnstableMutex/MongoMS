using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;
using Microsoft.Practices.Prism.Regions;

namespace MongoMS
{
    public class CloseTabbedViewAction : TriggerAction<FrameworkElement>
    {
        protected override void Invoke(object parameter)
        {
            RoutedEventArgs args = parameter as RoutedEventArgs;
            if (args == null) return;

            // Find the parent tab item that contains the view to remove.
            TabItem tabItem = FindVisualParent<TabItem>(args.OriginalSource as DependencyObject);

            // Find the parent tab control that represents the region.
            TabControl tabControl = FindVisualParent<TabControl>(tabItem);

            if (tabControl != null && tabItem != null)
            {
                // Get the view.
                object view = tabItem.Content;

                // Get the region associated with the tab control.
                IRegion region = RegionManager.GetObservableRegion(tabControl).Value;
                if (region != null)
                {
                    region.Remove(view);
                }
            }
        }

        private T FindVisualParent<T>(DependencyObject node) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(node);
            if (parent == null || parent is T) return (T)parent;

            // Recurse up the visual tree.
            return FindVisualParent<T>(parent);
        }
    }
}