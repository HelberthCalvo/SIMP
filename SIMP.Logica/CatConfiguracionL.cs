using SIMP.Datos;
using SIMP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Logica
{
    public class CatConfiguracionL
    {
        CatConfiguracionDatos CatConfiguracionD = new CatConfiguracionDatos();
        public List<CatConfiguracionE> ObtenerCatConfiguracion(CatConfiguracionE CatConfiguracion)
        {
            return CatConfiguracionD.ObtieneCatConfiguracion(CatConfiguracion);

        }


        public void Mantenimiento(CatConfiguracionE CatConfiguracion)
        {
            CatConfiguracionD.Mantenimiento(CatConfiguracion);
        }
    }
}
