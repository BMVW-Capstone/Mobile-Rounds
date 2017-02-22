namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovableRecords : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "IsMarkedAsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Readings", "IsMarkedAsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Rounds", "IsMarkedAsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Regions", "IsMarkedAsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Specifications", "IsMarkedAsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.UnitOfMeasures", "IsMarkedAsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Stations", "IsMarkedAsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stations", "IsMarkedAsDeleted");
            DropColumn("dbo.UnitOfMeasures", "IsMarkedAsDeleted");
            DropColumn("dbo.Specifications", "IsMarkedAsDeleted");
            DropColumn("dbo.Regions", "IsMarkedAsDeleted");
            DropColumn("dbo.Rounds", "IsMarkedAsDeleted");
            DropColumn("dbo.Readings", "IsMarkedAsDeleted");
            DropColumn("dbo.Items", "IsMarkedAsDeleted");
        }
    }
}
