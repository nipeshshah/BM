using BM4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BM4.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UpdateProfile()
        {
            ApplicationDbContext context = new ApplicationDbContext();
      if(User.Identity.IsAuthenticated)
      {
        if(context.Users.Count(t => t.UserName == User.Identity.Name) > 0)
        {
          string UserId = context.Users.Where(t => t.UserName == User.Identity.Name).First().Id;
          if(context.UserProfiles.Count(t => t.UserId == UserId) == 0)
          {
            context.UserProfiles.Add(new UserProfile()
            {
              UserId = UserId
            });
            context.SaveChanges();
          }

          UserProfile profile = context.UserProfiles.Where(t => t.UserId == UserId).First();
          return View(profile);
        }
      }
      return View(new UserProfile());
        }

        [HttpPost]
        public ActionResult UpdateProfile(UserProfile userProfile)
        { 
            //ApplicationDbContext context = new ApplicationDbContext();
            //if (User.Identity.IsAuthenticated)
            //{
            //    if (context.Users.Count(t => t.UserName == User.Identity.Name) > 0)
            //    {
            //        string UserId = context.Users.Where(t => t.UserName == User.Identity.Name).First().Id;
            //        UserProfile profile = context.UserProfiles.Where(t => t.UserId == UserId).First();
            //        return View(profile);
            //    }
            //}
            return View(new UserProfile());
        }
    }
}
