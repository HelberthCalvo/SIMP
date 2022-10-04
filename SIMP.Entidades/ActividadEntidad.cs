using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Entidades
{
    public class ActividadEntidad:EntidadBase
    {
        public int Id { get; set; }
        public int IdFase { get; set; }
        public int IdUsuario { get; set; }
        public int IdEstado { get; set; }
        public string NombreFase { get; set; }
        public string NombreUsuario { get; set; }
        public string Descripcion { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Estimada { get; set; }
        public string Fecha_Finalizacion { get; set; }
    }
}
