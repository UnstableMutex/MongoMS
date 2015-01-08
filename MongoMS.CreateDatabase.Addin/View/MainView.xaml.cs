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
using Microsoft.Practices.Prism.Regions;
using MongoMS.Common;
using MongoMS.CreateDatabase.Addin.ViewModel;

namespace MongoMS.CreateDatabase.Addin.View
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        private readonly IRegionManager _regionManager;

        public MainView(MainViewModel viewModel,IRegionManager regionManager)
        {
            _regionManager = regionManager;
            InitializeComponent();

            DataContext = viewModel;
            viewModel.CloseRequest += viewModel_CloseRequest;

        }

        void viewModel_CloseRequest(object sender, EventArgs e)
        {
            _regionManager.Regions[RegionNames.TabControlRegion].Remove(this);
        }
    }
}
