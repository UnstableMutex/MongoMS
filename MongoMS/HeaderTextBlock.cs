using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MongoMS.Common;

namespace MongoMS
{
    public class HeaderTextBlock : TextBlock
    {

        protected override void OnInitialized(EventArgs e)
        {
            var ti = FindVisualParent<TabItem>(this);
            var dc = (ti.Content as UserControl).DataContext as ITabContent;
            this.Text = dc.Header;
        }

        private T FindVisualParent<T>(DependencyObject node) where T : DependencyObject
        {
            var tp = this.TemplatedParent;
            DependencyObject parent = VisualTreeHelper.GetParent(node);
            if (parent == null || parent is T) return (T)parent;

            // Recurse up the visual tree.
            return FindVisualParent<T>(parent);
        }
    }
}