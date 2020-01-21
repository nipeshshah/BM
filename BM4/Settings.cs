using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BM4
{
    public class Settings
    {
        //USer 1 "FD1FF0D9-F692-4284-A425-01A395B8EDB6"
        //User 2 "32A0EE21-1670-4541-A083-E2AE4185166D"
        //public static string UserId = "FD1FF0D9-F692-4284-A425-01A395B8EDB6";
        //public static string TestUserId1 = "FD1FF0D9-F692-4284-A425-01A395B8EDB6";
        //public static string TestUserId2 = "32A0EE21-1670-4541-A083-E2AE4185166D";
    }
    public class Constants
    {
        public static List<string> MONTHNAMESHORT = Enumerable.Range(1, 12).Select(i => DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(i)).ToList();
        public static List<string> MONTHNAMESFULL = Enumerable.Range(1, 12).Select(i => DateTimeFormatInfo.CurrentInfo.GetMonthName(i)).ToList();
    }
}