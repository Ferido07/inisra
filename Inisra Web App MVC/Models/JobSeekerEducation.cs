using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inisra_Web_App_MVC.Models
{
    public class JobSeekerEducation
    {
        [Key, Column(Order = 0)]
        public int JobSeekerID { get; set; }

        public virtual JobSeeker JobSeeker { get; set; }

        [Key, Column(Order = 1)]
        public int EducationID { get; set; }

        public virtual Education Education { get; set; }

        public int? InstitutionID { get; set; }

        public virtual Institution Institution { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }
         
        [Display(Name = "Date Completed")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CompletionDate { get; set; }

        //since it is enum no separate table is created for it and it wont be a foreign key
        [Display(Name = "Eduacation Type")]
        public EducationType? EducationType { get; set; }
    }
}