using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace MongoMS
{
    internal class PairedObservableCollections<T>
    {
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

        public PairedObservableCollections(IEnumerable<T> source, IEnumerable<T> selected)
        {
            Source = new ObservableCollection<T>(source);
            SelectedItems = new ObservableCollection<T>(selected);
            SelectCommand = new RelayCommand(Select);
            UnSelectCommand = new RelayCommand(UnSelect);
        }

        public ObservableCollection<T> Source { get; set; }
        public ObservableCollection<T> SelectedItems { get; set; }

        public T SourceSelected { get; set; }

        public T SelectedSelected { get; set; }
        public ICommand SelectCommand { get; private set; }
        public ICommand UnSelectCommand { get; private set; }
        public event EventHandler<SelectedEventArgs> Selected;
        public event EventHandler<UnSelectedEventArgs> UnSelected;

        private void Select()
        {
            T tmp = SourceSelected;
            Source.Remove(tmp);
            SelectedItems.Add(tmp);
            if (Selected != null)
            {
                Selected(this, new SelectedEventArgs(tmp));
            }
        }


        private void UnSelect()
        {
            T tmp = SelectedSelected;
            SelectedItems.Remove(tmp);
            Source.Add(tmp);
            if (UnSelected != null)
            {
                UnSelected(this, new UnSelectedEventArgs(tmp));
            }
        }
    }
}