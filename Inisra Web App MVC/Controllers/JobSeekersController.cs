using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Inisra_Web_App_MVC.Models;
using Inisra_Web_App_MVC.BLL;
using Inisra_Web_App_MVC.DTOs;

namespace Inisra_Web_App_MVC.Controllers
{
    public class JobSeekersController : Controller
    {

        private JobSeekerBLL bll = new JobSeekerBLL();
        // GET: JobSeekers
        public async Task<ActionResult> Index()
        {
            var jobSeekers = await bll.GetJobSeekers();
            return View(jobSeekers);
        }

        // GET: JobSeekers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var jobSeeker = await bll.GetJobSeekerById(id.Value);
            if (jobSeeker == null)
            {
                return HttpNotFound();
            }
            var dto = AutoMapper.Mapper.Map<JobSeeker, JobSeekerDto>(jobSeeker);
            return View(dto);
        }

        [Authorize(Roles ="Company")]  
        public async Task<FileContentResult> DownloadCV(int id, int jobSeekerId)
        {
            
            var cvs = await bll.GetCVs(jobSeekerId);
            CV cv = cvs.Find(c => c.ID == id);
            Response.AppendHeader("content-disposition", "attachment;filename=" + cv.DocumentName);
            return new FileContentResult(cv.Document, "text/plain");
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
        public async Task<ActionResult> Create([Bind(Include = "ID,FirstName,LastName,Email,PhoneNo,IsFemale,Birthday,LocationID")] JobSeeker jobSeeker)
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
                bll.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
