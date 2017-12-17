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

       
    }
}