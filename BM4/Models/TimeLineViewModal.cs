using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BM4.Models
{
    [NotMapped]
    public class TimeLineViewModal
    {
        public List<int> years { get; set; }

        public IEnumerable<string> months { get; set; }
        public IEnumerable<MonthEvents> monthEvents { get; set; }

        public int currentYear { get; set; }
        public int firstYear { get; set; }
        public int lastYear { get; set; }
    }

    [NotMapped]
    public class MonthEvents
    {
        public int month { get; set; }
        public int locationId { get; set; }
        public string location { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public bool IsInMonth(int year, int month)
        {
            DateTime dt = new DateTime(year, month, 1);
            if (dt >= startDate && dt <= endDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    [NotMapped]
    public class TimeLineDetailedModel
    {
        public int LocationId { get; set; }
        public string Title { get; set; }

        public List<UserViewModel> Users { get; set; }
    }

}