namespace Inisra_Web_App_MVC.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DocumentValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CVs", "Document", c => c.Binary(nullable: false));
            AlterColumn("dbo.JobDescriptions", "Document", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobDescriptions", "Document", c => c.Binary());
            AlterColumn("dbo.CVs", "Document", c => c.Binary());
        }
    }
}
