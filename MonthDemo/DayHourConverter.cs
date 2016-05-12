using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MonthDemo
{
    public class DayHourConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int? count = value as int?;
            if (count == null)
            {
                return null;
            }
            else if (count == 0)
            {
                return String.Empty;
            }
            return count.Value * 8 + " 小时";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
