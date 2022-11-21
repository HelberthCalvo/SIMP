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
    public class CargaUsuarioDatos
    {
        public static List<CargaTrabajoEntidad> GetCargaTrabajo(CargaTrabajoEntidad cargaTrabajoEntidad)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{cargaTrabajoEntidad.Esquema}.PA_CON_SIMP_CARGA_USUARIO";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", cargaTrabajoEntidad.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", cargaTrabajoEntidad.Opcion);

                cmd.Parameters.AddWithValue("@P_PK_TBL_SIMP_SEG_USUARIO", cargaTrabajoEntidad.IdUsuario);
                cmd.Parameters.AddWithValue("@P_FECHA_INICIO", "");
                cmd.Parameters.AddWithValue("@P_FECHA_FINAL", "");
                cmd.Parameters.AddWithValue("@P_ESQUEMA", cargaTrabajoEntidad.Esquema);

                myConexion.Open();
                reader = cmd.ExecuteReader();

                List<CargaTrabajoEntidad> lista = new List<CargaTrabajoEntidad>();

                while (reader.Read())
                {
                    CargaTrabajoEntidad obj = new CargaTrabajoEntidad();
                    obj.IdUsuario = UtilitarioSQL.ObtieneInt(reader, "PK_TBL_SIMP_SEG_USUARIO");
                    obj.Nombre_Cliente = UtilitarioSQL.ObtieneString(reader, "NOMBRE_CLIENTE");
                    obj.Nombre_Proyecto = UtilitarioSQL.ObtieneString(reader, "NOMBRE_PROYECTO");
                    obj.Nombre_Fase = UtilitarioSQL.ObtieneString(reader, "NOMBRE_FASE");
                    obj.Nombre_Actividad = UtilitarioSQL.ObtieneString(reader, "NOMBRE_ACTIVIDAD");
                    obj.Nombre_Usuario = UtilitarioSQL.ObtieneString(reader, "NOMBRE_USUARIO");
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
