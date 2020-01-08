using BM4.Models;
using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace BM4.Code
{
  public class Settings
  {
    internal static string UserName(IPrincipal user)
    {
      if(user.Identity.IsAuthenticated)
      {
        return user.Identity.Name;
      }
      else
      {
        return string.Empty;
      }
    }

    public static string UserId = "F8C46DEC-F2BC-46A2-AD78-41981227668A";
    public static string TestUserId1 = "D4F6C157-E2BF-473E-82B4-977A2240D2AA";
    public static string TestUserId2 = "62F729D9-A32D-4D9B-9034-AF910B9BB569";

    internal static string UserIdX(IPrincipal user, HttpSessionStateBase session)
    {
      if(UserName(user) == string.Empty && session["USER"] == null)
      {
        return string.Empty;
      }
      else if(UserName(user) != string.Empty && session["USER"] != null)
      {
        return session["USER"].ToString();
      }
      else if(UserName(user) != string.Empty && session["USER"] == null)
      {
        ApplicationDbContext context = new ApplicationDbContext();
        var userId = context.Users.Where(t => t.UserName == UserName(user)).First().Id;
        session["USER"] = userId;
        return userId;
      }
      return string.Empty;
    }
  }
}
