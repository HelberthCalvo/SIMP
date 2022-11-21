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
    public static class BitacoraDatos
    {
        public static List<BitacoraEntidad> GetBitacoras(BitacoraEntidad bitacora)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{bitacora.Esquema}.PA_CON_TBL_SIMP_BITACORA";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", bitacora.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", bitacora.Opcion);
                cmd.Parameters.AddWithValue("@P_ID", bitacora.Id);
                cmd.Parameters.AddWithValue("@P_TABLA", bitacora.Tabla);
                cmd.Parameters.AddWithValue("@P_ACCION", bitacora.Accion);
                cmd.Parameters.AddWithValue("@P_DATOS_ANTERIORES", bitacora.Datos_Anteriores);
                cmd.Parameters.AddWithValue("@P_DATOS_NUEVOS", bitacora.Datos_Nuevos);
                cmd.Parameters.AddWithValue("@P_FECHA_INICIO", bitacora.FechaInicio);
                cmd.Parameters.AddWithValue("@P_FECHA_FINAL", bitacora.FechaFinal);
                cmd.Parameters.AddWithValue("@P_ESQUEMA", bitacora.Esquema);

                myConexion.Open();
                reader = cmd.ExecuteReader();

                List<BitacoraEntidad> lista = new List<BitacoraEntidad>();

                while (reader.Read())
                {
                    BitacoraEntidad obj = new BitacoraEntidad();
                    obj.Id = UtilitarioSQL.ObtieneInt(reader, "PK_TBL_SIMP_BITACORA");
                    obj.Usuario = UtilitarioSQL.ObtieneString(reader, "USUARIO");
                    obj.Tabla = UtilitarioSQL.ObtieneString(reader, "TABLA");
                    obj.Accion = UtilitarioSQL.ObtieneString(reader, "ACCION");
                    obj.Datos_Anteriores = UtilitarioSQL.ObtieneString(reader, "DATOS_ANTERIORES");
                    obj.Datos_Nuevos = UtilitarioSQL.ObtieneString(reader, "DATOS_NUEVOS");
                    obj.UsuarioBitacora = UtilitarioSQL.ObtieneString(reader, "USUARIO");
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
