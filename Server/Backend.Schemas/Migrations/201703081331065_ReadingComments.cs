namespace Backend.Schemas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReadingComments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Readings", "Comments", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Readings", "Comments");
        }
    }
}
