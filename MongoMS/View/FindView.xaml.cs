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
                var dc = (FindViewModel)((sender as DataGrid).DataContext);
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
            if (FindTextBox.Text == "{}")
                FindTextBox.SelectionStart = 1;
        }


        private void FindTextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {

          FindCriteriaInputController.OverrideInput(FindTextBox,e);
        }

        private void FindTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            /*
            http://professorweb.ru/my/WPF/base_WPF/level5/5_8.php
             textcompositionmanager
             почитать
            
             Наиболее подходящим вариантом является обработка события PreviewTextlnput (где выполняется большая часть проверки) 
             в сочетании с событием PreviewKeyDown для нажатий тех клавиш, которые не генерируют событие PreviewTextInput в текстовом поле (например, пробела).
            
             */
        }
    }

   static class FindCriteriaInputController
    {
       public static void OverrideInput(TextBox tb, KeyEventArgs eventArgs)
       {
           if (eventArgs.Key == Key.Oem7&&tb.SelectionLength==0) // ' key
           {
               WorkOEM7(tb, eventArgs);
               return;
           }
       }

       private static void WorkOEM7(TextBox tb, KeyEventArgs eventArgs)
       {
           //var text = tb.Text;
           //var start = text.Substring(0, tb.SelectionStart);
           //var end = text.Substring(tb.SelectionStart);
           //var newText = start + "''" + end;
           //tb.Text = newText;
           //tb.SelectionStart = tb.SelectionStart + 1;
           //eventArgs.Handled = true;
       }
    }

}