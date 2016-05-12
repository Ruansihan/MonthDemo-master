using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MonthDemo
{
    public class RecordDateConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var date = (value as DateTime?).Value;
            String hour = date.Hour.ToString();
            String minute = date.Minute.ToString();
            String second = date.Second.ToString();
            return String.Format("{0}:{1}:{2}", date.Hour > 9 ? hour : "0" + hour,
                date.Minute > 9 ? minute : "0" + minute,
                date.Second > 9 ? second : "0" + second);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
