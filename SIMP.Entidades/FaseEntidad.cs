using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Entidades
{
    public class FaseEntidad : EntidadBase
    {
        public int Id { get; set; }
        public int IdEstado { get; set; }
        public int IdProyecto { get; set; }
        public string NombreProyecto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
