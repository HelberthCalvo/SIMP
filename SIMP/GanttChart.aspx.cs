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
    public partial class GanttChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioSistema"] == null || Session["IdProyectoGantt"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            CargarDatos();
        }

        private void CargarDatos()
        {
            int idProyecto = Convert.ToInt32(Session["IdProyectoGantt"].ToString());

            var listaActividades = ActividadLogica.GetActividades(new ActividadEntidad()
            {
                IdProyecto = idProyecto,
                Opcion = 2
            });

        }
    }
}