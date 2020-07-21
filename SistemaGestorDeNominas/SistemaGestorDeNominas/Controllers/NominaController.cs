using SistemaGestorDeNominas.Services.Contribucion_Y_Otros_Impuestos;
using SistemaGestorDeNominas.Services.Empleado;
using SistemaGestorDeNominas.Services.Nomina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult NuevaNomina()
        {
            // Esta acción muestra un listado de empleados, a los cuales agregaremos a la nomina.
            _listadoEmpleados = _empleadoCRUD.ListadoEmpleadoActivos();
            var miNomina = _nominaCRUD.NominaEmpleados(_listadoEmpleados);
            ViewBag.Message = _mesesDelAnio.Meses();
            return View(miNomina);
        }
        [HttpPost]
        public ActionResult NuevaNomina(string mesesDelAnio)
        {
            _listadoEmpleados = _empleadoCRUD.ListadoEmpleadoActivos();
            var miNomina = _nominaCRUD.NominaEmpleados(_listadoEmpleados);
            // Guardar la nomina y redireccionar al usuario a la ventana filtrar nomina.
            if (_mesesDelAnio.ValidarMesDelAnio(mesesDelAnio) != null)
            {
                _nominaCRUD.NuevaNomina(miNomina);
                return RedirectToAction(nameof(NominaFiltrada));
            }
            return RedirectToAction(nameof(NuevaNomina));
        }

        public ActionResult FiltrarNomina()
        {
            // La nomina sera filtrada por mes y año.
            return View();
        }

        [HttpPost]
        public ActionResult NominaFiltrada(string mesesDelAnio) 
        {
            return View();
        }

        public ActionResult GenerarNuevaNomina() 
        {
            // Esto se desarrollara según
            return View();
        }
    }
}