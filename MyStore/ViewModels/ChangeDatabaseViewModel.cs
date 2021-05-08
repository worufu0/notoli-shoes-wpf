using MyStore.Helpers;
using MyStore.Models;
using MyStore.Views;
using System.Configuration;
using System.Windows.Input;

namespace MyStore.ViewModels
{
    public class ChangeDatabaseViewModel
    {
        public ChangeDatabaseModel model { get; set; }
        public ICommand Apply { get; set; }

        public ChangeDatabaseViewModel()
        {
            model = new ChangeDatabaseModel
            {
                closeWindow = false,
                serverName = ConfigurationManager.AppSettings["cServerName"],
                databaseName = ConfigurationManager.AppSettings["cDatabaseName"]
            };

            Apply = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as ChangeDatabaseView;

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["cServerName"].Value = model.serverName;
                config.AppSettings.Settings["cDatabaseName"].Value = model.databaseName;
                config.Save(ConfigurationSaveMode.Minimal);
                ConfigurationManager.RefreshSection("appSettings");

                MyMessageBox.ShowConfirmation(target, $"Server has changed to '{model.serverName}'\nDatabase has changed to '{model.databaseName}'");

                target.Close();
            });
        }
    }
}
