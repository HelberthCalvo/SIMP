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
    public partial class Fase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UsuarioSistema"] = "hcalvo";
            if (Session["UsuarioSistema"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                //HabilitaOpcionesPermisos();
                CargarGridFases();
                CargarProyectos();
            }
        }

        private void CargarProyectos()
        {
            try
            {
                List<ProyectoEntidad> lstProyectos = new List<ProyectoEntidad>();
                lstProyectos = ProyectoLogica.GetProyectos(new ProyectoEntidad { Esquema = "dbo", IdEstado = 1 });

                ddlProyectos.DataSource = lstProyectos;

                ddlProyectos.DataTextField = "Nombre";
                ddlProyectos.DataValueField = "Id";
                ddlProyectos.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        private void Mensaje(string titulo, string msg, bool esCorrecto, string textoBoton = "Ok")
        {
            string icono = esCorrecto ? "success" : "error";
            string script = "Swal.fire({ title: '" + titulo + "!', text: '" + msg + "', icon: '" + icono + "', confirmButtonText: '" + textoBoton + "' })";
            ScriptManager.RegisterStartupScript(this, GetType(), "script", script, true);
        }

        private void CargarGridFases()
        {
            try
            {
                List<FaseEntidad> lstFases = new List<FaseEntidad>();
                lstFases = FaseLogica.GetFases(new FaseEntidad()
                {
                    Esquema = "dbo"
                }).FindAll(x => x.IdEstado == 1);

                gvFases.DataSource = lstFases;
                gvFases.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (CamposVacios())
                {
                    return;
                }
                FaseEntidad fase = new FaseEntidad()
                {
                    Nombre = txbNombre.Text,
                    Descripcion = txbDescripcion.Text,
                    IdProyecto = Convert.ToInt32(ddlProyectos.SelectedValue),
                    IdEstado = 1,
                    Usuario = "hcalvo",
                    Esquema = "dbo",
                    Opcion = 0
                };
                if (!string.IsNullOrEmpty(hdnIdFase.Value))
                {
                    fase.Id = Convert.ToInt32(hdnIdFase.Value);
                    fase.IdProyecto = Convert.ToInt32(hdnIdProyecto.Value);
                    hdnIdFase.Value = "";
                    hdnIdProyecto.Value = "";
                }
                FaseLogica.MantFase(fase);
                Mensaje("Aviso", "La fase se guardó correctamente", true);
                LimpiarCampos();
                CargarGridFases();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }


        protected void gvFases_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            var id = gvFases.Rows[index].Cells[0].Text;
            var idProyecto = gvFases.Rows[index].Cells[1].Text;
            var idEstado = gvFases.Rows[index].Cells[2].Text;
            var nombre = gvFases.Rows[index].Cells[3].Text;
            var descripcion = gvFases.Rows[index].Cells[4].Text;

            if (e.CommandName == "Editar")
            {
                hdnIdFase.Value = Convert.ToInt32(id).ToString();
                hdnIdProyecto.Value = Convert.ToInt32(idProyecto).ToString();
                txbNombre.Text = nombre;
                txbDescripcion.Text = descripcion;
                ddlProyectos.SelectedValue = idProyecto;
                ddlProyectos.Enabled = false;
            }
            else if (e.CommandName == "Eliminar")
            {
                //ClienteLogica.MantCliente(new ClienteEntidad { Id = Convert.ToInt32(id), Opcion = 1, Esquema = "dbo" });
            }
            else if (e.CommandName == "Finalizar")
            {
                FaseLogica.MantFase(new FaseEntidad { Id = Convert.ToInt32(id), Opcion = 0, Esquema = "dbo", IdEstado = 2, IdProyecto = Convert.ToInt32(idProyecto) });
                CargarGridFases();
            }
        }

        private bool CamposVacios()
        {
            if (ddlProyectos.Items.Count <= 0)
            {
                Mensaje("Aviso", "No hay proyectos disponibles. Por favor agregue uno para continuar", false);
                return true;
            }
            else if (string.IsNullOrEmpty(txbNombre.Text))
            {
                Mensaje("Aviso", "Debe ingresar un nombre", false);
                return true;
            }
            else if (string.IsNullOrEmpty(txbDescripcion.Text))
            {
                Mensaje("Aviso", "Debe ingresar una descripción", false);
                return true;
            }
            return false;
        }

        private void LimpiarCampos()
        {
            txbNombre.Text = string.Empty;
            txbDescripcion.Text = string.Empty;
            ddlProyectos.Enabled = true;
        }

        protected void gvFases_PreRender(object sender, EventArgs e)
        {

        }

    }
}