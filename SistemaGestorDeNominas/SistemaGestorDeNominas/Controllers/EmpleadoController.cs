using SistemaGestorDeNominas.Models.DTOs;
using SistemaGestorDeNominas.Services.Empleado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistemaGestorDeNominas.Controllers
{
    [Route("[controller]/[action]")]
    public class EmpleadoController : Controller
    {
        private IEmpleadoCRUD _empleadoCRUD;
        private EmpleadoDTO _objEmpleadoDTO;
        private FiltroParaEmpleado _filtroParaEmpleado;

        public EmpleadoController(IEmpleadoCRUD empleadoCRUD)
        {
            //....
            _empleadoCRUD = empleadoCRUD;
            _filtroParaEmpleado = new FiltroParaEmpleado();
        }
        [HttpGet]
        public ActionResult AgregarEmpleado() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarEmpleado(string nombre, string apellido, string sexo, double sueldo)
        {
            //Primero valido los que los datos no sean nulos.
            var objValidarEmpleado = new ValidarDatosEmpleados();
            _objEmpleadoDTO = objValidarEmpleado.ValidarDatosAlAgregar(nombre, apellido, sexo, sueldo);
            if (_objEmpleadoDTO != null)
            {
                // Los datos son registrados en la base de datos.
                _empleadoCRUD.AgregarEmpleado(_objEmpleadoDTO);
                return RedirectToAction(nameof(ListadoEmpleado));
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditarEmpleado(int id_empleado)
        {
            var model = _empleadoCRUD.PerfilEmpleado(id_empleado);
            if (model != null)
            {
                return View(model);
            }
            return RedirectToAction(nameof(ListadoEmpleado));
        }

        [HttpPost]
        public ActionResult EditarEmpleado(int id_empleado, string nombre, string apellido, 
            string sexo, double sueldo, string estadoDelCliente)
        {
            // Validando y capturando los datos para despues guardarlos.
            var objValidarDatosEmpleados = new ValidarDatosEmpleados();
            var datos_A_Actualizar = objValidarDatosEmpleados.ValidarDatosAlIntentarEditar(id_empleado, nombre, 
                                                                        apellido, sexo, sueldo, estadoDelCliente);
            if (datos_A_Actualizar != null)
            {
                _empleadoCRUD.ModificarEmpleado(datos_A_Actualizar);
                return RedirectToAction(nameof(ListadoEmpleado));
            }
            return View();
        }

        [HttpGet]
        public ActionResult ListadoEmpleado()
        {
            // El listado siempre llamara los empleados que estan activos.
            var model = _empleadoCRUD.ListadoEmpleadoActivos();
            if (model != null)
            {
                return View(model);
            }
            return RedirectToAction(nameof(AgregarEmpleado));
        }

        [HttpGet]
        public ActionResult ListadoEmpleadoFiltrado(int estado_A_Filtrar)
        {
            // 1 para activo y 0 para inactivo.
            var listado = _filtroParaEmpleado.ListadoEmpleadoPorEstado(estado_A_Filtrar);
            if (listado != null)
            {
                return View(listado);
            }
            return RedirectToAction(nameof(ListadoEmpleado));
        }

        public ActionResult CambiarEstado_A_Activo(int id_empleado)
        {
            // Al cambiar los datos esta acción redireccionara a la accion lista.
            return RedirectToAction(nameof(ListadoEmpleado));
        }

        public ActionResult CambiarEstado_A_Inactivo(int id_empleado)
        {
            // Al cambiar los datos esta acción redireccionara a la accion lista.
            var validarID = new ValidarDatosEmpleados();
            if (validarID.ValidarElIdDelEmpleado(id_empleado) == true)
            {
                //...
                _empleadoCRUD.BloquearEmpleado(id_empleado);
                return RedirectToAction(nameof(ListadoEmpleado));
            }
            return RedirectToAction(nameof(ListadoEmpleado));
        }
    }
}