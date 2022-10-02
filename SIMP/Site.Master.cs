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

        }

        protected void linkBtnCliente_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/Pages/Cliente/Index.aspx");
            Response.Redirect("~/Cliente.aspx");
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
    }
}