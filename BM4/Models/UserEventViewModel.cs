using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BM4.Models
{
  [NotMapped]
  public class UserEventViewModel
  {
    public int EventId { get; internal set; }
    public string Location1 { get; internal set; }
    public string Location2 { get; internal set; }
    public string Location3 { get; internal set; }
    public string Location4 { get; internal set; }
    public string City { get; internal set; }
    public DateTime StartingDate { get; internal set; }
    public DateTime EndingDate { get; internal set; }
  }
}
