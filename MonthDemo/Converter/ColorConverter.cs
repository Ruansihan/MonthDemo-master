using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MonthDemo
{
    public class ColorConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Record record = value as Record;
            if (record.Type == Record.RecordType.WORKDAY)
            {
                return "White";
            }
            else if (record.Type == Record.RecordType.NOT_WORKDAY || 
                record.Type == Record.RecordType.REALLY_OVERTIME)
            {
                return "Green";
            }
            else
            {
                return "Red";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
