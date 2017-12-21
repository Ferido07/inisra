using Inisra_Web_App_MVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Inisra_Web_App_MVC.DAL
{
    public class InisraContext : IdentityDbContext<InisraUser>
    {
        public InisraContext()
            : base("InisraContext", throwIfV1Schema: false)
        {
        }

        public static InisraContext Create()
        {
            return new InisraContext();
        }


        public DbSet<JobSeeker> JobSeekers { set; get; }
        public DbSet<Company> Companies { set; get; }
        public DbSet<Job> Jobs { set; get; }
        public DbSet<Application> Applications { set; get; }
        public DbSet<Invitation> Invitations { set; get; }
        public DbSet<Skill> Skills { set; get; }
        public DbSet<Certificate> Certificates { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           // modelBuilder.Entity<IdentityUser>().ToTable("InisraUsers");
            modelBuilder.Entity<InisraUser>().ToTable("InisraUsers", "dbo");
            modelBuilder.Entity<IdentityRole>().ToTable("InisraRoles", "dbo");
            modelBuilder.Entity<IdentityUserRole>().ToTable("InisraUserRoles", "dbo");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("InisraUserLogins", "dbo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("InisraUserClaims", "dbo");

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

            modelBuilder.Entity<Application>().HasKey(k=>new { k.JobSeekerID, k.JobID });
            modelBuilder.Entity<Invitation>().HasKey(k => new { k.JobID, k.JobSeekerID });

            modelBuilder.Entity<JobSeeker>().HasMany(s => s.Skills).WithMany(js => js.JobSeekers)
                .Map(left => left.MapLeftKey("JobSeekerID").MapRightKey("SkillID")
                .ToTable("JobSeekerSkills","dbo"));

            modelBuilder.Entity<Company>().HasMany(l => l.Locations).WithMany(c => c.Companies)
                .Map(left => left.MapLeftKey("CompanyID").MapRightKey("LocationID")
                .ToTable("CompanyLocations", "dbo"));

            modelBuilder.Entity<Institution>().HasMany(l => l.Locations).WithMany(i => i.Institutions)
                .Map(left => left.MapLeftKey("InstitutionID").MapRightKey("LocationID")
                .ToTable("InstitutionLocations", "dbo"));
        }

       
    }
}