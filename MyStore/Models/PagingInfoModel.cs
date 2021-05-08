using PropertyChanged;
using System.ComponentModel;

namespace MyStore.Models
{
    public class PagingInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int totalItem { get; set; }
        public int filteredItem { get; set; }
        public int itemPerPage { get; set; }
        public int currentPage { get; set; }
        public int totalPage { get; set; }
    }
}
