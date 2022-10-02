using SIMP.Datos;
using SIMP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Logica
{
    public class PerfilLogica
    {
        PerfilDatos perfilDatos = new PerfilDatos();
        public List<PerfilEntidad> GetPerfiles(PerfilEntidad perfil)
        {
            return perfilDatos.GetPerfiles(perfil);
        }
        public void MantPerfil(PerfilEntidad perfil)
        {
            perfilDatos.MantPerfil(perfil);
        }
    }
}
