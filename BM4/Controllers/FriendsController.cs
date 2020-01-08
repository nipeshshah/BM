using BM4.Code;
using BM4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BM4.Controllers
{
    public class FriendsController : Controller
    {
        // GET: Friends
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            List<FriendConnection> friends = context.FriendConnections.Where(t => t.UserId2 == Settings.UserId && t.Status == "Approved").OrderBy(t => t.User1.FirstName).ToList();
            return View(friends);
        }

        public ActionResult Requests()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            List<FriendConnection> pendingRequests = context.FriendConnections.Where(t => t.UserId2 == Settings.UserId && t.Status == "Pending").OrderByDescending(t => t.ConnectedDate).ToList();
            return View(pendingRequests);
        }
    }
}
