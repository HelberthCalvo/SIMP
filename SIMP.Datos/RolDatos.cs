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
    public class RolDatos
    {
        public List<RolEntidad> GetRoles(RolEntidad rol)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{rol.Esquema}.PA_CON_ROL";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", rol.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", rol.Opcion);

                cmd.Parameters.AddWithValue("@P_ID", rol.Id);
                cmd.Parameters.AddWithValue("@P_DESCRIPCION", rol.Descripcion);


                cmd.Parameters.AddWithValue("@P_ESQUEMA", rol.Esquema);

                myConexion.Open();
                reader = cmd.ExecuteReader();

                List<RolEntidad> lista = new List<RolEntidad>();

                while (reader.Read())
                {
                    RolEntidad obj = new RolEntidad();
                    obj.Id = UtilitarioSQL.ObtieneInt(reader, "PK_TBL_SIMP_ROL");
                    //obj.rol = UtilitarioSQL.ObtieneInt(reader, "IDrol");
                    obj.Descripcion = UtilitarioSQL.ObtieneString(reader, "DESCRIPCION");
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
        public void MantRol(RolEntidad rol)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{rol.Esquema}.PA_MAN_ROL";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", rol.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", rol.Opcion);
                cmd.Parameters.AddWithValue("@P_ID", rol.Id);
                cmd.Parameters.AddWithValue("@P_DESCRIPCION", rol.Descripcion);
                cmd.Parameters.AddWithValue("@P_ESQUEMA", rol.Esquema);

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
