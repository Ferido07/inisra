using Inisra_Web_App_MVC.BLL;
using Inisra_Web_App_MVC.DTOs;
using Inisra_Web_App_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Inisra_Web_App_MVC.Controllers
{
    [Authorize(Roles = "Company")]
    public class CompanyProfileController : Controller
    {
        private CompanyBLL bll = new CompanyBLL();
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
            var companyUser = (CompanyUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            Company company = await bll.GetCompanyByIdAsync((int)companyUser.CompanyID);
            //just in case but almost never happens
            if (company == null)
            {
                return HttpNotFound();
            }
            var dto = AutoMapper.Mapper.Map<Company, CompanyDto>(company);
            return View(dto);
        }

        // GET: CompanyProfile/Edit
        public async Task<ActionResult> Edit()
        {
            var companyUser = (CompanyUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            Company company = await bll.GetCompanyByIdAsync((int)companyUser.CompanyID);
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
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost()
        {
            var companyUser = (CompanyUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            var companyToUpdate = await bll.GetCompanyByIdAsync((int)companyUser.CompanyID);
            if(TryUpdateModel(companyToUpdate,"",new string[] { "Name","Email","PhoneNo","Description" }))
            {
                try
                {
                    await bll.UpdateCompanyAsync(companyToUpdate);
                }
                catch (Exception) { }
            } 
            return View(companyToUpdate);
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

        //GET: CompanyProfile/Jobs?title=Manager
        public async Task<ActionResult> Jobs(string title)
        {
            var companyUser = (CompanyUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            var jobDtos = bll.GetCompanyJobs((int)companyUser.CompanyID, title);
            return View(jobDtos);
        }

        //GET: CompanyProfile/Applications?jobID=5
        public async Task<ActionResult> Applications(int? jobID)
        {
            var companyUser = (CompanyUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            var applicationDtos = bll.GetApplications(companyUser.CompanyID.Value, jobID);
            return View(applicationDtos);
        }

        //GET: CompanyProfile/Invite
        public async Task<ActionResult> Invite(int? jobID, int? jobSeekerID, bool? cancel)
        {
            //abort inviting hence clear data   
            if (cancel == true)
            {
                Session["JobID"] = Session["JobSeekerID"] = null;
                return RedirectToAction("Invitations");
            }
            //if jobID or jobSeekerID is not null save it in session or update the existing one in session(i.e update when both have value)
            if (jobID != null)
                Session["JobID"] = jobID;
            if (jobSeekerID != null)
                Session["JobSeekerID"] = jobSeekerID;

            //when both are null       
            if ((jobID == null) && (Session["JobID"] == null))
            {
                return RedirectToAction("Jobs");
            }
            //same as above
            if ((jobSeekerID == null) && (Session["JobSeekerID"] == null))
            {
                return RedirectToAction("Index", "JobSeekers");
            }

            var companyUser = (CompanyUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            
            //assigning stored value from previous request with jobID or jobSeekerID or the same request
            //so that these variables can be used intead of session object 
            //can be removed but the invitaion object must be initialized with the data from session if removed
            //and the queries must also be changed to corresponding session object
            jobID = (int)Session["JobID"];
            jobSeekerID = (int)Session["JobSeekerID"];

            //checking if the person is already invited to the job
            Invitation invitation = await bll.GetInvitationAsync(jobID.Value, jobSeekerID.Value);
            if (invitation == null)
            {
                var job = await bll.GetJob(companyUser.CompanyID.Value, jobID.Value);
                JobSeeker jobSeeker = null;

                using (JobSeekerBLL jsbll = new BLL.JobSeekerBLL())
                    jobSeeker = await jsbll.GetJobSeekerById(jobSeekerID.Value);

                //if job or jobseeker does not exist
                if (job == null || jobSeeker == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    invitation = new Invitation()
                    {
                        JobID = jobID.Value,
                        JobSeekerID = jobSeekerID.Value,
                        Job = job,
                        JobSeeker = jobSeeker
                    };
                    return View(invitation);
                }
            }
            else
            {
                //todo tell company user that the job seeeker is already invited to the job
                //clear session data since already invited
                Session["JobID"] = Session["JobSeekerID"] = null;
                return RedirectToAction("Invitations");
            }        
        }

        //POST: CompanyProfile/Invite
        [HttpPost, ActionName("Invite")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InviteConfirmed()
        {
            //means already invited maybe through another tab or sometime before and since the invitation exists 
            //the session data cleared or some server error happened
            if ((Session["JobID"] == null) || (Session["JobSeekerID"] == null))
            {
                return RedirectToAction("Invitations");
                //return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
             
            int companyId = ((CompanyUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()))).CompanyID.Value;
            int jobID = (int)Session["JobID"];
            int jobSeekerID = (int)Session["JobSeekerID"];

            bll.Invite(companyId, jobID, jobSeekerID);

            //clear the data since succesfully added invitation
            Session["JobID"] = Session["JobSeekerID"] = null;

            return RedirectToAction("Invitations");
        }

        //GET: CompanyProfile/Invitations
        public async Task<ActionResult> Invitations()
        {
            var companyUser = (CompanyUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            var invitationDtos = bll.GetCompanyInvitations((int)companyUser.CompanyID);
            return View(invitationDtos);
        }

        //POST: CompanyProfile/Invitations/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteInvitation(int jobID, int jobSeekerID)
        {
            var companyUser = (CompanyUser)(await UserManager.FindByIdAsync(User.Identity.GetUserId()));
            await bll.DeleteInivitationAsync(companyUser.CompanyID.Value, jobID, jobSeekerID);
            return RedirectToAction("Invitations");
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
