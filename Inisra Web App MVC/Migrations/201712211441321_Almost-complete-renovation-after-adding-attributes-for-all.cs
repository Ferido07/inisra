namespace Inisra_Web_App_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Almostcompleterenovationafteraddingattributesforall : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Educations", "Institution_ID", "dbo.Institutions");
            DropForeignKey("dbo.Skills", "JobSeekerID", "dbo.JobSeekers");
            DropForeignKey("dbo.Applications", "JobSeekerID", "dbo.JobSeekers");
            DropForeignKey("dbo.Invitations", "JobSeekerID", "dbo.JobSeekers");
            DropIndex("dbo.Applications", new[] { "JobSeekerID" });
            DropIndex("dbo.Educations", new[] { "Institution_ID" });
            DropIndex("dbo.Skills", new[] { "JobSeekerID" });
            DropIndex("dbo.Invitations", new[] { "JobSeekerID" });
            DropPrimaryKey("dbo.Applications");
            DropPrimaryKey("dbo.Invitations");
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EducationInstitutions",
                c => new
                    {
                        Education_ID = c.Int(nullable: false),
                        Institution_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Education_ID, t.Institution_ID })
                .ForeignKey("dbo.Educations", t => t.Education_ID, cascadeDelete: true)
                .ForeignKey("dbo.Institutions", t => t.Institution_ID, cascadeDelete: true)
                .Index(t => t.Education_ID)
                .Index(t => t.Institution_ID);
            
            CreateTable(
                "dbo.JobSeekerSkills",
                c => new
                    {
                        JobSeekerID = c.Int(nullable: false),
                        SkillID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.JobSeekerID, t.SkillID })
                .ForeignKey("dbo.JobSeekers", t => t.JobSeekerID, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillID, cascadeDelete: true)
                .Index(t => t.JobSeekerID)
                .Index(t => t.SkillID);
            
            CreateTable(
                "dbo.InstitutionLocations",
                c => new
                    {
                        InstitutionID = c.Int(nullable: false),
                        LocationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.InstitutionID, t.LocationID })
                .ForeignKey("dbo.Institutions", t => t.InstitutionID, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LocationID, cascadeDelete: true)
                .Index(t => t.InstitutionID)
                .Index(t => t.LocationID);
            
            CreateTable(
                "dbo.CompanyLocations",
                c => new
                    {
                        CompanyID = c.Int(nullable: false),
                        LocationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CompanyID, t.LocationID })
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LocationID, cascadeDelete: true)
                .Index(t => t.CompanyID)
                .Index(t => t.LocationID);
            
            AddColumn("dbo.Applications", "JobSeeker_ID", c => c.Int());
            AddColumn("dbo.Jobs", "Profession", c => c.String(maxLength: 50));
            AddColumn("dbo.Jobs", "Salary", c => c.Int());
            AddColumn("dbo.Jobs", "SalaryCurrency", c => c.Int());
            AddColumn("dbo.Jobs", "SalaryRate", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "LocationID", c => c.Int());
            AddColumn("dbo.JobSeekers", "LocationID", c => c.Int());
            AddColumn("dbo.Educations", "EducationType", c => c.Int());
            AddColumn("dbo.Skills", "SkillName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Invitations", "JobSeeker_ID", c => c.Int());
            AlterColumn("dbo.Applications", "JobSeekerID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Jobs", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Companies", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Companies", "PhoneNo", c => c.Int());
            AlterColumn("dbo.Companies", "Description", c => c.String(maxLength: 300));
            AlterColumn("dbo.JobSeekers", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.JobSeekers", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.JobSeekers", "PhoneNo", c => c.Int());
            AlterColumn("dbo.Certificates", "Name", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Certificates", "CertificateIssuer", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Certificates", "IssueDate", c => c.DateTime());
            AlterColumn("dbo.Certificates", "Description", c => c.String(maxLength: 250));
            AlterColumn("dbo.Educations", "Name", c => c.String(maxLength: 60));
            AlterColumn("dbo.Educations", "DateCompleted", c => c.DateTime());
            AlterColumn("dbo.EducationLevels", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Institutions", "Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.Institutions", "About", c => c.String(maxLength: 300));
            AlterColumn("dbo.Invitations", "JobSeekerID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Applications", "JobSeekerID");
            AddPrimaryKey("dbo.Invitations", "JobSeekerID");
            CreateIndex("dbo.Applications", "JobSeeker_ID");
            CreateIndex("dbo.Jobs", "LocationID");
            CreateIndex("dbo.JobSeekers", "LocationID");
            CreateIndex("dbo.Invitations", "JobSeeker_ID");
            AddForeignKey("dbo.JobSeekers", "LocationID", "dbo.Locations", "ID");
            AddForeignKey("dbo.Jobs", "LocationID", "dbo.Locations", "ID");
            AddForeignKey("dbo.Applications", "JobSeeker_ID", "dbo.JobSeekers", "ID");
            AddForeignKey("dbo.Invitations", "JobSeeker_ID", "dbo.JobSeekers", "ID");
            DropColumn("dbo.Applications", "ID");
            DropColumn("dbo.Jobs", "Location");
            DropColumn("dbo.JobSeekers", "Address");
            DropColumn("dbo.Educations", "Type");
            DropColumn("dbo.Educations", "Institution_ID");
            DropColumn("dbo.Institutions", "Location");
            DropColumn("dbo.Skills", "JobSeekerID");
            DropColumn("dbo.Skills", "Name");
            DropColumn("dbo.Invitations", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invitations", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Skills", "Name", c => c.String());
            AddColumn("dbo.Skills", "JobSeekerID", c => c.Int(nullable: false));
            AddColumn("dbo.Institutions", "Location", c => c.String());
            AddColumn("dbo.Educations", "Institution_ID", c => c.Int());
            AddColumn("dbo.Educations", "Type", c => c.Int());
            AddColumn("dbo.JobSeekers", "Address", c => c.String());
            AddColumn("dbo.Jobs", "Location", c => c.String());
            AddColumn("dbo.Applications", "ID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Invitations", "JobSeeker_ID", "dbo.JobSeekers");
            DropForeignKey("dbo.Applications", "JobSeeker_ID", "dbo.JobSeekers");
            DropForeignKey("dbo.CompanyLocations", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.CompanyLocations", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.Jobs", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.InstitutionLocations", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.InstitutionLocations", "InstitutionID", "dbo.Institutions");
            DropForeignKey("dbo.JobSeekerSkills", "SkillID", "dbo.Skills");
            DropForeignKey("dbo.JobSeekerSkills", "JobSeekerID", "dbo.JobSeekers");
            DropForeignKey("dbo.JobSeekers", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.EducationInstitutions", "Institution_ID", "dbo.Institutions");
            DropForeignKey("dbo.EducationInstitutions", "Education_ID", "dbo.Educations");
            DropIndex("dbo.CompanyLocations", new[] { "LocationID" });
            DropIndex("dbo.CompanyLocations", new[] { "CompanyID" });
            DropIndex("dbo.InstitutionLocations", new[] { "LocationID" });
            DropIndex("dbo.InstitutionLocations", new[] { "InstitutionID" });
            DropIndex("dbo.JobSeekerSkills", new[] { "SkillID" });
            DropIndex("dbo.JobSeekerSkills", new[] { "JobSeekerID" });
            DropIndex("dbo.EducationInstitutions", new[] { "Institution_ID" });
            DropIndex("dbo.EducationInstitutions", new[] { "Education_ID" });
            DropIndex("dbo.Invitations", new[] { "JobSeeker_ID" });
            DropIndex("dbo.JobSeekers", new[] { "LocationID" });
            DropIndex("dbo.Jobs", new[] { "LocationID" });
            DropIndex("dbo.Applications", new[] { "JobSeeker_ID" });
            DropPrimaryKey("dbo.Invitations");
            DropPrimaryKey("dbo.Applications");
            AlterColumn("dbo.Invitations", "JobSeekerID", c => c.Int(nullable: false));
            AlterColumn("dbo.Institutions", "About", c => c.String());
            AlterColumn("dbo.Institutions", "Name", c => c.String());
            AlterColumn("dbo.EducationLevels", "Name", c => c.String());
            AlterColumn("dbo.Educations", "DateCompleted", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Educations", "Name", c => c.String());
            AlterColumn("dbo.Certificates", "Description", c => c.String());
            AlterColumn("dbo.Certificates", "IssueDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Certificates", "CertificateIssuer", c => c.String());
            AlterColumn("dbo.Certificates", "Name", c => c.String());
            AlterColumn("dbo.JobSeekers", "PhoneNo", c => c.Int(nullable: false));
            AlterColumn("dbo.JobSeekers", "LastName", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.JobSeekers", "FirstName", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Companies", "Description", c => c.String(maxLength: 250));
            AlterColumn("dbo.Companies", "PhoneNo", c => c.Int(nullable: false));
            AlterColumn("dbo.Companies", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Jobs", "Title", c => c.String());
            AlterColumn("dbo.Applications", "JobSeekerID", c => c.Int(nullable: false));
            DropColumn("dbo.Invitations", "JobSeeker_ID");
            DropColumn("dbo.Skills", "SkillName");
            DropColumn("dbo.Educations", "EducationType");
            DropColumn("dbo.JobSeekers", "LocationID");
            DropColumn("dbo.Jobs", "LocationID");
            DropColumn("dbo.Jobs", "SalaryRate");
            DropColumn("dbo.Jobs", "SalaryCurrency");
            DropColumn("dbo.Jobs", "Salary");
            DropColumn("dbo.Jobs", "Profession");
            DropColumn("dbo.Applications", "JobSeeker_ID");
            DropTable("dbo.CompanyLocations");
            DropTable("dbo.InstitutionLocations");
            DropTable("dbo.JobSeekerSkills");
            DropTable("dbo.EducationInstitutions");
            DropTable("dbo.Locations");
            AddPrimaryKey("dbo.Invitations", "ID");
            AddPrimaryKey("dbo.Applications", "ID");
            CreateIndex("dbo.Invitations", "JobSeekerID");
            CreateIndex("dbo.Skills", "JobSeekerID");
            CreateIndex("dbo.Educations", "Institution_ID");
            CreateIndex("dbo.Applications", "JobSeekerID");
            AddForeignKey("dbo.Invitations", "JobSeekerID", "dbo.JobSeekers", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Applications", "JobSeekerID", "dbo.JobSeekers", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Skills", "JobSeekerID", "dbo.JobSeekers", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Educations", "Institution_ID", "dbo.Institutions", "ID");
        }
    }
}
