using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoMS.ViewModel.Aggregate
{
    class AggregateStepViewModel
    {
        private  string _json;
        public ushort Position { get; set; }
        public bool Checked { get; set; }
        public string Json
        {
            get { return _json; }
            set { _json = value; }
        }
        public AggregateStepViewModel(string json)
        {
            Json = json;
        }
    }
}
