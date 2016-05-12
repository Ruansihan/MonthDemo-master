using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using MonthDemo;

namespace MonthDemoTest
{
    [TestClass]
    public class AttendanceTest
    {
        [TestInitialize]
        public void Setup()
        {
            Environment.CurrentDirectory = "H:\\WPFProject\\MonthDemo\\MonthDemo\\bin\\Debug";
        }

        [TestMethod]
        public void TestReadRecord()
        {
            string str = "总公司	罗洪鹏	2	2012-04-05 12:33:41	1		FP	";
            var record = new AttendanceDate(str);
            Assert.AreEqual("总公司", record.Department);
            Assert.AreEqual("罗洪鹏", record.Name);
            Assert.AreEqual("2", record.Id);
            DateTime date = DateTime.Parse("2012-04-05 12:33:41");
            Assert.AreEqual(date, record.Date);
        }

        [TestMethod]
        public void TestProduceTestData()
        {
            var morningDate = DateTime.Parse("2012-01-01 09:00:00");
            var afternoonDate = DateTime.Parse("2012-01-01 18:00:00");
            var list = new List<AttendanceDate>();
            for (int i = 0; i < 31; i++)
            {
                var morning = new AttendanceDate
                {
                    Department = "开发",
                    Name = "郑文伟",
                    Id = "10",
                    Date = morningDate.AddDays(i)
                };
                var afternoon = new AttendanceDate
                {
                    Department = "开发",
                    Name = "郑文伟",
                    Id = "10",
                    Date = afternoonDate.AddDays(i)
                };
                list.Add(morning);
                list.Add(afternoon);
            }
            String path = "testdata.txt";
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine("title");
                foreach (var item in list)
                {
                    writer.WriteLine(item.ToString());
                }
            }
            var validate = AttendanceDate.LoadFile(path);
            Assert.AreEqual(list.Count, validate["10"].Count);
        }

        /*
        [TestMethod]
        public void TestProduceErrorData()
        {
            //因为2012年10月的1号正好是星期一
            var morningDate = DateTime.Parse("2012-10-01 09:00:00");
            var afternoonDate = DateTime.Parse("2012-10-01 18:00:00");
            AttendanceDate morning = null;
            AttendanceDate afternoon = null;
            var list = new List<AttendanceDate>();
            //迟到
            morning = new AttendanceDate
            {
                Department = "开发",
                Name = "郑文伟",
                Id = "10",
                Date = morningDate.AddMinutes(16)
            };
            afternoon = new AttendanceDate
            {
                Department = "开发",
                Name = "郑文伟",
                Id = "10",
                Date = afternoonDate
            };
            //1
            list.Add(morning);
            insertToiletRecord(list, morning, afternoon);
            list.Add(afternoon);
            morningDate = morningDate.AddDays(1);
            afternoonDate = afternoonDate.AddDays(1);
            //早退
            morning = new AttendanceDate
            {
                Department = "开发",
                Name = "郑文伟",
                Id = "10",
                Date = morningDate
            };
            afternoon = new AttendanceDate
            {
                Department = "开发",
                Name = "郑文伟",
                Id = "10",
                Date = afternoonDate.AddMinutes(-1)
            };
            //2
            list.Add(morning);
            insertToiletRecord(list, morning, afternoon);
            list.Add(afternoon);
            morningDate = morningDate.AddDays(1);
            afternoonDate = afternoonDate.AddDays(1);
            //错误
            //既迟到又早退
            morning = new AttendanceDate
            {
                Department = "开发",
                Name = "郑文伟",
                Id = "10",
                Date = morningDate.AddHours(1)
            };
            afternoon = new AttendanceDate
            {
                Department = "开发",
                Name = "郑文伟",
                Id = "10",
                Date = afternoonDate.AddHours(-1)
            };
            //3
            list.Add(morning);
            insertToiletRecord(list, morning, afternoon);
            list.Add(afternoon);
            morningDate = morningDate.AddDays(1);
            afternoonDate = afternoonDate.AddDays(1);
            //缺少上午的记录
            afternoon = new AttendanceDate
            {
                Department = "开发",
                Name = "郑文伟",
                Id = "10",
                Date = afternoonDate.AddHours(-1)
            };
            //4
            list.Add(afternoon);
            morningDate = morningDate.AddDays(1);
            afternoonDate = afternoonDate.AddDays(1);
            //缺席
            //5
            morningDate = morningDate.AddDays(1);
            afternoonDate = afternoonDate.AddDays(1);
            //加班
            morning = new AttendanceDate
            {
                Department = "开发",
                Name = "郑文伟",
                Id = "10",
                Date = morningDate
            };
            afternoon = new AttendanceDate
            {
                Department = "开发",
                Name = "郑文伟",
                Id = "10",
                Date = afternoonDate
            };
            //6
            list.Add(morning);
            insertToiletRecord(list, morning, afternoon);
            list.Add(afternoon);
            morningDate = morningDate.AddDays(1);
            afternoonDate = afternoonDate.AddDays(1);
            //加班错误（缺少下午的记录）
            afternoon = new AttendanceDate
            {
                Department = "开发",
                Name = "郑文伟",
                Id = "10",
                Date = afternoonDate
            };
            //7
            list.Add(afternoon);
            morningDate = morningDate.AddDays(1);
            afternoonDate = afternoonDate.AddDays(1);
            //正常上班
            while (morningDate.Month == 10)
            {
                if (morningDate.DayOfWeek != DayOfWeek.Saturday &&
                    morningDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    morning = new AttendanceDate
                    {
                        Department = "开发",
                        Name = "郑文伟",
                        Id = "10",
                        Date = morningDate
                    };
                    afternoon = new AttendanceDate
                    {
                        Department = "开发",
                        Name = "郑文伟",
                        Id = "10",
                        Date = afternoonDate
                    };
                    list.Add(morning);
                    insertToiletRecord(list, morning, afternoon);
                    list.Add(afternoon);
                }
                morningDate = morningDate.AddDays(1);
                afternoonDate = afternoonDate.AddDays(1);
            }

            String[] names = new String[] { 
                "邱震海",
                "杜宇",
                "吴明伟",
                "郑和",
                "爱新觉罗"
            };
            int i = 0;
            foreach (var item in names)
            {
                insertSomebody(item, "某部门", (i++).ToString(), list);
            }

            String path = "errdata.txt";
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine("title");
                foreach (var item in list)
                {
                    writer.WriteLine(item.ToString());
                }
            }
            var collection = MonthRecordCollection.LoadFile(path);
            Assert.AreEqual(2012, collection.Year);
            Assert.AreEqual(10, collection.Month);
            var records = collection.Records;
            var zwwRecords = records[0];
            Assert.AreEqual(1, zwwRecords.LateRecords.Count);
            Assert.AreEqual(1, zwwRecords.EarlyLeaveRecords.Count);
            Assert.AreEqual(2, zwwRecords.ErrorRecords.Count);
            Assert.AreEqual(1, zwwRecords.AbsenteeismRecords.Count);
            Assert.AreEqual(1, zwwRecords.OvertimeRecords.Count);
            Assert.AreEqual(1, zwwRecords.OvertimeWithErrorRecords.Count);
            Assert.AreEqual(18, zwwRecords.NormalRecords.Count);
            var luoRecords = records[1];
            Assert.AreEqual(23, luoRecords.NormalRecords.Count);
        }
        */

        [TestMethod]
        public void TestProduceMemberConfig()
        {
            String path = "members.txt";
            var collection = MonthDemo.MonthRecordCollection.LoadFile("201208.txt");
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (var item in collection.Records)
                {
                    writer.WriteLine(String.Format("{0} {1}", item.Id, item.Name));
                }
            }
        }

        [TestMethod]
        public void TestMemberConfig()
        {
            MemberConfig config = MemberConfig.GetInstance();
            Assert.AreEqual(36, config.Members.Count);
            Assert.AreEqual("蔡晓丽", config.Members["81"].Name);
        }

        private static void insertSomebody(String name, String department, String id, List<AttendanceDate> list)
        {
            AttendanceDate morning;
            AttendanceDate afternoon;
            var morningDate = DateTime.Parse("2012-10-01 09:00:00");
            var afternoonDate = DateTime.Parse("2012-10-01 18:00:00");
            for (int i = 0; i < 31; i++)
            {
                morning = new AttendanceDate
                {
                    Department = department,
                    Name = name,
                    Id = id,
                    Date = morningDate.AddDays(i)
                };
                afternoon = new AttendanceDate
                {
                    Department = department,
                    Name = name,
                    Id = id,
                    Date = afternoonDate.AddDays(i)
                };
                list.Add(morning);
                list.Add(afternoon);
            }
        }

        private Random mRandom = new Random();
        //穿插上厕所的记录
        private void insertToiletRecord(List<AttendanceDate> list, AttendanceDate start, AttendanceDate end)
        {
            int count = mRandom.Next(6);
            if (start.Date.AddMinutes(100) > end.Date)
            {
                return;
            }
            int totalMinutes = (end.Date.Hour - start.Date.Hour) * 60 + (end.Date.Minute - start.Date.Minute);
            int step = totalMinutes / 100;
            if (count > 0)
            {
                var positions = genRandomIntsBetween0To100(count);
                foreach (var item in positions)
                {
                    list.Add(new AttendanceDate()
                    {
                        Department = start.Department,
                        Name = start.Name,
                        Id = start.Id,
                        Date = start.Date.AddMinutes(item * step)
                    });
                }
            }
        }

        private List<int> genRandomIntsBetween0To100(int num)
        {
            List<int> result = new List<int>();
            int start = mRandom.Next(100);
            result.Add(start);
            for (int i = 1; i < num && start < 100; i++)
            {
                start = mRandom.Next(start, 100);
                result.Add(start);
                start++;
            }
            return result;
        }

        /*
        //全勤, VIVA
        [TestMethod]
        public void TestFull()
        {
            String path = "testdata.txt";
            var monthRecord = MonthRecordCollection.LoadFile(path).Records[0];
            Assert.AreEqual(2012, monthRecord.Year);
            Assert.AreEqual(1, monthRecord.Month);
            Assert.AreEqual(22, monthRecord.NormalRecords.Count);
            Assert.AreEqual(0, monthRecord.LateRecords.Count);
            Assert.AreEqual(0, monthRecord.EarlyLeaveRecords.Count);
            Assert.AreEqual(0, monthRecord.AbsenteeismRecords.Count);
            Assert.AreEqual(9, monthRecord.OvertimeRecords.Count);
        }
        */
    }
}
