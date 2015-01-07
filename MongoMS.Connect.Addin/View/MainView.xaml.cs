using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.Commands;
using MongoMS.Connect.Addin.ViewModel;

namespace MongoMS.Connect.Addin.View
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
        private void Lb_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                dynamic dc = (sender as ListBox).DataContext;
                (dc.OKCommand as ICommand).Execute(null);
            }
            catch (Exception)
            {
            }
        }

      
    }
}
