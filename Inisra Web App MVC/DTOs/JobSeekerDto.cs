using System;
using System.Collections.Generic;


namespace Inisra_Web_App_MVC.DTOs
{
    public class JobSeekerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? PhoneNo { get; set; }
        public string Sex { get; set; }
        public DateTime? Birthday { get; set; }
        public int? LocationId { get; set; }
        public string LocationName { get; set; }
        public List<Models.CV> CVs { get; set; } 
    }
}