namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeKeyNameForItems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Readings", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Specifications", "ItemId", "dbo.Items");
            DropPrimaryKey("dbo.Items");
            AddColumn("dbo.Items", "ItemId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Items", "ItemId");
            AddForeignKey("dbo.Readings", "ItemId", "dbo.Items", "ItemId");
            AddForeignKey("dbo.Specifications", "ItemId", "dbo.Items", "ItemId");
            DropColumn("dbo.Items", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Id", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Specifications", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Readings", "ItemId", "dbo.Items");
            DropPrimaryKey("dbo.Items");
            DropColumn("dbo.Items", "ItemId");
            AddPrimaryKey("dbo.Items", "Id");
            AddForeignKey("dbo.Specifications", "ItemId", "dbo.Items", "Id");
            AddForeignKey("dbo.Readings", "ItemId", "dbo.Items", "Id");
        }
    }
}
