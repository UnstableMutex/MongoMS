using MongoDB.Driver;

namespace MongoMS.ViewModel
{
    [Header("Инфо о сервере")]
    internal class ServerOverViewViewModel : OKVMB
    {
        private readonly MongoServer _serv;

        public ServerOverViewViewModel(MongoServer serv)
        {
            _serv = serv;
        }
    }
}