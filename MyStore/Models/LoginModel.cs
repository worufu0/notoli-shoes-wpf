using System.ComponentModel;
using PropertyChanged;

namespace MyStore.Models
{
    public class LoginModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public bool closeWindow { get; set; }
        public string appVersion { get; set; }
        public string username { get; set; }
        public bool rememberMe { get; set; }
    }
}
