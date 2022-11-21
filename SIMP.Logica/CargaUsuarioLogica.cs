using SIMP.Datos;
using SIMP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Logica
{
    public class CargaUsuarioLogica
    {
        public static List<CargaTrabajoEntidad> GetCargaUsuarios(CargaTrabajoEntidad cargaTrabajoEntidad)
        {
            return CargaUsuarioDatos.GetCargaTrabajo(cargaTrabajoEntidad);
        }
    }
}
