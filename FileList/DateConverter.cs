using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FileBrowser.FileList
{
    public class DateConverter : IValueConverter
    {
        private string NoDate = String.Empty;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                var date = (DateTime)value;

                if (date == DateTime.MinValue)
                {
                    return NoDate;
                }

                return date.ToString();
            }

            return NoDate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
