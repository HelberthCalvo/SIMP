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
    public partial class Actividad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioSistema"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                HabilitaOpcionesPermisos();
                CargarGridActividades();
                CargarUsuarios();
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
                        btnGuardar.Visible = false;

                        permisos += "- Crear ";
                    }

                    if (!obMenu.EditarPermiso)
                    {
                        gvActividad.Columns[9].Visible = false;
                        gvActividad.Columns[10].Visible = false;
                        gvActividad.Columns[11].Visible = false;
                        permisos += "- Editar ";
                    }

                    if (!obMenu.VerPermiso)
                    {
                        gvActividad.Visible = false;

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
                foreach (GridViewRow item in gvActividad.Rows)
                {
                    item.Cells[9].ToolTip = "Editar";
                    item.Cells[10].ToolTip = "Cambiar estado";
                    item.Cells[11].ToolTip = "Marcar como finalizado";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private string FormatoFechaGridView(string fecha)
        {
            return fecha.Split(' ')[0];
        }

        private void CargarGridActividades()
        {
            try
            {
                List<ActividadEntidad> lstActividades = new List<ActividadEntidad>();
                lstActividades = ActividadLogica.GetActividades(new ActividadEntidad()
                {
                    Esquema = "dbo"
                });
                lstActividades.ForEach(x =>
                {
                    x.Fecha_Inicio = FormatoFechaGridView(x.Fecha_Inicio);
                    x.Fecha_Estimada = FormatoFechaGridView(x.Fecha_Estimada);
                    x.NombreEstado = x.IdEstado == 1 ? "Activo" : "Inactivo";
                });
                gvActividad.DataSource = lstActividades;
                gvActividad.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        private string NombreCompleto(string nombre, string primer_apellido, string segundo_apellido)
        {
            return nombre + " " + primer_apellido + " " + segundo_apellido;
        }

        private void CargarProyectos()
        {
            try
            {
                List<ProyectoEntidad> lstProyetos = new List<ProyectoEntidad>();
                lstProyetos = ProyectoLogica.GetProyectos(new ProyectoEntidad()
                {
                    Esquema = "dbo"
                });

                gvModalProyecto.DataSource = lstProyetos;
                gvModalProyecto.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        private bool CargarFases()
        {
            try
            {
                if (!string.IsNullOrEmpty(hdnIdProyecto.Value))
                {
                    List<FaseEntidad> lstFases = FaseLogica.GetFases(new FaseEntidad()
                    {
                        Esquema = "dbo",
                        IdProyecto = Convert.ToInt32(hdnIdProyecto.Value),
                        Opcion = 1
                    });

                    if (lstFases.Count > 0)
                    {
                        gvModalFase.DataSource = lstFases;
                        gvModalFase.DataBind();
                        return true;
                    }
                    else
                    {
                        Mensaje("Error", "No existen fases para este proyecto", false);
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
            return false;
        }

        private void CargarUsuarios()
        {
            try
            {
                UsuarioLogica usuarioLogica = new UsuarioLogica();
                List<UsuarioEntidad> lstUsuarios = new List<UsuarioEntidad>();
                lstUsuarios = new UsuarioLogica().GetUsuarios(new UsuarioEntidad() { Id = 0, Opcion = 0, Usuario = "hcalvo", Estado = 1, Esquema = "dbo", Contrasena = "" });

                gvModalUsuario.DataSource = lstUsuarios;
                gvModalUsuario.DataBind();
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

        private string FormatoFecha(string fecha)
        {
            var lstFecha = fecha.Split('/');
            string formato = lstFecha[1] + "/" + lstFecha[0] + "/" + lstFecha[2];
            return formato;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (CamposVacios())
                {
                    return;
                }

                ActividadEntidad actividad = new ActividadEntidad()
                {
                    Descripcion = txbDescripcion.Text,
                    Fecha_Inicio = FormatoFecha(txbFechaInicio.Text),
                    Fecha_Estimada = FormatoFecha(txbFechaEstimada.Text),
                    IdFase = Convert.ToInt32(hdnIdFase.Value),
                    IdUsuario = Convert.ToInt32(hdnIdUsuario.Value),
                    IdEstado = 1,
                    Usuario = "hcalvo",
                    Esquema = "dbo",
                    Opcion = 0
                };
                if (!string.IsNullOrEmpty(hdnIdActividad.Value))
                {
                    actividad.Id = Convert.ToInt32(hdnIdActividad.Value);
                    actividad.Fecha_Inicio = FormatoFecha(txbFechaInicio.Text);
                    actividad.Fecha_Estimada = FormatoFecha(txbFechaEstimada.Text);

                }
                ActividadLogica.MantActividad(actividad);
                Mensaje("Aviso", "La actividad se guardó correctamente", true);
                LimpiarCampos();
                hdnIdActividad.Value = "";
                CargarGridActividades();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        private void LimpiarCampos()
        {
            txbDescripcion.Text = string.Empty;
            txbFechaInicio.Text = string.Empty;
            txbFechaEstimada.Text = string.Empty;
            btnModalProyecto.Enabled = true;
            btnModalFase.Enabled = true;
            hdnIdProyecto.Value = string.Empty;
            hdnIdFase.Value = string.Empty;
            hdnIdUsuario.Value = string.Empty;
            hdnIdActividad.Value = string.Empty;
        }

        private bool CamposVacios()
        {
            if (string.IsNullOrEmpty(txbDescripcion.Text))
            {
                Mensaje("Aviso", "Debe ingresar una descripción", false);
                return true;
            }
            else if (string.IsNullOrEmpty(txbFechaInicio.Text))
            {
                Mensaje("Aviso", "Debe ingresar una fecha de inicio", false);
                return true;
            }
            else if (string.IsNullOrEmpty(txbFechaEstimada.Text))
            {
                Mensaje("Aviso", "Debe ingresar una fecha de finalización", false);
                return true;
            }
            else if (string.IsNullOrEmpty(hdnIdProyecto.Value))
            {
                Mensaje("Aviso", "Debe seleccionar un proyecto", false);
                return true;
            }
            else if (string.IsNullOrEmpty(hdnIdFase.Value))
            {
                Mensaje("Aviso", "Debe seleccionar una fase", false);
                return true;
            }
            else if (string.IsNullOrEmpty(hdnIdUsuario.Value))
            {
                Mensaje("Aviso", "Debe seleccionar un usuario", false);
                return true;
            }
            return false;
        }

        protected void gvActividad_PreRender(object sender, EventArgs e)
        {
            if (gvActividad.Rows.Count > 0)
            {
                gvActividad.UseAccessibleHeader = true;
                gvActividad.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvActividad_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);

                var id = gvActividad.Rows[index].Cells[0].Text;
                var idFase = gvActividad.Rows[index].Cells[1].Text;
                var idUsuario = gvActividad.Rows[index].Cells[2].Text;
                var nombreUsuario = gvActividad.Rows[index].Cells[4].Text;
                var descripcion = gvActividad.Rows[index].Cells[5].Text;
                var fecha_inicio = gvActividad.Rows[index].Cells[6].Text;
                var fecha_estimada = gvActividad.Rows[index].Cells[7].Text;
                var nombreEstado = gvActividad.Rows[index].Cells[8].Text;

                if (e.CommandName == "Editar")
                {
                    hdnIdActividad.Value = Convert.ToInt32(id).ToString();
                    hdnIdFase.Value = idFase;
                    btnModalProyecto.Enabled = false;
                    btnModalFase.Enabled = false;
                    hdnIdUsuario.Value = idUsuario;
                    txbDescripcion.Text = descripcion;
                    txbFechaInicio.Text = fecha_inicio;
                    txbFechaEstimada.Text = fecha_estimada;
                    txtNombreUsuario.Text = nombreUsuario;
                }
                else if (e.CommandName == "CambiarEstado")
                {
                    ActividadLogica.MantActividad(new ActividadEntidad
                    {
                        Id = Convert.ToInt32(id),
                        Opcion = 0,
                        Esquema = "dbo",
                        Descripcion = descripcion,
                        Fecha_Inicio = FormatoFecha(fecha_inicio),
                        Fecha_Estimada = FormatoFecha(fecha_estimada),
                        IdEstado = nombreEstado == "Activo" ? 2 : 1,
                        IdFase = Convert.ToInt32(idFase),
                        IdUsuario = Convert.ToInt32(idUsuario)
                    });
                    Mensaje("Aviso", "Estado de la actividad actualizado con éxito", true);
                    CargarGridActividades();
                }
                else if (e.CommandName == "Finalizar")
                {
                    ActividadLogica.MantActividad(new ActividadEntidad
                    {
                        Id = Convert.ToInt32(id),
                        Descripcion = descripcion,
                        Fecha_Inicio = FormatoFecha(fecha_inicio),
                        Fecha_Estimada = FormatoFecha(fecha_estimada),
                        IdEstado = 2,
                        IdFase = Convert.ToInt32(idFase),
                        IdUsuario = Convert.ToInt32(idUsuario),
                        Opcion = 0,
                        Esquema = "dbo",
                        Fecha_Finalizacion = FormatoFecha(DateTime.Now.ToString())
                    });
                    Mensaje("Aviso", "Actividad finalizada con éxito", true);
                    CargarGridActividades();
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void gvModalProyecto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Seleccionar")
                {
                    hdnIdProyecto.Value = gvModalProyecto.Rows[index].Cells[0].Text;
                    txtNombreProyecto.Text = gvModalProyecto.Rows[index].Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this, GetType(), "modalProyecto", "$('#modalProyecto').modal('hide')", true);
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        protected void btnModalProyecto_Click(object sender, EventArgs e)
        {
            CargarProyectos();
            ScriptManager.RegisterStartupScript(this, GetType(), "modalProyecto", "$('#modalProyecto').modal('show')", true);
        }

        protected void gvModalFase_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Seleccionar")
                {
                    hdnIdFase.Value = gvModalFase.Rows[index].Cells[0].Text;
                    txtNombreFase.Text = gvModalFase.Rows[index].Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this, GetType(), "modalFase", "$('#modalFase').modal('hide')", true);
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        protected void btnModalFase_Click(object sender, EventArgs e)
        {
            if (CargarFases())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "modalFase", "$('#modalFase').modal('show')", true);
            }
            else
            {
                Mensaje("Error", "Debe seleccionar un proyecto primero", false);
            }
        }

        protected void btnModalUsuario_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
            ScriptManager.RegisterStartupScript(this, GetType(), "modalUsuario", "$('#modalUsuario').modal('show')", true);
        }

        protected void gvModalUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Seleccionar")
                {
                    hdnIdUsuario.Value = gvModalUsuario.Rows[index].Cells[0].Text;
                    txtNombreUsuario.Text = gvModalUsuario.Rows[index].Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this, GetType(), "modalUsuario", "$('#modalUsuario').modal('hide')", true);
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }
    }
}