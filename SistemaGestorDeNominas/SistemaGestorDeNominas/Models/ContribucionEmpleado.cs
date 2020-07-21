using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaGestorDeNominas.Models
{
    public class ContribucionEmpleado
    {
        public int ID { get; set; }
        public double ARS { get; set; }
        public double AFP { get; set; }
        public double IRS { get; set; }
    }
}