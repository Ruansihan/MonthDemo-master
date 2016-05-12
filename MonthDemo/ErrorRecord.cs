using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonthDemo
{
    public class ErrorRecord: Record
    {
        public ErrorRecord(List<DateTime> records)
            : base(records)
        {
            mDescription = "打卡错误";
        }
    }
}
