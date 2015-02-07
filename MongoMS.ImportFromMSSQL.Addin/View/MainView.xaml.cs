using System.Windows.Controls;
using MongoMS.ImportFromMSSQL.Addin.ViewModel;

namespace MongoMS.ImportFromMSSQL.Addin.View
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
