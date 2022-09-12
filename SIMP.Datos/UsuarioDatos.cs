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
        public List<UsuarioEntidad> GetUsuarios(UsuarioEntidad usuario)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{usuario.Esquema}.PA_CON_USUARIO";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", usuario.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", usuario.Opcion);
                cmd.Parameters.AddWithValue("@P_ID", usuario.Id);
                cmd.Parameters.AddWithValue("@P_IDROL", usuario.Rol);
                cmd.Parameters.AddWithValue("@P_IDESTADO", usuario.Estado);
                cmd.Parameters.AddWithValue("@P_NOMBRE", usuario.Nombre);
                cmd.Parameters.AddWithValue("@P_PRIMER_APELLIDO", usuario.Primer_Apellido);
                cmd.Parameters.AddWithValue("@P_SEGUNDO_APELLIDO", usuario.Segundo_Apellido);
                cmd.Parameters.AddWithValue("@P_ESQUEMA", usuario.Esquema);

                myConexion.Open();
                reader = cmd.ExecuteReader();

                List<UsuarioEntidad> lista = new List<UsuarioEntidad>();

                while (reader.Read())
                {
                    UsuarioEntidad obj = new UsuarioEntidad();
                    obj.Id = UtilitarioSQL.ObtieneInt(reader, "ID");
                    obj.Rol = UtilitarioSQL.ObtieneInt(reader, "IDROL");
                    obj.Estado = UtilitarioSQL.ObtieneInt(reader, "IDESTADO");
                    obj.Nombre = UtilitarioSQL.ObtieneString(reader, "NOMBRE");
                    obj.Primer_Apellido = UtilitarioSQL.ObtieneString(reader, "PRIMER_APELLIDO");
                    obj.Segundo_Apellido = UtilitarioSQL.ObtieneString(reader, "SEGUNDO_APELLIDO");
                    obj.Usuario = UtilitarioSQL.ObtieneString(reader, "USUARIO");
                    obj.Contrasena = UtilitarioSQL.ObtieneString(reader, "CONTRASENA");
                    obj.Usuario1 = UtilitarioSQL.ObtieneString(reader, "USUARIO");
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
        public void MantUsuario(UsuarioEntidad usuario)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{usuario.Esquema}.PA_MAN_USUARIO";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", usuario.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", usuario.Opcion);
                cmd.Parameters.AddWithValue("@P_ID", usuario.Id);
                cmd.Parameters.AddWithValue("@P_IDROL", usuario.Rol);
                cmd.Parameters.AddWithValue("@P_IDESTADO", usuario.Estado);
                cmd.Parameters.AddWithValue("@P_NOMBRE", usuario.Nombre);
                cmd.Parameters.AddWithValue("@P_PRIMER_APELLIDO", usuario.Primer_Apellido);
                cmd.Parameters.AddWithValue("@P_SEGUNDO_APELLIDO", usuario.Segundo_Apellido);
                cmd.Parameters.AddWithValue("@P_ESQUEMA", usuario.Esquema);

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
