using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MongoDB.Bson;
using MongoMS.ViewModel;

namespace MongoMS.View
{
    /// <summary>
    /// Логика взаимодействия для FindView.xaml
    /// </summary>
    public partial class FindView : UserControl
    {
        public FindView()
        {
            InitializeComponent();
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(cdg.GetBindingExpression(DataGrid.SelectedItemProperty).ResolvedSourcePropertyName);
        }

        private void Cdg_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var dc = (FindViewModel) ((sender as DataGrid).DataContext);
                dc.Selected = e.AddedItems[0] as BsonDocument;
            }
            catch (IndexOutOfRangeException exception)
            {
               // Console.WriteLine(exception);
            }

        }
    }
   
   
}
