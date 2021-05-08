using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using PropertyChanged;

namespace MyStore.Models
{
    public class InventoryUserControlModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Category> categoryTable { get; set; }
        public CollectionView collectionView { get; set; }
        public PagingInfo pagingInfo { get; set; }
        public bool chosen { get; set; }
        public InventoryUserControlModel()
        {
        }
    }
}
