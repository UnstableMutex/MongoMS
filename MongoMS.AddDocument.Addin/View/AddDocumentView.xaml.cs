using System.Windows.Controls;
using MongoMS.AddDocument.Addin.ViewModel;

namespace MongoMS.AddDocument.Addin.View
{
    /// <summary>
    /// Логика взаимодействия для AddDocumentView.xaml
    /// </summary>
    public partial class AddDocumentView : UserControl
    {
        public AddDocumentView(AddDocumentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
