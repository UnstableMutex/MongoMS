using System;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using MongoMS.Common;
using MongoMS.CreateCollection.Addin.ViewModel;

namespace MongoMS.CreateCollection.Addin.View
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        private readonly IRegionManager _regionManager;

        public MainView(MainViewModel viewModel, IRegionManager regionManager)
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
