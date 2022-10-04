using SIMP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SIMP.Logica.UTILITIES
{
    public static class PermisosHelper
    {
        public static bool tienePermiso(string nombrePermiso, string nombrePaginaDelPermiso)
        {
            MenuEntidad obMenuE = new MenuEntidad();
            MenuLogica obMenuL = new MenuLogica();
            MenuEntidad menuEncontrado = new MenuEntidad();

            try
            {
                string usuarioLogin = HttpContext.Current.Session["UsuarioSistema"].ToString();
                string Compania = HttpContext.Current.Session["Compañia"].ToString();
                Type myType = typeof(MenuEntidad);

                obMenuE.Opcion = 0;
                obMenuE.Usuario = usuarioLogin;
                obMenuE.Esquema = Compania;
                //obMenuE.IdPerfil = (HttpContext.Current.Session["UsuarioLoguiadoE"] as UsuarioEntidad).IdPerfil;

                if (HttpContext.Current.Session["menu_" + nombrePermiso + "_" + nombrePaginaDelPermiso] != null)
                {
                    menuEncontrado = (MenuEntidad)HttpContext.Current.Session["menu_" + nombrePermiso + "_" + nombrePaginaDelPermiso];
                }
                else
                {

                    List<MenuEntidad> listaMenu = obMenuL.ObtenerMenu(obMenuE);

                    menuEncontrado = listaMenu
                                        .Where(menu => menu.Url == nombrePaginaDelPermiso &&
                                                (bool)obtenerValorPropiedad(menu, nombrePermiso) == true)
                                        .FirstOrDefault();

                    HttpContext.Current.Session["menu_" + nombrePermiso + "_" + nombrePaginaDelPermiso] = menuEncontrado;

                }


            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error al verificar el permiso necesario. " + ex.Message);
            }

            return menuEncontrado != null;

        }


        private static object obtenerValorPropiedad(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
