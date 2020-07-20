namespace SistemaGestorDeNominas.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SistemaGestorDeNominas.Context.SistemaDeGestionDeNomina>
    {
        public Configuration()
        {
            // cambie de false a true que asi las migraciones se hagan de manera mas sencilla.
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SistemaGestorDeNominas.Context.SistemaDeGestionDeNomina context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
