using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Entidades
{
    public class BitacoraEntidad : EntidadBase
    {
        public int Id { get; set; }
        public string Tabla { get; set; }
        public string Accion { get; set; }
        public string Datos_Anteriores { get; set; }
        public string Datos_Nuevos { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public string UsuarioBitacora { get; set; }
    }
}
