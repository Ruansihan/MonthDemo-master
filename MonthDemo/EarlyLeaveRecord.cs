using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonthDemo
{
    public class EarlyLeaveRecord: Record
    {
        public EarlyLeaveRecord(List<DateTime> records)
            : base(records)
        {
            mDescription = "早退";
            var date = records[records.Count - 1];
            mMintues = (18 - date.Hour) * 60 - date.Minute;
        }

        private int mMintues;
        public int Minutes
        {
            get
            {
                return mMintues;
            }
        }
    }
}
