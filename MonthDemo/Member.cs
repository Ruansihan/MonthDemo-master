using System;
using System.Linq;
using System.Text;

namespace MonthDemo
{
    public class Member
    {
        public String Id { get; private set; }
        public String Name { get; private set; }

        public Member(String str)
        {
            String[] fields = str.Split();
            Id = fields[0];
            Name = fields[1];
        }
    }
}
