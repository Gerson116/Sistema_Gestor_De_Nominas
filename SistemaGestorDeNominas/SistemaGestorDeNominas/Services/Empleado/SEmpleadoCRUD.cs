using SistemaGestorDeNominas.Context;
using SistemaGestorDeNominas.Models.DTOs;
using SistemaGestorDeNominas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaGestorDeNominas.Services.Empleado
{
    public class SEmpleadoCRUD : IEmpleadoCRUD
    {
        private List<Models.Empleado> _listadoEmpleado;
        private Models.Empleado _objEmpleado;
        private EmpleadoDTO _objEmpleadoDTO;
        public SEmpleadoCRUD()
        {
            //...
            _listadoEmpleado = new List<Models.Empleado>();
            _objEmpleado = new Models.Empleado();
            _objEmpleadoDTO = new EmpleadoDTO();
        }
        public EmpleadoDTO PerfilEmpleado(int id_empleado) 
        {
            using(var dbContext = new SistemaDeGestionDeNomina())
            {
                // Capturando los datos del usuario por ID
                _objEmpleado = dbContext.Empleado.Find(id_empleado);
                if(_objEmpleado != null)
                {
                    _objEmpleadoDTO.ID = _objEmpleado.ID;
                    _objEmpleadoDTO.Nombre = _objEmpleado.Nombre;
                    _objEmpleadoDTO.Apellido = _objEmpleado.Apellido;
                    _objEmpleadoDTO.Sexo = _objEmpleado.Sexo;
                    _objEmpleadoDTO.Sueldo = _objEmpleado.Sueldo;
                    return _objEmpleadoDTO;
                }
            }
            return null;
        }
        public void AgregarEmpleado(EmpleadoDTO datosDelEmpleado)
        {
            // Llamando el DbContext y guardando los datos.
            using (var dbContext = new SistemaDeGestionDeNomina())
            {
                _objEmpleado.Nombre = datosDelEmpleado.Nombre;
                _objEmpleado.Apellido = datosDelEmpleado.Apellido;
                _objEmpleado.Sexo = datosDelEmpleado.Sexo;
                _objEmpleado.Sueldo = datosDelEmpleado.Sueldo;
                _objEmpleado.FechaDeEntrada = datosDelEmpleado.FechaDeEntrada;
                _objEmpleado.EstadoDelEmpleado = datosDelEmpleado.EstadoDelEmpleado;

                // Guardando datos.
                dbContext.Empleado.Add(_objEmpleado);
                dbContext.SaveChanges();
            }
        }

        public bool BloquearEmpleado(int id_empleado)
        {
            // En caso de el usuario existir, sera bloquedo y retornara verdadero o falso.
            using (var dbContext = new SistemaDeGestionDeNomina()) 
            {
                _objEmpleado = dbContext.Empleado.Find(id_empleado);
                if (_objEmpleado != null) 
                {
                    // Cambios al estado
                    _objEmpleado.EstadoDelEmpleado = "Inactivo";
                    dbContext.Entry(_objEmpleado).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();

                    return true;
                }
            }
            return false;
        }

        public List<Models.Empleado> ListadoEmpleadoActivos()
        {
            using(var dbContext = new SistemaDeGestionDeNomina())
            {
                //....
                var listadoEmpleado = new List<Models.Empleado>();
                _listadoEmpleado = dbContext.Empleado.Where(e => e.EstadoDelEmpleado == "Activo").ToList();
                if (_listadoEmpleado.Count >= 1) 
                {
                    return _listadoEmpleado;
                }
            }
            return null;
        }

        public List<Models.Empleado> ListadoEmpleadoInactivos()
        {
            using (var dbContext = new SistemaDeGestionDeNomina())
            {
                //....
                var listadoEmpleado = new List<Models.Empleado>();
                _listadoEmpleado = dbContext.Empleado.Where(e => e.EstadoDelEmpleado == "Inactivo").ToList();
                if (_listadoEmpleado.Count >= 1)
                {
                    return _listadoEmpleado;
                }
            }
            return null;
        }

        public void ModificarEmpleado(EmpleadoDTO datosDelEmpleado)
        {
            // Validar si el sexo no fue modificado.
            if (datosDelEmpleado.Sexo != null)
            {
                using (var dbContext = new SistemaDeGestionDeNomina())
                {
                    _objEmpleado = dbContext.Empleado.Find(datosDelEmpleado.ID);
                    _objEmpleado.Nombre = datosDelEmpleado.Nombre;
                    _objEmpleado.Apellido = datosDelEmpleado.Apellido;
                    _objEmpleado.Sexo = datosDelEmpleado.Sexo;
                    _objEmpleado.Sueldo = datosDelEmpleado.Sueldo;

                    // Guardando los datos
                    dbContext.Entry(_objEmpleado).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            else if(datosDelEmpleado.EstadoDelEmpleado != null)
            {
                using (var dbContext = new SistemaDeGestionDeNomina())
                {
                    _objEmpleado = dbContext.Empleado.Find(datosDelEmpleado.ID);
                    _objEmpleado.Nombre = datosDelEmpleado.Nombre;
                    _objEmpleado.Apellido = datosDelEmpleado.Apellido;
                    _objEmpleado.EstadoDelEmpleado = datosDelEmpleado.EstadoDelEmpleado;
                    _objEmpleado.Sueldo = datosDelEmpleado.Sueldo;

                    // Guardando los datos
                    dbContext.Entry(_objEmpleado).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            else
            {
                using (var dbContext = new SistemaDeGestionDeNomina())
                {
                    _objEmpleado = dbContext.Empleado.Find(datosDelEmpleado.ID);
                    _objEmpleado.Nombre = datosDelEmpleado.Nombre;
                    _objEmpleado.Apellido = datosDelEmpleado.Apellido;
                    _objEmpleado.Sueldo = datosDelEmpleado.Sueldo;

                    // Guardando los datos
                    dbContext.Entry(_objEmpleado).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
        }
    }
}