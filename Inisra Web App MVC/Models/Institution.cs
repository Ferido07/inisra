using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inisra_Web_App_MVC.Models
{
    //many-to-many relationship with education
    public class Institution
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Inistitution name cannot be longer than 100 characters.")]
        [Display(Name = "Institution")]
        public string Name { get; set; }

        [StringLength(300, ErrorMessage = "About cannot be longer than 300 characters.")]
        public string About { get; set; }

        public virtual ICollection<Location> Locations { get; set; }

        public virtual ICollection<JobSeekerEducation> JobSeekerEducations { get; set; }  
    }
}