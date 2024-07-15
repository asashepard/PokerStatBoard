namespace PokerStatBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsPlaying : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlayerModels", "IsPlaying", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlayerModels", "IsPlaying");
        }
    }
}
