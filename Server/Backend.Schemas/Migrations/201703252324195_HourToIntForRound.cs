namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HourToIntForRound : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Rounds", "RoundHour");
            AddColumn("dbo.Rounds", "RoundHour", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rounds", "RoundHour", c => c.DateTime(nullable: false));
        }
    }
}
