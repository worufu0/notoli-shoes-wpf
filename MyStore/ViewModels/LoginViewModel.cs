using MyStore.Helpers;
using MyStore.Models;
using MyStore.Views;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MyStore.ViewModels
{
    public class LoginViewModel
    {
        public LoginModel model { get; set; }
        public ICommand Login { get; set; }
        public ICommand ForgotPassword { get; set; }
        public ICommand BlockSpace { get; set; }
        public ICommand OpenChangeDatabaseWindow { get; set; }
        public ICommand MinimizeWindow { get; set; }
        public ICommand CloseWindow { get; set; }

        public LoginViewModel()
        {
            model = new LoginModel
            {
                closeWindow = false,
                appVersion = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                rememberMe = Convert.ToBoolean(ConfigurationManager.AppSettings["cRememberMe"]),
                username = ConfigurationManager.AppSettings["cUsername"]
            };

            Login = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as LoginView;

                var serverName = ConfigurationManager.AppSettings["cServerName"];
                var databaseName = ConfigurationManager.AppSettings["cDatabaseName"];

                var connectionString = $"Data Source={serverName};Initial Catalog={databaseName};" +
                $"User ID= {model.username};Password={target.passwordBox.Password};Connection Timeout=3";

                var connection = new SqlConnection(connectionString);
                try
                {
                    connection.Open();

                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["cRememberMe"].Value = model.rememberMe.ToString();
                    ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
                    if (model.rememberMe)
                    {
                        config.AppSettings.Settings["cUsername"].Value = model.username;
                    }
                    else
                    {
                        config.AppSettings.Settings["cUsername"].Value = string.Empty;
                    }
                    config.Save(ConfigurationSaveMode.Minimal);

                    MyMessageBox.ShowConfirmation(target, "Logged in successfully");

                    AccordingToAccountRole(target, connectionString);
                }
                catch (SqlException exception)
                {
                    for (int i = 0; i < exception.Errors.Count; i++)
                        MyMessageBox.ShowConfirmation(target, exception.Errors[i].Message);
                }
                finally
                {
                    connection.Close();
                }
            });

            ForgotPassword = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as LoginView;

                MyMessageBox.ShowConfirmation(target, "This function has not been developed");
            });

            BlockSpace = new RelayCommand<object>(p => p != null, p => { });

            OpenChangeDatabaseWindow = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as LoginView;
                var changeDatabaseView = new ChangeDatabaseView();

                target.OpacityMask = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000000"));
                target.OpacityMask.Opacity = 0.3;

                changeDatabaseView.Owner = target;
                changeDatabaseView.ShowDialog();

                if (target.DialogResult == null)
                    target.OpacityMask = null;
            });

            MinimizeWindow = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as LoginView;

                target.WindowState = WindowState.Minimized;
            });

            CloseWindow = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as LoginView;

                MyMessageBox.ShowChosen(target, "Are you sure you want to exit?", ChooseYesNo);

                if (model.closeWindow)
                    target.Close();
            });
        }

        private void AccordingToAccountRole(Window loginWindow, string connectionString)
        {
            var provider = new DataProvider(connectionString);
            var roleID = DataProvider.instance.database.Accounts.Find(model.username).AccountRole;
            var roleName = DataProvider.instance.database.Roles.Find(roleID).RoleName;

            Window resultView = null;

            loginWindow.Hide();

            if (roleID == 1)
            {
                resultView = new BossView();
                resultView.DataContext = new BossViewModel(model.username, roleName);
            }
            else if (roleID == 2)
            {
                resultView = new SalesmanView();
                resultView.DataContext = new SalesmanViewModel(model.username, roleName);
            }

            Application.Current.MainWindow = resultView;
            resultView.Show();

            loginWindow.Close();
        }
        private void ChooseYesNo(int chosen)
        {
            model.closeWindow = chosen == 1 ? true : false;
        }
    }
}
