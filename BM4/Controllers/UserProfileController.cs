using BM4.Code;
using BM4.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BM4.Controllers
{
    public class UserProfileController : BaseController
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
            if (User.Identity.IsAuthenticated)
            {
                CommonFunctions functions = new CommonFunctions();
                UserProfile profile = functions.CurrentUserProfile(User);
                return View(profile);
            }
            return RedirectToAction("Login","Account");
        }

        [HttpPost]
        public ActionResult PostProfile([Bind(Include = "Title,FirstName,MiddleName,LastName,DateOfBirth,ProfileImage,ProfilePic,City,UserId,UserName")] UserProfile userProfile)
        {
            //if (ModelState.IsValid)
            //{
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                if (file.ContentLength > 0)
                {
                    //var fileName = Path.GetFileName(file.FileName);
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = userProfile.UserId + extension;
                    file.SaveAs(Path.Combine(Server.MapPath("~/Images"), fileName));
                    userProfile.ProfilePic = ConfigurationManager.AppSettings["UserImagePath"] + fileName;
                }

                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    context.UserProfiles.Attach(userProfile);
                    context.Entry(userProfile).Property(x => x.Title).IsModified = true;
                    context.Entry(userProfile).Property(x => x.FirstName).IsModified = true;
                    context.Entry(userProfile).Property(x => x.MiddleName).IsModified = true;
                    context.Entry(userProfile).Property(x => x.LastName).IsModified = true;
                    context.Entry(userProfile).Property(x => x.DateOfBirth).IsModified = true;
                    context.Entry(userProfile).Property(x => x.ProfilePic).IsModified = true;
                    context.Entry(userProfile).Property(x => x.City).IsModified = true;

                    //UserProfile newProfile = context.Students.Find(id);
                    //context.Entry(userProfile).State = System.Data.Entity.EntityState.Modified;
                    System.Diagnostics.Debug.WriteLine(context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s));
                    context.SaveChanges();
                }
                return RedirectToAction("UpdateProfile");
            }            
            return View(new UserProfile());
        }

        public ActionResult Public(string UserId)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated || true)
            {
                UserProfile profile = context.UserProfiles.Where(t => t.UserId == UserId).First();
                profile.UserConnections = context.UserConnections.Where(t => t.UserId == UserId).ToList();
                return View(profile);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
