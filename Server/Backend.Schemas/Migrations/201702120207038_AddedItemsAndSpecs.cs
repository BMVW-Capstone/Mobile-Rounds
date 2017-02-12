namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedItemsAndSpecs : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Items");
            CreateTable(
                "dbo.ComparisonTypes",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Specifications",
                c => new
                    {
                        ItemName = c.String(nullable: false, maxLength: 128),
                        ItemMeter = c.String(nullable: false, maxLength: 128),
                        UnitName = c.String(nullable: false, maxLength: 128),
                        ComparisionTypeName = c.String(nullable: false, maxLength: 128),
                        LowerBoundValue = c.String(nullable: false),
                        UpperBoundValue = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.ItemName, t.ItemMeter })
                .ForeignKey("dbo.ComparisonTypes", t => t.ComparisionTypeName, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => new { t.ItemName, t.ItemMeter })
                .ForeignKey("dbo.UnitOfMeasures", t => t.UnitName, cascadeDelete: true)
                .Index(t => t.ItemName, unique: true)
                .Index(t => new { t.ItemName, t.ItemMeter })
                .Index(t => t.ItemMeter)
                .Index(t => t.UnitName)
                .Index(t => t.ComparisionTypeName);
            
            CreateTable(
                "dbo.UnitOfMeasures",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Abbreviation = c.String(),
                    })
                .PrimaryKey(t => t.Name)
                .Index(t => t.Name, unique: true);
            
            AlterColumn("dbo.Items", "Meter", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Items", new[] { "Name", "Meter" });
            CreateIndex("dbo.Items", "Meter");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Specifications", "UnitName", "dbo.UnitOfMeasures");
            DropForeignKey("dbo.Specifications", new[] { "ItemName", "ItemMeter" }, "dbo.Items");
            DropForeignKey("dbo.Specifications", "ComparisionTypeName", "dbo.ComparisonTypes");
            DropIndex("dbo.UnitOfMeasures", new[] { "Name" });
            DropIndex("dbo.Specifications", new[] { "ComparisionTypeName" });
            DropIndex("dbo.Specifications", new[] { "UnitName" });
            DropIndex("dbo.Specifications", new[] { "ItemMeter" });
            DropIndex("dbo.Specifications", new[] { "ItemName", "ItemMeter" });
            DropIndex("dbo.Specifications", new[] { "ItemName" });
            DropIndex("dbo.Items", new[] { "Meter" });
            DropIndex("dbo.ComparisonTypes", new[] { "Name" });
            DropPrimaryKey("dbo.Items");
            AlterColumn("dbo.Items", "Meter", c => c.String());
            DropTable("dbo.UnitOfMeasures");
            DropTable("dbo.Specifications");
            DropTable("dbo.ComparisonTypes");
            AddPrimaryKey("dbo.Items", "Name");
        }
    }
}
