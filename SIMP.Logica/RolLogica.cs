using SIMP.Datos;
using SIMP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Logica
{
    public class RolLogica
    {
        RolDatos rolDatos = new RolDatos();
        public List<RolEntidad> GetRoles(RolEntidad rol)
        {
            return rolDatos.GetRoles(rol);
        }
        public void MantRol(RolEntidad rol)
        {
            rolDatos.MantRol(rol);
        }
    }
}
