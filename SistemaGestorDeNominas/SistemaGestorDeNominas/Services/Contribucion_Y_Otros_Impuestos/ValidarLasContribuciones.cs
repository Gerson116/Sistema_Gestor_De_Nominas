using SistemaGestorDeNominas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaGestorDeNominas.Services.Contribucion_Y_Otros_Impuestos
{
    public class ValidarLasContribuciones
    {
        private double _arsEmpleador;
        private double _afpEmpleador;
        private double _riesgoLaboral;

        private double _arsEmpleado;
        private double _afpEmpleado;

        private CalcularIRS _objCalcularIRS;
        private ReglaDeTresParaCalcularPorcientos _objCalcularPorcientos;

        public ValidarLasContribuciones()
        {
            _arsEmpleador = 7.09;
            _afpEmpleador = 7.10;
            _riesgoLaboral = 1.03;

            _arsEmpleado = 3.04;
            _afpEmpleado = 2.87;

            _objCalcularIRS = new CalcularIRS();
            _objCalcularPorcientos = new ReglaDeTresParaCalcularPorcientos();
        }
        public ContribucionEmpleado ValidarContribucionDelEmpleado(double sueldo)
        {
            var objContribucionEmpleado = new ContribucionEmpleado();
            objContribucionEmpleado.ARS = _objCalcularPorcientos.ARS(sueldo, _arsEmpleado);
            objContribucionEmpleado.AFP = _objCalcularPorcientos.AFP(sueldo, _afpEmpleado);
            objContribucionEmpleado.IRS = _objCalcularIRS.ResultadoIRSEmpleado(sueldo);
            return objContribucionEmpleado;
        }

        public ContribucionEmpleador ValidarContribucionDelEmpleador()
        {
            var objContribucionEmpleador = new ContribucionEmpleador();
            return null;
        }
    }
}