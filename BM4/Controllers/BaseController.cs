using BM4.Models;
using System.Web.Mvc;

namespace BM4.Controllers
{
    public abstract partial class BaseController : Controller
    {
        public BaseController()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                BM4.Code.CommonFunctions functions = new Code.CommonFunctions();
                UserProfile profile = functions.CurrentUserProfile(System.Web.HttpContext.Current.User);

                ViewBag.UserTitle = string.IsNullOrEmpty(profile.Title) ? profile.UserName : profile.Title;
                ViewBag.ProfilePic = string.IsNullOrEmpty(profile.ProfilePic) ? "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSDgTNKTeE985pM29w_MVlLv6Q6zXuK8qHKq4O0pcB_aWH4JbQV&s" : profile.ProfilePic;
            }
        }
    }
}