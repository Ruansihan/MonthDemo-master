using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonthDemo
{
    public class OvertimeWithErrorRecord: Record
    {
        public OvertimeWithErrorRecord(List<DateTime> records)
            : base(records)
        {
            mDescription = "加班打卡错误";
        }
    }
}
