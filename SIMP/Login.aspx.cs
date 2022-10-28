using SIMP.Entidades;
using SIMP.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    Response.Redirect("Proyecto.aspx", false);
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

    }
}