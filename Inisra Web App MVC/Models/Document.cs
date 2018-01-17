using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inisra_Web_App_MVC.Models
{
    public class CV
    {
        public int ID { get; set; }

        public int JobSeekerID { get; set; } 

        public virtual JobSeeker JobSeeker { get; set; }

        public byte[] Document { get; set; }
    }

    public class JobDescription
    {
        [Key]
        [ForeignKey("Job")]
        public int JobID { get; set; }

        public virtual Job Job { get; set; }
        
        public byte[] Document { get; set; }

    }
}