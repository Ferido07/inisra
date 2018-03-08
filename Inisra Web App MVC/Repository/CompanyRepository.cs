using Inisra_Web_App_MVC.DAL;
using Inisra_Web_App_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Inisra_Web_App_MVC.Repository
{
    public class CompanyRepository
    {
        private InisraContext context = new InisraContext();

        public async Task<List<Company>> GetCompanies()
        {
            return await context.Companies.ToListAsync();
        }

        public async Task<Company> FindCompany(int companyId)
        {
            var company = await context.Companies.FindAsync(companyId);
            return company;
        }

        public void Update(Company company)
        {
            //todo: Maybe add a chech if the company exists or not before changing the modified 
            context.Entry(company).State = EntityState.Modified;
            //context.Companies.Update(company); ---> Only available for EFCore implementation
            context.SaveChangesAsync();
        }

        public IEnumerable<Job> GetCompanyJobs(int companyId, string jobTitle)
        {
            var jobs = context.Jobs.Where(j => j.CompanyID == companyId).Include(l => l.Location);
            if (!String.IsNullOrEmpty(jobTitle))
                jobs = jobs.Where(j => j.Title.Contains(jobTitle));

            return jobs.ToList();
        }

        public IEnumerable<Application> GetAllApplicationsForCompany(int companyId)
        {
           var applications = context.Applications.Where(a => a.Job.CompanyID == companyId)
                                .Include(a => a.Job).Include(a => a.JobSeeker);
            //applications.GroupBy(a => a.Job.Title);
            return applications.ToList();
        }

        public IEnumerable<Application> GetJobApplicationsForCompany(int companyId, int jobId)
        {
           var applications = context.Applications.Where(a => a.JobID == jobId && a.Job.CompanyID == companyId)
                                .Include(a => a.Job).Include(a => a.JobSeeker);

            return applications.ToList();
        }

        public IEnumerable<Invitation> GetInvitationsOfCompany (int CompanyId)
        {
            var invitations = from i in context.Invitations
                              where i.Job.CompanyID == CompanyId
                              select i;
            invitations.Include(i => i.Job).Include(i => i.JobSeeker);

            return invitations.ToList();
        }

        public async Task<Invitation> FindInvitation(int jobId, int jobSeekerId)
        {
            Invitation invitation = await context.Invitations.FindAsync(jobId, jobSeekerId);
            return invitation;
        }

        public int Invite (int jobId, int jobSeekerId)
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

        public async Task<bool> DeleteInivitation(int jobId, int jobSeekerId)
        {
            try
            {
                var invitation = context.Invitations.Single(i => i.JobID == jobId && i.JobSeekerID == jobSeekerId);
                context.Invitations.Remove(invitation);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal void Dispose()
        {
            context.Dispose();
        }


    }
}