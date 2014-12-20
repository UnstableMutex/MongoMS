using System;
using System.Linq;
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

        private void FindView_OnLoaded(object sender, RoutedEventArgs e)
        {
              FocusManager.SetFocusedElement(this, FindTextBox);
            Keyboard.Focus(FindTextBox);
        }

        private void FindTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (FindTextBox.Text=="{}")
            FindTextBox.SelectionStart = 1;
        }
        private void FindTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var selstart = FindTextBox.SelectionStart;
            var text = FindTextBox.Text;
            var firstchange = e.Changes.First();
            var start = text.Substring(0, firstchange.Offset);
            var end = text.Substring(firstchange.Offset + 1);

            var addedsymb = text.Substring(firstchange.Offset, 1);
            switch (addedsymb)
            {
                case "'":
                   FindTextBox.  Text = start + "''" + end;
                     FindTextBox.SelectionStart = selstart;
                    break;
                default:
                    return;
            }
        
        }

       
    }
}