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
    public class FaseDatos
    {
        public static List<FaseEntidad> GetFases(FaseEntidad fase)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{fase.Esquema}.PA_CON_FASE";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", fase.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", fase.Opcion);

                cmd.Parameters.AddWithValue("@P_ID", fase.Id);
                cmd.Parameters.AddWithValue("@P_IDPROYECTO", fase.IdProyecto);
                cmd.Parameters.AddWithValue("@P_IDESTADO", fase.IdEstado);
                cmd.Parameters.AddWithValue("@P_NOMBRE", fase.Nombre);
                cmd.Parameters.AddWithValue("@P_DESCRIPCION", fase.Descripcion);

                cmd.Parameters.AddWithValue("@P_ESQUEMA", fase.Esquema);

                myConexion.Open();
                reader = cmd.ExecuteReader();

                List<FaseEntidad> lista = new List<FaseEntidad>();

                while (reader.Read())
                {
                    FaseEntidad obj = new FaseEntidad();
                    obj.Id = UtilitarioSQL.ObtieneInt(reader, "PK_TBL_SIMP_PY_FASE");
                    obj.IdProyecto = UtilitarioSQL.ObtieneInt(reader, "FK_TBL_SIMP_PY_PROYECTO");
                    obj.IdEstado = UtilitarioSQL.ObtieneInt(reader, "FK_TBL_SIMP_ESTADO");
                    obj.Nombre = UtilitarioSQL.ObtieneString(reader, "NOMBRE");
                    obj.Descripcion = UtilitarioSQL.ObtieneString(reader, "DESCRIPCION");
                    obj.NombreProyecto = UtilitarioSQL.ObtieneString(reader, "NOMBREPROYECTO");
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
        public static void MantFase(FaseEntidad fase)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{fase.Esquema}.PA_MAN_FASE";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", fase.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", fase.Opcion);

                cmd.Parameters.AddWithValue("@P_ID", fase.Id);
                cmd.Parameters.AddWithValue("@P_IDPROYECTO", fase.IdProyecto);
                cmd.Parameters.AddWithValue("@P_IDESTADO", fase.IdEstado);
                cmd.Parameters.AddWithValue("@P_NOMBRE", fase.Nombre);
                cmd.Parameters.AddWithValue("@P_DESCRIPCION", fase.Descripcion);

                cmd.Parameters.AddWithValue("@P_ESQUEMA", fase.Esquema);

                myConexion.Open();
                reader = cmd.ExecuteReader();

                reader.Dispose();
                cmd.Dispose();
                myConexion.Close();
                myConexion.Dispose();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en Base de Datos" + ex.Message);
            }
        }
    }
}
