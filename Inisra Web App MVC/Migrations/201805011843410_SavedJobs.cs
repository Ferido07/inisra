namespace Inisra_Web_App_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SavedJobs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobSeekerSavedJobs",
                c => new
                    {
                        JobSeekerID = c.Int(nullable: false),
                        JobID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.JobSeekerID, t.JobID })
                .ForeignKey("dbo.JobSeekers", t => t.JobSeekerID, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.JobID, cascadeDelete: true)
                .Index(t => t.JobSeekerID)
                .Index(t => t.JobID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobSeekerSavedJobs", "JobID", "dbo.Jobs");
            DropForeignKey("dbo.JobSeekerSavedJobs", "JobSeekerID", "dbo.JobSeekers");
            DropIndex("dbo.JobSeekerSavedJobs", new[] { "JobID" });
            DropIndex("dbo.JobSeekerSavedJobs", new[] { "JobSeekerID" });
            DropTable("dbo.JobSeekerSavedJobs");
        }
    }
}
