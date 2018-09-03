using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using WebApp.Comunes;

namespace SIGE.WebApp.EO
{
    public partial class VentanaMetasperiodo : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdRol;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private XElement SeleccionMetasEvaluado { get; set; }
        private XElement RESULTADOS { get; set; }
        decimal sum = 0;
        decimal vSumaEvaluadas = 0;

        private List<E_METAS_EVALUADO> vListaMetasEvaluado
        {
            get { return (List<E_METAS_EVALUADO>)ViewState["vs_vListaMetasEvaluado"]; }
            set { ViewState["vs_vListaMetasEvaluado"] = value; }
        }

        private List<E_META> vListaMetas
        {
            get { return (List<E_META>)ViewState["vs_vListaMetas"]; }
            set { ViewState["vs_vListaMetas"] = value; }
        }

        private int vIdPeriodoDesempeno
        {
            get { return (int)ViewState["vsIdPeriodoDesempeno"]; }
            set { ViewState["vsIdPeriodoDesempeno"] = value; }
        }

        private int vIdEmpleado
        {
            get { return (int)ViewState["vsvIdEmpleado"]; }
            set { ViewState["vsvIdEmpleado"] = value; }
        }

        public int vIdPeriodo
        {
            get { return (int)ViewState["vsIdPeriodo"]; }
            set { ViewState["vsIdPeriodo"] = value; }
        }

        private int? vEstatusDisenoMetas
        {
            get { return (int?)ViewState["vEstatusDisenoMetas"]; }
            set { ViewState["vEstatusDisenoMetas"] = value; }
        }

        public string vClOrigenPeriodo
        {
            get { return (string)ViewState["vs_vClOrigenPeriodo"]; }
            set { ViewState["vs_vClOrigenPeriodo"] = value; }
        }

        public int? vNoReplica
        {
            get { return (int?)ViewState["vs_vNoReplica"]; }
            set { ViewState["vs_vNoReplica"] = value; }
        }

        #endregion

        #region Métodos

        //public string validarDsNotas(string vdsNotas)
        //{
        //    E_NOTAS pNota = null;
        //    if (vdsNotas != null)
        //    {
        //        XElement vNotas = XElement.Parse(vdsNotas.ToString());
        //        if (ValidarRamaXml(vNotas, "NOTA"))
        //        {
        //            pNota = vNotas.Elements("NOTA").Select(el => new E_NOTAS
        //            {
        //                DS_NOTA = UtilXML.ValorAtributo<string>(el.Attribute("DS_NOTA")),
        //                FE_NOTA = (DateTime?)UtilXML.ValorAtributo(el.Attribute("FE_NOTA"), E_TIPO_DATO.DATETIME),
        //            }).FirstOrDefault();
        //        }

        //        if (pNota != null)
        //        {
        //            if (pNota.DS_NOTA != null)
        //            {
        //                return pNota.DS_NOTA.ToString();
        //            }
        //            else return "";
        //        }
        //        else
        //            return "";
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}

        //public Boolean ValidarRamaXml(XElement parentEl, string elementsName)
        //{
        //    var foundEl = parentEl.Element(elementsName);

        //    if (foundEl != null)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //private void CargarDatosContexto()
        //{
        //    PeriodoDesempenoNegocio periodo = new PeriodoDesempenoNegocio();
        //    var oPeriodo = periodo.ObtienePeriodoDesempenoContexto(vIdPeriodo, null);
        //    if (oPeriodo != null)
        //    {
        //        txtClPeriodo.InnerText = oPeriodo.CL_PERIODO;
        //        txtPeriodos.InnerText = oPeriodo.DS_PERIODO;
        //        txtFechas.InnerText = oPeriodo.FE_INICIO.ToString("d") + " a " + oPeriodo.FE_TERMINO.Value.ToShortDateString();
        //        txtTipoMetas.InnerText = oPeriodo.CL_TIPO_PERIODO;
        //        txtTipoCapturista.InnerText = Utileria.LetrasCapitales(oPeriodo.CL_TIPO_CAPTURISTA);
        //        txtTipoBono.InnerText = oPeriodo.CL_TIPO_BONO;
        //        txtTipoPeriodo.InnerText = oPeriodo.CL_ORIGEN_CUESTIONARIO;

        //        if (oPeriodo.DS_NOTAS != null)
        //        {
        //            XElement vNotas = XElement.Parse(oPeriodo.DS_NOTAS);
        //            if (vNotas != null)
        //            {
        //                txtNotas.InnerHtml = validarDsNotas(vNotas.ToString());
        //            }
        //        }
        //    }
        //}


        private void CargarDatos()
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();

            var vPeriodoDesempeno = nPeriodo.ObtienePeriodosDesempeno(pIdPeriodo: vIdPeriodo).FirstOrDefault();

            vClOrigenPeriodo = vPeriodoDesempeno.CL_ORIGEN_CUESTIONARIO;
            vNoReplica = vPeriodoDesempeno.NO_REPLICA;



            vIdPeriodoDesempeno = vPeriodoDesempeno.ID_PERIODO_DESEMPENO;
            // CargarDatosContexto();

            if (vPeriodoDesempeno.CL_TIPO_METAS == "DESCRIPTIVO")
            {
                grdMetas.MasterTableView.DetailTables[0].Columns[0].Visible = true;
            }
            else if (vPeriodoDesempeno.CL_TIPO_METAS == "CERO")
            {
                grdMetas.MasterTableView.DetailTables[0].Columns.FindByUniqueName("NB_INDICADOR").Visible = false;
            }


        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["PeriodoId"] != null)
                {
                    vIdPeriodo = int.Parse(Request.QueryString["PeriodoId"]);
                    CargarDatos();
                }
                else
                {
                    vIdPeriodo = 0;
                }
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;
        }

        protected void grdMetas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            grdMetas.DataSource = nPeriodo.ObtieneEvaluados(pIdPeriodo: vIdPeriodo, pIdRol: vIdRol);
        }

        protected void grdMetas_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem vDataItem = (GridDataItem)e.DetailTableView.ParentItem;
            vListaMetas = new List<E_META>();
            if (e.DetailTableView.Name == "gtvMetas")
            {
                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();

                vIdEmpleado = int.Parse(vDataItem.GetDataKeyValue("ID_EVALUADO").ToString());
                vSumaEvaluadas = 0;
                vListaMetas = nPeriodo.ObtieneMetas(vIdPeriodo, vIdEmpleado);

                e.DetailTableView.DataSource = vListaMetas;

                foreach (E_META Item in vListaMetas)
                {
                    int vIdEval_Metas = int.Parse(Item.ID_EVALUADO.ToString());
                    bool vfgEvalua = bool.Parse(Item.FG_EVALUAR.ToString());
                    if (vIdEval_Metas == vIdEmpleado)
                    {
                        if (vfgEvalua == true)
                        {
                            vSumaEvaluadas += decimal.Parse(Item.PR_META.ToString());
                        }
                    }
                }
            }
        }

        protected void grdMetas_ItemCommand(object sender, GridCommandEventArgs e)
        {
            //if (e.CommandName == "Delete")
            //{
            //    PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            //    XElement vXmlEvaluados = new XElement("EVALUADORES");

            //    GridDataItem item = e.Item as GridDataItem;
            //    vXmlEvaluados.Add(new XElement("EVALUADOR", new XAttribute("ID_EVALUADO", item.GetDataKeyValue("ID_EVALUADO").ToString()),
            //                                                new XAttribute("ID_EVALUADOR", item.GetDataKeyValue("ID_EVALUADOR").ToString())));

            //    E_RESULTADO vResultado = nPeriodo.EliminaEvaluadorEvaluado(vIdPeriodo, vXmlEvaluados, vClUsuario, vNbPrograma);
            //    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            //    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "recargarEvaluados()");

            //}

            if (e.CommandName == RadGrid.ExpandCollapseCommandName)
            {
                foreach (GridItem item in e.Item.OwnerTableView.Items)
                {
                    if (item.Expanded && item != e.Item)
                    {
                        item.Expanded = false;
                    }
                }
            }
        }

        protected void grdMetas_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridFooterItem & e.Item.OwnerTableView.Name.Equals("gtvMetas"))
            {
                GridFooterItem footer = (GridFooterItem)e.Item;
                footer["PR_META"].Text = vSumaEvaluadas.ToString();
            }

        }
    }
}