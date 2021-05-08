using System.ComponentModel;
using PropertyChanged;

namespace MyStore.Models
{
    public class MyMessageBoxModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int boxType { get; set; }
        public string content { get; set; }
    }
}
