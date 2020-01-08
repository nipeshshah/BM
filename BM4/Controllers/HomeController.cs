using BM4.Code;
using BM4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BM4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(bool? CreateTestData)
        {
            if (CreateTestData.HasValue && CreateTestData.Value)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                context.UserEvents.Add(new UserEvent()
                {
                    LocationId = 13,
                    StartingDate = new DateTime(2019, 3, 1),
                    EndingDate = new DateTime(2019, 6, 1),
                    UserId = Settings.TestUserId1
                });

                context.UserEvents.Add(new UserEvent()
                {
                    LocationId = 14,
                    StartingDate = new DateTime(2019, 7, 1),
                    EndingDate = new DateTime(2019, 10, 1),
                    UserId = Settings.TestUserId1
                });

                context.UserEvents.Add(new UserEvent()
                {
                    LocationId = 15,
                    StartingDate = new DateTime(2019, 11, 1),
                    EndingDate = new DateTime(2020, 2, 1),
                    UserId = Settings.TestUserId1
                });

                context.UserEvents.Add(new UserEvent()
                {
                    LocationId = 13,
                    StartingDate = new DateTime(2019, 3, 1),
                    EndingDate = new DateTime(2019, 6, 1),
                    UserId = Settings.TestUserId2
                });

                context.UserEvents.Add(new UserEvent()
                {
                    LocationId = 14,
                    StartingDate = new DateTime(2019, 7, 1),
                    EndingDate = new DateTime(2019, 10, 1),
                    UserId = Settings.TestUserId2
                });

                context.UserConnections.Add(new UserConnections()
                {
                    AppType = "FB",
                    AppUrl = "http://FB.Test/TestUser1",
                    UserId = Settings.TestUserId1
                });

                context.UserConnections.Add(new UserConnections()
                {
                    AppType = "TW",
                    AppUrl = "http://TW.Test/TestUser1",
                    UserId = Settings.TestUserId1
                });

                context.UserConnections.Add(new UserConnections()
                {
                    AppType = "FB",
                    AppUrl = "http://FB.Test/TestUser2",
                    UserId = Settings.TestUserId2
                });

                context.SaveChanges();
            }
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpGet]
        public ActionResult LocationType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LocationType(LocationType locationType)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            if (locationType.LocationTypes == "1" || locationType.LocationTypes == "2")
            {
                if (locationType.Standard.Length == 0)
                {
                    throw new Exception("Standard is required");
                }
                string[] standards = locationType.Standard.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

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
                        Text3 = locationType.EducationBoard
                    };
                    context.MainLocations.Add(mainLocation);
                }

                foreach (string standard in standards)
                {
                    Location location = new Location()
                    {
                        LocationTypes = locationType.LocationTypes,
                        Text1 = standard.Trim(),
                        MainLocation = mainLocation
                    };
                    context.Locations.Add(location);
                }
                //context.LocationTypes.Add(locationType);
            }
            else if (locationType.LocationTypes == "3" || locationType.LocationTypes == "4")
            {
                if (locationType.SemesterYear.Length == 0)
                {
                    throw new Exception("Semisters are required");
                }
                string[] semisters = locationType.SemesterYear.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
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
                        Text4 = locationType.University
                    };
                    context.MainLocations.Add(mainLocation);
                }
                foreach (string sems in semisters)
                {
                    Location location = new Location()
                    {
                        LocationTypes = locationType.LocationTypes,
                        Text1 = sems.Trim(),
                        MainLocation = mainLocation
                    };
                    context.Locations.Add(location);
                }
                //context.LocationTypes.Add(locationType);
            }

            int result = context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
