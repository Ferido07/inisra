using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inisra_Web_App_MVC.Models
{
    public class Application
    {
        public int ID { get; set; }

        public int JobSeekerID { get; set; }

        public int JobID { get; set; }

        public DateTime ApplicationDate { get; set; }

        public virtual JobSeeker JobSeeker { get; set; }

        public virtual Job Job { get; set; }

    }
}