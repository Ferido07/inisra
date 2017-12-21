using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inisra_Web_App_MVC.Models
{
    public enum Type
    {
        FULLTIME, ONLINE, EXTENSION
    }

    public class Education
    {
        public int ID { get; set; }

        public int JobSeekerID { get; set; }

        /// <summary>
        /// indicates the name Example Electrical and Computer Engineering
        /// </summary>
        [StringLength(60)]
        [Display(Name = "Education")]
        public string Name { get; set; }

        public int EducationLevelID { get; set; }
        
        //only add this if the relationship is 1 to 1 instead of 1 to 0 or 1 and if institution is required
        // public int InstitutionID { get; set; }

        /// <summary>
        /// online, full time, extension,...
        /// </summary>
        [Display(Name = "Education Type")]
        public Type? EducationType { get; set; }

        [Display(Name = "Date Completed")]
        [DataType(DataType.Date)]
        public DateTime? DateCompleted { get; set; }

        public virtual JobSeeker JobSeeker { get; set; }

        public virtual EducationLevel EducationLevel { get; set; }

        public virtual ICollection<Institution> Institutions { get; set; } 
    }

    //todo think about it maybe replaced by enum or struct, n do research
    public class EducationLevel
    {
        public int ID { get; set; }
        /// <summary>
        /// Name describes the name. Example Bachelors, Grade 12 ,...
        /// </summary>
        [Required]
        [Display(Name = "Level of Education")]
        public string Name { get; set; }

        /// <summary>
        /// Offers shorthand for the name. Example Bsc, Dr, G12,...
        /// </summary>
        public string ShorthandNotation { get; set; }

        public virtual ICollection<Education> Educations { get; set; }


    }
}