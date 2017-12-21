using System;
using System.ComponentModel.DataAnnotations;

namespace Inisra_Web_App_MVC.Models
{
    //might follow the approach of skills by making it many-to-many after further study
    public class Certificate
    {
        public int ID { get; set; }

        public int JobSeekerID { get; set; }

        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [Required]
        [Display(Name="Certified by")]
        [StringLength(60)]
        public string CertificateIssuer { get; set; }

        [Display(Name="Certification Date")]
        [DataType(DataType.Date)]
        public DateTime? IssueDate { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public virtual JobSeeker JobSeeker { get; set; }
    }
}