using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MongoMS.View
{
    /// <summary>
    ///     Логика взаимодействия для DatabaseExplorerView.xaml
    /// </summary>
    public partial class DatabaseExplorerView : UserControl
    {
        public DatabaseExplorerView()
        {
            InitializeComponent();
        }

        private void OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);
            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        private static TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            try
            {
                while (source != null && !(source is TreeViewItem))
                    source = VisualTreeHelper.GetParent(source);
                return source as TreeViewItem;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}