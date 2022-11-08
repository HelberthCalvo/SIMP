using SIMP.Datos;
using SIMP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Logica
{
    public class ProgresoProyectoLogica
    {
        public static List<ProgresoProyectoEntidad> GetProgresoProyecto(ProgresoProyectoEntidad progresoProyecto)
        {
            return ProgresoProyectoDatos.GetProgresoProyecto(progresoProyecto);
        }
    }
}
