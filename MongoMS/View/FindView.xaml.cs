using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MongoDB.Bson;
using MongoMS.ViewModel;

namespace MongoMS.View
{
    /// <summary>
    ///     Логика взаимодействия для FindView.xaml
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


        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void FindView_OnInitialized(object sender, EventArgs e)
        {
            FocusManager.SetFocusedElement(this, FindTextBox);
        }
    }
}