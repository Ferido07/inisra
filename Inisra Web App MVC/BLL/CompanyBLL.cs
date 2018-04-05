using Inisra_Web_App_MVC.DAL;
using Inisra_Web_App_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Inisra_Web_App_MVC.DTOs;

namespace Inisra_Web_App_MVC.BLL
{
    public class CompanyBLL : IDisposable
    {
        private InisraContext context = new InisraContext();

        public async Task<IEnumerable<CompanyDto>> GetCompaniesAsync()
        {
            var companies = context.Companies.ProjectTo<CompanyDto>();
            return await companies.ToListAsync();
        }

        public async Task<Company> GetCompanyByIdAsync(int companyId)
        { 
            return await context.Companies.FindAsync(companyId);
        }

        //Note: this code could be removed just leave it for now
        public async Task<IEnumerable<Company>> GetCompaniesByNameAsync(string name)
        {
            var company = context.Companies.Where(c => c.Name.Equals(name));
            return await company.ToListAsync();    
        }

        public async Task<IEnumerable<Company>> SearchCompaniesAsync(string name, string location)
        {
            var companies = context.Companies.Include(c => c.Locations);
            //
            if (!string.IsNullOrEmpty(name))
                companies = companies.Where(c => c.Name.Contains(name));

            //todo lookup join statement 
            if (!string.IsNullOrEmpty(location)) {
                var locations = context.Locations.Where(l => l.Name.Contains(location));
                //companies = companies.Where(c => c.Locations.f)
            }
            return await companies.ToListAsync();
        }

        //may not be used. that is y it is private so that it is not used by accident before implementing it 
        private void AddCompany(Company company)
        {

        }

        //
        public async Task<bool> UpdateCompanyAsync(Company company)
        {
            //todo: Maybe add a chech if the company exists or not before changing the modified 
            context.Entry(company).State = EntityState.Modified;
            //context.Companies.Update(company); ---> Only available for EFCore implementation
            return await context.SaveChangesAsync() == 1? true : false;
            //method return type could be chenged to reflect result of update
        }

        //may not be used. that is y it is private so that it is not used by accident before implementing it 
        private void DeleteCompany(Company comapany)
        {

        }

        //todo: refactor to jobBLL if refactored code in CompanyProfile Controller gets error and also JobBLL would be needed in the controller
        public IEnumerable<JobDto> GetCompanyJobs(int companyId, string jobTitle)
        {
            var jobs = context.Jobs.Where(j => j.CompanyID == companyId).Include(l => l.Location);
            if (!string.IsNullOrEmpty(jobTitle))
                jobs = jobs.Where(j => j.Title.Contains(jobTitle));

            var dtos = jobs.ProjectTo<JobDto>();
            return dtos.ToList();
        }
        public async Task<Job> GetJob(int companyId, int jobId)
        {
            Job job = await context.Jobs.SingleOrDefaultAsync(j => j.CompanyID == companyId && j.ID == jobId);
            return job;
        }
        
        public IEnumerable<ApplicationDto> GetApplications(int companyId, int? jobId = null)
        {
            var applications = context.Applications.Where(a => a.Job.CompanyID == companyId)
                     .Include(a => a.Job).Include(a => a.JobSeeker);

            if (jobId.HasValue)
            {
                applications = applications.Where(a => a.JobID == jobId.Value);
            }

            var dtos = applications.ProjectTo<ApplicationDto>();
            return dtos.ToList();
        } 

        public IEnumerable<InvitationDto> GetCompanyInvitations(int companyId)
        {
            var invitations = from i in context.Invitations
                              where i.Job.CompanyID == companyId
                              select i;
            var dtos = invitations.Include(i => i.Job).Include(i => i.JobSeeker).ProjectTo<InvitationDto>();

            return dtos.ToList();
        }

        public async Task<Invitation> GetInvitationAsync(int jobId, int jobSeekerId)
        {
            Invitation invitation = await context.Invitations.FindAsync(jobId, jobSeekerId);
            return invitation;
        }

        public int Invite(int companyId, int jobId, int jobSeekerId)
        {
            /*todo :Note: the checking meckanism before adding invitation may need to be incorporated here
              than in the controller */
            Invitation invitation = new Invitation()
            {
                JobID = jobId,
                JobSeekerID = jobSeekerId,
            };

            context.Invitations.Add(invitation);
            return context.SaveChanges();
        }

        public async Task DeleteInivitationAsync(int companyId, int jobId, int jobSeekerId)
        {
            var invitation = await context.Invitations.FindAsync(jobId, jobSeekerId);
            if (invitation != null && invitation.Job.CompanyID == companyId)
            {
                context.Invitations.Remove(invitation);
                await context.SaveChangesAsync();
            }
        }

        public Job FindACompanyJob(int companyId, int jobId)
        {
            return null;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    context.Dispose();
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}