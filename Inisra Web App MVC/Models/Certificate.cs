using System;

namespace Inisra_Web_App_MVC.Models
{
    public class Certificate
    {
        public int ID { get; set; }

        public int JobSeekerID { get; set; }

        public string Name { get; set; }

        public string CertificateIssuer { get; set; }

        public DateTime IssueDate { get; set; }

        public string Description { get; set; }

        public virtual JobSeeker JobSeeker { get; set; }
    }
}