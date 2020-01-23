using BM4.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BM4.Controllers
{
    public class EventsController : BaseController
  {
        // GET: Events
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                using(ApplicationDbContext context = new ApplicationDbContext())
                {
                    var fromCityDatabaseEF = new
                        SelectList(context.MainLocations.Select(t => new SelectListItem() { Text = t.City, Value = t.City }).Distinct().OrderBy(t => t.Text).ToList(), "Value", "Text");
                    ViewData["City"] = fromCityDatabaseEF;
                }

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult Index(UserEvent userEvent)
        {
            ViewData["Error"] = "";
            if(User.Identity.IsAuthenticated)
            {
                BM4.Code.CommonFunctions commonFunctions = new Code.CommonFunctions();
                userEvent.UserId = User.Identity.GetUserId(); // commonFunctions.CurrentUserProfile(User).UserId;
                if(!ModelState.IsValid)
                {
                    using(ApplicationDbContext contextx = new ApplicationDbContext())
                    {
                        var fromCityDatabaseEF = new
                            SelectList(contextx.MainLocations.Select(t => new SelectListItem() { Text = t.City, Value = t.City }).Distinct().OrderBy(t => t.Text).ToList(), "Value", "Text");
                        ViewData["City"] = fromCityDatabaseEF;
                    }
                    return View(userEvent);
                }
                if(userEvent.StartingDate > userEvent.EndingDate)
                {
                    using(ApplicationDbContext contextx = new ApplicationDbContext())
                    {
                        var fromCityDatabaseEF = new
                            SelectList(contextx.MainLocations.Select(t => new SelectListItem() { Text = t.City, Value = t.City }).Distinct().OrderBy(t => t.Text).ToList(), "Value", "Text");
                        ViewData["City"] = fromCityDatabaseEF;
                    }
                    ModelState.AddModelError("InvalidDates", "Start Date should be smaller then End Date");
                    return View(userEvent);
                }
                using(ApplicationDbContext context = new ApplicationDbContext())
                {
                    context.UserEvents.Add(userEvent);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult ViewAllEvents()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            string userId = User.Identity.GetUserId();
            List<UserEventViewModel> userEvents = context.UserEvents.Where(t => t.UserId == userId)
                .Select(x => new UserEventViewModel()
                {
                    EventId = x.UserEventId,
                    Location1 = x.Location.Text1,
                    Location2 = x.Location.MainLocation.Text2,
                    Location3 = x.Location.MainLocation.Text3,
                    Location4 = x.Location.MainLocation.Text4,
                    City = x.Location.MainLocation.City,
                    StartingDate = x.StartingDate,
                    EndingDate = x.EndingDate
                }).OrderByDescending(t=> t.StartingDate).ToList();

            userEvents.Add(new UserEventViewModel()
            {
                StartingDate = context.UserProfiles.Where(t => t.UserId == userId).First().DateOfBirth,
                EndingDate = context.UserProfiles.Where(t => t.UserId == userId).First().DateOfBirth
            });

            List<UserEventViewModel> finalEvents = new List<UserEventViewModel>();
            //finalEvents.Add(userEvents.ElementAt(0));
            for (int i=0;i< userEvents.Count()-1; i++)
            {
                finalEvents.Add(userEvents.ElementAt(i));
                if (userEvents.ElementAt(i + 1).EndingDate < userEvents.ElementAt(i).StartingDate)
                {
                    finalEvents.Add(new UserEventViewModel()
                    {
                        Location1 = "Where you have been ? Add your life event and connect with your friends.",
                        StartingDate = userEvents.ElementAt(i + 1).EndingDate,
                        EndingDate = userEvents.ElementAt(i).StartingDate,
                        HasScope = true
                    });
                }

            }
            return View(finalEvents.OrderByDescending(t => t.StartingDate));
        }

        [HttpPost]
        public ActionResult GetLocations(string CityName)
        {
            if(!string.IsNullOrEmpty(CityName))
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var fromDatabaseEF = context.MainLocations.Where(t => t.City == CityName).OrderBy(t => t.Text2).ToList();
                return Json(fromDatabaseEF);
            }
            return null;
        }

        [HttpPost]
        public ActionResult GetSubLocations(string CityName, int LocationId)
        {
            if (!string.IsNullOrEmpty(CityName))
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var fromDatabaseEF = context.Locations.Where(t => t.MainLocation.City == CityName && t.MainLocationId == LocationId).OrderBy(t => t.MainLocation.Text2).ThenBy(t => t.Text1).Select(
                    c =>
                    new
                    {
                        c.LocationId,
                        c.Text1
                    }).ToList();
                return Json(fromDatabaseEF);
            }
            return null;
        }

        public ActionResult DeleteEvent(int Id)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                UserEvent userEvent = context.UserEvents.Where(t => t.UserEventId == Id).First();
                context.UserEvents.Remove(userEvent);
                context.SaveChanges();
            }
            return RedirectToAction("ViewAllEvents");
        }
    }
}
