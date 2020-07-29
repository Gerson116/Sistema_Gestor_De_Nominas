using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestorDeNominas.Services.Nomina
{
    public interface INominaCRUD
    {
        List<Models.Nomina> NominaEmpleados(List<Models.Empleado> empleados);
        List<Models.Nomina> NuevaNomina(List<Models.Nomina> empleados, int mes, int year);
        List<Models.Nomina> ListadoDeNominas(); // Listado Total de las nominas almacenadas en base de dato.
        List<Models.Nomina> ListadoDeNominasFiltradaPorFecha(string fecha);
        List<Models.Nomina> ListadoDeNominasFiltradaPorFecha(int mes, int year);
        bool ModificarNominaExistente(int mes, int year);
    }
}
