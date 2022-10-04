using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Entidades
{
    public class PrivilegioEntidad:EntidadBase
    {
        public int Id { get; set; }
        public int IdPerfil { get; set; }
        public int IdMenu { set; get; }
        public string Descripcion { set; get; }

        public bool EstadoPermiso { set; get; }
        public bool CrearPermiso { set; get; }
        public bool EditarPermiso { set; get; }
        public bool VerPermiso { set; get; }

        public string ListaMenu { set; get; }
        public string ListaPermisos { set; get; }
    }
}
