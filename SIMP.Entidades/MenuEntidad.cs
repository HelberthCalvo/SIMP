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
        public int IdPerfil { get; set; }
        public string Codigo_Padre { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public string Url { get; set; }
        public string EstadoMenu { get; set; }
        public bool CrearMenu { get; set; }
        public bool EditarMenu { get; set; }
        public bool VerMenu { get; set; }
        public bool EnviarMenu { get; set; }
        public string EstadoPermiso { get; set; }
        public bool CrearPermiso { get; set; }
        public bool EditarPermiso { get; set; }
        public bool VerPermiso { get; set; }
        public bool EnviarPermiso { get; set; }
    }
}
