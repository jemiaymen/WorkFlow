namespace WorkFlow.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WorkFlow.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WorkFlow.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WorkFlow.Models.ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(
                 r => r.Name,
                 new IdentityRole { Name = "Responsable Achat" },
                 new IdentityRole { Name = "Decideur" },
                 new IdentityRole { Name = "Demandeur" }
                 );

            context.Departments.AddOrUpdate(
                new Department { Dep = "Department Piece et Service", Budget = 0, Depense = 0 },
                new Department { Dep = "Department Administratif et Financiere", Budget = 0, Depense = 0 },
                new Department { Dep = "Direction Generale", Budget = 0, Depense = 0 },
                new Department { Dep = "Direction Centrale", Budget = 0, Depense = 0 }
            );
        }
    }
}
