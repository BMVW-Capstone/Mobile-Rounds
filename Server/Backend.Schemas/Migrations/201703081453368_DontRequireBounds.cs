namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DontRequireBounds : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Specifications", "LowerBoundValue", c => c.String());
            AlterColumn("dbo.Specifications", "UpperBoundValue", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Specifications", "UpperBoundValue", c => c.String(nullable: false));
            AlterColumn("dbo.Specifications", "LowerBoundValue", c => c.String(nullable: false));
        }
    }
}
