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
    public class JobBLL : IDisposable
    {
        private InisraContext context = new InisraContext();

        public async Task<Job> GetJobById(int id)
        {
            var job = await context.Jobs.FindAsync(id);
            //var dto = Mapper.Map<Job, JobDto>(job);
            return job;
        }

        public async Task<IEnumerable<JobDto>> GetJobs()
        {
            /*  This could alternatively be used to skip the 5 checks in searchjobs
             *  
             *  var jobs = context.Jobs.Include(j => j.Company).Include(j => j.Location);
                jobs = jobs.Where(j => j.IsInvitationOnly == false && j.IsOpen == true && j.ApplicationDeadlineDate.CompareTo(DateTime.Now) == 1);
                return await jobs.ToListAsync();
            */
            return await SearchJobs("", "", "", "", null);
        }

        public async Task<IEnumerable<JobDto>> GetJobsByCompanyId(int companyId)
        {
            var jobs = context.Jobs.Where(j => j.CompanyID == companyId).Include(l => l.Location).ProjectTo<JobDto>();
            return await jobs.ToListAsync();
        }

        public async Task<IEnumerable<JobDto>> SearchJobs(string title, string profession, string location, string companyName, int? companyId)
        {
            //
            var jobs = context.Jobs.Include(j => j.Company).Include(j => j.Location);
            jobs = jobs.Where(j => j.IsInvitationOnly == false && j.IsOpen == true && j.ApplicationDeadlineDate.CompareTo(DateTime.Now) == 1);
            
            //IEnumerable<JobDto> dto = 
            //
            if (!string.IsNullOrEmpty(title))
                jobs = jobs.Where(j => j.Title.Contains(title));

            //
            if (!string.IsNullOrEmpty(profession))
                jobs = jobs.Where(j => j.Profession.Contains(profession));

            //
            if (!string.IsNullOrEmpty(location))
                jobs = jobs.Where(j => j.Location.Name.Contains(location));
            
            //
            if (!string.IsNullOrEmpty(companyName))
                jobs = jobs.Where(j => j.Company.Name.Contains(companyName));

            //
            if (companyId.HasValue)
                jobs = jobs.Where(j => j.CompanyID == companyId.Value);

            return await jobs.ProjectTo<JobDto>().ToListAsync();   
        }

        public async Task AddJob(Job job)
        {
            if (await CheckJobValidity(job)) {

                //check if the location already Exists. if the location doesnt exist the default value returned is null
                // the "==" operator is case insensitive.
                var location = await context.Locations.SingleOrDefaultAsync(l => l.Name == job.Location.Name);
                if (location != null)
                {
                    job.LocationID = location.ID;
                    job.Location = location;
                }
                else
                    context.Locations.Add(job.Location);
                context.Jobs.Add(job);
                await context.SaveChangesAsync();
                var jd = job.JobDescriptionDocument;
                if (jd != null)
                {
                    await Inisra.Solr.Operations.SolrOperations.AddJobDescriptionAsync(
                                fileStream: new System.IO.MemoryStream(jd.Document),
                                id: ""+ jd.JobID,
                                resourceName: jd.DocumentName,
                                owner: job.Company.Name,
                                title: job.Title
                    );
                }

            }
        } 

        public async Task UpdateJob(Job job)
        {
            if (await CheckJobValidity(job)) {

                //check if the location already Exists. if the location doesnt exist the default value returned is null         
                var location = await context.Locations.SingleOrDefaultAsync(l => l.Name == job.Location.Name);
                if (location != null)
                {
                    job.LocationID = location.ID;
                    job.Location = location;
                }
                else
                    context.Locations.Add(job.Location);
                if (job.JobDescriptionDocument != null)
                {
                    var jd = await context.JobDescriptions.FindAsync(job.ID);
                    if (jd != null)
                    {
                        var newJD = job.JobDescriptionDocument;
                        jd.Document = newJD.Document;
                        jd.DocumentName = newJD.DocumentName;
                        jd.DocumentType = newJD.DocumentType;
                        jd.ValidityConfirmed = false;
                        jd.LastUpdated = DateTime.Now;
                        job.JobDescriptionDocument = jd;
                        context.Entry(jd).State = EntityState.Modified;

                        await Inisra.Solr.Operations.SolrOperations.AddJobDescriptionAsync(
                                    fileStream: new System.IO.MemoryStream(jd.Document),
                                    id: "" + jd.JobID,
                                    resourceName: jd.DocumentName,
                                    owner: job.Company.Name,
                                    title: job.Title
                        );
                    }
                    else
                        context.JobDescriptions.Add(job.JobDescriptionDocument);                       
                }
                //check if the applications deadline is modified if yes then chck if its later than current date.
                context.Entry(job).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }

        }

        public async Task DeleteJob(Job job)
        {
            if (await CheckJobValidity(job))
            {


                context.Jobs.Remove(job);
                await context.SaveChangesAsync();
            }
        }

        private async Task<bool> CheckJobValidity(Job job)
        {
            //todo: Check for job
            //check application deadline is later than the post date
            /*
             * For Use with update so the tope check comment can be removed if ti is implemented here.
             * check if the applications deadline is modified if yes then chck if its later than current date.
             */
            var company = await context.Companies.FindAsync(job.CompanyID);
            if (company != null)
            {
                job.Company = company;
                return true;
            }
            else
                return false;
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