namespace Inisra_Web_App_MVC.Migrations
{
    using DAL;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Inisra_Web_App_MVC.DAL.InisraContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Inisra_Web_App_MVC.DAL.InisraContext context)
        {
            //hash of password = Ferid@123
            string passwordHash = "ABuYjU9Mw0+Lu7BW+ie8hiIAoLOTSpkhAQKtloA+Ty53ZyVMjn5QP0dfz8QuIb+pTg==";
            var JobSeekers = new List<JobSeeker>
            {
                new JobSeeker {
                    FirstName = "Ferid",
                    LastName = "Zuber",
                    Email = "feridyz@gmail.com",
                    isFemale = false,
                    PhoneNo = 0910636418,
                    Birthday = new DateTime(1994,12,7)
                },
                new JobSeeker {
                    FirstName ="Abenezer",
                    LastName = "Genanaw",
                    Email = "abenezergenanaw@gmail.com",
                    isFemale = false,
                    PhoneNo = 0910116107
                },
                new JobSeeker
                {
                    FirstName = "Fisseha",
                    LastName = "Araya",
                    Email = "Fisseharaya@gmail.com",
                    isFemale = false,
                    PhoneNo = 0920953186
                },
                new JobSeeker
                {
                    FirstName = "Fesseha",
                    LastName = "Tilahun",
                    Email = "fishtilahun73@gmail.com",
                    isFemale = false,
                    PhoneNo = 0913206083,
                    Birthday = new DateTime(1994,12,27)
                },
                new JobSeeker
                {
                    FirstName = "Alazar",
                    LastName = "Tibebu",
                    Email = "tibebualazar@gmail.com",
                    PhoneNo = 0910493825,
                    isFemale = false 
                }

            };
            JobSeekers.ForEach(js => context.JobSeekers.AddOrUpdate(property => property.Email, js));
            context.SaveChanges();

            var JobSeekerUsers = new List<JobSeekerUser>
            {
                new JobSeekerUser
                {
                    Email = "feridyz@gmail.com",
                    UserName = "feridyz@gmail.com",
                    PasswordHash = passwordHash,
                    JobSeeker = JobSeekers.ElementAtOrDefault<JobSeeker>(0)
                },
                new JobSeekerUser
                {
                    Email = "abenezergenanaw@gmail.com",
                    UserName = "abenezergenanaw@gmail.com",
                    PasswordHash = passwordHash,
                    JobSeeker = JobSeekers.ElementAtOrDefault<JobSeeker>(1)
                },
                new JobSeekerUser
                {
                    Email = "Fisseharaya@gmail.com",
                    UserName = "Fisseharaya@gmail.com",
                    PasswordHash = passwordHash,
                    JobSeeker = JobSeekers.ElementAtOrDefault<JobSeeker>(2)
                },
                new JobSeekerUser
                {
                    Email = "fishtilahun73@gmail.com",
                    UserName = "fishtilahun73@gmail.com",
                    PasswordHash = passwordHash,
                    JobSeeker = JobSeekers.ElementAtOrDefault<JobSeeker>(3)
                },
                new JobSeekerUser
                {
                    Email = "tibebualazar@gmail.com",
                    UserName = "tibebualazar@gmail.com",
                    PasswordHash = passwordHash,
                    JobSeeker = JobSeekers.ElementAtOrDefault<JobSeeker>(4)
                }
            };

            JobSeekerUsers.ForEach(jsu => context.Users.AddOrUpdate(js => js.Email, jsu));
            context.SaveChanges();
            /*  ApplicationUserManager UserManager = new ApplicationUserManager.c;
              var result = UserManager.Create(JobSeekerUsers.ElementAtOrDefault<JobSeekerUser>(0), "");
              var result = UserManager.Create(JobSeekerUsers.ElementAtOrDefault<JobSeekerUser>(0), "");
              var result = UserManager.CreateAsync(JobSeekerUsers.ElementAtOrDefault<JobSeekerUser>(0), "");
              */

            var Companies = new List<Company>
            {
                new Company
                {
                    Name = "Inisra",
                    Email = "inisra@gmail.com",
                    PhoneNo = 0910636418,
                    Description = "A tech company facilitating job search and meetup."
                },
                new Company
                {
                    Name = "US Emabassy in Ethiopia",
                    Email = "PASAddis@state.gov",
                    Description = "Embassy of the United States of America in Ehiopia."
                }, 
                new Company
                {
                    Name = "Wayne Enterprises",
                    Email = "wayne@enterprise.com",
                    Description = "A fictious company built by the Wayne's residing in Gotham City." 
                }
            };
            Companies.ForEach(com => context.Companies.AddOrUpdate(c => c.Email, com));
            context.SaveChanges();

            var CompanyUsers = new List<CompanyUser>
            {
                new CompanyUser
                {
                    Email = "inisra@gmail.com",
                    UserName = "inisra@gmail.com",
                    PasswordHash = passwordHash,
                    Company = Companies.ElementAtOrDefault<Company>(0)
                },
                new CompanyUser
                {
                    Email = "PASAddis@state.gov",
                    UserName = "PASAddis@state.gov",
                    PasswordHash = passwordHash,
                    Company = Companies.ElementAtOrDefault<Company>(1)
                },
                new CompanyUser
                {
                    Email = "wayne@enterprise.com",
                    UserName = "wayne@enterprise.com",
                    PasswordHash = passwordHash,
                    Company = Companies.ElementAtOrDefault<Company>(2)
                }
            };

            CompanyUsers.ForEach(cu => context.Users.AddOrUpdate(c => c.Email, cu));
            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
