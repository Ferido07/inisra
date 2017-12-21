using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inisra_Web_App_MVC.Models
{
    //Has many-to-many relationship with JobSeeker
    public class Skill
    {
        //Good Idea to have ID as int rather than just the name itself because people might make spelling errors
        //and hence the error can be corected by just fixing the name without touching any related tables.
        //Got the Idea from Table of Roles.
        public int ID { get; set; }

        [Required]
        [Display(Name ="Skill")]
        [StringLength(50, ErrorMessage = "Skill cannot be longer than 50 characters.")]
        public string Name { get; set; }

        public virtual ICollection<JobSeeker> JobSeekers { get; set; }
    }
}