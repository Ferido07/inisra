using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inisra_Web_App_MVC.Models
{
    public class Invitation
    {   
        public int JobID { get; set; }
        
        public int JobSeekerID { get; set; }

        [Display(Name = "Invitation Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InvitationDate { get; set; } = DateTime.Now;

        public virtual Job Job { get; set; }

        public virtual JobSeeker JobSeeker { get; set; }
    }
}