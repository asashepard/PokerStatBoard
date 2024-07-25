namespace PokerStatBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveAccessLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUserGroupModels", "AccessLevel", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "accessLevel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "accessLevel", c => c.Int(nullable: false));
            DropColumn("dbo.AppUserGroupModels", "AccessLevel");
        }
    }
}
