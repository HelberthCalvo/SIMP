using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Entidades
{
    public class ConfiguracionEntidad:EntidadBase
    {
        public long PK_CONFIGURACION { get; set; }
        public string DESCRIPCION { get; set; }
        public string NOMBRE_PAGINA { get; set; }
        public string ESTADO { get; set; }
        public string ESQUEMA { get; set; }
        public string USUARIO_CREACION { get; set; }
        public DateTime FECHA_CREACION { get; set; }
        public string USUARIO_MODIFICACION { get; set; }
        public DateTime FECHA_MODIFICACION { get; set; }
    }
}
