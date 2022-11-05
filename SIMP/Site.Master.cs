using SIMP.Entidades;
using SIMP.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIMP
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioSistema"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                Usuario_Sistema.Text = ((UsuarioEntidad)Session["UsuarioSistema"]).Usuario_Sistema;
                CargarMenu();
                SessionesAccesoPaginas();
            }
        }

        private void CargarMenu()
        {
            try
            {
                MenuEntidad obMenuEntidad = new MenuEntidad();
                MenuLogica obMenuL = new MenuLogica();
                string Compania = Session["Compañia"].ToString();
                string usuarioLogin = ((UsuarioEntidad)Session["UsuarioSistema"]).Usuario_Sistema;

                obMenuEntidad.Opcion = 0;
                obMenuEntidad.Esquema = Compania;
                obMenuEntidad.Usuario = usuarioLogin;
                obMenuEntidad.IdPerfil = (Session["UsuarioSistema"] as UsuarioEntidad).Perfil;
                obMenuEntidad.EstadoMenu = "1";
                obMenuEntidad.EstadoPermiso = "1";
                List<MenuEntidad> listaMenu = obMenuL.ObtenerMenu(obMenuEntidad);

                string PK_CODIGO_PADRE = "";
                if (listaMenu.Where(X => X.Descripcion == "Menú SIMP").FirstOrDefault() != null)
                {
                    PK_CODIGO_PADRE = listaMenu.Where(X => X.Descripcion == "Menú SIMP").FirstOrDefault().Id.ToString();
                }
                else
                {
                    PK_CODIGO_PADRE = "0";
                }

                List<MenuEntidad> listaMenuPadre = new List<MenuEntidad>();
                List<MenuEntidad> listaMenuHijos = new List<MenuEntidad>();

                listaMenuPadre = listaMenu.ToList().Where(x => x.Codigo_Padre == PK_CODIGO_PADRE).ToList();

                //sidebarnav.InnerHtml =
                //  "";


                foreach (MenuEntidad iMenu in listaMenuPadre)
                {
                    listaMenuHijos = listaMenu.ToList().Where(x => x.Codigo_Padre == iMenu.Id.ToString()).ToList();

                    if (listaMenuHijos.Count <= 0)
                    {
                        if (iMenu.Codigo_Padre == PK_CODIGO_PADRE)
                        {
                            if (iMenu.Descripcion.Equals("Seguridad") && iMenu.Estado == "1")
                            {
                                linkBtnSeguridad.Visible = true;
                            }
                            else if (iMenu.Descripcion.Equals("Proyecto") && iMenu.Estado == "1")
                            {
                                linkBtnProyecto.Visible = true;
                            }
                            else if (iMenu.Descripcion.Equals("Fases") && iMenu.Estado == "1")
                            {
                                linkBtnFase.Visible = true;
                            }
                            else if (iMenu.Descripcion.Equals("Actividades") && iMenu.Estado == "1")
                            {
                                linkBtnActividad.Visible = true;
                            }
                            else if (iMenu.Descripcion.Equals("Clientes") && iMenu.Estado == "1")
                            {
                                linkBtnCliente.Visible = true;
                            }
                            //sidebarnav.InnerHtml += $"<li class='sidebar-item'><a class='sidebar-link' href='/{iMenu.Url}'><span class='hide-menu'>{iMenu.Descripcion}</span></a></li>";
                        }
                    }
                    else
                    {
                        /*MENU HIJOS*/
                        if (iMenu.Codigo_Padre == PK_CODIGO_PADRE)
                        {
                            //sidebarnav.InnerHtml += "<li class='sidebar-item'>";
                            //sidebarnav.InnerHtml += $"<a class='sidebar-link has-arrow waves-effect waves-dark profile-dd' aria-expanded='false'><i class='{iMenu.Icono}'></i> <span class='hide-menu'>{iMenu.Descripcion}</span></a>";
                            //sidebarnav.InnerHtml += "<ul aria-expanded='false' class='collapse first-level'>";
                        }

                        foreach (MenuEntidad iMenuHijos in listaMenuHijos)
                        {
                            ObtenerSubMenus(iMenuHijos, listaMenu);
                        }

                        if (iMenu.Codigo_Padre == PK_CODIGO_PADRE)
                        {
                            //sidebarnav.InnerHtml += "</ul>";
                            //sidebarnav.InnerHtml += "</li>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string mesage = ex.Message;
                throw;
            }

        }


        private void ObtenerSubMenus(MenuEntidad pMenuHijos, List<MenuEntidad> plistaMenu)
        {
            try
            {
                List<MenuEntidad> listaMenuHijos = new List<MenuEntidad>();
                List<MenuEntidad> listaMenuTieneHijos = new List<MenuEntidad>();
                List<MenuEntidad> listaMenu = plistaMenu;

                listaMenuHijos = listaMenu.ToList().Where(x => x.Codigo_Padre == pMenuHijos.Id.ToString()).ToList();
                if (listaMenuHijos.Count == 0)
                {
                    //sidebarnav.InnerHtml += $"<li class='sidebar-item'><a class='sidebar-link' href='/{pMenuHijos.Url}'><span class='hide-menu'>{pMenuHijos.Descripcion}</span></a></li>";
                    return;
                }

                //sidebarnav.InnerHtml += "<li class='sidebar-item'>";
                //sidebarnav.InnerHtml += $"<a class='sidebar-link has-arrow waves-effect waves-dark profile-dd' aria-expanded='false'><i class='{pMenuHijos.Icono}'></i> <span class='hide-menu'>{pMenuHijos.Descripcion}</span></a>";
                //sidebarnav.InnerHtml += "<ul aria-expanded='false' class='collapse first-level'>";

                foreach (MenuEntidad iMenuHijos in listaMenuHijos)
                {
                    ObtenerSubMenus(iMenuHijos, listaMenu);
                }

                //sidebarnav.InnerHtml += "</ul>";
                //sidebarnav.InnerHtml += "</li>";

            }
            catch (Exception ex)
            {
                string mesage = ex.Message;
                throw;
            }
        }


        private void SessionesAccesoPaginas()
        {
            try
            {
                MenuEntidad obMenuEntidad = new MenuEntidad();
                MenuLogica obMenuL = new MenuLogica();
                string Compania = Session["Compañia"].ToString();
                string usuarioLogin = Session["UsuarioSistema"].ToString();

                obMenuEntidad.Opcion = 0;
                obMenuEntidad.Usuario = usuarioLogin;
                obMenuEntidad.Esquema = Compania;
                obMenuEntidad.IdPerfil = (Session["UsuarioSistema"] as UsuarioEntidad).Perfil;
                List<MenuEntidad> listaMenu = obMenuL.ObtenerMenu(obMenuEntidad);


                foreach (MenuEntidad iMenu in listaMenu.Where(x => !string.IsNullOrEmpty(x.Url)).ToList())
                {

                    Session.Remove(iMenu.Url);
                    Session.Remove("Permiso_" + iMenu.Url);


                    Session[iMenu.Url] = iMenu.EstadoPermiso;
                    Session["Permiso_" + iMenu.Url] = iMenu;
                }

            }
            catch (Exception ex)
            {
                string mesage = ex.Message;
                throw;
            }

        }

        protected void linkBtnCliente_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/Pages/Cliente/Index.aspx");
            Response.Redirect("~/Clientes.aspx");
        }

        protected void linkBtnUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Usuario.aspx");
        }

        protected void linkBtnProyecto_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Proyecto.aspx");
        }

        protected void linkBtnFase_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Fase.aspx");
        }

        protected void linkBtnActividad_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Actividad.aspx");
        }

        protected void linkBtnSeguridad_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Seguridad.aspx");
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session["UsuarioSistema"] = null;
            Response.Redirect("~/Login.aspx");
        }

    }
}