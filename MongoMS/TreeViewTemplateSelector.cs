using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MongoMS.Common;
using MongoMS.Extention;
using MongoMS.ViewModel;
namespace MongoMS
{
    internal class TreeViewTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            //     <StackPanel Orientation="Horizontal">
            //    <Image Source="coffie.jpg" Height="30"></Image>
            //    <TextBlock Text="Coffie"></TextBlock>
            //</StackPanel>

            var t = item.GetType();
            string image;
            if (t == typeof(DatabaseExplorerServerViewModel))
            {
                image = "server";
            }
            else if (t == typeof(DatabaseExplorerDatabaseViewModel))
            {
                image = "database";
            }
            else 
            {
                image = "table";
            }
         
         


            var dt = new HierarchicalDataTemplate(t);
            dt.ItemsSource = new Binding("Children");
            var f = new FrameworkElementFactory(typeof(StackPanel));
            f.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
            var fi = new FrameworkElementFactory(typeof(Image));
            fi.SetValue(Image.SourceProperty, new BitmapImage(new Uri(@"pack://application:,,,/MongoMS;component/View/"+image+".png")));
 var ft = new FrameworkElementFactory(typeof(TextBlock));
            ft.SetBinding(TextBlock.TextProperty,new Binding("Name"));
            f.AppendChild(fi);
            f.AppendChild(ft);
            var menu = GetMenu(item.GetType());
            f.SetValue(FrameworkElement.ContextMenuProperty, menu);
            var fcc = new FrameworkElementFactory(typeof (ContentControl));
           fcc.AddHandler(Control.MouseDoubleClickEvent, new MouseButtonEventHandler(tb_MouseUp));
            fcc.AppendChild(f);
            dt.VisualTree = fcc;
            return dt;
        }
        ContextMenu GetMenu(Type t)
        {
            ContextMenu cm = new ContextMenu();
            var commands = t.GetProperties().Where(p => p.PropertyType == typeof(ICommand));
            foreach (var cmd in commands)
            {
                var mi = new MenuItem();
                var att = cmd.GetAttribute<WindowCommandAttribute>();
                mi.Header = att.Name;
                if (att.IsDefault)
                {
                    mi.FontWeight = FontWeights.Bold;
}
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
