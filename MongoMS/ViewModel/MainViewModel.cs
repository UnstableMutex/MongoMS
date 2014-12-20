using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    internal class MainViewModel : VMB, INotifyPropertyChanging, ISaveable
    {
        private ObservableCollection<object> _content;
        private object _selected;

        public MainViewModel()
        {
            Content = new ObservableCollection<object>();
            Content.CollectionChanged += Content_CollectionChanged;
            // Content.Add( new ConnectionsViewModel());
            SimpleIoc.Default.Register<DatabaseExplorerViewModel>();
            SettingsCommand = new OpenTabCommand(() => new SettingsViewModel());
            Explorer = SimpleIoc.Default.GetInstance<DatabaseExplorerViewModel>();
            AssignCommands<NoWeakRelayCommand>();
            CloseTabCommand = new RelayCommand<object>(ct);
        }

        public ICommand SettingsCommand { get; set; }

        public ICommand NewConnectionCommand { get; private set; }

        public object Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                RaisePropertyChangedNoSave();
            }
        }

        public ICommand CloseTabCommand { get; private set; }

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

        public DatabaseExplorerViewModel Explorer { get; private set; }

        public event PropertyChangingEventHandler PropertyChanging;

        public void Save()
        {
            SaveContent();
        }

        private void NewConnection()
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


        private void Content_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                int c = e.NewItems.Count;
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

        private void ct(object o)
        {
            Content.Remove(o);
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

        public void RaisePropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }
    }
}