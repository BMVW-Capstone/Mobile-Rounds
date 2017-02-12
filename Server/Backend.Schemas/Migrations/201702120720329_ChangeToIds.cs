namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeToIds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComparisonTypes",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 300, unicode: false),
                        Meter = c.String(nullable: false),
                        StationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stations", t => t.StationId)
                .Index(t => t.Name)
                .Index(t => t.StationId);
            
            CreateTable(
                "dbo.Readings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ItemId = c.Guid(nullable: false),
                        TimeTaken = c.DateTime(nullable: false),
                        RoundId = c.Guid(nullable: false),
                        Value = c.String(nullable: false),
                        IsOutOfSpec = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId)
                .ForeignKey("dbo.Rounds", t => t.RoundId)
                .Index(t => t.ItemId)
                .Index(t => t.TimeTaken, unique: true)
                .Index(t => t.RoundId);
            
            CreateTable(
                "dbo.Rounds",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoundHour = c.DateTime(nullable: false),
                        RegionId = c.Guid(nullable: false),
                        AssignedTo = c.String(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Regions", t => t.RegionId)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 300, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Specifications",
                c => new
                    {
                        ItemId = c.Guid(nullable: false),
                        UnitId = c.Guid(nullable: false),
                        ComparisionTypeName = c.String(nullable: false, maxLength: 128),
                        LowerBoundValue = c.String(nullable: false),
                        UpperBoundValue = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.ComparisonTypes", t => t.ComparisionTypeName)
                .ForeignKey("dbo.UnitOfMeasures", t => t.UnitId)
                .ForeignKey("dbo.Items", t => t.ItemId)
                .Index(t => t.ItemId)
                .Index(t => t.UnitId)
                .Index(t => t.ComparisionTypeName);
            
            CreateTable(
                "dbo.UnitOfMeasures",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 300, unicode: false),
                        Abbreviation = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 300, unicode: false),
                        RegionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Regions", t => t.RegionId)
                .Index(t => t.Name)
                .Index(t => t.RegionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "StationId", "dbo.Stations");
            DropForeignKey("dbo.Stations", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Specifications", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Specifications", "UnitId", "dbo.UnitOfMeasures");
            DropForeignKey("dbo.Specifications", "ComparisionTypeName", "dbo.ComparisonTypes");
            DropForeignKey("dbo.Readings", "RoundId", "dbo.Rounds");
            DropForeignKey("dbo.Rounds", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Readings", "ItemId", "dbo.Items");
            DropIndex("dbo.Stations", new[] { "RegionId" });
            DropIndex("dbo.Stations", new[] { "Name" });
            DropIndex("dbo.UnitOfMeasures", new[] { "Name" });
            DropIndex("dbo.Specifications", new[] { "ComparisionTypeName" });
            DropIndex("dbo.Specifications", new[] { "UnitId" });
            DropIndex("dbo.Specifications", new[] { "ItemId" });
            DropIndex("dbo.Regions", new[] { "Name" });
            DropIndex("dbo.Rounds", new[] { "RegionId" });
            DropIndex("dbo.Readings", new[] { "RoundId" });
            DropIndex("dbo.Readings", new[] { "TimeTaken" });
            DropIndex("dbo.Readings", new[] { "ItemId" });
            DropIndex("dbo.Items", new[] { "StationId" });
            DropIndex("dbo.Items", new[] { "Name" });
            DropIndex("dbo.ComparisonTypes", new[] { "Name" });
            DropTable("dbo.Stations");
            DropTable("dbo.UnitOfMeasures");
            DropTable("dbo.Specifications");
            DropTable("dbo.Regions");
            DropTable("dbo.Rounds");
            DropTable("dbo.Readings");
            DropTable("dbo.Items");
            DropTable("dbo.ComparisonTypes");
        }
    }
}
