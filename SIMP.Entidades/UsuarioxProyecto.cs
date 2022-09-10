using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Entidades
{
    public class UsuarioxProyecto : EntidadBase
    {
        public int IdUsuario { get; set; }
        public int IdProyecto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
