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
    public class CatConfiguracionDatos
    {
        public List<CatConfiguracionEntidad> ObtieneCatConfiguracion(CatConfiguracionEntidad CatConfiguracion)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = CatConfiguracion.Esquema + ".PA_CON_TBL_SIMP_CAT_CONFIGURACION";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", CatConfiguracion.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", CatConfiguracion.Opcion);
                cmd.Parameters.AddWithValue("@P_PK_TBL_SIMP_CAT_CONFIGURACION", CatConfiguracion.ID);
                cmd.Parameters.AddWithValue("@P_LLAVE01", CatConfiguracion.Llave01);
                cmd.Parameters.AddWithValue("@P_LLAVE02", CatConfiguracion.Llave02);
                cmd.Parameters.AddWithValue("@P_LLAVE03", CatConfiguracion.Llave03);
                cmd.Parameters.AddWithValue("@P_LLAVE04", CatConfiguracion.Llave04);
                cmd.Parameters.AddWithValue("@P_LLAVE05", CatConfiguracion.Llave05);
                cmd.Parameters.AddWithValue("@P_LLAVE06", CatConfiguracion.Llave06);
                cmd.Parameters.AddWithValue("@P_ESQUEMA", CatConfiguracion.Esquema);

                myConexion.Open();
                reader = cmd.ExecuteReader();

                List<CatConfiguracionEntidad> lista = new List<CatConfiguracionEntidad>();

                while (reader.Read())
                {
                    CatConfiguracionEntidad obj = new CatConfiguracionEntidad();
                    obj.ID = UtilitarioSQL.ObtieneInt(reader, "PK_TBL_SIMP_CAT_CONFIGURACION");
                    obj.Descripcion = UtilitarioSQL.ObtieneString(reader, "DESCRIPCION");
                    obj.Llave01 = UtilitarioSQL.ObtieneString(reader, "LLAVE01");
                    obj.Llave02 = UtilitarioSQL.ObtieneString(reader, "LLAVE02");
                    obj.Llave03 = UtilitarioSQL.ObtieneString(reader, "LLAVE03");
                    obj.Llave04 = UtilitarioSQL.ObtieneString(reader, "LLAVE04");
                    obj.Llave05 = UtilitarioSQL.ObtieneString(reader, "LLAVE05");
                    obj.Llave06 = UtilitarioSQL.ObtieneString(reader, "LLAVE06");
                    obj.Valor = UtilitarioSQL.ObtieneString(reader, "VALOR");

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
                throw new ApplicationException("Error en Base de Datos " + ex.Message);
            }
        }

        public void Mantenimiento(CatConfiguracionEntidad CatConfiguracion)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{CatConfiguracion.Esquema}.PA_MAN_TBL_SIMP_CAT_CONFIGURACION";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", CatConfiguracion.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", CatConfiguracion.Opcion);
                cmd.Parameters.AddWithValue("@P_PK_TBL_SIMP_CAT_CONFIGURACION", CatConfiguracion.ID);
                cmd.Parameters.AddWithValue("@P_DESCRIPCION", CatConfiguracion.Descripcion);
                cmd.Parameters.AddWithValue("@P_VALOR", CatConfiguracion.Valor);
                cmd.Parameters.AddWithValue("@P_ESQUEMA", CatConfiguracion.Esquema);

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
