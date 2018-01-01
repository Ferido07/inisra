using Inisra_Web_App_MVC.DAL;
using Inisra_Web_App_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Inisra_Web_App_MVC.Controllers
{
    [Authorize(Roles ="Company")]
    public class CompanyProfileController : Controller
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

        // GET: CompanyProfile
        public ActionResult Index()
        {
            return View();
        }

        // GET: CompanyProfile/Details
        public async Task<ActionResult> Details()
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

        // GET: CompanyProfile/Edit
        public async Task<ActionResult> Edit()
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

        // POST: CompanyProfile/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Email,PhoneNo,Description")] Company company)
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
         * 
        // GET: CompanyProfile/Delete
        public async Task<ActionResult> Delete()
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

        // POST: CompanyProfile/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
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
 