namespace PokerStatBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReversePlayerActionRelations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BuyInModels", "PlayerID", c => c.Guid(nullable: false));
            AddColumn("dbo.CashOutModels", "PlayerID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CashOutModels", "PlayerID");
            DropColumn("dbo.BuyInModels", "PlayerID");
        }
    }
}
