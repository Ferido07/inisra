using System;

namespace Inisra_Web_App_MVC.DTOs
{
    public class InvitationDto
    {
        public int JobId { get; set; }
        public int JobSeekerId { get; set; }
        public DateTime InvitationDate { get; set; }
        public string JobTitle { get; set; }
        public string JobSeekerFullName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}