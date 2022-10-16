using SIMP.Entidades;
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