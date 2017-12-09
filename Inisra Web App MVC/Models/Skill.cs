using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inisra_Web_App_MVC.Models
{
    public class Skill
    {
        public int ID { get; set; }

        public int JobSeekerID { get; set; }

        public string Name { get; set; }

        public virtual JobSeeker JobSeeker { get; set; }
    }
}