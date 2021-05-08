using MyStore.ViewModels;
using MyStore.Views;
using System;
using System.Windows;

namespace MyStore.Helpers
{
    public class MyMessageBox
    {
        public static void ShowConfirmation(Window owner, string content)
        {
            var dataContext = new MyMessageBoxViewModel(0, content);
            var myMessageBox = new MyMessageBoxView
            {
                Owner = owner,
                DataContext = dataContext
            };
            myMessageBox.ShowDialog();
        }

        public static void ShowChosen(Window owner, string content, MyMessageBoxViewModel.Choose action)
        {
            var dataContext = new MyMessageBoxViewModel(1, content);
            var myMessageBox = new MyMessageBoxView
            {
                Owner = owner,
                DataContext = dataContext
            };
            dataContext.ChooseHandler += action;
            myMessageBox.ShowDialog();
        }
    }
}
