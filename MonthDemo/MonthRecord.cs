using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MonthDemo
{
    //TODO 处理加班和节假日调整
    public class MonthRecord
    {
        private Dictionary<String, List<DateTime>> mDateRecords = new Dictionary<string, List<DateTime>>();
        public List<Record> Records { get; private set; }
        public List<Record> WorkdayRecords { get; private set; }
        public List<Record> OvertimeWorkdayRecords { get; private set; }
        public List<Record> ShortWorkdayRecords { get; private set; }
        public List<Record> WorkdayErrorRecords { get; private set; }
        public List<Record> NotWorkdayRecords { get; private set; }
        public List<Record> NotWorkdayErrorRecords { get; private set; }
        public List<Record> AbsenteeismRecords { get; private set; }
        public int Year { get; private set; }
        public int Month { get; private set; }
        public String Name 
        {
            get
            {
                if (Member != null)
                {
                    return Member.Name;
                }
                return "Unknown";
            }
        }
        public String Id{ get; private set; }
        public Member Member { get; private set; }
        public int Minutes { get; private set;}

        public MonthRecord(List<AttendanceDate> records)
        {
            Year = records[0].Date.Year;
            Month = records[0].Date.Month;
            Id = records[0].Id;
            Member member;
            MemberConfig.GetInstance().Members.TryGetValue(Id, out member);
            Member = member;

            //将打卡记录按照日期进行归类，每天06:00到次日06:00为一工作日（考虑通宵）
            foreach (var item in records)
            {
                String key = genKeyCaringAboutWorkHour(item.Date);
                if (key == null)
                {
                    continue;
                }
                if (!mDateRecords.ContainsKey(key))
                {
                    mDateRecords[key] = new List<DateTime>();
                }
                mDateRecords[key].Add(item.Date);
            }
        }

        public void assemb()
        {
            Records = new List<Record>();
            foreach (var key in mDateRecords.Keys)
            {
                Record record = Record.create(mDateRecords[key], isWorkday);
                Records.Add(record);
            }
            //统计缺席
            foreach (var item in mWorkdaysList)
            {
                String key = genKey(item.Year, item.Month, item.Day);
                if (!mDateRecords.ContainsKey(key))
                {
                    Record record = Record.createAbsenteeismRecord(item);
                    Records.Add(record);
                }
            }
            Records.Sort((left, right) => left.Day.Day.CompareTo(right.Day.Day));
            WorkdayRecords = Records.FindAll((record) => record.Type == Record.RecordType.WORKDAY);
            WorkdayErrorRecords = Records.FindAll((record) => record.Type == Record.RecordType.WORKDAY_ERROR);
            OvertimeWorkdayRecords = Records.FindAll((record) => record.Type == Record.RecordType.REALLY_OVERTIME);
            ShortWorkdayRecords = Records.FindAll((record) => record.Type == Record.RecordType.REALLY_SHORT);
            NotWorkdayRecords = Records.FindAll((record) => record.Type == Record.RecordType.NOT_WORKDAY);
            NotWorkdayErrorRecords = Records.FindAll((record) => record.Type == Record.RecordType.NOT_WORKDAY_ERROR);
            AbsenteeismRecords = Records.FindAll((record) => record.Type == Record.RecordType.ABSENTEEISM);
            Minutes = Records.Sum((record) => record.Minutes);
        }

        private String genKeyCaringAboutWorkHour(DateTime date)
        {
            if (date.Hour > 6)
            {
                return genKey(date.Year, date.Month, date.Day);
            }
            else if (date.Day > 1)
            {
                return genKey(date.Year, date.Month, date.Day - 1);
            }
            else
            {
                return null;
            }
        }

        private String genKey(int year, int month, int day)
        {
            return String.Format("{0}{1}{2}", year, month, day);
        }

        private bool isWorkday(DateTime date)
        {
            String key = genKey(date.Year, date.Month, date.Day);
            return !mNotWorkdaysKeyList.Contains(key);
        }

        private List<DateTime> mWorkdaysList = new List<DateTime>();
        private List<String> mNotWorkdaysKeyList = new List<String>();
        public void setNotWorkdays(List<DateTime> notWorkdays)
        {
            mWorkdaysList.Clear();
            mNotWorkdaysKeyList.Clear();
            foreach (var item in notWorkdays)
            {
                mNotWorkdaysKeyList.Add(genKey(item.Year, item.Month, item.Day));
            }
            DateTime start = new DateTime(Year, Month, 1);
            while (start.Month == Month)
            {
                if (isWorkday(start))
                {
                    mWorkdaysList.Add(start);
                }
                start = start.AddDays(1);
            }
        }
    }
}
