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
    public partial class Usuario : System.Web.UI.Page
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
                CargarGridUsuario();
            }
        }

        //private void HabilitaOpcionesPermisos()
        //{
        //    try
        //    {
        //        string nombreUrl = Request.Url.Segments[Request.Url.Segments.Length - 1].ToString();
        //        if (Session["Permiso_" + nombreUrl] != null)
        //        {
        //            MenuE obMenu = (Session["Permiso_" + nombreUrl] as MenuE);
        //            string permisos = string.Empty;


        //            if (!obMenu.CrearPermiso)
        //            {
        //                btnGuardar.Visible = false;
        //                permisos += "- Crear ";
        //            }

        //            if (!obMenu.EditarPermiso)
        //            {
        //                gvMoneda.Columns[4].Visible = false;
        //                gvMoneda.Columns[5].Visible = false;
        //                permisos += "- Editar ";
        //            }

        //            if (!obMenu.VerPermiso)
        //            {
        //                gvMoneda.Visible = false;
        //                permisos += "- Consultar ";
        //            }

        //            if (!string.IsNullOrEmpty(permisos))
        //            {
        //                mensajePermiso.Visible = true;
        //                lblMensajePermisos.Text = "El usuario no cuenta con permisos para: " + permisos;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
        //    }
        //}

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                Mensaje("Moneda", "Debe ingresar todos los datos", false);
                return;
            }
            UsuarioEntidad usuario = new UsuarioEntidad();
            usuario.Id = !string.IsNullOrEmpty(txtId.Text.ToString()) ? Convert.ToInt32(txtId.Text) : 0;
            usuario.Rol = !string.IsNullOrEmpty(txtRol.Text.ToString()) ? Convert.ToInt32(txtRol.Text) : 0;
            usuario.Estado = !string.IsNullOrEmpty(txtEstado.Text.ToString()) ? Convert.ToInt32(txtEstado.Text) : 0;
            usuario.Nombre = txtNombre.Text;
            usuario.Primer_Apellido = txtPrimer_Apellido.Text;
            usuario.Segundo_Apellido = txtSegundo_Apellido.Text;
            usuario.Usuario1 = txtUsuario.Text;
            usuario.Contrasena = txtContrasena.Text;
            usuario.Esquema = "dbo";
            usuario.Usuario = "Helberth C";
            new UsuarioLogica().MantUsuario(usuario);

            LimpiarCampos();
            CargarGridUsuario();
            Mensaje("Usuario", "Guardada correctamente", true);
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(txtPrimer_Apellido.Text))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(txtSegundo_Apellido.Text))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(txtContrasena.Text))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(txtRol.Text))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(txtEstado.Text))
            {
                return true;
            }
            return false;
        }

        private void LimpiarCampos()
        {
            txtId.Text = null;
            txtRol.Text = string.Empty;
            txtEstado.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtPrimer_Apellido.Text = string.Empty;
            txtSegundo_Apellido.Text = string.Empty;
            txtUsuario.Text = string.Empty;
            txtContrasena.Text = string.Empty;
            string estado = string.Empty;
        }

        private void CargarGridUsuario()
        {
            try
            {
                List<UsuarioEntidad> lstUsuario = new List<UsuarioEntidad>();
                lstUsuario = new UsuarioLogica().GetUsuarios(new UsuarioEntidad() { Id = 0, Opcion = 0, Esquema = "dbo" });
                gvUsuarios.DataSource = lstUsuario;
                gvUsuarios.DataBind();
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

        protected void gvUsuarios_PreRender(object sender, EventArgs e)
        {
            if (gvUsuarios.Rows.Count > 0)
            {
                gvUsuarios.UseAccessibleHeader = true;
                gvUsuarios.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //try
            //{
            //    if (e.CommandName == "editar")
            //    {
            //        int index = Convert.ToInt32(e.CommandArgument);
            //        hfIdFlowMoneda.Value = gvMoneda.DataKeys[index].Values[0].ToString();
            //        txtDescripcion.Text = gvMoneda.DataKeys[index].Values[1].ToString();
            //        txtSimbolo.Text = gvMoneda.DataKeys[index].Values[2].ToString();
            //        txtTipoCambio.Text = gvMoneda.DataKeys[index].Values[3].ToString();
            //        string estado = gvMoneda.DataKeys[index].Values[4].ToString();
            //        if (estado == "1")
            //        {
            //            rdbInactivo.Checked = false;
            //            rdbActivo.Checked = true;
            //        }
            //        else
            //        {
            //            rdbActivo.Checked = false;
            //            rdbInactivo.Checked = true;
            //        }
            //    }
            //    else if (e.CommandName == "eliminar")
            //    {
            //        int index = Convert.ToInt32(e.CommandArgument);
            //        MonedaE moneda = new MonedaE();
            //        moneda.ID = Convert.ToInt32(gvMoneda.DataKeys[index].Values[0]);
            //        moneda.Descripcion = gvMoneda.DataKeys[index].Values[1].ToString();
            //        moneda.Simbolo = gvMoneda.DataKeys[index].Values[2].ToString();
            //        moneda.TipoCambio = Convert.ToDecimal(gvMoneda.DataKeys[index].Values[3]);
            //        moneda.Estado = "0";
            //        moneda.Esquema = Session["Compañia"].ToString();
            //        moneda.Usuario = Session["UsuarioSistema"].ToString();

            //        new MonedaL().MantMonedas(moneda);

            //        CargarGridMoneda();
            //        Mensaje("Moneda", "Desactivada correctamente", true);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            //}
        }
    }
}