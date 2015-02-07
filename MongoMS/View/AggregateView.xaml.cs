using System.Windows;
using System.Windows.Controls;

namespace MongoMS.View
{
    /// <summary>
    ///     Логика взаимодействия для AggregateView.xaml
    /// </summary>
    public partial class AggregateView : UserControl
    {
        public AggregateView()
        {
            InitializeComponent();
        }

        private void AggregateView_OnLoaded(object sender, RoutedEventArgs e)
        {
            FocusedTextBox.Focus();
        }
    }
}