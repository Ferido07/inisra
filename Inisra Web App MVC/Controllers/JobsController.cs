using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inisra_Web_App_MVC.DAL;
using Inisra_Web_App_MVC.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace Inisra_Web_App_MVC.Controllers
{
    public class JobsController : Controller
    {
        private InisraContext db = new InisraContext();
        private InisraUserManager _userManager;

        public InisraUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<InisraUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Jobs
        public async Task<ActionResult> Index(string title, string profession)
        {
            var jobs = from j in db.Jobs select j;

            if (!String.IsNullOrEmpty(title))
            {
                // When you call the Contains  method on an IEnumerable  collection, you get the .NET Framework implementation;
                // when you call it on an IQueryable  object, you get the database provider implementation.
                // contains is converted to like on the database which is case insensitive for the entity framework 
                // provider implementation
                jobs = jobs.Where(j => j.Title.Contains(title));
            }
            if (!String.IsNullOrEmpty(profession))
                jobs = jobs.Where(j => j.Profession.Contains(profession));
            //jobs = db.Jobs.Include(j => j.Company);
            jobs.Include(j => j.Company);
            return View(await jobs.ToListAsync());
        }

        // GET: Jobs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Post
        [Authorize(Roles = "Company")]
        public ActionResult Post()
        {
            var companyUser = (CompanyUser)(UserManager.FindByName(User.Identity.Name));
            var companyID = (int)companyUser.CompanyID;
            var job = new Job {
                CompanyID = companyID,
                Company = db.Companies.Single(c => c.ID == companyID)
            };
            return View(job);
        }

        // POST: Jobs/Post
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Company")]
        public async Task<ActionResult> Post([Bind(Include = "ID,CompanyID,Title,isOpen,isInvitationOnly,Location,PostDate,ApplicationDeadlineDate,Description")] Job job)
        {
            if (ModelState.IsValid)
            {
                //check if the location already Exists
                try {
                    var location = db.Locations.Single(l => l.Name == job.Location.Name);
                    if (location != null)
                    {
                        job.LocationID = location.ID;
                        job.Location = location;
                    }         
                }
                catch(InvalidOperationException IOE)
                {
                    //if the inverse of the location specified doesnt exist 
                    //i.e some other error
                    //else it would just go out and save the new location
                    if (!IOE.Message.Equals("Sequence contains no elements"))
                    {
                        ModelState.AddModelError("", IOE.Message);
                        return View(job);
                    }
                    db.Locations.Add(job.Location);
                }
                db.Jobs.Add(job);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(job);
        }

        // GET: Jobs/Edit/5
        [Authorize(Roles = "Company")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            var companyUser = (CompanyUser)(UserManager.FindByName(User.Identity.Name));
            if (job.CompanyID != companyUser.CompanyID)
            {
                //todo add needed error report that the job is not his to edit 
                return RedirectToAction("Index");
            }
            
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Company")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,CompanyID,Title,isOpen,isInvitationOnly,Location,PostDate,ApplicationDeadlineDate,Description")] Job job)
        {
            if (ModelState.IsValid)
            {
                //check if the location already Exists
                try
                {
                    var location = db.Locations.Single(l => l.Name == job.Location.Name);
                    if (location != null)
                    {
                        job.LocationID = location.ID;
                        job.Location = location;
                    }
                }
                catch (InvalidOperationException IOE)
                {
                    //if the inverse of the location specified doesnt exist 
                    //i.e some other error
                    //else it would just go out and save the new location
                    if (!IOE.Message.Equals("Sequence contains no elements"))
                    {
                        ModelState.AddModelError("", IOE.Message);
                        return View(job);
                    }
                    db.Locations.Add(job.Location);
                }
                db.Entry(job).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
           
            return View(job);
        }

        // GET: Jobs/Delete/5
        [Authorize(Roles = "Company")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            var companyUser = (CompanyUser)(UserManager.FindByName(User.Identity.Name));
            if (job.CompanyID != companyUser.CompanyID)
            {
                //todo add needed error report that the job is not his to edit 
                return RedirectToAction("Index");
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Company")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Job job = await db.Jobs.FindAsync(id);
            db.Jobs.Remove(job);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Jobs/Apply/5
        [Authorize(Roles = "JobSeeker")]
        public async Task<ActionResult> Apply(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
            //todo any qualifcation checks(like is the job invitationonly
            //does the user have enough experience for application set by the company, etc.)
            //needed for a user to apply should be done here
            
        }

        // POST: Jobs/Apply/5
        [HttpPost, ActionName("Apply")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="JobSeeker")]
        public async Task<ActionResult> ApplyConfirmed(int id)
        {
            Job job = await db.Jobs.FindAsync(id);
            var jobSeekerUser = (JobSeekerUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            var application = new Application()
            {
                JobID = job.ID,
                JobSeekerID = (int)jobSeekerUser.JobSeekerID
            };
            db.Applications.Add(application);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
