namespace Inisra_Web_App_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedDocumentClasses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CVs", "DocumentName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.CVs", "DocumentType", c => c.Int(nullable: false));
            AddColumn("dbo.CVs", "ValidityConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.CVs", "LastUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.JobDescriptions", "DocumentName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.JobDescriptions", "DocumentType", c => c.Int(nullable: false));
            AddColumn("dbo.JobDescriptions", "ValidityConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.JobDescriptions", "LastUpdated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobDescriptions", "LastUpdated");
            DropColumn("dbo.JobDescriptions", "ValidityConfirmed");
            DropColumn("dbo.JobDescriptions", "DocumentType");
            DropColumn("dbo.JobDescriptions", "DocumentName");
            DropColumn("dbo.CVs", "LastUpdated");
            DropColumn("dbo.CVs", "ValidityConfirmed");
            DropColumn("dbo.CVs", "DocumentType");
            DropColumn("dbo.CVs", "DocumentName");
        }
    }
}
