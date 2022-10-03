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
            Session["UsuarioSistema"] = "hcalvo";
            if (Session["UsuarioSistema"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                //HabilitaOpcionesPermisos();
                CargarGridActividades();
                CargarProyectos();
                CargarUsuarios();
                CargarFases(0);
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
                });
                gvActividad.DataSource = lstActividades;
                gvActividad.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        private string NombreCompleto(string nombre, string primer_apellido)
        {
            return nombre + " " + primer_apellido;
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

                ddlProyecto.DataSource = lstProyetos;
                ddlProyecto.DataTextField = "Nombre";
                ddlProyecto.DataValueField = "Id";
                ddlProyecto.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        private void CargarFases(int idProyecto)
        {
            try
            {
                List<ProyectoEntidad> lstProyetos = new List<ProyectoEntidad>();
                List<FaseEntidad> lstFases = new List<FaseEntidad>();

                if (idProyecto == 0)
                {
                    lstProyetos = ProyectoLogica.GetProyectos(new ProyectoEntidad()
                    {
                        Esquema = "dbo"
                    });

                    if (lstProyetos.Count > 0)
                    {
                        lstFases = FaseLogica.GetFases(new FaseEntidad()
                        {
                            Esquema = "dbo",
                            IdProyecto = lstProyetos[0].Id,
                            Opcion = 1
                        });

                        ddlFase.DataSource = lstFases;
                        ddlFase.DataTextField = "Nombre";
                        ddlFase.DataValueField = "Id";
                        ddlFase.DataBind();
                    }
                }
                else
                {
                    lstFases = FaseLogica.GetFases(new FaseEntidad()
                    {
                        Esquema = "dbo",
                        IdProyecto = idProyecto,
                        Opcion = 1
                    });
                    ddlFase.DataSource = lstFases;
                    ddlFase.DataTextField = "Nombre";
                    ddlFase.DataValueField = "Id";
                    ddlFase.DataBind();
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                UsuarioLogica usuarioLogica = new UsuarioLogica();
                List<UsuarioEntidad> lstUsuarios = new List<UsuarioEntidad>();
                lstUsuarios = usuarioLogica.GetUsuarios(new UsuarioEntidad()
                {
                    Esquema = "dbo"
                });
                lstUsuarios.ForEach(x => { x.Nombre = NombreCompleto(x.Nombre, x.Primer_Apellido); });
                ddlUsuario.DataSource = lstUsuarios;
                ddlUsuario.DataTextField = "Nombre";
                ddlUsuario.DataValueField = "Id";
                ddlUsuario.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        protected void ddlProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idProyecto = Convert.ToInt32(ddlProyecto.SelectedValue);
            CargarFases(idProyecto);
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
                    Fecha_Inicio = txbFechaInicio.Text,
                    Fecha_Estimada = txbFechaEstimada.Text,
                    IdFase = Convert.ToInt32(ddlFase.SelectedValue),
                    IdUsuario = Convert.ToInt32(ddlUsuario.SelectedValue),
                    IdEstado = 1,
                    Usuario = "hcalvo",
                    Esquema = "dbo",
                    Opcion = 0
                };
                if (!string.IsNullOrEmpty(hdnIdActividad.Value))
                {
                    actividad.Id = Convert.ToInt32(hdnIdActividad.Value);
                    actividad.IdFase = 0;
                    actividad.Fecha_Inicio = FormatoFecha(txbFechaInicio.Text);
                    actividad.Fecha_Estimada = FormatoFecha(txbFechaEstimada.Text);
                    hdnIdActividad.Value = "";
                }
                ActividadLogica.MantActividad(actividad);
                Mensaje("Aviso", "La actividad se guardó correctamente", true);
                LimpiarCampos();
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
            ddlProyecto.Enabled = true;
            ddlFase.Enabled = true;
            //ddlProyecto.SelectedIndex = 0;
            //CargarFases(0);
        }

        private bool CamposVacios()
        {
            if (ddlProyecto.Items.Count <= 0)
            {
                Mensaje("Aviso", "No hay proyectos disponibles. Por favor agregue uno para continuar", false);
                return true;
            }
            else if (ddlUsuario.Items.Count <= 0)
            {
                Mensaje("Aviso", "No hay usuarios disponibles. Por favor agregue uno para continuar", false);
                return true;
            }
            else if (ddlFase.Items.Count <= 0)
            {
                Mensaje("Aviso", "No hay fases disponibles. Por favor agregue una para continuar", false);
                return true;
            }
            else if (string.IsNullOrEmpty(txbDescripcion.Text))
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
            return false;
        }

        protected void gvActividad_PreRender(object sender, EventArgs e)
        {

        }

        protected void gvActividad_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);

                var id = gvActividad.Rows[index].Cells[0].Text;
                var idFase = gvActividad.Rows[index].Cells[1].Text;
                var idUsuario = gvActividad.Rows[index].Cells[2].Text;
                var descripcion = gvActividad.Rows[index].Cells[5].Text;
                var fecha_inicio = gvActividad.Rows[index].Cells[6].Text;
                var fecha_estimada = gvActividad.Rows[index].Cells[7].Text;

                if (e.CommandName == "Editar")
                {
                    hdnIdActividad.Value = Convert.ToInt32(id).ToString();
                    ddlProyecto.Enabled = false;
                    ddlFase.Enabled = false;
                    ddlUsuario.SelectedValue = idUsuario;
                    txbDescripcion.Text = descripcion;
                    txbFechaInicio.Text = fecha_inicio;
                    txbFechaEstimada.Text = fecha_estimada;
                }
                else if (e.CommandName == "Eliminar")
                {
                    //ClienteLogica.MantCliente(new ClienteEntidad { Id = Convert.ToInt32(id), Opcion = 1, Esquema = "dbo" });
                }
                else if (e.CommandName == "Finalizar")
                {
                    ActividadLogica.MantActividad(new ActividadEntidad { Id = Convert.ToInt32(id), Opcion = 0, Esquema = "dbo", Fecha_Finalizacion = DateTime.Now.ToString() });
                    CargarGridActividades();
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
            
        }

    }
}