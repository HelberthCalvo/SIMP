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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                if (CamposVacios())
                {
                    return;
                }

                UsuarioEntidad usuario = new UsuarioEntidad()
                {
                    Esquema = "dbo",
                    Usuario = txtNombreUsuario.Text,
                    Contrasena = txtContrasena.Text,
                    Opcion = 1,
                    Estado = 1
                };
                UsuarioLogica usuario1 = new UsuarioLogica();
                List<UsuarioEntidad> usuarioBusqueda = usuario1.GetUsuarios(usuario);
                if (usuarioBusqueda.Count > 0)
                {
                    Session["UsuarioSistema"] = usuarioBusqueda[0];
                    Session["Compañia"] = "dbo";
                    Response.Redirect("Proyecto.aspx",false);
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

        private bool CamposVacios()
        {

            if (string.IsNullOrEmpty(txtNombreUsuario.Text))
            {
                Mensaje("Aviso", "Debe ingresar un nombre", false);
                return true;
            }
            else if (string.IsNullOrEmpty(txtContrasena.Text))
            {
                Mensaje("Aviso", "Debe ingresar una contraseña", false);
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

    }
}