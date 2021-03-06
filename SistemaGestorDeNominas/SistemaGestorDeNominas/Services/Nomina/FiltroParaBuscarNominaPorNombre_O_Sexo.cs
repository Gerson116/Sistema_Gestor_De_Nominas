﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;

namespace SistemaGestorDeNominas.Services.Nomina
{
    public class FiltroParaBuscarNominaPorNombre_O_Sexo
    {
        private SNominaCRUD _filtrarPorSexo;

        public FiltroParaBuscarNominaPorNombre_O_Sexo()
        {
            _filtrarPorSexo = new SNominaCRUD();
        }
        public List<Models.Nomina> Fecha(string fecha) 
        {
            if (fecha != string.Empty)
            {
                DateTime cambiarFecha = DateTime.Parse(fecha);
                return _filtrarPorSexo.ListadoDeNominasFiltradaPorFecha(cambiarFecha.ToString("MM/dd/yyyy"));
            }
            return null;
        }
        public List<Models.Nomina> Fecha(int mes, int year)
        {
            if (mes > 0 && year > 0)
            {
                var nominaFiltrada = _filtrarPorSexo.ListadoDeNominasFiltradaPorFecha(mes, year);
                if (nominaFiltrada != null) 
                {
                    return nominaFiltrada;
                }
            }
            return null;
        }
        public List<Models.Nomina> Fecha_Y_Sexo(string fecha, string sexo) 
        {
            if (fecha == string.Empty && sexo != string.Empty)
            {
                return null;
            }
            else if (fecha != string.Empty && sexo != string.Empty)
            {
                DateTime cambiarFecha = DateTime.Parse(fecha);
                if(sexo == "f" || sexo == "m")
                {
                    return _filtrarPorSexo.ListadoDeNominasFiltradaPorFecha(cambiarFecha.ToString("MM/dd/yyyy")).Where(s => s.Sexo == sexo).ToList();
                }
            }
            return null;
        }
        public List<Models.Nomina> Fecha_Y_Sexo(string mes, string year, string sexo)
        {
            if (mes == string.Empty && sexo != string.Empty)
            {
                return null;
            }
            else if (mes != string.Empty && sexo != string.Empty)
            {
                if (sexo == "f" || sexo == "m")
                {
                    var resultadoDeLaNominaFiltrada = _filtrarPorSexo.ListadoDeNominasFiltradaPorFecha(
                                                        int.Parse(mes), int.Parse(year)).Where(s => s.Sexo == sexo).ToList();
                    return resultadoDeLaNominaFiltrada;
                }
            }
            return null;
        }
    }
}