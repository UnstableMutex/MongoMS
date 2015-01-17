using System.Windows.Controls;
using MongoMS.Find.Addin.ViewModel;

namespace MongoMS.Find.Addin.View
{
    /// <summary>
    /// Логика взаимодействия для FindView.xaml
    /// </summary>
    public partial class FindView : UserControl
    {
        public FindView(FindViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
