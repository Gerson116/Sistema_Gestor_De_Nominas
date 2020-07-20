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
        [HttpGet]
        public ActionResult AgregarEmpleado() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarEmpleado(string nombre, string apellido, string sexo, double sueldo, string fecha_de_entrada)
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditarEmpleado(int id_empleado)
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditarEmpleado(string nombre, string apellido, string sexo, double sueldo, DateTime fecha_de_entrada)
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListadoEmpleado()
        {
            // el listado siempre llamara los empleados que estan activos.
            return View();
        }

        public ActionResult ListadoEmpleado(int estado_A_Filtrar)
        {
            // 1 para activo y 0 para inactivo
            return View();
        }

        public ActionResult CambiarEstado_A_Activo(int id_empleado)
        {
            // Al eliminar los datos esta acción redireccionara a la accion lista.
            return RedirectToAction(nameof(ListadoEmpleado));
        }

        public ActionResult CambiarEstado_A_Inactivo(int id_empleado)
        {
            // Al eliminar los datos esta acción redireccionara a la accion lista.
            return RedirectToAction(nameof(ListadoEmpleado));
        }
    }
}