using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using Telerik.Web.UI;
using WebApp.Comunes;

namespace SIGE.WebApp.FYD
{
    public partial class AutorizaProgramaCapacitacion : System.Web.UI.Page
    {

        #region Variables
        public string cssModulo = String.Empty;
        private int? vslotCompetenciaColor
        {
            get { return (int?)ViewState["vs_slotCompetenciaColor"]; }
            set { ViewState["vs_slotCompetenciaColor"] = value; }
        }
        private Guid? vFlAutorizacion
        {
            get { return (Guid?)ViewState["vs_FlAutorizacion"]; }
            set { ViewState["vs_FlAutorizacion"] = value; }
        }
        private int? pID_PROGRAMA
        {
            get { return (int?)ViewState["vs_ID_PROGRAMA"]; }
            set { ViewState["vs_ID_PROGRAMA"] = value; }
        }
        private List<E_PROGRAMA_CAPACITACION> vProgramasCapacitacion
        {
            get { return (List<E_PROGRAMA_CAPACITACION>)ViewState["vs_ProgramasCapacitacion"]; }
            set { ViewState["vs_ProgramasCapacitacion"] = value; }
        }
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = (ContextoUsuario.oUsuario != null) ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO";

            string vClModulo = "FORMACION";
            string vModulo = Request.QueryString["m"];
            if (vModulo != null)
                vClModulo = vModulo;

            cssModulo = Utileria.ObtenerCssModulo(vClModulo);

            if (!IsPostBack)
            {
                vslotCompetenciaColor = 0;
                if (Request.QueryString["ID"] != null)
                {
                    pID_PROGRAMA = int.Parse((Request.QueryString["ID"]));
                    vFlAutorizacion = Guid.Parse((Request.QueryString["TOKEN"]));
                    ProgramaNegocio nPrograma = new ProgramaNegocio();

                    var programas = nPrograma.ObtieneKernelProgramaCapacitacion(pIdPrograma: pID_PROGRAMA);
                    vProgramasCapacitacion = cargarParseProgramasCapacitacion(programas);
                    pgridDetalle.DataSource = vProgramasCapacitacion;

                    if (vProgramasCapacitacion.Count > 0)
                    {
                        txtNbDepartamento.Text = vProgramasCapacitacion.ElementAt(0).NB_DEPARTAMENTO.ToString();
                        txtPeriodo.Text = vProgramasCapacitacion.ElementAt(0).CL_PROGRAMA.ToString();
                        txtTipoEvaluacion.Text = vProgramasCapacitacion.ElementAt(0).TIPO_EVALUACION.ToString();
                        radEditorNotas.Content = vProgramasCapacitacion.ElementAt(0).DS_NOTAS.ToString();
                    }
                }
            }
        }

        public List<E_PROGRAMA_CAPACITACION> cargarParseProgramasCapacitacion(List<SPE_OBTIENE_K_PROGRAMA_Result> lista)
        {
            List<E_PROGRAMA_CAPACITACION> programas = new List<E_PROGRAMA_CAPACITACION>();
            if (lista.Count > 0)
            {
                foreach (var item in lista)
                {
                    programas.Add(new E_PROGRAMA_CAPACITACION
                    {
                        ID_PROGRAMA_EMPLEADO_COMPETENCIA = item.ID_PROGRAMA_EMPLEADO_COMPETENCIA,
                        ID_PROGRAMA = item.ID_PROGRAMA,
                        ID_PROGRAMA_COMPETENCIA = item.ID_PROGRAMA_COMPETENCIA,
                        ID_PROGRAMA_EMPLEADO = item.ID_PROGRAMA_EMPLEADO,
                        CL_PRIORIDAD = item.CL_PRIORIDAD,
                        PR_RESULTADO = item.PR_RESULTADO,
                        CL_PROGRAMA = item.CL_PROGRAMA,
                        NB_PROGRAMA = item.NB_PROGRAMA,
                        CL_TIPO_PROGRAMA = item.CL_TIPO_PROGRAMA,
                        CL_ESTADO = item.CL_ESTADO,
                        CL_VERSION = item.CL_VERSION,
                        ID_DOCUMENTO_AUTORIZACION = item.ID_DOCUMENTO_AUTORIZACION,
                        NB_COMPETENCIA = item.NB_COMPETENCIA,
                        NB_CLASIFICACION_COMPETENCIA = item.NB_CLASIFICACION_COMPETENCIA,
                        CL_TIPO_COMPETENCIA = item.CL_TIPO_COMPETENCIA,
                        CL_COLOR = item.CL_COLOR,
                        NB_EVALUADO = item.NB_EVALUADO,
                        CL_EVALUADO = item.CL_EVALUADO,
                        NB_PUESTO = item.NB_PUESTO,
                        CL_PUESTO = item.CL_PUESTO,
                        NB_DEPARTAMENTO = item.NB_DEPARTAMENTO,
                        ID_COMPETENCIA = item.ID_COMPETENCIA,
                        ID_EMPLEADO = item.ID_EMPLEADO,
                        ID_PERIODO = item.ID_PERIODO,
                        CL_PERIODO = item.CL_PERIODO,
                        NB_PERIODO = item.NB_PERIODO,
                        DS_PERIODO = item.DS_PERIODO,
                        TIPO_EVALUACION = item.TIPO_EVALUACION,
                        DS_NOTAS = (item.DS_NOTAS != null) ? valdarDsNotas(item.DS_NOTAS.ToString()) : ""
                    });
                }
            }
            return programas;
        }

        public string valdarDsNotas(string vdsNotas)
        {
            E_NOTA pNota = null;
            if (!vdsNotas.Equals(""))
            {
                XElement vNotas = XElement.Parse(vdsNotas.ToString());
                if (ValidarRamaXml(vNotas, "NOTA"))
                {
                    pNota = vNotas.Elements("NOTA").Select(el => new E_NOTA
                     {
                         DS_NOTA = el.Attribute("DS_NOTA").Value,
                         FE_NOTA = (DateTime?)UtilXML.ValorAtributo(el.Attribute("FE_NOTA"), E_TIPO_DATO.DATETIME),
                     }).FirstOrDefault();
                }
                return pNota.DS_NOTA.ToString();
            }
            else
            {
                return "";
            }
        }

        public Boolean ValidarRamaXml(XElement parentEl, string elementsName)
        {
            var foundEl = parentEl.Element(elementsName);

            if (foundEl != null)
            {
                return true;
            }

            return false;
        }

        protected void ramDocumentosAutorizar_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
        }

        protected void pgridDetalle_NeedDataSource(object sender, Telerik.Web.UI.PivotGridNeedDataSourceEventArgs e)
        {
        }

        protected void pgridDetalle_CellDataBound(object sender, Telerik.Web.UI.PivotGridCellDataBoundEventArgs e)
        {
            if (e.Cell is PivotGridRowHeaderCell)
            {
                if (e.Cell.Controls.Count > 1)
                {
                    (e.Cell.Controls[0] as Button).Visible = false;
                }

                PivotGridRowHeaderCell cell = e.Cell as PivotGridRowHeaderCell;
                if (vslotCompetenciaColor == cell.Slot)
                {
                    switch (cell.Field.DataField.ToString())
                    {
                        case "NB_CLASIFICACION_COMPETENCIA":
                            cell.BorderColor = System.Drawing.ColorTranslator.FromHtml(vProgramasCapacitacion.ElementAt(cell.Slot).CL_COLOR.ToString());

                            cell.BorderWidth = 2;
                            break;
                        case "NB_COMPETENCIA":
                            cell.BorderColor = System.Drawing.ColorTranslator.FromHtml(vProgramasCapacitacion.ElementAt(cell.Slot).CL_COLOR.ToString());
                            cell.BorderWidth = 2;
                            break;

                        case "CL_TIPO_COMPETENCIA":
                            cell.BorderColor = System.Drawing.ColorTranslator.FromHtml(vProgramasCapacitacion.ElementAt(cell.Slot).CL_COLOR.ToString());
                            vslotCompetenciaColor++;
                            cell.BorderWidth = 2;
                            break;
                    }
                }
            }

            if (e.Cell is PivotGridColumnHeaderCell)
            {
                PivotGridColumnHeaderCell cell = e.Cell as PivotGridColumnHeaderCell;
                switch (cell.Field.DataField.ToString())
                {
                    case "NB_EVALUADO":
                        var objetoNecesidad = vProgramasCapacitacion.Where(x => x.NB_EVALUADO.Equals(cell.DataItem.ToString())).FirstOrDefault();
                        cell.Text = "<a>" + ((objetoNecesidad != null) ? objetoNecesidad.CL_EVALUADO.ToString() : "") + "</a> <br>" + cell.DataItem.ToString(); break;
                    case "NB_PUESTO":
                        var vobjetoNecesidadPuesto = vProgramasCapacitacion.Where(x => x.NB_PUESTO.Equals(cell.DataItem.ToString())).FirstOrDefault();
                        cell.Text = "<a>" + ((vobjetoNecesidadPuesto != null) ? vobjetoNecesidadPuesto.CL_PUESTO.ToString() : "") + "</a> <br>" + cell.DataItem.ToString(); break;
                    default: break;
                }
            }

            if (e.Cell is PivotGridDataCell)
            {
                PivotGridDataCell cell = e.Cell as PivotGridDataCell;
                if (cell.Field.DataField.ToString().Equals("PR_RESULTADO"))
                {
                    string Valor = cell.Text.ToString();

                    Decimal Devalor = Decimal.Parse(Valor);

                    if (Devalor == 100)
                    {
                        cell.CssClass = "divBlanco";
                        cell.HorizontalAlign = HorizontalAlign.Right;
                    }
                    else if (Devalor >= 70)
                    {
                        cell.CssClass = "divAmarrillo";
                        cell.BorderColor = System.Drawing.Color.White;
                        cell.BorderWidth = 1;
                        cell.HorizontalAlign = HorizontalAlign.Right;
                    }
                    else if (Devalor >= 0 & Devalor < 70)
                    {
                        cell.CssClass = "divRojo";
                        cell.BorderColor = System.Drawing.Color.White;
                        cell.BorderWidth = 1;
                        cell.HorizontalAlign = HorizontalAlign.Right;
                    }
                    else 
                    {
                        cell.CssClass = "divGrid";
                        cell.Text = "N/A";
                    }
                    
                }
            }
        }

        protected void pgridDetalle_ItemCommand(object sender, Telerik.Web.UI.PivotGridCommandEventArgs e)
        {
            if (e.CommandName == "ExpandCollapse")
            {
                PivotGridColumnHeaderCell cell = e.Item.Cells[0] as Telerik.Web.UI.PivotGridColumnHeaderCell;

                if (cell.Expanded)
                {
                    cell.CssClass = "Contraido";
                }
                else
                {
                    cell.CssClass = "Expandido";
                }
            }
        }

        public List<E_NECESIDADES_CAPACITACION> parseNecesidadesCapacitacion(List<SPE_OBTIENE_NECESIDADES_CAPACITACION_Result> lista)
        {
            List<E_NECESIDADES_CAPACITACION> vNecesidadesCapacitaciones = new List<E_NECESIDADES_CAPACITACION>();

            foreach (var item in lista)
            {
                vNecesidadesCapacitaciones.Add(new
                E_NECESIDADES_CAPACITACION
                {
                    ID_PERIODO = item.ID_PERIODO,
                    CL_TIPO_COMPETENCIA = item.CL_TIPO_COMPETENCIA,
                    NB_TIPO_COMPETENCIA = item.NB_TIPO_COMPETENCIA,
                    CL_CLASIFICACION = item.CL_CLASIFICACION,
                    NB_CLASIFICACION_COMPETENCIA = item.NB_CLASIFICACION_COMPETENCIA,
                    CL_COLOR = item.CL_COLOR,
                    ID_COMPETENCIA = item.ID_COMPETENCIA,
                    NB_COMPETENCIA = item.NB_COMPETENCIA,
                    ID_EMPLEADO = item.ID_EMPLEADO,
                    CL_EVALUADO = item.CL_EVALUADO,
                    NB_EVALUADO = item.NB_EVALUADO,
                    ID_PUESTO = item.ID_PUESTO,
                    CL_PUESTO = item.CL_PUESTO,
                    NB_PUESTO = item.NB_PUESTO,
                    ID_DEPARTAMENTO = item.ID_DEPARTAMENTO,
                    CL_DEPARTAMENTO = item.CL_DEPARTAMENTO,
                    NB_DEPARTAMENTO = item.NB_DEPARTAMENTO,
                    PR_RESULTADO = item.PR_RESULTADO,
                    NB_PRIORIDAD = item.NB_PRIORIDAD

                });
            }

            return vNecesidadesCapacitaciones;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            DocumentoAutorizarNegocio nDocumentoAutorizar = new DocumentoAutorizarNegocio();

            string autoriza = (chkActivo.Checked) ? "Autorizado" : "No autorizado";
            XElement vXelementNotas = null;

            var vXelementNota =
            new XElement("NOTA",
            new XAttribute("FE_NOTA", DateTime.Now.ToString()),
            new XAttribute("DS_NOTA", radObservaciones.Content.ToString())
                 );
            vXelementNotas = new XElement("NOTAS", vXelementNota);

            E_RESULTADO vResultado = nDocumentoAutorizar.ActualizaEstatusDocumentoAutorizacion(vFlAutorizacion, autoriza, vXelementNotas.ToString(), null, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
            string myUrl = ResolveUrl("~/Logon.aspx");
            Response.Redirect(ContextoUsuario.nbHost + myUrl);
        }
    }
}