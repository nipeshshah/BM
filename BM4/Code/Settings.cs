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
    //USer 1 "FD1FF0D9-F692-4284-A425-01A395B8EDB6"
    //User 2 "32A0EE21-1670-4541-A083-E2AE4185166D"
    public static string UserId = "32A0EE21-1670-4541-A083-E2AE4185166D";
    public static string TestUserId1 = "FD1FF0D9-F692-4284-A425-01A395B8EDB6";
    public static string TestUserId2 = "32A0EE21-1670-4541-A083-E2AE4185166D";
  
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
