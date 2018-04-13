using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inisra_Web_App_MVC.Models
{
    public class CV
    {
        public int ID { get; set; }

        public int JobSeekerID { get; set; } 

        public virtual JobSeeker JobSeeker { get; set; }

        [Required, FileExtensions(Extensions =".docx", ErrorMessage ="Incorrect file format select a .docx file.")]
        [MaxLength(1048576, ErrorMessage ="Maximum Size of 1MB Exceeded")]
        public byte[] Document { get; set; }
    }

    public class JobDescription
    {
        [Key]
        [ForeignKey("Job")]
        public int JobID { get; set; }

        public virtual Job Job { get; set; }
        
        [Required, FileExtensions(Extensions = ".docx", ErrorMessage = "Incorrect file format select a .docx file.")]
        [MaxLength(1048576, ErrorMessage = "Maximum Size of 1MB Exceeded")]
        public byte[] Document { get; set; }
    }
}