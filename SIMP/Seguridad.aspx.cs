using SIMP.Entidades;
using SIMP.Logica;
using SIMP.Logica.UTILITIES;
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
    public partial class Seguridad : System.Web.UI.Page
    {
        private const int CHECKEAR_NODOS = 1;
        private const int DESCHECKEAR_NODOS = 2;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioSistema"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                HabilitaOpcionesPermisos();
                CargarGridPerfil();
                CargarGridUsuario();
                CargarTreeViewPermisos();
                tvPermisos.Attributes.Add("onclick", "postBackByObject()");
                CargarTooltips();
            }
        }

        private void CargarTooltips()
        {
            try
            {
                foreach (GridViewRow item in gvUsuario.Rows)
                {
                    item.Cells[6].ToolTip = "Editar";
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
                        btnGuardarUsuario.Visible = false;
                        btnGuardarPerfil.Visible = false;
                        btnGuardarPermisos.Visible = false;
                        permisos += "- Crear ";
                    }

                    if (!obMenu.EditarPermiso)
                    {
                        gvPerfil.Columns[2].Visible = false;
                        gvPerfil.Columns[3].Visible = false;
                        gvUsuario.Columns[5].Visible = false;
                        gvUsuario.Columns[6].Visible = false;
                        gvUsuario.Columns[7].Visible = false;
                        permisos += "- Editar ";
                    }

                    if (!obMenu.VerPermiso)
                    {
                        gvUsuario.Visible = false;
                        gvPerfil.Visible = false;
                        ddlPerfilPermisos.Enabled = false;
                        permisos += "- Consultar ";
                    }

                    if (obMenu.EnviarPermiso)
                    {
                        hdfPermisoEnviarCorreos.Value = "1";
                    }
                    else
                    {
                        hdfPermisoEnviarCorreos.Value = "0";
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

        #region PerfilEntidads
        protected void gvPerfil_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "editar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    hfIdPerfil.Value = gvPerfil.DataKeys[index].Values[0].ToString();
                    txtDescripcionPerfil.Text = gvPerfil.DataKeys[index].Values[1].ToString();
                    string estado = gvPerfil.DataKeys[index].Values[2].ToString();
                    if (estado == "Activo")
                    {
                        rdbInactivoPerfil.Checked = false;
                        rdbActivoPerfil.Checked = true;
                    }
                    else
                    {
                        rdbActivoPerfil.Checked = false;
                        rdbInactivoPerfil.Checked = true;
                    }
                }
                else if (e.CommandName == "eliminar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    PerfilEntidad perfil = new PerfilEntidad();
                    perfil.Id = Convert.ToInt32(gvPerfil.DataKeys[index].Values[0]);
                    perfil.Descripcion = gvPerfil.DataKeys[index].Values[1].ToString();
                    perfil.Estado = "0";
                    perfil.Esquema = Session["Compañia"].ToString();
                    perfil.Usuario = ((UsuarioEntidad)Session["UsuarioSistema"]).Usuario_Sistema;

                    new PerfilLogica().MantPerfil(perfil);

                    CargarGridPerfil();
                    Mensaje("Perfil", "Desactivado correctamente", true);
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        protected void gvPerfil_PreRender(object sender, EventArgs e)
        {
            if (gvPerfil.Rows.Count > 0)
            {
                gvPerfil.UseAccessibleHeader = true;
                gvPerfil.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void btnCancelarPerfil_Click(object sender, EventArgs e)
        {
            LimpiarCamposPerfil();
        }

        protected void btnGuardarPerfil_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtDescripcionPerfil.Text))
                {
                    Mensaje("Perfil", "Debe ingresar una descripción", false);
                    return;
                }
                PerfilEntidad perfil = new PerfilEntidad();
                perfil.Id = !string.IsNullOrEmpty(hfIdPerfil.Value.ToString()) ? Convert.ToInt32(hfIdPerfil.Value) : 0;
                perfil.Descripcion = txtDescripcionPerfil.Text;
                perfil.Estado = rdbActivoPerfil.Checked ? "1" : "0";
                perfil.Esquema = "dbo";
                perfil.Usuario = ((UsuarioEntidad)(Session["UsuarioSistema"])).Usuario_Sistema;
                new PerfilLogica().MantPerfil(perfil);

                LimpiarCamposPerfil();
                CargarGridPerfil();
                Mensaje("Perfil", "Guardada correctamente", true);
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }
        void LimpiarCamposPerfil()
        {
            hfIdPerfil.Value = null;
            txtDescripcionPerfil.Text = string.Empty;
        }
        void CargarGridPerfil()
        {
            try
            {
                List<PerfilEntidad> lstPerfil = new List<PerfilEntidad>();
                lstPerfil = new PerfilLogica().GetPerfiles(new PerfilEntidad() { Id = 0, Opcion = 0, Usuario = "hcalvo", Estado = "1", Esquema = "dbo" });
                lstPerfil.ForEach(x => { x.NombreEstado = x.Estado == "1" ? "Activo" : "Inactivo"; });
                gvPerfil.DataSource = lstPerfil;
                gvPerfil.DataBind();

                if (lstPerfil.Count > 0)
                {
                    ddlPerfil.DataSource = lstPerfil;
                    ddlPerfil.DataValueField = "ID";
                    ddlPerfil.DataTextField = "Descripcion";
                    ddlPerfil.DataBind();
                    ddlPerfil.Items.Insert(0, new ListItem() { Text = "-- SELECCIONE EL PERFIL --", Value = "0" });
                    ddlPerfilPermisos.DataSource = lstPerfil;
                    ddlPerfilPermisos.DataValueField = "ID";
                    ddlPerfilPermisos.DataTextField = "Descripcion";
                    ddlPerfilPermisos.DataBind();
                    ddlPerfilPermisos.Items.Insert(0, new ListItem() { Text = "-- SELECCIONE EL PERFIL --", Value = "0" });
                }
                else
                {

                    ddlPerfil.DataSource = "";
                    ddlPerfil.DataBind();
                    ddlPerfil.Items.Insert(0, new ListItem() { Text = "-- SELECCIONE EL PERFIL --", Value = "0" });

                    ddlPerfilPermisos.DataSource = "";
                    ddlPerfilPermisos.DataBind();
                    ddlPerfilPermisos.Items.Insert(0, new ListItem() { Text = "-- SELECCIONE EL PERFIL --", Value = "0" });
                }


            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        #endregion

        #region Usuarios
        void CargarGridUsuario()
        {
            try
            {
                List<UsuarioEntidad> lstUsuario = new List<UsuarioEntidad>();
                lstUsuario = new UsuarioLogica().GetUsuarios(new UsuarioEntidad() { Id = 0, Opcion = 0, Usuario = "hcalvo", Estado = 1, Esquema = "dbo", Contrasena = "" });
                lstUsuario.ForEach(x => { x.NombreEstado = x.Estado == 1 ? "Activo" : "Inactivo"; });
                gvUsuario.DataSource = lstUsuario;
                gvUsuario.DataBind();
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

        protected void gvUsuario_PreRender(object sender, EventArgs e)
        {
            if (gvUsuario.Rows.Count > 0)
            {
                gvUsuario.UseAccessibleHeader = true;
                gvUsuario.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "editar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    hfIdUsuario.Value = gvUsuario.DataKeys[index].Values[0].ToString();
                    txtUsuario.Text = gvUsuario.DataKeys[index].Values[1].ToString();
                    txtUsuario.Enabled = false;
                    txtNombre.Text = gvUsuario.DataKeys[index].Values[2].ToString();
                    txtCorreo.Text = gvUsuario.DataKeys[index].Values[3].ToString();
                    ddlPerfil.SelectedValue = gvUsuario.DataKeys[index].Values[4].ToString();
                    txtContraseña.Text = gvUsuario.DataKeys[index].Values[5].ToString();

                    string estado = gvUsuario.DataKeys[index].Values[6].ToString();
                    string cambioClave = gvUsuario.DataKeys[index].Values[7].ToString();
                    if (estado == "1")
                    {
                        rdbInactivoUsuario.Checked = false;
                        rdbActivoUsuario.Checked = true;
                    }
                    else
                    {
                        rdbActivoUsuario.Checked = false;
                        rdbInactivoUsuario.Checked = true;
                    }


                }
                else if (e.CommandName == "eliminar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    UsuarioEntidad usuario = new UsuarioEntidad();
                    usuario.Id = Convert.ToInt32(gvUsuario.DataKeys[index].Values[0]);
                    usuario.Usuario_Sistema = gvUsuario.DataKeys[index].Values[1].ToString();
                    usuario.Nombre = gvUsuario.DataKeys[index].Values[2].ToString();
                    usuario.Correo = gvUsuario.DataKeys[index].Values[3].ToString();
                    usuario.Perfil = Convert.ToInt32(gvUsuario.DataKeys[index].Values[4]);
                    usuario.Contrasena = gvUsuario.DataKeys[index].Values[5].ToString();
                    usuario.Estado = 2;
                    usuario.Esquema = Session["Compañia"].ToString();
                    usuario.Usuario = ((UsuarioEntidad)(Session["UsuarioSistema"])).Usuario_Sistema;
                    usuario.Cambio_Clave = gvUsuario.DataKeys[index].Values[7].ToString();

                    new UsuarioLogica().MantUsuario(usuario);

                    CargarGridUsuario();
                    Mensaje("Usuario", "Desactivado correctamente", true);

                }
                else if (e.CommandName == "cambiarContrasena")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    UsuarioEntidad usuario = new UsuarioEntidad();
                    usuario.Id = Convert.ToInt32(gvUsuario.DataKeys[index].Values[0]);
                    usuario.Usuario_Sistema = gvUsuario.DataKeys[index].Values[1].ToString();
                    usuario.Nombre = gvUsuario.DataKeys[index].Values[2].ToString();
                    usuario.Correo = gvUsuario.DataKeys[index].Values[3].ToString();
                    usuario.Perfil = Convert.ToInt32(gvUsuario.DataKeys[index].Values[4]);
                    usuario.PerfilNombre = gvUsuario.DataKeys[index].Values[8].ToString();
                    usuario.Opcion = 1;
                    usuario.Esquema = Session["Compañia"].ToString();
                    usuario.Usuario = ((UsuarioEntidad)(Session["UsuarioSistema"])).Usuario_Sistema;
                    usuario.Cambio_Clave = "1";
                    usuario.Contrasena = contraseniaAlfanumerica(8);

                    new UsuarioLogica().MantUsuario(usuario);

                    if (hdfPermisoEnviarCorreos.Value == "1")
                    {
                        EnviarCorreoCrearUsuario(usuario);
                    }
                    Mensaje("Usuario", "Se ha reestablecido la clave correctamente", true);
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        protected void btnCancelarUsuario_Click(object sender, EventArgs e)
        {
            LimpiarCamposUsuario();
        }
        public bool ValidarEmailMultiple(string email)
        {
            try
            {
                string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                foreach (string correo in email.Split(';'))
                {
                    if (Regex.IsMatch(correo.Trim(), expresion))
                    {
                        if (Regex.Replace(correo.Trim(), expresion, String.Empty).Length == 0)
                        {
                            //return true;
                        }
                        else
                        { return false; }
                    }
                    else
                    { return false; }
                }
                return true;
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
                return false;
            }
        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            try
            {

                if (ValidarUsuarioCampos())
                {
                    return;
                }

                if (!ValidarEmailMultiple(txtCorreo.Text))
                {
                    Mensaje("Usuario", "Debe ingresar un correo con formato correcto", false);
                    return;
                }

                UsuarioEntidad usuario = new UsuarioEntidad();
                usuario.Id = !string.IsNullOrEmpty(hfIdUsuario.Value.ToString()) ? Convert.ToInt32(hfIdUsuario.Value) : 0;

                usuario.Esquema = "dbo";
                usuario.Usuario = ((UsuarioEntidad)(Session["UsuarioSistema"])).Usuario_Sistema;

                usuario.Usuario_Sistema = txtUsuario.Text;
                if (usuario.Id == 0)
                {
                    usuario.Opcion = 1;
                    if (new UsuarioLogica().GetUsuarios(usuario).Count() > 0)
                    {
                        Mensaje("Error", "Ya existe un usuario con este código, por favor digite otro código", false);
                        return;
                    }
                }

                usuario.Opcion = 0;
                usuario.Nombre = txtNombre.Text;
                usuario.Correo = txtCorreo.Text;
                usuario.Perfil = Convert.ToInt32(ddlPerfil.SelectedValue);
                usuario.PerfilNombre = ddlPerfil.SelectedItem.Text;
                usuario.Contrasena = txtContraseña.Text;
                usuario.Estado = rdbActivoUsuario.Checked ? 1 : 2;
                usuario.Cambio_Clave = "0";

                if (usuario.Id == 0)
                {
                    usuario.Contrasena = contraseniaAlfanumerica(8);
                }
                new UsuarioLogica().MantUsuario(usuario);

                if (hdfPermisoEnviarCorreos.Value == "1")
                {
                    if (usuario.Id == 0)
                    {
                        EnviarCorreoCrearUsuario(usuario);
                    }
                }
                LimpiarCamposUsuario();
                CargarGridUsuario();
                Mensaje("Usuario", "Guardada correctamente", true);
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
                    "<p> Se ha registrado su Usuario el día " + DateTime.Now.ToString("dd/MM/yyyy") + "</p>" +
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

        void LimpiarCamposUsuario()
        {
            hfIdUsuario.Value = null;
            txtUsuario.Text = string.Empty;
            txtUsuario.Enabled = true;
            txtNombre.Text = string.Empty;
            //txtContraseña.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            ddlPerfil.SelectedIndex = -1;
            rdbActivoUsuario.Checked = true;
            rdbInactivoUsuario.Checked = false;
        }
        private bool ValidarUsuarioCampos()
        {
            Regex regex = new Regex(@"^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{9,15}$");
            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                Mensaje("Aviso", "Debe digitar un usuario", false);
                return true;
            }
            else if (string.IsNullOrEmpty(txtNombre.Text))
            {
                Mensaje("Aviso", "Debe digitar un nombre", false);
                return true;
            }
            else if (string.IsNullOrEmpty(txtCorreo.Text))
            {
                Mensaje("Aviso", "Debe digitar un correo electrónico", false);
                return true;
            }
            else if (rdbActivoUsuario.Checked == false && rdbInactivoUsuario.Checked == false)
            {
                return true;
            }
            else if (!regex.IsMatch(txtContraseña.Text))
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
        #endregion

        private void CargarTreeViewPermisos()
        {
            try
            {
                TreeNode nodoMenu;
                MenuEntidad obMenuEntidad = new MenuEntidad();
                MenuLogica obMenuL = new MenuLogica();
                string usuarioLogin = "hcalvo";
                string Compania = "dbo";
                List<MenuEntidad> listaMenu = new List<MenuEntidad>();


                obMenuEntidad.Opcion = 0;
                obMenuEntidad.Usuario = usuarioLogin;
                obMenuEntidad.Esquema = Compania;
                obMenuEntidad.IdPerfil = string.IsNullOrEmpty(ddlPerfilPermisos.SelectedValue) ? 0 : Convert.ToInt32(ddlPerfilPermisos.SelectedValue);
                listaMenu = obMenuL.ObtenerMenu(obMenuEntidad);

                List<MenuEntidad> listaMenuPadre = listaMenu.ToList().Where(x => x.Codigo_Padre == "0").ToList();

                foreach (MenuEntidad iMenu in listaMenuPadre)
                {
                    nodoMenu = new TreeNode();
                    nodoMenu.Text = "&nbsp;<span><i class='" + iMenu.Icono + "' aria-hidden='true'></i> &nbsp;" + iMenu.Descripcion + "</span>";

                    nodoMenu.Value = "menu-" + iMenu.Id; // valor para identificar que el nodo pertenece a un menu
                    nodoMenu.ShowCheckBox = true;
                    nodoMenu.Checked = iMenu.EstadoMenu == "1" ? true : false;

                    ObtenerPermisos(nodoMenu,
                      iMenu.CrearMenu,
                      iMenu.EditarMenu,
                      iMenu.VerMenu,
                      iMenu.CrearPermiso,
                      iMenu.EditarPermiso,
                      iMenu.VerPermiso,
                      iMenu.Id,
                      iMenu.EnviarMenu,
                      iMenu.EnviarPermiso
                      );

                    ObtenerSubmenus(nodoMenu, iMenu, listaMenu);

                    tvPermisos.Nodes.Add(nodoMenu);
                }

            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);

            }

        }

        private void ObtenerSubmenus(TreeNode nodoPadre, MenuEntidad pMenuHijos, List<MenuEntidad> plistaMenu)
        {
            List<MenuEntidad> listaMenuHijos = new List<MenuEntidad>();
            List<MenuEntidad> listaMenuTieneHijos = new List<MenuEntidad>();
            List<MenuEntidad> listaMenu = plistaMenu;

            TreeNode nodoSubmenu;

            try
            {
                listaMenuHijos = listaMenu.ToList().Where(x => x.Codigo_Padre == pMenuHijos.Id.ToString()).ToList();
                // verificacion para identificar si el menu tiene submenus
                if (listaMenuHijos.Count > 0)
                {
                    foreach (MenuEntidad submenu in listaMenuHijos)
                    {
                        nodoSubmenu = new TreeNode();

                        nodoSubmenu.Text = "&nbsp;<span><i class='" + submenu.Icono + "' aria-hidden='true'></i> &nbsp;" + submenu.Descripcion + "</span>";
                        nodoSubmenu.Value = "menu-" + submenu.Id; // valor para identificar que el nodo pertenece a un menu
                        nodoSubmenu.ShowCheckBox = true;
                        nodoSubmenu.Checked = submenu.Estado == "1" ? true : false;

                        ObtenerPermisos(nodoSubmenu,
                          submenu.CrearMenu,
                          submenu.EditarMenu,
                          submenu.VerMenu,
                          submenu.CrearPermiso,
                          submenu.EditarPermiso,
                          submenu.VerPermiso,
                          submenu.Id,
                          submenu.EnviarMenu,
                          submenu.EnviarPermiso);

                        ObtenerSubmenus(nodoSubmenu, submenu, listaMenu);

                        nodoPadre.ChildNodes.Add(nodoSubmenu);
                    }
                }

            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }

        }

        private void ObtenerPermisos(TreeNode nodo,
          bool CrearMenu,
          bool EditarMenu,
          bool VerMenu,
          bool CrearPermiso,
          bool EditarPermiso,
          bool VerPermiso,
          int ID,
          bool EnviarMenu,
          bool EnviarPermiso)
        {
            try
            {
                if (CrearMenu)
                {
                    TreeNode nodoPermiso = new TreeNode();
                    nodoPermiso.Text = "<i class='fa fa-plus' aria-hidden='true'></i> &nbsp;<span>Crear </span>";
                    nodoPermiso.Value = "permiso-crear/" + ID;
                    nodoPermiso.ShowCheckBox = true;
                    nodoPermiso.Checked = CrearPermiso ? true : false;
                    nodo.ChildNodes.Add(nodoPermiso);
                }
                if (EditarMenu)
                {
                    TreeNode nodoPermiso = new TreeNode();
                    nodoPermiso.Text = "<i class='fa fa-align-justify' aria-hidden='true'></i> &nbsp;<span>Editar </span>";
                    nodoPermiso.Value = "permiso-editar/" + ID;
                    nodoPermiso.ShowCheckBox = true;
                    nodoPermiso.Checked = EditarPermiso ? true : false;
                    nodo.ChildNodes.Add(nodoPermiso);
                }
                if (VerMenu)
                {
                    TreeNode nodoPermiso = new TreeNode();
                    nodoPermiso.Text = "<i class='fa fa-align-justify' aria-hidden='true'></i> &nbsp;<span>Consultar </span>";
                    nodoPermiso.Value = "permiso-ver/" + ID;
                    nodoPermiso.ShowCheckBox = true;
                    nodoPermiso.Checked = VerPermiso ? true : false;
                    nodo.ChildNodes.Add(nodoPermiso);
                }

                if (EnviarMenu)
                {
                    TreeNode nodoPermiso = new TreeNode();
                    nodoPermiso.Text = "<i class='fa fa-envelope-o' aria-hidden='true'></i> &nbsp;<span>Enviar Correos </span>";
                    nodoPermiso.Value = "permiso-enviar/" + ID;
                    nodoPermiso.ShowCheckBox = true;
                    nodoPermiso.Checked = EnviarPermiso ? true : false;
                    nodo.ChildNodes.Add(nodoPermiso);
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }


        protected void tvPermisos_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            try
            {
                TreeNode nodoCheckeado = e.Node;
                TreeNode nodoPadre = nodoCheckeado.Parent ?? nodoCheckeado;
                int contNodosCheckeados = 0;
                IList<TreeNode> listaPadres = TreeHelpers.GetAncestors(nodoCheckeado, nodo => nodo.Parent).ToList();
                int accionARealizar;

                // identificando si el nodo esta deshabilitado
                if (!nodoCheckeado.Text.Contains("color: grey"))
                {
                    contNodosCheckeados = ObtenerNodosHermanosCheckeados(nodoPadre);
                    accionARealizar = AccionARealizarConNodosPadres(contNodosCheckeados, listaPadres.Count);

                    if (accionARealizar == CHECKEAR_NODOS)
                    {
                        cambiarEstadoNodosPadre(true, listaPadres);

                    }
                    else if (accionARealizar == DESCHECKEAR_NODOS)
                    {
                        cambiarEstadoNodosPadre(false, listaPadres);
                    }

                    CambiarEstadoNodosHijos(nodoCheckeado);
                }
                else
                {
                    nodoCheckeado.Checked = !nodoCheckeado.Checked;
                }


            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        private int ObtenerNodosHermanosCheckeados(TreeNode nodoPadre)
        {
            int contNodosCheckeados = 0;

            foreach (TreeNode node in nodoPadre.ChildNodes)
            {
                if (node.Checked == true)
                {
                    contNodosCheckeados++;
                }
            }

            return contNodosCheckeados;

        }

        private int AccionARealizarConNodosPadres(int contNodosCheckeados, int tamListaDesendencia)
        {
            int accion = -1;

            if (contNodosCheckeados == 1 && tamListaDesendencia > 0)
                accion = CHECKEAR_NODOS;

            if (contNodosCheckeados == 0 && tamListaDesendencia > 0)
                accion = DESCHECKEAR_NODOS;

            return accion;

        }

        private void cambiarEstadoNodosPadre(bool estado, IList<TreeNode> nodosPadre)
        {
            int contNodosPadresCheckeados = 0;
            // verificando si existen nodos padres que modificar
            if (nodosPadre.Count > 0)
            {
                for (int i = 0; i < nodosPadre.Count; i++)
                {
                    // cambiando el estado del primer nodo padre
                    nodosPadre[i].Checked = estado;
                    // verificando si se esta descheckeando los nodos padre
                    if (!estado)
                    {
                        // obteniendo la cantidad de nodos padre hermanos que se encuentran checkeados en el caso de que el nodo padre los tenga
                        if (nodosPadre.Count > 1 && i < nodosPadre.Count - 1)
                        {
                            contNodosPadresCheckeados = ObtenerNodosHermanosCheckeados(nodosPadre[i + 1]);
                            // evitando el cambio de estado de los demas padres cuando se encuentra que existen hijos checkeados
                            if (contNodosPadresCheckeados >= 1)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void CambiarEstadoNodosHijos(TreeNode nodoCheckeado)
        {

            IEnumerable<TreeNode> desendencia;
            if (nodoCheckeado.ChildNodes.Count > 0)
            {
                desendencia = ObtenerDesendencia(nodoCheckeado);

                foreach (TreeNode nodoDesendiente in desendencia)
                {
                    nodoDesendiente.Checked = nodoCheckeado.Checked;
                }
            }
        }

        private List<TreeNode> ObtenerDesendencia(TreeNode nodo)
        {
            List<TreeNode> listaNodos = new List<TreeNode>();

            if (nodo.ChildNodes.Count > 0)
            {
                foreach (TreeNode nodoHijo in nodo.ChildNodes)
                {
                    listaNodos.AddRange(ObtenerDesendencia(nodoHijo));

                    listaNodos.Add(nodoHijo);
                }
            }
            else
            {
                listaNodos.Add(nodo);
            }

            return listaNodos;
        }

        private void EliminarNodosTreeView()
        {
            tvPermisos.Nodes.Clear();
            tvPermisos.ExpandAll();

        }


        protected void btnGuardarPermisos_Click(object sender, EventArgs e)
        {

            try
            {
                if (ddlPerfilPermisos.SelectedValue == "0")
                {
                    Mensaje("Error", "Debe de seleccionar un perfil antes de insertar los permisos", false);
                    return;
                }

                PrivilegioEntidad obPrivilegiosE = new PrivilegioEntidad();
                PrivilegioLogica obPrivilegiosL = new PrivilegioLogica();
                string usuarioLogin = ((UsuarioEntidad)(Session["UsuarioSistema"])).Usuario_Sistema;
                string Compania = Session["Compañia"].ToString();
                string result = "";

                string listaMenusSeleccionados = ObtenerIdsDeNodosCheckeados("menu");
                string listaPermisosSeleccionados = ObtenerIdsDeNodosCheckeados("permiso");

                obPrivilegiosE.Opcion = 0;
                obPrivilegiosE.Esquema = Compania;
                obPrivilegiosE.Usuario = usuarioLogin;
                obPrivilegiosE.IdPerfil = Convert.ToInt32(ddlPerfilPermisos.SelectedValue);
                obPrivilegiosE.ListaMenu = listaMenusSeleccionados;
                obPrivilegiosE.ListaPermisos = listaPermisosSeleccionados;

                result = obPrivilegiosL.InsertarPermisos(obPrivilegiosE);

                Limpiar();

                Mensaje("Permisos", result, true);

            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);

            }
        }

        protected void btnCancelarPermisos_Click(object sender, EventArgs e)
        {
            try
            {
                Limpiar();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);

            }
        }

        public void Limpiar()
        {
            try
            {
                ddlPerfilPermisos.SelectedValue = "0";
                EliminarNodosTreeView();
                CargarTreeViewPermisos();
                tvPermisos.ExpandAll();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        private string ObtenerIdsDeNodosCheckeados(string datoAContener = null) // dato para identificar cualesc a cuales nodos seleccionados se le van a obtener el valor
        {

            int size = tvPermisos.CheckedNodes.Count;
            TreeNode[] list = new TreeNode[size];
            tvPermisos.CheckedNodes.CopyTo(list, 0);

            string datosNodosPermisos = "";

            foreach (TreeNode nodo in list)
            {
                if (datoAContener != null)
                {
                    if (nodo.Value.Contains(datoAContener))
                    {
                        datosNodosPermisos += string.Format("{0},", // se utiliza la coma para separar cada uno de los permisos
                                                                    nodo.Value.Split('-')[1]);
                    }
                }
                else
                {
                    datosNodosPermisos += string.Format("{0},", // se utiliza la coma para separar cada uno de los permisos
                                                                    nodo.Value.Split('-')[1]);
                }



            }

            return datosNodosPermisos += "";
        }

        protected void ddlPerfilPermisos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                EliminarNodosTreeView();
                CargarTreeViewPermisos();
                tvPermisos.ExpandAll();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }
    }
}