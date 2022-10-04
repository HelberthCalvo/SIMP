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
    public class MenuDatos
    {
        public List<MenuEntidad> ObtenerMenu(MenuEntidad menu)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = menu.Esquema + ".PA_CON_TBL_FLOW_MENU";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", menu.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", menu.Opcion);
                cmd.Parameters.AddWithValue("@P_PK_TBL_SIMP_SEG_MENU", menu.Id);
                cmd.Parameters.AddWithValue("@P_FK_TBL_FLOW_PERFIL", menu.IdPerfil);
                cmd.Parameters.AddWithValue("@P_ESTADO_MENU", menu.EstadoMenu);
                cmd.Parameters.AddWithValue("@P_ESTADO_PERMISO", menu.EstadoPermiso);
                cmd.Parameters.AddWithValue("@P_ESQUEMA", menu.Esquema);

                myConexion.Open();
                reader = cmd.ExecuteReader();

                List<MenuEntidad> lista = new List<MenuEntidad>();

                while (reader.Read())
                {
                    MenuEntidad obj = new MenuEntidad();
                    obj.Id = UtilitarioSQL.ObtieneInt(reader, "PK_TBL_SIMP_SEG_MENU");
                    obj.Codigo_Padre = UtilitarioSQL.ObtieneString(reader, "CODIGO_PADRE");
                    obj.Descripcion = UtilitarioSQL.ObtieneString(reader, "DESCRIPCION");
                    obj.Icono = UtilitarioSQL.ObtieneString(reader, "ICONO");
                    obj.Url = UtilitarioSQL.ObtieneString(reader, "URL");
                    obj.EstadoMenu = UtilitarioSQL.ObtieneString(reader, "ESTADO_MENU");
                    obj.CrearMenu = UtilitarioSQL.ObtieneBool(reader, "CREAR_MENU");
                    obj.EditarMenu = UtilitarioSQL.ObtieneBool(reader, "EDITAR_MENU");
                    obj.VerMenu = UtilitarioSQL.ObtieneBool(reader, "VER_MENU");
                    obj.EnviarMenu = UtilitarioSQL.ObtieneBool(reader, "ENVIAR_MENU");
                    obj.EstadoPermiso = UtilitarioSQL.ObtieneString(reader, "ESTADO_PERMISO");
                    obj.CrearPermiso = UtilitarioSQL.ObtieneBool(reader, "CREAR_PERMISO");
                    obj.EditarPermiso = UtilitarioSQL.ObtieneBool(reader, "EDITAR_PERMISO");
                    obj.VerPermiso = UtilitarioSQL.ObtieneBool(reader, "VER_PERMISO");
                    obj.EnviarPermiso = UtilitarioSQL.ObtieneBool(reader, "ENVIAR_PERMISO");

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
    }
}
