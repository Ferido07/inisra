using Inisra_Web_App_MVC.DAL;
using Inisra_Web_App_MVC.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;

namespace Inisra_Web_App_MVC.BLL
{
    public class JobSeekerBLL
    {
        private InisraContext context = new InisraContext();

        public async Task<IEnumerable<JobSeeker>> GetJobSeekers()
        {
            return await context.JobSeekers.ToListAsync();
        }

        //Note: This code could be changed to incorporate navigation properties but probably not needed
        public async Task<JobSeeker> GetJobSeekerById(int jobSeekerId)
        {
            return await context.JobSeekers.FindAsync(jobSeekerId);
        }

        public async Task<IEnumerable<JobSeeker>> SearchJobSeekers(string name)
        {
            var jobSeekers = from js in context.JobSeekers select js;

            if (!string.IsNullOrEmpty(name))
            {
                jobSeekers = jobSeekers.Where(j => j.FirstName.Contains(name) || j.LastName.Contains(name));
            }

            return await jobSeekers.ToListAsync();
        }

        //Note: may not be used. that is y it is private so that it is not used by accident before implementing it 
        private async void AddJobSeeker (JobSeeker jobSeeker)
        {
            
        }

        public async void UpdateJobSeeker (JobSeeker jobSeeker)
        {
            if (CheckJobSeekerValidity(jobSeeker))
            {
                context.Entry(jobSeeker).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        //Note: may not be used. that is y it is private so that it is not used by accident before implementing it 
        private async void DeleteJobSeeker (JobSeeker jobSeeker)
        {

        }

        private bool CheckJobSeekerValidity(JobSeeker jobSeeker)
        {
            return true;
        }

        public async void Apply(int jobSeekerId, int jobId)
        {
            //todo: check if application already exists and other checks
            var application = new Application { JobSeekerID = jobSeekerId, JobID = jobId };
            context.Applications.Add(application);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Application>> GetJobSeekerApplications(int jobSeekerId)
        {
            var applications = context.Applications.Where(a => a.JobSeekerID == jobSeekerId).Include(a => a.Job)
                                .Include(a => a.Job.Company);
            return await applications.ToListAsync();
        }

        public async Task<IEnumerable<Invitation>> GetJobSeekerInvitaions(int jobSeekerId)
        {
            var invitations = context.Invitations.Where(i => i.JobSeekerID == jobSeekerId).Include(i => i.Job)
                                    .Include(i => i.Job.Company);
            return await invitations.ToListAsync();
        }

        public async Task<Application> GetApplication(int jobSeekerId, int jobId)
        {
            var application = await context.Applications.FindAsync(jobSeekerId, jobId);
            return application;
        }

        public async void DeleteApplication(int jobSeekerId, int jobId)
        {
            //TODO: test this code it might cause errors due to the find method
            var application = context.Applications.Find(jobSeekerId, jobId);
            if (application != null)
            {
                context.Applications.Remove(application);
                await context.SaveChangesAsync();
            }
        }

        
    }
}