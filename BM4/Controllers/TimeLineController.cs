using BM4.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BM4.Controllers
{
    public class TimeLineController : BaseController
    {
        // GET: TimeLine
        public ActionResult Index(int? year)
        {
            /*	On top Current Year to Birth Year
	
	            Below Months of Selected Year
	
	            With Months, List of Locations
	
	            Button to Details
            */
            if (User.Identity.IsAuthenticated)
            {
                TimeLineViewModal modal = new TimeLineViewModal();
                ApplicationDbContext context = new ApplicationDbContext();
                BM4.Code.CommonFunctions functions = new Code.CommonFunctions();
                UserProfile user = functions.CurrentUserProfile(User);

                modal.years = new List<int>();
                for (int i = user.DateOfBirth.Year; i <= DateTime.Now.Year; i++)
                {
                    modal.years.Add(i);
                }
                modal.years.Reverse();
                modal.firstYear = modal.years.First();
                modal.lastYear = modal.years.Last();
                if (year.HasValue)
                {
                    modal.currentYear = modal.years[modal.years.IndexOf(year.Value)];
                }
                else
                {
                    modal.currentYear = modal.years[0];
                }
                modal.months = BM4.Code.Constants.MONTHNAMESHORT.Select(t => t + " " + modal.currentYear);

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
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
       
        public ActionResult EventDetail(int? id, int year, int month)
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                TimeLineDetailedModel model = new TimeLineDetailedModel();
                Location location = context.Locations.Where(t => t.LocationId == id).First();
                model.Title = location.Text1 + ", " + location.MainLocation.Text2 + ", " + location.MainLocation.City;
                string userId = User.Identity.GetUserId();
                DateTime currentDate = new DateTime(year, month, 1);
                model.Users = context.UserEvents.Where(t => t.LocationId == id && currentDate >= t.StartingDate && currentDate <= t.EndingDate)
                    .Select(x => new UserViewModel
                    {
                        Friend = x.User,
                        IsConnected = (context.FriendConnections.Count(t => t.User1.UserId == userId && t.User2.UserId == x.User.UserId && t.Status == "Approved") == 1),
                        Urls = context.UserConnections.Where(t => t.UserId == x.User.UserId),
                        Pic = x.User.ProfilePic,
                        ConnectionStatus = context.FriendConnections.FirstOrDefault(t => t.User1.UserId == userId && t.User2.UserId == x.User.UserId).Status
                    }).ToList();

                //model.Users = new List<string>();
                //model.Users.Add("User 1");
                //model.Users.Add("User 2");
                //model.Users.Add("User 3");
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public JsonResult SendConnectRequest(string FriendId)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            string userId = User.Identity.GetUserId();
            if (context.FriendConnections.Count(t => t.UserId1 == userId && t.UserId2 == FriendId) == 0)
            {
                context.FriendConnections.Add(new FriendConnection()
                {
                    UserId1 = User.Identity.GetUserId(),
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
