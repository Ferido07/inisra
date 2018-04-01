using System;

namespace Inisra_Web_App_MVC.DTOs
{
    public class ApplicationDto
    {
        public int JobSeekerId { get; set; }
        public int JobId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string JobSeekerFullName { get; set; }
        public string JobTitle { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}