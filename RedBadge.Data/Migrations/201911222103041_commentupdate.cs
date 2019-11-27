namespace RedBadge.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentupdate : DbMigration
    {
        public override void Up()
        {
            //RenameTable(name: "dbo.ProfileTeam", newName: "TeamProfile");
            //DropPrimaryKey("dbo.ProfileTeam");
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        UserID = c.Guid(nullable: false),
                        ProfileID = c.Int(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                        DateSent = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Profile", t => t.ProfileID, cascadeDelete: true)
                .Index(t => t.ProfileID);
            
            //AddPrimaryKey("dbo.TeamProfile", new[] { "Team_TeamID", "Profile_ProfileID" });
            DropColumn("dbo.Profile", "Comment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profile", "Comment", c => c.String());
            DropForeignKey("dbo.Comment", "ProfileID", "dbo.Profile");
            DropIndex("dbo.Comment", new[] { "ProfileID" });
            //DropPrimaryKey("dbo.TeamProfile");
            DropTable("dbo.Comment");
            //AddPrimaryKey("dbo.ProfileTeam", new[] { "Profile_ProfileID", "Team_TeamID" });
            //RenameTable(name: "dbo.TeamProfile", newName: "ProfileTeam");
        }
    }
}
