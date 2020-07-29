using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaGestorDeNominas.Services.Contribucion_Y_Otros_Impuestos
{
    public class ReglaDeTresParaCalcularPorcientos
    {
        public double IRSEmpleado(double excedenteObtenido, double irsEmpleado)
        {
            // Que es la variable excento?
            // La variable excedente contendra el resultado obtenido de la resta del sueldo anual
            // con el monto dado por Impuestos Internos para cada grupo.
            double cantidad_A_Pagar = (excedenteObtenido * irsEmpleado) / 100;
            double irs_A_Pagar = cantidad_A_Pagar / 12;
            return irs_A_Pagar;
        }
        public double IRSEmpleado(double excedenteObtenido, double irsEmpleado, double otrosImpuestosAgregados)
        {
            // Que es la variable excento?
            // La variable excedente contendra el resultado obtenido de la resta del sueldo anual
            // con el monto dado por Impuestos Internos para cada grupo.
            double porcientoAplicadoAlExcedenteDelSueldoAnual = (excedenteObtenido * irsEmpleado) / 100;
            double irs_A_Pagar = (porcientoAplicadoAlExcedenteDelSueldoAnual + otrosImpuestosAgregados) / 12;
            return irs_A_Pagar;
        }

        public double ARS(double sueldo, double ars) 
        {
            // Aplica para ambos casos.
            double ars_A_Pagar = (sueldo * ars) / 100;
            return ars_A_Pagar;
        }

        public double AFP(double sueldo, double afp) 
        {
            // Aplica para ambos casos.
            double afp_A_Pagar = (sueldo * afp) / 100;
            return afp_A_Pagar;
        }

        public double RiesgoLaboral(double sueldo, double riesgoLaboral)
        {
            double cantidad_A_Pagar_riesgoLaboral = (sueldo * riesgoLaboral) / 100;
            return cantidad_A_Pagar_riesgoLaboral;
        }
    }
}