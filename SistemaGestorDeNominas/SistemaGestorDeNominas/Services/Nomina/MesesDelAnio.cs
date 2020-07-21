using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaGestorDeNominas.Services.Nomina
{
    // Escribín anio por año.
    public class MesesDelAnio
    {
        private List<SelectListItem> _mesesDelAnio;
        public MesesDelAnio()
        {
            //...
            _mesesDelAnio = new List<SelectListItem>
            {
                new SelectListItem { Text = "Enero", Value = "1" },
                new SelectListItem { Text = "Febrero", Value = "2" },
                new SelectListItem { Text = "Marzo", Value = "3" },
                new SelectListItem { Text = "Abril", Value = "4" },
                new SelectListItem { Text = "Mayo", Value = "5" },
                new SelectListItem { Text = "Junio", Value = "6" },
                new SelectListItem { Text = "Julio", Value = "7" },
                new SelectListItem { Text = "Agosto", Value = "8" },
                new SelectListItem { Text = "Septiembre", Value = "9" },
                new SelectListItem { Text = "Octubre", Value = "10" },
                new SelectListItem { Text = "Noviembre", Value = "11" },
                new SelectListItem { Text = "Diciembre", Value = "12" }
            };
        }
        public List<SelectListItem> Meses()
        {
            // Retornando los meses del año con un SelectListItem.
            return _mesesDelAnio;
        }

        public string ValidarMesDelAnio(string mesSeleccionado) 
        {
            switch (int.Parse(mesSeleccionado))
            {
                case 1:
                    return "1";
                    break;
                case 2:
                    return "2";
                    break;
                case 3:
                    return "3";
                    break;
                case 4:
                    return "4";
                    break;
                case 5:
                    return "5";
                    break;
                case 6:
                    return "6";
                    break;
                case 7:
                    return "7";
                    break;
                case 8:
                    return "8";
                    break;
                case 9:
                    return "9";
                    break;
                case 10:
                    return "10";
                    break;
                case 11:
                    return "11";
                    break;
                case 12:
                    return "12";
                    break;
                default:
                    return null;
            }
        }
    }
}