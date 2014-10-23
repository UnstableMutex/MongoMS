using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
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

            var f = new FrameworkElementFactory(typeof(Label));
            f.SetBinding(Label.ContentProperty, new Binding("Name"));
            f.AddHandler(Label.MouseDoubleClickEvent, new MouseButtonEventHandler(tb_MouseUp));
            ContextMenu cm = new ContextMenu();



            switch (i.Type)
            {
                case ItemType.Server:
                    cm.Items.Add(AddDBMI());
                    f.SetValue(FrameworkElement.ContextMenuProperty, cm);
                    break;

                case ItemType.Database:
                    cm.Items.Add(AddCollMI());
                    cm.Items.Add(DropDBMI());
 cm.Items.Add(ExportMSSQLMI());
                    f.SetValue(FrameworkElement.ContextMenuProperty, cm);
                    break;

                case ItemType.Collection:
                    cm.Items.Add(FindInCollMI());
                    cm.Items.Add(CollectionStatsMI());
                    cm.Items.Add(AddDocMI());
                    cm.Items.Add(AggregateMI());
                    cm.Items.Add(DropMI());
                    f.SetValue(Label.ContextMenuProperty, cm);
                    break;



            }

            dt.VisualTree = f;
            return dt;
        }

        private MenuItem ExportMSSQLMI()
        {
              MenuItem mi = new MenuItem();

            mi.Header = "export mssql";
             mi.SetBinding(MenuItem.CommandProperty, new Binding("ExportFromMSSQLCommand"));
            return mi;

        }

        private MenuItem DropDBMI()
        {
            MenuItem mi = new MenuItem();

            mi.Header = "Drop";
             mi.SetBinding(MenuItem.CommandProperty, new Binding("DropCommand"));
            return mi;

        }

        private MenuItem DropMI()
        {
            MenuItem mi = new MenuItem();

            mi.Header = "Drop";
            mi.SetBinding(MenuItem.CommandProperty, new Binding("DropCommand"));
            return mi;

        }

        private MenuItem AggregateMI()
        {
            MenuItem mi = new MenuItem();

            mi.Header = "Aggregate...";
            mi.SetBinding(MenuItem.CommandProperty, new Binding("AggregateCommand"));
            return mi;
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
        private MenuItem AddDocMI()
        {
            MenuItem mi = new MenuItem();

            mi.Header = "add doc";
            mi.SetBinding(MenuItem.CommandProperty, new Binding("AddDocumentCommand"));
            return mi;
        }

        private MenuItem CollectionStatsMI()
        {
            MenuItem mi = new MenuItem();
            mi.Header = "stats";
            mi.SetBinding(MenuItem.CommandProperty, new Binding("StatsCommand"));
            return mi;
        }

        private MenuItem FindInCollMI()
        {
            MenuItem mi = new MenuItem();
            mi.FontWeight = FontWeights.Bold;
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


