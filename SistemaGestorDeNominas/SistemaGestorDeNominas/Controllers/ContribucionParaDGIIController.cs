using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaGestorDeNominas.Controllers
{
    [Route("[controller]/[action]")]
    public class ContribucionParaDGIIController : Controller
    {
        /* Este controlador gestiona los datos de las contribuciones 
           tanto del empleado como del empleador.*/
        public ActionResult FormularioContribucionesAgregar()
        {
            return View();
        }

        public ActionResult FormularioContribucionesEditar() 
        {
            return View();
        }
    }
}