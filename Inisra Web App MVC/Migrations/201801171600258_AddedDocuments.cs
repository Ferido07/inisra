namespace Inisra_Web_App_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDocuments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CVs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobSeekerID = c.Int(nullable: false),
                        Document = c.Binary(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JobSeekers", t => t.JobSeekerID, cascadeDelete: true)
                .Index(t => t.JobSeekerID);
            
            CreateTable(
                "dbo.JobDescriptions",
                c => new
                    {
                        JobID = c.Int(nullable: false),
                        Document = c.Binary(),
                    })
                .PrimaryKey(t => t.JobID)
                .ForeignKey("dbo.Jobs", t => t.JobID)
                .Index(t => t.JobID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobDescriptions", "JobID", "dbo.Jobs");
            DropForeignKey("dbo.CVs", "JobSeekerID", "dbo.JobSeekers");
            DropIndex("dbo.JobDescriptions", new[] { "JobID" });
            DropIndex("dbo.CVs", new[] { "JobSeekerID" });
            DropTable("dbo.JobDescriptions");
            DropTable("dbo.CVs");
        }
    }
}
