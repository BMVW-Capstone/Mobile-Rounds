namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReadingRemovedUnique : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Readings", new[] { "ItemName" });
            DropIndex("dbo.Readings", new[] { "ItemMeter" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Readings", "ItemMeter");
            CreateIndex("dbo.Readings", "ItemName", unique: true);
        }
    }
}
