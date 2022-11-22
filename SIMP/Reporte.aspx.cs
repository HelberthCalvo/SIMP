using SIMP.Entidades;
using SIMP.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using SIMP.Datos;
using System.Net;
using System.Drawing.Printing;
using System.Collections;
using SIMP.Logica.UTILITIES;
using System.Net.Mail;
using System.Net.Mime;

namespace SIMP
{
    public partial class Reporte : System.Web.UI.Page
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
                CargarGridProgresoProyecto();
                CargarGridTiempoRealEstimado();
                CargarGridCargaTrabajo();
            }
        }
        private void CargarGridProgresoProyecto()
        {
            try
            {
                DateTime fechaInicio = DateTime.Now.AddYears(-1);
                DateTime fechaFinal = DateTime.Now.AddDays(1);
                List<ProgresoProyectoEntidad> lstProyectoEntidad = new List<ProgresoProyectoEntidad>();
                lstProyectoEntidad = ProgresoProyectoLogica.GetProgresoProyecto(new ProgresoProyectoEntidad()
                {
                    Esquema = "dbo",
                    Opcion = 1,
                    Fecha_Inicio = fechaInicio,
                    Fecha_Final = fechaFinal
                });
                lstProyectoEntidad.ForEach(x =>
                {
                    x.NombreEstado = x.Estado == "1" ? "Activo" : "Inactivo";
                });
                gvProgresoProyecto.DataSource = lstProyectoEntidad;
                gvProgresoProyecto.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }
        private void CargarGridTiempoRealEstimado()
        {
            try
            {
                DateTime fechaInicio = DateTime.Now.AddYears(-1);
                DateTime fechaFinal = DateTime.Now.AddDays(1);
                List<TiempoRealEstimadoEntidad> lstTiempoRealEstimado = new List<TiempoRealEstimadoEntidad>();
                lstTiempoRealEstimado = TiempoRealEstimadoLogica.GetTiempoRealEstimado(new TiempoRealEstimadoEntidad()
                {
                    Esquema = "dbo",
                    Opcion = 1,
                    Fecha_Inicio = fechaInicio,
                    Fecha_Final = fechaFinal
                });
                lstTiempoRealEstimado.ForEach(x =>
                {
                    x.NombreEstado = x.Estado == "1" ? "Activo" : "Inactivo";
                });
                gvTiempo.DataSource = lstTiempoRealEstimado;
                gvTiempo.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }
        private void CargarGridCargaTrabajo()
        {
            try
            {
                List<CargaTrabajoEntidad> lstCargaTrabajo = new List<CargaTrabajoEntidad>();
                lstCargaTrabajo = CargaUsuarioLogica.GetCargaUsuarios(new CargaTrabajoEntidad()
                {
                    Esquema = "dbo",
                    Opcion = 1
                });
                lstCargaTrabajo.ForEach(x =>
                {
                    x.NombreEstado = x.Estado == "1" ? "Activo" : "Inactivo";
                });
                gvCargaTrabajo.DataSource = lstCargaTrabajo;
                gvCargaTrabajo.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
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
                        btnBuscarProgresoProyecto.Visible = false;

                        permisos += "- Crear ";
                    }

                    if (!obMenu.EditarPermiso)
                    {
                        gvProgresoProyecto.Columns[9].Visible = false;
                        gvProgresoProyecto.Columns[10].Visible = false;
                        gvProgresoProyecto.Columns[11].Visible = false;
                        permisos += "- Editar ";
                    }

                    if (!obMenu.VerPermiso)
                    {
                        gvProgresoProyecto.Visible = false;

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
        protected void btnBuscarProgresoProyecto_Click(object sender, EventArgs e)
        {
            try
            {
                List<ProgresoProyectoEntidad> lstProyectoEntidad = new List<ProgresoProyectoEntidad>();
                if (!string.IsNullOrEmpty(txbFechaInicioProgreso.Text) && !string.IsNullOrEmpty(txbFechaFinalProgreso.Text))
                {
                    DateTime fechaInicio = DateTime.Parse(txbFechaInicioProgreso.Text);
                    DateTime fechaFinal = DateTime.Parse(txbFechaFinalProgreso.Text).AddDays(1);
                    
                    lstProyectoEntidad = ProgresoProyectoLogica.GetProgresoProyecto(new ProgresoProyectoEntidad()
                    {
                        Esquema = "dbo",
                        Opcion = 1,
                        Fecha_Inicio = fechaInicio,
                        Fecha_Final = fechaFinal
                    });
                    lstProyectoEntidad.ForEach(x =>
                    {
                        x.NombreEstado = x.Estado == "1" ? "Activo" : "Inactivo";
                    });
                }
                else
                {
                    DateTime fechaInicio = DateTime.Now.AddYears(-1);
                    DateTime fechaFinal = DateTime.Now.AddDays(1);
                    lstProyectoEntidad = ProgresoProyectoLogica.GetProgresoProyecto(new ProgresoProyectoEntidad()
                    {
                        Esquema = "dbo",
                        Opcion = 1,
                        Fecha_Inicio = fechaInicio,
                        Fecha_Final = fechaFinal
                    });
                    lstProyectoEntidad.ForEach(x =>
                    {
                        x.NombreEstado = x.Estado == "1" ? "Activo" : "Inactivo";
                    });
                    
                }
                gvProgresoProyecto.DataSource = lstProyectoEntidad;
                gvProgresoProyecto.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        protected void gvProgresoProyecto_PreRender(object sender, EventArgs e)
        {
            if (gvProgresoProyecto.Rows.Count > 0)
            {
                gvProgresoProyecto.UseAccessibleHeader = true;
                gvProgresoProyecto.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvProgresoProyecto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string error = string.Empty;
            int index = Convert.ToInt32(e.CommandArgument);
            var id = gvProgresoProyecto.Rows[index].Cells[0].Text;
            try
            {
                if (e.CommandName == "GenerarPDF")
                {
                    Hashtable parametrosReporte = new Hashtable();
                    parametrosReporte.Add("@P_USUARIO", "hcalvo");
                    parametrosReporte.Add("@P_OPCION", 0);
                    parametrosReporte.Add("@P_PK_TBL_SIMP_PY_PROYECTO", id);
                    parametrosReporte.Add("@P_FECHA_INICIO", null);
                    parametrosReporte.Add("@P_FECHA_FINAL", null);
                    parametrosReporte.Add("@P_ESQUEMA", "DBO");

                    string rutaReportes = Server.MapPath(Path.Combine("Reportes", "ReportePorcentajeProyecto.rpt"));




                    PrintHelper.imprimirDocumento(parametrosReporte, "Microsoft Print to PDF", rutaReportes);
                }

                if (e.CommandName == "EnviarCorreo")
                {
                    Hashtable parametrosReporte = new Hashtable();
                    parametrosReporte.Add("@P_USUARIO", "hcalvo");
                    parametrosReporte.Add("@P_OPCION", 0);
                    parametrosReporte.Add("@P_PK_TBL_SIMP_PY_PROYECTO", id);
                    parametrosReporte.Add("@P_FECHA_INICIO", null);
                    parametrosReporte.Add("@P_FECHA_FINAL", null);
                    parametrosReporte.Add("@P_ESQUEMA", "DBO");
                    string rutaReportes = Server.MapPath(Path.Combine("Reportes", "ReportePorcentajeProyecto.rpt"));
                    EnviarCorreoReporte(new ClienteEntidad { Correo_Electronico = "hcalvo4@hotmail.com", Nombre = "Helberth", Primer_Apellido = "Calvo", Segundo_Apellido = "Picado" }, PrintHelper.generarPDF(parametrosReporte, "PrimoPDF", rutaReportes));
                    Mensaje("Aviso", "El correo se envío exitosamente", true);
                }

            }
            catch (Exception ex)
            {
                Mensaje("Error: ", ex.Message, true);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "$('#myModalError').modal();", true);
            }
        }

        public void EnviarCorreoReporte(ClienteEntidad pCliente, Stream pdf)
        {
            string rutaImgLogo = @"~\Content\img\logo-sitsa.png";
            string contentID1 = "logoEmpresa";
            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            try
            {

                Attachment inlineLogo = new Attachment(Server.MapPath(rutaImgLogo));
                inlineLogo.ContentId = contentID1;
                //haciendo que la imagen no se inserte como un adjunto al correo
                inlineLogo.ContentDisposition.Inline = true;
                inlineLogo.ContentDisposition.DispositionType = DispositionTypeNames.Inline;


                //Direccion de correo electronico a la que queremos enviar el mensaje
                mmsg.To.Add(pCliente.Correo_Electronico);

                //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

                //Asunto
                mmsg.Subject = "Reportes SIMP";
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                //Cuerpo del Mensaje


                string mensaje = @"<h1 style='text-align:center'><b> Usuario " + pCliente.Nombre + "</b></h1>" +
                  "<p><b> Estimado(a) Cliente: </b>" + pCliente.Nombre.ToUpper() + "</p>" +
                    "<p> Se ha generado un nuevo reporte de progreso de proyectos el día " + DateTime.Now.ToString("dd/MM/yyyy") + "</p>" +
                    "<p>Datos del Cliente:</p>" +
                    "<p>Nombre: " + pCliente.Nombre + "</p>" +
                     "<p>Apellidos: " + pCliente.Primer_Apellido + " " + pCliente.Segundo_Apellido + "</p>" +
                    "<p><em>No responder este correo, ya que se generó de forma automática por el sistema SIMP<em></p>" +
                       "<p>Gracias.</p>" +
                       "<p>&nbsp;</p> " +
                       "<p> <img src='cid:" + contentID1 + "' width='450' height='150'/></p> ";

                mmsg.Body = mensaje;

                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                mmsg.IsBodyHtml = true; //Si no queremos que se envíe como HTML


                //correo de alias 

                string CorreoAlias = string.Empty;

                CorreoAlias = GlobalesE.CorreoSalida;


                string NombreUsuario = "Sistema SIMP";


                //Correo electronico desde la que enviamos el mensaje
                mmsg.From = new System.Net.Mail.MailAddress(CorreoAlias, NombreUsuario);

                mmsg.ReplyToList.Add(new System.Net.Mail.MailAddress(CorreoAlias));

                mmsg.Attachments.Add(inlineLogo);

                Attachment data = new Attachment(pdf, MediaTypeNames.Application.Octet);
                data.Name = "Reporte SIMP.pdf";
                mmsg.Attachments.Add(data);
                /*-------------------------CLIENTE DE CORREO----------------------*/

                //Hay que crear las credenciales del correo emisor
                cliente.Credentials = new System.Net.NetworkCredential(GlobalesE.CorreoSalida, GlobalesE.ContrasennaCorreoSalida);

                /*-------------------------CLIENTE DE CORREO----------------------*/


                //Hay que crear las credenciales del correo emisor
                cliente.Credentials =
                    //new System.Net.NetworkCredential("taxisirazu@curlingtech.com", "Ir@ZuTaxi$2018");
                    new System.Net.NetworkCredential(GlobalesE.CorreoSalida, GlobalesE.ContrasennaCorreoSalida);

                /*-------------------------ENVIO DE CORREO----------------------*/
                // mail.servidordominio.com
                //Para Gmail "smtp.gmail.com";
                cliente.Host = GlobalesE.SMTPCorreo;

                //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail
                cliente.Port = Convert.ToInt32(GlobalesE.PuertoCorreo);
                cliente.EnableSsl = false;

                //Enviamos el mensaje      
                cliente.Send(mmsg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                try
                {
                    cliente.EnableSsl = true;
                    //Enviamos el mensaje      
                    cliente.Send(mmsg);
                }
                catch (Exception exx)
                {
                    Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
                }
            }
            finally
            {
                cliente.Dispose();
                mmsg.Dispose();
            }
        }
        protected void btnBuscarTiempo_Click(object sender, EventArgs e)
        {

        }

        protected void gvTiempo_PreRender(object sender, EventArgs e)
        {
            if (gvTiempo.Rows.Count > 0)
            {
                gvTiempo.UseAccessibleHeader = true;
                gvTiempo.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvTiempo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string error = string.Empty;
            int index = Convert.ToInt32(e.CommandArgument);
            var id = gvTiempo.Rows[index].Cells[0].Text;
            try
            {
                if (e.CommandName == "GenerarPDF")
                {
                    Hashtable parametrosReporte = new Hashtable();
                    parametrosReporte.Add("@P_USUARIO", "hcalvo");
                    parametrosReporte.Add("@P_OPCION", 0);
                    parametrosReporte.Add("@P_PK_TBL_SIMP_PY_PROYECTO", id);
                    parametrosReporte.Add("@P_FECHA_INICIO", null);
                    parametrosReporte.Add("@P_FECHA_FINAL", null);
                    parametrosReporte.Add("@P_ESQUEMA", "DBO");

                    string rutaReportes = Server.MapPath(Path.Combine("Reportes", "ReporteTiempoRealEstimado.rpt"));




                    PrintHelper.imprimirDocumento(parametrosReporte, "Microsoft Print to PDF", rutaReportes);
                }

                if (e.CommandName == "EnviarCorreo")
                {
                    Hashtable parametrosReporte = new Hashtable();
                    parametrosReporte.Add("@P_USUARIO", "hcalvo");
                    parametrosReporte.Add("@P_OPCION", 0);
                    parametrosReporte.Add("@P_PK_TBL_SIMP_PY_PROYECTO", id);
                    parametrosReporte.Add("@P_FECHA_INICIO", null);
                    parametrosReporte.Add("@P_FECHA_FINAL", null);
                    parametrosReporte.Add("@P_ESQUEMA", "DBO");
                    string rutaReportes = Server.MapPath(Path.Combine("Reportes", "ReporteTiempoRealEstimado.rpt"));
                    EnviarCorreoReporte(new ClienteEntidad { Correo_Electronico = "hcalvo4@hotmail.com", Nombre = "Helberth", Primer_Apellido = "Calvo", Segundo_Apellido = "Picado" }, PrintHelper.generarPDF(parametrosReporte, "PrimoPDF", rutaReportes));
                    Mensaje("Aviso", "El correo se envío exitosamente", true);
                }

            }
            catch (Exception ex)
            {
                Mensaje("Error: ", ex.Message, true);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "$('#myModalError').modal();", true);
            }
        }

        protected void gvCargaTrabajo_PreRender(object sender, EventArgs e)
        {
            if (gvCargaTrabajo.Rows.Count > 0)
            {
                gvCargaTrabajo.UseAccessibleHeader = true;
                gvCargaTrabajo.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvCargaTrabajo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string error = string.Empty;
            int index = Convert.ToInt32(e.CommandArgument);
            var id = gvCargaTrabajo.Rows[index].Cells[0].Text;
            try
            {
                if (e.CommandName == "GenerarPDF")
                {
                    Hashtable parametrosReporte = new Hashtable();
                    parametrosReporte.Add("@P_USUARIO", "hcalvo");
                    parametrosReporte.Add("@P_OPCION", 0);
                    parametrosReporte.Add("@P_PK_TBL_SIMP_SEG_USUARIO", id);
                    parametrosReporte.Add("@P_FECHA_INICIO", null);
                    parametrosReporte.Add("@P_FECHA_FINAL", null);
                    parametrosReporte.Add("@P_ESQUEMA", "DBO");

                    string rutaReportes = Server.MapPath(Path.Combine("Reportes", "ReporteCargaUsuario.rpt"));




                    PrintHelper.imprimirDocumento(parametrosReporte, "Microsoft Print to PDF", rutaReportes);
                }

                if (e.CommandName == "EnviarCorreo")
                {
                    Hashtable parametrosReporte = new Hashtable();
                    parametrosReporte.Add("@P_USUARIO", "hcalvo");
                    parametrosReporte.Add("@P_OPCION", 0);
                    parametrosReporte.Add("@P_PK_TBL_SIMP_SEG_USUARIO", id);
                    parametrosReporte.Add("@P_FECHA_INICIO", null);
                    parametrosReporte.Add("@P_FECHA_FINAL", null);
                    parametrosReporte.Add("@P_ESQUEMA", "DBO");
                    string rutaReportes = Server.MapPath(Path.Combine("Reportes", "ReporteCargaUsuario.rpt"));
                    EnviarCorreoReporte(new ClienteEntidad { Correo_Electronico = "hcalvo4@hotmail.com", Nombre = "Helberth", Primer_Apellido = "Calvo", Segundo_Apellido = "Picado" }, PrintHelper.generarPDF(parametrosReporte, "PrimoPDF", rutaReportes));
                    Mensaje("Aviso", "El correo se envío exitosamente", true);
                }

            }
            catch (Exception ex)
            {
                Mensaje("Error: ", ex.Message, true);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "$('#myModalError').modal();", true);
            }
        }

        protected void btnBuscarCargaTrabajo_Click(object sender, EventArgs e)
        {

        }
        private void Mensaje(string titulo, string msg, bool esCorrecto, string textoBoton = "Ok")
        {
            string icono = esCorrecto ? "success" : "error";
            string script = "Swal.fire({ title: '" + titulo + "!', text: '" + msg + "', icon: '" + icono + "', confirmButtonText: '" + textoBoton + "' })";
            ScriptManager.RegisterStartupScript(this, GetType(), "script", script, true);
        }

        protected void btnBuscarTiempos_Click(object sender, EventArgs e)
        {
            try
            {
                List<TiempoRealEstimadoEntidad> lstTiempoRealEstimado = new List<TiempoRealEstimadoEntidad>();
                if (!string.IsNullOrEmpty(txbFechaInicioTiempos.Text) && !string.IsNullOrEmpty(txbFechaFinalTiempos.Text))
                {
                    DateTime fechaInicio = DateTime.Parse(txbFechaInicioTiempos.Text);
                    DateTime fechaFinal = DateTime.Parse(txbFechaFinalTiempos.Text).AddDays(1);
                    lstTiempoRealEstimado = TiempoRealEstimadoLogica.GetTiempoRealEstimado(new TiempoRealEstimadoEntidad()
                    {
                        Esquema = "dbo",
                        Opcion = 0,
                        Fecha_Inicio = fechaInicio,
                        Fecha_Final = fechaFinal
                    });
                    lstTiempoRealEstimado.ForEach(x =>
                    {
                        x.NombreEstado = x.Estado == "1" ? "Activo" : "Inactivo";
                    });
                }
                else
                {
                    DateTime fechaInicio = DateTime.Now.AddYears(-1);
                    DateTime fechaFinal = DateTime.Now.AddDays(1);
                    lstTiempoRealEstimado = TiempoRealEstimadoLogica.GetTiempoRealEstimado(new TiempoRealEstimadoEntidad()
                    {
                        Esquema = "dbo",
                        Opcion = 1,
                        Fecha_Inicio = fechaInicio,
                        Fecha_Final = fechaFinal
                    });
                    lstTiempoRealEstimado.ForEach(x =>
                    {
                        x.NombreEstado = x.Estado == "1" ? "Activo" : "Inactivo";
                    });
                }
                gvTiempo.DataSource = lstTiempoRealEstimado;
                gvTiempo.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }
    }
}