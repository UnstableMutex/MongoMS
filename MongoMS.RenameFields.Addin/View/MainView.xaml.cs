using System.Windows.Controls;
using MongoMS.RenameFields.Addin.ViewModel;

namespace MongoMS.RenameFields.Addin.View
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
