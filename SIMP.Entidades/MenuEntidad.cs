using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Entidades
{
    public class MenuEntidad:EntidadBase
    {
        public int Id { get; set; }
        public string Codigo_Padre { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public string Url { get; set; }
        public string Estado { get; set; }
        public bool Crear { get; set; }
        public bool Editar { get; set; }
        public bool Ver { get; set; }
        public bool Enviar { get; set; }
    }
}
