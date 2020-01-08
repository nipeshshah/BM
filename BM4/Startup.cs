using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BM4.Startup))]
namespace BM4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
