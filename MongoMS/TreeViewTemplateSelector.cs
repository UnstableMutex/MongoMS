using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MongoMS.ViewModel;

namespace MongoMS
{
    internal class TreeViewTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var i = item as DatabaseExplorerTreeItemBase;

            var dt = new HierarchicalDataTemplate(item.GetType());

            dt.ItemsSource = new Binding("Children");

            var f = new FrameworkElementFactory(typeof(TextBlock));
            f.SetBinding(TextBlock.TextProperty, new Binding("Name"));
            ContextMenu cm = new ContextMenu();


            switch (i.Type)
            {
                case ItemType.Server:
                    cm.Items.Add(AddDBMI());
                    f.SetValue(FrameworkElement.ContextMenuProperty, cm);
                    break;

                case ItemType.Database:
                    cm.Items.Add(AddCollMI());
                    f.SetValue(FrameworkElement.ContextMenuProperty, cm);
                    break;

                case ItemType.Collection:
                    cm.Items.Add(FindInCollMI());
                    f.SetValue(FrameworkElement.ContextMenuProperty, cm);
                    break;
                    break;


            }

            dt.VisualTree = f;
            return dt;
        }

        private MenuItem FindInCollMI()
        {
            MenuItem mi = new MenuItem();
            mi.Header = "find";
            mi.SetBinding(MenuItem.CommandProperty, new Binding("FindCommand"));
            return mi;
        }
        private MenuItem AddDBMI()
        {
            MenuItem mi = new MenuItem();
            mi.Header = "add db";
            mi.SetBinding(MenuItem.CommandProperty, new Binding("AddDBCommand"));
            return mi;
        }
        private MenuItem AddCollMI()
        {
            MenuItem mi = new MenuItem();
            mi.Header = "add coll";
            mi.SetBinding(MenuItem.CommandProperty, new Binding("AddCollectionCommand"));
            return mi;
        }
    }
}


