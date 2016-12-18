using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VerejneOsvetlenie.Converters
{
    public class StringDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
                return string.Empty;
            if (value is DateTime)
                return ((DateTime) value);
            return DateTime.Parse(value.ToString());
            //DateTime d;
            ////return DateTime.TryParse(value?.ToString(), out d) ? d.ToString("dd.MM.yyyy") : string.Empty;
            //return DateTime.Parse(value?.ToString()).ToString("dd.MM.yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
                return ((DateTime)value).ToString("dd.MM.yyyy");
            return string.Empty;
        }
    }
}
