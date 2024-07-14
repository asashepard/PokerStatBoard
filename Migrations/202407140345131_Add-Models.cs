namespace PokerStatBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuyInModels",
                c => new
                    {
                        BuyInID = c.Guid(nullable: false),
                        PokerGameID = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BuyInID);
            
            CreateTable(
                "dbo.CashOutModels",
                c => new
                    {
                        CashOutID = c.Guid(nullable: false),
                        PokerGameID = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CashOutID);
            
            CreateTable(
                "dbo.PlayerModels",
                c => new
                    {
                        PlayerID = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerID);
            
            CreateTable(
                "dbo.PokerGameModels",
                c => new
                    {
                        PokerGameID = c.Guid(nullable: false),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PokerGameID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PokerGameModels");
            DropTable("dbo.PlayerModels");
            DropTable("dbo.CashOutModels");
            DropTable("dbo.BuyInModels");
        }
    }
}
