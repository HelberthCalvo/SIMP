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
    public partial class Bitacora : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioSistema"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                HabilitaOpcionesPermisos();
                CargarGridBitacora();
                CargarTooltips();
            }
        }
        private void HabilitaOpcionesPermisos()
        {
            try
            {
                string nombreUrl = Request.Url.Segments[Request.Url.Segments.Length - 1].ToString();
                if (Session["Permiso_" + nombreUrl] != null)
                {
                    MenuEntidad obMenu = (Session["Permiso_" + nombreUrl] as MenuEntidad);
                    string permisos = string.Empty;

                    if (!obMenu.CrearPermiso)
                    {
                        btnBuscar.Visible = false;

                        permisos += "- Crear ";
                    }

                    if (!obMenu.EditarPermiso)
                    {
                        gvBitacoras.Columns[6].Visible = false;
                        permisos += "- Editar ";
                    }

                    if (!obMenu.VerPermiso)
                    {
                        gvBitacoras.Visible = false;

                        permisos += "- Consultar ";
                    }

                    if (obMenu.EnviarPermiso)
                    {
                        //hdfPermisoEnviarCorreos.Value = "1";
                    }
                    else
                    {
                        //hdfPermisoEnviarCorreos.Value = "0";
                        permisos += "- Enviar Correos";
                    }

                    if (!string.IsNullOrEmpty(permisos))
                    {
                        mensajePermiso.Visible = true;
                        lblMensajePermisos.Text = "El usuario no cuenta con permisos para: " + permisos;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }
        private void CargarTooltips()
        {
            try
            {
                foreach (GridViewRow item in gvBitacoras.Rows)
                {
                    item.Cells[6].ToolTip = "Editar";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        protected void gvBitacoras_PreRender(object sender, EventArgs e)
        {
            if (gvBitacoras.Rows.Count > 0)
            {
                gvBitacoras.UseAccessibleHeader = true;
                gvBitacoras.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void CargarGridBitacora()
        {
            try
            {
                List<BitacoraEntidad> lstBitacora = new List<BitacoraEntidad>();
                DateTime fechaInicio = DateTime.Now.AddYears(-1);
                DateTime fechaFinal = DateTime.Now;
                lstBitacora = BitacoraLogica.GetBitacoras(new BitacoraEntidad() { Id = 0, Opcion = 0, Esquema = "dbo", FechaInicio = fechaInicio, FechaFinal = fechaFinal });
                gvBitacoras.DataSource = lstBitacora;
                gvBitacoras.DataBind();
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

        private void LimpiarCampos()
        {
            txbFechaInicio.Text = null;
            txbFechaFinal.Text = null;

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<BitacoraEntidad> lstBitacora = new List<BitacoraEntidad>();
                if (!string.IsNullOrEmpty(txbFechaInicio.Text) && !string.IsNullOrEmpty(txbFechaFinal.Text))
                {
                    DateTime fechaInicio = DateTime.Parse(txbFechaInicio.Text);
                    DateTime fechaFinal = DateTime.Parse(txbFechaFinal.Text).AddDays(1);
                    lstBitacora = BitacoraLogica.GetBitacoras(new BitacoraEntidad() { Id = 0, Opcion = 0, Esquema = "dbo", FechaInicio = fechaInicio, FechaFinal = fechaFinal });
                    
                }
                else
                {
                    DateTime fechaInicio = DateTime.Now.AddYears(-1);
                    DateTime fechaFinal = DateTime.Now;
                    lstBitacora = BitacoraLogica.GetBitacoras(new BitacoraEntidad() { Id = 0, Opcion = 0, Esquema = "dbo", FechaInicio = fechaInicio, FechaFinal = fechaFinal });
                }
                gvBitacoras.DataSource = lstBitacora;
                gvBitacoras.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }



        protected void gvBitacoras_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}