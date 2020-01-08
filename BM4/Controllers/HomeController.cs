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
        public ActionResult Index()
        {
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

            if(locationType.LocationTypes == "1" || locationType.LocationTypes == "2")
            {
                if(locationType.Standard.Length == 0)
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
