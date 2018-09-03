using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.ModulosApoyo
{
    public partial class stiDiseñoReportes : System.Web.UI.Page
    {

        #region Eventos Load

        protected void Page_Load(object sender, EventArgs e)
        {
            string ruta;
            if (Request.QueryString["ruta"] != null)
            {
                ruta = Request.QueryString["ruta"];
                if (ruta != string.Empty)
                {
                    StiReport reporte = new StiReport();
                    reporte.Load(ruta);
                    StiDiseñoReportes.Design(reporte);
                }
            }
            else
            {      
                SqlConnection sqlConection = new SqlConnection();
               try
               {
                    StiReport  reporte = new StiReport();
                   reporte.Dictionary.Databases.Clear();
                   reporte.Dictionary.DataSources.Clear();
                   reporte.DataSources.Clear();

                   string cadenaConexion =ConfigurationManager.ConnectionStrings["SistemaSigeinEntities"].ConnectionString.Substring(ConfigurationManager.ConnectionStrings["SistemaSigeinEntities"].ConnectionString.IndexOf("connection string=", 0) + 19).Substring(0,ConfigurationManager.ConnectionStrings["SistemaSigeinEntities"].ConnectionString.Substring(ConfigurationManager.ConnectionStrings["SistemaSigeinEntities"].ConnectionString.IndexOf("connection string=", 0) + 19).Length -1);                                     
                   
                   sqlConection.ConnectionString = cadenaConexion;
                   sqlConection.Open();

                   reporte.Dictionary.Databases.Clear();
                   reporte.Dictionary.Databases.Add(new StiSqlDatabase(sqlConection.Database, sqlConection.Database, cadenaConexion,false));                   

                   StiDiseñoReportes.Design(reporte);
                   sqlConection.Close();
               }
               catch(Exception ex) { sqlConection.Close(); }
            }           

        }

        #endregion

        #region Eventos

        protected void StiDiseñoReportes_SaveReport(object sender, Stimulsoft.Report.Web.StiWebDesigner.StiSaveReportEventArgs e)
        {
            StiReport reporte = e.Report;
            string guid = reporte.ReportGuid;
            string strReporte = reporte.SaveToString();
            e.Report.Save(Server.MapPath(reporte.ReportName + ".mrt"));
        }

        protected void StiDiseñoReportes_PreInit(object sender, Stimulsoft.Report.Web.StiWebDesigner.StiPreInitEventArgs e)
        {
            Stimulsoft.Report.Web.StiWebDesignerOptions.ModifyConnections = false;
            Stimulsoft.Report.Web.StiWebDesignerOptions.Connection.ClientRequestTimeout = 9000;
        }

        #endregion
    }
}