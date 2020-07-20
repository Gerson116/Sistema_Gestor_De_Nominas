using SistemaGestorDeNominas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SistemaGestorDeNominas.Context
{
    public class SistemaDeGestionDeNomina : DbContext
    {
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<Nomina> Nomina { get; set; }
        public DbSet<ContribucionEmpleado> ContribucionEmpleado { get; set; }
        public DbSet<ContribucionEmpleador> ContribucionEmpleador { get; set; }
    }
}