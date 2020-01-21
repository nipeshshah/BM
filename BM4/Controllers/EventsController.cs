using BM4.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace BM4.Controllers
{
  public class EventsController : BaseController
  {
    // GET: Events
    public ActionResult Index()
    {
      if(User.Identity.IsAuthenticated)
      {
        using(ApplicationDbContext context = new ApplicationDbContext())
        {
          var fromCityDatabaseEF = new
              SelectList(context.MainLocations.Select(t => new SelectListItem() { Text = t.City, Value = t.City }).Distinct().OrderBy(t => t.Text).ToList(), "Value", "Text");
          ViewData["City"] = fromCityDatabaseEF;
        }

        return View();
      }
      else
      {
        return RedirectToAction("Login", "Account");
      }
    }

    [HttpPost]
    public ActionResult Index(UserEvent userEvent)
    {
      ViewData["Error"] = "";
      if(User.Identity.IsAuthenticated)
      {
        BM4.Code.CommonFunctions commonFunctions = new Code.CommonFunctions();
        userEvent.UserId = User.Identity.GetUserId(); // commonFunctions.CurrentUserProfile(User).UserId;
        if(!ModelState.IsValid)
        {
          using(ApplicationDbContext contextx = new ApplicationDbContext())
          {
            var fromCityDatabaseEF = new
                SelectList(contextx.MainLocations.Select(t => new SelectListItem() { Text = t.City, Value = t.City }).Distinct().OrderBy(t => t.Text).ToList(), "Value", "Text");
            ViewData["City"] = fromCityDatabaseEF;
          }
          return View(userEvent);
        }
        if(userEvent.StartingDate > userEvent.EndingDate)
        {
          using(ApplicationDbContext contextx = new ApplicationDbContext())
          {
            var fromCityDatabaseEF = new
                SelectList(contextx.MainLocations.Select(t => new SelectListItem() { Text = t.City, Value = t.City }).Distinct().OrderBy(t => t.Text).ToList(), "Value", "Text");
            ViewData["City"] = fromCityDatabaseEF;
          }
          ModelState.AddModelError("InvalidDates", "Start Date should be smaller then End Date");
          return View(userEvent);
        }
        using(ApplicationDbContext context = new ApplicationDbContext())
        {
          context.UserEvents.Add(userEvent);
          context.SaveChanges();
          return RedirectToAction("Index");
        }
      }
      else
      {
        return RedirectToAction("Login", "Account");
      }
    }

    public ActionResult ViewAllEvents()
    {
      return View();
    }

    [HttpPost]
    public ActionResult GetLocations(string CityName)
    {
      if(!string.IsNullOrEmpty(CityName))
      {
        ApplicationDbContext context = new ApplicationDbContext();
        var fromDatabaseEF = context.Locations.Where(t => t.MainLocation.City == CityName).OrderBy(t => t.MainLocation.Text2).ThenBy(t => t.Text1).Select(
            c =>
            new
            {
              c.LocationId,
              Text1 = c.Text1 + ", " + c.MainLocation.Text2 + ", " + c.MainLocation.Text3 + ", " + c.MainLocation.Text4
            }).ToList();
        return Json(fromDatabaseEF);
      }
      return null;
    }

    public ActionResult DeleteEvent(int Id)
    {
      using(ApplicationDbContext context = new ApplicationDbContext())
      {
        UserEvent userEvent = context.UserEvents.Where(t => t.UserEventId == Id).First();
        context.UserEvents.Remove(userEvent);
        context.SaveChanges();
      }
      return RedirectToAction("ViewAllEvents");
    }
  }
}
