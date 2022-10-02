using SIMP.Datos;
using SIMP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Logica
{
    public class ProyectoLogica
    {
        public static List<ProyectoEntidad> GetProyectos(ProyectoEntidad proyecto)
        {
            return ProyectoDatos.GetProyectos(proyecto);
        }
        public static void MantProyecto(ProyectoEntidad proyecto)
        {
            ProyectoDatos.MantProyecto(proyecto);
        }
    }
}
