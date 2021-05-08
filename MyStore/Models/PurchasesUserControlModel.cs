using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using PropertyChanged;

namespace MyStore.Models
{
    public class PurchasesUserControlModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Category> categoryTable { get; set; }
        public List<Customer> customerTable { get; set; }
        public List<Status> statusTable { get; set; }
        public CollectionView collectionView { get; set; }
        public bool chosen { get; set; }
        public PurchasesUserControlModel()
        {
        }
    }
}
