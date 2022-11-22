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
    public class TiempoRealEstimadoDatos
    {
        public static List<TiempoRealEstimadoEntidad> GetTiempoRealEstimado(TiempoRealEstimadoEntidad tiempoRealEstimadoEntidad)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{tiempoRealEstimadoEntidad.Esquema}.PA_CON_SIMP_TIEMPO_REAL_ESTIMADO";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", tiempoRealEstimadoEntidad.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", tiempoRealEstimadoEntidad.Opcion);

                cmd.Parameters.AddWithValue("@P_PK_TBL_SIMP_PY_PROYECTO", tiempoRealEstimadoEntidad.IdProyecto);
                cmd.Parameters.AddWithValue("@P_FECHA_INICIO", tiempoRealEstimadoEntidad.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@P_FECHA_FINAL", tiempoRealEstimadoEntidad.Fecha_Final);
                cmd.Parameters.AddWithValue("@P_ESQUEMA", tiempoRealEstimadoEntidad.Esquema);

                myConexion.Open();
                reader = cmd.ExecuteReader();

                List<TiempoRealEstimadoEntidad> lista = new List<TiempoRealEstimadoEntidad>();

                while (reader.Read())
                {
                    TiempoRealEstimadoEntidad obj = new TiempoRealEstimadoEntidad();
                    obj.IdProyecto = UtilitarioSQL.ObtieneInt(reader, "PK_TBL_SIMP_PY_PROYECTO");
                    obj.Nombre_Cliente = UtilitarioSQL.ObtieneString(reader, "NOMBRE_CLIENTE");
                    obj.Nombre_Proyecto = UtilitarioSQL.ObtieneString(reader, "NOMBRE_PROYECTO");
                    obj.Nombre_Fase = UtilitarioSQL.ObtieneString(reader, "NOMBRE_FASE");
                    obj.Nombre_Actividad = UtilitarioSQL.ObtieneString(reader, "NOMBRE_ACTIVIDAD");
                    obj.Nombre_Usuario = UtilitarioSQL.ObtieneString(reader, "NOMBRE_USUARIO");
                    obj.Horas_Estimadas = UtilitarioSQL.ObtieneDecimal(reader, "HORAS_ESTIMADAS");
                    obj.Horas_Reales = UtilitarioSQL.ObtieneDecimal(reader, "HORAS_REALES");
                    obj.Fecha = UtilitarioSQL.ObtieneDateTime(reader, "FECHA");

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
