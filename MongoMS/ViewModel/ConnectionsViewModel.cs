using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Xml.Serialization;
using GalaSoft.MvvmLight.Ioc;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    [Header("Соединиться")]
    internal class ConnectionsViewModel : VMB, ISaveable
    {
        private const string SettingsFileName = "settings.xml";
        private string _newCsName;
        private string _newCsServer;
        private KeyValuePair<string, string> _selected;

        public ConnectionsViewModel()
        {
            AssignCommands<NoWeakRelayCommand>();
            Load();
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

        public ICommand SelectCommand { get; private set; }

        public void Save()
        {
            var d = new List<Entry>();
            foreach (var connection in Connections)
            {
                d.Add(new Entry(connection.Key, connection.Value));
            }

            var ser = new XmlSerializer(d.GetType());
            ser.Serialize(new StreamWriter(SettingsFileName, false), d);
        }

        private void Load()
        {
            if (File.Exists(SettingsFileName))
            {
                var ser = new XmlSerializer(typeof (List<Entry>));
                using (var sr = new StreamReader(SettingsFileName))
                {
                    var dic = (List<Entry>) ser.Deserialize(sr);


                    var d = new ObservableDictionary<string, string>();
                    foreach (Entry entry in dic)
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

        private void DeleteConnection()
        {
            Connections.Remove(Selected);
            Selected = default(KeyValuePair<string, string>);
        }

        private void AddNewConnection()
        {
            var sb = new MongoConnectionStringBuilder();
            sb.Server = new MongoServerAddress(NewCSServer);

            Connections.Add(NewCSName, sb.ConnectionString);
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
                var sb = new MongoConnectionStringBuilder(Selected.Value);
                NewCSServer = sb.Server.Host;
                NewCSName = Selected.Key;
            }
        }

        private void Select()
        {
            var exp = SimpleIoc.Default.GetInstance<DatabaseExplorerViewModel>();
            if (exp.Servers.All(x => x.Name != Selected.Key))
            {
                exp.Servers.Add(new DatabaseExplorerServerViewModel(Selected.Key,
                    new MongoClient(Selected.Value).GetServer()));
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