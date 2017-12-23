namespace Inisra_Web_App_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobSeekerEducation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Educations", "EducationLevelID", "dbo.EducationLevels");
            DropForeignKey("dbo.EducationInstitutions", "Education_ID", "dbo.Educations");
            DropForeignKey("dbo.EducationInstitutions", "Institution_ID", "dbo.Institutions");
            DropForeignKey("dbo.Educations", "JobSeekerID", "dbo.JobSeekers");
            DropIndex("dbo.Educations", new[] { "JobSeekerID" });
            DropIndex("dbo.Educations", new[] { "EducationLevelID" });
            DropIndex("dbo.EducationInstitutions", new[] { "Education_ID" });
            DropIndex("dbo.EducationInstitutions", new[] { "Institution_ID" });
            CreateTable(
                "dbo.JobSeekerEducations",
                c => new
                    {
                        JobSeekerID = c.Int(nullable: false),
                        EducationID = c.Int(nullable: false),
                        InstitutionID = c.Int(),
                        StartDate = c.DateTime(),
                        CompletionDate = c.DateTime(),
                        EducationType = c.Int(),
                    })
                .PrimaryKey(t => new { t.JobSeekerID, t.EducationID })
                .ForeignKey("dbo.Educations", t => t.EducationID, cascadeDelete: true)
                .ForeignKey("dbo.Institutions", t => t.InstitutionID)
                .ForeignKey("dbo.JobSeekers", t => t.JobSeekerID, cascadeDelete: true)
                .Index(t => t.JobSeekerID)
                .Index(t => t.EducationID)
                .Index(t => t.InstitutionID);
            
            AlterColumn("dbo.Educations", "Name", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.Educations", "Name", unique: true);
            DropColumn("dbo.Educations", "JobSeekerID");
            DropColumn("dbo.Educations", "EducationLevelID");
            DropColumn("dbo.Educations", "EducationType");
            DropColumn("dbo.Educations", "DateCompleted");
            DropTable("dbo.EducationLevels");
            DropTable("dbo.EducationInstitutions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EducationInstitutions",
                c => new
                    {
                        Education_ID = c.Int(nullable: false),
                        Institution_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Education_ID, t.Institution_ID });
            
            CreateTable(
                "dbo.EducationLevels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ShorthandNotation = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Educations", "DateCompleted", c => c.DateTime());
            AddColumn("dbo.Educations", "EducationType", c => c.Int());
            AddColumn("dbo.Educations", "EducationLevelID", c => c.Int(nullable: false));
            AddColumn("dbo.Educations", "JobSeekerID", c => c.Int(nullable: false));
            DropForeignKey("dbo.JobSeekerEducations", "JobSeekerID", "dbo.JobSeekers");
            DropForeignKey("dbo.JobSeekerEducations", "InstitutionID", "dbo.Institutions");
            DropForeignKey("dbo.JobSeekerEducations", "EducationID", "dbo.Educations");
            DropIndex("dbo.Educations", new[] { "Name" });
            DropIndex("dbo.JobSeekerEducations", new[] { "InstitutionID" });
            DropIndex("dbo.JobSeekerEducations", new[] { "EducationID" });
            DropIndex("dbo.JobSeekerEducations", new[] { "JobSeekerID" });
            AlterColumn("dbo.Educations", "Name", c => c.String(maxLength: 60));
            DropTable("dbo.JobSeekerEducations");
            CreateIndex("dbo.EducationInstitutions", "Institution_ID");
            CreateIndex("dbo.EducationInstitutions", "Education_ID");
            CreateIndex("dbo.Educations", "EducationLevelID");
            CreateIndex("dbo.Educations", "JobSeekerID");
            AddForeignKey("dbo.Educations", "JobSeekerID", "dbo.JobSeekers", "ID", cascadeDelete: true);
            AddForeignKey("dbo.EducationInstitutions", "Institution_ID", "dbo.Institutions", "ID", cascadeDelete: true);
            AddForeignKey("dbo.EducationInstitutions", "Education_ID", "dbo.Educations", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Educations", "EducationLevelID", "dbo.EducationLevels", "ID", cascadeDelete: true);
        }
    }
}
