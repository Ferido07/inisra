using Inisra_Web_App_MVC.DAL;
using Inisra_Web_App_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

            var context = new InisraContext();
            var IdentityResult = new IdentityResult();

            var InisraRoleManager = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));

            if (!InisraRoleManager.RoleExists("Admin"))
            {
                IdentityResult = InisraRoleManager.Create(new IdentityRole("Admin"));
            }

            if (!InisraRoleManager.RoleExists("JobSeeker"))
            {
                InisraRoleManager.Create(new IdentityRole("JobSeeker"));
            }

            if (!InisraRoleManager.RoleExists("Company"))
            {
                InisraRoleManager.Create(new IdentityRole("Company"));
            }

            var InisraUserManager = new InisraUserManager(new UserStore<InisraUser>(context));
            var Admin = new Administrator {
                FirstName = "Ferid",
                LastName = "Zuber",
                UserName = "Ferido07",
                Email = "feridyz@gmail.com"
            };

            var Result = InisraUserManager.Create(Admin, "Admin@123");

            if(InisraRoleManager.RoleExists("Admin") && Result.Succeeded)
            {
                InisraUserManager.AddToRole(InisraUserManager.FindByName("Ferido07").Id, "Admin");
            }

            AutoMapperConfig.Configure(); 

        }
    }
}
