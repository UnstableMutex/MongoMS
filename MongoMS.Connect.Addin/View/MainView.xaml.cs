using System;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using MongoMS.Common;
using MongoMS.Connect.Addin.ViewModel;

namespace MongoMS.Connect.Addin.View
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
        }
        private void Lb_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                dynamic dc = (sender as ListBox).DataContext;
                (dc.OKCommand as ICommand).Execute(null);
                _regionManager.Regions[RegionNames.TabControlRegion].Remove(this);

            }
            catch (Exception)
            {
            }
        }

      
    }
}
