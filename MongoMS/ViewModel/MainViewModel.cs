using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class MainViewModel : VMB, INotifyPropertyChanging, ISaveable
    {
        private ObservableCollection<object> _content;
        private object _selected;

        public MainViewModel()
        {
            Content = new ObservableCollection<object>();
            Content.CollectionChanged += Content_CollectionChanged;
           // Content.Add( new ConnectionsViewModel());
            SimpleIoc.Default.Register<DatabaseExplorerViewModel>();
           
            Explorer = SimpleIoc.Default.GetInstance<DatabaseExplorerViewModel>();
            AssignCommands<NoWeakRelayCommand>();

        }
        public ICommand NewConnectionCommand { get; private set; }

        void NewConnection()
        {
            if (Content.OfType<ConnectionsViewModel>().Count() == 0)
            {
                Content.Add(new ConnectionsViewModel());
            }
            else if (Content.OfType<ConnectionsViewModel>().Count() == 1)
            {
                Selected = Content.OfType<ConnectionsViewModel>().Single();
            }
        }
        void Content_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                var c = e.NewItems.Count;
                if (c > 0)
                {
                    Selected = e.NewItems[0];
                }
            }
            else
            {
                if (e.OldItems.Contains(Selected))
                {
                    Selected = null;
                }
            }
        }

        public object Selected

        {
            get { return _selected; }
            set
            {
                _selected = value;
                RaisePropertyChangedNoSave();
            }
        }

        public ObservableCollection<object> Content
        {
            get { return _content; }
            set
            {
                SaveContent();
                _content = value;
                RaisePropertyChangedNoSave();
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
