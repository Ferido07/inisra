namespace Inisra_Web_App_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobSeekers", "IsFemale", c => c.Boolean());
            DropColumn("dbo.JobSeekers", "Sex");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobSeekers", "Sex", c => c.String());
            DropColumn("dbo.JobSeekers", "IsFemale");
        }
    }
}
