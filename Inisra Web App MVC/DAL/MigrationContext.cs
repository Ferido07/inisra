using Inisra_Web_App_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Inisra_Web_App_MVC.DAL
{
    public class MigrationContext : DbContext 
    {
        public MigrationContext()
            : base("InisraContext")
        {

        }
        public DbSet<JobSeeker> JobSeekers { set; get; }
        public DbSet<Company> Companies { set; get; }
        public DbSet<Job> Jobs { set; get; }
        public DbSet<Application> Applications { set; get; }
        public DbSet<Invitation> Invitations { set; get; }
        public DbSet<Skill> Skills { set; get; }
        public DbSet<Certificate> Certificates { set; get; }

        public DbSet<InisraUser> InisraUsers { set; get; }
    }
}