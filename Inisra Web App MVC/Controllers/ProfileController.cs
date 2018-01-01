﻿using Inisra_Web_App_MVC.DAL;
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
    [Authorize(Roles ="JobSeeker")]
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
        public ActionResult Index()
        {
            return View();
        }

        // GET: Profile/Details
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



        // GET: Profile/Edit
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

       

        /* todo: delete methods not finished. User manager involvement required and also deleting a user policy not clear
                
        /*
        // GET: Profile/Delete
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