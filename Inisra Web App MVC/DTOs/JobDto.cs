using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inisra_Web_App_MVC.DTOs
{
    public class JobDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Title { get; set; }
        public string Profession { get; set; }
        public int? Salary { get; set; }
        //these two need conversion resolver 
        //todo: for phase 1. look into datatype currency and how to map it to string or more impoertantly how to use it.
        //public string Currency { get; set; }
        public string Rate { get; set; }
        public bool IsOpen { get; set; } 
        public bool IsInvitationOnly { get; set; } 
        public string LocationName { get; set; }
        public DateTime PostDate { get; set; } 
        public DateTime ApplicationDeadlineDate { set; get; }
        public string Description { set; get; }
    }
}