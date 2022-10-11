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
    public class PerfilDatos
    {
        public List<PerfilEntidad> GetPerfiles(PerfilEntidad perfil)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{perfil.Esquema}.PA_CON_PERFIL";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", perfil.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", perfil.Opcion);

                cmd.Parameters.AddWithValue("@P_ID", perfil.Id);
                cmd.Parameters.AddWithValue("@P_DESCRIPCION", perfil.Descripcion);


                cmd.Parameters.AddWithValue("@P_ESQUEMA", perfil.Esquema);

                myConexion.Open();
                reader = cmd.ExecuteReader();

                List<PerfilEntidad> lista = new List<PerfilEntidad>();

                while (reader.Read())
                {
                    PerfilEntidad obj = new PerfilEntidad();
                    obj.Id = UtilitarioSQL.ObtieneInt(reader, "PK_TBL_SIMP_SEG_PERFIL");
                    //obj.perfil = UtilitarioSQL.ObtieneInt(reader, "IDperfil");
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
        public void MantPerfil(PerfilEntidad perfil)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{perfil.Esquema}.PA_MAN_PERFIL";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", perfil.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", perfil.Opcion);
                cmd.Parameters.AddWithValue("@P_ID", perfil.Id);
                cmd.Parameters.AddWithValue("@P_DESCRIPCION", perfil.Descripcion);
                cmd.Parameters.AddWithValue("@P_ESTADO", perfil.Estado);
                cmd.Parameters.AddWithValue("@P_ESQUEMA", perfil.Esquema);

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
