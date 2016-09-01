using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GTMDweixinManagement.Startup))]
namespace GTMDweixinManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
