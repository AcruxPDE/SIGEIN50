using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stimulsoft.Report;
using Stimulsoft.Report.Web;
using SIGE.Negocio.Utilerias;
namespace SIGE.WebApp.IDP
{
    public partial class VentanaVistaPreviaDescriptivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["pIdPuesto"] != null)
                {
                    StiReport report = new StiReport();
                    string pathValue = string.Empty;

                    report.Load(Server.MapPath("~/Assets/reports/IDP/Descriptivo2.mrt"));
                    report.Dictionary.Databases.Clear();

                    //System.Configuration.Configuration rootWebConfig1 = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
                    //pathValue = rootWebConfig1.ConnectionStrings.ConnectionStrings[0].ConnectionString;

                   pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
                   report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));

                    report.Dictionary.Variables["ID_PUESTO"].Value = Request.Params["pIdPuesto"].ToString();
                    if (ContextoApp.ADM.fgVisibleMensajes)
                    {
                        report.Dictionary.Variables["FG_VISIBLE"].Value = ContextoApp.ADM.AutoridadPoliticaIntegral.fgVisible.ToString();
                        report.Dictionary.Variables["DS_MENSAJE"].Value = ContextoApp.ADM.AutoridadPoliticaIntegral.dsMensaje.ToString();
                    }

                    report.Compile();
                    
                    //report["NB_PUESTO"] = "Esta es una prueba, no el Id del puesto";
                    report["ID_PUESTO"] = int.Parse(Request.Params["pIdPuesto"].ToString());
                    

                    //report.DataSources["Puesto"].Parameters["@PIN_ID_PUESTO"].ParameterValue = int.Parse(Request.Params["pIdPuesto"].ToString());
                    //report.DataSources["Puesto_Escolaridad"].Parameters["@PIN_ID_PUESTO"].ParameterValue = int.Parse(Request.Params["pIdPuesto"]);                    
                    //report.DataSources["Puesto_Competencias_Perfil"].Parameters["@PIN_ID_PUESTO"].ParameterValue = int.Parse(Request.Params["pIdPuesto"]);
                    //report.DataSources["Puesto_Experiencia"].Parameters["@PIN_ID_PUESTO"].ParameterValue = int.Parse(Request.Params["pIdPuesto"]);
                    //report.DataSources["Puesto_Relaciones"].Parameters["@PIN_ID_PUESTO"].ParameterValue = int.Parse(Request.Params["pIdPuesto"]);
                    //report.DataSources["Funciones_Genericas"].Parameters["@PIN_ID_PUESTO"].ParameterValue = int.Parse(Request.Params["pIdPuesto"]);
                    //report.DataSources["Puesto_Funciones_Genericas"].Parameters["@PIN_ID_PUESTO"].ParameterValue = int.Parse(Request.Params["pIdPuesto"]);
                    //report.DataSources["Competencias_Funciones"].Parameters["@PIN_ID_PUESTO"].ParameterValue = int.Parse(Request.Params["pIdPuesto"]);
                    //report.DataSources["Competencias_Indicadores"].Parameters["@PIN_ID_PUESTO"].ParameterValue = int.Parse(Request.Params["pIdPuesto"]);
                    //report.DataSources["Puesto_Competencias_Genericas"].Parameters["@PIN_ID_PUESTO"].ParameterValue = int.Parse(Request.Params["pIdPuesto"]);
                    //report.DataSources["Puesto_Campos_Adicionales"].Parameters["@PIN_ID_PUESTO"].ParameterValue = int.Parse(Request.Params["pIdPuesto"]);


                    //report.Compile();
                    //report.Show();
                    swvReporte.Report = report;
                }    
            }            
        }
    }
}