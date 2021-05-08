using System.Windows;
using System.Windows.Controls;

namespace MyStore.Helpers
{
    public static class PasswordBoxExtension
    {
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.RegisterAttached(
                "IsActive", typeof(bool), typeof(PasswordBoxExtension),
                new FrameworkPropertyMetadata(OnIsActiveChanged));

        public static readonly DependencyPropertyKey IsPasswordEmptyPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                "IsPasswordEmpty", typeof(bool), typeof(PasswordBoxExtension),
                new FrameworkPropertyMetadata());

        public static readonly DependencyProperty IsPasswordEmptyProperty =
            IsPasswordEmptyPropertyKey.DependencyProperty;

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached(
                "Password", typeof(string), typeof(PasswordBoxExtension),
                new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached(
                "Attach", typeof(bool), typeof(PasswordBoxExtension),
                new PropertyMetadata(false, Attach));

        private static readonly DependencyProperty IsUpdatingProperty =
           DependencyProperty.RegisterAttached(
               "IsUpdating", typeof(bool), typeof(PasswordBoxExtension));

        private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;
            if (passwordBox == null) return;

            passwordBox.PasswordChanged -= OnPasswordChanged;
            if ((bool)e.NewValue)
            {
                SetIsPasswordEmpty(passwordBox);
                passwordBox.PasswordChanged += OnPasswordChanged;
            }
        }

        private static void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            SetIsPasswordEmpty((PasswordBox)sender);
        }

        public static void SetIsActive(PasswordBox element, bool value)
        {
            element.SetValue(IsActiveProperty, value);
        }

        public static bool GetIsActive(PasswordBox element)
        {
            return (bool)element.GetValue(IsActiveProperty);
        }

        private static void SetIsPasswordEmpty(PasswordBox element)
        {
            element.SetValue(IsPasswordEmptyPropertyKey, element.SecurePassword.Length == 0);
        }

        public static bool GetIsPasswordEmpty(PasswordBox element)
        {
            return (bool)element.GetValue(IsPasswordEmptyProperty);
        }

        public static void SetAttach(PasswordBox element, bool value)
        {
            element.SetValue(AttachProperty, value);
        }

        public static bool GetAttach(PasswordBox element)
        {
            return (bool)element.GetValue(AttachProperty);
        }

        public static string GetPassword(PasswordBox element)
        {
            return (string)element.GetValue(PasswordProperty);
        }

        public static void SetPassword(PasswordBox element, string value)
        {
            element.SetValue(PasswordProperty, value);
        }

        private static bool GetIsUpdating(PasswordBox element)
        {
            return (bool)element.GetValue(IsUpdatingProperty);
        }

        private static void SetIsUpdating(PasswordBox element, bool value)
        {
            element.SetValue(IsUpdatingProperty, value);
        }

        private static void OnPasswordPropertyChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            passwordBox.PasswordChanged -= PasswordChanged;

            if (!(bool)GetIsUpdating(passwordBox))
            {
                passwordBox.Password = (string)e.NewValue;
            }
            passwordBox.PasswordChanged += PasswordChanged;
        }

        private static void Attach(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;

            if (passwordBox == null)
                return;

            if ((bool)e.OldValue)
            {
                passwordBox.PasswordChanged -= PasswordChanged;
            }

            if ((bool)e.NewValue)
            {
                passwordBox.PasswordChanged += PasswordChanged;
            }
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            SetIsUpdating(passwordBox, true);
            SetPassword(passwordBox, passwordBox.Password);
            SetIsUpdating(passwordBox, false);
        }
    }
}
