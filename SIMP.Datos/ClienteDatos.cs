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
    public static class ClienteDatos
    {
        public static List<ClienteEntidad> GetClientes(ClienteEntidad cliente)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{cliente.Esquema}.PA_CON_CLIENTE";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", cliente.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", cliente.Opcion);

                cmd.Parameters.AddWithValue("@P_ID", cliente.Id);
                cmd.Parameters.AddWithValue("@P_NOMBRE", cliente.Nombre);
                cmd.Parameters.AddWithValue("@P_PRIMER_APELLIDO", cliente.Primer_Apellido);
                cmd.Parameters.AddWithValue("@P_SEGUNDO_APELLIDO", cliente.Segundo_Apellido);
                cmd.Parameters.AddWithValue("@P_CORREO_ELECTRONICO", cliente.Correo_Electronico);
                cmd.Parameters.AddWithValue("@P_TELEFONO", cliente.Telefono);

                cmd.Parameters.AddWithValue("@P_ESQUEMA", cliente.Esquema);

                myConexion.Open();
                reader = cmd.ExecuteReader();

                List<ClienteEntidad> lista = new List<ClienteEntidad>();

                while (reader.Read())
                {
                    ClienteEntidad obj = new ClienteEntidad();
                    obj.Id = UtilitarioSQL.ObtieneInt(reader, "PK_TBL_SIMP_CLIENTE");
                    //obj.Estado = UtilitarioSQL.ObtieneInt(reader, "IDESTADO");
                    obj.Nombre = UtilitarioSQL.ObtieneString(reader, "NOMBRE");
                    obj.Primer_Apellido = UtilitarioSQL.ObtieneString(reader, "PRIMER_APELLIDO");
                    obj.Segundo_Apellido = UtilitarioSQL.ObtieneString(reader, "SEGUNDO_APELLIDO");
                    obj.Correo_Electronico = UtilitarioSQL.ObtieneString(reader, "CORREO_ELECTRONICO");
                    obj.Telefono = UtilitarioSQL.ObtieneString(reader, "TELEFONO");
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
        public static void MantCliente(ClienteEntidad cliente)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{cliente.Esquema}.PA_MAN_CLIENTE";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", cliente.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", cliente.Opcion);

                cmd.Parameters.AddWithValue("@P_ID", cliente.Id);
                cmd.Parameters.AddWithValue("@P_NOMBRE", cliente.Nombre);
                cmd.Parameters.AddWithValue("@P_PRIMER_APELLIDO", cliente.Primer_Apellido);
                cmd.Parameters.AddWithValue("@P_SEGUNDO_APELLIDO", cliente.Segundo_Apellido);
                cmd.Parameters.AddWithValue("@P_CORREO_ELECTRONICO", cliente.Correo_Electronico);
                cmd.Parameters.AddWithValue("@P_TELEFONO", cliente.Telefono);

                cmd.Parameters.AddWithValue("@P_ESQUEMA", cliente.Esquema);

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

