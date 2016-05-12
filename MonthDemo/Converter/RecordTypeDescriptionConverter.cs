using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MonthDemo
{
    public class RecordTypeDescriptionConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var type = value as Record.RecordType?;
            switch (type)
            {
                case Record.RecordType.WORKDAY:
                    return "工作日";
                case Record.RecordType.WORKDAY_ERROR:
                    return "工作日打卡错误";
                case Record.RecordType.REALLY_OVERTIME:
                    return "漫长工作日";
                case Record.RecordType.REALLY_SHORT:
                    return "短暂工作日";
                case Record.RecordType.NOT_WORKDAY:
                    return "非工作日";
                case Record.RecordType.NOT_WORKDAY_ERROR:
                    return "非工作日打卡错误";
                case Record.RecordType.ABSENTEEISM:
                    return "缺席";
                default:
                    return "未知类型";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
