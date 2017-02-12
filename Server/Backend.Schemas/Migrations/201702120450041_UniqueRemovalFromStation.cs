namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueRemovalFromStation : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Stations", new[] { "Name" });
            DropIndex("dbo.Stations", new[] { "RegionName" });
            CreateIndex("dbo.Stations", "Name");
            CreateIndex("dbo.Stations", "RegionName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Stations", new[] { "RegionName" });
            DropIndex("dbo.Stations", new[] { "Name" });
            CreateIndex("dbo.Stations", "RegionName");
            CreateIndex("dbo.Stations", "Name", unique: true);
        }
    }
}
