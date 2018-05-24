using Inisra_Web_App_MVC.DAL;
using Inisra_Web_App_MVC.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using System;
using AutoMapper.QueryableExtensions;
using Inisra_Web_App_MVC.DTOs;

namespace Inisra_Web_App_MVC.BLL
{
    public class JobSeekerBLL : IDisposable
    {
        private InisraContext context = new InisraContext();

        public async Task<IEnumerable<JobSeekerDto>> GetJobSeekers()
        {
            var jobSeekers = await context.JobSeekers.ProjectTo<JobSeekerDto>().ToListAsync();
            return jobSeekers;
        }

        //Note: This code could be changed to incorporate navigation properties but probably not needed
        public async Task<JobSeeker> GetJobSeekerById(int jobSeekerId)
        {
            return await context.JobSeekers.FindAsync(jobSeekerId);
        }

        public async Task<IEnumerable<JobSeekerDto>> SearchJobSeekers(string name)
        {
            var jobSeekers = from js in context.JobSeekers select js;

            if (!string.IsNullOrEmpty(name))
            {
                jobSeekers = jobSeekers.Where(j => j.FirstName.Contains(name) || j.LastName.Contains(name));
            }
            var dto = jobSeekers.ProjectTo<JobSeekerDto>();
            return await dto.ToListAsync();
        }

        //Note: may not be used. that is y it is private so that it is not used by accident before implementing it 
        private void AddJobSeeker(JobSeeker jobSeeker)
        {

        }

        public async Task UpdateJobSeeker (JobSeeker jobSeeker)
        {
            if (CheckJobSeekerValidity(jobSeeker))
            {
                context.Entry(jobSeeker).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }


        //Note: may not be used. that is y it is private so that it is not used by accident before implementing it 
        private void DeleteJobSeeker(JobSeeker jobSeeker)
        {

        }

        private bool CheckJobSeekerValidity(JobSeeker jobSeeker)
        {
            return true;
        }

        public async Task Apply(int jobSeekerId, int jobId)
        {
            //todo: check if application already exists and other checks
            var application = new Application { JobSeekerID = jobSeekerId, JobID = jobId };
            context.Applications.Add(application);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ApplicationDto>> GetApplications(int jobSeekerId)
        {
            var applications = context.Applications.Where(a => a.JobSeekerID == jobSeekerId).Include(a => a.Job)
                                .Include(a => a.Job.Company).ProjectTo<ApplicationDto>();
            
            return await applications.ToListAsync();
        }

        public async Task<ApplicationDto> GetApplication(int jobSeekerId, int jobId)
        {
            var application = await context.Applications.Where(a => a.JobSeekerID == jobSeekerId && a.JobID == jobId).ProjectTo<ApplicationDto>().FirstOrDefaultAsync();
            return application;
        }

        public async Task DeleteApplication(int jobSeekerId, int jobId)
        {
            var application = context.Applications.Find(jobSeekerId, jobId);
            if (application != null)
            {
                context.Applications.Remove(application);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<InvitationDto>> GetInvitaions(int jobSeekerId)
        {
            var invitations = context.Invitations.Where(i => i.JobSeekerID == jobSeekerId).Include(i => i.Job)
                                    .Include(i => i.Job.Company).ProjectTo<InvitationDto>();
            return await invitations.ToListAsync();
        }

        public async Task<InvitationDto> GetInvitation(int jobId, int jobSeekerId)
        {
            var invitation = await context.Invitations.Where(i => i.JobID == jobId && i.JobSeekerID == jobSeekerId).Include(i => i.Job)
                                    .Include(i => i.Job.Company).ProjectTo<InvitationDto>().FirstOrDefaultAsync();
            return invitation;
        }

        public async Task AddResume(int jobSeekerId, CV cv)
        {
            JobSeeker jobSeeker = await GetJobSeekerById(jobSeekerId);
            if (jobSeeker != null && jobSeeker.CVs.Count < 3) {
               // var CV = new CV { JobSeekerID = jobSeekerId, Document = resumeDocument };
                jobSeeker.CVs.Add(cv);
                await context.SaveChangesAsync();
            }
            //todo add message that CV count is maximum.
        }

        public async Task RemoveResume(int jobSeekerId, int resumeId)
        {
            JobSeeker jobSeeker = await GetJobSeekerById(jobSeekerId);
            if (jobSeeker != null /*&& jobSeeker.CVs.Any(resume => resume.ID == resumeId)*/)
            {
             CV cv = jobSeeker.CVs.FirstOrDefault(resume => resume.ID == resumeId);
                if (cv != null)
                {
                    if(jobSeeker.CVs.Remove(cv))
                        await context.SaveChangesAsync();
                }
            }

        }
        public async Task<List<CV>> GetCVs(int jobSeekerId)
        {
            
            JobSeeker js = await GetJobSeekerById(jobSeekerId);
            if (js != null)
            {
                var cv = js.CVs.ToList();
                return cv;
            }
            else
                return null;                   
        }
        public async Task BookmarkJob(int jobSeekerId, int jobId)
        {
            
            JobSeeker js = await GetJobSeekerById(jobSeekerId);
            var bookmarkedJob = js.BookmarkedJobs.Where(j => j.ID == jobId).FirstOrDefault();
            if (bookmarkedJob == null)
            {
                Job job = await context.Jobs.FindAsync(jobId);
                js.BookmarkedJobs.Add(job);
                await context.SaveChangesAsync();
            }
            
           /* var bookmarkedJob =context.JobSeekers.Where(js => js.ID == jobSeekerId).Single().BookmarkedJobs.Where(j => j.ID == jobId).FirstOrDefault();
            if (bookmarkedJob == null)
            {
                
            }
            */
        }
        /*todo  ** Really Important find out if all the GetXXX Jobseeker are necessary considering the alternatives of 
             using eager loading when a persons profile is loaded and lazy loading for other details like checking for a
             presense of an application before adding a new application. 
             And with relation to their usage with the android app.
        **/
        public async Task<List<JobDto>> GetBookmarkedJobs(int jobSeekerId)
        {
            var js = await GetJobSeekerById(jobSeekerId);
            var bookmarkedJobs = js.BookmarkedJobs.ToList();
            return AutoMapper.Mapper.Map<List<JobDto>>(bookmarkedJobs);
        }

        public async Task RemoveBookmark(int jobSeekerId, int jobId)
        {
            var js = await GetJobSeekerById(jobSeekerId);
            var job = js.BookmarkedJobs.Where(j => j.ID == jobId).FirstOrDefault();
            if (job != null)
            {
                js.BookmarkedJobs.Remove(job);
                await context.SaveChangesAsync();
            }
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