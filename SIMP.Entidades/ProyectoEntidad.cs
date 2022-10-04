using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Entidades
{
    public class ProyectoEntidad : EntidadBase
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Nombre_Cliente { get; set; }
        public int IdEstado { get; set; }
        public string NombreEstado { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Estimada { get; set; }
        public string Fecha_Finalizacion { get; set; }
    }
}
