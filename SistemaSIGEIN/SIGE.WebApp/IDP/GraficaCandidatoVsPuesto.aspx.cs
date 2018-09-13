using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.IntegracionDePersonal;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class GraficaCandidatoVsPuesto : System.Web.UI.Page
    {
        #region Variables

        public Guid vIdCandidatoVsPuesto
        {
            get { return (Guid)ViewState["vs_vIdCandidatoVsPuesto"]; }
            set { ViewState["vs_vIdCandidatoVsPuesto"] = value; }
        }

        public int vIdCandidato
        {
            get { return (int)ViewState["vs_vIdCandidato"]; }
            set { ViewState["vs_vIdCandidato"] = value; }
        }

        public int? vIdPuestoCandidato
        {
            get { return (int?)ViewState["vs_vIdPuestoCandidato"]; }
            set { ViewState["vs_vIdPuestoCandidato"] = value; }
        }

        public string vs_NB_CANDIDATO
        {
            get { return (string)ViewState["vs_NB_CANDIDATO"]; }
            set { ViewState["vs_NB_CANDIDATO"] = value; }
        }

        public bool vFgConsultaParcial
        {
            get { return (bool)ViewState["vs_vFgConsultaParcial"]; }
            set { ViewState["vs_vFgConsultaParcial"] = value; }
        }

        public bool vFgEvaluacionesCero
        {
            get { return (bool)ViewState["vs_vFgEvaluacionesCero"]; }
            set { ViewState["vs_vFgEvaluacionesCero"] = value; }
        }

        public List<int> vListaPuestos
        {
            get { return (List<int>)ViewState["vs_vListaPuestos"]; }
            set { ViewState["vs_vListaPuestos"] = value; }
        }

        public List<E_GRAFICA_PUESTO_CANDIDATOS> vCandidatoPuestos
        {
            get { return (List<E_GRAFICA_PUESTO_CANDIDATOS>)ViewState["vs_vCandidatoPuestos"]; }
            set { ViewState["vs_vCandidatoPuestos"] = value; }
        }

        private string vClUsuario
        {
            get { return (string)ViewState["vsvClUsuario"]; }
            set { ViewState["vsvClUsuario"] = value; }
        }

        private string vNbPrograma
        {
            get { return (string)ViewState["vsvNbPrograma"]; }
            set { ViewState["vsvNbPrograma"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = (ContextoUsuario.oUsuario != null ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO");
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                vIdCandidatoVsPuesto = Guid.Empty;
                vFgConsultaParcial = ContextoApp.IDP.ConfiguracionIntegracion.FgConsultasParciales;
                vFgEvaluacionesCero = ContextoApp.IDP.ConfiguracionIntegracion.FgIgnorarCompetencias;
                if (Request.Params["vIdCandidatoVsPuestos"] != null)
                {
                    vIdCandidatoVsPuesto = Guid.Parse(Request.Params["vIdCandidatoVsPuestos"].ToString());
                }
                else
                    vIdCandidatoVsPuesto = Guid.NewGuid();

                if (ContextoConsultasComparativas.oCandidatoVsPuestos == null)
                {
                    ContextoConsultasComparativas.oCandidatoVsPuestos = new List<E_CANDIDATO_VS_PUESTOS>();
                }

                if (Request.Params["IdCandidato"] != null)
                {
                    vIdCandidato = int.Parse(Request.Params["IdCandidato"]);
                }
                if (Request.Params["pIdPuestoTablero"] != null)
                {
                    vIdPuestoCandidato = int.Parse(Request.Params["pIdPuestoTablero"]);
                }

                CargarCandidatoPuestos();
            }

        }

        protected void CargarCandidatoPuestos()
        {
            vListaPuestos = new List<int>();

            if (vIdPuestoCandidato == null)
            {
                if (vIdCandidatoVsPuesto != Guid.Empty)
                {
                    E_CANDIDATO_VS_PUESTOS oCandidatoPuestos = ContextoConsultasComparativas.oCandidatoVsPuestos.Where(w => w.vIdCandidatoVsPuestos == vIdCandidatoVsPuesto).FirstOrDefault();
                    vListaPuestos = oCandidatoPuestos.vListaPuestos;
                }
            }
            else
            {
                vListaPuestos = new List<int>() { (int)vIdPuestoCandidato };
            }

            var vXelements = vListaPuestos.Select(x =>
                                           new XElement("PUESTO",
                                           new XAttribute("ID_PUESTO", x)
                                ));
            XElement SELECCIONADOS =
            new XElement("PUESTOS", vXelements
                );

            ConsultasComparativasNegocio nComparativas = new ConsultasComparativasNegocio();
            vCandidatoPuestos = nComparativas.ObtieneCandidatoPuestos(pID_CANDIDATO: vIdCandidato, pXML_PUESTOS: SELECCIONADOS.ToString(), vFgConsultaParcial: vFgConsultaParcial, vFgCalificacionCero: vFgEvaluacionesCero).Select(s => new E_GRAFICA_PUESTO_CANDIDATOS
            {
                CL_COMPETENCIA = s.CL_COMPETENCIA,
                NB_COMPETENCIA = s.NB_COMPETENCIA,
                NO_VALOR_NIVEL = s.NO_VALOR_NIVEL,
                NB_CANDIDATO = s.NB_CANDIDATO,
                NO_VALOR_CANDIDATO = s.NO_VALOR_CANDIDATO,
                PR_CANDIDATO_PUESTO = s.PR_CANDIDATO_PUESTO,
                DS_COMPETENCIA = s.DS_COMPETENCIA,
                ID_BATERIA = s.ID_BATERIA,
                NB_PUESTO = s.NB_PUESTO,
                CL_PUESTO = s.CL_PUESTO,
                CL_COLOR = s.CL_COLOR,
                ID_PUESTO = s.ID_PUESTO,
                PR_TRUNCADO = CalculaPorcentaje(s.PR_CANDIDATO_PUESTO),
                CL_SOLICITUD = s.CL_SOLICITUD
            }).OrderBy(s => s.CL_COMPETENCIA).ToList();

            //if (vCandidatoPuestos.Count > 0)
            //{
            //    vs_NB_CANDIDATO = vCandidatoPuestos.FirstOrDefault().NB_CANDIDATO;
            //}

            //dvPuestos.Controls.Add(GenerarTablaPuestos(vCandidatoPuestos));

            //var vPuestoCompetencia = vCandidatoPuestos.Select(s => new { s.NO_VALOR_CANDIDATO, s.NB_CANDIDATO, s.NB_COMPETENCIA, s.CL_SOLICITUD }).Distinct().ToList();
            ColumnSeries vCandidato = new ColumnSeries();
           // if (vPuestoCompetencia.Count > 0)
              //  lbCandidatosCom.InnerText = "(" + vPuestoCompetencia.FirstOrDefault().CL_SOLICITUD + ") " + vPuestoCompetencia.FirstOrDefault().NB_CANDIDATO;

            GraficaCandidatoPuestos(vCandidatoPuestos);

            List<E_PROMEDIO> vlstPromedios = new List<E_PROMEDIO>();
            foreach (var item in vListaPuestos)
            {
                List<E_PROMEDIO> vlist = new List<E_PROMEDIO>();
                foreach (var i in vCandidatoPuestos)
                {
                    if (item == i.ID_PUESTO && i.NO_VALOR_NIVEL != 0)
                    {
                        vlist.Add(new E_PROMEDIO { NB_PUESTO = i.NB_PUESTO, PORCENTAJE = i.PR_TRUNCADO, PORCENTAJE_NO_TRUNCADO = i.PR_CANDIDATO_PUESTO });
                    }
                }
                vlstPromedios.Add(new E_PROMEDIO
                {
                    NB_PUESTO = vlist.Select(s => s.NB_PUESTO).FirstOrDefault(),
                    PROMEDIO = vlist.Average(s => s.PORCENTAJE),
                    FG_SUPERA_130 = vlist.Average(s => s.PORCENTAJE_NO_TRUNCADO) >= 130 ? true : false,
                    PROMEDIO_TXT = vlist.Average(s => s.PORCENTAJE_NO_TRUNCADO) >= 130 ? Decimal.Round(vlist.Average(s => s.PORCENTAJE) ?? 1, 2).ToString() + "(*)" : Decimal.Round(vlist.Average(s => s.PORCENTAJE) ?? 1, 2).ToString()
                                          ,
                    PORCENTAJE_NO_TRUNCADO = vlist.Average(s => s.PORCENTAJE_NO_TRUNCADO)
                });
            }
            dvTablaPuestoResul.Controls.Add(GenerarTotales(vlstPromedios));
            //rgdPromedios.DataSource = vlstPromedios.Where(w => w.NB_PUESTO != null);
            //rgdPromedios.DataBind();
            //rgdPromedios.Rebind();

            for (int i = 0; i < vlstPromedios.Count; i++)
            {
                if (vlstPromedios[i].FG_SUPERA_130 == true)
                {
                    divMensajeMayor130.Visible = true;
                    i = vlstPromedios.Count;
                }
            }
        }

        protected decimal? CalculaPorcentaje(decimal? pPorcentaje)
        {
            decimal? vPorcentaje = 0;
            if (pPorcentaje > 100)
                vPorcentaje = 100;
            else vPorcentaje = pPorcentaje;
            return vPorcentaje;
        }

        protected HtmlGenericControl GenerarTotales(List<E_PROMEDIO> plstPromedios)
        {
            HtmlGenericControl vTabla = new HtmlGenericControl("table");
            vTabla.Attributes.Add("style", "border-collapse: collapse;");

            HtmlGenericControl vCtrlColumn = new HtmlGenericControl("thead");
            vCtrlColumn.Attributes.Add("style", "background: #E6E6E6;");

            HtmlGenericControl vCtrlRowEncabezado1 = new HtmlGenericControl("tr");
            vCtrlRowEncabezado1.Attributes.Add("style", "page-break-inside:avoid; page-break-after:auto;");

            HtmlGenericControl vCtrlTh1 = new HtmlGenericControl("td");
            vCtrlTh1.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:200px;");
            vCtrlTh1.InnerText = String.Format("{0}", "Puesto");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh1);

            HtmlGenericControl vCtrlTh2 = new HtmlGenericControl("td");
            vCtrlTh2.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:100px;");
            vCtrlTh2.InnerText = String.Format("{0}", "% de compatibilidad");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh2);

            vCtrlColumn.Controls.Add(vCtrlRowEncabezado1);

            vTabla.Controls.Add(vCtrlColumn);

            HtmlGenericControl vCtrlTbody = new HtmlGenericControl("tbody");

            foreach (var item in plstPromedios.Where(w => w.NB_PUESTO != null))
            {
                HtmlGenericControl vCtrlRowNivel = new HtmlGenericControl("tr");
                vCtrlRowNivel.Attributes.Add("style", "page-break-inside:avoid; page-break-after:auto;");

                HtmlGenericControl vCtrlNivel = new HtmlGenericControl("td");
                vCtrlNivel.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt;");
                vCtrlNivel.InnerText = String.Format("{0}", item.NB_PUESTO);
                vCtrlRowNivel.Controls.Add(vCtrlNivel);

                HtmlGenericControl vCtrlPromedio = new HtmlGenericControl("td");
                vCtrlPromedio.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt;");
                vCtrlPromedio.Attributes.Add("align", "right");
                vCtrlPromedio.InnerText = String.Format("{0}%", item.PROMEDIO_TXT);
                vCtrlRowNivel.Controls.Add(vCtrlPromedio);

                vCtrlTbody.Controls.Add(vCtrlRowNivel);
            }

            vTabla.Controls.Add(vCtrlTbody);

            return vTabla;
        }

        protected HtmlGenericControl GenerarTablaPuestos(List<E_GRAFICA_PUESTO_CANDIDATOS> plstCandidatoPuestos)
        {
            HtmlGenericControl vTabla = new HtmlGenericControl("table");
            vTabla.Attributes.Add("style", "border-collapse: collapse;");

            HtmlGenericControl vCtrlTbody = new HtmlGenericControl("tbody");

            foreach (var item in vListaPuestos)
            {
                string vPuestosComp = "";

                if (plstCandidatoPuestos.Where(w => w.ID_PUESTO == item).FirstOrDefault() != null)
                    if (plstCandidatoPuestos.Where(w => w.ID_PUESTO == item).FirstOrDefault().CL_PUESTO != null)
                        vPuestosComp = "(" + plstCandidatoPuestos.Where(w => w.ID_PUESTO == item).FirstOrDefault().CL_PUESTO + ") " + plstCandidatoPuestos.Where(w => w.ID_PUESTO == item).FirstOrDefault().NB_PUESTO;
                    else
                        vPuestosComp = "( ) " + plstCandidatoPuestos.Where(w => w.ID_PUESTO == item).FirstOrDefault().NB_PUESTO;


                HtmlGenericControl vCtrlRowNivel = new HtmlGenericControl("tr");
                vCtrlRowNivel.Attributes.Add("style", "page-break-inside:avoid; page-break-after:auto;");

                HtmlGenericControl vCtrlNivel = new HtmlGenericControl("td");
                vCtrlNivel.Attributes.Add("style", "font-family:arial; font-size: 11pt;");
                vCtrlNivel.InnerText = String.Format("{0}", vPuestosComp);
                vCtrlRowNivel.Controls.Add(vCtrlNivel);

                vCtrlTbody.Controls.Add(vCtrlRowNivel);
            }
            vTabla.Controls.Add(vCtrlTbody);

            return vTabla;
        }

        protected void GraficaCandidatoPuestos(List<E_GRAFICA_PUESTO_CANDIDATOS> plstCandidatoPuestos)
        {
            List<ColumnSeries> lstPuestos = new List<ColumnSeries>();
            string vPuestosComp = "";

            bool continua = false;
            rhcCandidatoPuestos.PlotArea.Series.Clear();

            foreach (var item in vListaPuestos)
            {
                ColumnSeries vPuestos = new ColumnSeries();
                if (plstCandidatoPuestos.Where(w => w.ID_PUESTO == item).FirstOrDefault() != null)
                    if (plstCandidatoPuestos.Where(w => w.ID_PUESTO == item).FirstOrDefault().CL_PUESTO != null)
                        vPuestosComp = vPuestosComp + "- (" + plstCandidatoPuestos.Where(w => w.ID_PUESTO == item).FirstOrDefault().CL_PUESTO + ")" + plstCandidatoPuestos.Where(w => w.ID_PUESTO == item).FirstOrDefault().NB_PUESTO + "<br>";
                    else
                        vPuestosComp = vPuestosComp + "- ()" + plstCandidatoPuestos.Where(w => w.ID_PUESTO == item).FirstOrDefault().NB_PUESTO + "<br>";

                foreach (var i in plstCandidatoPuestos)
                {
                    if (item == i.ID_PUESTO)
                    {
                        vPuestos.SeriesItems.Add(new CategorySeriesItem(i.NO_VALOR_NIVEL));
                        vPuestos.LabelsAppearance.Visible = false;
                        vPuestos.Name = "(" + i.CL_PUESTO + ")" + i.NB_PUESTO;
                        continua = true;
                    }
                }
                if (continua)
                {
                    vPuestos.SeriesItems.Add(new CategorySeriesItem(0));
                    lstPuestos.Add(vPuestos);
                    continua = false;
                }
            }
            // lbPuestos.InnerHtml = vPuestosComp;

            var vPuestoCompetencia = plstCandidatoPuestos.Select(s => new { s.NO_VALOR_CANDIDATO, s.NB_CANDIDATO, s.NB_COMPETENCIA, s.CL_SOLICITUD }).Distinct().ToList();
            ColumnSeries vCandidato = new ColumnSeries();
            //if (vPuestoCompetencia.Count > 0)
            //    lbCandidatosCom.InnerText = "- (" + vPuestoCompetencia.FirstOrDefault().CL_SOLICITUD + ") " + vPuestoCompetencia.FirstOrDefault().NB_CANDIDATO;

            foreach (var item in vPuestoCompetencia)
            {
                vCandidato.SeriesItems.Add(new CategorySeriesItem(item.NO_VALOR_CANDIDATO));
                vCandidato.LabelsAppearance.Visible = false;
                vCandidato.Name = "(" + item.CL_SOLICITUD + ")" + item.NB_CANDIDATO;
                rhcCandidatoPuestos.PlotArea.XAxis.Items.Add(item.NB_COMPETENCIA);
                rhcCandidatoPuestos.PlotArea.XAxis.LabelsAppearance.RotationAngle = 270;
                rhcCandidatoPuestos.PlotArea.YAxis.MaxValue = 5;
            }
            rhcCandidatoPuestos.PlotArea.Series.Add(vCandidato);

            foreach (var it in lstPuestos)
            {
                rhcCandidatoPuestos.PlotArea.Series.Add(it);
            }
        }


        //public HtmlGenericControl CrearTabla(List<E_GRAFICA_PUESTO_CANDIDATOS> pValoresTabla)
        //{
        //    var vPuestoCompetencia = pValoresTabla.Select(s => new { s.NO_VALOR_CANDIDATO, s.NB_CANDIDATO, s.NB_COMPETENCIA, s.CL_COMPETENCIA, s.DS_COMPETENCIA, s.CL_SOLICITUD, s.CL_COLOR }).Distinct().ToList();

        //    HtmlGenericControl vCtrlTabla = new HtmlGenericControl("table");
        //    vCtrlTabla.Attributes.Add("style", "border-collapse: collapse; width:100%;");

        //    HtmlGenericControl vCtrlColumn = new HtmlGenericControl("thead");
        //    vCtrlColumn.Attributes.Add("style", "background: #E6E6E6;");

        //    HtmlGenericControl vCtrlRowEncabezado1 = new HtmlGenericControl("tr");
        //    vCtrlRowEncabezado1.Attributes.Add("style", "page-break-inside:avoid; page-break-after:auto;");

        //    //HtmlGenericControl vCtrlTh1 = new HtmlGenericControl("td");
        //    //vCtrlTh1.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:50px;");
        //    //vCtrlRowEncabezado1.Controls.Add(vCtrlTh1);

        //    HtmlGenericControl vCtrlTh2 = new HtmlGenericControl("td");
        //    vCtrlTh2.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:150px;");
        //    vCtrlTh2.InnerText = String.Format("{0}", "Competencia");
        //    vCtrlRowEncabezado1.Controls.Add(vCtrlTh2);

        //    HtmlGenericControl vCtrlTh3 = new HtmlGenericControl("td");
        //    vCtrlTh3.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:300px;");
        //    vCtrlTh3.InnerText = String.Format("{0}", "Descripción");
        //    vCtrlRowEncabezado1.Controls.Add(vCtrlTh3);

        //    foreach (var item in vListaPuestos)
        //    {
        //        string vNbPuesto = "";
        //        if (pValoresTabla.Where(w => w.ID_PUESTO == item).FirstOrDefault() != null)
        //        {
        //            vNbPuesto = pValoresTabla.Where(w => w.ID_PUESTO == item).FirstOrDefault().NB_PUESTO.ToString();
        //            HtmlGenericControl vCtrlThPuesto = new HtmlGenericControl("td");
        //            vCtrlThPuesto.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:100px;");
        //            vCtrlThPuesto.Attributes.Add("align", "center");
        //            HtmlGenericControl vCtrlDiv = new HtmlGenericControl("div");
        //            vCtrlDiv.Attributes.Add("style", "writing-mode:tb-rl; height: 250px;");
        //            vCtrlDiv.InnerText = String.Format("{0}", vNbPuesto);
        //            vCtrlThPuesto.Controls.Add(vCtrlDiv);
        //            vCtrlRowEncabezado1.Controls.Add(vCtrlThPuesto);
        //        }
        //    }

        //    vCtrlColumn.Controls.Add(vCtrlRowEncabezado1);

        //    vCtrlTabla.Controls.Add(vCtrlColumn);


        //    HtmlGenericControl vCtrlTbody = new HtmlGenericControl("tbody");

        //    foreach (var item in vPuestoCompetencia)
        //    {
        //        HtmlGenericControl vCtrlRow = new HtmlGenericControl("tr");
        //        vCtrlRow.Attributes.Add("style", "page-break-inside:avoid; page-break-after:auto;");


        //        //HtmlGenericControl vCtrlColor = new HtmlGenericControl("td");
        //        //vCtrlColor.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
        //        //vCtrlColor.InnerHtml = String.Format("{0}", "<div style=\"border: 1px solid gray; height:100%; border-radius: 5px; background:" + item.CL_COLOR + ";\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>");
        //        //vCtrlRow.Controls.Add(vCtrlColor);

        //        HtmlGenericControl vCtrlNbCompetencia = new HtmlGenericControl("td");
        //        vCtrlNbCompetencia.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
        //        vCtrlNbCompetencia.InnerText = String.Format("{0}", item.NB_COMPETENCIA);
        //        vCtrlRow.Controls.Add(vCtrlNbCompetencia);


        //        HtmlGenericControl vCtrlDsCompetencia = new HtmlGenericControl("td");
        //        vCtrlDsCompetencia.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
        //        vCtrlDsCompetencia.InnerText = String.Format("{0}", item.DS_COMPETENCIA);
        //        vCtrlRow.Controls.Add(vCtrlDsCompetencia);

        //        foreach (var vValor in vListaPuestos)
        //        {
        //            var vCumplimientoCandidato = pValoresTabla.Where(w => w.ID_PUESTO == vValor && w.CL_COMPETENCIA == item.CL_COMPETENCIA).FirstOrDefault();
        //            if (vCumplimientoCandidato != null)
        //            {
        //                HtmlGenericControl vCtrlPrCandidato = new HtmlGenericControl("td");
        //                vCtrlPrCandidato.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
        //                vCtrlPrCandidato.Attributes.Add("align", "right");
        //                vCtrlPrCandidato.InnerText = String.Format("{0}%", vCumplimientoCandidato.PR_CANDIDATO_PUESTO);
        //                vCtrlRow.Controls.Add(vCtrlPrCandidato);
        //            }
        //        }

        //        vCtrlTbody.Controls.Add(vCtrlRow);

        //    }
        //    vCtrlTabla.Controls.Add(vCtrlTbody);

        //    return vCtrlTabla;
        //}
    }
}