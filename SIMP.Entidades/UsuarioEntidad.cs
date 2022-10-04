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
        public string Usuario_Sistema { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public int Perfil { get; set; }
        public string PerfilNombre { get; set; }
        public string Contrasena { get; set; }
        public int Estado { get; set; }
        public string Cambio_Clave { get; set; }
    }
}
