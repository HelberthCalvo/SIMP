using SIMP.Datos;
using SIMP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Logica
{
    public class UsuarioLogica
    {
        UsuarioDatos usuarioDatos = new UsuarioDatos();
        public List<UsuarioEntidad> GetUsuarios(UsuarioEntidad usuario)
        {
            return usuarioDatos.GetUsuarios(usuario);
        }
        public void MantUsuario(UsuarioEntidad usuario)
        {
            usuarioDatos.MantUsuario(usuario);
        }
    }
}
