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
            Session["UsuarioSistema"] = "hcalvo";
            if (Session["UsuarioSistema"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                //HabilitaOpcionesPermisos();
                CargarGridProyectos();
                CargarClientes();
            }
        }

        private string NombreCompleto(string nombre, string primer_apellido)
        {
            return nombre + " " + primer_apellido;
        }

        private void CargarClientes()
        {
            try
            {
                List<ClienteEntidad> lstClientes = new List<ClienteEntidad>();
                lstClientes = ClienteLogica.GetClientes(new ClienteEntidad { Esquema = "dbo" });
                lstClientes.ForEach(x => { x.Nombre = NombreCompleto(x.Nombre, x.Primer_Apellido); });

                ddlClientes.DataSource = lstClientes;

                ddlClientes.DataTextField = "Nombre";
                ddlClientes.DataValueField = "Id";
                ddlClientes.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        private void Mensaje(string titulo, string msg, bool esCorrecto, string textoBoton = "Ok")
        {
            string icono = esCorrecto ? "success" : "error";
            string script = $"alert({msg})";
            //string script = "Swal.fire({ title: '" + titulo + "!', text: '" + msg + "', icon: '" + icono + "', confirmButtonText: '" + textoBoton + "' })";
            ScriptManager.RegisterStartupScript(this, GetType(), "script", script, true);
        }

        private string FormatoFecha(string fecha)
        {
            var lstFecha = fecha.Split('/');
            string formato = lstFecha[1] + "/" + lstFecha[0] + "/" + lstFecha[2];
            return formato;
        }

        private void CargarGridProyectos()
        {
            try
            {
                List<ProyectoEntidad> lstProyetos = new List<ProyectoEntidad>();
                lstProyetos = ProyectoLogica.GetProyectos(new ProyectoEntidad() { 
                    Esquema = "dbo"
                }).FindAll(x=>x.IdEstado==1);
                gvProyectos.DataSource = lstProyetos;
                gvProyectos.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (CamposVacios())
                {
                    Mensaje("Aviso", "Debe ingresar todos los datos", false);
                    return;
                }

                ProyectoEntidad proyecto = new ProyectoEntidad()
                {
                    Nombre = txbNombre.Text,
                    Descripcion = txbDescripcion.Text,
                    Fecha_Inicio = txbFechaInicio.Text,
                    Fecha_Estimada = txbFechaEstimada.Text,
                    IdCliente = Convert.ToInt32(ddlClientes.SelectedValue),
                    IdEstado = 1,
                    Usuario = "hcalvo",
                    Esquema = "dbo",
                    Opcion = 0
                };
                if (!string.IsNullOrEmpty(idProyecto.Value))
                {
                    proyecto.Id = Convert.ToInt32(idProyecto.Value);
                    proyecto.Fecha_Inicio = FormatoFecha(txbFechaInicio.Text);
                    proyecto.Fecha_Estimada = FormatoFecha(txbFechaEstimada.Text);
                    idProyecto.Value = "";
                }
                ProyectoLogica.MantProyecto(proyecto);

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
                return true;
            }
            else if (string.IsNullOrEmpty(txbDescripcion.Text))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(txbFechaInicio.Text))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(txbFechaEstimada.Text))
            {
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
        }

        protected void gvProyectos_PreRender(object sender, EventArgs e)
        {

        }

        protected void gvProyectos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            var id = gvProyectos.Rows[index].Cells[0].Text;
            var idCliente = gvProyectos.Rows[index].Cells[1].Text;
            var nombre = gvProyectos.Rows[index].Cells[2].Text;
            var descripcion = gvProyectos.Rows[index].Cells[4].Text;
            var fecha_inicio = gvProyectos.Rows[index].Cells[5].Text;
            var fecha_estimada = gvProyectos.Rows[index].Cells[6].Text;

            if (e.CommandName == "Editar")
            {
                idProyecto.Value = Convert.ToInt32(id).ToString();
                txbNombre.Text = nombre;
                txbDescripcion.Text = descripcion;
                ddlClientes.SelectedValue = idCliente;
                txbFechaInicio.Text = fecha_inicio;
                txbFechaEstimada.Text = fecha_estimada;
            }
            else if (e.CommandName == "Eliminar")
            {
                //ClienteLogica.MantCliente(new ClienteEntidad { Id = Convert.ToInt32(id), Opcion = 1, Esquema = "dbo" });
            }
            else if (e.CommandName == "Finalizar")
            {
                ProyectoLogica.MantProyecto(new ProyectoEntidad { Id = Convert.ToInt32(id), Opcion = 0, Esquema = "dbo", IdEstado = 2, IdCliente = Convert.ToInt32(idCliente) });
                CargarGridProyectos();
            }
        }
    }
}