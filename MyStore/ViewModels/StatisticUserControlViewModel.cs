using LiveCharts;
using LiveCharts.Wpf;
using MyStore.Helpers;
using MyStore.Models;
using MyStore.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace MyStore.ViewModels
{
    public class StatisticUserControlViewModel
    {
        public StatisticUserControlModel model { get; set; }
        public ICommand ChangeMonthYear { get; set; }

        public StatisticUserControlViewModel()
        {
            var now = DateTime.Now;

            model = new StatisticUserControlModel
            {
                days = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16",
                    "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" },
                months = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },
                years = new[] { 2018, 2019, 2020, 2021 },
                monthNow = now.Month,
                yearNow = now.Year,
                customerCount = DataProvider.instance.database.Customers.Count(),
                productCount = DataProvider.instance.database.Products.Count(),
                purchaseCount = DataProvider.instance.database.Purchases.Count(),
                earning = (int)DataProvider.instance.database.Purchases.Select(pc => pc).Where(pc => pc.PurchaseStatus == 3).Sum(pc => pc.PurchaseTotal),
                maxAmplitude = 0,
                averageSales = 0,
                totalSales = 0,
                categoryTable = new ObservableCollection<Category>(DataProvider.instance.database.Categories)
            };

            ChangeMonthYear = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as StatisticUserControlView;

                if (target.monthComboBox.SelectedValue != null && target.yearComboBox.SelectedValue != null)
                {
                    model.salesSeriesCollection = new SeriesCollection
                    {
                        new LineSeries
                        {
                            Values = GetSalesChartValues((int)target.monthComboBox.SelectedValue, (int)target.yearComboBox.SelectedValue),
                            PointGeometry = null
                        }
                    };

                    model.salesLabels = model.days;
                    model.salesLabelFormatter = value => value.ToString("N");

                    var products = DataProvider.instance.database.Purchases.Join(
                        DataProvider.instance.database.PurchaseDetails,
                        pur => pur.PurchaseID,
                        pd => pd.PurchaseDetailPurchase,
                        (pur, pd) => new
                        {
                            ProductName = pd.Product.ProductName,
                            ProductSold = pd.PurchaseDetailQuantity,
                            PurchaseReceivedDate = pur.PurchaseReceivedDate,
                            PurchaseStatus = pur.PurchaseStatus
                        })
                        .Select(pur => pur)
                        .Where(pur =>
                        pur.PurchaseReceivedDate.Value.Year == (int)target.yearComboBox.SelectedValue &&
                        pur.PurchaseReceivedDate.Value.Month == (int)target.monthComboBox.SelectedValue &&
                        pur.PurchaseStatus == 3);

                    var topProducts = products
                        .GroupBy(t => t.ProductName)
                        .Select(t => new { ProductName = t.Key, SumSold = t.Sum(pd => pd.ProductSold) })
                        .OrderByDescending(t => t.SumSold)
                        .Take(3)
                        .ToList();

                    model.productSeriesCollection = new SeriesCollection();

                    if (topProducts.Count == 0)
                        for (int i = 0; i <= 2; i++)
                            model.productSeriesCollection.Add(new PieSeries { Title = "None", Values = new ChartValues<double> { 1 } });
                    else
                    {   
                        foreach (var product in topProducts)
                        {
                            var values = new ChartValues<double> { (double)product.SumSold };
                            model.productSeriesCollection.Add(new PieSeries { Title = product.ProductName, Values = values, DataLabels = true });
                        }
                    }
                }
            });
        }

        private ChartValues<double> GetSalesChartValues(int month, int year)
        {
            var result = new ChartValues<double> { };
            var purchases = DataProvider.instance.database.Purchases
                .Select(pur => pur)
                .Where(pur =>
                pur.PurchaseReceivedDate.Value.Year == year &&
                pur.PurchaseReceivedDate.Value.Month == month &&
                pur.PurchaseStatus == 3)
                .ToList();

            for (int i = 0; i <= 30; i++)
                result.Add(0.0);

            int max = 0, min = 0, sum = 0, count = 0;
            foreach (var purchase in purchases)
            {
                var day = purchase.PurchaseReceivedDate.Value.Day;
                var temp = result[day - 1];

                temp += purchase.PurchaseTotal.Value;
                result[day - 1] = temp;

                if (result[day - 1] > max)
                    max = (int)result[day - 1];
                if (result[day - 1] < min)
                    min = (int)result[day - 1];
                sum += (int)result[day - 1];
                count++;
            }

            model.maxAmplitude = max - min;
            if (count != 0)
                model.averageSales = (double)sum / count;
            else
                model.averageSales = 0;
            model.totalSales = sum;

            return result;
        }
    }
}
