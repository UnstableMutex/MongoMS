﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GongSolutions.Wpf.DragDrop;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoMS.Common;

namespace MongoMS.GridFS.Addin.ViewModel
{
    public class GridFSViewModel : BindableBase, IDropTarget, ITabContent
    {
        public string Header { get { return "GridFS"; } }
        private readonly MongoDatabase _database;
        private readonly IUnityContainer _unity;
        private readonly IEventAggregator _eventAggregator;
        private string _key;
        private GridFSFileViewModel _selectedFile;

        public GridFSViewModel(MongoDatabase database, IUnityContainer unity, IEventAggregator eventAggregator)
        {
            _database = database;
            _unity = unity;
            _eventAggregator = eventAggregator;
            AddCommand = new DelegateCommand(Add, CanAdd);
            var fa = _database.GridFS.FindAll();
            Files = new ObservableCollection<GridFSFileViewModel>();
            foreach (MongoGridFSFileInfo fileInfo in fa)
            {
                Files.Add(new GridFSFileViewModel(fileInfo));
            }

        }



        public ICommand AddCommand { get; private set; }

        private void Add()
        {
            Upload();
        }

        bool CanAdd()
        {
            return true;
        }




        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            var dt = dropInfo.Data as IDataObject;

            if (dt.GetDataPresent(DataFormats.FileDrop))
            {
                dropInfo.Effects = DragDropEffects.Copy;
            }
            else
            {
                dropInfo.Effects = DragDropEffects.None;
            }
        }

        public string Key
        {
            get { return _key; }
            set { SetProperty(ref _key, value); }
        }

        string FileName { get; set; }
        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            var a = dropInfo.Data as IDataObject;
            var file = a.GetData(DataFormats.FileDrop);
            var fn = file as string[];
            var f = fn[0];
            Key = FileName = f;
        }

        void Upload()
        {
            var uploadresult = _database.GridFS.Upload(FileName, Key);
            Files.Add(new GridFSFileViewModel(uploadresult));
            Key = FileName = null;
        }
        public ObservableCollection<GridFSFileViewModel> Files { get; private set; }

        public GridFSFileViewModel SelectedFile
        {
            get { return _selectedFile; }
            set { SetProperty(ref _selectedFile, value); }
        }
    }

    public class GridFSFileViewModel
    {
        private readonly MongoGridFSFileInfo _fileInfo;

        public GridFSFileViewModel(MongoGridFSFileInfo fileInfo)
        {
            _fileInfo = fileInfo;
        }
        public string Name { get { return _fileInfo.Name; } }
        public DateTime CreateDate { get { return _fileInfo.UploadDate; } }
    }
}
