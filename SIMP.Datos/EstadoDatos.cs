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
    public class EstadoDatos
    {
        public List<EstadoEntidad> GetEstados(EstadoEntidad estado)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{estado.Esquema}.PA_CON_ESTADO";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_estado", estado.Estado);
                cmd.Parameters.AddWithValue("@P_OPCION", estado.Opcion);
                cmd.Parameters.AddWithValue("@P_ID", estado.Id);
                //cmd.Parameters.AddWithValue("@P_IDROL", estado.Rol);
                //cmd.Parameters.AddWithValue("@P_IDESTADO", estado.Estado);
                //cmd.Parameters.AddWithValue("@P_NOMBRE", estado.Nombre);
                //cmd.Parameters.AddWithValue("@P_PRIMER_APELLIDO", estado.Primer_Apellido);
                //cmd.Parameters.AddWithValue("@P_SEGUNDO_APELLIDO", estado.Segundo_Apellido);
                cmd.Parameters.AddWithValue("@P_ESQUEMA", estado.Esquema);

                myConexion.Open();
                reader = cmd.ExecuteReader();

                List<EstadoEntidad> lista = new List<EstadoEntidad>();

                while (reader.Read())
                {
                    EstadoEntidad obj = new EstadoEntidad();
                    obj.Id = UtilitarioSQL.ObtieneInt(reader, "PK_TBL_SIMP_SEG_estado");
                    //obj.Rol = UtilitarioSQL.ObtieneInt(reader, "FK_TBL_SIMP_ROL");
                    //obj.Estado = UtilitarioSQL.ObtieneInt(reader, "FK_TBL_SIMP_ESTADO");
                    //obj.Nombre = UtilitarioSQL.ObtieneString(reader, "NOMBRE");
                    //obj.Primer_Apellido = UtilitarioSQL.ObtieneString(reader, "PRIMER_APELLIDO");
                    //obj.Segundo_Apellido = UtilitarioSQL.ObtieneString(reader, "SEGUNDO_APELLIDO");
                    //obj.estado = UtilitarioSQL.ObtieneString(reader, "estado");
                    //obj.Contrasena = UtilitarioSQL.ObtieneString(reader, "CONTRASENA");
                    //obj.estado1 = UtilitarioSQL.ObtieneString(reader, "estado");
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
        public void Mantestado(EstadoEntidad estado)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{estado.Esquema}.PA_MAN_estado";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_estado", estado.Estado);
                cmd.Parameters.AddWithValue("@P_OPCION", estado.Opcion);
                cmd.Parameters.AddWithValue("@P_ID", estado.Id);
                //cmd.Parameters.AddWithValue("@P_IDROL", estado.Rol);
                //cmd.Parameters.AddWithValue("@P_IDESTADO", estado.Estado);
                //cmd.Parameters.AddWithValue("@P_NOMBRE", estado.Nombre);
                //cmd.Parameters.AddWithValue("@P_PRIMER_APELLIDO", estado.Primer_Apellido);
                //cmd.Parameters.AddWithValue("@P_SEGUNDO_APELLIDO", estado.Segundo_Apellido);
                //cmd.Parameters.AddWithValue("@P_estado1", estado.estado1);
                //cmd.Parameters.AddWithValue("@P_CONTRASENA", estado.Contrasena);
                cmd.Parameters.AddWithValue("@P_ESQUEMA", estado.Esquema);

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
