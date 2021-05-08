using MyStore.Helpers;
using MyStore.Models;
using MyStore.Views;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace MyStore.ViewModels
{
    public class PurchasesUserControlViewModel
    {
        public PurchasesUserControlModel model { get; set; }
        public ICommand SearchByID { get; set; }
        public ICommand FilterByOrderDate { get; set; }
        public ICommand PurchaseStatusChanged { get; set; }
        public ICommand PopupAddPurchase { get; set; }
        public ICommand AddPurchase { get; set; }
        public ICommand DeletePurchase { get; set; }
        public ICommand PopupUpdatePurchase { get; set; }
        public ICommand UpdatePurchase { get; set; }
        public ICommand PopupAddPurchaseDetail { get; set; }
        public ICommand AddPurchaseDetail { get; set; }
        public ICommand DeletePurchaseDetail { get; set; }
        public ICommand PopupUpdatePurchaseDetail { get; set; }
        public ICommand UpdatePurchaseDetail { get; set; }
        public PurchasesUserControlViewModel()
        {
            model = new PurchasesUserControlModel
            {
                customerTable = DataProvider.instance.database.Customers.ToList(),
                categoryTable = DataProvider.instance.database.Categories.ToList(),
                statusTable = DataProvider.instance.database.Status.ToList()
            };

            FilterByOrderDate = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as PurchasesUserControlView;

                model.collectionView = (CollectionView)CollectionViewSource.GetDefaultView(target.purchaseDataGrid.ItemsSource);
                model.collectionView.Filter = null;
                model.collectionView.Filter = new Predicate<object>(purchase =>
                    ((Purchase)purchase).PurchaseDate <= target.filterToDatePicker.SelectedDate &&
                    ((Purchase)purchase).PurchaseDate >= target.filterFromDatePicker.SelectedDate);
            });

            SearchByID = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as PurchasesUserControlView;

                if (target.purchaseDataGrid.ItemsSource != null)
                {
                    model.collectionView = (CollectionView)CollectionViewSource.GetDefaultView(target.purchaseDataGrid.ItemsSource);
                    model.collectionView.Filter = new Predicate<object>(purchase => ((Purchase)purchase).PurchaseID.ToString().Contains(target.searchBox.Text));
                }
            });

            PopupAddPurchase = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as PurchasesUserControlView;

                if (target.addPurchaseToggleButton.IsChecked == true)
                    target.addPurchasePopup.IsOpen = true;
                else
                {
                    target.popupAddPurchaseStatusComboBox.SelectedIndex = 0;
                    target.popupAddPurchasePlaceTextBox.Clear();
                    target.popupAddPurchaseReceivedDatePicker.SelectedDate = null;
                    target.addPurchasePopup.IsOpen = false;
                }
            });

            AddPurchase = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as PurchasesUserControlView;

                var savedIndex = target.customerComboBox.SelectedIndex;
                target.addPurchasePopup.IsOpen = false;
                target.addPurchaseDetailToggleButton.IsChecked = false;

                var newPurchase = new Purchase
                {
                    PurchaseID = NextPurchaseIDGenerate(),
                    PurchaseCustomer = (int)target.customerComboBox.SelectedValue,
                    PurchaseDate = DateTime.Today,
                    PurchaseStatus = (int)target.popupAddPurchaseStatusComboBox.SelectedValue,
                    PurchaseExpectedDeliveryDate = DateTime.Today.AddDays(DateTime.Today.Day + 7),
                    PurchaseReceivedDate = target.popupAddPurchaseReceivedDatePicker.SelectedDate,
                    PurchasePlace = target.popupAddPurchasePlaceTextBox.Text
                };

                DataProvider.instance.database.Purchases.Add(newPurchase);
                DataProvider.instance.database.SaveChanges();

                model.customerTable = DataProvider.instance.database.Customers.ToList();
                target.customerComboBox.SelectedIndex = -1;
                target.customerComboBox.SelectedIndex = savedIndex;

                MyMessageBox.ShowConfirmation(Application.Current.MainWindow, $"Add new purchase successfully");

                target.popupAddPurchaseStatusComboBox.SelectedIndex = 0;
                target.popupAddPurchasePlaceTextBox.Clear();
                target.popupAddPurchaseReceivedDatePicker.SelectedDate = null;
            });

            DeletePurchase = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as PurchasesUserControlView;

                var savedIndex = target.customerComboBox.SelectedIndex;

                if (target.purchaseDataGrid.SelectedItem != null)
                {
                    MyMessageBox.ShowChosen(Application.Current.MainWindow, $"Are you sure you want to delete this purchase?", ChooseYesNo);

                    if (model.chosen)
                    {
                        DataProvider.instance.database.Purchases.Remove((Purchase)target.purchaseDataGrid.SelectedItem);
                        DataProvider.instance.database.SaveChanges();

                        model.customerTable = DataProvider.instance.database.Customers.ToList();
                        target.customerComboBox.SelectedIndex = -1;
                        target.customerComboBox.SelectedIndex = savedIndex;

                        MyMessageBox.ShowConfirmation(Application.Current.MainWindow, "Purchase deleted successfully");
                    }
                }
            });

            PopupUpdatePurchase = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as PurchasesUserControlView;

                if (target.purchaseDataGrid.SelectedItem != null)
                {
                    if (target.updatePurchaseToggleButton.IsChecked == true)
                        target.updatePurchasePopup.IsOpen = true;
                    else
                        target.updatePurchasePopup.IsOpen = false;
                }
            });

            UpdatePurchase = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as PurchasesUserControlView;

                var savedIndex = target.customerComboBox.SelectedIndex;

                target.updatePurchasePopup.IsOpen = false;
                target.updatePurchaseToggleButton.IsChecked = false;

                var currentPurchase = target.purchaseDataGrid.SelectedItem as Purchase;
                var newPurchase = new Purchase
                {
                    PurchaseStatus = (int)target.popupUpdatePurchaseStatusComboBox.SelectedValue,
                    PurchaseReceivedDate = target.popupUpdatePurchaseReceivedDatePicker.SelectedDate,
                    PurchasePlace = target.popupUpdatePurchasePlaceTextBox.Text
                };

                if (currentPurchase != newPurchase)
                {
                    var purchase = DataProvider.instance.database.Purchases.Find(currentPurchase.PurchaseID);
                    purchase.PurchaseStatus = newPurchase.PurchaseStatus;
                    purchase.PurchaseReceivedDate = newPurchase.PurchaseReceivedDate;
                    purchase.PurchasePlace = newPurchase.PurchasePlace;
                    DataProvider.instance.database.SaveChanges();
                }

                model.customerTable = DataProvider.instance.database.Customers.ToList();
                target.customerComboBox.SelectedIndex = -1;
                target.customerComboBox.SelectedIndex = savedIndex;

                MyMessageBox.ShowConfirmation(Application.Current.MainWindow, $"Update purchase successfully");
            });

            PopupAddPurchaseDetail = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as PurchasesUserControlView;

                if (target.purchaseDataGrid.SelectedItem != null)
                {
                    if (target.addPurchaseDetailToggleButton.IsChecked == true)
                        target.addPurchaseDetailPopup.IsOpen = true;
                    else
                    {
                        target.popupAddPurchaseDetailCategoryComboBox.SelectedIndex = 0;
                        target.popupAddPurchaseDetailProductComboBox.SelectedIndex = 0;
                        target.popupAddPurchaseDetailQuantityTextBox.Clear();
                        target.addPurchaseDetailPopup.IsOpen = false;
                    }
                }
            });

            AddPurchaseDetail = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as PurchasesUserControlView;
                if (target.popupAddPurchaseDetailQuantityTextBox.Text != string.Empty)
                {
                    var customerSavedIndex = target.customerComboBox.SelectedIndex;
                    var purchaseSavedIndex = target.purchaseDataGrid.SelectedIndex;
                    target.addPurchaseDetailPopup.IsOpen = false;
                    target.addPurchaseDetailToggleButton.IsChecked = false;

                    var newPurchaseDetail = new PurchaseDetail
                    {
                        PurchaseDetailID = NextPuchaseDetailIDGenerate(),
                        PurchaseDetailPurchase = ((Purchase)target.purchaseDataGrid.SelectedItem).PurchaseID,
                        PurchaseDetailProduct = (int)target.popupAddPurchaseDetailProductComboBox.SelectedValue,
                        PurchaseDetailQuantity = int.Parse(target.popupAddPurchaseDetailQuantityTextBox.Text)
                    };
                    newPurchaseDetail.PurchaseDetailTotal = DataProvider.instance.database.Products.Find(newPurchaseDetail.PurchaseDetailProduct).Price * newPurchaseDetail.PurchaseDetailQuantity;
                    if (DataProvider.instance.database.Purchases.Find(((Purchase)target.purchaseDataGrid.SelectedItem).PurchaseID).PurchaseTotal == null)
                        DataProvider.instance.database.Purchases.Find(((Purchase)target.purchaseDataGrid.SelectedItem).PurchaseID).PurchaseTotal = 0;
                    DataProvider.instance.database.Purchases.Find(((Purchase)target.purchaseDataGrid.SelectedItem).PurchaseID).PurchaseTotal += newPurchaseDetail.PurchaseDetailTotal;

                    DataProvider.instance.database.PurchaseDetails.Add(newPurchaseDetail);
                    DataProvider.instance.database.SaveChanges();

                    model.customerTable = DataProvider.instance.database.Customers.ToList();
                    target.customerComboBox.SelectedIndex = -1;
                    target.customerComboBox.SelectedIndex = customerSavedIndex;
                    target.purchaseDataGrid.SelectedIndex = -1;
                    target.purchaseDataGrid.SelectedIndex = purchaseSavedIndex;

                    MyMessageBox.ShowConfirmation(Application.Current.MainWindow, $"Add new purchase detail successfully");

                    target.popupAddPurchaseDetailCategoryComboBox.SelectedIndex = 0;
                    target.popupAddPurchaseDetailProductComboBox.SelectedIndex = 0;
                    target.popupAddPurchaseDetailQuantityTextBox.Clear();
                }
            });

            DeletePurchaseDetail = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as PurchasesUserControlView;

                var customerSavedIndex = target.customerComboBox.SelectedIndex;
                var purchaseSavedIndex = target.purchaseDataGrid.SelectedIndex;

                if (target.purchaseDetailDataGrid.SelectedItem != null)
                {
                    MyMessageBox.ShowChosen(Application.Current.MainWindow, $"Are you sure you want to delete this purchase detail?", ChooseYesNo);

                    if (model.chosen)
                    {
                        DataProvider.instance.database.Purchases.Find(((Purchase)target.purchaseDataGrid.SelectedItem).PurchaseID).PurchaseTotal -= ((PurchaseDetail)target.purchaseDetailDataGrid.SelectedItem).PurchaseDetailTotal;
                        if (DataProvider.instance.database.Purchases.Find(((Purchase)target.purchaseDataGrid.SelectedItem).PurchaseID).PurchaseTotal == 0)
                            DataProvider.instance.database.Purchases.Find(((Purchase)target.purchaseDataGrid.SelectedItem).PurchaseID).PurchaseTotal = null;
                        DataProvider.instance.database.PurchaseDetails.Remove((PurchaseDetail)target.purchaseDetailDataGrid.SelectedItem);
                        DataProvider.instance.database.SaveChanges();

                        model.customerTable = DataProvider.instance.database.Customers.ToList();
                        target.customerComboBox.SelectedIndex = -1;
                        target.customerComboBox.SelectedIndex = customerSavedIndex;
                        target.purchaseDataGrid.SelectedIndex = -1;
                        target.purchaseDataGrid.SelectedIndex = purchaseSavedIndex;

                        MyMessageBox.ShowConfirmation(Application.Current.MainWindow, "Purchase detail deleted successfully");
                    }
                }
            });

            PopupUpdatePurchaseDetail = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as PurchasesUserControlView;

                if (target.purchaseDetailDataGrid.SelectedItem != null)
                {
                    if (target.updatePurchaseDetailToggleButton.IsChecked == true)
                        target.updatePurchaseDetailPopup.IsOpen = true;
                    else
                        target.updatePurchaseDetailPopup.IsOpen = false;
                }
            });

            UpdatePurchaseDetail = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as PurchasesUserControlView;

                if (target.popupUpdatePurchaseDetailQuantityTextBox.Text != string.Empty)
                {
                    var customerSavedIndex = target.customerComboBox.SelectedIndex;
                    var purchaseSavedIndex = target.purchaseDataGrid.SelectedIndex;

                    target.updatePurchaseDetailPopup.IsOpen = false;
                    target.updatePurchaseDetailToggleButton.IsChecked = false;

                    var currentPurchaseDetail = target.purchaseDetailDataGrid.SelectedItem as PurchaseDetail;
                    var newPurchaseDetail = new PurchaseDetail
                    {
                        PurchaseDetailProduct = (int)target.popupUpdatePurchaseDetailProductComboBox.SelectedValue,
                        PurchaseDetailQuantity = int.Parse(target.popupUpdatePurchaseDetailQuantityTextBox.Text)
                    };
                    newPurchaseDetail.PurchaseDetailTotal = DataProvider.instance.database.Products.Find(newPurchaseDetail.PurchaseDetailProduct).Price * newPurchaseDetail.PurchaseDetailQuantity;

                    if (currentPurchaseDetail != newPurchaseDetail)
                    {
                        var purchaseDetail = DataProvider.instance.database.PurchaseDetails.Find(currentPurchaseDetail.PurchaseDetailID);
                        DataProvider.instance.database.Purchases.Find(((Purchase)target.purchaseDataGrid.SelectedItem).PurchaseID).PurchaseTotal += newPurchaseDetail.PurchaseDetailTotal - currentPurchaseDetail.PurchaseDetailTotal;

                        purchaseDetail.PurchaseDetailProduct = newPurchaseDetail.PurchaseDetailProduct;
                        purchaseDetail.PurchaseDetailQuantity = newPurchaseDetail.PurchaseDetailQuantity;
                        purchaseDetail.PurchaseDetailTotal = newPurchaseDetail.PurchaseDetailTotal;

                        DataProvider.instance.database.SaveChanges();
                    }

                    model.customerTable = DataProvider.instance.database.Customers.ToList();
                    target.customerComboBox.SelectedIndex = -1;
                    target.customerComboBox.SelectedIndex = customerSavedIndex;
                    target.purchaseDataGrid.SelectedIndex = -1;
                    target.purchaseDataGrid.SelectedIndex = purchaseSavedIndex;

                    MyMessageBox.ShowConfirmation(Application.Current.MainWindow, $"Update purchase detail successfully");
                }
            });
        }

        private int NextPurchaseIDGenerate()
        {
            var purchaseTable = DataProvider.instance.database.Purchases;

            for (int i = 1; true; i++)
            {
                if (purchaseTable.Find(i) != null)
                {
                    if (purchaseTable.Find(i).PurchaseID == i)
                        continue;
                    continue;
                }
                else
                    return i;
            }
        }

        private int NextPuchaseDetailIDGenerate()
        {
            var purchaseDetailTable = DataProvider.instance.database.PurchaseDetails;

            for (int i = 1; true; i++)
            {
                if (purchaseDetailTable.Find(i) != null)
                {
                    if (purchaseDetailTable.Find(i).PurchaseDetailID == i)
                        continue;
                    continue;
                }
                else
                    return i;
            }
        }

        private void ChooseYesNo(int chosen)
        {
            model.chosen = chosen == 1 ? true : false;
        }
    }
}
