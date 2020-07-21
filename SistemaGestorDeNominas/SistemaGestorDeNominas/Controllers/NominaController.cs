﻿using SistemaGestorDeNominas.Services.Contribucion_Y_Otros_Impuestos;
using SistemaGestorDeNominas.Services.Empleado;
using SistemaGestorDeNominas.Services.Nomina;
using System;
using System.Collections.Generic;
using System.Collections;
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
                _nominaCRUD.NuevaNomina(miNomina, int.Parse(mesesDelAnio), DateTime.Today.Year);
                return RedirectToAction(nameof(FiltrarNomina));
            }
            return RedirectToAction(nameof(NuevaNomina));
        }

        public ActionResult FiltrarNomina()
        {
            // La nomina sera filtrada por mes y año.
            return View();
        }

        [HttpPost]
        public ActionResult FiltrarNomina(string fechaDeBusqueda, string sexo)
        {
            // La nomina sera filtrada por mes y año.
            var nomina = _nominaCRUD.ListadoDeNominasFiltradaPorFecha(fechaDeBusqueda);
            TempData["list"] = nomina;
            return RedirectToAction(nameof(NominaFiltrada));
        }

        [HttpGet]
        public ActionResult NominaFiltrada()
        {
            //var nomina = _nominaCRUD.ListadoDeNominasFiltradaPorFecha(fechaDeBusqueda);
            var model = TempData["list"];
            return View(model);
        }

        [HttpPost]
        public ActionResult NominaFiltrada(string fechaDeEmicion, string sexo)
        {
            var filtrando = new FiltroParaBuscarNominaPorNombre_O_Sexo();
            var model = TempData["list"];
            if (filtrando.Fecha_Y_Sexo(fechaDeEmicion, sexo) != null)
            {
                model = filtrando;
                return View(model);
            }
            else if (filtrando.Fecha(fechaDeEmicion) != null)
            {
                model = filtrando;
                return View(model);
            }
            return FiltrarNomina();
        }

        public ActionResult GenerarNuevaNomina() 
        {
            // Esto se desarrollara según
            return View();
        }
    }
}