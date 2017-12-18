namespace Inisra_Web_App_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddescriptioncolumnforcompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Description", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "Description");
        }
    }
}
