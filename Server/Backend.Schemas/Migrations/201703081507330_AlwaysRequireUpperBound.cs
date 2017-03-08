namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlwaysRequireUpperBound : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Specifications", "UpperBoundValue", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Specifications", "UpperBoundValue", c => c.String());
        }
    }
}
