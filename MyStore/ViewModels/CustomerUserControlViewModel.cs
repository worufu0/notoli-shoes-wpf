using MyStore.Helpers;
using MyStore.Models;
using MyStore.Views;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace MyStore.ViewModels
{
    public class CustomerUserControlViewModel
    {
        public delegate void CommandExecution(object window, object item);
        public event CommandExecution ChangeTabHandler;
        public CustomersUserControlModel model { get; set; }
        public ICommand SearchByName { get; set; }
        public ICommand PopupAddCustomer { get; set; }
        public ICommand AddCustomer { get; set; }
        public ICommand DeleteCustomer { get; set; }
        public ICommand PopupUpdateCustomer { get; set; }
        public ICommand UpdateCustomer { get; set; }
        public ICommand ViewPurchases { get; set; }
        public CustomerUserControlViewModel()
        {
            model = new CustomersUserControlModel
            {
                customerTable = DataProvider.instance.database.Customers.ToList()
            };

            SearchByName = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as CustomersUserControlView;

                if (target.customerDataGrid.ItemsSource != null)
                {
                    model.collectionView = (CollectionView)CollectionViewSource.GetDefaultView(target.customerDataGrid.ItemsSource);
                    model.collectionView.Filter = new Predicate<object>(customer => ((Customer)customer).CustomerName.Contains(target.searchBox.Text));
                }
            });

            PopupAddCustomer = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as CustomersUserControlView;

                if (target.addCustomerToggleButton.IsChecked == true)
                    target.addCustomerPopup.IsOpen = true;
                else
                {
                    target.popupAddCustomerNameTextBox.Clear();
                    target.popupAddCustomerAddressTextBox.Clear();
                    target.popupAddCustomerTelTextBox.Clear();
                    target.addCustomerPopup.IsOpen = false;
                }
            });

            AddCustomer = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as CustomersUserControlView;

                target.addCustomerPopup.IsOpen = false;
                target.addCustomerToggleButton.IsChecked = false;

                var newCustomer = new Customer
                {
                    CustomerID = NextCustomerIDGenerate(),
                    CustomerName = target.popupAddCustomerNameTextBox.Text,
                    CustomerAddress = target.popupAddCustomerAddressTextBox.Text,
                    CustomerTel = int.Parse(target.popupAddCustomerTelTextBox.Text)
                };

                DataProvider.instance.database.Customers.Add(newCustomer);
                DataProvider.instance.database.SaveChanges();

                model.customerTable = DataProvider.instance.database.Customers.ToList();

                MyMessageBox.ShowConfirmation(Application.Current.MainWindow, $"Add new customer '{target.popupAddCustomerNameTextBox.Text}' successfully");

                target.popupAddCustomerNameTextBox.Clear();
                target.popupUpdateCustomerAddressTextBox.Clear();
                target.popupAddCustomerTelTextBox.Clear();
            });

            DeleteCustomer = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as CustomersUserControlView;

                if (target.customerDataGrid.SelectedItem != null)
                {
                    MyMessageBox.ShowChosen(Application.Current.MainWindow, $"Are you sure you want to delete the customer '{((Customer)target.customerDataGrid.SelectedItem).CustomerName}'?", ChooseYesNo);

                    if (model.chosen)
                    {
                        DataProvider.instance.database.Customers.Remove((Customer)target.customerDataGrid.SelectedItem);
                        DataProvider.instance.database.SaveChanges();
                        
                        model.customerTable = DataProvider.instance.database.Customers.ToList();

                        MyMessageBox.ShowConfirmation(Application.Current.MainWindow, "Customer deleted successfully");
                    }
                }
            });

            PopupUpdateCustomer = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as CustomersUserControlView;

                if (target.customerDataGrid.SelectedItem != null)
                {
                    if (target.updateCustomerToggleButton.IsChecked == true)
                        target.updateCustomerPopup.IsOpen = true;
                    else
                        target.updateCustomerPopup.IsOpen = false;
                }
            });

            UpdateCustomer = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as CustomersUserControlView;

                target.updateCustomerPopup.IsOpen = false;
                target.updateCustomerToggleButton.IsChecked = false;

                var currentCustomer = target.customerDataGrid.SelectedItem as Customer;
                var newCustomer = new Customer
                {
                    CustomerName = target.popupUpdateCustomerNameTextBox.Text,
                    CustomerAddress = target.popupUpdateCustomerAddressTextBox.Text,
                    CustomerTel = int.Parse(target.popupUpdateCustomerTelTextBox.Text)
                };

                if (currentCustomer != newCustomer)
                {
                    var customer = DataProvider.instance.database.Customers.Find(currentCustomer.CustomerID);
                    customer.CustomerName = newCustomer.CustomerName;
                    customer.CustomerAddress = newCustomer.CustomerAddress;
                    customer.CustomerTel = newCustomer.CustomerTel;
                    DataProvider.instance.database.SaveChanges();
                }

                model.customerTable = DataProvider.instance.database.Customers.ToList();

                MyMessageBox.ShowConfirmation(Application.Current.MainWindow, $"Update customer successfully");
            });

            ViewPurchases = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as CustomersUserControlView;

                ChangeTabHandler?.Invoke(Application.Current.MainWindow, target.customerDataGrid.SelectedItem);
            });
        }

        private int NextCustomerIDGenerate()
        {
            var customerTable = DataProvider.instance.database.Customers;

            for (int i = 1; true; i++)
            {
                if (customerTable.Find(i) != null)
                {
                    if (customerTable.Find(i).CustomerID == i)
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
