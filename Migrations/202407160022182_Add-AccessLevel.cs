namespace PokerStatBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAccessLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "accessLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "accessLevel");
        }
    }
}
