using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using ZonaFranca.Entidades;
using ZonaFranca.Logica;

namespace ZonaFRanca.Datos
{
    public class ProveedorDatos
    {
        public static List<Proveedores> SeleccionarProveedores(Proveedores proveedor)
        {

            OdbcConnection conn = new OdbcConnection(Conexion.StringConexionBD(VariablesGlobales.BD));
            try
            {
                conn.ConnectionTimeout = 0;
                conn.Open();
                OdbcCommand command = new OdbcCommand();
                string Sql = "{call SITSA.spSITSASeleccionarProveedores(?,?)}";

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Sql;
                command.CommandTimeout = 0;
                command.Connection = conn;
                command.Parameters.Add("@P_Compannia", OdbcType.VarChar);
                command.Parameters["@P_Compannia"].Value = VariablesGlobales.Compannia;
                command.Parameters.Add("@P_Proveedor", OdbcType.VarChar);
                command.Parameters["@P_Proveedor"].Value = '%' + proveedor.Codigo + '%';

                OdbcDataReader reader = command.ExecuteReader();
                List<Proveedores> Proveedor = new List<Proveedores>();

                while (reader.Read())
                {
                    string Num = reader["Num"].ToString();
                    string Codigo = reader["Proveedor"].ToString();
                    string Nombre = reader["Nombre"].ToString();

                    Proveedores a = new Proveedores();
                    a.Codigo = Codigo;
                    a.Descripcion = Nombre;
                    a.Num = Num;
                    Proveedor.Add(a);
                }
                return Proveedor;
            }
            catch (OdbcException ax)
            {
                throw new ApplicationException("Error en Base de Datos..! \n" + ax.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ERROR AL OBTENER LOS PROVEEDORES. DETALLE: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Proveedores> SeleccionarProveedores2(Proveedores proveedor)
        {

            OdbcConnection conn = new OdbcConnection(Conexion.StringConexionBD(VariablesGlobales.BD));
            try
            {
                conn.ConnectionTimeout = 0;
                conn.Open();
                OdbcCommand command = new OdbcCommand();
                string Sql = "{call SITSA.spSITSASeleccionarProveedores2(?,?)}";

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Sql;
                command.CommandTimeout = 0;
                command.Connection = conn;
                command.Parameters.Add("vProveedor", OdbcType.VarChar);
                command.Parameters["vProveedor"].Value = '%' + proveedor.Codigo + '%';
                command.Parameters.Add("vDescripcion", OdbcType.VarChar);
                command.Parameters["vDescripcion"].Value = '%' + proveedor.Descripcion + '%';

                OdbcDataReader reader = command.ExecuteReader();
                List<Proveedores> Proveedor = new List<Proveedores>();

                while (reader.Read())
                {
                    string Num = reader["Num"].ToString();
                    string Codigo = reader["Proveedor"].ToString();
                    string Nombre = reader["Nombre"].ToString();

                    Proveedores a = new Proveedores();
                    a.Codigo = Codigo;
                    a.Descripcion = Nombre;
                    a.Num = Num;
                    Proveedor.Add(a);
                }
                return Proveedor;
            }
            catch (OdbcException ax)
            {
                throw new ApplicationException("Error en Base de Datos..! \n" + ax.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ERROR AL OBTENER LOS PROVEEDORES. DETALLE: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public static List<Proveedores> SeleccionarProveedores1(Proveedores proveedor)
        {

            OdbcConnection conn = new OdbcConnection(Conexion.StringConexionBD(VariablesGlobales.BD));
            try
            {
                conn.ConnectionTimeout = 0;
                conn.Open();
                OdbcCommand command = new OdbcCommand();
                string Sql = "{call SITSA.spSITSASeleccionarProveedores1(?)}";

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Sql;
                command.CommandTimeout = 0;
                command.Connection = conn;
                command.Parameters.Add("vProveedor", OdbcType.VarChar);
                command.Parameters["vProveedor"].Value = proveedor.Codigo;

                OdbcDataReader reader = command.ExecuteReader();
                List<Proveedores> Proveedor = new List<Proveedores>();

                while (reader.Read())
                {
                    string Num = reader["Num"].ToString();
                    string Codigo = reader["Proveedor"].ToString();
                    string Nombre = reader["Nombre"].ToString();

                    Proveedores a = new Proveedores();
                    a.Codigo = Codigo;
                    a.Descripcion = Nombre;
                    a.Num = Num;
                    Proveedor.Add(a);
                }
                return Proveedor;
            }
            catch (OdbcException ax)
            {
                throw new ApplicationException("Error en Base de Datos..! \n" + ax.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ERROR AL OBTENER LOS PROVEEDORES. DETALLE: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public static List<Proveedores> SeleccionarProveedoresDepartamento(Proveedores proveedor)
        {

            OdbcConnection conn = new OdbcConnection(Conexion.StringConexionBD(VariablesGlobales.BD));
            try
            {
                conn.ConnectionTimeout = 0;
                conn.Open();
                OdbcCommand command = new OdbcCommand();
                string Sql = "{call SITSA.spSelectDepartamentoProveedor(?)}";

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Sql;
                command.CommandTimeout = 0;
                command.Connection = conn;
                command.Parameters.Add("vDepartamento", OdbcType.VarChar);
                command.Parameters["vDepartamento"].Value = '%' + proveedor.Departamento + '%';

                OdbcDataReader reader = command.ExecuteReader();
                List<Proveedores> Proveedor = new List<Proveedores>();

                while (reader.Read())
                {

                    string Departamento = reader["Departamento"].ToString();
                    string Proveedor1 = reader["Proveedor"].ToString();
                    string nombre = reader["nombre"].ToString();
                    Proveedores a = new Proveedores();
                    a.Departamento = Departamento;
                    a.Codigo = Proveedor1;
                    a.Descripcion = nombre;
                    Proveedor.Add(a);
                }
                return Proveedor;
            }
            catch (OdbcException ax)
            {
                throw new ApplicationException("Error en Base de Datos..! \n" + ax.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ERROR AL OBTENER LOS PROVEEDORES. DETALLE: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Proveedores> SeleccionarProveedoresDepartamento1(Proveedores proveedor)
        {

            OdbcConnection conn = new OdbcConnection(Conexion.StringConexionBD(VariablesGlobales.BD));
            try
            {
                conn.ConnectionTimeout = 0;
                conn.Open();
                OdbcCommand command = new OdbcCommand();
                string Sql = "{call SITSA.spSelectDepartamentoProveedor1(?,?,?)}";

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Sql;
                command.CommandTimeout = 0;
                command.Connection = conn;
                command.Parameters.Add("vDepartamento", OdbcType.VarChar);
                command.Parameters["vDepartamento"].Value = '%' + proveedor.Departamento + '%';
                command.Parameters.Add("vProveedor", OdbcType.VarChar);
                command.Parameters["vProveedor"].Value = '%' + proveedor.Codigo + '%';
                command.Parameters.Add("vNombre", OdbcType.VarChar);
                command.Parameters["vNombre"].Value = '%' + proveedor.Nombre + '%';


                OdbcDataReader reader = command.ExecuteReader();
                List<Proveedores> Proveedor = new List<Proveedores>();

                while (reader.Read())
                {

                    string Departamento = reader["Departamento"].ToString();
                    string Proveedor1 = reader["Proveedor"].ToString();
                    string des = reader["Nombre"].ToString();

                    Proveedores a = new Proveedores();
                    a.Departamento = Departamento;
                    a.Descripcion = des;
                    a.Codigo = Proveedor1;
                    Proveedor.Add(a);
                }
                return Proveedor;
            }
            catch (OdbcException ax)
            {
                throw new ApplicationException("Error en Base de Datos..! \n" + ax.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ERROR AL OBTENER LOS PROVEEDORES. DETALLE: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public static void InsertarDepartamentoProveedor(Proveedores proveedor)
        {
            OdbcConnection conn = new OdbcConnection(Conexion.StringConexionBD(VariablesGlobales.BD));
            try
            {
                conn.ConnectionTimeout = 0;
                conn.Open();
                OdbcCommand command = new OdbcCommand();
                string Sql = "{call SITSA.spInsertDepartamentoProveedor(?,?)}";

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Sql;
                command.Connection = conn;
                command.Parameters.Add("vDepartamento", OdbcType.VarChar);
                command.Parameters["vDepartamento"].Value = proveedor.Departamento;

                command.Parameters.Add("vProveedor", OdbcType.VarChar);
                command.Parameters["vProveedor"].Value = proveedor.Codigo;

                command.ExecuteNonQuery();
            }
            catch (OdbcException ax)
            {
                throw new ApplicationException("Error en Base de Datos..! \n" + ax.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ERROR AL INSERTAR DEPARTAMENTO-PROVEEDOR. DETALLE: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public static void EliminarDepartamentoProveedor(Proveedores proveedor)
        {
            OdbcConnection conn = new OdbcConnection(Conexion.StringConexionBD(VariablesGlobales.BD));
            try
            {
                conn.ConnectionTimeout = 0;
                conn.Open();
                OdbcCommand command = new OdbcCommand();
                string Sql = "{call SITSA.spEliminarDepartProveedor(?,?)}";

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Sql;
                command.Connection = conn;
                command.Parameters.Add("vDepartamento", OdbcType.VarChar);
                command.Parameters["vDepartamento"].Value = proveedor.Departamento;

                command.Parameters.Add("vProveedor", OdbcType.VarChar);
                command.Parameters["vProveedor"].Value = proveedor.Codigo;

                command.ExecuteNonQuery();
            }
            catch (OdbcException ax)
            {
                throw new ApplicationException("Error en Base de Datos..! \n" + ax.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ERROR AL INSERTAR DEPARTAMENTO-PROVEEDOR. DETALLE: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }


        public static List<Proveedores> SeleccionarProveedoresArticulo(Proveedores proveedor)
        {

            OdbcConnection conn = new OdbcConnection(Conexion.StringConexionBD(VariablesGlobales.BD));
            try
            {
                conn.ConnectionTimeout = 0;
                conn.Open();
                OdbcCommand command = new OdbcCommand();
                string Sql = "{call SITSA.spSelectProveedorArt(?,?)}";

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Sql;
                command.CommandTimeout = 0;
                command.Connection = conn;
                command.Parameters.Add("vDepartamento", OdbcType.VarChar);
                command.Parameters["vDepartamento"].Value = proveedor.Departamento;
                command.Parameters.Add("vProveedor", OdbcType.VarChar);
                command.Parameters["vProveedor"].Value = proveedor.Codigo;

                OdbcDataReader reader = command.ExecuteReader();
                List<Proveedores> Proveedor = new List<Proveedores>();

                while (reader.Read())
                {
                    string Num = reader["Num"].ToString();
                    string Departamento = reader["Departamento"].ToString();
                    string Proveedor1 = reader["Proveedor"].ToString();
                    string Articulo = reader["Articulo"].ToString();
                    string Descripcion = reader["Descripcion"].ToString();

                    Proveedores a = new Proveedores();
                    a.Num = Num;
                    a.Departamento = Departamento;
                    a.Codigo = Proveedor1;
                    a.Articulo = Articulo;
                    a.Descripcion = Descripcion;
                    Proveedor.Add(a);
                }
                return Proveedor;
            }
            catch (OdbcException ax)
            {
                throw new ApplicationException("Error en Base de Datos..! \n" + ax.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ERROR AL OBTENER LOS ARTICULOS. DETALLE: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void InsertarProveedorArticulo(Proveedores proveedor)
        {
            OdbcConnection conn = new OdbcConnection(Conexion.StringConexionBD(VariablesGlobales.BD));
            try
            {
                conn.ConnectionTimeout = 0;
                conn.Open();
                OdbcCommand command = new OdbcCommand();
                string Sql = "{call SITSA.spInsertProveedorArticulo(?,?,?)}";

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Sql;
                command.Connection = conn;
                command.Parameters.Add("vDepartamento", OdbcType.VarChar);
                command.Parameters["vDepartamento"].Value = proveedor.Departamento;

                command.Parameters.Add("vProveedor", OdbcType.VarChar);
                command.Parameters["vProveedor"].Value = proveedor.Codigo;

                command.Parameters.Add("vArticulo", OdbcType.VarChar);
                command.Parameters["vArticulo"].Value = proveedor.Articulo;

                command.ExecuteNonQuery();
            }
            catch (OdbcException ax)
            {
                throw new ApplicationException("Error en Base de Datos..! \n" + ax.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ERROR AL INSERTAR DEPARTAMENTO-PROVEEDOR. DETALLE: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public static void EliminarProveedorArticulo(Proveedores proveedor)
        {
            OdbcConnection conn = new OdbcConnection(Conexion.StringConexionBD(VariablesGlobales.BD));
            try
            {
                conn.ConnectionTimeout = 0;
                conn.Open();
                OdbcCommand command = new OdbcCommand();
                string Sql = "{call SITSA.spEliminarProveedorArt(?,?,?)}";

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Sql;
                command.Connection = conn;
                command.Parameters.Add("vDepartamento", OdbcType.VarChar);
                command.Parameters["vDepartamento"].Value = proveedor.Departamento;

                command.Parameters.Add("vProveedor", OdbcType.VarChar);
                command.Parameters["vProveedor"].Value = proveedor.Codigo;

                command.Parameters.Add("vArticulo", OdbcType.VarChar);
                command.Parameters["vArticulo"].Value = proveedor.Articulo;

                command.ExecuteNonQuery();
            }
            catch (OdbcException ax)
            {
                throw new ApplicationException("Error en Base de Datos..! \n" + ax.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ERROR AL INSERTAR PROVEEDOR-ARTICULO. DETALLE: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public static List<Proveedores> SeleccionarArticulos(Proveedores proveedor)
        {

            OdbcConnection conn = new OdbcConnection(Conexion.StringConexionBD(VariablesGlobales.BD));
            try
            {
                conn.ConnectionTimeout = 0;
                conn.Open();
                OdbcCommand command = new OdbcCommand();
                string Sql = "{call SITSA.spSelectArticulosP(?)}";

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Sql;
                command.CommandTimeout = 0;
                command.Connection = conn;
                command.Parameters.Add("vProveedor", OdbcType.VarChar);
                command.Parameters["vProveedor"].Value = proveedor.Codigo;

                OdbcDataReader reader = command.ExecuteReader();
                List<Proveedores> Proveedor = new List<Proveedores>();

                while (reader.Read())
                {
                    string Num = reader["Num"].ToString();
                    string Descripcion = reader["Descripcion"].ToString();
                    string Articulo = reader["Articulo"].ToString();

                    Proveedores a = new Proveedores();
                    a.Num = Num;
                    a.Descripcion = Descripcion;
                    a.Articulo = Articulo;
                    Proveedor.Add(a);
                }
                return Proveedor;
            }
            catch (OdbcException ax)
            {
                throw new ApplicationException("Error en Base de Datos..! \n" + ax.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ERROR AL OBTENER LOS ARTICULOS. DETALLE: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Proveedores> SeleccionarArticulos1(Proveedores proveedor)
        {

            OdbcConnection conn = new OdbcConnection(Conexion.StringConexionBD(VariablesGlobales.BD));
            try
            {
                conn.ConnectionTimeout = 0;
                conn.Open();
                OdbcCommand command = new OdbcCommand();
                string Sql = "{call SITSA.spSelectArticulosP1(?,?,?)}";

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Sql;
                command.CommandTimeout = 0;
                command.Connection = conn;
                command.Parameters.Add("vDepartamento", OdbcType.VarChar);
                command.Parameters["vDepartamento"].Value = proveedor.Departamento;

                command.Parameters.Add("vProveedor", OdbcType.VarChar);
                command.Parameters["vProveedor"].Value = "%" + proveedor.Codigo + "%";

                command.Parameters.Add("vArticulo", OdbcType.VarChar);
                command.Parameters["vArticulo"].Value = "%" + proveedor.Articulo + "%";



                OdbcDataReader reader = command.ExecuteReader();
                List<Proveedores> Proveedor = new List<Proveedores>();

                while (reader.Read())
                {
                    string Num = reader["Num"].ToString();
                    string Descripcion = reader["Descripcion"].ToString();
                    string Articulo = reader["Articulo"].ToString();
                    string Proveedor1 = reader["Proveedor"].ToString();
                    string Nombre = reader["Nombre"].ToString();


                    Proveedores a = new Proveedores();
                    a.Num = Num;
                    a.Descripcion = Descripcion;
                    a.Articulo = Articulo;
                    a.Codigo = Proveedor1;
                    a.DescDepartamento = Nombre;
                    Proveedor.Add(a);
                }
                return Proveedor;
            }
            catch (OdbcException ax)
            {
                throw new ApplicationException("Error en Base de Datos..! \n" + ax.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ERROR AL OBTENER LOS ARTICULOS. DETALLE: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Proveedores> SeleccionarDatosProveedorSP()
        {
            OdbcConnection conn = new OdbcConnection(Conexion.StringConexionBD(VariablesGlobales.BD));
            try
            {
                conn.Open();
                OdbcCommand command = new OdbcCommand();
                string Sql = "{call sitsa.spSelectDatosProveedor(?)}";

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Sql;
                command.Connection = conn;
                command.Parameters.Add("@P_Compannia", OdbcType.VarChar);
                command.Parameters["@P_Compannia"].Value = VariablesGlobales.Compannia;
                OdbcDataReader reader = command.ExecuteReader();
                List<Proveedores> lista = new List<Proveedores>();
                Proveedores proveedor = null;
                while (reader.Read())
                {
                    string id = reader["Proveedor"].ToString();
                    string codPago = reader["Condicion_pago"].ToString();
                    string moneda = reader["Moneda"].ToString();
                    string Pais = reader["Pais"].ToString();
                    string nombre = reader["Nombre"].ToString();
                    string local = reader["Local"].ToString();

                    proveedor = new Proveedores();

                    proveedor.Codigo = id;
                    proveedor.Nombre = nombre;
                    proveedor.CondicionPago = codPago;
                    proveedor.Pais = Pais;
                    proveedor.Moneda = moneda;
                    proveedor.Local = local;

                    lista.Add(proveedor);
                }
                return lista;
            }
            catch (OdbcException ax)
            {
                throw new ApplicationException("Error en Base de Datos..! \n" + ax.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ERROR AL OBTENER LA CONSULTA. DETALLE: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public static Proveedores SeleccionarDatosProveedorSPFiltro(string proveedor)
        {
            OdbcConnection conn = new OdbcConnection(Conexion.StringConexionBD(VariablesGlobales.BD));
            try
            {
                conn.Open();
                OdbcCommand command = new OdbcCommand();
                string Sql = "{call sitsa.SPSELECTDATOS_PAIS_MONEDA_PAGO_POR_PROVEEDOR(?,?)}";

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Sql;
                command.Connection = conn;
                command.Parameters.Add("@P_Proveedor", OdbcType.VarChar);
                command.Parameters["@P_Proveedor"].Value = proveedor;
                command.Parameters.Add("@P_Compannia", OdbcType.VarChar);
                command.Parameters["@P_Compannia"].Value = VariablesGlobales.Compannia;
                OdbcDataReader reader = command.ExecuteReader();
                Proveedores lista = null;

                while (reader.Read())
                {
                    string id = reader["Proveedor"].ToString();
                    string codPago = reader["Condicion_pago"].ToString();
                    string moneda = reader["Moneda"].ToString();
                    string Pais = reader["Pais"].ToString();
                    string nombre = reader["Nombre"].ToString();
                    string local = reader["Local"].ToString();

                    lista = new Proveedores();
                    lista.Codigo = id;
                    lista.Nombre = nombre;
                    lista.CondicionPago = codPago;
                    lista.Pais = Pais;
                    lista.Moneda = moneda;
                    lista.Local = local;

                }
                return lista;
            }
            catch (OdbcException ax)
            {
                throw new ApplicationException("Error en Base de Datos..! \n" + ax.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ERROR AL OBTENER LA CONSULTA. DETALLE: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public static List<Proveedores> SeleccionarProveedoresArticulo1(Proveedores proveedor)
        {

            OdbcConnection conn = new OdbcConnection(Conexion.StringConexionBD(VariablesGlobales.BD));
            try
            {
                conn.ConnectionTimeout = 0;
                conn.Open();
                OdbcCommand command = new OdbcCommand();
                string Sql = "{call SITSA.spSelectProveedorArticulo(?,?,?,?,?)}";

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Sql;
                command.CommandTimeout = 0;
                command.Connection = conn;
                command.Parameters.Add("@P_Departamento", OdbcType.VarChar);
                command.Parameters["@P_Departamento"].Value = proveedor.Departamento;
                command.Parameters.Add("@P_Proveedor", OdbcType.VarChar);
                command.Parameters["@P_Proveedor"].Value = proveedor.Codigo;
                command.Parameters.Add("@P_Articulo", OdbcType.VarChar);
                command.Parameters["@P_Articulo"].Value = proveedor.Articulo;
                command.Parameters.Add("@P_Descripcion", OdbcType.VarChar);
                command.Parameters["@P_Descripcion"].Value = proveedor.Descripcion;
                command.Parameters.Add("@P_Compannia", OdbcType.VarChar);
                command.Parameters["@P_Compannia"].Value = VariablesGlobales.Compannia;
                OdbcDataReader reader = command.ExecuteReader();
                List<Proveedores> Proveedor = new List<Proveedores>();

                while (reader.Read())
                {
                    string Descripcion = reader["Descripcion"].ToString();
                    string Articulo = reader["Articulo"].ToString();

                    Proveedores a = new Proveedores();

                    a.Descripcion = Descripcion;
                    a.Articulo = Articulo;
                    Proveedor.Add(a);
                }
                return Proveedor;
            }
            catch (OdbcException ax)
            {
                throw new ApplicationException("Error en Base de Datos..! \n" + ax.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ERROR AL OBTENER LOS ARTICULOS. DETALLE: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
