using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Entidades
{
    public class CargaTrabajoEntidad : EntidadBase
    {
        public int IdUsuario { get; set; }
        public string Nombre_Cliente { get; set; }
        public string Nombre_Proyecto { get; set; }
        public string Nombre_Fase { get; set; }
        public string Nombre_Actividad { get; set; }
        public string Nombre_Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Final { get; set; }
    }
}
