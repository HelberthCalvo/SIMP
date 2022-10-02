using SIMP.Datos;
using SIMP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Logica
{
    public class ActividadLogica
    {
        public static List<ActividadEntidad> GetActividades(ActividadEntidad actividad)
        {
            return ActividadDatos.GetActividades(actividad);
        }
        public static void MantActividad(ActividadEntidad actividad)
        {
            ActividadDatos.MantActividad(actividad);
        }
    }
}
