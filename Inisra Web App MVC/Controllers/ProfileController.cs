using Inisra_Web_App_MVC.BLL;
using Inisra_Web_App_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Inisra_Web_App_MVC.Controllers
{
    [Authorize(Roles ="JobSeeker")]
    public class ProfileController : Controller
    {
        private JobSeekerBLL bll = new JobSeekerBLL();
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
            JobSeeker jobSeeker = await bll.GetJobSeekerById(jobSeekerUser.JobSeekerID.Value);
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
            JobSeeker jobSeeker = await bll.GetJobSeekerById(jobSeekerUser.JobSeekerID.Value);
            //just in case but almost never happens
            if (jobSeeker == null)
            {
                return HttpNotFound();
            }
            //todo remove and replace the dropdown for location with textbox
            //ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name", jobSeeker.LocationID);
            return View(jobSeeker);
        }

        // POST: Profile/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,FirstName,LastName,Email,PhoneNo,IsFemale,Birthday")] JobSeeker jobSeeker)
        {
            if (ModelState.IsValid)
            {
                await bll.UpdateJobSeeker(jobSeeker);
                return RedirectToAction("Index");
            }
            return View(jobSeeker);
        }

        public async Task<ActionResult> Resumes()
        {
            var jobSeekerUser = (JobSeekerUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            JobSeeker jobSeeker = await bll.GetJobSeekerById(jobSeekerUser.JobSeekerID.Value);
            //ViewBag.ResumeCount = jobSeeker.CVs;
            return View(jobSeeker.CVs);
        }

        [HttpPost]
        public async Task<ActionResult> UploadCV(HttpPostedFileBase file)
        { 
            if (file != null && file.ContentLength > 0)
            {
                var jobSeekerUser = (JobSeekerUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
                var fileName = Path.GetFileName(file.FileName);
                var extensions = fileName.Split('.');
                var extension = extensions[extensions.Length - 1].ToLower();
                if (extension.ToLower().Equals("docx"))
                {
                    MemoryStream ms = new MemoryStream();
                    file.InputStream.CopyTo(ms);
                    CV cv = new CV { DocumentName = fileName, DocumentType = DocumentType.docx, Document = ms.ToArray() };
                    await bll.AddResume(jobSeekerUser.JobSeekerID.Value, cv);
                }
                else
                {
                    ViewBag.Message = "Select a valid file with 'docx' extension.";
                    return View("Resumes");
                }
            }
            return RedirectToAction("Details");
        }

        public async Task<FileContentResult> DownloadCV(int id)
        {
            var jobSeekerUser = (JobSeekerUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            var cvs = await bll.GetCVs(jobSeekerUser.JobSeekerID.Value);
            
            CV cv = cvs.Find(c => c.ID == id);
            Response.AppendHeader("content-disposition", "attachment;filename="+ cv.DocumentName );
            return new FileContentResult(cv.Document, "text/plain");
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
        // GET: Profile/Applications
        public async Task<ActionResult> Applications()
        {
            var jobSeekerUser = (JobSeekerUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            var applications = await bll.GetApplications(jobSeekerUser.JobSeekerID.Value);
            return View(applications);
        }

        //POST: Profile/DeleteApplication/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteApplication (int jobID)
        {
            var jobSeekerUser = (JobSeekerUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            await bll.DeleteApplication(jobSeekerUser.JobSeekerID.Value, jobID);
            return RedirectToAction("Applications");
        }

        //GET: Profile/Invitations
        public async Task<ActionResult> Invitations()
        {
            var jobSeekerUser = (JobSeekerUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            var invitations = await bll.GetInvitaions(jobSeekerUser.JobSeekerID.Value);
            return View(invitations);
        }


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