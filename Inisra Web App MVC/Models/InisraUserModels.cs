using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inisra_Web_App_MVC.Models
{
    public class JobSeeker
    {
        // [DatabaseGenerated(DatabaseGeneratedOption.)]
        public int ID { get; set; }

        /*was added to establish a one to one relationship between JobSeeker and JobSeeker User account but
         removed in order to maintain its independence from implementation of user management */
        //public Guid UserID { get; set; }

        [Required, Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required, Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { set; get; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number", Description = "Primary phone number")]
        public int? PhoneNo { set; get; }

        //todo maybe add additional phonenos or change the one already created to collection and mark one as primary

        [Display(Name = "Sex")]
        public bool? isFemale { set; get; }
        
        [Display(Name = "Birthday"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }

       // [StringLength(100, ErrorMessage = "Location cannot be longer than 100 characters.")]
        public int? LocationID { set; get; }

        //todo add cv

        /*It doesnt need to hold the account as it is independent and in different use cases(domain)*/
        //public virtual JobSeekerUser JobSeekerUser { get; set; }

        public virtual Location Location { get; set; }

        public virtual ICollection<Education> Educations { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }

        public virtual ICollection<Certificate> Certificates { get; set; } 
        
       // public virtual ICollection<Contact> Contacts { set; get; }
        
    }

    public class Company
    {
        public int ID { get; set; }

        /*Same as jobseeker case*/
        //public Guid CompanyUserID { get; set; }

        [Required, StringLength(100 ,ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { set; get; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { set; get; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number", Description = "Primary phone number")]
        public int? PhoneNo { set; get; }

        //todo check if both stringlength and multilinetext are necessary?
        [StringLength(300, ErrorMessage = "Description cannot be longer than 300 characters."), DataType(DataType.MultilineText)]
        public string Description { set; get; }
        
        /*same as jobseeker navigation canceling*/
        //public virtual CompanyUser CompanyUser { get; set; }

        public virtual ICollection<Job> Jobs { set; get; }

        public virtual ICollection<Location> Locations { set; get; }
    }


}