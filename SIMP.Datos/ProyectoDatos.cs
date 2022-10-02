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
    public class ProyectoDatos
    {
        public static List<ProyectoEntidad> GetProyectos(ProyectoEntidad proyecto)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{proyecto.Esquema}.PA_CON_PROYECTO";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", proyecto.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", proyecto.Opcion);

                cmd.Parameters.AddWithValue("@P_ID", proyecto.Id);
                cmd.Parameters.AddWithValue("@P_IDCLIENTE", proyecto.IdCliente);
                cmd.Parameters.AddWithValue("@P_IDESTADO", proyecto.IdEstado);
                cmd.Parameters.AddWithValue("@P_NOMBRE", proyecto.Nombre);
                cmd.Parameters.AddWithValue("@P_DESCRIPCION", proyecto.Descripcion);
                cmd.Parameters.AddWithValue("@P_FECHA_INICIO", proyecto.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@P_FECHA_ESTIMADA", proyecto.Fecha_Estimada);
                cmd.Parameters.AddWithValue("@P_FECHA_FINALIZACION", proyecto.Fecha_Finalizacion);

                cmd.Parameters.AddWithValue("@P_ESQUEMA", proyecto.Esquema);

                myConexion.Open();
                reader = cmd.ExecuteReader();

                List<ProyectoEntidad> lista = new List<ProyectoEntidad>();

                while (reader.Read())
                {
                    ProyectoEntidad obj = new ProyectoEntidad();
                    obj.Id = UtilitarioSQL.ObtieneInt(reader, "PK_TBL_SIMP_PY_PROYECTO");
                    obj.IdCliente = UtilitarioSQL.ObtieneInt(reader, "FK_TBL_SIMP_CLIENTE");
                    obj.IdEstado = UtilitarioSQL.ObtieneInt(reader, "FK_TBL_SIMP_ESTADO");
                    obj.Nombre = UtilitarioSQL.ObtieneString(reader, "NOMBRE");
                    obj.Nombre_Cliente = UtilitarioSQL.ObtieneString(reader, "NOMBRE_CLIENTE");
                    obj.Descripcion = UtilitarioSQL.ObtieneString(reader, "DESCRIPCION");
                    obj.Fecha_Inicio = UtilitarioSQL.ObtieneString(reader, "FECHA_INICIO");
                    obj.Fecha_Estimada = UtilitarioSQL.ObtieneString(reader, "FECHA_ESTIMADA");
                    obj.Fecha_Finalizacion = UtilitarioSQL.ObtieneString(reader, "FECHA_FINALIZACION");
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
        public static void MantProyecto(ProyectoEntidad proyecto)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{proyecto.Esquema}.PA_MAN_PROYECTO";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", proyecto.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", proyecto.Opcion);

                cmd.Parameters.AddWithValue("@P_ID", proyecto.Id);
                cmd.Parameters.AddWithValue("@P_IDCLIENTE", proyecto.IdCliente);
                cmd.Parameters.AddWithValue("@P_IDESTADO", proyecto.IdEstado);
                cmd.Parameters.AddWithValue("@P_NOMBRE", proyecto.Nombre);
                cmd.Parameters.AddWithValue("@P_DESCRIPCION", proyecto.Descripcion);
                cmd.Parameters.AddWithValue("@P_FECHA_INICIO", proyecto.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@P_FECHA_ESTIMADA", proyecto.Fecha_Estimada);
                cmd.Parameters.AddWithValue("@P_FECHA_FINALIZACION", proyecto.Fecha_Finalizacion);

                cmd.Parameters.AddWithValue("@P_ESQUEMA", proyecto.Esquema);

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
