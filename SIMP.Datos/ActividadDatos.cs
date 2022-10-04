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
    public class ActividadDatos
    {
        public static List<ActividadEntidad> GetActividades(ActividadEntidad actividad)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{actividad.Esquema}.PA_CON_ACTIVIDAD";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", actividad.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", actividad.Opcion);

                cmd.Parameters.AddWithValue("@P_ID", actividad.Id);
                cmd.Parameters.AddWithValue("@P_IDFASE", actividad.IdFase);
                cmd.Parameters.AddWithValue("@P_IDUSUARIO", actividad.IdUsuario);
                cmd.Parameters.AddWithValue("@P_IDESTADO", actividad.IdEstado);
                cmd.Parameters.AddWithValue("@P_DESCRIPCION", actividad.Descripcion);
                cmd.Parameters.AddWithValue("@P_FECHA_INICIO", actividad.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@P_FECHA_ESTIMADA", actividad.Fecha_Estimada);
                cmd.Parameters.AddWithValue("@P_FECHA_FINALIZACION", actividad.Fecha_Finalizacion);

                cmd.Parameters.AddWithValue("@P_ESQUEMA", actividad.Esquema);

                myConexion.Open();
                reader = cmd.ExecuteReader();

                List<ActividadEntidad> lista = new List<ActividadEntidad>();

                while (reader.Read())
                {
                    ActividadEntidad obj = new ActividadEntidad();
                    obj.Id = UtilitarioSQL.ObtieneInt(reader, "PK_TBL_SIMP_PY_ACTIVIDAD");
                    obj.IdFase = UtilitarioSQL.ObtieneInt(reader, "FK_TBL_SIMP_PY_FASE");
                    obj.IdUsuario = UtilitarioSQL.ObtieneInt(reader, "FK_TBL_SIMP_USUARIO");
                    obj.IdEstado = UtilitarioSQL.ObtieneInt(reader, "FK_TBL_SIMP_ESTADO");
                    obj.Descripcion = UtilitarioSQL.ObtieneString(reader, "DESCRIPCION");
                    obj.Fecha_Inicio = UtilitarioSQL.ObtieneString(reader, "FECHA_INICIO");
                    obj.Fecha_Estimada = UtilitarioSQL.ObtieneString(reader, "FECHA_ESTIMADA");
                    obj.Fecha_Finalizacion = UtilitarioSQL.ObtieneString(reader, "FECHA_FINALIZACION");
                    obj.NombreFase = UtilitarioSQL.ObtieneString(reader, "NOMBRE_FASE");
                    obj.NombreUsuario = UtilitarioSQL.ObtieneString(reader, "NOMBRE_USUARIO");
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
        public static void MantActividad(ActividadEntidad actividad)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{actividad.Esquema}.PA_MAN_ACTIVIDAD";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", actividad.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", actividad.Opcion);

                cmd.Parameters.AddWithValue("@P_ID", actividad.Id);
                cmd.Parameters.AddWithValue("@P_IDFASE", actividad.IdFase);
                cmd.Parameters.AddWithValue("@P_IDUSUARIO", actividad.IdUsuario);
                cmd.Parameters.AddWithValue("@P_IDESTADO", actividad.IdEstado);
                cmd.Parameters.AddWithValue("@P_DESCRIPCION", actividad.Descripcion);
                cmd.Parameters.AddWithValue("@P_FECHA_INICIO", actividad.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@P_FECHA_ESTIMADA", actividad.Fecha_Estimada);
                cmd.Parameters.AddWithValue("@P_FECHA_FINALIZACION", actividad.Fecha_Finalizacion);

                cmd.Parameters.AddWithValue("@P_ESQUEMA", actividad.Esquema);

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
