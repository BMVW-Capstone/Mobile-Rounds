namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStationsAndRounds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rounds",
                c => new
                    {
                        RoundHour = c.DateTime(nullable: false),
                        RegionName = c.String(nullable: false, maxLength: 128),
                        AssignedTo = c.String(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoundHour, t.RegionName })
                .ForeignKey("dbo.Regions", t => t.RegionName, cascadeDelete: true)
                .Index(t => t.RegionName);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        RegionName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Name, t.RegionName })
                .ForeignKey("dbo.Regions", t => t.RegionName, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.RegionName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stations", "RegionName", "dbo.Regions");
            DropForeignKey("dbo.Rounds", "RegionName", "dbo.Regions");
            DropIndex("dbo.Stations", new[] { "RegionName" });
            DropIndex("dbo.Stations", new[] { "Name" });
            DropIndex("dbo.Rounds", new[] { "RegionName" });
            DropTable("dbo.Stations");
            DropTable("dbo.Rounds");
        }
    }
}
