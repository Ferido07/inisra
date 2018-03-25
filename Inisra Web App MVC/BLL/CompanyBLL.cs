﻿using Inisra_Web_App_MVC.DAL;
using Inisra_Web_App_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Inisra_Web_App_MVC.BLL
{
    public class CompanyBLL : IDisposable
    {
        private InisraContext context = new InisraContext();

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await context.Companies.ToListAsync();
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
        public IEnumerable<Job> GetCompanyJobs(int companyId, string jobTitle)
        {
            var jobs = context.Jobs.Where(j => j.CompanyID == companyId).Include(l => l.Location);
            if (!String.IsNullOrEmpty(jobTitle))
                jobs = jobs.Where(j => j.Title.Contains(jobTitle));

            return jobs.ToList();
        }
        

        public IEnumerable<Application> GetCompanyApplications(int companyId)
        {
           var applications = context.Applications.Where(a => a.Job.CompanyID == companyId)
                                .Include(a => a.Job).Include(a => a.JobSeeker);
            //applications.GroupBy(a => a.Job.Title);
            return applications.ToList();
        }

        public IEnumerable<Application> GetCompanyApplicationsForAJob(int companyId, int jobId)
        {
           var applications = context.Applications.Where(a => a.JobID == jobId && a.Job.CompanyID == companyId)
                                .Include(a => a.Job).Include(a => a.JobSeeker);

            return applications.ToList();
        }

        public IEnumerable<Invitation> GetCompanyInvitations(int companyId)
        {
            var invitations = from i in context.Invitations
                              where i.Job.CompanyID == companyId
                              select i;
            invitations.Include(i => i.Job).Include(i => i.JobSeeker);

            return invitations.ToList();
        }

        public async Task<Invitation> GetInvitationAsync(int jobId, int jobSeekerId)
        {
            Invitation invitation = await context.Invitations.FindAsync(jobId, jobSeekerId);
            return invitation;
        }

        public int Invite(int jobId, int jobSeekerId)
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

        public async Task<bool> DeleteInivitationAsync(int jobId, int jobSeekerId)
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