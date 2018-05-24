namespace Inisra_Web_App_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameSavedJobsToBookmarkedJobs : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.JobSeekerSavedJobs", newName: "BookmarkedJobs");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.BookmarkedJobs", newName: "JobSeekerSavedJobs");
        }
    }
}
