using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;

namespace SIMP.Datos
{
  public class Conexion
  {
    public const string ESQUEMA_PADRE = "dbo";
    
    protected static ArrayList CargarPreferencias()
    {
      ArrayList datos = new ArrayList();
      string direccion = ConfigurationManager.AppSettings["Ruta_conexion"];
      try
      {
        if (File.Exists(direccion))
        {
          XmlDocument Conexiones = new XmlDocument();
          Conexiones.Load(direccion);
          datos.Add(Conexiones.SelectSingleNode("/CONEXION/SERVER").InnerText);
          datos.Add(Conexiones.SelectSingleNode("/CONEXION/DATABASE").InnerText);
          datos.Add(Conexiones.SelectSingleNode("/CONEXION/USER").InnerText);
          datos.Add(Conexiones.SelectSingleNode("/CONEXION/PASSWORD").InnerText);
        }
      }
      catch
      {
      }
      return datos;
    }

    public static string CadenaDeConexion()
    {
      ArrayList Conexion = new ArrayList();
      Conexion = CargarPreferencias();
      return "Data Source=" + Conexion[0].ToString() +
      ";Initial Catalog=" + Conexion[1].ToString() +
      ";User ID=" + Conexion[2].ToString() +
      ";Password=" + Conexion[3].ToString();
    }

    public string ObtenerDato(int dato)
    {
      ArrayList Conexion = new ArrayList();
      Conexion = CargarPreferencias();
      return Conexion[dato].ToString();
    }
  }
}
