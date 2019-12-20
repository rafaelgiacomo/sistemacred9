﻿using System.Data.Entity.Migrations;

namespace SistemaCred9.EntityFramework.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SistemaCred9.EntityFramework.Context.Cred9DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SistemaCred9.EntityFramework.Context.Cred9DbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
