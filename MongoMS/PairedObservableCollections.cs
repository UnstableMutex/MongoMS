using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace MongoMS
{
    class PairedObservableCollections<T>
    {
        public event EventHandler<SelectedEventArgs> Selected;
        public event EventHandler<UnSelectedEventArgs> UnSelected;

        public PairedObservableCollections()
        {
            Source = new ObservableCollection<T>();
            SelectedItems = new ObservableCollection<T>();
            SelectCommand = new RelayCommand(Select);
            UnSelectCommand = new RelayCommand(UnSelect);
        }

        public PairedObservableCollections(IEnumerable<T> source)
        {
            Source = new ObservableCollection<T>(source);
            SelectedItems = new ObservableCollection<T>();
            SelectCommand = new RelayCommand(Select);
            UnSelectCommand = new RelayCommand(UnSelect);
        }
        public PairedObservableCollections(IEnumerable<T> source,IEnumerable<T> selected)
        {
            Source = new ObservableCollection<T>(source);
            SelectedItems = new ObservableCollection<T>(selected);
            SelectCommand = new RelayCommand(Select);
            UnSelectCommand = new RelayCommand(UnSelect);
        }
        public ObservableCollection<T> Source { get; set; }
        public ObservableCollection<T> SelectedItems { get; set; }

         void Select()
         {

             var tmp = SourceSelected;
            Source.Remove(tmp);
            SelectedItems.Add(tmp);
            if (Selected != null)
            {
                Selected(this, new SelectedEventArgs(tmp));
            }
        }


         void UnSelect()
         {
             var tmp = SelectedSelected;
            SelectedItems.Remove(tmp);
            Source.Add(tmp);
            if (UnSelected != null)
            {
                UnSelected(this, new UnSelectedEventArgs(tmp));
            }
        }

        public T SourceSelected { get; set; }

        public T SelectedSelected { get; set; }
        public ICommand SelectCommand { get; private set; }
        public ICommand UnSelectCommand { get; private set; }
    }
}
