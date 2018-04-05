using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Inisra_Web_App_MVC.BLL;
using Inisra_Web_App_MVC.Models;
using Inisra_Web_App_MVC.DTOs;

namespace Inisra_Web_App_MVC.Api
{
    [RoutePrefix("api/companies/{companyId:int}")]
    public class CompaniesController : ApiController
    {
        private CompanyBLL bll = new CompanyBLL();

        // GET: api/Companies
        [HttpGet]
        [Route("~/api/companies")]
        public async Task<IEnumerable<CompanyDto>> GetCompanies()
        {
            return await bll.GetCompaniesAsync();
        }

        // GET: api/Companies/5
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(CompanyDto))]
        public async Task<IHttpActionResult> GetCompany(int companyId)
        {
            Company company = await bll.GetCompanyByIdAsync(companyId);
            if (company == null)
            {
                return NotFound();
            }
            var dto = AutoMapper.Mapper.Map<Company, CompanyDto>(company);
            return Ok(dto);
        }

        // PUT: api/Companies/5
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCompany(int companyId, Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (companyId != company.ID)
            {
                return BadRequest();
            }

            await bll.UpdateCompanyAsync(company);
            /*
            try
            {
                await bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
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

        [HttpGet]
        [Route("jobs")]
        public IEnumerable<JobDto> GetJobs(int companyId, string title = null)
        {
            var jobs = bll.GetCompanyJobs(companyId, title);
            return jobs;
        }

        [HttpGet]
        [Route("applications")]
        public  IEnumerable<ApplicationDto> GetApplications (int companyId, int? jobId = null)
        {
            var dtos = bll.GetApplications(companyId, jobId);
            return dtos;
        }

        [HttpPost]
        [Route("invite/{jobId:int}/{jobSeekerId:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Invite (int companyId, int jobId, int jobSeekerId)
        {
            bll.Invite(companyId, jobId, jobSeekerId);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [Route("invitations")]
        public IEnumerable<InvitationDto> GetInvitations(int companyId)
        {
            var dtos = bll.GetCompanyInvitations(companyId);
            return dtos;
        }

        [HttpDelete]
        [Route("invitations/{jobId:int}/{jobSeekerId:int}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> DeleteInvitation(int jobId, int jobSeekerId,int companyId) 
        {
            await bll.DeleteInivitationAsync(companyId, jobId, jobSeekerId);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /*
        // POST: api/Companies
        [ResponseType(typeof(Company))]
        public async Task<IHttpActionResult> PostCompany(Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bll.Companies.Add(company);
            await bll.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = company.ID }, company);
        }

        // DELETE: api/Companies/5
        [ResponseType(typeof(Company))]
        public async Task<IHttpActionResult> DeleteCompany(int id)
        {
            Company company = await bll.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            bll.Companies.Remove(company);
            await bll.SaveChangesAsync();

            return Ok(company);
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