using System;
using System.Globalization;
using System.Windows.Data;

namespace Periotris.ViewModel
{
    public class TimeSpanStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "--:--";
            }
            if (value is TimeSpan realVal)
            {
                return string.Format("{0:D2}:{1:D2}", realVal.Minutes, realVal.Seconds);
            }
            throw new ArgumentException(nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Two-way binding is not supported on " +
                                            nameof(TimeSpanStringConverter));
        }
    }
}
