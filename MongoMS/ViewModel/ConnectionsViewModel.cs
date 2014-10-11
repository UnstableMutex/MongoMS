using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class ConnectionsViewModel:VMB
    {
        public ConnectionsViewModel()
        {
            AssignCommands<NoWeakRelayCommand>();
            Connections = new ObservableDictionary<string, string>();
        }

        void Load()
        {
            if (File.Exists(SettingsFileName))
            {
                XmlSerializer ser = new XmlSerializer(typeof(Dictionary<string, string>));
                var dic = (Dictionary<string, string>)ser.Deserialize(new StreamReader(SettingsFileName));
                Connections = new ObservableDictionary<string, string>(dic);
            }
        }
        const string SettingsFileName = "settings.xml";
        void Save()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            foreach (var connection in Connections)
            {
                d.Add(connection.Key, connection.Value);
            }

            XmlSerializer ser = new XmlSerializer(d.GetType());
            ser.Serialize(new StreamWriter(SettingsFileName, false), d);
        }

        public string NewCSServer { get; set; }



        public ObservableDictionary<string, string> Connections { get; private set; }
      
        public ICommand AddNewConnectionCommand { get; private set; }

        void AddNewConnection()
        {
            MongoConnectionStringBuilder sb=new MongoConnectionStringBuilder();
            sb.Server = new MongoServerAddress(NewCSServer);

            Connections.Add(NewCSName,sb.ConnectionString);
        }
        public string NewCSName { get; set; }
        public KeyValuePair<string,string> Selected { get; set; }
        public ICommand SelectCommand { get; private set; }

        void Select()
        {
            MessageBox.Show("selected"+ Selected.ToString());

        }
    }
}
