namespace Inisra_Web_App_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredInstitutionName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Institutions", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Institutions", "Name", c => c.String(maxLength: 100));
        }
    }
}
