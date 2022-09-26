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
                Nombre = txbNombre.Text,
                Primer_Apellido = txbApellido1.Text,
                Segundo_Apellido = txbApellido2.Text,
                Correo_Electronico = txbEmail.Text,
                Telefono = txbTelefono.Text,
                Estado = "",
                Usuario = "hcalvo",
                Esquema = "dbo"
            };
            ClienteLogica.MantCliente(cliente);

            LimpiarCampos();
            Response.Redirect("Index.aspx");
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
        }
    }
}