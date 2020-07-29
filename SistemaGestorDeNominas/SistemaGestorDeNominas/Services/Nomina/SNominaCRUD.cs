using SistemaGestorDeNominas.Context;
using SistemaGestorDeNominas.Services.Contribucion_Y_Otros_Impuestos;
using SistemaGestorDeNominas.Services.Empleado;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SistemaGestorDeNominas.Services.Nomina
{
    public class SNominaCRUD : INominaCRUD
    {
        private SEmpleadoCRUD _empleadoCRUD;
        private List<Models.Nomina> _nominaEmpleados;
        private double _totalDescuento;
        private double _sueldoNeto;
        private ValidarExistenciaDeNomina _objValidarExistenciaDeNomina;
        public SNominaCRUD()
        {
            //...
            _empleadoCRUD = new SEmpleadoCRUD();
            _nominaEmpleados = new List<Models.Nomina>();
            _objValidarExistenciaDeNomina = new ValidarExistenciaDeNomina();
        }

        public List<Models.Nomina> ListadoDeNominas()
        {
            return _nominaEmpleados;
        }

        public List<Models.Nomina> ListadoDeNominasFiltradaPorFecha(string fecha)
        {
            //DateTime cambiarFormatoDeFecha = DateTime.Parse(fecha);
            //string fecha_A_Utilizar = cambiarFormatoDeFecha.ToString("MM/dd/yyyy");
            int mes = DateTime.Parse(fecha).Month;
            int year = DateTime.Parse(fecha).Year;
            using (var dbContext = new SistemaDeGestionDeNomina())
            {
                //var objListadoNomina = dbContext.Nomina.ToList();
                //var nominas = ob bjListadoNomina.Where(n => n.MesDeLaNomina == mes).ToList();
                var nominas = dbContext.Nomina.Where(n => n.MesDeLaNomina == mes).ToList();
                if (nominas.Count >= 1 && nominas.Where(n => n.AnioDeLaNomia == year).ToList().Count > 1)
                {
                    _nominaEmpleados = nominas;
                }
                return _nominaEmpleados;
            }
            return null;
        }

        public List<Models.Nomina> ListadoDeNominasFiltradaPorFecha(int mes, int year)
        {
            using (var dbContext = new SistemaDeGestionDeNomina())
            {
                _nominaEmpleados = dbContext.Nomina.Where(nomina =>
                                   nomina.MesDeLaNomina == mes && nomina.AnioDeLaNomia == year).ToList();
                if (_nominaEmpleados.Count > 0)
                {
                    return _nominaEmpleados;
                }
            }
            return null;
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

        public List<Models.Nomina> NuevaNomina(List<Models.Nomina> nuevaNominaEmpleados, int mes, int year)
        {
            var objNomina = new Models.Nomina();
            //... Primero valido que no exista una nomina de este mes y año.
            if (_objValidarExistenciaDeNomina.NominaExitente(mes, year) == null)
            {
                //...
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
                        objNomina.MesDeLaNomina = mes;
                        objNomina.AnioDeLaNomia = year;

                        // Guardando los datos.
                        dbContext.Nomina.Add(objNomina);
                        dbContext.SaveChanges();
                    }
                }
                return nuevaNominaEmpleados;
            }
            return null;
        }

        public bool ModificarNominaExistente(int mes, int year)
        {
            //  Aquí seran eliminadas las nominas que el usuario considere ocurrrio un error y
            //  desea eliminar la nomina ya creada.
            var listadoEmpleadosActivos = _empleadoCRUD.ListadoEmpleadoActivos();
            var objNomina = NominaEmpleados(listadoEmpleadosActivos);

            using (var dbContext = new SistemaDeGestionDeNomina())
            {
                //La variable "datosDeLaNomina" la utilizo para capturar los datos de cada empleado que seran modificados.
                var datosDeLaNomina = objNomina;
                _nominaEmpleados = dbContext.Nomina.Where(n => n.MesDeLaNomina == mes && n.AnioDeLaNomia == year).ToList();
                foreach (var nomina_A_Modificar in _nominaEmpleados)
                {
                    dbContext.Nomina.Remove(nomina_A_Modificar);
                    dbContext.SaveChanges();
                    //nomina_A_Modificar.Nombre = datosDeLaNomina[contador].Nombre;
                    //nomina_A_Modificar.Apellido = datosDeLaNomina[contador].Apellido;
                    //nomina_A_Modificar.Sexo = datosDeLaNomina[contador].Sexo;
                    //nomina_A_Modificar.FechaDeIngreso = datosDeLaNomina[contador].FechaDeIngreso;
                    //nomina_A_Modificar.SueldoBruto = datosDeLaNomina[contador].SueldoBruto;
                    //nomina_A_Modificar.ARS = datosDeLaNomina[contador].ARS;
                    //nomina_A_Modificar.AFP = datosDeLaNomina[contador].AFP;
                    //nomina_A_Modificar.IRS = datosDeLaNomina[contador].IRS;
                    //nomina_A_Modificar.TotalDesc = datosDeLaNomina[contador].TotalDesc;
                    //nomina_A_Modificar.SueldoNeto = datosDeLaNomina[contador].SueldoNeto;
                    //nomina_A_Modificar.FechaDeEmicion = datosDeLaNomina[contador].FechaDeEmicion;
                    //contador++;
                    //dbContext.Nomina.Add(nomina_A_Modificar);
                    //dbContext.SaveChanges();
                }
                NuevaNomina(datosDeLaNomina, mes, year);
            }
            return true;
        }
    }
}