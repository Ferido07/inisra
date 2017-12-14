using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inisra_Web_App_MVC.Models
{
    // You can add profile data for the user by adding more properties to your InisraUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class InisraUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<InisraUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class InisraIdentityContext : IdentityDbContext<InisraUser>
    {
        public InisraIdentityContext()
            : base("InisraContext", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JobSeekerUser>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("JobSeekerUsers");
            });
            modelBuilder.Entity<CompanyUser>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("CompanyUsers");
            });
            modelBuilder.Entity<Administrator>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Administrators");
            });
        }

        public static InisraIdentityContext Create()
        {
            return new InisraIdentityContext();
        }
    }

    public class JobSeekerUser : InisraUser
    {
        //[Key]
        //[ForeignKey("JobSeeker")] removed ecause default values used
        public int? JobSeekerID { get; set; }
        public virtual JobSeeker JobSeeker { get; set; }

    }

    public class CompanyUser : InisraUser
    {
        //[ForeignKey("Company")]
        public int? CompanyID { get; set; }
        public virtual Company Company { get; set; }
    }

    public class Administrator : InisraUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}