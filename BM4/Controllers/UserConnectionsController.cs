using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BM4.Models;
using Microsoft.AspNet.Identity;

namespace BM4.Controllers
{
    public class UserConnectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserConnections
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var userConnections = db.UserConnections.Include(u => u.User).Where(t => t.UserId == userId);
            return View(userConnections.ToList());
        }

        // GET: UserConnections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserConnections userConnections = db.UserConnections.Find(id);
            if (userConnections == null)
            {
                return HttpNotFound();
            }
            return View(userConnections);
        }

        // GET: UserConnections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserConnections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConnectionId,UserId,AppType,AppUrl")] UserConnections userConnections)
        {
            if (ModelState.IsValid)
            {
                userConnections.UserId = User.Identity.GetUserId();
                db.UserConnections.Add(userConnections);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userConnections);
        }

        // GET: UserConnections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserConnections userConnections = db.UserConnections.Find(id);
            if (userConnections == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "Title", userConnections.UserId);
            return View(userConnections);
        }

        // POST: UserConnections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConnectionId,UserId,AppType,AppUrl")] UserConnections userConnections)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userConnections).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "Title", userConnections.UserId);
            return View(userConnections);
        }

        // GET: UserConnections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserConnections userConnections = db.UserConnections.Find(id);
            if (userConnections == null)
            {
                return HttpNotFound();
            }
            return View(userConnections);
        }

        // POST: UserConnections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserConnections userConnections = db.UserConnections.Find(id);
            db.UserConnections.Remove(userConnections);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
