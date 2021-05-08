using MyStore.Helpers;
using MyStore.Models;
using MyStore.Views;
using System.Windows;
using System.Windows.Input;

namespace MyStore.ViewModels
{
    public class SalesmanViewModel
    {
        public SalesmanModel model { get; set; }
        public ICommand Logout { get; set; }
        public ICommand MinimizeWindow { get; set; }
        public ICommand CloseWindow { get; set; }

        public SalesmanViewModel()
        {
        }

        public SalesmanViewModel(string username, string accountRole)
        {
            model = new SalesmanModel
            {
                closeWindow = false,
                name = username,
                role = accountRole,
            };

            Logout = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as SalesmanView;

                target.Hide();

                var loginView = new LoginView();
                Application.Current.MainWindow = loginView;
                loginView.Show();

                target.Close();
            });

            MinimizeWindow = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as SalesmanView;

                target.WindowState = WindowState.Minimized;
            });

            CloseWindow = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as SalesmanView;

                MyMessageBox.ShowChosen(target, "Are you sure you want to exit?", ChooseYesNo);

                if (model.closeWindow)
                    target.Close();
            });
        }
        private void ChooseYesNo(int chosen)
        {
            model.closeWindow = chosen == 1 ? true : false;
        }
    }
}
