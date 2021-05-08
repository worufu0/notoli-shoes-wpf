using MyStore.Helpers;
using MyStore.Models;
using MyStore.Views;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MyStore.ViewModels
{
    public class BossViewModel
    {
        public BossModel model { get; set; }
        public ICommand Navigate { get; set; }
        public ICommand Logout { get; set; }
        public ICommand MinimizeWindow { get; set; }
        public ICommand CloseWindow { get; set; }

        public BossViewModel()
        {
        }

        public BossViewModel(string username, string accountRole)
        {
            model = new BossModel
            {
                closeWindow = false,
                name = username,
                role = accountRole,
            };

            var statisticUserControlView = new StatisticUserControlView();
            var inventoryUserControlView = new InventoryUserControlView();
            var purchasesUserControl = new PurchasesUserControlView();
            var customersUserControlView = new CustomersUserControlView();
            var customerUserControlDataContext = customersUserControlView.DataContext as CustomerUserControlViewModel;
            customerUserControlDataContext.ChangeTabHandler += GoPurchasesTab;
            var employeesUserControlView = new EmployeesUserControlView();

            Navigate = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as BossView;

                target.mainGrid.Children.Clear();

                if (target.menuListView.SelectedIndex == 0)
                    target.mainGrid.Children.Add(statisticUserControlView);
                if (target.menuListView.SelectedIndex == 1)
                    target.mainGrid.Children.Add(inventoryUserControlView);
                if (target.menuListView.SelectedIndex == 2)
                    target.mainGrid.Children.Add(purchasesUserControl);
                if (target.menuListView.SelectedIndex == 3)
                    target.mainGrid.Children.Add(customersUserControlView);
                if (target.menuListView.SelectedIndex == 4)
                    target.mainGrid.Children.Add(employeesUserControlView);
            });

            Logout = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as BossView;

                target.Hide();

                var loginView = new LoginView();
                Application.Current.MainWindow = loginView;
                loginView.Show();

                target.Close();
            });

            MinimizeWindow = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as BossView;

                target.WindowState = WindowState.Minimized;
            });

            CloseWindow = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as BossView;

                MyMessageBox.ShowChosen(target, "Are you sure you want to exit?", ChooseYesNo);

                if (model.closeWindow)
                    target.Close();
            });
        }

        private void DisableMainGrid(object window)
        {
            var bossView = window as BossView;

            bossView.mainGrid.IsEnabled = false;
        }

        private void GoPurchasesTab(object window, object item)
        {
            var bossView = window as BossView;
            bossView.menuListView.SelectedIndex = 2;

            var purchasesUserControlView = bossView.mainGrid.Children[0] as PurchasesUserControlView;
            purchasesUserControlView.customerComboBox.SelectedItem = item;
        }

        private void ChooseYesNo(int chosen)
        {
            model.closeWindow = chosen == 1 ? true : false;
        }
    }
}
