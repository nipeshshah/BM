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
            if (User.Identity.IsAuthenticated)
            {
                CommonFunctions functions = new CommonFunctions();
                UserProfile profile = functions.CurrentUserProfile(User);
                return View(profile);
            }
            return View(new UserProfile());
        }

        [HttpPost]
        public ActionResult PostProfile(UserProfile userProfile)
        {
            //if (ModelState.IsValid)
            //{
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    userProfile.ProfilePic = Path.Combine(
                        Server.MapPath("~/Images"), fileName);
                    file.SaveAs(userProfile.ProfilePic);
                }

                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    userProfile.UserName = User.Identity.Name;
                    context.Entry(userProfile).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
                return RedirectToAction("UpdateProfile");
            }
            //}

            //Use Namespace called :  System.IO  
            //string FileName = Path.GetFileNameWithoutExtension(userProfile.ProfileImage.FileName);

            ////To Get File Extension  
            //string FileExtension = Path.GetExtension(userProfile.ProfileImage.FileName);

            ////Add Current Date To Attached File Name  
            //FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

            ////Get Upload path from Web.Config file AppSettings.  
            //string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();

            ////Its Create complete path to store in server.  
            //userProfile.ProfilePic = UploadPath + FileName;

            ////To copy and save file into server.  
            //userProfile.ProfileImage.SaveAs(userProfile.ProfilePic);

            //return View();
            return View(new UserProfile());
        }

        public ActionResult Public(string UserId)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated || true)
            {
                UserProfile profile = context.UserProfiles.Where(t => t.UserId == UserId).First();
                return View(profile);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}