using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using MongoMS.Common;
using MongoMS.Extention;
using MongoMS.ViewModel;
namespace MongoMS
{
    internal class TreeViewTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var dt = new HierarchicalDataTemplate(item.GetType());
            dt.ItemsSource = new Binding("Children");
            var f = new FrameworkElementFactory(typeof(Label));
            f.SetBinding(ContentControl.ContentProperty, new Binding("Name"));
            f.AddHandler(Control.MouseDoubleClickEvent, new MouseButtonEventHandler(tb_MouseUp));
            var menu = GetMenu(item.GetType());
            f.SetValue(FrameworkElement.ContextMenuProperty,menu);
            dt.VisualTree = f;
            return dt;
        }
        ContextMenu GetMenu(Type t)
        {
            ContextMenu cm = new ContextMenu();
            var commands = t.GetProperties().Where(p => p.PropertyType == typeof (ICommand));
            foreach (var cmd in commands)
            {
                var mi = new MenuItem();
                var att = cmd.GetAttribute<WindowCommandAttribute>();
                mi.Header = att.Name;
                mi.SetBinding(MenuItem.CommandProperty, new Binding(cmd.Name));
                cm.Items.Add(mi);
            }
            return cm;
        }
        void tb_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var tb = (FrameworkElement)sender;
                DatabaseExplorerCollectionViewModel mi =
                    tb.DataContext as DatabaseExplorerCollectionViewModel;
                mi.FindCommand.Execute(null);
            }
            catch (Exception)
            {
            }
        }
    }
}
