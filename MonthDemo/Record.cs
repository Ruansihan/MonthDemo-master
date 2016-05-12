using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonthDemo
{
    public abstract class Record
    {
        public Record(List<DateTime> records)
        {
            if (records != null)
            {
                mRecordsOfDay = records;
                mDay = new DateTime(mRecordsOfDay[0].Year, mRecordsOfDay[0].Month, mRecordsOfDay[0].Day);
            }
        }

        protected string mDescription;
        public String Description
        {
            get
            {
                return mDescription;
            }
        }
        protected DateTime mDay;
        public DateTime Day
        {
            get
            {
                return mDay;
            }
        }
        protected List<DateTime> mRecordsOfDay;
        public List<DateTime> RecordsOfDay
        {
            get
            {
                return mRecordsOfDay;
            }
        }
    }
}
