using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inisra_Web_App_MVC.Models
{
    public enum Rate
    {
        HOURLY, WEEKLY, MONTHLY, YEARLY
    }

    public class Job
    {
        public int ID { get; set; }

        public int CompanyID { set; get; }

        [Required]
        [StringLength(50, ErrorMessage = "Title cannot be longer than 50 characters.")]
        public string Title { set; get; }

        [StringLength(50, ErrorMessage = "Profession cannot be longer than 50 characters.") ]
        public string Profession { set; get; }

        public int? Salary { set; get; }

        [Display(Name ="Currency")]
        [DataType(DataType.Currency)]
        public int? SalaryCurrency { set; get; }

        [Display(Name ="Rate")]
        [DataType(DataType.Custom)]
        public Rate SalaryRate { set; get; }

        [Display(Name = "Open for Application")]
        public bool isOpen { set; get; } = true;

        [Display(Name = "Privacy")]
        public bool isInvitationOnly { set; get; } = false;

        [StringLength(100, ErrorMessage = "Location cannot be longer than 100 characters.")]
        public string Location { set; get; }

        [Display(Name = "Post Time")]
        [DataType(DataType.DateTime)]
        public DateTime PostDate { set; get; } = DateTime.Now;

        [Display(Name ="Application Deadline")]
        [DataType(DataType.Date)]
        public DateTime ApplicationDeadlineDate { set; get; }

        [DataType(DataType.MultilineText)]
        public string Description { set; get; }

        public virtual Company Company { set; get; }

    }

    public class JobHistory
    {
        public int JobID { set; get; }

        public DateTime UpdateTime { set; get; }

        public bool isOpen
        {
            get
            {//todo finish or remove JobHistory last action 
                //if (1)
                return true;
            }
        }

        public DateTime LastOpenDate { set; get; }

        public DateTime LastClosedDate { set; get; }

        public virtual Job Job { set; get; }
    }
}