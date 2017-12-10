using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inisra_Web_App_MVC.Models
{
    public class Job
    {
        public int ID { get; set; }

        //[ForeignKey("Company")] not needed bedause it is understood by ef default
        public int CompanyID { set; get; }

        public string Title { set; get; }

        public Boolean isOpen { set; get; }

        public Boolean isInvitationOnly { set; get; }//Todo: make the default false

        public string Location { set; get; }

        [Display(Name = "Post Time")]
        public DateTime PostDate { set; get; } = DateTime.Now;

        [Display(Name ="Application Deadline")]
        public DateTime ApplicationDeadlineDate { set; get; }

        [DataType(DataType.MultilineText)]
        public string Description { set; get; }

        public virtual Company Company { set; get; }

    }

    public class JobHistory
    {
        public int ID { set; get; }

        public int JobID { set; get; }

        public bool isOpen
        {
            get
            {//todo finish or remove JobHistory last action 
                //if (1)
                return true;
            }
        }

        DateTime LastOpenDate { set; get; }

        DateTime LastClosedDate { set; get; }
    }
}