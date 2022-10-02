using SIMP.Datos;
using SIMP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Logica
{
    public class FaseLogica
    {
        public static List<FaseEntidad> GetFases(FaseEntidad fase)
        {
            return FaseDatos.GetFases(fase);
        }
        public static void MantFase(FaseEntidad fase)
        {
            FaseDatos.MantFase(fase);
        }
    }
}
