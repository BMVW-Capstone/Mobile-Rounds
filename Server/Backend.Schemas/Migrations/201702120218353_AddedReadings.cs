namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReadings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Readings",
                c => new
                    {
                        TimeTaken = c.DateTime(nullable: false),
                        ItemName = c.String(nullable: false, maxLength: 128),
                        ItemMeter = c.String(nullable: false, maxLength: 128),
                        Value = c.String(nullable: false),
                        IsOutOfSpec = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.TimeTaken, t.ItemName, t.ItemMeter })
                .ForeignKey("dbo.Items", t => new { t.ItemName, t.ItemMeter }, cascadeDelete: true)
                .Index(t => t.TimeTaken, unique: true)
                .Index(t => t.ItemName, unique: true)
                .Index(t => new { t.ItemName, t.ItemMeter })
                .Index(t => t.ItemMeter);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Readings", new[] { "ItemName", "ItemMeter" }, "dbo.Items");
            DropIndex("dbo.Readings", new[] { "ItemMeter" });
            DropIndex("dbo.Readings", new[] { "ItemName", "ItemMeter" });
            DropIndex("dbo.Readings", new[] { "ItemName" });
            DropIndex("dbo.Readings", new[] { "TimeTaken" });
            DropTable("dbo.Readings");
        }
    }
}
