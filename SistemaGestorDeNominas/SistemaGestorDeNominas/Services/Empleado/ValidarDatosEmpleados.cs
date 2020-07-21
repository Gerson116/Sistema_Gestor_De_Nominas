using SistemaGestorDeNominas.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaGestorDeNominas.Services.Empleado
{
    public class ValidarDatosEmpleados
    {
        public bool ValidarElIdDelEmpleado(int id_empleado) 
        {
            // Validar el ID del empleado que se desea consultar.
            var objSEmpleadoCRUD = new SEmpleadoCRUD();
            var datosDelEmpleado = objSEmpleadoCRUD.PerfilEmpleado(id_empleado);
            if (datosDelEmpleado != null)
            {
                return true;
            }
            return false;
        }
        public EmpleadoDTO ValidarDatosAlAgregar(string nombre, string apellido, string sexo, double sueldo) 
        {
            // Validando los datos al intentar ingresar un nuevo empleado.
            var objEmpleado = new EmpleadoDTO();
            if (nombre != string.Empty && apellido != string.Empty && sexo != string.Empty && sueldo > 0)
            {
                objEmpleado.Nombre = nombre;
                objEmpleado.Apellido = apellido;
                objEmpleado.Sexo = sexo;
                objEmpleado.Sueldo = sueldo;
                objEmpleado.FechaDeEntrada = DateTime.Today;
                objEmpleado.EstadoDelEmpleado = "Activo";
                return objEmpleado;
            }
            return null;
        }

        public EmpleadoDTO ValidarDatosAlIntentarEditar(int idEmpleado, string nombre, string apellido, 
            string sexo, double sueldo, string estadoDelCliente)
        {
            // Este metodo lo utilizo para validar los datos del empleado al ser editados.
            var objEmpleado = new EmpleadoDTO();
            if (idEmpleado > 0 && nombre != string.Empty && apellido != string.Empty && 
                sexo == null && sueldo > 0 && estadoDelCliente == null)
            {
                // asumo que si el usuario dejo el sexo sin marcar es porque no desea cambiarlo.
                objEmpleado.ID = idEmpleado;
                objEmpleado.Nombre = nombre;
                objEmpleado.Apellido = apellido;
                objEmpleado.Sueldo = sueldo;
                return objEmpleado;
            }
            else if (idEmpleado > 0 && nombre != string.Empty && apellido != string.Empty &&
                    sexo != string.Empty && sueldo > 0 && estadoDelCliente == null)
            {
                objEmpleado.ID = idEmpleado;
                objEmpleado.Nombre = nombre;
                objEmpleado.Apellido = apellido;
                objEmpleado.Sexo = sexo;
                objEmpleado.Sueldo = sueldo;
                return objEmpleado;
            }
            else if (idEmpleado > 0 && nombre != string.Empty && apellido != string.Empty &&
                    sexo == null && sueldo > 0 && estadoDelCliente.ToLower() == "activar") 
            {
                objEmpleado.ID = idEmpleado;
                objEmpleado.Nombre = nombre;
                objEmpleado.Apellido = apellido;
                objEmpleado.Sueldo = sueldo;
                objEmpleado.EstadoDelEmpleado = "Activo";
                return objEmpleado;
            }
            return null;
        }
    }
}