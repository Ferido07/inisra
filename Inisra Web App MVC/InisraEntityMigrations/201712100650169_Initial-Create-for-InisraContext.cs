namespace Inisra_Web_App_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreateforInisraContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobSeekerID = c.Int(nullable: false),
                        JobID = c.Int(nullable: false),
                        ApplicationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Jobs", t => t.JobID, cascadeDelete: true)
                .ForeignKey("dbo.JobSeekers", t => t.JobSeekerID, cascadeDelete: true)
                .Index(t => t.JobSeekerID)
                .Index(t => t.JobID);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompanyID = c.Int(nullable: false),
                        Title = c.String(),
                        isOpen = c.Boolean(nullable: false),
                        isInvitationOnly = c.Boolean(nullable: false),
                        Location = c.String(),
                        PostDate = c.DateTime(nullable: false),
                        ApplicationDeadlineDate = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        PhoneNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.JobSeekers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 40),
                        LastName = c.String(nullable: false, maxLength: 40),
                        Email = c.String(nullable: false),
                        PhoneNo = c.Int(nullable: false),
                        isFemale = c.Boolean(),
                        Birthday = c.DateTime(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Certificates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobSeekerID = c.Int(nullable: false),
                        Name = c.String(),
                        CertificateIssuer = c.String(),
                        IssueDate = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JobSeekers", t => t.JobSeekerID, cascadeDelete: true)
                .Index(t => t.JobSeekerID);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobSeekerID = c.Int(nullable: false),
                        Name = c.String(),
                        EducationLevelID = c.Int(nullable: false),
                        Type = c.Int(),
                        DateCompleted = c.DateTime(nullable: false),
                        Institution_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EducationLevels", t => t.EducationLevelID, cascadeDelete: true)
                .ForeignKey("dbo.Institutions", t => t.Institution_ID)
                .ForeignKey("dbo.JobSeekers", t => t.JobSeekerID, cascadeDelete: true)
                .Index(t => t.JobSeekerID)
                .Index(t => t.EducationLevelID)
                .Index(t => t.Institution_ID);
            
            CreateTable(
                "dbo.EducationLevels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShorthandNotation = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Institutions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.String(),
                        About = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobSeekerID = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JobSeekers", t => t.JobSeekerID, cascadeDelete: true)
                .Index(t => t.JobSeekerID);
            
            CreateTable(
                "dbo.Invitations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobID = c.Int(nullable: false),
                        JobSeekerID = c.Int(nullable: false),
                        InvitationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Jobs", t => t.JobID, cascadeDelete: true)
                .ForeignKey("dbo.JobSeekers", t => t.JobSeekerID, cascadeDelete: true)
                .Index(t => t.JobID)
                .Index(t => t.JobSeekerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invitations", "JobSeekerID", "dbo.JobSeekers");
            DropForeignKey("dbo.Invitations", "JobID", "dbo.Jobs");
            DropForeignKey("dbo.Applications", "JobSeekerID", "dbo.JobSeekers");
            DropForeignKey("dbo.Skills", "JobSeekerID", "dbo.JobSeekers");
            DropForeignKey("dbo.Educations", "JobSeekerID", "dbo.JobSeekers");
            DropForeignKey("dbo.Educations", "Institution_ID", "dbo.Institutions");
            DropForeignKey("dbo.Educations", "EducationLevelID", "dbo.EducationLevels");
            DropForeignKey("dbo.Certificates", "JobSeekerID", "dbo.JobSeekers");
            DropForeignKey("dbo.Applications", "JobID", "dbo.Jobs");
            DropForeignKey("dbo.Jobs", "CompanyID", "dbo.Companies");
            DropIndex("dbo.Invitations", new[] { "JobSeekerID" });
            DropIndex("dbo.Invitations", new[] { "JobID" });
            DropIndex("dbo.Skills", new[] { "JobSeekerID" });
            DropIndex("dbo.Educations", new[] { "Institution_ID" });
            DropIndex("dbo.Educations", new[] { "EducationLevelID" });
            DropIndex("dbo.Educations", new[] { "JobSeekerID" });
            DropIndex("dbo.Certificates", new[] { "JobSeekerID" });
            DropIndex("dbo.Jobs", new[] { "CompanyID" });
            DropIndex("dbo.Applications", new[] { "JobID" });
            DropIndex("dbo.Applications", new[] { "JobSeekerID" });
            DropTable("dbo.Invitations");
            DropTable("dbo.Skills");
            DropTable("dbo.Institutions");
            DropTable("dbo.EducationLevels");
            DropTable("dbo.Educations");
            DropTable("dbo.Certificates");
            DropTable("dbo.JobSeekers");
            DropTable("dbo.Companies");
            DropTable("dbo.Jobs");
            DropTable("dbo.Applications");
        }
    }
}
