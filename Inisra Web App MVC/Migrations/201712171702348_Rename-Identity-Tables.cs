namespace Inisra_Web_App_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameIdentityTables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetRoles", newName: "InisraRoles");
            RenameTable(name: "dbo.AspNetUserRoles", newName: "InisraUserRoles");
            RenameTable(name: "dbo.AspNetUsers", newName: "InisraUsers");
            RenameTable(name: "dbo.AspNetUserClaims", newName: "InisraUserClaims");
            RenameTable(name: "dbo.AspNetUserLogins", newName: "InisraUserLogins");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.InisraUserLogins", newName: "AspNetUserLogins");
            RenameTable(name: "dbo.InisraUserClaims", newName: "AspNetUserClaims");
            RenameTable(name: "dbo.InisraUsers", newName: "AspNetUsers");
            RenameTable(name: "dbo.InisraUserRoles", newName: "AspNetUserRoles");
            RenameTable(name: "dbo.InisraRoles", newName: "AspNetRoles");
        }
    }
}
