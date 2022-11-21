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
    public partial class Proyecto : System.Web.UI.Page
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
                CargarGridProyectos();
                CargarTooltips();
            }
        }

        private void CargarTooltips()
        {
            try
            {
                foreach (GridViewRow item in gvProyectos.Rows)
                {
                    item.Cells[8].ToolTip = "Editar";
                    item.Cells[9].ToolTip = "Cambiar estado";
                    item.Cells[10].ToolTip = "Marcar como finalizado";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                        gvProyectos.Columns[8].Visible = false;
                        gvProyectos.Columns[9].Visible = false;
                        gvProyectos.Columns[10].Visible = false;
                        permisos += "- Editar ";
                    }

                    if (!obMenu.VerPermiso)
                    {
                        gvProyectos.Visible = false;
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
                }
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

        private void CargarClientes()
        {
            try
            {
                List<ClienteEntidad> lstClientes = new List<ClienteEntidad>();
                lstClientes = ClienteLogica.GetClientes(new ClienteEntidad { Esquema = "dbo" });
                lstClientes.ForEach(x => { x.Nombre = NombreCompleto(x.Nombre, x.Primer_Apellido, x.Segundo_Apellido); });

                gvModalCliente.DataSource = lstClientes;
                gvModalCliente.DataBind();
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

        private string FormatoFechaGridView(string fecha)
        {
            return fecha.Split(' ')[0];
        }

        private void CargarGridProyectos()
        {
            try
            {
                List<ProyectoEntidad> lstProyetos = new List<ProyectoEntidad>();
                lstProyetos = ProyectoLogica.GetProyectos(new ProyectoEntidad()
                {
                    Esquema = "dbo"
                });
                lstProyetos.ForEach(x => {
                    x.Fecha_Inicio = FormatoFechaGridView(x.Fecha_Inicio);
                    x.Fecha_Estimada = FormatoFechaGridView(x.Fecha_Estimada);
                    x.Nombre = x.Nombre.Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u').Replace('ñ', 'n');
                    x.Descripcion = x.Descripcion.Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u').Replace('ñ', 'n');
                });
                lstProyetos.ForEach(x => { x.NombreEstado = x.IdEstado == 1 ? "Activo" : "Inactivo"; });
                gvProyectos.DataSource = lstProyetos;
                gvProyectos.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message, false);
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

                ProyectoEntidad proyecto = new ProyectoEntidad()
                {
                    Nombre = txbNombre.Text,
                    Descripcion = txbDescripcion.Text,
                    Fecha_Inicio = FormatoFecha(txbFechaInicio.Text),
                    Fecha_Estimada = FormatoFecha(txbFechaEstimada.Text),
                    IdCliente = Convert.ToInt32(hdnIdCliente.Value),
                    IdEstado = 1,
                    Usuario = "hcalvo",
                    Esquema = "dbo",
                    Opcion = 0
                };
                if (!string.IsNullOrEmpty(hdnIdProyecto.Value))
                {
                    proyecto.Id = Convert.ToInt32(hdnIdProyecto.Value);
                    proyecto.Fecha_Inicio = FormatoFecha(txbFechaInicio.Text);
                    proyecto.Fecha_Estimada = FormatoFecha(txbFechaEstimada.Text);
                }
                ProyectoLogica.MantProyecto(proyecto);
                Mensaje("Aviso", "El proyecto se guardó correctamente", true);
                hdnIdProyecto.Value = "";
                LimpiarCampos();
                CargarGridProyectos();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        private bool CamposVacios()
        {
            if (string.IsNullOrEmpty(txbNombre.Text))
            {
                Mensaje("Aviso", "Debe ingresar un nombre", false);
                return true;
            }
            else if (string.IsNullOrEmpty(txbDescripcion.Text))
            {
                Mensaje("Aviso", "Debe ingresar una descripción", false);
                return true;
            }
            else if (string.IsNullOrEmpty(hdnIdCliente.Value))
            {
                Mensaje("Aviso", "Debe seleccionar un cliente", false);
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

        private void LimpiarCampos()
        {
            txbNombre.Text = string.Empty;
            txbDescripcion.Text = string.Empty;
            txbFechaInicio.Text = string.Empty;
            txbFechaEstimada.Text = string.Empty;
            txtNombreCliente.Text = string.Empty;
            hdnIdProyecto.Value = string.Empty;
            hdnIdCliente.Value = string.Empty;
        }

        protected void gvProyectos_PreRender(object sender, EventArgs e)
        {
            if (gvProyectos.Rows.Count > 0)
            {
                gvProyectos.UseAccessibleHeader = true;
                gvProyectos.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvProyectos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);

                var id = gvProyectos.Rows[index].Cells[0].Text;
                var idCliente = gvProyectos.Rows[index].Cells[1].Text;
                var nombre = gvProyectos.Rows[index].Cells[2].Text;
                var nombreCliente = gvProyectos.Rows[index].Cells[3].Text;
                var descripcion = gvProyectos.Rows[index].Cells[4].Text;
                var fecha_inicio = gvProyectos.Rows[index].Cells[5].Text;
                var fecha_estimada = gvProyectos.Rows[index].Cells[6].Text;
                var nombreEstado = gvProyectos.Rows[index].Cells[7].Text;

                if (e.CommandName == "Editar")
                {
                    hdnIdProyecto.Value = Convert.ToInt32(id).ToString();
                    txbNombre.Text = nombre;
                    txbDescripcion.Text = descripcion;
                    hdnIdCliente.Value = idCliente;
                    txbFechaInicio.Text = fecha_inicio;
                    txbFechaEstimada.Text = fecha_estimada;
                    txtNombreCliente.Text = nombreCliente;
                }
                else if (e.CommandName == "CambiarEstado")
                {
                    ProyectoLogica.MantProyecto(new ProyectoEntidad
                    {
                        Id = Convert.ToInt32(id),
                        Descripcion = descripcion,
                        Nombre = nombre,
                        Opcion = 0,
                        Fecha_Inicio = FormatoFecha(fecha_inicio),
                        Fecha_Estimada = FormatoFecha(fecha_estimada),
                        Esquema = "dbo",
                        IdEstado = nombreEstado == "Activo" ? 2 : 1,
                        IdCliente = Convert.ToInt32(idCliente)
                    });
                    Mensaje("Aviso", "Estado del proyecto actualizado con éxito", true);
                    CargarGridProyectos();
                }
                else if (e.CommandName == "Finalizar")
                {
                    ProyectoLogica.MantProyecto(new ProyectoEntidad { 
                        Id = Convert.ToInt32(id), 
                        Opcion = 0, 
                        Esquema = "dbo", 
                        Descripcion = descripcion,
                        Nombre = nombre,
                        Fecha_Inicio = FormatoFecha(fecha_inicio),
                        Fecha_Estimada = FormatoFecha(fecha_estimada),
                        IdCliente = Convert.ToInt32(idCliente), 
                        IdEstado = 2,
                        Fecha_Finalizacion = FormatoFecha(DateTime.Now.ToString()) 
                    });
                    Mensaje("Aviso", "Proyecto finalizado con éxito", true);
                    CargarGridProyectos();
                }
                else if (e.CommandName == "Gantt")
                {
                    var listaActividades = ActividadLogica.GetActividades(new ActividadEntidad()
                    {
                        IdProyecto = Convert.ToInt32(id),
                        Opcion = 2,
                        Estado = "1"
                    });
                    if (listaActividades.Count > 0)
                    {
                        Session["IdProyectoGantt"] = id;
                        Response.Redirect("GanttChart.aspx");
                    }
                    else
                    {
                        Mensaje("Aviso", "Este proyecto no posee ninguna actividad", false);
                    }
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

        protected void btnModalCliente_Click(object sender, EventArgs e)
        {
            CargarClientes();
            ScriptManager.RegisterStartupScript(this, GetType(), "modalCliente", "$('#modalCliente').modal('show')", true);           
        }

        protected void gvModalCliente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Seleccionar")
                {
                    hdnIdCliente.Value = gvModalCliente.Rows[index].Cells[0].Text;
                    txtNombreCliente.Text = gvModalCliente.Rows[index].Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this, GetType(), "modalCliente", "$('#modalCliente').modal('hide')", true);
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }
    }
}