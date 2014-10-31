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
using GalaSoft.MvvmLight.Ioc;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    [Header("Соединиться")]
    class ConnectionsViewModel : VMB, ISaveable
    {
        private KeyValuePair<string, string> _selected;
        private string _newCsServer;
        private string _newCsName;

        public ConnectionsViewModel()
        {
            AssignCommands<NoWeakRelayCommand>();
            Load();
        }

        void Load()
        {
            if (File.Exists(SettingsFileName))
            {
                XmlSerializer ser = new XmlSerializer(typeof(List<Entry>));
                using (var sr = new StreamReader(SettingsFileName))
                {
                    var dic = (List<Entry>)ser.Deserialize(sr);



                    var d = new ObservableDictionary<string, string>();
                    foreach (var entry in dic)
                    {
                        d.Add(entry.Name, entry.ConnectionString);
                    }
                    Connections = d;
                }

            }
            else
            {
                Connections = new ObservableDictionary<string, string>();
            }
        }
        const string SettingsFileName = "settings.xml";
        public void Save()
        {
            var d = new List<Entry>();
            foreach (var connection in Connections)
            {
                d.Add(new Entry(connection.Key, connection.Value));
            }

            XmlSerializer ser = new XmlSerializer(d.GetType());
            ser.Serialize(new StreamWriter(SettingsFileName, false), d);
        }


        public string NewCSServer
        {
            get { return _newCsServer; }
            set
            {
                _newCsServer = value;
                RaisePropertyChanged();
            }
        }


        public ObservableDictionary<string, string> Connections { get; private set; }
        public ICommand DeleteConnectionCommand { get; private set; }
        public ICommand AddNewConnectionCommand { get; private set; }

        void DeleteConnection()
        {
            Connections.Remove(Selected);
            Selected = default(KeyValuePair<string, string>);
        }
        void AddNewConnection()
        {
            MongoConnectionStringBuilder sb = new MongoConnectionStringBuilder();
            sb.Server = new MongoServerAddress(NewCSServer);

            Connections.Add(NewCSName, sb.ConnectionString);
        }

        public string NewCSName
        {
            get { return _newCsName; }
            set
            {
                _newCsName = value;
                RaisePropertyChangedNoSave();
            }
        }

        public KeyValuePair<string, string> Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                RaisePropertyChanged();
                OnSelectedChanged();
            }
        }

        private void OnSelectedChanged()
        {
            if (Selected.Equals(default(KeyValuePair<string, string>)))
            {
                NewCSServer = null;
                NewCSName = null;
            }
            else
            {
                MongoConnectionStringBuilder sb = new MongoConnectionStringBuilder(Selected.Value);
                NewCSServer = sb.Server.Host;
                NewCSName = Selected.Key;
            }
        }

        public ICommand SelectCommand { get; private set; }

        void Select()
        {

            var exp = SimpleIoc.Default.GetInstance<DatabaseExplorerViewModel>();
            if (exp.Servers.All(x => x.Name != Selected.Key))
            {
                exp.Servers.Add(new DatabaseExplorerServerViewModel(Selected.Key,new MongoClient( Selected.Value).GetServer()));
                SimpleIoc.Default.GetInstance<MainViewModel>().Content.Remove(this);
            }
            // exp.Servers.Add(new DatabaseExplorerServerViewModel(Selected.Key,  Selected.Value));
        }
    }
    [Serializable]
    public class Entry
    {
        public Entry()
        {

        }
        public Entry(string name, string cs)
        {
            Name = name;
            ConnectionString = cs;
        }
        public string Name { get; set; }

        public string ConnectionString { get; set; }
    }
}
