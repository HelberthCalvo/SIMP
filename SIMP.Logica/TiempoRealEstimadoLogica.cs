using SIMP.Datos;
using SIMP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Logica
{
    public class TiempoRealEstimadoLogica
    {
        public static List<TiempoRealEstimadoEntidad> GetTiempoRealEstimado(TiempoRealEstimadoEntidad tiempoRealEstimadoEntidad)
        {
            return TiempoRealEstimadoDatos.GetTiempoRealEstimado(tiempoRealEstimadoEntidad);
        }
    }
}
