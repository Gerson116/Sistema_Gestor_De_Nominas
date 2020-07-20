using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaGestorDeNominas.Controllers
{
    [Route("[controller]/[action]")]
    public class EmpleadoController : Controller
    {
        public ActionResult AgregarEmpleado() 
        {
            return View();
        }

        public ActionResult ListadoEmpleado()
        {
            return View();
        }

        public ActionResult EditarEmpleado()
        {
            return View();
        }

        public ActionResult CambiarEstado_A_Activo()
        {
            // Al eliminar los datos esta acción redireccionara a la accion lista.
            return RedirectToAction(nameof(ListadoEmpleado));
        }
        public ActionResult CambiarEstado_A_Inactivo()
        {
            // Al eliminar los datos esta acción redireccionara a la accion lista.
            return RedirectToAction(nameof(ListadoEmpleado));
        }
    }
}