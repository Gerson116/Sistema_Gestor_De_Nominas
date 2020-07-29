using SistemaGestorDeNominas.Services.Contribucion_Y_Otros_Impuestos;
using SistemaGestorDeNominas.Services.Empleado;
using SistemaGestorDeNominas.Services.Nomina;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaGestorDeNominas.Models;

namespace SistemaGestorDeNominas.Controllers
{
    [Route("[controller]/[action]")]
    public class NominaController : Controller
    {
        private IEmpleadoCRUD _empleadoCRUD;
        private INominaCRUD _nominaCRUD;
        private ValidarLasContribuciones _objValidarLasContribuciones;
        private List<Models.Empleado> _listadoEmpleados;
        private MesesDelAnio _mesesDelAnio;

        public NominaController(IEmpleadoCRUD empleadoCRUD, INominaCRUD nominaCRUD)
        {
            //...
            _empleadoCRUD = empleadoCRUD;
            _nominaCRUD = nominaCRUD;
            _objValidarLasContribuciones = new ValidarLasContribuciones();
            _listadoEmpleados = new List<Models.Empleado>();
            _mesesDelAnio = new MesesDelAnio();
        }

        [HttpGet]
        public ActionResult NuevaNomina()
        {
            // Esta acción muestra un listado de empleados, a los cuales agregaremos a la nomina.
            _listadoEmpleados = _empleadoCRUD.ListadoEmpleadoActivos();
            var miNomina = _nominaCRUD.NominaEmpleados(_listadoEmpleados);

            ViewBag.Message = _mesesDelAnio.Meses();
            ViewBag.Mes = TempData["mes"];
            ViewBag.MesEnQueFueCredaLaNomina = TempData["MesEnLaQueFueCreadaLaNomina"];
            ViewBag.NominaExistente = TempData["estaNominaExite"];
            ViewBag.DatosEliminadosConExito = TempData["DatosEliminadosConExito"];
            ViewBag.NominaGuardadaExistamente = TempData["NominaGuardadaExistamente"];
            return View(miNomina);
        }

        [HttpPost]
        public ActionResult NuevaNomina(string mesesDelAnio)
        {
            _listadoEmpleados = _empleadoCRUD.ListadoEmpleadoActivos();
            var miNomina = _nominaCRUD.NominaEmpleados(_listadoEmpleados);
            var nominaExistente = new List<Models.Nomina>();
            // Guardar la nomina y redireccionar al usuario a la ventana filtrar nomina.
            if (_mesesDelAnio.ValidarMesDelAnio(mesesDelAnio) != null)
            {
                nominaExistente = _nominaCRUD.ListadoDeNominasFiltradaPorFecha(int.Parse(mesesDelAnio), DateTime.Today.Year);
                TempData["mes"] = int.Parse(mesesDelAnio);
                TempData["MesEnLaQueFueCreadaLaNomina"] = _mesesDelAnio.ValidarMesDelAnio(mesesDelAnio);
                if (nominaExistente != null)
                {
                    TempData["estaNominaExite"] = true;
                    return RedirectToAction(nameof(NuevaNomina));
                }
                var model = _nominaCRUD.NuevaNomina(miNomina, int.Parse(mesesDelAnio), DateTime.Today.Year);
                TempData["NominaGuardadaExistamente"] = true;
                return RedirectToAction(nameof(NuevaNomina));
            }
            return RedirectToAction(nameof(NuevaNomina));
        }

        public ActionResult FiltrarNomina()
        {
            // La nomina sera filtrada por mes y año.
            ViewBag.MesesDelAnio = _mesesDelAnio.Meses();
            ViewBag.Years = _mesesDelAnio.Years();
            return View();
        }

        /*Este código era utilizado anteriormente pero ya esta anulado
        //public ActionResult FiltrarNomina(string fechaDeBusqueda)
        //{
        //    // La nomina sera filtrada por mes y año.
        //    DateTime cabiarFecha = DateTime.Parse(fechaDeBusqueda);
        //    int capturarMes = DateTime.Parse(cabiarFecha.ToString("MM/dd/yyyy")).Month;
        //    var nominaDelMes = new MesesDelAnio().ValidarMesDelAnio(capturarMes.ToString());
        //    var nomina = new FiltroParaBuscarNominaPorNombre_O_Sexo().Fecha(fechaDeBusqueda);
        //    TempData["list"] = nomina;
        //    return RedirectToAction(nameof(NominaFiltrada));
        //}*/

        [HttpPost]
        public ActionResult FiltrarNomina(string meses, string years)
        {
            // La nomina sera filtrada por mes y año.
            var nominaDelMes = new MesesDelAnio().ValidarMesDelAnio(meses);
            var nomina = new FiltroParaBuscarNominaPorNombre_O_Sexo().Fecha(int.Parse(meses), int.Parse(years));
            if (nomina != null)
            {
                TempData["list"] = nomina;
                return RedirectToAction(nameof(NominaFiltrada));
            }
            return RedirectToAction(nameof(FiltrarNomina));
        }

        [HttpGet]
        public ActionResult NominaFiltrada()
        {
            //var nomina = _nominaCRUD.ListadoDeNominasFiltradaPorFecha(fechaDeBusqueda);
            ViewBag.MesesDelAnio = _mesesDelAnio.Meses();
            ViewBag.Years = _mesesDelAnio.Years();
            var model = TempData["list"];
            return View(model);
        }

        /*Este código era utilizado anteriormente pero ya esta anulado
        //public ActionResult NominaFiltrada(string fechaDeEmicion, string sexo)
        //{
        //    var filtrando = new FiltroParaBuscarNominaPorNombre_O_Sexo();
        //    var model = new List<Models.Nomina>();
            
        //    if (filtrando.Fecha_Y_Sexo(fechaDeEmicion, sexo) != null)
        //    {
        //        model = filtrando.Fecha_Y_Sexo(fechaDeEmicion, sexo);
        //        if (model != null)
        //        {
        //            ViewData["mes"] = DateTime.Parse(DateTime.Parse(fechaDeEmicion).ToString("MM/dd/yyyy")).Month;
        //            ViewData["year"] = DateTime.Parse(DateTime.Parse(fechaDeEmicion).ToString("MM/dd/yyyy")).Year;
        //            return View(model);
        //        }
        //    }
        //    else if (filtrando.Fecha(fechaDeEmicion) != null)
        //    {
        //        model = filtrando.Fecha(fechaDeEmicion);
        //        ViewData["mes"] = DateTime.Parse(DateTime.Parse(fechaDeEmicion).ToString("MM/dd/yyyy")).Month;
        //        ViewData["year"] = DateTime.Parse(DateTime.Parse(fechaDeEmicion).ToString("MM/dd/yyyy")).Year;
        //        return View(model);
        //    }
        //    return RedirectToAction(nameof(FiltrarNomina));
        //}*/

        [HttpPost]
        public ActionResult NominaFiltrada(string meses, string years, string sexo)
        {
            var filtrando = new FiltroParaBuscarNominaPorNombre_O_Sexo();
            var model = new List<Models.Nomina>();

            ViewBag.MesesDelAnio = _mesesDelAnio.Meses();
            ViewBag.Years = _mesesDelAnio.Years();

            if (filtrando.Fecha_Y_Sexo(meses, years, sexo) != null)
            {
                model = filtrando.Fecha_Y_Sexo(meses, years, sexo);
                if (model != null)
                {
                    ViewData["mes"] = int.Parse(meses);
                    ViewData["year"] = int.Parse(years);
                    return View(model);
                }
            }
            else if (filtrando.Fecha(int.Parse(meses), int.Parse(years)) != null)
            {
                model = filtrando.Fecha(int.Parse(meses), int.Parse(years));
                ViewData["mes"] = int.Parse(meses);
                ViewData["year"] = int.Parse(years);
                return View(model);
            }
            return RedirectToAction(nameof(FiltrarNomina));
        }

        public ActionResult GenerarNuevaNomina() 
        {
            // Esto se desarrollara según
            return View();
        }

        public ActionResult ModificarNomina(int mes, int year)
        {
            // ESTE ES EL BLOQUE DE CODIGO QUE ESTOY TRABAJANDO.
            // var listadoEmpleados = _empleadoCRUD.ListadoEmpleadoActivos();
            // var nominaConDatosActualizados = _nominaCRUD.NominaEmpleados(listadoEmpleados);
            _nominaCRUD.ModificarNominaExistente(mes, year);

            TempData["DatosEliminadosConExito"] = true;
            TempData["MesEnLaQueFueCreadaLaNomina"] = _mesesDelAnio.ValidarMesDelAnio(mes.ToString());
            return RedirectToAction(nameof(NuevaNomina));
        }
    }
}