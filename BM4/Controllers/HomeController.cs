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
            if (User.Identity.IsAuthenticated)
            {
                BM4.Code.CommonFunctions functions = new Code.CommonFunctions();
                UserProfile profile = functions.CurrentUserProfile(User);

                ViewBag.UserTitle = string.IsNullOrEmpty(profile.Title) ? profile.UserName : profile.Title;
                ViewBag.ProfilePic = string.IsNullOrEmpty(profile.ProfilePic) ? "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSDgTNKTeE985pM29w_MVlLv6Q6zXuK8qHKq4O0pcB_aWH4JbQV&s" : profile.ProfilePic;
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpGet]
        public ActionResult LocationType()
        {
            if (User.Identity.IsAuthenticated)
                return View();
            else
                return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult LocationType(LocationType locationType)
        {
            List<string> errors = new List<string>();
            ApplicationDbContext context = new ApplicationDbContext();

            if (locationType.LocationTypes == "1" || locationType.LocationTypes == "2")
            {
                if (locationType.Standard == null || locationType.Standard.Length == 0)
                {
                    errors.Add("Standard(s) are Requred");                    
                }
                if(locationType.CitySearchbox == null || locationType.CitySearchbox.Length == 0)
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
                if (locationType.SemesterYear == null || locationType.SemesterYear.Length == 0)
                {
                    errors.Add("Semester or Year is Requred");
                }
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
    }
}
