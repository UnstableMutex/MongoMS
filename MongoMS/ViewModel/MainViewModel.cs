using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace MongoMS.ViewModel
{
    class MainViewModel:ViewModelBase
    {
        private object _content;

        public MainViewModel()
        {
            
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
