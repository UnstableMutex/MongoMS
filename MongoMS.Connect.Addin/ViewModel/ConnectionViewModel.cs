using System.Reflection.Emit;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using MongoDB.Driver;
using MongoMS.Common;
using MongoMS.Common.Events;

namespace MongoMS.Connect.Addin.ViewModel
{
    public class ConnectionViewModel : OKViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private string _name;
        public ConnectionViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            CS = new MongoConnectionStringBuilder();
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                SetProperty(ref _name, value);
            }
        }
        public MongoConnectionStringBuilder CS { get; set; }
        protected override void OK()
        {
            var e = _eventAggregator.GetEvent<PubSubEvent<RequestConnect>>();
            e.Publish(new RequestConnect(Name, CS.ConnectionString));
        }
    }
}