using SistemaGestorDeNominas.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaGestorDeNominas.Services.Nomina
{
    public class ValidarExistenciaDeNomina
    {
        public List<Models.Nomina> NominaExitente(int mesDeCreacionDeLaNomina, int year)
        {
            var nomina = new List<Models.Nomina>();
            using (var dbContext = new SistemaDeGestionDeNomina())
            {
                nomina = dbContext.Nomina.Where(n => n.MesDeLaNomina == mesDeCreacionDeLaNomina &&
                                                    n.AnioDeLaNomia == year).ToList();
                if (nomina.Count == 0)
                {
                    return null;
                }
            }
            return nomina;
        }
    }
}