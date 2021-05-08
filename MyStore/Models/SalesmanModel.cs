using System.ComponentModel;
using PropertyChanged;

namespace MyStore.Models
{
    public class SalesmanModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string name { get; set; }
        public string role { get; set; }
        public bool closeWindow { get; set; }
    }
}
