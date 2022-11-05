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
    public class ConfiguracionDatos
    {
        public List<ConfiguracionEntidad> GetConfiguracion(string usuario, long pkConfiguracionEntidad, string estado, string esquema, int opcion = 0)
        {
            using (SqlConnection conexionSql = new SqlConnection(Conexion.CadenaDeConexion()))
            {
                conexionSql.Open();
                using (SqlCommand comandoSql = new SqlCommand($"{Conexion.ESQUEMA_PADRE}.PA_CON_TBL_SIMP_ConfiguracionEntidad", conexionSql))
                {
                    comandoSql.CommandType = System.Data.CommandType.StoredProcedure;
                    comandoSql.Parameters.AddWithValue("@P_OPCION", opcion);
                    comandoSql.Parameters.AddWithValue("@P_USUARIO", usuario);
                    comandoSql.Parameters.AddWithValue("@P_PK_TBL_SIMP_ConfiguracionEntidad", pkConfiguracionEntidad);
                    comandoSql.Parameters.AddWithValue("@P_ESTADO", estado);
                    comandoSql.Parameters.AddWithValue("@P_ESQUEMA", esquema);

                    using (SqlDataReader _readerSql = comandoSql.ExecuteReader())
                    {
                        List<ConfiguracionEntidad> _listaDeConfiguracionEntidades = new List<ConfiguracionEntidad>();

                        while (_readerSql.Read())
                        {
                            ConfiguracionEntidad ConfiguracionEntidad = new ConfiguracionEntidad();
                            ConfiguracionEntidad.PK_CONFIGURACION = Convert.ToInt64(_readerSql["PK_TBL_SIMP_ConfiguracionEntidad"].ToString());
                            ConfiguracionEntidad.DESCRIPCION = _readerSql["DESCRIPCION"].ToString();
                            ConfiguracionEntidad.NOMBRE_PAGINA = _readerSql["NOMBRE_PAGINA"].ToString();
                            ConfiguracionEntidad.ESTADO = _readerSql["ESTADO"].ToString();

                            _listaDeConfiguracionEntidades.Add(ConfiguracionEntidad);
                        }

                        return _listaDeConfiguracionEntidades;
                    }
                }

            }
        }

        public void MantConfiguracionEstado(ConfiguracionEntidad config)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{config.ESQUEMA}.PA_MAN_TBL_SIMP_ConfiguracionEntidad";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", config.USUARIO_MODIFICACION);
                cmd.Parameters.AddWithValue("@P_OPCION", 1);
                cmd.Parameters.AddWithValue("@P_PK_TBL_SIMP_ConfiguracionEntidad", config.PK_CONFIGURACION);
                cmd.Parameters.AddWithValue("@P_ESTADO", config.ESTADO);
                cmd.Parameters.AddWithValue("@P_ESQUEMA", config.ESQUEMA);

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
