using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inisra_Web_App_MVC.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Inisra_Web_App_MVC.BLL;
using Inisra_Web_App_MVC.DTOs;
using System.IO;

namespace Inisra_Web_App_MVC.Controllers
{
    public class JobsController : Controller
    {
        private JobBLL bll = new JobBLL();
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
            var jobs = await bll.SearchJobs(title, profession, "", "", null);
            return View(jobs);
        }

        // GET: Jobs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await bll.GetJobById(id.Value);
            if (job == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("Company"))
            {
                var companyUser=(CompanyUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
                ViewBag.IsOwner = (job.CompanyID == companyUser.CompanyID) ? true : false;
            }
            var dto = AutoMapper.Mapper.Map<Job, JobDto>(job);
            return View(dto);
        }

        // GET: Jobs/Post
        [Authorize(Roles = "Company")]
        public async Task<ActionResult> Post()
        {
            var companyUser = (CompanyUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            var companyID = (int)companyUser.CompanyID;
            var job = new Job {
                CompanyID = companyID,
                
            };
            using (CompanyBLL combll = new BLL.CompanyBLL())
                job.Company = await combll.GetCompanyByIdAsync(companyUser.CompanyID.Value);

            return View(job);
        }

        // POST: Jobs/Post
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Company")]
        public async Task<ActionResult> Post([Bind(Include = "CompanyID,Title,Profession,Salary,SalaryRate,IsOpen,IsInvitationOnly,Location,ApplicationDeadlineDate,Description")] Job job, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var extensions = fileName.Split('.');
                    var extension = extensions[extensions.Length - 1].ToLower();
                    if (extension.ToLower().Equals("docx"))
                    {
                        MemoryStream ms = new MemoryStream();
                        file.InputStream.CopyTo(ms);
                        JobDescription JD = new JobDescription { DocumentName = fileName, DocumentType = DocumentType.docx, Document = ms.ToArray() };
                        job.JobDescriptionDocument = JD;
                    }
                    else
                    {
                        ViewBag.Message = "Select a valid file with 'docx' extension.";
                        return View(job);
                    }
                }
                await bll.AddJob(job);
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
            Job job = await bll.GetJobById(id.Value);
            if (job == null)
            {
                return HttpNotFound();
            }
            var companyUser = (CompanyUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
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
        public async Task<ActionResult> Edit([Bind(Include = "ID,CompanyID,Title,Profession,Salary,SalaryRate,IsOpen,IsInvitationOnly,Location,ApplicationDeadlineDate,Description")] Job job, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var extensions = fileName.Split('.');
                    var extension = extensions[extensions.Length - 1].ToLower();
                    if (extension.ToLower().Equals("docx"))
                    {
                        MemoryStream ms = new MemoryStream();
                        file.InputStream.CopyTo(ms);
                        JobDescription JD = new JobDescription { JobID = job.ID, Job=job, DocumentName = fileName, DocumentType = DocumentType.docx, Document = ms.ToArray() };
                        job.JobDescriptionDocument = JD;
                    }
                    else
                    {
                        ViewBag.Message = "Select a valid file with 'docx' extension.";
                        return View(job);
                    }
                }
                await bll.UpdateJob(job);
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
            Job job = await bll.GetJobById(id.Value);
            if (job == null)
            {
                return HttpNotFound();
            }
            var companyUser = (CompanyUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            if (job.CompanyID != companyUser.CompanyID)
            {
                //todo add needed error report that the job is not his to delete 
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
            Job job = await bll.GetJobById(id);
            await bll.DeleteJob(job);
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
            Job job = await bll.GetJobById(id.Value);
            if (job == null)
            {
                return HttpNotFound();
            }

            //check if the user has already applied
            var jobSeekerUser = (JobSeekerUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            using (JobSeekerBLL jsbll = new BLL.JobSeekerBLL())
            {
                var application = await jsbll.GetApplication(jobSeekerUser.JobSeekerID.Value, job.ID);
                if (application == null)
                {
                    return View(job);
                }
            }
            //todo how to tell the user that he has already applied.
            return RedirectToAction("Index");
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
            var jobSeekerUser = (JobSeekerUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            using (JobSeekerBLL jsbll = new JobSeekerBLL())
            {
                var application = await jsbll.GetApplication(jobSeekerUser.JobSeekerID.Value, id);
                if (application == null)
                {
                    await jsbll.Apply(jobSeekerUser.JobSeekerID.Value, id);
                }
            }        
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "JobSeeker")]
        public async Task<ActionResult> BookmarkJob(int jobId)
        {
            var jobSeekerUser = (JobSeekerUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            using (JobSeekerBLL jsbll = new JobSeekerBLL())
            {
                await jsbll.BookmarkJob(jobSeekerUser.JobSeekerID.Value, jobId);
                return RedirectToAction("Index");
            }
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
