using Inisra_Web_App_MVC.DAL;
using Inisra_Web_App_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Inisra_Web_App_MVC.Controllers
{
    public class ProfileController : Controller
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

        // GET: Profile
        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("JobSeeker"))
                return RedirectToAction("Details");
            if (User.IsInRole("Company"))
                return RedirectToAction("CompanyDetails");
            return View();
        }

        // GET: Profile/Details
        [Authorize(Roles ="JobSeeker")]
        public async Task<ActionResult> Details()
        {
            var jobSeekerUser = (JobSeekerUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            JobSeeker jobSeeker = await db.JobSeekers.FindAsync(jobSeekerUser.JobSeekerID);
            //just in case but almost never happens
            if (jobSeeker == null)
            {
                return HttpNotFound();
            }
            return View(jobSeeker);
        }

        // GET: Profile/CompanyDetails
        [Authorize(Roles ="Company")]
        public async Task<ActionResult> CompanyDetails()
        {
            var compnayUser = (CompanyUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            Company company = await db.Companies.FindAsync(compnayUser.CompanyID);
            //just in case but almost never happens
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Profile/Edit
        [Authorize(Roles ="JobSeeker")]
        public async Task<ActionResult> Edit()
        {
            var jobSeekerUser = (JobSeekerUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            JobSeeker jobSeeker = await db.JobSeekers.FindAsync(jobSeekerUser.JobSeekerID);
            //just in case but almost never happens
            if (jobSeeker == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name", jobSeeker.LocationID);
            return View(jobSeeker);
        }

        // POST: Profile/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JobSeeker")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,FirstName,LastName,Email,PhoneNo,isFemale,Birthday,LocationID")] JobSeeker jobSeeker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobSeeker).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name", jobSeeker.LocationID);
            return View(jobSeeker);
        }

        // GET: Profile/EditCompany
        [Authorize(Roles = "Company")]
        public async Task<ActionResult> EditCompany()
        {
            var companyUser = (CompanyUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            Company company = await db.Companies.FindAsync(companyUser.CompanyID);
            //just in case but almost never happens
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Profile/EditCompany
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Company")]
        public async Task<ActionResult> EditCompany([Bind(Include = "ID,Name,Email,PhoneNo,Description")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        /* todo: delete methods not finished. User manager involvement required and also deleting a user policy not clear
                
        /*
        // GET: Profile/Delete
        [Authorize(Roles ="JobSeeker")]
        public async Task<ActionResult> Delete()
        {
            var jobSeekerUser = (JobSeekerUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            JobSeeker jobSeeker = await db.JobSeekers.FindAsync(jobSeekerUser.JobSeekerID);
            //just in case but almost never happens
            if (jobSeeker == null)
            {
                return HttpNotFound();
            }
            return View(jobSeeker);
        }

        // POST: Profile/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            JobSeeker jobSeeker = await db.JobSeekers.FindAsync(id);
            db.JobSeekers.Remove(jobSeeker);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Profile/DeleteCompany
        [Authorize(Roles ="Company")]
        public async Task<ActionResult> DeleteCompany()
        {
            var companyUser = (CompanyUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            Company company = await db.Companies.FindAsync(companyUser.CompanyID);
            //just in case but almost never happens
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Profile/DeleteCompany
        [HttpPost, ActionName("DeleteCompany")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCompanyConfirmed(int id)
        {
            Company company = await db.Companies.FindAsync(id);
            db.Companies.Remove(company);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        */
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