using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inisra_Web_App_MVC.Models
{
    public class CV
    {    
        public int ID { get; set; }

        public int JobSeekerID { get; set; } 

        /*
         * Caused ENtityValidation Errors which took ages to resolve such a messy attribute.
         * [FileExtensions(Extensions =".docx", ErrorMessage ="Incorrect file format select a .docx file.")]
         */
        [Required]
        [StringLength(50,ErrorMessage = "Document Name cannot be more than 50")]
        public string DocumentName { get; set; }

        public DocumentType DocumentType { get; set; }

        [Required]
        [MaxLength(1048576, ErrorMessage ="Maximum Size of 1MB Exceeded")]
        public byte[] Document { get; set; }

        public bool ValidityConfirmed { get; set; } = false;

        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }

    public class JobDescription
    {
        [Key]
        [ForeignKey("Job")]
        public int JobID { get; set; }

        public Job Job { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Document Name cannot be more than 50")]
        public string DocumentName { get; set; }

        public DocumentType DocumentType { get; set; }

        //[FileExtensions(Extensions = ".docx", ErrorMessage = "Incorrect file format select a .docx file.")]
        [Required] 
        [MaxLength(1048576, ErrorMessage = "Maximum Size of 1MB Exceeded")]
        public byte[] Document { get; set; }

        public bool ValidityConfirmed { get; set; } = false;

        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }

    public enum DocumentType
    {
        docx
    }
}