namespace Inisra_Web_App_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationandInvitationfix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Applications", "JobSeeker_ID", "dbo.JobSeekers");
            DropForeignKey("dbo.Invitations", "JobSeeker_ID", "dbo.JobSeekers");
            DropIndex("dbo.Applications", new[] { "JobSeeker_ID" });
            DropIndex("dbo.Invitations", new[] { "JobSeeker_ID" });
            DropColumn("dbo.Applications", "JobSeeker_ID");
            DropColumn("dbo.Invitations", "JobSeeker_ID");

            DropPrimaryKey("dbo.Applications");
            DropPrimaryKey("dbo.Invitations");
            AddColumn("dbo.Skills", "Name", c => c.String(nullable: false, maxLength: 50));
           // AlterColumn("dbo.Applications", "JobSeekerID", c => c.Int(nullable: false));
           // AlterColumn("dbo.Applications", "JobSeekerID", c => c.Int(nullable: false));
           // AlterColumn("dbo.Invitations", "JobSeekerID", c => c.Int(nullable: false));
           // AlterColumn("dbo.Invitations", "JobSeekerID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Applications", new[] { "JobSeekerID", "JobID" });
            AddPrimaryKey("dbo.Invitations", new[] { "JobID", "JobSeekerID" });
            CreateIndex("dbo.Applications", "JobSeekerID");
            CreateIndex("dbo.Invitations", "JobSeekerID");
            AddForeignKey("dbo.Applications", "JobSeekerID", "dbo.JobSeekers", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Invitations", "JobSeekerID", "dbo.JobSeekers", "ID", cascadeDelete: true);
            DropColumn("dbo.Skills", "SkillName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skills", "SkillName", c => c.String(nullable: false, maxLength: 50));
            DropForeignKey("dbo.Invitations", "JobSeekerID", "dbo.JobSeekers");
            DropForeignKey("dbo.Applications", "JobSeekerID", "dbo.JobSeekers");
            DropIndex("dbo.Invitations", new[] { "JobSeekerID" });
            DropIndex("dbo.Applications", new[] { "JobSeekerID" });
            DropPrimaryKey("dbo.Invitations");
            DropPrimaryKey("dbo.Applications");
            AlterColumn("dbo.Invitations", "JobSeekerID", c => c.Int());
            AlterColumn("dbo.Invitations", "JobSeekerID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Applications", "JobSeekerID", c => c.Int());
            AlterColumn("dbo.Applications", "JobSeekerID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Skills", "Name");
            AddPrimaryKey("dbo.Invitations", "JobSeekerID");
            AddPrimaryKey("dbo.Applications", "JobSeekerID");
            RenameColumn(table: "dbo.Invitations", name: "JobSeekerID", newName: "JobSeeker_ID");
            RenameColumn(table: "dbo.Applications", name: "JobSeekerID", newName: "JobSeeker_ID");
            AddColumn("dbo.Invitations", "JobSeekerID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Applications", "JobSeekerID", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Invitations", "JobSeeker_ID");
            CreateIndex("dbo.Applications", "JobSeeker_ID");
            AddForeignKey("dbo.Invitations", "JobSeeker_ID", "dbo.JobSeekers", "ID");
            AddForeignKey("dbo.Applications", "JobSeeker_ID", "dbo.JobSeekers", "ID");
        }
    }
}
