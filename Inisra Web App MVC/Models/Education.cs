using System;

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
        /// indicates the nsme Example Electrical and Computer Engineering
        /// </summary>
        public string Name { get; set; }

        public int EducationLevelID { get; set; }
        
        //only add this if the relationship is 1 to 1 instead of 1 to 0 or 1 and if institution is required
       // public int InstitutionID { get; set; }

        /// <summary>
        /// online, full time, extension,...
        /// </summary>
        public Type? Type { get; set; }

        public DateTime DateCompleted { get; set; }

        public virtual JobSeeker JobSeeker { get; set; }
        
        public virtual Institution Institution { get; set; } 

        public virtual EducationLevel EducationLevel { get; set; }
    }

    //todo think about it maybe replaced by enum or struct, n do research
    public class EducationLevel
    {
        public int ID { get; set; }
        /// <summary>
        /// Name describes the name. Example Bachelors, Grade 12 ,...
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Offers shorthand for the name. Example Bsc, Dr, G12,...
        /// </summary>
        public string ShorthandNotation { get; set; }


    }
}