using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace BM4.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<BM4.Models.LocationType> LocationTypes { get; set; }
        public System.Data.Entity.DbSet<BM4.Models.Location> Locations { get; set; }
        public System.Data.Entity.DbSet<BM4.Models.MainLocation> MainLocations { get; set; }
                                          
        public System.Data.Entity.DbSet<BM4.Models.UserEvent> UserEvents { get; set; }
                                          
        public System.Data.Entity.DbSet<BM4.Models.UserProfile> UserProfiles { get; set; }
        public System.Data.Entity.DbSet<BM4.Models.UserConnections> UserConnections { get; set; }
        public System.Data.Entity.DbSet<BM4.Models.FriendConnection> FriendConnections { get; set; }
    }
}