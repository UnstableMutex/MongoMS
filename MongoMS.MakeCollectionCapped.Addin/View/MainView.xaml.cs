using System.Windows.Controls;
using MongoMS.MakeCollectionCapped.Addin.ViewModel;

namespace MongoMS.MakeCollectionCapped.Addin.View
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
