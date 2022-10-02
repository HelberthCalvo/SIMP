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

        protected void gvClientes_PreRender(object sender, EventArgs e)
        {

        }

        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            var id = gvClientes.Rows[index].Cells[0].Text;
            var nombre = gvClientes.Rows[index].Cells[1].Text;
            var primer_apellido = gvClientes.Rows[index].Cells[2].Text;
            var segundo_apellido = gvClientes.Rows[index].Cells[3].Text;
            var correo_electronico = gvClientes.Rows[index].Cells[4].Text;
            var telefono = gvClientes.Rows[index].Cells[5].Text;

            if (e.CommandName == "Editar")
            {
                idCliente.Value = Convert.ToInt32(id).ToString();
                txbNombre.Text = nombre;
                txbApellido1.Text = primer_apellido;
                txbApellido2.Text = segundo_apellido;
                txbEmail.Text = correo_electronico;
                txbTelefono.Text = telefono;
            }
            else if (e.CommandName == "Eliminar")
            {
                ClienteLogica.MantCliente(new ClienteEntidad { Id = Convert.ToInt32(id), Opcion = 1, Esquema = "dbo" });
            }

        }
        private void CargarGridCliente()
        {
            try
            {
                List<ClienteEntidad> lstCliente = new List<ClienteEntidad>();
                lstCliente = ClienteLogica.GetClientes(new ClienteEntidad() { Id = 0, Opcion = 0, Esquema = "dbo" });
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (CamposVacios())
            {
                Mensaje("Aviso", "Debe ingresar todos los datos", false);
                return;
            }

            ClienteEntidad cliente = new ClienteEntidad()
            {
                Nombre = txbNombre.Text,
                Primer_Apellido = txbApellido1.Text,
                Segundo_Apellido = txbApellido2.Text,
                Correo_Electronico = txbEmail.Text,
                Telefono = txbTelefono.Text,
                Estado = "",
                Usuario = "hcalvo",
                Esquema = "dbo",
                Opcion = 0
            };
            if (!string.IsNullOrEmpty(idCliente.Value))
            {
                cliente.Id = Convert.ToInt32(idCliente.Value);
                idCliente.Value = "";
            }
            ClienteLogica.MantCliente(cliente);

            LimpiarCampos();
            CargarGridCliente();
        }
        private bool CamposVacios()
        {
            if (string.IsNullOrEmpty(txbNombre.Text))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(txbApellido1.Text))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(txbApellido2.Text))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(txbEmail.Text))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(txbTelefono.Text))
            {
                return true;
            }
            return false;
        }

        private void LimpiarCampos()
        {
            txbNombre.Text = null;
            txbApellido1.Text = string.Empty;
            txbApellido2.Text = string.Empty;
            txbEmail.Text = string.Empty;
            txbTelefono.Text = string.Empty;
        }
    }
}