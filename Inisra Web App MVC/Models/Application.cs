using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inisra_Web_App_MVC.Models
{
    public class Application
    {
        public int JobSeekerID { get; set; }

        public int JobID { get; set; }

        [Display(Name = "Application Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ApplicationDate { get; set; }

        public virtual JobSeeker JobSeeker { get; set; }

        public virtual Job Job { get; set; }
    }
}