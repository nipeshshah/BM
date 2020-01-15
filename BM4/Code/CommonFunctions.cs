using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using BM4;
using BM4.Models;
using Microsoft.AspNet.Identity;

namespace BM4.Code
{
    public class CommonFunctions
    {
        public UserProfile CurrentUserProfile(IPrincipal User)
        {
            UserProfile profile = null;
            if (User.Identity.IsAuthenticated)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                if (context.UserProfiles.Count(t => t.UserName == User.Identity.Name) > 0)
                {
                    profile = context.UserProfiles.Where(t => t.UserName == User.Identity.Name).First();
                    ////string UserId = context.Users.Where(t => t.UserName == User.Identity.Name).First().Id;


                    //ViewBag.UserTitle = profile.Title;
                    //ViewBag.ProfilePic = profile.ProfilePic;
                }
                else
                {
                    string userId = context.Users.Where(t => t.UserName == User.Identity.Name).First().Id;
                    profile = context.UserProfiles.Add(new UserProfile()
                    {
                        UserId = userId,
                        UserName = User.Identity.Name,
                        DateOfBirth = DateTime.Now,
                        DateOfRegistration = DateTime.UtcNow
                    });
                    context.SaveChanges();
                }                
            }
            return profile;
        }
    }
}