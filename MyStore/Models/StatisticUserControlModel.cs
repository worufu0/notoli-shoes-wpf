using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using LiveCharts;
using PropertyChanged;

namespace MyStore.Models
{
    public class StatisticUserControlModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string[] days { get; set; }
        public int[] months { get; set; }
        public int[] years { get; set; }
        public int monthNow { get; set; }
        public int yearNow { get; set; }
        public int customerCount { get; set; }
        public int productCount { get; set; }
        public int purchaseCount { get; set; }
        public int earning { get; set; }
        public SeriesCollection salesSeriesCollection { get; set; }
        public string[] salesLabels { get; set; }
        public Func<double, string> salesLabelFormatter { get; set; }
        public int maxAmplitude { get; set; }
        public double averageSales { get; set; }
        public int totalSales { get; set; }
        public SeriesCollection productSeriesCollection { get; set; }
        public string[] productLabels { get; set; }
        public ObservableCollection<Category> categoryTable { get; set; }
        public StatisticUserControlModel()
        {
        }
    }
}
