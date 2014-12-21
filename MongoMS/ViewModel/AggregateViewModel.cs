using System.Collections.ObjectModel;
using System.Windows.Input;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoMS.ViewModel.Aggregate;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    [Header("Агрегация")]
    [CollectionLevelCommand]
    internal class AggregateViewModel : CollectionVMB
    {
        public AggregateViewModel(MongoCollection<BsonDocument> coll)
            : base(coll)
        {
            Steps = new ObservableCollection<AggregateStepViewModel>();
            AssignCommands<NoWeakRelayCommand>();
        }
        public ObservableCollection<AggregateStepViewModel> Steps { get; private set; }
        public AggregateStepViewModel Selected { get; set; }
        public string Json { get; set; }
        public ICommand AddStepCommand { get; private set; }

        private void AddStep()
        {
            Steps.Add(new AggregateStepViewModel(Json));
            Json = null;
            RaisePropertyChanged("Json");
        }
    }
}