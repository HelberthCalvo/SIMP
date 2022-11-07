using SIMP.Entidades;
using SIMP.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;

namespace SIMP
{
    public partial class GanttChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioSistema"] == null || Session["IdProyectoGantt"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            CargarDatos();
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

        private void CargarDatos()
        {
            List<GanttEntidad> datos = new List<GanttEntidad>();
            int idProyecto = Convert.ToInt32(Session["IdProyectoGantt"].ToString());

            var listaActividades = ActividadLogica.GetActividades(new ActividadEntidad()
            {
                IdProyecto = idProyecto,
                Opcion = 2,
                Estado = "1"
            });

            int cont = 0;
            string nombreFase = "";
            int indexColor = 0;
            string[] colores = { "ganttOrange", "ganttGreen", "ganttRed" };
            string colorActual = "ganttOrange";

            foreach (var item in listaActividades)
            {
                var values = new List<GanttValues>();
                var fecha_inicio = item.Fecha_Inicio.Split(' ')[0];
                var fecha_finalizacion = item.Fecha_Finalizacion.Split(' ')[0];

                var ganttValues = new GanttValues()
                {
                    from = FormatoFecha(fecha_inicio),
                    to = FormatoFecha(fecha_finalizacion),
                    label = item.Descripcion,
                    customClass = colorActual,
                    dataObj = { },
                    desc = "Actividad: " + item.Descripcion + " | Horas estimadas: " + item.HorasEstimadas.ToString() + " | Horas reales: " + item.HorasReales.ToString() + " |"
                };
                var ganttEntidad = new GanttEntidad()
                {
                    name = item.NombreFase,
                    desc = item.Descripcion,
                    values = values
                };

                if (cont <= 0)
                {
                    cont++;
                    nombreFase = item.NombreFase;
                }
                else
                {
                    //Misma fase
                    if (nombreFase == item.NombreFase)
                    {
                        ganttEntidad.name = "";
                        ganttValues.customClass = colorActual;
                    }
                    //Diferente fase
                    else
                    {
                        var valor = indexColor + 1;
                        if (valor == 3)
                        {
                            //Reinicia los valores
                            indexColor = 0;
                            colorActual = "ganttOrange";
                        }
                        else
                        {
                            //Coloca el color actual de la fase
                            colorActual = colores[valor];
                            indexColor++;
                        }
                        ganttValues.customClass = colorActual;
                        
                    }
                }
                values.Add(ganttValues);
                datos.Add(ganttEntidad);
                nombreFase = item.NombreFase;
            }
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(datos, options);
            ScriptManager.RegisterStartupScript(this, GetType(), "ganttData", "CargarDatos(" + jsonString + ")", true);
        }

    }
}