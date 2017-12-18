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
        [StringLength(40, ErrorMessage = "First name cannot be longer than 40 characters.")]
        public string FirstName { get; set; }

        [Required, Display(Name = "Last Name")]
        [StringLength(40, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { set; get; }

        [Required, DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number", Description = "Primary phone number")]
        public int PhoneNo { set; get; }

        //todo maybe add additional phonenos or change the one already created to collection and mark one as primary

        [Display(Name = "Sex")]
        public Boolean? isFemale { set; get; }
        
        [Display(Name = "Birthday"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }

        public string Address { set; get; }

        //todo add cv

        /*It doesnt need to hold the account as it is independent and in different use cases(domain)*/
        //public virtual JobSeekerUser JobSeekerUser { get; set; }

        public virtual List<Education> Educations { get; set; }

        public virtual List<Skill> Skills { get; set; }

        public virtual List<Certificate> Certificates { get; set; } 
        
       // public virtual ICollection<Contact> Contacts { set; get; }
        
    }

    public class Company
    {
        public int ID { get; set; }

        /*Same as jobseeker case*/
        //public Guid CompanyUserID { get; set; }

        [Required, StringLength(50)]
        public string Name { set; get; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { set; get; }

        [Required, DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number", Description = "Primary phone number")]
        public int PhoneNo { set; get; }

        //todo check if both stringlength and multilinetext are necessary?
        [StringLength(250), DataType(DataType.MultilineText)]
        public string Description { set; get; }
        
        /*same as jobseeker navigation canceling*/
        //public virtual CompanyUser CompanyUser { get; set; }

        public ICollection<Job> Jobs { set; get; }


    }
}