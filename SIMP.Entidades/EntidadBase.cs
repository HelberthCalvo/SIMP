using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Entidades
{
  public class EntidadBase
  {
    public string Usuario { get; set; }
    public int Opcion { get; set; }
    public string Estado { get; set; }
    public string NombreEstado { get; set; }
    public string Esquema { get; set; }
  }
}
