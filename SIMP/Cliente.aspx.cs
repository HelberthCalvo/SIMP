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
    public partial class Cliente : System.Web.UI.Page
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
                CargarGridCliente();
            }
        }

        private void CargarGridCliente()
        {
            try
            {
                List<ClienteEntidad> lstCliente = new List<ClienteEntidad>();
                lstCliente = new ClienteLogica().GetClientes(new ClienteEntidad() { Id = 0, Opcion = 0, Esquema = "dbo" });
                gvClientes.DataSource = lstCliente;
                gvClientes.DataBind();
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

        protected void gvClientes_PreRender(object sender, EventArgs e)
        {

        }

        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (CamposVacios())
            {
                //Mensaje("Moneda", "Debe ingresar todos los datos", false);
                return;
            }
            ClienteEntidad cliente = new ClienteEntidad()
            {
                Nombre = txtNombre.Text,
                Primer_Apellido = txtApellido1.Text,
                Segundo_Apellido = txtApellido2.Text,
                Correo_Electronico = txtEmail.Text,
                Telefono = txtTelefono.Text,
                Estado = "",
                Usuario = "hcalvo",
                Esquema = "dbo"
            };
            new ClienteLogica().MantCliente(cliente);

            LimpiarCampos();
            Response.Redirect("Index.aspx");
        }
        private bool CamposVacios()
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(txtApellido1.Text))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(txtApellido2.Text))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(txtEmail.Text))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                return true;
            }
            return false;
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = null;
            txtApellido1.Text = string.Empty;
            txtApellido2.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }
    }
}