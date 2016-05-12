using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MonthDemo
{
    public class MinuteToHalfHourConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int? mintues = value as int?;
            if (mintues > 30)
            {
                return (mintues / 30).Value / 2.0 + " 小时"; }
            else
            {
                return mintues + " 分钟";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
