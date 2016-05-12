using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonthDemo
{
    public class OvertimeRecord: Record
    {
        public OvertimeRecord(List<DateTime> records)
            : base(records)
        {
            mDescription = "加班";
            var start = records[0];
            var end = records[records.Count - 1];
            mMintues = (end.Hour - start.Hour - 1) * 60 + (end.Minute - start.Minute);
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
