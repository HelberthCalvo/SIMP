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
    public class UsuarioDatos
    {
        public List<Usuario> GetUsuarios(Usuario usuario)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{usuario.Esquema}.PA_CON_TBL_FLOW_CANALES";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", usuario.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", usuario.Opcion);
                cmd.Parameters.AddWithValue("@P_PK_TBL_FLOW_CANALES", usuario.ID);
                cmd.Parameters.AddWithValue("@P_FK_TBL_FLOW_PRODUCTO", usuario.IDProducto);
                cmd.Parameters.AddWithValue("@P_FK_TBL_FLOW_FLUJO", usuario.IDFlujo);
                cmd.Parameters.AddWithValue("@P_ESTADO", usuario.Estado);
                cmd.Parameters.AddWithValue("@P_ESQUEMA", usuario.Esquema);

                myConexion.Open();
                reader = cmd.ExecuteReader();

                List<Usuario> lista = new List<Usuario>();

                while (reader.Read())
                {
                    Usuario obj = new Usuario();
                    obj.ID = UtilitarioSQL.ObtieneInt(reader, "PK_TBL_FLOW_CANALES");
                    obj.Descripcion = UtilitarioSQL.ObtieneString(reader, "DESCRIPCION");
                    obj.Estado = UtilitarioSQL.ObtieneString(reader, "ESTADO");

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
        public void MantUsuario(Usuario usuario)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{canal.Esquema}.PA_MAN_TBL_FLOW_CANALES";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", canal.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", canal.Opcion);
                cmd.Parameters.AddWithValue("@P_PK_TBL_FLOW_CANALES", canal.ID);
                cmd.Parameters.AddWithValue("@P_DESCRIPCION", canal.Descripcion);
                cmd.Parameters.AddWithValue("@P_ESTADO", canal.Estado);
                cmd.Parameters.AddWithValue("@P_ESQUEMA", canal.Esquema);

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
