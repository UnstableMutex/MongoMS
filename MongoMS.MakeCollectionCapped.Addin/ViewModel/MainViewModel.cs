using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoMS.Common;

namespace MongoMS.MakeCollectionCapped.Addin.ViewModel
{
    public class MainViewModel:OKViewModel
    {
        private readonly MongoCollection _coll;

        public MainViewModel(MongoCollection collection)
            
        {
            _coll = collection;
            _isMBChecked = true;
            MaxSize = 1;
            UnitsCommand = new  DelegateCommand<string>(SetUnits);
        }

        public override string Header
        {
            get { return "MakeCapped"; }
        }

        // [Int32Validator("Необходимо число")]
        public int MaxSize { get; set; }

       // [Int32Validator("Необходимо число")]
        public int MaxCount { get; set; }

        public bool MaxCountEnabled
        {
            get { return _maxCountEnabled; }
            set
            {
               
                SetProperty(ref _maxCountEnabled, value);
            }
        }

        public ICommand UnitsCommand { get; private set; }
        private int Multiplier { get; set; }

        protected override void OK()
        {
            //db.runCommand({"convertToCapped": "mycoll", size: 100000});  ? не поддерживает maxdocs
            Guid g = Guid.NewGuid();
            string s = g.ToString();
            var b = new CollectionOptionsBuilder();
            b.SetCapped(true).SetMaxDocuments(MaxCount).SetMaxSize(MaxSize);
            _coll.Database.CreateCollection(s, b);
            MongoCollection<BsonDocument> newcoll = _coll.Database.GetCollection(s);
            foreach (BsonDocument item in _coll.FindAllAs<BsonDocument>())
            {
                newcoll.Save(item);
            }
            _coll.Drop();
        }

        private void SetUnits(string unitPow1)
        {
            byte unitPow = byte.Parse(unitPow1);
            Multiplier = (int) Math.Pow(1024, unitPow);
            switch (unitPow)
            {
                case 0:
                    IsBChecked = true;
                    IsKBChecked = false;
                    IsMBChecked = false;
                    IsGBChecked = false;

                    break;
                case 1:
                    IsBChecked = false;
                    IsKBChecked = true;
                    IsMBChecked = false;
                    IsGBChecked = false;

                    break;
                case 2:
                    IsBChecked = false;
                    IsKBChecked = false;
                    IsMBChecked = true;
                    IsGBChecked = false;

                    break;
                case 3:
                    IsBChecked = false;
                    IsKBChecked = false;
                    IsMBChecked = false;
                    IsGBChecked = true;

                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        #region

        private bool _isBChecked;
        private bool _isGBChecked;
        private bool _isKBChecked;
        private bool _isMBChecked;
        private bool _maxCountEnabled;

        public bool IsBChecked
        {
            get { return _isBChecked; }
            private set
            {
                SetProperty(ref _isBChecked, value);
            }
        }

        public bool IsKBChecked
        {
            get { return _isKBChecked; }
            private set
            {
                SetProperty(ref _isKBChecked,value);
            }
        }

        public bool IsMBChecked
        {
            get { return _isMBChecked; }
            private set
            {
                _isMBChecked = value;
                SetProperty(ref _isMBChecked,value);
            }
        }

        public bool IsGBChecked
        {
            get { return _isGBChecked; }
            private set
            {
                _isGBChecked = value;
                SetProperty(ref _isGBChecked,value);
            }
        }

        #endregion
    }
}
