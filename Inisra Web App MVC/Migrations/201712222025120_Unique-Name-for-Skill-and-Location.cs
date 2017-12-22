namespace Inisra_Web_App_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueNameforSkillandLocation : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Locations", "Name", unique: true);
            CreateIndex("dbo.Skills", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Skills", new[] { "Name" });
            DropIndex("dbo.Locations", new[] { "Name" });
        }
    }
}
