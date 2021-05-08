using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using PropertyChanged;

namespace MyStore.Models
{
    public class CustomersUserControlModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Customer> customerTable{ get; set; }
        public CollectionView collectionView { get; set; }
        public bool chosen { get; set; }
        public CustomersUserControlModel()
        {
        }
    }
}
