namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NonUniqueRegionOnRound : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Rounds", new[] { "RegionName" });
            CreateIndex("dbo.Rounds", "RegionName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Rounds", new[] { "RegionName" });
            CreateIndex("dbo.Rounds", "RegionName");
        }
    }
}
