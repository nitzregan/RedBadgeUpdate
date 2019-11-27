namespace RedBadge.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Teammessagedata : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Profile", "TeamMessaging_MessageID", "dbo.TeamMessaging");
            DropIndex("dbo.Profile", new[] { "TeamMessaging_MessageID" });
            DropColumn("dbo.Profile", "TeamMessaging_MessageID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profile", "TeamMessaging_MessageID", c => c.Int());
            CreateIndex("dbo.Profile", "TeamMessaging_MessageID");
            AddForeignKey("dbo.Profile", "TeamMessaging_MessageID", "dbo.TeamMessaging", "MessageID");
        }
    }
}
