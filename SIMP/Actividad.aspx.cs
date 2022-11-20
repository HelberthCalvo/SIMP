using Aspose.Cells;
using ExcelDataReader;
using SIMP.Entidades;
using SIMP.Logica;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIMP
{
    public partial class Actividad : System.Web.UI.Page
    {
        private static bool esIndefinidoStatic = false;
        private static string descripcionStatic = "";
        private static double horasEstimadasStatic = 0;
        private static string FechaInicioStatic = "";
        private static string FechaFinalizacionStatic = "";

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

        private void CargarGridActividades()
        {
            try
            {
                List<ActividadEntidad> lstActividades = new List<ActividadEntidad>();
                lstActividades = ActividadLogica.GetActividades(new ActividadEntidad()
                {
                    Esquema = "dbo",
                    Opcion = 0
                });
                lstActividades.ForEach(x =>
                {
                    x.NombreEstado = x.IdEstado == 1 ? "Activo" : "Inactivo";
                    x.Descripcion = x.Descripcion.Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u');
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
            string formato = DateTime.Now.ToString();
            if (lstFecha.Length > 1)
            {
                formato = lstFecha[1] + "/" + lstFecha[0] + "/" + lstFecha[2];
            }
            return formato;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (CamposValidos())
                {
                    return;
                }

                ActividadEntidad actividad = new ActividadEntidad()
                {
                    Descripcion = txbDescripcion.Text,
                    IdFase = Convert.ToInt32(hdnIdFase.Value),
                    IdUsuario = Convert.ToInt32(hdnIdUsuario.Value),
                    HorasEstimadas = Convert.ToDouble(txtHorasEstimadas.Text),
                    IdEstado = 1,
                    Usuario = "hcalvo",
                    Esquema = "dbo",
                    Opcion = 0,
                    Fecha_Inicio = FechaInicioStatic,
                    Fecha_Finalizacion = FechaFinalizacionStatic
                };
                //Si esta editando
                if (!string.IsNullOrEmpty(hdnIdActividad.Value))
                {
                    actividad.Fecha_Inicio = FormatoFecha(FechaInicioStatic);
                    actividad.Fecha_Finalizacion = FormatoFecha(FechaFinalizacionStatic);
                    if (hdnIdProyecto.Value == "0")
                    {
                        Mensaje("Aviso", "Debe seleccionar un proyecto", false);
                        return;
                    }
                    else if (hdnIdFase.Value == "0")
                    {
                        Mensaje("Aviso", "Debe seleccionar una fase", false);
                        return;
                    }
                    else if (hdnIdUsuario.Value == "0")
                    {
                        Mensaje("Aviso", "Debe seleccionar un usuario", false);
                        return;
                    }
                    actividad.Id = Convert.ToInt32(hdnIdActividad.Value);
                }
                ActividadLogica.MantActividad(actividad);
                Mensaje("Aviso", "La actividad se guardó correctamente", true);
                LimpiarDatos();
                hdnIdActividad.Value = "";
                CargarGridActividades();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        private void LimpiarDatos()
        {
            txbDescripcion.Text = string.Empty;
            txtHorasEstimadas.Text = string.Empty;
            hdnIdProyecto.Value = string.Empty;
            hdnIdFase.Value = string.Empty;
            hdnIdUsuario.Value = string.Empty;
            hdnIdActividad.Value = string.Empty;
            txtNombreFase.Text = string.Empty;
            txtNombreProyecto.Text = string.Empty;
            txtNombreUsuario.Text = string.Empty;
            txtHorasEstimadas.Text = string.Empty;
            txtHorasReales.Text = string.Empty;
            descripcionStatic = string.Empty;
            esIndefinidoStatic = false;
            FechaInicioStatic = string.Empty;
            FechaFinalizacionStatic = string.Empty;
        }

        private bool CamposValidos()
        {
            if (string.IsNullOrEmpty(txbDescripcion.Text))
            {
                Mensaje("Aviso", "Debe ingresar una descripción", false);
                return true;
            }
            else if (string.IsNullOrEmpty(txtHorasEstimadas.Text))
            {
                Mensaje("Aviso", "Debe ingresar las horas estimadas", false);
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
                var idProyecto = gvActividad.Rows[index].Cells[1].Text;
                var idFase = gvActividad.Rows[index].Cells[2].Text;
                var idUsuario = gvActividad.Rows[index].Cells[3].Text;
                var nombreProyecto = gvActividad.Rows[index].Cells[4].Text;
                var nombreFase = gvActividad.Rows[index].Cells[5].Text;
                var nombreUsuario = gvActividad.Rows[index].Cells[6].Text;
                var descripcion = gvActividad.Rows[index].Cells[7].Text;
                var horasEstimadas = gvActividad.Rows[index].Cells[8].Text;
                var horasReales = gvActividad.Rows[index].Cells[9].Text;
                var nombreEstado = gvActividad.Rows[index].Cells[10].Text;
                var fecha_inicio = gvActividad.Rows[index].Cells[11].Text;
                var fecha_finalizacion = gvActividad.Rows[index].Cells[12].Text;

                if (e.CommandName == "Editar")
                {
                    hdnIdActividad.Value = Convert.ToInt32(id).ToString();
                    hdnIdProyecto.Value = idProyecto;
                    hdnIdFase.Value = idFase;
                    hdnIdUsuario.Value = idUsuario;
                    FechaInicioStatic = fecha_inicio;
                    FechaFinalizacionStatic = fecha_finalizacion;
                    txbDescripcion.Text = descripcion;
                    txtHorasEstimadas.Text = horasEstimadas;
                    txtNombreUsuario.Text = nombreUsuario;
                    txtNombreFase.Text = nombreFase;
                    txtNombreProyecto.Text = nombreProyecto;
                }
                else if (e.CommandName == "CambiarEstado")
                {
                    bool esIndefinido = nombreProyecto.Equals("Indefinido") || nombreFase.Equals("Indefinido") || nombreUsuario.Equals("Indefinido");

                    ActividadLogica.MantActividad(new ActividadEntidad
                    {
                        Id = Convert.ToInt32(id),
                        IdEstado = nombreEstado == "Activo" ? 2 : 1,
                        Opcion = esIndefinido? 1: 0,
                        Esquema = "dbo",
                        Descripcion = descripcion,
                        IdUsuario = Convert.ToInt32(idUsuario),
                        IdFase = Convert.ToInt32(idFase),
                        HorasEstimadas = Convert.ToDouble(horasEstimadas),
                        HorasReales = Convert.ToDouble(horasReales),
                        Fecha_Inicio = FormatoFecha(fecha_inicio),
                        Fecha_Finalizacion = FormatoFecha(fecha_finalizacion)
                    });
                    Mensaje("Aviso", "Estado de la actividad actualizado con éxito", true);
                    CargarGridActividades();
                }
                else if (e.CommandName == "Finalizar")
                {
                    esIndefinidoStatic = nombreProyecto.Equals("Indefinido") || nombreFase.Equals("Indefinido") || nombreUsuario.Equals("Indefinido");
                    descripcionStatic = descripcion;
                    horasEstimadasStatic = Convert.ToDouble(horasEstimadas);
                    hdnIdActividad.Value = id;
                    hdnIdFase.Value = idFase;
                    FechaInicioStatic = fecha_inicio;
                    hdnIdUsuario.Value = idUsuario;
                    ScriptManager.RegisterStartupScript(this, GetType(), "modalHorasReales", "$('#modalHorasReales').modal('show')", true);
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarDatos();
        }

        protected void gvModalProyecto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Seleccionar")
                {
                    hdnIdProyecto.Value = gvModalProyecto.Rows[index].Cells[0].Text;
                    hdnIdFase.Value = null;
                    txtNombreFase.Text = "";
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

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            string serverpath = Server.MapPath("~");

            string fullpath = serverpath + "temp\\excel\\";

            if (!Directory.Exists(fullpath))
            {
                Directory.CreateDirectory(fullpath);
            }

            if (FileUpload2.HasFile)
            {
                string fileName = Server.HtmlEncode(FileUpload2.FileName);

                string extension = Path.GetExtension(fileName);

                if (extension.Contains(".xls"))
                {
                    fullpath += fileName;
                    FileUpload2.SaveAs(fullpath);
                    CargarActividadesExcel(fullpath);
                }
                else
                {
                    Mensaje("Error", "El archivo debe ser de tipo .xls", false);
                }

            }
            else
            {
                Mensaje("Error", "El archivo no pudo subirse correctamente", false);
            }
        }

        private bool esDiaSemana(string valor)
        {
            if (valor.Equals("Lunes") || valor.Equals("Martes") || valor.Equals("Miercoles") ||
                valor.Equals("Jueves") || valor.Equals("Viernes") || valor.Equals("Sabado"))
            {
                return true;
            }
            return false;
        }

        private void CargarActividadesExcel(string filePath)
        {
            try
            {
                Workbook wb = new Workbook(filePath);

                // Obtener todas las hojas de trabajo
                WorksheetCollection collection = wb.Worksheets;

                // Recorra todas las hojas de trabajo
                for (int worksheetIndex = 2; worksheetIndex < collection.Count; worksheetIndex++)
                {
                    // Obtener hoja de trabajo usando su índice
                    Worksheet worksheet = collection[worksheetIndex];

                    // Obtener el número de filas y columnas
                    int rows = 80;

                    // Bucle a través de filas
                    for (int i = 7; i < rows; i++)
                    {
                        // Recorra cada columna en la fila seleccionada
                        var tarea = worksheet.Cells[i, 1].Value;
                        var horas = worksheet.Cells[i, 6].Value;
                        if (tarea != null && !esDiaSemana(tarea.ToString()))
                        {
                            ActividadLogica.MantActividad(new ActividadEntidad()
                            {
                                IdEstado = 1,
                                Descripcion = tarea.ToString(),
                                HorasEstimadas = Convert.ToDouble(horas.ToString()),
                                HorasReales = Convert.ToDouble(horas.ToString()),
                                Opcion = 1,
                                Fecha_Inicio = DateTime.Now.ToString(),
                                Fecha_Finalizacion = DateTime.Now.ToString()
                            }); ;
                        }
                    }
                }
                Mensaje("Aviso", "Actividades cargadas correctamente", true);
                CargarGridActividades();
            }
            catch (Exception ex)
            {
                Mensaje("Error", "No se pudo cargar las actividades correctamente. Error: " + ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }

        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtHorasReales.Text))
            {
                ActividadLogica.MantActividad(new ActividadEntidad
                {
                    Id = Convert.ToInt32(hdnIdActividad.Value),
                    HorasEstimadas = horasEstimadasStatic,
                    HorasReales = Convert.ToDouble(txtHorasReales.Text),
                    IdEstado = 2,
                    Opcion = esIndefinidoStatic ? 1 : 0,
                    Esquema = "dbo",
                    IdFase = Convert.ToInt32(hdnIdFase.Value),
                    IdUsuario = Convert.ToInt32(hdnIdUsuario.Value),
                    Descripcion = descripcionStatic,
                    Fecha_Inicio = FormatoFecha(FechaInicioStatic),
                    Fecha_Finalizacion = FormatoFecha(DateTime.Now.ToString())
                });
                Mensaje("Aviso", "Actividad finalizada con éxito", true);
                LimpiarDatos();
                CargarGridActividades();
            }
            else
            {
                Mensaje("Aviso", "Debe ingresar las horas reales para finalizar la actividad", false);
            }
            
        }
    }
}
