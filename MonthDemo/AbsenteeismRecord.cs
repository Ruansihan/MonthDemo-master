using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonthDemo
{
    public class AbsenteeismRecord: Record
    {
        public AbsenteeismRecord(DateTime day)
            : base(null)
        {
            mDay = day;
            mDescription = "缺席";
        }
    }
}
