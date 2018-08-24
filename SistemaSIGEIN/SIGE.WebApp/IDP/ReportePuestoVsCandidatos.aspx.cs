using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.IntegracionDePersonal;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class ReportePuestoVsCandidatos : System.Web.UI.Page
    {
        #region Variables

        public Guid vIdPuestoVsCandidatos
        {
            get { return (Guid)ViewState["vs_vIdPersonaVsCandidatos"]; }
            set { ViewState["vs_vIdPersonaVsCandidatos"] = value; }
        }

        public int vIdPuesto
        {
            get { return (int)ViewState["vs_vIdPuesto"]; }
            set { ViewState["vs_vIdPuesto"] = value; }
        }

        public bool vFgConsultaParcial
        {
            get { return (bool)ViewState["vs_vFgConsultaParcial"]; }
            set { ViewState["vs_vFgConsultaParcial"] = value; }
        }

        public bool vFgCalificacionCero
        {
            get { return (bool)ViewState["vs_vFgCalificacionCero"]; }
            set { ViewState["vs_vFgCalificacionCero"] = value; }
        }

        public List<int> vListaCandidatos
        {
            get { return (List<int>)ViewState["vs_vListaCandidatos"]; }
            set { ViewState["vs_vListaCandidatos"] = value; }
        }

        public List<E_GRAFICA_PUESTO_CANDIDATOS> vPuestoCandidatos
        {
            get { return (List<E_GRAFICA_PUESTO_CANDIDATOS>)ViewState["vs_vPuestoCandidatos"]; }
            set { ViewState["vs_vPuestoCandidatos"] = value; }
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

        public string vs_NB_PUESTO
        {
            get { return (string)ViewState["vs_NB_PUESTO"]; }
            set { ViewState["vs_NB_PUESTO"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = (ContextoUsuario.oUsuario != null ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO");
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                vIdPuestoVsCandidatos = Guid.Empty;
                vFgConsultaParcial = ContextoApp.IDP.ConfiguracionIntegracion.FgConsultasParciales;
                vFgCalificacionCero = ContextoApp.IDP.ConfiguracionIntegracion.FgIgnorarCompetencias;
                if (Request.Params["vIdPuestoVsCandidatos"] != null)
                {
                    vIdPuestoVsCandidatos = Guid.Parse(Request.Params["vIdPuestoVsCandidatos"].ToString());
                }
                else
                    vIdPuestoVsCandidatos = Guid.NewGuid();

                if (ContextoConsultasComparativas.oPuestoVsCandidatos == null)
                {
                    ContextoConsultasComparativas.oPuestoVsCandidatos = new List<E_PUESTO_VS_CANDIDATOS>();
                }

                if (Request.Params["IdPuesto"] != null)
                {
                    vIdPuesto = int.Parse(Request.Params["IdPuesto"]);
                }
                CargarPuestoCandidatos();
                divTabla.Controls.Add(CrearTabla(vPuestoCandidatos));
            }
        }

        protected void CargarPuestoCandidatos()
        {

            vListaCandidatos = new List<int>();

            if (vIdPuestoVsCandidatos != Guid.Empty)
            {
                E_PUESTO_VS_CANDIDATOS oPuestoCandidatos = ContextoConsultasComparativas.oPuestoVsCandidatos.Where(t => t.vIdPuestoVsCandidatos == vIdPuestoVsCandidatos).FirstOrDefault();
                vListaCandidatos = oPuestoCandidatos.vListaCandidatos;
            }

            var vXelements = vListaCandidatos.Select(x =>
                                           new XElement("CANDIDATO",
                                           new XAttribute("ID_CANDIDATO", x)));

            XElement SELECCIONADOS =
            new XElement("CANDIDATOS", vXelements
                );

            ConsultasComparativasNegocio nComparativas = new ConsultasComparativasNegocio();
            vPuestoCandidatos = nComparativas.ObtienePuestoCandidatos(pID_PUESTO: vIdPuesto, pXML_CANDIDATOS: SELECCIONADOS.ToString(), pFgConsultaParcial: vFgConsultaParcial, pFgCalificacionCero: vFgCalificacionCero).Select(s => new E_GRAFICA_PUESTO_CANDIDATOS
            {
                NB_COMPETENCIA = s.NB_COMPETENCIA,
                NO_VALOR_NIVEL = s.NO_VALOR_NIVEL,
                CL_CANDIDATO = s.CL_CANDIDATO,
                NB_CANDIDATO = s.NB_CANDIDATO,
                NO_VALOR_CANDIDATO = s.NO_VALOR_CANDIDATO,
                ID_CANDIDATO = s.ID_CANDIDATO,
                DS_COMPETENCIA = s.DS_COMPETENCIA,
                ID_BATERIA = s.ID_BATERIA,
                CL_PUESTO = s.CL_PUESTO,
                NB_PUESTO = s.NB_PUESTO,
                PR_CANDIDATO = s.PR_CANDIDATO,
                CL_COLOR = s.CL_COLOR,
                PR_TRUNCADO = CalculaPorcentaje(s.PR_CANDIDATO),
                CL_COMPETENCIA = s.CL_COMPETENCIA,
                CL_SOLICITUD = s.CL_SOLICITUD

            }).OrderBy(s => s.CL_COMPETENCIA).ToList();

            if (vPuestoCandidatos.Count > 0)
            {
                vs_NB_PUESTO = vPuestoCandidatos.FirstOrDefault().NB_PUESTO;
            }

            lbCandidatos.Controls.Add(GenerarTablaCandidatos(vPuestoCandidatos));

            var vPuestoCompetencia = vPuestoCandidatos.Select(s => new { s.NO_VALOR_NIVEL, s.NB_PUESTO, s.NB_COMPETENCIA, s.CL_PUESTO }).Distinct().ToList();
            if (vPuestoCompetencia.Count > 0)
            lbPuestoCom.InnerText = "- (" + vPuestoCompetencia.FirstOrDefault().CL_PUESTO + ") " + vPuestoCompetencia.FirstOrDefault().NB_PUESTO;

            //GraficaPuestoCandidatos(vPuestoCandidatos);

            //txtPromedio.Enabled = false;
            //var promedio = vPuestoCandidatos.Average(s => s.PR_TRUNCADO);
            ////if (Convert.ToDouble(promedio) <= 100 && Convert.ToDouble(promedio) >= 0) 
            //     txtPromedio.Value = Convert.ToDouble(promedio);
            //else if(Convert.ToDouble(promedio) > 100)
            //    txtPromedio.Value = 100;
            //else if (Convert.ToDouble(promedio) < 0)
            //    txtPromedio.Value = 0;

            //List<E_PROMEDIO> vlstPromedios = new List<E_PROMEDIO>();
            //foreach (var item in vListaCandidatos)
            //{
            //    List<E_PROMEDIO> vlist = new List<E_PROMEDIO>();
            //    foreach (var i in vPuestoCandidatos)
            //    {
            //        if (item == i.ID_CANDIDATO && i.NO_VALOR_NIVEL != 0)
            //        {
            //            vlist.Add(new E_PROMEDIO { NB_PUESTO = i.NB_CANDIDATO, PORCENTAJE = i.PR_TRUNCADO, PORCENTAJE_NO_TRUNCADO = i.PR_CANDIDATO });
            //        }
            //    }
            //    vlstPromedios.Add(new E_PROMEDIO
            //    {
            //        NB_PUESTO = vlist.Select(s => s.NB_PUESTO).FirstOrDefault(),
            //        PROMEDIO = vlist.Average(s => s.PORCENTAJE),
            //        FG_SUPERA_130 = vlist.Average(s => s.PORCENTAJE_NO_TRUNCADO) >= 130 ? true : false,
            //        PROMEDIO_TXT = vlist.Average(s => s.PORCENTAJE_NO_TRUNCADO) >= 130 ? Decimal.Round(vlist.Average(s => s.PORCENTAJE) ?? 1, 2).ToString() + "(*)" : Decimal.Round(vlist.Average(s => s.PORCENTAJE) ?? 1, 2).ToString()
            //    });
            //}
            //rgdPromedios.DataSource = vlstPromedios.Where(w => w.NB_PUESTO != null);
            //rgdPromedios.DataBind();
            //rgdPromedios.Rebind();

            //for (int i = 0; i < vlstPromedios.Count; i++)
            //{
            //    if (vlstPromedios[i].FG_SUPERA_130 == true)
            //    {
            //        divMensajeMayor130.Visible = true;
            //        i = vlstPromedios.Count;
            //    }
            //}
        }

        protected decimal? CalculaPorcentaje(decimal? pPorcentaje)
        {
            decimal? vPorcentaje = 0;
            if (pPorcentaje > 100)
                vPorcentaje = 100;
            else vPorcentaje = pPorcentaje;
            return vPorcentaje;
        }

        protected HtmlGenericControl GenerarTablaCandidatos(List<E_GRAFICA_PUESTO_CANDIDATOS> plstPuestoCandidatos)
        {
            HtmlGenericControl vTabla = new HtmlGenericControl("table");
            vTabla.Attributes.Add("style", "border-collapse: collapse;");

            HtmlGenericControl vCtrlTbody = new HtmlGenericControl("tbody");

            foreach (var item in vListaCandidatos)
            {
                string vPuestosComp = "";

                if (plstPuestoCandidatos.Where(w => w.ID_CANDIDATO == item).FirstOrDefault() != null)
                    if (plstPuestoCandidatos.Where(w => w.ID_CANDIDATO == item).FirstOrDefault().CL_SOLICITUD != null)
                        vPuestosComp = "(" + plstPuestoCandidatos.Where(w => w.ID_CANDIDATO == item).FirstOrDefault().CL_SOLICITUD + ") " + plstPuestoCandidatos.Where(w => w.ID_CANDIDATO == item).FirstOrDefault().NB_CANDIDATO;
                    else
                        vPuestosComp = "( ) " + plstPuestoCandidatos.Where(w => w.ID_CANDIDATO == item).FirstOrDefault().NB_CANDIDATO;


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

        //protected void GraficaPuestoCandidatos(List<E_GRAFICA_PUESTO_CANDIDATOS> plstPuestoCandidatos)
        //{
        //    List<ColumnSeries> lstCandidatos = new List<ColumnSeries>();

        //    bool continua = false;
        //    rhcPuestoCandidatos.PlotArea.Series.Clear();
        //    string vCandidatosComp = "";

        //    foreach (var item in vListaCandidatos)
        //    {
        //        ColumnSeries vCandidatos = new ColumnSeries();

        //        if (plstPuestoCandidatos.Where(w => w.ID_CANDIDATO == item).FirstOrDefault() != null)
        //            if (plstPuestoCandidatos.Where(w => w.ID_CANDIDATO == item).FirstOrDefault().CL_SOLICITUD != null)
        //                vCandidatosComp = vCandidatosComp + "- (" + plstPuestoCandidatos.Where(w => w.ID_CANDIDATO == item).FirstOrDefault().CL_SOLICITUD + ")" + plstPuestoCandidatos.Where(w => w.ID_CANDIDATO == item).FirstOrDefault().NB_CANDIDATO + "<br>";
        //            else
        //                vCandidatosComp = vCandidatosComp + "- ()" + plstPuestoCandidatos.Where(w => w.ID_CANDIDATO == item).FirstOrDefault().NB_CANDIDATO + "<br>";

        //        foreach (var i in plstPuestoCandidatos)
        //        {
        //            if (item == i.ID_CANDIDATO)
        //            {
        //                vCandidatos.SeriesItems.Add(new CategorySeriesItem(i.NO_VALOR_CANDIDATO));
        //                vCandidatos.LabelsAppearance.Visible = false;

        //                vCandidatos.Name = "(" + i.CL_SOLICITUD + ")" + i.NB_CANDIDATO;
        //                continua = true;
        //            }
        //        }
        //        if (continua)
        //        {
        //            vCandidatos.SeriesItems.Add(new CategorySeriesItem(0));
        //            lstCandidatos.Add(vCandidatos);
        //            //rhcPuestoCandidatos.PlotArea.Series.Add(vCandidatos);
        //            continua = false;
        //        }

        //    }
        //    lbCandidatos.InnerHtml = vCandidatosComp;

        //    var vPuestoCompetencia = plstPuestoCandidatos.Select(s => new { s.NO_VALOR_NIVEL, s.NB_PUESTO, s.NB_COMPETENCIA, s.CL_PUESTO }).Distinct().ToList();
        //    ColumnSeries vPuesto = new ColumnSeries();
        //    lbPuestoCom.InnerText = "- (" + vPuestoCompetencia.FirstOrDefault().CL_PUESTO + ") " + vPuestoCompetencia.FirstOrDefault().NB_PUESTO;


        //    foreach (var item in vPuestoCompetencia)
        //    {
        //        vPuesto.SeriesItems.Add(new CategorySeriesItem(item.NO_VALOR_NIVEL));
        //        vPuesto.LabelsAppearance.Visible = false;
        //        vPuesto.Name = "(" + item.CL_PUESTO + ") " + item.NB_PUESTO;
        //        rhcPuestoCandidatos.PlotArea.XAxis.Items.Add(item.NB_COMPETENCIA);
        //        rhcPuestoCandidatos.PlotArea.XAxis.LabelsAppearance.RotationAngle = 270;
        //        rhcPuestoCandidatos.PlotArea.YAxis.MaxValue = 6;
        //    }
        //    rhcPuestoCandidatos.PlotArea.Series.Add(vPuesto);

        //    foreach (var it in lstCandidatos)
        //    {
        //        rhcPuestoCandidatos.PlotArea.Series.Add(it);
        //    }
        //}


        public HtmlGenericControl CrearTabla(List<E_GRAFICA_PUESTO_CANDIDATOS> pValoresTabla)
        {
            var vPuestoCompetencia = pValoresTabla.Select(s => new { s.NO_VALOR_NIVEL, s.NB_PUESTO, s.CL_COMPETENCIA, s.NB_COMPETENCIA, s.DS_COMPETENCIA, s.CL_COLOR, s.CL_PUESTO }).Distinct().ToList();

            HtmlGenericControl vCtrlTabla = new HtmlGenericControl("table");
            vCtrlTabla.Attributes.Add("style", "border-collapse: collapse; width:100%;");

            HtmlGenericControl vCtrlColumn = new HtmlGenericControl("thead");
            vCtrlColumn.Attributes.Add("style", "background: #E6E6E6;");

            HtmlGenericControl vCtrlRowEncabezado1 = new HtmlGenericControl("tr");
            vCtrlRowEncabezado1.Attributes.Add("style", "page-break-inside:avoid; page-break-after:auto;");

            HtmlGenericControl vCtrlTh2 = new HtmlGenericControl("td");
            vCtrlTh2.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:150px;");
            vCtrlTh2.InnerText = String.Format("{0}", "Competencia");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh2);

            HtmlGenericControl vCtrlTh3 = new HtmlGenericControl("td");
            vCtrlTh3.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:300px;");
            vCtrlTh3.InnerText = String.Format("{0}", "Descripción");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh3);

            foreach (var item in vListaCandidatos)
            {
                string vNbCandidato = "";
                if (pValoresTabla.Where(w => w.ID_CANDIDATO == item).FirstOrDefault() != null)
                {
                    vNbCandidato = pValoresTabla.Where(w => w.ID_CANDIDATO == item).FirstOrDefault().NB_CANDIDATO.ToString();
                    HtmlGenericControl vCtrlThCandidato = new HtmlGenericControl("th");
                    vCtrlThCandidato.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:100px;");
                    vCtrlThCandidato.Attributes.Add("align", "center");
                    HtmlGenericControl vCtrlDiv = new HtmlGenericControl("div");
                    vCtrlDiv.Attributes.Add("style", "writing-mode:tb-rl; height: 250px;");
                    vCtrlDiv.InnerText = String.Format("{0}", vNbCandidato);
                    vCtrlThCandidato.Controls.Add(vCtrlDiv);
                    vCtrlRowEncabezado1.Controls.Add(vCtrlThCandidato);
                }
            }


            vCtrlColumn.Controls.Add(vCtrlRowEncabezado1);

            vCtrlTabla.Controls.Add(vCtrlColumn);

            HtmlGenericControl vCtrlTbody = new HtmlGenericControl("tbody");

            foreach (var item in vPuestoCompetencia)
            {
                HtmlGenericControl vCtrlRow = new HtmlGenericControl("tr");
                vCtrlRow.Attributes.Add("style", "page-break-inside:avoid; page-break-after:auto;");

                HtmlGenericControl vCtrlNbCompetencia = new HtmlGenericControl("td");
                vCtrlNbCompetencia.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                vCtrlNbCompetencia.InnerText = String.Format("{0}", item.NB_COMPETENCIA);
                vCtrlRow.Controls.Add(vCtrlNbCompetencia);


                HtmlGenericControl vCtrlDsCompetencia = new HtmlGenericControl("td");
                vCtrlDsCompetencia.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                vCtrlDsCompetencia.InnerText = String.Format("{0}", item.DS_COMPETENCIA);
                vCtrlRow.Controls.Add(vCtrlDsCompetencia);

                foreach (var vValor in vListaCandidatos)
                {
                    var vCumplimientoCandidato = pValoresTabla.Where(w => w.ID_CANDIDATO == vValor && w.CL_COMPETENCIA == item.CL_COMPETENCIA).FirstOrDefault();
                    if (vCumplimientoCandidato != null)
                    {
                        HtmlGenericControl vCtrlPrCandidato = new HtmlGenericControl("td");
                        vCtrlPrCandidato.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                        vCtrlPrCandidato.Attributes.Add("align", "right");
                        vCtrlPrCandidato.InnerText = String.Format("{0}%", vCumplimientoCandidato.PR_CANDIDATO);
                        vCtrlRow.Controls.Add(vCtrlPrCandidato);
                    }
                }
                vCtrlTbody.Controls.Add(vCtrlRow);

            }
            vCtrlTabla.Controls.Add(vCtrlTbody);

            return vCtrlTabla;
        }


        #region Clases

        [Serializable]
        public class E_PROMEDIO_CANDIDATO
        {
            public string Persona { get; set; }
            public string Compatibilidad { get; set; }
        }

        [Serializable]
        class E_GRAPHIC_PUESTO_DATOS
        {
            public string Candidato { get; set; }
            public List<E_GRAPHIC_CANDIDATOS> Datos { get; set; }
        }

        [Serializable]
        class E_GRAPHIC_CANDIDATOS
        {
            public string Candidato { get; set; }
            public string Competencia { get; set; }
            public string Descripcion { get; set; }
            public Nullable<decimal> valor { get; set; }
            public Nullable<decimal> Promedio { get; set; }
        }

        [Serializable]
        class E_GRAPHIC_PUESTO_COMPETENCIA
        {
            public string Competencia { get; set; }
            public Nullable<decimal> valor { get; set; }
        }

        class JSON_VALUES
        {
            public string svcImage { get; set; }
        }


        #endregion
    }
}