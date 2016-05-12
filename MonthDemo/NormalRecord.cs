using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonthDemo
{
    public class NormalRecord: Record
    {
        public NormalRecord(List<DateTime> records)
            : base(records)
        {
            mDescription = "正常";
        }
    }
}
