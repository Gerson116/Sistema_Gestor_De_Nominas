using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaGestorDeNominas.Models
{
    public class Empleado
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(50), MinLength(3)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(50), MinLength(3)]
        public string Apellido { get; set; }
        [Required]
        [MaxLength(1), MinLength(1)]
        public string Sexo { get; set; }
        [Required]
        public double Sueldo { get; set; }
        [Required]
        public DateTime FechaDeEntrada { get; set; }
        public string EstadoDelEmpleado { get; set; }
    }
}