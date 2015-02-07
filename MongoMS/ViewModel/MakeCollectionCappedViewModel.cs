using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    [Header("Make Capped")]
    [CommandLevel(Level.Collection)]
    internal class MakeCollectionCappedViewModel : CollectionVMB
    {
        public MakeCollectionCappedViewModel(MongoCollection<BsonDocument> coll) : base(coll)
        {
            _isMBChecked = true;

            MaxSize = 1;
            UnitsCommand = new RelayCommand<string>(SetUnits);
        }


        [Int32Validator("Необходимо число")]
        public int MaxSize { get; set; }

        [Int32Validator("Необходимо число")]
        public int MaxCount { get; set; }

        public bool MaxCountEnabled
        {
            get { return _maxCountEnabled; }
            set
            {
                _maxCountEnabled = value;
                RaisePropertyChangedNoSave();
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
                _isBChecked = value;
                RaisePropertyChangedNoSave();
            }
        }

        public bool IsKBChecked
        {
            get { return _isKBChecked; }
            private set
            {
                _isKBChecked = value;
                RaisePropertyChangedNoSave();
            }
        }

        public bool IsMBChecked
        {
            get { return _isMBChecked; }
            private set
            {
                _isMBChecked = value;
                RaisePropertyChangedNoSave();
            }
        }

        public bool IsGBChecked
        {
            get { return _isGBChecked; }
            private set
            {
                _isGBChecked = value;
                RaisePropertyChangedNoSave();
            }
        }

        #endregion
    }
}