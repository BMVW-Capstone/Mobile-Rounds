namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpecComparisonTypeNameRename : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Specifications", name: "ComparisionTypeName", newName: "ComparisonTypeName");
            RenameIndex(table: "dbo.Specifications", name: "IX_ComparisionTypeName", newName: "IX_ComparisonTypeName");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Specifications", name: "IX_ComparisonTypeName", newName: "IX_ComparisionTypeName");
            RenameColumn(table: "dbo.Specifications", name: "ComparisonTypeName", newName: "ComparisionTypeName");
        }
    }
}
