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
        private List<SelectListItem> _years;
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
            _years = new List<SelectListItem> 
            {
                new SelectListItem { Text = "2020", Value="2020" },
                new SelectListItem { Text = "2021", Value="2021" },
                new SelectListItem { Text = "2022", Value="2022" },
                new SelectListItem { Text = "2023", Value="2023" },
                new SelectListItem { Text = "2024", Value="2024" }
            };
        }
        public List<SelectListItem> Meses()
        {
            // Retornando los meses del año con un SelectListItem.
            return _mesesDelAnio;
        }

        public List<SelectListItem> Years() 
        {
            //  Retornando los años
            return _years;
        }

        public string ValidarMesDelAnio(string mesSeleccionado) 
        {
            switch (int.Parse(mesSeleccionado))
            {
                case 1:
                    return "Enero";
                    break;
                case 2:
                    return "Febrero";
                    break;
                case 3:
                    return "Marzo";
                    break;
                case 4:
                    return "Abril";
                    break;
                case 5:
                    return "Mayo";
                    break;
                case 6:
                    return "Junio";
                    break;
                case 7:
                    return "Julio";
                    break;
                case 8:
                    return "Agosto";
                    break;
                case 9:
                    return "Septiembre";
                    break;
                case 10:
                    return "Octubre";
                    break;
                case 11:
                    return "Noviembre";
                    break;
                case 12:
                    return "Diciembre";
                    break;
                default:
                    return null;
            }
        }
    }
}