using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Inisra_Web_App_MVC.Models;
using Inisra_Web_App_MVC.BLL;
using Inisra_Web_App_MVC.DTOs;

namespace Inisra_Web_App_MVC.Api
{
    [RoutePrefix("api/jobseekers/{jobSeekerId:int}")]
    public class JobSeekersController : ApiController
    {
        private JobSeekerBLL bll = new JobSeekerBLL();

        // GET: api/Profile
        [HttpGet]
        [Route("~/api/jobseekers")]
        public async Task<IEnumerable<JobSeekerDto>> GetJobSeekers()
        {
            return await bll.GetJobSeekers();
        }

        // GET: api/Profile/5
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(JobSeekerDto))]
        public async Task<IHttpActionResult> GetJobSeeker(int jobSeekerId)
        {
            JobSeeker jobSeeker = await bll.GetJobSeekerById(jobSeekerId);
            if (jobSeeker == null)
            {
                return NotFound();
            }
            var dto = AutoMapper.Mapper.Map<JobSeeker, JobSeekerDto>(jobSeeker);
            return Ok(dto);
        }

        [HttpGet]
        [Route("applications")]
        public async Task<IEnumerable<ApplicationDto>> GetApplications(int jobSeekerId)
        {
           return await bll.GetApplications(jobSeekerId);
        }

        [HttpGet]
        [Route("applications/{jobId:int}")]
        public async Task<ApplicationDto> GetApplication(int jobSeekerId, int jobId)
        {
            return await bll.GetApplication(jobSeekerId, jobId);   
        }

        [HttpPost]
        [Route("apply/{jobId:int}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Apply(int jobSeekerId, int jobId)
        {
            await bll.Apply(jobSeekerId, jobId);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("applications/{jobId:int}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> DeleteApplication(int jobSeekerId, int jobId)
        {
            await bll.DeleteApplication(jobSeekerId, jobId);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [Route("invitations")]
        public async Task<IEnumerable<InvitationDto>> GetInvitations(int jobSeekerId)
        {
            return await bll.GetInvitaions(jobSeekerId);
        }

        [HttpGet]
        [Route("invitations/{jobId:int}")]
        public async Task<InvitationDto> GetInvitation(int jobId, int jobSeekerId)
        {
            return await bll.GetInvitation(jobId, jobSeekerId);
        }

        // PUT: api/Profile/5
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutJobSeeker(int jobSeekerId, JobSeeker jobSeeker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (jobSeekerId != jobSeeker.ID)
            {
                return BadRequest();
            }
            await bll.UpdateJobSeeker(jobSeeker);
           /* db.Entry(jobSeeker).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobSeekerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }*/

            return StatusCode(HttpStatusCode.NoContent);
        }

        //todo:Post muust be added. Post and Delete removed for the time being.
        /*
        // POST: api/Profile
        [ResponseType(typeof(JobSeeker))]
        public async Task<IHttpActionResult> PostJobSeeker(JobSeeker jobSeeker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobSeekers.Add(jobSeeker);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = jobSeeker.ID }, jobSeeker);
        }

        // DELETE: api/Profile/5
        [ResponseType(typeof(JobSeeker))]
        public async Task<IHttpActionResult> DeleteJobSeeker(int id)
        {
            JobSeeker jobSeeker = await db.JobSeekers.FindAsync(id);
            if (jobSeeker == null)
            {
                return NotFound();
            }

            db.JobSeekers.Remove(jobSeeker);
            await db.SaveChangesAsync();

            return Ok(jobSeeker);
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