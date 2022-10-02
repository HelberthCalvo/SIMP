using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Entidades
{
    public class PerfilEntidad : EntidadBase
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
    }
}
