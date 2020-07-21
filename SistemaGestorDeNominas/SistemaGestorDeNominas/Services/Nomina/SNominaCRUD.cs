using SistemaGestorDeNominas.Context;
using SistemaGestorDeNominas.Services.Contribucion_Y_Otros_Impuestos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaGestorDeNominas.Services.Nomina
{
    public class SNominaCRUD : INominaCRUD
    {
        private List<Models.Nomina> _nominaEmpleados;
        private double _totalDescuento;
        private double _sueldoNeto;
        public SNominaCRUD()
        {
            //...
            _nominaEmpleados = new List<Models.Nomina>();
        }
        public List<Models.Nomina> NominaEmpleados(List<Models.Empleado> empleados)
        {
            // El objeto "objValidarLasContribuciones" es utilizado para hacer los calculos,
            // de los impuestos.
            var objValidarLasContribuciones = new ValidarLasContribuciones();
            var empleadosEnNomina = new Models.Nomina();

            foreach (var datosDelEmpleado in empleados)
            {
                // Calculando los descuentos para despues guardarlos en la lista.
                var descuentos = objValidarLasContribuciones.ValidarContribucionDelEmpleado(datosDelEmpleado.Sueldo);
                _totalDescuento = descuentos.ARS + descuentos.AFP + descuentos.IRS;
                _sueldoNeto = datosDelEmpleado.Sueldo - _totalDescuento;

                //... Recorriendo la lista empleado y agregando sus valores a la lista Nomina.
                empleadosEnNomina = new Models.Nomina 
                {
                    Nombre = datosDelEmpleado.Nombre,
                    Apellido = datosDelEmpleado.Apellido,
                    Sexo = datosDelEmpleado.Sexo,
                    FechaDeIngreso = datosDelEmpleado.FechaDeEntrada,
                    SueldoBruto = datosDelEmpleado.Sueldo,
                    ARS = descuentos.ARS,
                    AFP = descuentos.AFP,
                    IRS = descuentos.IRS,
                    TotalDesc = _totalDescuento,
                    SueldoNeto = _sueldoNeto - _totalDescuento,
                    FechaDeEmicion = DateTime.Today
                };
                _nominaEmpleados.Add(empleadosEnNomina);
            }
            return _nominaEmpleados;
        }

        public void NuevaNomina(List<Models.Nomina> nuevaNominaEmpleados)
        {
            var objNomina = new Models.Nomina();
            using (var dbContext = new SistemaDeGestionDeNomina())
            {
                foreach (var datosDeLaNomina in nuevaNominaEmpleados)
                {
                    objNomina.Nombre = datosDeLaNomina.Nombre;
                    objNomina.Apellido = datosDeLaNomina.Apellido;
                    objNomina.Sexo = datosDeLaNomina.Sexo;
                    objNomina.FechaDeIngreso = datosDeLaNomina.FechaDeIngreso;
                    objNomina.SueldoBruto = datosDeLaNomina.SueldoBruto;
                    objNomina.ARS = datosDeLaNomina.ARS;
                    objNomina.AFP = datosDeLaNomina.AFP;
                    objNomina.IRS = datosDeLaNomina.IRS;
                    objNomina.TotalDesc = datosDeLaNomina.TotalDesc;
                    objNomina.SueldoNeto = datosDeLaNomina.SueldoNeto;
                    objNomina.FechaDeEmicion = datosDeLaNomina.FechaDeEmicion;

                    // Guardando los datos.
                    //dbContext.Nomina.Add(objNomina);
                    //dbContext.SaveChanges();
                }
            }
        }
    }
}