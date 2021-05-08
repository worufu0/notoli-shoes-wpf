using MyStore.Helpers;
using MyStore.Models;
using MyStore.Views;
using System.Windows.Input;

namespace MyStore.ViewModels
{
    public class MyMessageBoxViewModel
    {
        public delegate void Choose(int chosen);
        public event Choose ChooseHandler;
        public MyMessageBoxModel model { get; set; }
        public ICommand Ok { get; set; }
        public ICommand Yes { get; set; }
        public ICommand No { get; set; }

        public MyMessageBoxViewModel()
        {
        }

        public MyMessageBoxViewModel(int type, string content)
        {
            model = new MyMessageBoxModel
            {
                content = content,
                boxType = type
            };

            Ok = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as MyMessageBoxView;

                ChooseHandler?.Invoke(0);
                target.Close();
            });

            Yes = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as MyMessageBoxView;

                ChooseHandler?.Invoke(1);
                target.Close();
            });

            No = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as MyMessageBoxView;

                ChooseHandler?.Invoke(2);
                target.Close();
            });
        }
    }
}
