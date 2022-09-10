using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Entidades
{
    public class Actividad
    {
        public int Id { get; set; }
        public int IdFase { get; set; }
        public int IdUsuario { get; set; }
        public int IdEstado { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Estimada { get; set; }
        public DateTime Fecha_Finalizacion { get; set; }
    }
}
