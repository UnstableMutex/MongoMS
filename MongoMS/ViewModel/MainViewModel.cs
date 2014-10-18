using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace MongoMS.ViewModel
{
    class MainViewModel : ViewModelBase, INotifyPropertyChanging, ISaveable
    {
        private object _content;

        public MainViewModel()
        {
            Content = new ConnectionsViewModel();
            SimpleIoc.Default.Register<DatabaseExplorerViewModel>();
           
            Explorer = SimpleIoc.Default.GetInstance<DatabaseExplorerViewModel>();

        }


        public object Content
        {
            get { return _content; }
            set
            {
                SaveContent();
                _content = value;
                RaisePropertyChanged();
            }
        }

        private void SaveContent()
        {
            if (Content != null)
            {
                var saveable = Content as ISaveable;
                if (saveable != null)
                {
                    saveable.Save();
                }
            }

        }

        public event PropertyChangingEventHandler PropertyChanging;

        public void RaisePropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        public void Save()
        {
            SaveContent();
        }
        public DatabaseExplorerViewModel Explorer { get; private set; }
    }
}
