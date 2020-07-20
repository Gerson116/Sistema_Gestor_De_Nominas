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
        public ActionResult NuevaNomina()
        {
            // Esta acción muestra un listado de empleados, a los cuales agregaremos a la nomina.
            return View();
        }

        public ActionResult FiltrarNomina()
        {
            // La nomina sera filtrada por mes y año.
            return View();
        }

        public ActionResult NominaFiltrada() 
        {
            return View();
        }

        public ActionResult ContribucionEmpleador() 
        {
            return View();
        }

        public ActionResult ContribucionEmpleado() 
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