namespace Inisra_Web_App_MVC.IdentityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId });
            
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
                "dbo.JobSeekerUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        JobSeekerID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobSeekers", t => t.JobSeekerID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.JobSeekerID);
            
            CreateTable(
                "dbo.CompanyUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        CompanyID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CompanyUsers", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.JobSeekerUsers", "JobSeekerID", "dbo.JobSeekers");
            DropForeignKey("dbo.Skills", "JobSeekerID", "dbo.JobSeekers");
            DropForeignKey("dbo.Educations", "JobSeekerID", "dbo.JobSeekers");
            DropForeignKey("dbo.Educations", "Institution_ID", "dbo.Institutions");
            DropForeignKey("dbo.Educations", "EducationLevelID", "dbo.EducationLevels");
            DropForeignKey("dbo.Certificates", "JobSeekerID", "dbo.JobSeekers");
            DropForeignKey("dbo.Jobs", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.Administrators", "UserNameIndex");
            DropIndex("dbo.CompanyUsers", new[] { "CompanyID" });
            DropIndex("dbo.CompanyUsers", "UserNameIndex");
            DropIndex("dbo.JobSeekerUsers", new[] { "JobSeekerID" });
            DropIndex("dbo.JobSeekerUsers", "UserNameIndex");
            DropIndex("dbo.Skills", new[] { "JobSeekerID" });
            DropIndex("dbo.Educations", new[] { "Institution_ID" });
            DropIndex("dbo.Educations", new[] { "EducationLevelID" });
            DropIndex("dbo.Educations", new[] { "JobSeekerID" });
            DropIndex("dbo.Certificates", new[] { "JobSeekerID" });
            DropIndex("dbo.Jobs", new[] { "CompanyID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.Administrators");
            DropTable("dbo.CompanyUsers");
            DropTable("dbo.JobSeekerUsers");
            DropTable("dbo.Skills");
            DropTable("dbo.Institutions");
            DropTable("dbo.EducationLevels");
            DropTable("dbo.Educations");
            DropTable("dbo.Certificates");
            DropTable("dbo.JobSeekers");
            DropTable("dbo.Jobs");
            DropTable("dbo.Companies");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
