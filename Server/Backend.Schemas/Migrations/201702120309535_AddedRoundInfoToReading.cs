namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRoundInfoToReading : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Readings", "RoundHour", c => c.DateTime(nullable: false));
            AddColumn("dbo.Readings", "RegionName", c => c.String(maxLength: 128));
            CreateIndex("dbo.Readings", new[] { "RoundHour", "RegionName" });
            AddForeignKey("dbo.Readings", new[] { "RoundHour", "RegionName" }, "dbo.Rounds", new[] { "RoundHour", "RegionName" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Readings", new[] { "RoundHour", "RegionName" }, "dbo.Rounds");
            DropIndex("dbo.Readings", new[] { "RoundHour", "RegionName" });
            DropColumn("dbo.Readings", "RegionName");
            DropColumn("dbo.Readings", "RoundHour");
        }
    }
}
