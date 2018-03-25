using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Inisra_Web_App_MVC.Models;
using Inisra_Web_App_MVC.BLL;

namespace Inisra_Web_App_MVC.Api
{
    public class JobsController : ApiController
    {
        
        private JobBLL jobBLL = new JobBLL();

        // GET: api/Jobs
        public async Task<IEnumerable<Job>> GetJobs()
        {
            return await jobBLL.GetJobs();
        }

        //GET: api/Jobs
        public async Task<IEnumerable<Job>> GetJobs(string title, string profession, string location, string companyName, int? companyId)
        {
            return await jobBLL.SearchJobs(title, profession, location, companyName, companyId);
        }

        // GET: api/Jobs/5
        [ResponseType(typeof(Job))]
        public async Task<IHttpActionResult> GetJob(int id)
        {
            Job job = await jobBLL.GetJobById(id);
            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);
        }

        // PUT: api/Jobs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutJob(int id, Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != job.ID)
            {
                return BadRequest();
            }
            await jobBLL.UpdateJob(job);
           /* db.Entry(job).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            */
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Jobs
        [ResponseType(typeof(Job))]
        public async Task<IHttpActionResult> PostJob(Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await jobBLL.AddJob(job);

            return CreatedAtRoute("DefaultApi", new { id = job.ID }, job);
        }

        // DELETE: api/Jobs/5
        [ResponseType(typeof(Job))]
        public async Task<IHttpActionResult> DeleteJob(int id)
        {
            Job job = await jobBLL.GetJobById(id);
            if (job == null)
            {
                return NotFound();
            }

            await jobBLL.DeleteJob(job);

            return Ok(job);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                jobBLL.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}