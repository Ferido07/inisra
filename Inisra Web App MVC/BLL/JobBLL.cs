﻿using Inisra_Web_App_MVC.DAL;
using Inisra_Web_App_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;


namespace Inisra_Web_App_MVC.BLL
{
    public class JobBLL
    {
        private InisraContext context = new InisraContext();

        public async Task<Job> GetJobById(int id)
        {
           return await context.Jobs.FindAsync(id);
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            /*  This could alternatively be used to skip the 5 checks in searchjobs
             *  
             *  var jobs = context.Jobs.Include(j => j.Company).Include(j => j.Location);
                jobs = jobs.Where(j => j.IsInvitationOnly == false && j.IsOpen == true && j.ApplicationDeadlineDate.CompareTo(DateTime.Now) == 1);
                return await jobs.ToListAsync();
            */
            return await SearchJobs("", "", "", "", null);
        }

        public async Task<IEnumerable<Job>> GetJobsByCompanyId(int companyId)
        {
            var jobs = context.Jobs.Where(j => j.CompanyID == companyId).Include(l => l.Location);
            return await jobs.ToListAsync();
        }
        //todo: nef gets
        //could do getjobsbylocationid but that is useless. for all practical purpose that is not used only search is used

        public async Task<IEnumerable<Job>> SearchJobs(string title, string profession, string location, string companyName, int? companyId)
        {
            //
            var jobs = context.Jobs.Include(j => j.Company).Include(j => j.Location);
            jobs = jobs.Where(j => j.IsInvitationOnly == false && j.IsOpen == true && j.ApplicationDeadlineDate.CompareTo(DateTime.Now) == 1);

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

            return await jobs.ToListAsync();
        }

        //todo : possible to have postjob, editjob then make addjob and updatejob private but only gives name convention for the respective services 

        public async void AddJob(Job job)
        {
            if (CheckJobValidity(job)) { 
                context.Jobs.Add(job);
                await context.SaveChangesAsync();
            }
        } 

        public async void UpdateJob(Job job)
        {
            if (CheckJobValidity(job)) { 
                //check if the applications deadline is modified if yes then chck if its later than current date.
                context.Entry(job).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }

        }

        public async void DeleteJob(Job job)
        {
            if (CheckJobValidity(job))
            {
                context.Jobs.Remove(job);
                await context.SaveChangesAsync();
            }
        }

        private bool CheckJobValidity(Job job)
        {
            //todo: Check for job
            //check application deadline is later than the post date
            /*
             * For Use with update so the tope check comment can be removed if ti is implemented here.
             * check if the applications deadline is modified if yes then chck if its later than current date.
             */
            return true;
        }

        internal void Dispose()
        {
            context.Dispose();
        }
    }
}