using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MonthDemo
{
    public class MemberConfig
    {
        public Dictionary<String, Member> Members { get; private set; }

        private static MemberConfig sInstance = null;
        public static MemberConfig GetInstance()
        {
            if (sInstance == null)
            {
                sInstance = new MemberConfig();
            }
            return sInstance;
        }

        public MemberConfig()
        {
            Members = LoadFile("members.txt");
        }

        private static Dictionary<String, Member> LoadFile(String path)
        {
            var result = new Dictionary<String, Member>();
            using (StreamReader reader = new StreamReader(path))
            {
                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    var member = new Member(line);
                    result[member.Id] = member;
                }
            }
            return result;
        }
    }
}
