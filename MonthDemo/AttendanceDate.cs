using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MonthDemo
{
    public class AttendanceDate
    {
        private static char[] SEPS = new char[]{' ', '\t'};
        public String Department { get; set;}
        public String Name { get; set; }
        public String Id { get; set; }
        public DateTime Date { get; set; }

        public AttendanceDate()
        {
        }

        public AttendanceDate(String str)
        {
            String[] fields = str.Split(SEPS);
            Department = fields[0];
            Name = fields[1];
            Id = fields[2];
            String dateStr = fields[3] + " " + fields[4];
            Date = DateTime.Parse(dateStr);
        }

        public override string ToString()
        {
            return Department + " " + Name + " " + Id + " " + Date.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static Dictionary<String, List<AttendanceDate>> LoadFile(String path)
        {
            var result = new Dictionary<String, List<AttendanceDate>>();
            using (StreamReader reader = new StreamReader(path))
            {
                //丢弃第一行
                reader.ReadLine();
                while (true)
                {
                    String line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    try
                    {
                        var record = new AttendanceDate(line);
                        String id = record.Id;
                        if (!result.ContainsKey(id))
                        {
                            result[id] = new List<AttendanceDate>();
                        }
                        result[id].Add(record);
                    }
                    catch(Exception e)
                    {
                        throw new FileFormatException(line, e);
                    }
                }
            }
            return result;
        }
    }
}
