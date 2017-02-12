namespace Backend.Schemas.Migrations
{
    using Seeding;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            Database.SetInitializer(new CreateDatabaseIfNotExists<DatabaseContext>());
        }

        protected override void Seed(DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.
            DatabaseSeeder seeder = new DatabaseSeeder();
            seeder.Seed(context);
        }
    }
}
