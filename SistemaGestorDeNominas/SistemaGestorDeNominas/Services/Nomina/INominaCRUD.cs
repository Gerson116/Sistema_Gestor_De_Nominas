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
        void NuevaNomina(List<Models.Nomina> empleados);
    }
}
