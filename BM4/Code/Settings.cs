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

    public static string UserId = "";
    public static string TestUserId1 = "";
    public static string TestUserId2 = "";

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
