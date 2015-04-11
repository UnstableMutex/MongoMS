using System;
using System.Collections.Generic;
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

namespace MongoMS.GridFS.Addin.ViewModel
{
    class GridFSViewModel : BindableBase,IDropTarget
    {
        private readonly MongoDatabase _database;
        private readonly IUnityContainer _unity;
        private readonly IEventAggregator _eventAggregator;
        public GridFSViewModel(MongoDatabase database, IUnityContainer unity, IEventAggregator eventAggregator)
        {
            _database = database;
            _unity = unity;
            _eventAggregator = eventAggregator;
         AddCommand = new DelegateCommand(Add,CanAdd);
        }

        public void AddFile(string fn)
        {
            _database.GridFS.Upload(fn);
        }

        public ICommand AddCommand { get; private set; }

        private void Add()
        {
          
        }

        bool CanAdd()
        {
            return true;
        }




        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
          
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            var a = dropInfo;
        }
    }
}
