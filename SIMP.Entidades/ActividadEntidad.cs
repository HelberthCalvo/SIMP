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
        public int IdProyecto { get; set; }
        public int IdFase { get; set; }
        public int IdUsuario { get; set; }
        public int IdEstado { get; set; }
        public string NombreProyecto { get; set; }
        public string NombreFase { get; set; }
        public string NombreUsuario { get; set; }
        public string Descripcion { get; set; }
        public double HorasEstimadas { get; set; }
        public double HorasReales { get; set; }
    }
}
