using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using ZonaFranca.Logica;

namespace ZonaFRanca.Datos
{
    public class Conexion
    {
        private System.Data.Odbc.OdbcConnection myConexion;

        static string ConexionString = "";
        public static string conexion()
        {
            return ConexionString = "Data Source=" + VariablesGlobales.servidor +
            ";Initial Catalog=" + VariablesGlobales.bd +
            ";User ID=" + VariablesGlobales.usuario +
            ";Password=" + VariablesGlobales.contrasenna;
        }
  

        // Cadena de conexión

        /* public static  string StringConexion
         {
             get
             {
                 return ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
             }
         }*/

        public static string StringConexionBD(string baseDatos)
       {           
           return ConfigurationManager.ConnectionStrings[baseDatos].ConnectionString;           
       }

      /* public static string CadenaConexionLogin
       {
           get
           {
               return "Dsn=SITSA;  uid=" + VariablesGlobales.Usuario + "; pwd=" + VariablesGlobales.Contrasenna+ ";";
           }
       }*/

       public static string CadenaConexion(string baseDatos)
       {
           return "Dsn="+baseDatos+";  uid=" + VariablesGlobales.Usuario + "; pwd=" + VariablesGlobales.Contrasenna+ ";";
            //return "Dsn=" + baseDatos + ";  uid=inolasati_exactus; pwd=NpF4haTI;";
        }

       //private string StringConexion = "ConnectionString1";//GlobalVariables.var_ConexionGen;     
       
        //Constructor por defecto
       public Conexion(string baseDatos)
       {
           myConexion = new System.Data.Odbc.OdbcConnection(StringConexionBD(baseDatos));
       }

      /* public Conexion() 
        {
            myConexion = new System.Data.Odbc.OdbcConnection(StringConexion);
        }*/

       public static bool ConnectToSql(string baseDatos)
       {
           OdbcConnection conn = new OdbcConnection();
           conn.ConnectionString = Conexion.CadenaConexion(baseDatos);
           try
           {
               conn.Open();
               return true;
           }
           catch (Exception ex)
           {
               return false;
           }
           finally
           {
               conn.Close();
           }
       }
       public bool ConsultaNoRetorno(string consulta)
       {

           try
           {
               myConexion.Open();

               OdbcCommand myComando = new OdbcCommand(consulta, myConexion);

               myComando.ExecuteNonQuery();
               myConexion.Close();
               return true;
           }
           catch (Exception)
           {
               myConexion.Close();
               return false;
               throw;
           }

       }

       public bool EjecutarPA(OdbcCommand myComando)
       {
           myComando.Connection = myConexion;

           try
           {
               myConexion.Open();
               myComando.ExecuteNonQuery();
               myConexion.Close();
               return true;
           }
           catch (Exception ex)
           {
               myConexion.Close();
               return false;
               throw;
           }


       }

       public DataTable EjecutarPAConRetorno(OdbcCommand myComando)
       {
           myComando.Connection = myConexion;
           OdbcDataReader myReader;
           DataTable MyTabla = new DataTable();
           DataRow myDataRow;

           try
           {
               myConexion.Open();
               myReader = myComando.ExecuteReader();

               if (myReader != null)
               {
                   //Recorro mi cursor en busca de las columnas retornadas por la consulta
                   for (int i = 0; i < myReader.FieldCount; i++)
                   {
                       MyTabla.Columns.Add(myReader.GetName(i));
                   }

                   //Recorro mi cursor en busca de los datos contenidos en cada tupla
                   while (myReader.Read())
                   {
                       //Instancio una nueva fila de myDataTable
                       myDataRow = MyTabla.NewRow();

                       for (int j = 0; j < myReader.FieldCount; j++)
                       {
                           myDataRow[j] = myReader.GetValue(j);
                       }

                       //Agrego la nueva fila a mi myDataTable
                       MyTabla.Rows.Add(myDataRow);
                   }
               }

               myConexion.Close();
               return MyTabla;
           }
           catch (Exception e)
           {
               myConexion.Close();
               return null;
               throw;
           }
           //finally { myConexion.Close(); }
       }


       // // Consulta que retorna un entero con un unico elemento 
       public string ConsultaConRetornoLlave(OdbcCommand myComando)
       {

           OdbcDataReader resultado;
           string valor;
           myComando.Connection = myConexion;
           try
           {
               myConexion.Open();
               resultado = myComando.ExecuteReader();
               if (resultado.Read())
                   valor = resultado.GetValue(0).ToString();
               else
                   valor = "";
               myConexion.Close();
               return valor;
           }
           catch (Exception ex)
           {
               myConexion.Close();
               return "";
               throw;
           }

       }

       public bool ConsultaConRetorno(string consulta)
       {
           int a = 0;
           try
           {
               myConexion.Open();
               OdbcCommand myComando = new OdbcCommand(consulta, myConexion);

               myComando.ExecuteNonQuery();
               myConexion.Close();
               return true;
           }
           catch (Exception)
           {
               myConexion.Close();
               return false;
               throw;
           }

       }

    }
}
