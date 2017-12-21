using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inisra_Web_App_MVC.Models
{
    public class Location
    {
        public int ID { get; set; }

        [Required]
        [Display(Name="Location")]
        [StringLength(100, ErrorMessage = "Location cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public virtual ICollection<JobSeeker>JobSeekers { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }

        public virtual ICollection<Institution> Institutions { get; set; }
    }
}