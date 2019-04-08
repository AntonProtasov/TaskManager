using System;
using System.Windows;
using System.Windows.Data;

namespace TaskManager.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(!(value is Visibility))
                return true;

            bool invert = false;
            if(parameter is bool)
                invert = (bool)parameter;

            bool ret = ((Visibility)value == Visibility.Visible);
            if(invert)
                ret = !ret;
            return ret;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(!(value is bool))
                return Visibility.Visible;

            bool invert = false;
            if(parameter is bool)
                invert = (bool)parameter;

            bool ret = (bool)value;
            if(invert)
                ret = !ret;

            return ret ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
