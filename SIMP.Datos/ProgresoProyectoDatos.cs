using SIMP.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Datos
{
    public class ProgresoProyectoDatos
    {
        public static List<ProgresoProyectoEntidad> GetProgresoProyecto(ProgresoProyectoEntidad progresoProyecto)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{progresoProyecto.Esquema}.PA_CON_SIMP_PROGRESO_PROYECTO";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", progresoProyecto.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", progresoProyecto.Opcion);

                cmd.Parameters.AddWithValue("@P_PK_TBL_SIMP_PY_PROYECTO", progresoProyecto.IdProyecto);
                cmd.Parameters.AddWithValue("@P_FECHA_INICIO", progresoProyecto.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@P_FECHA_FINAL", progresoProyecto.Fecha_Final);
                cmd.Parameters.AddWithValue("@P_ESQUEMA", progresoProyecto.Esquema);

                myConexion.Open();
                reader = cmd.ExecuteReader();

                List<ProgresoProyectoEntidad> lista = new List<ProgresoProyectoEntidad>();

                while (reader.Read())
                {
                    ProgresoProyectoEntidad obj = new ProgresoProyectoEntidad();
                    obj.IdProyecto = UtilitarioSQL.ObtieneInt(reader, "PK_TBL_SIMP_PY_PROYECTO");
                    obj.Nombre_Proyecto = UtilitarioSQL.ObtieneString(reader, "NOMBRE_PROYECTO");
                    obj.Nombre_Fase = UtilitarioSQL.ObtieneString(reader, "NOMBRE_FASE");
                    obj.Nombre_Actividad = UtilitarioSQL.ObtieneString(reader, "NOMBRE ACTIVIDAD");
                    obj.Fecha_Inicio = UtilitarioSQL.ObtieneDateTime(reader, "FECHA_INICIO");
                    obj.Porcentaje = UtilitarioSQL.ObtieneString(reader, "PORCENTAJE");
                    lista.Add(obj);
                }
                reader.Dispose();
                cmd.Dispose();
                myConexion.Close();
                myConexion.Dispose();
                return lista;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en Base de Datos" + ex.Message);
            }
        }
    }
}
