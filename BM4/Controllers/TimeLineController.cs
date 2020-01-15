using BM4.Code;
using BM4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BM4.Controllers
{
  public class TimeLineController : Controller
  {
    // GET: TimeLine
    public ActionResult Index(int? year)
    {
      /*    On top Current Year to Birth Year
    
                Below Months of Selected Year
    
                With Months, List of Locations
    
                Button to Details
            */

      TimeLineViewModal modal = new TimeLineViewModal();
      ApplicationDbContext context = new ApplicationDbContext();
      //UserProfile user = context.UserProfiles.Where(t => t.UserId == Settings.UserId(User)).First();
      UserProfile user = context.UserProfiles.Where(t => t.UserName == User.Identity.Name).First();
      modal.years = new List<int>();
      for(int i = user.DateOfBirth.Year; i <= DateTime.Now.Year; i++)
      {
        modal.years.Add(i);
      }
      modal.years.Reverse();
      modal.firstYear = modal.years.First();
      modal.lastYear = modal.years.Last();
      if(year.HasValue)
      {
        modal.currentYear = modal.years[modal.years.IndexOf(year.Value)];
      }
      else
      {
        modal.currentYear = modal.years[0];
      }
      modal.months = Constants.MONTHNAMESHORT.Select(t => t + " " + modal.currentYear);

      modal.monthEvents = context.UserEvents
//.Include("Locations,MainLocations,LocationTypes")
//.Include(x => x.)
                .Where(t => t.UserId == user.UserId && t.StartingDate.Year <= modal.currentYear && t.EndingDate.Year >= modal.currentYear)
        .Select(x => new MonthEvents()
        {
          startDate = x.StartingDate,
          endDate = x.EndingDate,
          locationId = x.LocationId,
          location = x.Location.Text1 + ", " + x.Location.MainLocation.Text2,
          locationsubheading = x.Location.MainLocation.Text3 + "<br/>" +
            x.Location.MainLocation.Text4 + "<br/>" +
            x.Location.MainLocation.City
        }).ToList();

      return View(modal);
    }

    public ActionResult EventDetail(int? id, int year, int month)
    {
      ApplicationDbContext context = new ApplicationDbContext();
      TimeLineDetailedModel model = new TimeLineDetailedModel();
      Location location = context.Locations.Where(t => t.LocationId == id).First();
      model.Title = location.Text1 + ", " + location.MainLocation.Text2 + ", " + location.MainLocation.City;

      DateTime currentDate = new DateTime(year, month, 1);
      model.Users = context.UserEvents.Where(t => t.LocationId == id && currentDate >= t.StartingDate && currentDate <= t.EndingDate)
        .Select(x => new UserViewModel
        {
          Friend = x.User,
          IsConnected = (context.FriendConnections.Count(t => t.User1.UserId == Settings.UserId(User) && t.User2.UserId == x.User.UserId && t.Status == "Approved") == 1),
          Urls = context.UserConnections.Where(t => t.UserId == x.User.UserId),
          Pic = x.User.ProfilePic,
          ConnectionStatus = context.FriendConnections.FirstOrDefault(t => t.User1.UserId == Settings.UserId(User) && t.User2.UserId == x.User.UserId).Status
        }).ToList();
      return View(model);
    }

    [HttpGet]
    public JsonResult SendConnectRequest(string FriendId)
    {
      ApplicationDbContext context = new ApplicationDbContext();
      if(context.FriendConnections.Count(t => t.UserId1 == Settings.UserId(User) && t.UserId2 == FriendId) == 0)
      {
        context.FriendConnections.Add(new FriendConnection()
        {
          UserId1 = Settings.UserId(User),
          UserId2 = FriendId,
          Status = "Pending",
          ConnectedDate = DateTime.Now
        });

        context.SaveChanges();
      }
      return Json("Sent", JsonRequestBehavior.AllowGet);
    }
  }
}
