using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaGestorDeNominas.Services.Empleado
{
    public class FiltroParaEmpleado
    {
        // Esta clase solo la utilizo para filtrar información sobre el empleado.
        private SEmpleadoCRUD _objSEmpleadoCRUD;

        public FiltroParaEmpleado()
        {
            _objSEmpleadoCRUD = new SEmpleadoCRUD();
        }
        public List<Models.Empleado> ListadoEmpleadoPorEstado(int estado_A_Filtrar) 
        {
            // Este metodo lo utilizo para retornar un listado de empleados,
            // dependiendo de si su estado es 1-"Activo" o 0-Inactivo.
            var listadoEmpleado = new List<Models.Empleado>();
            switch (estado_A_Filtrar)
            {
                case 0:
                    listadoEmpleado = _objSEmpleadoCRUD.ListadoEmpleadoInactivos();
                    break;
                case 1:
                    listadoEmpleado = _objSEmpleadoCRUD.ListadoEmpleadoActivos();
                    break;
                default:
                    listadoEmpleado = null;
                    break;
            }
            return listadoEmpleado;
        }
    }
}