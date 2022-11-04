using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Entidades
{
    public class CatConfiguracionE:EntidadBase
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public string Llave01 { get; set; }
        public string Llave02 { get; set; }
        public string Llave03 { get; set; }
        public string Llave04 { get; set; }
        public string Llave05 { get; set; }
        public string Llave06 { get; set; }
        public string Valor { get; set; }
    }
}
