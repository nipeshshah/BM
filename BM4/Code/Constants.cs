using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BM4.Code
{
  public class Constants
  {
  public static List<string> MONTHNAMESHORT =
  Enumerable.Range(1, 12).Select(i => DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(i) ).ToList();

    public static List<string> MONTHNAMEFULL =
    Enumerable.Range(1, 12).Select(i => DateTimeFormatInfo.CurrentInfo.GetMonthName(i)).ToList();
  }
}
