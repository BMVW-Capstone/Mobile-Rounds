namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegionNotNullNameConstraint : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Regions", new[] { "Name" });
            AlterColumn("dbo.Regions", "Name", c => c.String(nullable: false, maxLength: 300, unicode: false));
            CreateIndex("dbo.Regions", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Regions", new[] { "Name" });
            AlterColumn("dbo.Regions", "Name", c => c.String(maxLength: 300, unicode: false));
            CreateIndex("dbo.Regions", "Name", unique: true);
        }
    }
}
