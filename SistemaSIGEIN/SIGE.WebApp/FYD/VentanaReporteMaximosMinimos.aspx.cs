using SIGE.Entidades;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaReporteMaximosMinimos : System.Web.UI.Page
    {

        #region Variables

        private Guid vIdReporte
        {
            get { return (Guid)ViewState["vs_vrm_id_reporte"]; }
            set { ViewState["vs_vrm_id_reporte"] = value; }
        }

        private List<E_REPORTE_MAXIMO_MINIMO> oListaReporte
        {
            get { return (List<E_REPORTE_MAXIMO_MINIMO>)ViewState["vs_vrm_lista_reporte"]; }
            set { ViewState["vs_vrm_lista_reporte"] = value; }
        }

        private int vNoEmpleados {
            get { return (int)ViewState["vs_vrm_no_empleados"]; }
            set { ViewState["vs_vrm_no_empleados"] = value; }
        }

        private int vNoStock {
            get { return (int)ViewState["vs_vrm_no_stock"]; }
            set { ViewState["vs_vrm_no_stock"] = value; }
        }

        private int vIdPuesto {
            get { return (int)ViewState["vs_vrm_id_puesto"]; }
            set { ViewState["vs_vrm_id_puesto"] = value; }
        }

        #endregion

        #region Funciones

        private void CargarDatos() {

            E_REPORTE_MAXIMO_MINIMO oDatosReporte = ContextoReportes.oReporteMaximoMinimo.Where(t => t.ID_REPORTE == vIdReporte).FirstOrDefault();

            int empleados, stock;

            ConsultasFYDNegocio neg = new ConsultasFYDNegocio();

            vIdPuesto = oDatosReporte.ID_PUESTO_OBJETIVO;
            oListaReporte = neg.ReporteMaximosMinimos(out empleados,out stock, vIdPuesto);

            SPE_OBTIENE_M_PUESTO_Result oPuesto = neg.ObtienePuestos(ID_PUESTO: vIdPuesto).FirstOrDefault();

            vNoEmpleados = empleados;
            vNoStock = stock;

            txtClavePuesto.Text = oPuesto.CL_PUESTO;
            txtNombrePuesto.Text = oPuesto.NB_PUESTO;
            txtNoOcupantes.Text = vNoEmpleados.ToString();
            txtReorden.Text = oDatosReporte.NO_PUNTO_REORDEN.ToString();
            txtStock.Text = vNoStock.ToString();
            txtCapacitar.Text = ((oDatosReporte.NO_DIAS_CURSO * oDatosReporte.NO_ROTACION_PROMEDIO) / 30).ToString();

            if (vNoStock > oDatosReporte.NO_PUNTO_REORDEN)
            {
                lblMensaje.Visible = false;
                txtStock.ReadOnlyStyle.BackColor = System.Drawing.Color.Green;
            }
            else if (vNoStock < oDatosReporte.NO_PUNTO_REORDEN)
            {
                txtStock.ReadOnlyStyle.BackColor = System.Drawing.Color.Red;
                txtStock.ReadOnlyStyle.ForeColor = System.Drawing.Color.White;
                lblMensaje.Visible = true;
            }
            else
            {
                txtStock.ReadOnlyStyle.BackColor = System.Drawing.Color.Yellow;
                lblMensaje.Visible = true; 
            }
        }


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["IdReporte"] != null)
                {
                    vIdReporte = Guid.Parse(Request.Params["IdReporte"].ToString());
                    CargarDatos();
                }
            }
        }

        protected void rgReporte_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgReporte.DataSource = oListaReporte;
        }
    }
}