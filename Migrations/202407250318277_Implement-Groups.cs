namespace PokerStatBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImplementGroups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppUserGroupModels",
                c => new
                    {
                        ApplicationUserGroupID = c.Guid(nullable: false),
                        ApplicationUserID = c.Guid(nullable: false),
                        GroupID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicationUserGroupID);
            
            CreateTable(
                "dbo.GroupModels",
                c => new
                    {
                        GroupID = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        PokerGameID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.GroupID);
            
            AddColumn("dbo.PlayerModels", "GroupID", c => c.Guid(nullable: false));
            DropTable("dbo.CurrentGameModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CurrentGameModels",
                c => new
                    {
                        CurrentGameID = c.Guid(nullable: false),
                        PokerGameID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CurrentGameID);
            
            DropColumn("dbo.PlayerModels", "GroupID");
            DropTable("dbo.GroupModels");
            DropTable("dbo.AppUserGroupModels");
        }
    }
}
