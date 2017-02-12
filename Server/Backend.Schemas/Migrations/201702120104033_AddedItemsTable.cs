namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedItemsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        StationName = c.String(nullable: false, maxLength: 128),
                        RegionName = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 128),
                        Meter = c.String(),
                    })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.Stations", t => new { t.StationName, t.RegionName }, cascadeDelete: true)
                .Index(t => new { t.StationName, t.RegionName })
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", new[] { "StationName", "RegionName" }, "dbo.Stations");
            DropIndex("dbo.Items", new[] { "Name" });
            DropIndex("dbo.Items", new[] { "StationName", "RegionName" });
            DropTable("dbo.Items");
        }
    }
}
