using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaGestorDeNominas.Services.Contribucion_Y_Otros_Impuestos
{
    public class CalcularIRS
    {
        /*El impuesto sobre la renta puede variar dependiendo las decisiones que tome Gobierno.*/

        private int _mesesTrabajados;
        private double _exentosDePagoPorIRS;

        // Primer grupo a pagar IRS.
        private double _primerGrupo_A_Pagar_IRS;
        private double _irsPrimerGrupo;

        // Segundo grupo a pagar IRS.
        private double _segundoGrupo_A_Pagar_IRS;
        private double _otrosImpuestosCalculadosAlSegundoGrupo;
        private double _irsSegundoGrupo;

        // Tercer grupo a pagar IRS.
        private double _otrosImpuestosCalculadosAlTercerGrupo;
        private double _irsTercerGrupo;

        public CalcularIRS()
        {
            _mesesTrabajados = 12;
            _exentosDePagoPorIRS = 416220.01; // Esto para 2019-2020.

            // Todos los que sobrepasen 41622.01 - 624329.01 pagar 15% del excedente
            _primerGrupo_A_Pagar_IRS = 624329.01;
            _irsPrimerGrupo = 15;

            // Todos los que sobrepasen 624329.01 - 867123.00 pagar 20% del excedente mas 31216
            _segundoGrupo_A_Pagar_IRS = 867123;
            _otrosImpuestosCalculadosAlSegundoGrupo = 31216 / _mesesTrabajados;
            _irsSegundoGrupo = 20;

            // Todos los que sobrepasen 867123.00 en adelante pagar 25% del excedente mas 79776
            _otrosImpuestosCalculadosAlTercerGrupo = 79776 / _mesesTrabajados;
            _irsTercerGrupo = 25;
        }
        public double ResultadoIRSEmpleado(double sueldo) 
        {
            double sueldoAnual = sueldo * _mesesTrabajados;
            var objReglaDeTresParaCalcularIRS = new ReglaDeTresParaCalcularPorcientos();
            if (sueldoAnual <= _exentosDePagoPorIRS)
            {
                return 0;
            }
            else if (sueldoAnual >= _exentosDePagoPorIRS && sueldoAnual <= _primerGrupo_A_Pagar_IRS)
            {
                // Primer grupo a pagar IRS.
                return objReglaDeTresParaCalcularIRS.IRSEmpleado(sueldo, _irsPrimerGrupo);
            }
            else if (sueldoAnual >= _primerGrupo_A_Pagar_IRS && sueldoAnual <= _segundoGrupo_A_Pagar_IRS)
            {
                // Segundo grupo a pagar IRS.
                return objReglaDeTresParaCalcularIRS.IRSEmpleado(sueldo, _irsSegundoGrupo);
            }
            else if (sueldoAnual > _segundoGrupo_A_Pagar_IRS)
            {
                // Tercer grupo a pagar IRS.
                return objReglaDeTresParaCalcularIRS.IRSEmpleado(sueldo, _irsTercerGrupo);
            }
            return 0;
        }
        public double OtrosImpuestosParaGrupo_Dos_Y_Tres(double sueldo) 
        {
            // Estos impuestos se cobran junto al IRS.
            double sueldoAnual = sueldo * _mesesTrabajados;
            if (sueldoAnual >= _primerGrupo_A_Pagar_IRS && sueldoAnual <= _segundoGrupo_A_Pagar_IRS)
            {
                // Segundo grupo a pagar IRS.
                return sueldo * _otrosImpuestosCalculadosAlSegundoGrupo;
            }
            else if (sueldoAnual > _segundoGrupo_A_Pagar_IRS)
            {
                // Tercer grupo a pagar IRS.
                return sueldo * _otrosImpuestosCalculadosAlTercerGrupo;
            }
            return 0;
        }
    }
}