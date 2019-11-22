namespace RedBadge.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentupdate2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comment", "ProfileID", "dbo.Profile");
            DropIndex("dbo.Comment", new[] { "ProfileID" });
            AlterColumn("dbo.Comment", "ProfileID", c => c.Int());
            CreateIndex("dbo.Comment", "ProfileID");
            AddForeignKey("dbo.Comment", "ProfileID", "dbo.Profile", "ProfileID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "ProfileID", "dbo.Profile");
            DropIndex("dbo.Comment", new[] { "ProfileID" });
            AlterColumn("dbo.Comment", "ProfileID", c => c.Int(nullable: false));
            CreateIndex("dbo.Comment", "ProfileID");
            AddForeignKey("dbo.Comment", "ProfileID", "dbo.Profile", "ProfileID", cascadeDelete: true);
        }
    }
}
