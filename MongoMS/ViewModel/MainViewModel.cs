using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace MongoMS.ViewModel
{
    class MainViewModel:ViewModelBase
    {
        private object _content;

        public MainViewModel()
        {
            Content = new ConnectionsViewModel();
        }

        public object Content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged();
            }
        }
       
    }
}
