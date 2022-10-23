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
            if (Session["UsuarioSistema"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                //HabilitaOpcionesPermisos();
                CargarGridFases();
                CargarProyectos();
                CargarTooltips();
            }
        }

        private void CargarTooltips()
        {
            try
            {
                foreach (GridViewRow item in gvFases.Rows)
                {
                    item.Cells[7].ToolTip = "Editar";
                    item.Cells[8].ToolTip = "Cambiar estado";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                });
                lstFases.ForEach(x => { x.NombreEstado = x.IdEstado == 1 ? "Activo" : "Inactivo"; });
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
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);

                var id = gvFases.Rows[index].Cells[0].Text;
                var idProyecto = gvFases.Rows[index].Cells[1].Text;
                var nombreEstado = gvFases.Rows[index].Cells[6].Text;
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
                else if (e.CommandName == "CambiarEstado")
                {
                    FaseLogica.MantFase(new FaseEntidad
                    {
                        Id = Convert.ToInt32(id),
                        Nombre = nombre,
                        Descripcion = descripcion,
                        Opcion = 0,
                        Esquema = "dbo",
                        IdEstado = nombreEstado == "Activo" ? 2 : 1,
                        IdProyecto = Convert.ToInt32(idProyecto)
                    });
                    Mensaje("Aviso", "Estado de la fase actualizado con éxito", true);
                    CargarGridFases();
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
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
            if (gvFases.Rows.Count > 0)
            {
                gvFases.UseAccessibleHeader = true;
                gvFases.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

    }
}