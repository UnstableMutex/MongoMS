using System.Windows.Controls;
using MongoMS.Aggregate.Addin.ViewModel;

namespace MongoMS.Aggregate.Addin.View
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

    }
}
