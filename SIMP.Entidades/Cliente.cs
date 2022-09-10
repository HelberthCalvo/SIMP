using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Primer_Apellido { get; set; }
        public string Segundo_Apellido { get; set; }
        public string Correo_Electronico { get; set; }
        public string Telefono { get; set; }
    }
}
