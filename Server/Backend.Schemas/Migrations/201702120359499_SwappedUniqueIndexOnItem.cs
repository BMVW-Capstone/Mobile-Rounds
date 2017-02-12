namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SwappedUniqueIndexOnItem : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Items", new[] { "Name" });
            DropIndex("dbo.Items", new[] { "Meter" });
            CreateIndex("dbo.Items", "Meter", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Items", new[] { "Meter" });
            CreateIndex("dbo.Items", "Meter");
            CreateIndex("dbo.Items", "Name", unique: true);
        }
    }
}
