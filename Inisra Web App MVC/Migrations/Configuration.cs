using System.Data.Entity.Migrations;

namespace Inisra_Web_App_MVC.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DAL.InisraContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.InisraContext context)
        {
            /*All the following code is removed because it doesnt work after the first time set up. 
             * Always adds the new jobseekers because they doent have IDs assigned yet and then the jobSeekerUsers
             * fail to save since the the jobseekers are not obtained from the database and probably the data may
             * not exist.
             * Error: "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details."
             *  
            //hash of password = Ferid@123
            string passwordHash = "ABuYjU9Mw0+Lu7BW+ie8hiIAoLOTSpkhAQKtloA+Ty53ZyVMjn5QP0dfz8QuIb+pTg==";
            var JobSeekers = new List<JobSeeker>
            {
                new JobSeeker {
                    FirstName = "Ferid",
                    LastName = "Zuber",
                    Email = "feridyz@gmail.com",
                    IsFemale = false,
                    PhoneNo = 0910636418,
                    Birthday = new DateTime(1994,12,7)
                },
                new JobSeeker {
                    FirstName ="Abenezer",
                    LastName = "Genanaw",
                    Email = "abenezergenanaw@gmail.com",
                    IsFemale = false,
                    PhoneNo = 0910116107
                },
                new JobSeeker
                {
                    FirstName = "Fisseha",
                    LastName = "Araya",
                    Email = "Fisseharaya@gmail.com",
                    IsFemale = false,
                    PhoneNo = 0920953186
                },
                new JobSeeker
                {
                    FirstName = "Fesseha",
                    LastName = "Tilahun",
                    Email = "fishtilahun73@gmail.com",
                    IsFemale = false,
                    PhoneNo = 0913206083,
                    Birthday = new DateTime(1994,12,27)
                },
                new JobSeeker
                {
                    FirstName = "Alazar",
                    LastName = "Tibebu",
                    Email = "tibebualazar@gmail.com",
                    PhoneNo = 0910493825,
                    IsFemale = false
                }

            };
            JobSeekers.ForEach(js => context.JobSeekers.AddOrUpdate(property => property.ID, js));
            context.SaveChanges();

            var JobSeekerUsers = new List<JobSeekerUser>
            {
                new JobSeekerUser
                {
                    Email = "feridyz@gmail.com",
                    UserName = "feridyz@gmail.com",
                    PasswordHash = passwordHash,
                    JobSeeker = JobSeekers.ElementAtOrDefault(0)
                },
                new JobSeekerUser
                {
                    Email = "abenezergenanaw@gmail.com",
                    UserName = "abenezergenanaw@gmail.com",
                    PasswordHash = passwordHash,
                    JobSeeker = JobSeekers.ElementAtOrDefault(1)
                },
                new JobSeekerUser
                {
                    Email = "Fisseharaya@gmail.com",
                    UserName = "Fisseharaya@gmail.com",
                    PasswordHash = passwordHash,
                    JobSeeker = JobSeekers.ElementAtOrDefault(2)
                },
                new JobSeekerUser
                {
                    Email = "fishtilahun73@gmail.com",
                    UserName = "fishtilahun73@gmail.com",
                    PasswordHash = passwordHash,
                    JobSeeker = JobSeekers.ElementAtOrDefault(3)
                },
                new JobSeekerUser
                {
                    Email = "tibebualazar@gmail.com",
                    UserName = "tibebualazar@gmail.com",
                    PasswordHash = passwordHash,
                    JobSeeker = JobSeekers.ElementAtOrDefault(4)
                }
            };

            JobSeekerUsers.ForEach(jsu => context.Users.AddOrUpdate(js => js.Id, jsu));
            context.SaveChanges();
            /*  InisraUserManager UserManager = new InisraUserManager.c;
              var result = UserManager.Create(JobSeekerUsers.ElementAtOrDefault<JobSeekerUser>(0), "");
              var result = UserManager.Create(JobSeekerUsers.ElementAtOrDefault<JobSeekerUser>(0), "");
              var result = UserManager.CreateAsync(JobSeekerUsers.ElementAtOrDefault<JobSeekerUser>(0), "");
              

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
            Companies.ForEach(com => context.Companies.AddOrUpdate(c => c.ID, com));
            context.SaveChanges();

            var CompanyUsers = new List<CompanyUser>
            {
                new CompanyUser
                {
                    Email = "inisra@gmail.com",
                    UserName = "inisra@gmail.com",
                    PasswordHash = passwordHash,
                    Company = Companies.ElementAtOrDefault(0)
                },
                new CompanyUser
                {
                    Email = "PASAddis@state.gov",
                    UserName = "PASAddis@state.gov",
                    PasswordHash = passwordHash,
                    Company = Companies.ElementAtOrDefault(1)
                },
                new CompanyUser
                {
                    Email = "wayne@enterprise.com",
                    UserName = "wayne@enterprise.com",
                    PasswordHash = passwordHash,
                    Company = Companies.ElementAtOrDefault(2)
                }
            };
            

            CompanyUsers.ForEach(cu => context.Users.AddOrUpdate(c => c.Id, cu));
            context.SaveChanges();
            */
    
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
