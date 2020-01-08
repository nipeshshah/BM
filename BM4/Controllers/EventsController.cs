using BM4.Models;
using System.Linq;
using System.Web.Mvc;

namespace BM4.Controllers
{
  public class EventsController : Controller
  {
    // GET: Events
    public ActionResult Index()
    {
      using(ApplicationDbContext context = new ApplicationDbContext())
      {
        var fromCityDatabaseEF = new SelectList(context.MainLocations.Select(t => new { t.City }).Distinct().OrderBy(t => t.City).ToList(), "City", "City");
        ViewData["City"] = fromCityDatabaseEF;
      }
      Code.Settings.UserId(User, Session);
      return View();
    }

    public ActionResult AllEvents()
    {
      using(ApplicationDbContext context = new ApplicationDbContext())
      {
        string UserId = "";
        var fromCityDatabaseEF = context.UserEvents.Where(t => t.UserId == UserId).OrderBy(t => t.StartingDate).ToList();
        View(fromCityDatabaseEF);
      }
      return View();
    }

    [HttpPost]
    public ActionResult Index(UserEvent userEvent)
    {
      ApplicationDbContext context = new ApplicationDbContext();
      context.UserEvents.Add(userEvent);
      context.SaveChanges();
      return RedirectToAction("Index");
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
  }
}
