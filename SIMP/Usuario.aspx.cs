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
                CargarPerfiles();
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
                Mensaje("Usuario", "Debe ingresar todos los datos", false);
                return;
            }
            UsuarioEntidad usuario = new UsuarioEntidad();
            usuario.Id = !string.IsNullOrEmpty(txtId.Value.ToString()) ? Convert.ToInt32(txtId.Value) : 0;
            usuario.Perfil = !string.IsNullOrEmpty(ddlPerfil.SelectedValue.ToString()) ? Convert.ToInt32(ddlPerfil.SelectedValue) : 0;
            usuario.Estado = 1;
            usuario.Estado = rdbActivo.Checked? 1:2;
            usuario.Nombre = txtNombre.Text;
            usuario.Usuario_Sistema = txtUsuario.Text;
            usuario.Contrasena = txtContrasena.Text;
            usuario.Esquema = "dbo";
            usuario.Usuario = "hcalvo";
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
            else if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(txtContrasena.Text))
            {
                return true;
            }
            else if (ddlPerfil.Text == "-1")
            {
                return true;
            }
            //else if (string.IsNullOrEmpty(txtEstado.Text))
            //{
            //    return true;
            //}
            return false;
        }

        private void LimpiarCampos()
        {
            txtId.Value = string.Empty;
            //txtEstado.Text = string.Empty;
            txtNombre.Text = string.Empty;

            txtUsuario.Text = string.Empty;
            txtContrasena.Attributes["value"] = string.Empty;
            ddlPerfil.SelectedIndex = 0;
        }

        private void CargarGridUsuario()
        {
            try
            {
                List<UsuarioEntidad> lstUsuario = new List<UsuarioEntidad>();
                lstUsuario = new UsuarioLogica().GetUsuarios(new UsuarioEntidad() { Id = 0, Opcion = 0, Esquema = "dbo" });
                lstUsuario.ForEach(x =>
                {
                    x.NombreEstado = x.Estado == 1 ? "Activo" : "Inactivo";
                });
                gvUsuarios.DataSource = lstUsuario;
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }
        private void CargarPerfiles()
        {
            try
            {
                List<PerfilEntidad> lstPerfiles = new List<PerfilEntidad>();
                lstPerfiles.Add(new PerfilEntidad() { Id = 0, Descripcion = "Seleccione un perfil", Estado="1"});
                lstPerfiles.AddRange(new PerfilLogica().GetPerfiles(new PerfilEntidad() { Id = 0, Opcion = 0, Esquema = "dbo" }));
                
                ddlPerfil.DataSource = lstPerfiles;

                ddlPerfil.DataTextField = "Descripcion";
                ddlPerfil.DataValueField = "Id";
                ddlPerfil.DataBind();
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
            try
            {
                if (e.CommandName == "editar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    txtId.Value = gvUsuarios.DataKeys[index].Values[0].ToString();
                    txtNombre.Text = gvUsuarios.DataKeys[index].Values[1].ToString();
                    txtUsuario.Text = gvUsuarios.DataKeys[index].Values[4].ToString();
                    txtContrasena.Attributes["value"] = new UsuarioLogica().GetUsuarios(new UsuarioEntidad() { Id = 0, Opcion = 0, Esquema = "dbo" }).FirstOrDefault().Contrasena;
                    ddlPerfil.SelectedValue = gvUsuarios.DataKeys[index].Values[5].ToString();
                    //txtEstado.Text = gvUsuarios.DataKeys[index].Values[6].ToString();
                    string estado = gvUsuarios.DataKeys[index].Values[6].ToString();
                    if (estado == "1")
                    {
                        rdbInactivo.Checked = false;
                        rdbActivo.Checked = true;
                    }
                    else
                    {
                        rdbActivo.Checked = false;
                        rdbInactivo.Checked = true;
                    }
                    CargarGridUsuario();
                }
                else if (e.CommandName == "eliminar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    UsuarioEntidad usuario = new UsuarioEntidad();
                    usuario.Id = Convert.ToInt32(gvUsuarios.DataKeys[index].Values[0]);
                    usuario.Opcion = 1;
                    usuario.Esquema = "dbo";
                    usuario.Usuario = "hcalvo";

                    new UsuarioLogica().MantUsuario(usuario);

                    CargarGridUsuario();
                    Mensaje("Usuario", "Desactivado correctamente", true);
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void gvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[6].Text == "1")
                {
                    e.Row.Cells[6].Text = "Activo";

                }
                else
                {
                    e.Row.Cells[6].Text = "Inactivo";
                }
                e.Row.Cells[5].Text = new PerfilLogica().GetPerfiles(new PerfilEntidad() { Id = int.Parse(e.Row.Cells[5].Text), Opcion = 1, Esquema = "dbo" }).FirstOrDefault().Descripcion;
            }

        }
        //protected void gvOperaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "editar")
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        hfIdOperacion.Value = gvOperaciones.DataKeys[index].Values[0].ToString();
        //        try
        //        {
        //            ddlTipo.SelectedValue = gvOperaciones.DataKeys[index].Values[1].ToString();
        //        }
        //        catch (Exception)
        //        {
        //            ddlTipo.SelectedIndex = -1;
        //        }
        //        txtCuotasPorAño.Text = gvOperaciones.DataKeys[index].Values[2].ToString();
        //        txtPeriodos.Text = gvOperaciones.DataKeys[index].Values[14].ToString();
        //        txtDescripcion.Text = gvOperaciones.DataKeys[index].Values[4].ToString();
        //        txtBanco.Text = gvOperaciones.DataKeys[index].Values[5].ToString();
        //        txtFechaInicio.Text = gvOperaciones.DataKeys[index].Values[6].ToString();
        //        txtFechaFinal.Text = gvOperaciones.DataKeys[index].Values[7].ToString();

        //        decimal monto = Math.Round(Convert.ToDecimal(gvOperaciones.DataKeys[index].Values[8].ToString()), 2);
        //        decimal montoOtros = Math.Round(Convert.ToDecimal(gvOperaciones.DataKeys[index].Values[15].ToString()), 2);
        //        txtMontoPrincipal.Text = monto.ToString();
        //        txtMontoOtros.Text = montoOtros.ToString();
        //        txtTasaInteres.Text = gvOperaciones.DataKeys[index].Values[13].ToString();
        //        string estado = gvOperaciones.DataKeys[index].Values[10].ToString();
        //        txtDiaPago.Text = gvOperaciones.DataKeys[index].Values[11].ToString();
        //        if (estado == "1")
        //        {
        //            rdbEstadoInactivo.Checked = false;
        //            rdbEstadoActivo.Checked = true;
        //        }
        //        else
        //        {
        //            rdbEstadoActivo.Checked = false;
        //            rdbEstadoInactivo.Checked = true;
        //        }
        //    }
        //    else if (e.CommandName == "eliminar")
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        //int ID = Convert.ToInt32(gvOperaciones.DataKeys[0].Values[index]);
        //        OperacionBancariaE operacionBancaria = new OperacionBancariaE()
        //        {
        //            ID = Convert.ToInt32(gvOperaciones.DataKeys[index].Values[0].ToString()),
        //            Opcion = 1,
        //            Estado = "0",
        //            Esquema = Session["Compañia"].ToString(),
        //            Usuario = ((UsuarioEntidad)(Session["UsuarioSistema"])).Usuario_Sistema
        //        };

        //        new OperacionBancariasL().MantOperacionBancaria(operacionBancaria);

        //        LimpiarOperacionBancaria();
        //        CargarGridOperaciones();
        //        Mensaje("Operación Bancaría", "Desactivada correctamente", true);
        //    }
        //}
    }
}