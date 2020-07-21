using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaGestorDeNominas.Models
{
    public class Nomina
    {
        public int ID { get; set; }
        [Required]
        public int IdEmpleado { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Sexo { get; set; }
        [Required]
        public DateTime FechaDeIngreso { get; set; }
        [Required]
        public double SueldoBruto { get; set; }
        [Required]
        public double ARS { get; set; }
        [Required]
        public double AFP { get; set; }
        [Required]
        public double IRS { get; set; }
        [Required]
        public double TotalDesc { get; set; }
        [Required]
        public double SueldoNeto { get; set; }
        [Required]
        public DateTime FechaDeEmicion { get; set; }

        public int MesDeLaNomina { get; set; } // para filtrar.
        public int AnioDeLaNomia { get; set; } // para filtrar.
    }
}