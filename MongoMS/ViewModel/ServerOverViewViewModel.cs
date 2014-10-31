using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoMS.ViewModel
{
    class ServerOverViewViewModel:OKVMB
    {
        private readonly MongoServer _serv;

        public ServerOverViewViewModel(MongoServer serv)
        {
            _serv = serv;
        }
    }
}
