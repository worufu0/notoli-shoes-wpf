using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace MyStore.Helpers
{
    public class CurrencyToStringNumber : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var currency = (string)value;
            var stringNumber = currency.Replace(",", "");
            return stringNumber;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("CurrencyToStringNumber can only be used OneWay");
        }
    }

    public class QuantityIStockToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var quantityStock = (int)value;
            if (quantityStock == 1)
                return 1;
            if (quantityStock > 5)
                return 2;
            return 3;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("StatusNameToColor can only be used OneWay");
        }
    }

    public class StatusNameToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var statusName = (string)value;
            if (statusName == "Đã hủy")
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("StatusNameToColor can only be used OneWay");
        }
    }

    public class HasItemToHiddenText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hasItem = (bool)value;
            return hasItem == true ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("HasItemToHiddenText can only be used OneWay");
        }
    }

    public class IntToChosenBox : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boxType = (int)value;
            return boxType == 1 ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("IntToChosenBox can only be used OneWay");
        }
    }

    public class IntToCornfimationBox : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boxType = (int)value;
            return boxType == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("IntToCornfimationBox can only be used OneWay");
        }
    }

    public class FocusAndEmptyToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var isFocused = (bool)values[0];
            var isEmpty = (bool)values[1];

            return !isFocused && isEmpty ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("FocusAndEmptyToVisibilityConverter can only be used OneWay");
        }
    }
}
