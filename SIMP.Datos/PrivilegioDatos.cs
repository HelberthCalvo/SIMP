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
    public class PrivilegioDatos
    {
        public string InsertarPermisos(PrivilegioEntidad privilegios)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = privilegios.Esquema + ".PA_MAN_TBL_SIMP_PERMISOS";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_OPCION ", privilegios.Opcion);
                cmd.Parameters.AddWithValue("@P_MODO_EJECUCION", 0);
                cmd.Parameters.AddWithValue("@P_ESQUEMA", privilegios.Esquema);
                cmd.Parameters.AddWithValue("@P_USUARIO", privilegios.Usuario);
                cmd.Parameters.AddWithValue("@P_IDENTIFICADOR_EXTERNO", 0);
                cmd.Parameters.AddWithValue("@P_PK_TBL_SIMP_SEG_PERMISOS", privilegios.Id);
                cmd.Parameters.AddWithValue("@P_FK_TBL_SIMP_SEG_PERFIL", privilegios.IdPerfil);
                cmd.Parameters.AddWithValue("@P_FK_TBL_SIMP_SEG_MENU", privilegios.IdMenu);
                cmd.Parameters.AddWithValue("@P_DESCRIPCION", privilegios.Descripcion);
                cmd.Parameters.AddWithValue("@P_ESTADO", privilegios.EstadoPermiso);
                cmd.Parameters.AddWithValue("@P_LISTA_MENU", privilegios.ListaMenu);
                cmd.Parameters.AddWithValue("@P_LISTA_PERMISOS", privilegios.ListaPermisos);

                myConexion.Open();
                reader = cmd.ExecuteReader();

                reader.Dispose();
                cmd.Dispose();
                myConexion.Close();
                myConexion.Dispose();

                return "Datos Guardados Correctamente";
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en Base de Datos al ingresar los permisos" + ex.Message);
            }
        }
    }
}
