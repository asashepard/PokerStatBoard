namespace PokerStatBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCurrentGame : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurrentGameModels",
                c => new
                    {
                        CurrentGameID = c.Guid(nullable: false),
                        PokerGameID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CurrentGameID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CurrentGameModels");
        }
    }
}
