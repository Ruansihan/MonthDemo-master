using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonthDemo
{
    public class LateRecord: Record
    {
        public LateRecord(List<DateTime> records)
            : base(records)
        {
            mDescription = "迟到";
            mMinutes = (records[0].Hour - 9) * 60 + records[0].Minute;
        }

        private int mMinutes;
        public int Minutes
        {
            get
            {
                return mMinutes;
            }
        }
    }
}
