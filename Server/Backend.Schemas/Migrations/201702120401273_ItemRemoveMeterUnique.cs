namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemRemoveMeterUnique : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Items", new[] { "Meter" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Items", "Meter", unique: true);
        }
    }
}
