using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XX.Web.Startup))]
namespace XX.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
