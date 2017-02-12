namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpecRemoveUnique : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Specifications", new[] { "ItemName" });
            DropIndex("dbo.Specifications", new[] { "ItemMeter" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Specifications", "ItemMeter");
            CreateIndex("dbo.Specifications", "ItemName", unique: true);
        }
    }
}
