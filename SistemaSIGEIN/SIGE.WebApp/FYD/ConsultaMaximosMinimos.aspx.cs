using SIGE.Entidades.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.FYD
{
    public partial class ConsultaMaximosMinimos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        protected void btnMaximosMinimos_Click(object sender, EventArgs e)
        {
            Guid vIdReporte = Guid.NewGuid();

            if (ContextoReportes.oReporteMaximoMinimo == null)
            {
                ContextoReportes.oReporteMaximoMinimo = new List<E_REPORTE_MAXIMO_MINIMO>();
            }

            E_REPORTE_MAXIMO_MINIMO oReporte = new E_REPORTE_MAXIMO_MINIMO();

            oReporte.ID_PUESTO_OBJETIVO = int.Parse(rlbPuesto.Items[0].Value);
            oReporte.ID_REPORTE = vIdReporte;
            oReporte.NO_DIAS_CURSO = int.Parse(txtDiasCurso.Text);
            oReporte.NO_PORCENTAJE_NO_APROBADOS = int.Parse(txtPorcentaje.Text);
            oReporte.NO_PUNTO_REORDEN = int.Parse(txtReorden.Text);
            oReporte.NO_ROTACION_PROMEDIO = int.Parse(txtRotacion.Text);

            ContextoReportes.oReporteMaximoMinimo.Add(oReporte);

            string script = "OpenMaximosMinimosWindow(\"" + vIdReporte + "\");";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
    }
}