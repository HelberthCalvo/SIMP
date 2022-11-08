using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIMP.Datos;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Xml;

namespace General.Reports
{
    public class ReporteUtil
    {
        public static void ConfigurarReporte(ref ReportDocument repDoc)
        {
            new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo connectionInfo = new ConnectionInfo();
            Conexion con = new Conexion();
            connectionInfo.ServerName = con.ObtenerDato(0);
            connectionInfo.DatabaseName = con.ObtenerDato(1);
            connectionInfo.UserID = con.ObtenerDato(2);
            connectionInfo.Password = con.ObtenerDato(3);
            string compania = "dbo";
            connectionInfo.IntegratedSecurity = false;
            Tables tables = repDoc.Database.Tables;
            ConfigurarTables(crtableLogoninfo, connectionInfo, tables, compania);
            foreach (ReportDocument subreport in repDoc.Subreports)
            {
                tables = subreport.Database.Tables;
                ConfigurarTables(crtableLogoninfo, connectionInfo, tables, compania);
            }
        }

        public static void ConfigurarReporte(ref ReportClass repDoc)
        {
            new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo connectionInfo = new ConnectionInfo();
            Conexion con = new Conexion();
            connectionInfo.ServerName = con.ObtenerDato(0);
            connectionInfo.DatabaseName = con.ObtenerDato(1);
            connectionInfo.UserID = con.ObtenerDato(2);
            connectionInfo.Password = con.ObtenerDato(3);
            string compania = "dbo";
            connectionInfo.IntegratedSecurity = false;
            Tables tables = repDoc.Database.Tables;
            ConfigurarTables(crtableLogoninfo, connectionInfo, tables, compania);
            foreach (ReportDocument subreport in repDoc.Subreports)
            {
                tables = subreport.Database.Tables;
                ConfigurarTables(crtableLogoninfo, connectionInfo, tables, compania);
            }
        }

        public static void ConfigurarTables(TableLogOnInfo crtableLogoninfo, ConnectionInfo crConnectionInfo, Tables CrTables, string Schema)
        {
            foreach (Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
                if (LeerTablasSIMP().Find((string x) => x.Equals(CrTable.Location)) != null)
                {
                    CrTable.Location = "ERPADMIN." + CrTable.Location;
                }
                else
                {
                    CrTable.Location = Schema + "." + CrTable.Location;
                }
            }
        }
        public static List<string> LeerTablasSIMP()
        {
            string text = Path.Combine(HttpContext.Current.Server.MapPath("~"), "XML/TablasERPADMIN.xml");
            List<string> list = new List<string>();
            if (!File.Exists(text))
            {
                return list;
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(text);
            foreach (XmlNode item in xmlDocument.SelectNodes("TABLAS/TABLA"))
            {
                list.Add(item.InnerText);
            }

            return list;
        }
    }
}