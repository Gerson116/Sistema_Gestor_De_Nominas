using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaGestorDeNominas.Controllers
{
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

        public ActionResult PerfilEmpleado()
        {
            return View();
        }

        public ActionResult EditarEmpleado()
        {
            return View();
        }

        public ActionResult EliminarEmpleado()
        {
            // Al eliminar los datos esta acción redireccionara a la accion lista.
            return RedirectToAction(nameof(ListadoEmpleado));
        }
    }
}