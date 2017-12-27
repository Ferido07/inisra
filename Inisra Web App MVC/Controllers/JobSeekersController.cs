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

namespace Inisra_Web_App_MVC.Controllers
{
    public class JobSeekersController : Controller
    {
        private InisraContext db = new InisraContext();

        // GET: JobSeekers
        public async Task<ActionResult> Index()
        {
            var jobSeekers = db.JobSeekers.Include(j => j.Location);
            return View(await jobSeekers.ToListAsync());
        }

        // GET: JobSeekers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobSeeker jobSeeker = await db.JobSeekers.FindAsync(id);
            if (jobSeeker == null)
            {
                return HttpNotFound();
            }
            return View(jobSeeker);
        }
        /*
        // GET: JobSeekers/Create
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name");
            return View();
        }

        // POST: JobSeekers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,FirstName,LastName,Email,PhoneNo,isFemale,Birthday,LocationID")] JobSeeker jobSeeker)
        {
            if (ModelState.IsValid)
            {
                db.JobSeekers.Add(jobSeeker);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name", jobSeeker.LocationID);
            return View(jobSeeker);
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
