using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Entidades
{
    public class UsuarioEntidad : EntidadBase
    {
        public int Id { get; set; }
        public int Perfil { get; set; }
        public int Estado { get; set; }
        public string Nombre { get; set; }
        public string Primer_Apellido { get; set; }
        public string Segundo_Apellido { get; set; }
        public string Usuario1 { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
    }
}
