using BM4.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BM4.Controllers
{
    public class FriendsController : BaseController
    {
        // GET: Friends
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                string userId = User.Identity.GetUserId();
                //List<FriendConnection> friends = context.FriendConnections.Where(t => t.UserId1 == userId && t.Status == "Approved").OrderBy(t => t.User2.FirstName).ToList();
                List<FriendConnection> friends = context.FriendConnections.Where(t => t.UserId1 == userId).OrderBy(t => t.User2.FirstName).ToList();
                return View(friends);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //public ActionResult Requests()
        //{
        //    ApplicationDbContext context = new ApplicationDbContext();
        //    string userId = User.Identity.GetUserId();
        //    List<FriendConnection> pendingRequests = context.FriendConnections.Where(t => t.UserId1 == userId && t.Status == "Pending").OrderByDescending(t => t.ConnectedDate).ToList();
        //    return View(pendingRequests);
        //}

        public ActionResult UpdateRequest(string Status, string UserId)
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                string cuserId = User.Identity.GetUserId();
                //List<FriendConnection> friends = context.FriendConnections.Where(t => t.UserId1 == userId && t.Status == "Approved").OrderBy(t => t.User2.FirstName).ToList();
                FriendConnection friend = context.FriendConnections.Where(t => t.UserId1 == cuserId && t.UserId2 == UserId).FirstOrDefault();
                if(friend != null)
                {
                    friend.Status = Status;
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            //Rejected
            //    Approved
        }
    }
}
