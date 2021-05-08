using System.ComponentModel;
using PropertyChanged;

namespace MyStore.Models
{
    public class ChangeDatabaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public bool closeWindow { get; set; }
        public string serverName { get; set; }
        public string databaseName { get; set; }
    }
}
