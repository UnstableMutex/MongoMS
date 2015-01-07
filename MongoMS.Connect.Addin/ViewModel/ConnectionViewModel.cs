using System.Reflection.Emit;
using Microsoft.Practices.Prism.Mvvm;
using MongoDB.Driver;
namespace MongoMS.Connect.Addin.ViewModel
{
    public class ConnectionViewModel : BindableBase
    {
        public static ConnectionViewModel CreateDefault()
        {
            var c = new ConnectionViewModel("server=localhost");
            c.Name = "local";
            return c;
        }
        private string _name;
        public ConnectionViewModel()
        {
            CS = new MongoConnectionStringBuilder();
        }
        public ConnectionViewModel(string cs)
        {
            CS = new MongoConnectionStringBuilder(cs);
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
    }
}