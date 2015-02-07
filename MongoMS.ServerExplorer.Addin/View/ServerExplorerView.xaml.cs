using System.Windows.Controls;
using MongoMS.ServerExplorer.Addin.ViewModel;

namespace MongoMS.ServerExplorer.Addin.View
{
    /// <summary>
    /// Логика взаимодействия для ServerExplorerView.xaml
    /// </summary>
    public partial class ServerExplorerView : UserControl
    {
        public ServerExplorerView(ServerExplorerViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
