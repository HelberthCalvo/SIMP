
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIMP.Datos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Logica.UTILITIES
{
  public class PrintHelper
  {
    public static void imprimirDocumento(Hashtable parametrosReporte, string nombreImpresora, string rutaReporte)
    {
      try
      {
        string var_reporte = rutaReporte;
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Conexion con = new Conexion();
        crConnectionInfo.UserID = con.ObtenerDato(2);  //"ECOMMERCE"; 
        crConnectionInfo.Password = con.ObtenerDato(3); //"Sa123";
        crConnectionInfo.DatabaseName = con.ObtenerDato(1);  //"Exactus";
        crConnectionInfo.ServerName = con.ObtenerDato(0); // "Exactus";

        //  Conexion con = new Conexion();
        //  conex.DatabaseName = con.ObtenerDato(1);  //"Exactus";
        //  conex.UserID = con.ObtenerDato(2);  //"ECOMMERCE";  
        //  conex.Password = con.ObtenerDato(3); //"Sa123";
        //  conex.ServerName = con.ObtenerDato(0); // "Exactus";
        //  conex.Type = ConnectionInfoType.SQL;


        using (ReportDocument reportDocument = new ReportDocument())
        {
          reportDocument.FileName = var_reporte;
          reportDocument.Load(reportDocument.FileName);
          reportDocument.Refresh();

          ICollection listaNombresParametros = parametrosReporte.Keys;

          foreach (string nombreParametro in listaNombresParametros)
          {
            reportDocument.SetParameterValue(nombreParametro, parametrosReporte[nombreParametro]);
          }
          //System.Drawing.Printing.PrinterSettings printerSettings = new System.Drawing.Printing.PrinterSettings();
          PrinterSettings printerSettings = new PrinterSettings();
          printerSettings.PrinterName = nombreImpresora;

          reportDocument.SetDatabaseLogon(crConnectionInfo.UserID, crConnectionInfo.Password);

          //int fin = reportDocument.FormatEngine.GetLastPageNumber(new CrystalDecisions.Shared.ReportPageRequestContext());
          reportDocument.PrintToPrinter(printerSettings, new PageSettings(), false);
          
          //reportDocument.PrintToPrinter(1, false, 0, 0);
        }

      }
      catch (Exception ex)
      {
        throw new Exception("Ocurrio un error al intentar imprimir el tiquete. Verifique que la impresora esté conectada. " + ex.Message);
      }
    }

    public static Stream generarPDF(Hashtable parametrosReporte, string nombreImpresora, string rutaReporte)
    {
      try
      {
        string var_reporte = rutaReporte;
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Conexion con = new Conexion();
        crConnectionInfo.UserID = con.ObtenerDato(2);  //"ECOMMERCE"; 
        crConnectionInfo.Password = con.ObtenerDato(3); //"Sa123";
        crConnectionInfo.DatabaseName = con.ObtenerDato(1);  //"Exactus";
        crConnectionInfo.ServerName = con.ObtenerDato(0); // "Exactus";

        //  Conexion con = new Conexion();
        //  conex.DatabaseName = con.ObtenerDato(1);  //"Exactus";
        //  conex.UserID = con.ObtenerDato(2);  //"ECOMMERCE";  
        //  conex.Password = con.ObtenerDato(3); //"Sa123";
        //  conex.ServerName = con.ObtenerDato(0); // "Exactus";
        //  conex.Type = ConnectionInfoType.SQL;


        using (ReportDocument reportDocument = new ReportDocument())
        {
          reportDocument.FileName = var_reporte;
          reportDocument.Load(reportDocument.FileName);
          reportDocument.Refresh();

          ICollection listaNombresParametros = parametrosReporte.Keys;

          foreach (string nombreParametro in listaNombresParametros)
          {
            reportDocument.SetParameterValue(nombreParametro, parametrosReporte[nombreParametro]);
          }
          //System.Drawing.Printing.PrinterSettings printerSettings = new System.Drawing.Printing.PrinterSettings();
          PrinterSettings printerSettings = new PrinterSettings();
          printerSettings.PrinterName = nombreImpresora;

          reportDocument.SetDatabaseLogon(crConnectionInfo.UserID, crConnectionInfo.Password);

          //int fin = reportDocument.FormatEngine.GetLastPageNumber(new CrystalDecisions.Shared.ReportPageRequestContext());
          return reportDocument.ExportToStream(ExportFormatType.PortableDocFormat);

          //reportDocument.PrintToPrinter(1, false, 0, 0);
        }

      }
      catch (Exception ex)
      {
        throw new Exception("Ocurrio un error al intentar imprimir el tiquete. Verifique que la impresora esté conectada. " + ex.Message);
      }
    }
  }
}
