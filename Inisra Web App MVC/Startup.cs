using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Inisra_Web_App_MVC.Startup))]
namespace Inisra_Web_App_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
