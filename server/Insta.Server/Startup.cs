using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Insta.Server.Startup))]
namespace Insta.Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
