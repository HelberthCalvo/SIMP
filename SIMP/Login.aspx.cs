using SIMP.Entidades;
using SIMP.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIMP
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            
        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                if (CamposInvalidos())
                {
                    return;
                }

                UsuarioEntidad usuario = new UsuarioEntidad()
                {
                    Esquema = "dbo",
                    Correo = txtNombreUsuario.Text.Trim(),
                    Usuario = txtNombreUsuario.Text.Trim(),
                    Contrasena = txtContrasena.Text,
                    Opcion = 2,
                    Estado = 1
                };
                UsuarioLogica usuario1 = new UsuarioLogica();
                List<UsuarioEntidad> usuarioBusqueda = usuario1.GetUsuarios(usuario);
                if (usuarioBusqueda.Count > 0)
                {
                    Session["UsuarioSistema"] = usuarioBusqueda[0];
                    Session["Compañia"] = "dbo";
                    llenarParametrosCatConfiguracion();
                    if (usuarioBusqueda[0].Perfil == 5)
                    {
                        Response.Redirect("Proyecto.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("Actividad.aspx", false);
                    }
                    
                }
                else
                {
                    Mensaje("Error", "Usuario y/o contraseña inválido(s)", false);
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }
        public void llenarParametrosCatConfiguracion()
        {
            try
            {

                CatConfiguracionL obCatConfiguracionL = new CatConfiguracionL();

                CatConfiguracionEntidad obCatConfiguracion = new CatConfiguracionEntidad();

                string usuarioLogin = Session["UsuarioSistema"].ToString();
                string Compania = Session["Compañia"].ToString();
                obCatConfiguracion.Usuario = usuarioLogin;
                obCatConfiguracion.Opcion = 0;
                obCatConfiguracion.Esquema = Compania;
                obCatConfiguracion.Llave01 = "SIMP";
                obCatConfiguracion.Llave02 = "CORREO";
                obCatConfiguracion.Llave03 = "ENVIO_CORREO";
                obCatConfiguracion.Llave05 = "";
                obCatConfiguracion.Llave06 = "";

                obCatConfiguracion.Llave04 = "CORREO_SALIDA";
                GlobalesE.CorreoSalida = obCatConfiguracionL.ObtenerCatConfiguracion(obCatConfiguracion).ToList().FirstOrDefault().Valor;

                obCatConfiguracion.Llave04 = "CONTRASENNA_CORREO";
                GlobalesE.ContrasennaCorreoSalida = obCatConfiguracionL.ObtenerCatConfiguracion(obCatConfiguracion).ToList().FirstOrDefault().Valor;

                obCatConfiguracion.Llave04 = "SMTP_CORREO";
                GlobalesE.SMTPCorreo = obCatConfiguracionL.ObtenerCatConfiguracion(obCatConfiguracion).ToList().FirstOrDefault().Valor;

                obCatConfiguracion.Llave04 = "PUERTO_CORREO";
                GlobalesE.PuertoCorreo = obCatConfiguracionL.ObtenerCatConfiguracion(obCatConfiguracion).ToList().FirstOrDefault().Valor;
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }
        private bool CamposInvalidos()
        {
            Regex regex = new Regex(@"^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{9,15}$");
            if (string.IsNullOrEmpty(txtNombreUsuario.Text))
            {
                Mensaje("Aviso", "Debe ingresar un nombre de usuario", false);
                return true;
            }
            else if (string.IsNullOrEmpty(txtContrasena.Text))
            {
                Mensaje("Aviso", "Debe ingresar una contraseña", false);
                return true;
            }
            else if (txtContrasena.Text.Length < 9)
            {
                Mensaje("Aviso", "La contraseña debe poseer 9 caracteres como mínimo", false);
                return true;
            }
            else if (txtContrasena.Text.Length > 15)
            {
                Mensaje("Aviso", "La contraseña debe poseer 15 caracteres como máximo", false);
                return true;
            }
            else if (!regex.IsMatch(txtContrasena.Text))
            {
                string msj = "La contraseña debe cumplir con los siguientes parámetros: " +
                    "- Al menos una letra mayúscula" +
                    "- Al menos una letra minúscula" +
                    "- Al menos un número" +
                    "- Al menos un caracter especial ($%#@!&*?)";
                Mensaje("Aviso", msj, false);
                return true;
            }
            return false;
        }
        private void Mensaje(string titulo, string msg, bool esCorrecto, string textoBoton = "Ok")
        {
            string icono = esCorrecto ? "success" : "error";
            string script = "Swal.fire({ title: '" + titulo + "!', text: '" + msg + "', icon: '" + icono + "', confirmButtonText: '" + textoBoton + "' })";
            ScriptManager.RegisterStartupScript(this, GetType(), "script", script, true);
        }
        private string contraseniaAlfanumerica(int tamanioContrasenia)
        {
            const string caracteresValidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder stringBuilder = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (tamanioContrasenia-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    stringBuilder.Append(caracteresValidos[(int)(num % (uint)caracteresValidos.Length)]);
                }
            }

            return Convert.ToBase64String(Encoding.ASCII.GetBytes(stringBuilder.ToString()), Base64FormattingOptions.None);
        }
        protected void btnRecuperarContrasena_Click(object sender, EventArgs e)
        {
            try
            {
                Session["UsuarioSistema"] = txtUsuarioRecuperar.Text;
                Session["Compañia"] = "dbo";
                llenarParametrosCatConfiguracion();
                UsuarioEntidad usuario = new UsuarioEntidad();
                usuario.Usuario = txtUsuarioRecuperar.Text;
                usuario.Opcion = 1;
                usuario.Esquema = "dbo";
                usuario = new UsuarioLogica().GetUsuarios(usuario).FirstOrDefault();
                if(usuario == null)
                {
                    Mensaje("Aviso", "El usuario o correo no existe", false);
                    return;
                }
                usuario.Contrasena = contraseniaAlfanumerica(8);
                usuario.Contrasena = usuario.Contrasena + "9";
                new UsuarioLogica().MantUsuario(usuario);
                EnviarCorreoCrearUsuario(usuario);
                Mensaje("Usuario", "Se ha reestablecido la clave correctamente, revise su correo electrónico", true);
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }

        }
        public void EnviarCorreoCrearUsuario(UsuarioEntidad pUsuario)
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
                mmsg.To.Add(pUsuario.Correo);

                //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

                //Asunto
                mmsg.Subject = "Seguridad de SIMP";
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                //Cuerpo del Mensaje


                string mensaje = @"<h1 style='text-align:center'><b> Usuario " + pUsuario.Nombre + "</b></h1>" +
                  "<p><b> Estimado(a) Usuario: </b>" + pUsuario.Nombre.ToUpper() + "</p>" +
                    "<p> Se ha generado una nueva contraseña el día " + DateTime.Now.ToString("dd/MM/yyyy") + "</p>" +
                    "<p>Datos del Usuario:</p>" +
                    "<p>Código: " + pUsuario.Usuario_Sistema + "</p>" +
                    "<p>Nombre: " + pUsuario.Nombre + "</p>" +
                    "<p>Contraseña: " + pUsuario.Contrasena + "</p>" +
                    "<p>Perfil: " + pUsuario.PerfilNombre + "</p>" +
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


    }
}