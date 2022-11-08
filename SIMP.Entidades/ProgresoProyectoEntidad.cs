using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Entidades
{
    public class ProgresoProyectoEntidad : EntidadBase
    {
        public int IdProyecto { get; set; }
        public string Nombre_Proyecto { get; set; }
        public string Nombre_Fase { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Final { get; set; }
        public string Porcentaje { get; set; }
    }
}
