using BM4.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BM4.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                Code.CommonFunctions functions = new Code.CommonFunctions();
                UserProfile userProfile = functions.CurrentUserProfile(User);
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
                    }).OrderByDescending(t => t.StartingDate).ToList();

                if (userProfile.DateOfBirth != null)
                {
                    userEvents.Add(new UserEventViewModel()
                    {
                        StartingDate = userProfile.DateOfBirth,
                        EndingDate = userProfile.DateOfBirth
                    });
                }
                List<UserEventViewModel> finalEvents = new List<UserEventViewModel>();
                //finalEvents.Add(userEvents.ElementAt(0));
                for (int i = 0; i < userEvents.Count() - 1; i++)
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
            else
            {
                return View(new List<UserEventViewModel>());
            }
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Referral()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {

            }
            return View();
        }

        [HttpGet]
        public ActionResult LocationType()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult LocationType(LocationType locationType)
        {
            List<string> errors = new List<string>();
            ApplicationDbContext context = new ApplicationDbContext();

            if (locationType.LocationTypes == "1" || locationType.LocationTypes == "2")
            {
                if (locationType.CitySearchbox == null || locationType.CitySearchbox.Length == 0)
                {
                    errors.Add("City is Requred");
                }
                if (locationType.SchoolName == null || locationType.SchoolName.Length == 0)
                {
                    errors.Add("School Name is Requred");
                }
                if (locationType.EducationBoard == null || locationType.EducationBoard.Length == 0)
                {
                    errors.Add("Education Board is Requred");
                }
                if (errors.Count > 0)
                {
                    ViewBag.Errors = errors;
                    return View(locationType);
                }
                MainLocation mainLocation;
                if (context.MainLocations.Count(t => t.City == locationType.CitySearchbox &&
                    t.Text2 == locationType.SchoolName &&
                    t.Text3 == locationType.EducationBoard) > 0)
                {
                    mainLocation = context.MainLocations.Where(t => t.City == locationType.CitySearchbox &&
                    t.Text2 == locationType.SchoolName &&
                    t.Text3 == locationType.EducationBoard).First();
                }
                else
                {
                    mainLocation = new MainLocation()
                    {
                        City = locationType.CitySearchbox,
                        Text2 = locationType.SchoolName,
                        Text3 = locationType.EducationBoard,
                        LocationType = locationType.LocationTypes
                    };
                    context.MainLocations.Add(mainLocation);
                }

                //context.LocationTypes.Add(locationType);
            }
            else if (locationType.LocationTypes == "3" || locationType.LocationTypes == "4")
            {
                if (locationType.CourseFaculty == null || locationType.CourseFaculty.Length == 0)
                {
                    errors.Add("Course/Faculty or Year is Requred");
                }
                if (locationType.College == null || locationType.College.Length == 0)
                {
                    errors.Add("College or Year is Requred");
                }
                if (locationType.University == null || locationType.University.Length == 0)
                {
                    errors.Add("University or Year is Requred");
                }
                if (errors.Count > 0)
                {
                    ViewBag.Errors = errors;
                    return View(locationType);
                }
                MainLocation mainLocation;
                if (context.MainLocations.Count(t => t.City == locationType.CitySearchbox &&
                     t.Text2 == locationType.CourseFaculty &&
                     t.Text3 == locationType.College &&
                     t.Text4 == locationType.University) > 0)
                {
                    mainLocation = context.MainLocations.Where(t => t.City == locationType.CitySearchbox &&
                     t.Text2 == locationType.CourseFaculty &&
                     t.Text3 == locationType.College &&
                     t.Text4 == locationType.University).First();
                }
                else
                {
                    mainLocation = new MainLocation()
                    {
                        City = locationType.CitySearchbox,
                        Text2 = locationType.CourseFaculty,
                        Text3 = locationType.College,
                        Text4 = locationType.University,
                        LocationType = locationType.LocationTypes
                    };
                    context.MainLocations.Add(mainLocation);
                }
            }
            if (context.GetValidationErrors().Count() > 0)
            {
                return View(locationType);
            }
            else
            {
                int result = context.SaveChanges();
                return RedirectToAction("LocationType");
            }
        }

        [HttpGet]
        public ActionResult SubLocationType()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationDbContext context = new ApplicationDbContext();

                var fromCityDatabaseEF = new
                            SelectList(context.MainLocations.Select(t => new SelectListItem() { Text = t.City, Value = t.City }).Distinct().OrderBy(t => t.Text).ToList(), "Value", "Text");
                ViewData["City"] = fromCityDatabaseEF;

                var locationSubTypeEntites = new
                        SelectList(context.MainLocations.Where(t => t.MainLocationId == -1).Select(t => new SelectListItem() { Text = t.City, Value = t.City }).Distinct().OrderBy(t => t.Text).ToList(), "Value", "Text");
                ViewData["LocationSubTypes"] = locationSubTypeEntites;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult SubLocationType(SubLocationType subLocationType)
        {
            List<string> errors = new List<string>();
            ApplicationDbContext context = new ApplicationDbContext();

            if (subLocationType.LocationTypeId == 1 || subLocationType.LocationTypeId == 2)
            {
                if (subLocationType.Standard == null || subLocationType.Standard.Length == 0)
                {
                    errors.Add("Standard(s) are Requred");
                }
                if (errors.Count > 0)
                {
                    ViewBag.Errors = errors;
                    return View(subLocationType);
                }
                string[] standards = subLocationType.Standard.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                MainLocation mainLocation;
                if (context.MainLocations.Count(t => t.MainLocationId == subLocationType.LocationSubType) > 0)
                {
                    mainLocation = context.MainLocations.Where(t => t.MainLocationId == subLocationType.LocationSubType).First();
                    foreach (string standard in standards)
                    {
                        Location location = new Location()
                        {
                            LocationTypes = subLocationType.LocationSubType.ToString(),
                            Text1 = standard.Trim(),
                            MainLocation = mainLocation
                        };
                        context.Locations.Add(location);
                    }
                }
                else
                {
                    errors.Add("Please select Location");
                    return View(subLocationType);
                }
            }
            else if (subLocationType.LocationTypeId == 3 || subLocationType.LocationTypeId == 4)
            {
                if (subLocationType.SemesterYear == null || subLocationType.SemesterYear.Length == 0)
                {
                    errors.Add("Semester or Year is Requred");
                }
                if (errors.Count > 0)
                {
                    ViewBag.Errors = errors;
                    return View(subLocationType);
                }
                string[] semisters = subLocationType.SemesterYear.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                MainLocation mainLocation;
                if (context.MainLocations.Count(t => t.MainLocationId == subLocationType.LocationSubType) > 0)
                {
                    mainLocation = context.MainLocations.Where(t => t.MainLocationId == subLocationType.LocationSubType).First();
                    foreach (string sems in semisters)
                    {
                        Location location = new Location()
                        {
                            LocationTypes = subLocationType.LocationSubType.ToString(),
                            Text1 = sems.Trim(),
                            MainLocation = mainLocation
                        };
                        context.Locations.Add(location);
                    }
                }
                else
                {
                    errors.Add("Please select Location");
                    return View(subLocationType);
                }
                //context.LocationTypes.Add(locationType);
            }
            if (context.GetValidationErrors().Count() > 0)
            {
                return View(subLocationType);
            }
            else
            {
                int result = context.SaveChanges();
                return RedirectToAction("LocationType");
            }
        }

        [HttpPost]
        public ActionResult GetLocations(string CityName, string LocationTypeId)
        {
            if (!string.IsNullOrEmpty(CityName) && string.IsNullOrEmpty(LocationTypeId))
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var fromDatabaseEF = context.MainLocations.Where(t => t.City == CityName).OrderBy(t => t.Text2).ToList();
                return Json(fromDatabaseEF);
            }
            else if (string.IsNullOrEmpty(CityName) && !string.IsNullOrEmpty(LocationTypeId))
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var fromDatabaseEF = context.MainLocations.Where(t => t.LocationType == LocationTypeId).OrderBy(t => t.Text2).ToList();
                return Json(fromDatabaseEF);
            }
            else if (!string.IsNullOrEmpty(CityName) && !string.IsNullOrEmpty(LocationTypeId))
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var fromDatabaseEF = context.MainLocations.Where(t => t.City == CityName && t.LocationType == LocationTypeId).OrderBy(t => t.Text2).ToList();
                return Json(fromDatabaseEF);
            }
            return null;
        }

        public ActionResult Splash()
        {
            return View();
        }
    }
}
