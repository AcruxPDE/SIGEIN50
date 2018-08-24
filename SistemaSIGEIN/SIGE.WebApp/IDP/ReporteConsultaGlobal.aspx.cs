﻿using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.IntegracionDePersonal;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebApp.Comunes;

namespace SIGE.WebApp.IDP
{
    public partial class ReporteConsultaGlobal : System.Web.UI.Page
    {
        #region Variabes
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vClUsuario { get; set; }
        private string vNbPrograma { get; set; }

        public int vIdPuesto
        {
            get { return (int)ViewState["vs_vcg_id_puesto"]; }
            set { ViewState["vs_vcg_id_puesto"] = value; }
        }

        public int vIdCandidato
        {
            get { return (int)ViewState["vs_vcg_id_candidato"]; }
            set { ViewState["vs_vcg_id_candidato"] = value; }
        }

        public List<E_CONSULTA_GLOBAL> vLstCalificaciones
        {
            get { return (List<E_CONSULTA_GLOBAL>)ViewState["vs_vcg_lst_calificaciones"]; }
            set { ViewState["vs_vcg_lst_calificaciones"] = value; }
        }

        public decimal vPrPsicometria
        {
            get { return (int)ViewState["vs_vcg_pr_psicometria"]; }
            set { ViewState["vs_vcg_pr_psicometria"] = value; }
        }

        public decimal vPrIngles
        {
            get { return (int)ViewState["vs_vcg_pr_ingles"]; }
            set { ViewState["vs_vcg_pr_ingles"] = value; }
        }

        private int vIdConsultaGlobal
        {
            get { return (int)ViewState["vs_vcg_id_consulta_global"]; }
            set { ViewState["vs_vcg_id_consulta_global"] = value; }
        }

        #endregion

        #region Funciones

        private void CargarDatos()
        {
            CandidatoNegocio nCandidato = new CandidatoNegocio();
            DescriptivoNegocio nDescriptivo = new DescriptivoNegocio();

            var vCandidato = nCandidato.ObtieneCandidato(pIdCandidato: vIdCandidato).FirstOrDefault();

            if (vCandidato != null)
            {
                txtClSolicitud.InnerText = vCandidato.CL_SOLICITUD;
                txtNbCandidato.InnerText = vCandidato.NB_CANDIDATO_COMPLETO;
            }

            var vPuesto = nDescriptivo.ObtieneDescriptivo(vIdPuesto);

            if (vPuesto != null)
            {
                txtNbPuesto.InnerText = vPuesto.CL_PUESTO + " - " + vPuesto.NB_PUESTO;
            }

            CargarCalificaciones();

        }

        private void CargarCalificaciones()
        {
            ConsultasComparativasNegocio nComparativas = new ConsultasComparativasNegocio();

            E_RESULTADO vResultado = nComparativas.InsertaConsultaGlobalCalificaciones(vIdCandidato, vIdPuesto, vClUsuario, vNbPrograma);

            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
            {
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            }
            else
            {
                vLstCalificaciones = nComparativas.ObtieneConsultaGlobalCalificaciones(vIdCandidato, vIdPuesto);
                vIdConsultaGlobal = vLstCalificaciones[0].ID_CONSULTA_GLOBAL;

                if (vLstCalificaciones[0].DS_COMENTARIOS != null)
                {
                    txtComentarios.InnerHtml = Utileria.MostrarNotas(vLstCalificaciones[0].DS_COMENTARIOS);
                }

                decimal vPrtoTotalPonderacion = vLstCalificaciones.Sum(t => t.PR_FACTOR);
            }
        }

        protected void GraficaConsultaGlobal(List<E_CONSULTA_GLOBAL> plstConsultaGlobal)
        {
            rhcConsultaGlobal.PlotArea.Series.Clear();
            rhcConsultaGlobal.PlotArea.XAxis.Items.Clear();
            ColumnSeries vSolicitud = new ColumnSeries();
            ColumnSeries vPuesto = new ColumnSeries();

            foreach (var i in plstConsultaGlobal)
            {
                decimal vTotal;
                vSolicitud.SeriesItems.Add(new CategorySeriesItem(i.PR_FACTOR));
                vSolicitud.TooltipsAppearance.DataFormatString = "{0:N2}%";
                vSolicitud.LabelsAppearance.Visible = false;
                vSolicitud.Name = "Puesto";
                vTotal = decimal.Round((i.PR_CALIFICACION * i.PR_FACTOR / 100), 2, MidpointRounding.AwayFromZero);
                vPuesto.SeriesItems.Add(new CategorySeriesItem(vTotal));
                vPuesto.TooltipsAppearance.DataFormatString = "{0:N2}%";
                vPuesto.LabelsAppearance.Visible = false;
                vPuesto.Name = "Solicitud";
                rhcConsultaGlobal.PlotArea.XAxis.Items.Add(i.NB_FACTOR);
                rhcConsultaGlobal.PlotArea.XAxis.LabelsAppearance.RotationAngle = 270;
            }

            rhcConsultaGlobal.PlotArea.Series.Add(vSolicitud);
            rhcConsultaGlobal.PlotArea.Series.Add(vPuesto);
        }

        private void CargarReporte()
        {
            CargarDatos();
            GraficaConsultaGlobal(vLstCalificaciones);
            //rgdCompatibilidad.Rebind();
            if (vLstCalificaciones[0].DS_COMENTARIOS != null)
            {
                txtComentarios.InnerHtml = Utileria.MostrarNotas(vLstCalificaciones[0].DS_COMENTARIOS);
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                vLstCalificaciones = new List<E_CONSULTA_GLOBAL>();

                if (Request.Params["IdPuesto"] != null)
                {
                    vIdPuesto = int.Parse(Request.Params["IdPuesto"].ToString());
                }

                if (Request.Params["IdCandidato"] != null)
                {
                    vIdCandidato = int.Parse(Request.Params["IdCandidato"].ToString());
                }

                CargarDatos();
                GraficaConsultaGlobal(vLstCalificaciones);
                dvCompatibilidad.Controls.Add(GeneraTabla());
            }
        }

        protected HtmlGenericControl GeneraTabla()
        {
            HtmlGenericControl vTabla = new HtmlGenericControl("table");
            vTabla.Attributes.Add("style", "border-collapse: collapse;");

            HtmlGenericControl vCtrlColumn = new HtmlGenericControl("thead");
            vCtrlColumn.Attributes.Add("style", "background: #E6E6E6;");

            HtmlGenericControl vCtrlRowEncabezado1 = new HtmlGenericControl("tr");
            vCtrlRowEncabezado1.Attributes.Add("style", "page-break-inside:avoid; page-break-after:auto;");

            HtmlGenericControl vCtrlTh1 = new HtmlGenericControl("td");
            vCtrlTh1.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:200px;");
            vCtrlTh1.InnerText = String.Format("{0}", "Elemento");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh1);

            HtmlGenericControl vCtrlTh2 = new HtmlGenericControl("td");
            vCtrlTh2.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:100px;");
            vCtrlTh2.InnerText = String.Format("{0}", "Valor");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh2);

            vCtrlColumn.Controls.Add(vCtrlRowEncabezado1);

            vTabla.Controls.Add(vCtrlColumn);

            HtmlGenericControl vCtrlTbody = new HtmlGenericControl("tbody");

            foreach (var item in vLstCalificaciones)
            {
                HtmlGenericControl vCtrlRowNivel = new HtmlGenericControl("tr");
                vCtrlRowNivel.Attributes.Add("style", "page-break-inside:avoid; page-break-after:auto;");

                HtmlGenericControl vCtrlNivel = new HtmlGenericControl("td");
                vCtrlNivel.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt;");
                vCtrlNivel.InnerText = String.Format("{0}", item.NB_FACTOR);
                vCtrlRowNivel.Controls.Add(vCtrlNivel);

                HtmlGenericControl vCtrlPromedio = new HtmlGenericControl("td");
                vCtrlPromedio.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt;");
                vCtrlPromedio.Attributes.Add("align", "right");
                vCtrlPromedio.InnerText = String.Format("{0:N2}", item.PR_VALOR);
                vCtrlRowNivel.Controls.Add(vCtrlPromedio);

                vCtrlTbody.Controls.Add(vCtrlRowNivel);
            }

            vTabla.Controls.Add(vCtrlTbody);

            return vTabla;

           // rgdCompatibilidad.DataSource = vLstCalificaciones;
        }

    }
}