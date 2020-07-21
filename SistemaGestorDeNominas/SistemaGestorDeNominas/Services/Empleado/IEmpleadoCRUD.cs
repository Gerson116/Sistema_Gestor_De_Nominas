using SistemaGestorDeNominas.Models.DTOs;
using SistemaGestorDeNominas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestorDeNominas.Services.Empleado
{
    public interface IEmpleadoCRUD
    {
        EmpleadoDTO PerfilEmpleado(int id_empleado);
        List<Models.Empleado> ListadoEmpleadoActivos();
        List<Models.Empleado> ListadoEmpleadoInactivos();
        void AgregarEmpleado(EmpleadoDTO datosDelEmpleado);
        void ModificarEmpleado(EmpleadoDTO datosDelEmpleado);
        bool BloquearEmpleado(int id_empleado);
    }
}
