using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaGestorDeNominas.Models.DTOs
{
    public class ContribucionEmpleadorDTO
    {
        public int ID { get; set; }
        [Required]
        public double ARS { get; set; }
        [Required]
        public double AFP { get; set; }
        [Required]
        public double RiesgoLaboral { get; set; }
    }
}