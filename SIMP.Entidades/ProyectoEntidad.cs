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
        public int IdEstado { get; set; }
        public string Nombre { get; set; }
        public int Descripcion { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Estimada { get; set; }
        public DateTime Fecha_Finalizacion { get; set; }
    }
}
