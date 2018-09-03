using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Web.UI;
using SIGE.Entidades;
using SIGE.Negocio.FormacionDesarrollo;
using System.Xml.Linq;
using Telerik.Web.UI;

using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using SIGE.WebApp.Comunes;
using WebApp.Comunes;
using SIGE.Negocio.Utilerias;
using SIGE.Negocio.EvaluacionOrganizacional;

namespace SIGE.WebApp.EO.Cuestionarios
{
    public partial class Evaluar : System.Web.UI.Page
    {

        #region Variables

        public string cssModulo = String.Empty;
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int pIdEvaluador
        {
            get { return (int)ViewState["vs_pIdEvaluador"]; }
            set { ViewState["vs_pIdEvaluador"] = value; }
        }

        private int pIdIndex
        {
            get { return (int)ViewState["vs_pIdIndex"]; }
            set { ViewState["vs_pIdIndex"] = value; }
        }

        private int pIdEvaluadoEvaluador
        {
            get { return (int)ViewState["vs_pIdEvaluadoEvaluador"]; }
            set { ViewState["vs_pIdEvaluadoEvaluador"] = value; }
        }

        private int vIdEvaluado
        {
            get { return (int)ViewState["vs_id_evaluado"]; }
            set { ViewState["vs_id_evaluado"] = value; }
        }

        private int vIdPeriodo
        {
            get { return (int)ViewState["vs_id_periodo"]; }
            set { ViewState["vs_id_periodo"] = value; }
        }

        private List<E_PREGUNTAS> pListPeguntas
        {
            get { return (List<E_PREGUNTAS>)ViewState["vs_pListPeguntas"]; }
            set { ViewState["vs_pListPeguntas"] = value; }
        }

        private int vIdEvaluadoEvaluador
        {
            get { return (int)Session["ss_eval_id_evaluado_evaluador"]; }
            set { Session["ss_eval_id_evaluado_evaluador"] = value; }
        }

        private string vXmlCamposAdicionales
        {
            get { return (string)Session["ss_eval_xml_campos_adicionales"]; }
            set { Session["ss_eval_xml_campos_adicionales"] = value; }
        }

        private string vXmlCamposAdicionalesCatalogos
        {
            get { return (string)Session["ss_eval_xml_campos_adicionales_catalogos"]; }
            set { Session["ss_eval_xml_campos_adicionales_catalogos"] = value; }
        }

        private List<E_CLASIFICACION> pListClasificacion
        {
            get { return (List<E_CLASIFICACION>)ViewState["vs_pListClasificacion"]; }
            set { ViewState["vs_pListClasificacion"] = value; }
        }

        PeriodoNegocio negocio = new PeriodoNegocio();

        private int pOrden
        {
            get { return (int)ViewState["vs_pOrden"]; }
            set { ViewState["vs_pOrden"] = value; }
        }

        private string pClas
        {
            get { return (string)ViewState["vs_pClas"]; }
            set { ViewState["vs_pClas"] = value; }
        }

        private string vClColor
        {
            get { return (string)ViewState["vs_v_cl_color"]; }
            set { ViewState["vs_v_cl_color"] = value; }
        }

        private bool vTieneCuestionarios
        {
            get { return (bool)ViewState["vs_v_tieneCuestionarios"]; }
            set { ViewState["vs_v_tieneCuestionarios"] = value; }
        }

        private bool? vValidaCuestionario
        {
            get {return (bool?)ViewState["vs_vValidaCuestionario"]; }
            set { ViewState["vs_vValidaCuestionario"] = value; }
        }

        #endregion

        #region Funciones

        private void AsignarRespuestas()
        {
            int vIdCuestionarioPregunta;

            foreach (GridDataItem item in dgvCompetencias.MasterTableView.Items)
            {
                vIdCuestionarioPregunta = int.Parse(item.GetDataKeyValue("ID_CUESTIONARIO_PREGUNTA").ToString());

                var vPrgunta = pListPeguntas.Where(t => t.ID_CUESTIONARIO_PREGUNTA == vIdCuestionarioPregunta).FirstOrDefault();

                if (vPrgunta != null)
                {

                    //if ((item.FindControl("rbNivel0") as RadButton).Checked)
                    //{
                    //    vPrgunta.FG_VALOR0 = true;
                    //    vPrgunta.NO_VALOR_RESPUESTA = 0;
                    //}

                    if ((item.FindControl("rbNivel1") as RadButton).Checked)
                    {
                        vPrgunta.FG_VALOR1 = true;
                        vPrgunta.FG_VALOR2 = false;
                        vPrgunta.FG_VALOR3 = false;
                        vPrgunta.FG_VALOR4 = false;
                        vPrgunta.FG_VALOR5 = false;
                        vPrgunta.NO_VALOR_RESPUESTA = 1;
                    }

                    if ((item.FindControl("rbNivel2") as RadButton).Checked)
                    {
                        vPrgunta.FG_VALOR1 = false;
                        vPrgunta.FG_VALOR2 = true;
                        vPrgunta.FG_VALOR3 = false;
                        vPrgunta.FG_VALOR4 = false;
                        vPrgunta.FG_VALOR5 = false;
                        vPrgunta.NO_VALOR_RESPUESTA = 2;
                    }

                    if ((item.FindControl("rbNivel3") as RadButton).Checked)
                    {
                        vPrgunta.FG_VALOR1 = false;
                        vPrgunta.FG_VALOR2 = false;
                        vPrgunta.FG_VALOR3 = true;
                        vPrgunta.FG_VALOR4 = false;
                        vPrgunta.FG_VALOR5 = false;
                        vPrgunta.NO_VALOR_RESPUESTA = 3;
                    }

                    if ((item.FindControl("rbNivel4") as RadButton).Checked)
                    {
                        vPrgunta.FG_VALOR1 = false;
                        vPrgunta.FG_VALOR2 = false;
                        vPrgunta.FG_VALOR3 = false;
                        vPrgunta.FG_VALOR4 = true;
                        vPrgunta.FG_VALOR5 = false;
                        vPrgunta.NO_VALOR_RESPUESTA = 4;
                    }

                    if ((item.FindControl("rbNivel5") as RadButton).Checked)
                    {
                        vPrgunta.FG_VALOR1 = false;
                        vPrgunta.FG_VALOR2 = false;
                        vPrgunta.FG_VALOR3 = false;
                        vPrgunta.FG_VALOR4 = false;
                        vPrgunta.FG_VALOR5 = true;
                        vPrgunta.NO_VALOR_RESPUESTA = 5;
                    }
                }
            }


            ////factor B
            foreach (GridDataItem item in dgvCompetenciasfactorB.MasterTableView.Items)
            {
                vIdCuestionarioPregunta = int.Parse(item.GetDataKeyValue("ID_CUESTIONARIO_PREGUNTA").ToString());

                var vPrgunta = pListPeguntas.Where(t => t.ID_CUESTIONARIO_PREGUNTA == vIdCuestionarioPregunta).FirstOrDefault();

                if (vPrgunta != null)
                {

                    //if ((item.FindControl("rbNivel0") as RadButton).Checked)
                    //{
                    //    vPrgunta.FG_VALOR0 = true;
                    //    vPrgunta.NO_VALOR_RESPUESTA = 0;
                    //}

                    if ((item.FindControl("rbNivel1") as RadButton).Checked)
                    {
                        vPrgunta.FG_VALOR1 = true;
                        vPrgunta.FG_VALOR2 = false;
                        vPrgunta.FG_VALOR3 = false;
                        vPrgunta.FG_VALOR4 = false;
                        vPrgunta.FG_VALOR5 = false;
                        vPrgunta.NO_VALOR_RESPUESTA = 1;
                    }

                    if ((item.FindControl("rbNivel2") as RadButton).Checked)
                    {
                        vPrgunta.FG_VALOR1 = false;
                        vPrgunta.FG_VALOR2 = true;
                        vPrgunta.FG_VALOR3 = false;
                        vPrgunta.FG_VALOR4 = false;
                        vPrgunta.FG_VALOR5 = false;
                        vPrgunta.NO_VALOR_RESPUESTA = 2;
                    }

                    if ((item.FindControl("rbNivel3") as RadButton).Checked)
                    {
                        vPrgunta.FG_VALOR1 = false;
                        vPrgunta.FG_VALOR2 = false;
                        vPrgunta.FG_VALOR3 = true;
                        vPrgunta.FG_VALOR4 = false;
                        vPrgunta.FG_VALOR5 = false;
                        vPrgunta.NO_VALOR_RESPUESTA = 3;
                    }

                    if ((item.FindControl("rbNivel4") as RadButton).Checked)
                    {
                        vPrgunta.FG_VALOR1 = false;
                        vPrgunta.FG_VALOR2 = false;
                        vPrgunta.FG_VALOR3 = false;
                        vPrgunta.FG_VALOR4 = true;
                        vPrgunta.FG_VALOR5 = false;
                        vPrgunta.NO_VALOR_RESPUESTA = 4;
                    }

                    if ((item.FindControl("rbNivel5") as RadButton).Checked)
                    {
                        vPrgunta.FG_VALOR1 = false;
                        vPrgunta.FG_VALOR2 = false;
                        vPrgunta.FG_VALOR3 = false;
                        vPrgunta.FG_VALOR4 = false;
                        vPrgunta.FG_VALOR5 = true;
                        vPrgunta.NO_VALOR_RESPUESTA = 5;
                    }
                }
            }


            /////FACTOR C

            foreach (GridDataItem item in dgvCompetenciasfactorC.MasterTableView.Items)
            {
                vIdCuestionarioPregunta = int.Parse(item.GetDataKeyValue("ID_CUESTIONARIO_PREGUNTA").ToString());

                var vPrgunta = pListPeguntas.Where(t => t.ID_CUESTIONARIO_PREGUNTA == vIdCuestionarioPregunta).FirstOrDefault();

                if (vPrgunta != null)
                {

                    //if ((item.FindControl("rbNivel0") as RadButton).Checked)
                    //{
                    //    vPrgunta.FG_VALOR0 = true;
                    //    vPrgunta.NO_VALOR_RESPUESTA = 0;
                    //}

                    if ((item.FindControl("rbNivel1") as RadButton).Checked)
                    {
                        vPrgunta.FG_VALOR1 = true;
                        vPrgunta.FG_VALOR2 = false;
                        vPrgunta.FG_VALOR3 = false;
                        vPrgunta.FG_VALOR4 = false;
                        vPrgunta.FG_VALOR5 = false;
                        vPrgunta.NO_VALOR_RESPUESTA = 1;
                    }

                    if ((item.FindControl("rbNivel2") as RadButton).Checked)
                    {
                        vPrgunta.FG_VALOR1 = false;
                        vPrgunta.FG_VALOR2 = true;
                        vPrgunta.FG_VALOR3 = false;
                        vPrgunta.FG_VALOR4 = false;
                        vPrgunta.FG_VALOR5 = false;
                        vPrgunta.NO_VALOR_RESPUESTA = 2;
                    }

                    if ((item.FindControl("rbNivel3") as RadButton).Checked)
                    {
                        vPrgunta.FG_VALOR1 = false;
                        vPrgunta.FG_VALOR2 = false;
                        vPrgunta.FG_VALOR3 = true;
                        vPrgunta.FG_VALOR4 = false;
                        vPrgunta.FG_VALOR5 = false;
                        vPrgunta.NO_VALOR_RESPUESTA = 3;
                    }

                    if ((item.FindControl("rbNivel4") as RadButton).Checked)
                    {
                        vPrgunta.FG_VALOR1 = false;
                        vPrgunta.FG_VALOR2 = false;
                        vPrgunta.FG_VALOR3 = false;
                        vPrgunta.FG_VALOR4 = true;
                        vPrgunta.FG_VALOR5 = false;
                        vPrgunta.NO_VALOR_RESPUESTA = 4;
                    }

                    if ((item.FindControl("rbNivel5") as RadButton).Checked)
                    {
                        vPrgunta.FG_VALOR1 = false;
                        vPrgunta.FG_VALOR2 = false;
                        vPrgunta.FG_VALOR3 = false;
                        vPrgunta.FG_VALOR4 = false;
                        vPrgunta.FG_VALOR5 = true;
                        vPrgunta.NO_VALOR_RESPUESTA = 5;
                    }
                }
            }


            ///FACTOR D
            //foreach (GridDataItem item in dgvCompetenciasfactorD.MasterTableView.Items)
            //{
            //    vIdCuestionarioPregunta = int.Parse(item.GetDataKeyValue("ID_CUESTIONARIO_PREGUNTA").ToString());

            //    var vPrgunta = pListPeguntas.Where(t => t.ID_CUESTIONARIO_PREGUNTA == vIdCuestionarioPregunta).FirstOrDefault();

            //    if (vPrgunta != null)
            //    {

            //        //if ((item.FindControl("rbNivel0") as RadButton).Checked)
            //        //{
            //        //    vPrgunta.FG_VALOR0 = true;
            //        //    vPrgunta.NO_VALOR_RESPUESTA = 0;
            //        //}

            //        if ((item.FindControl("rbNivel1") as RadButton).Checked)
            //        {
            //            vPrgunta.FG_VALOR1 = true;
            //            vPrgunta.FG_VALOR2 = false;
            //            vPrgunta.FG_VALOR3 = false;
            //            vPrgunta.FG_VALOR4 = false;
            //            vPrgunta.FG_VALOR5 = false;
            //            vPrgunta.NO_VALOR_RESPUESTA = 1;
            //        }

            //        if ((item.FindControl("rbNivel2") as RadButton).Checked)
            //        {
            //            vPrgunta.FG_VALOR1 = false;
            //            vPrgunta.FG_VALOR2 = true;
            //            vPrgunta.FG_VALOR3 = false;
            //            vPrgunta.FG_VALOR4 = false;
            //            vPrgunta.FG_VALOR5 = false;
            //            vPrgunta.NO_VALOR_RESPUESTA = 2;
            //        }

            //        if ((item.FindControl("rbNivel3") as RadButton).Checked)
            //        {
            //            vPrgunta.FG_VALOR1 = false;
            //            vPrgunta.FG_VALOR2 = false;
            //            vPrgunta.FG_VALOR3 = true;
            //            vPrgunta.FG_VALOR4 = false;
            //            vPrgunta.FG_VALOR5 = false;
            //            vPrgunta.NO_VALOR_RESPUESTA = 3;
            //        }

            //        if ((item.FindControl("rbNivel4") as RadButton).Checked)
            //        {
            //            vPrgunta.FG_VALOR1 = false;
            //            vPrgunta.FG_VALOR2 = false;
            //            vPrgunta.FG_VALOR3 = false;
            //            vPrgunta.FG_VALOR4 = true;
            //            vPrgunta.FG_VALOR5 = false;
            //            vPrgunta.NO_VALOR_RESPUESTA = 4;
            //        }

            //        if ((item.FindControl("rbNivel5") as RadButton).Checked)
            //        {
            //            vPrgunta.FG_VALOR1 = false;
            //            vPrgunta.FG_VALOR2 = false;
            //            vPrgunta.FG_VALOR3 = false;
            //            vPrgunta.FG_VALOR4 = false;
            //            vPrgunta.FG_VALOR5 = true;
            //            vPrgunta.NO_VALOR_RESPUESTA = 5;
            //        }
            //    }
            //}

        }

        private void ObtenerProcentaje()
        {
            int vNoPreguntasContestadas, vNoPreguntasTotales;
            double vPrAvance;

            vNoPreguntasTotales = pListPeguntas.Count;
            vNoPreguntasContestadas = pListPeguntas.Where(t => t.FG_VALOR0 || t.FG_VALOR1 || t.FG_VALOR2 || t.FG_VALOR3 || t.FG_VALOR4 || t.FG_VALOR5).Count();

            vPrAvance = (((double)vNoPreguntasContestadas / (double)vNoPreguntasTotales) * 100);
            //cpbEvaluacion.Value = vPrAvance;

        }

        private void AsignarTooltip()
        {
            foreach (GridDataItem item in dgvCompetencias.MasterTableView.Items)
            {
                if (item.DataItem != null)
                {
                    E_PREGUNTAS p = (E_PREGUNTAS)item.DataItem;

                    //(item.FindControl("rbNivel0") as RadButton).ToolTip = p.DS_NIVEL0;
                    (item.FindControl("rbNivel1") as RadButton).ToolTip = p.DS_NIVEL1;
                    (item.FindControl("rbNivel2") as RadButton).ToolTip = p.DS_NIVEL2;
                    (item.FindControl("rbNivel3") as RadButton).ToolTip = p.DS_NIVEL3;
                    (item.FindControl("rbNivel4") as RadButton).ToolTip = p.DS_NIVEL4;
                    (item.FindControl("rbNivel5") as RadButton).ToolTip = p.DS_NIVEL5;
                }
            }
        }

        //private void GuardarRespuestasBD()
        //{
        //    XElement vXmlRespuestas = new XElement("CUESTIONARIO");

        //    vXmlRespuestas.Add(pListPeguntas.Select(t =>
        //        new XElement("RESPUESTA",
        //            new XAttribute("ID_CUESTIONARIO_PREGUNTA", t.ID_CUESTIONARIO_PREGUNTA),
        //            new XAttribute("NO_VALOR_RESPUESTA", t.NO_VALOR_RESPUESTA))));

        //    var vResultado = negocio.ActualizaRespuestaCuestionario(vXmlRespuestas.ToString(), vClUsuario, vNbPrograma);
        //    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
        //    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "closeWindow");
        //}

        private bool ValidarPreguntas(bool pValidarClasificacion, bool pValidarPreguntas)
        {
            bool vRespuesta = true;
            int vNoPreguntas;
            string vMensaje;

            if (pValidarPreguntas)
            {
                if (pValidarClasificacion)
                {
                    vNoPreguntas = pListPeguntas.Where(t => t.NO_VALOR_RESPUESTA == -1 & t.CL_CLASIFICACION == pClas).Count();
                    vMensaje = "No se han contestado todas las preguntas.";
                }
                else
                {
                    vNoPreguntas = pListPeguntas.Where(t => t.NO_VALOR_RESPUESTA == -1).Count();
                    vMensaje = "No se han contestado todas las preguntas.";
                }

                if (vNoPreguntas > 0)
                {
                    vRespuesta = false;
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
                }
            }


            return vRespuesta;
        }

        private void TerminarCuestionario(bool pTerminarCuestionario)
        {
            string vRepsuestasAdicionales = null;

            AsignarRespuestas();
            ObtenerProcentaje();

            if (ValidarPreguntas(false, true))
            {
                XElement vXmlRespuestasAdicionales = generalXmlAdicionales();

                if (vXmlRespuestasAdicionales != null)
                {
                    vRepsuestasAdicionales = vXmlRespuestasAdicionales.ToString();
                }

                XElement vXmlRespuestas = new XElement("CUESTIONARIO");

                vXmlRespuestas.Add(pListPeguntas.Select(t =>
                    new XElement("RESPUESTA",
                        new XAttribute("ID_CUESTIONARIO_PREGUNTA", t.ID_CUESTIONARIO_PREGUNTA),
                        new XAttribute("NO_VALOR_RESPUESTA", t.NO_VALOR_RESPUESTA))));


                var vResultado = negocio.TerminaCuestinario(vXmlRespuestas.ToString(), vIdPeriodo, vIdEvaluado, pIdEvaluadoEvaluador, 0, pTerminarCuestionario, vClUsuario, vNbPrograma, vRepsuestasAdicionales);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                //if (pTerminarCuestionario)
                //{
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
                //}
                //else
                //{
                //UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                //}                            
            }
        }

        protected void CrearFormulario(XElement pXmlPlantilla)
        {
            foreach (XElement vXmlControl in pXmlPlantilla.Elements("CAMPO"))
            {
                int vDefaultWidth = 200;
                int vDefaultLabelWidth = 150;

                vXmlControl.Add(new XAttribute("FG_HABILITADO", "true"));

                HtmlGenericControl vControlHTML;
                ControlDinamico vControl = new ControlDinamico(vXmlControl, true, vDefaultWidth, vDefaultLabelWidth);

                if (vControl.CtrlControl != null)
                {
                    vControlHTML = new HtmlGenericControl("div");
                    vControlHTML.Attributes.Add("class", "ctrlBasico");

                    if (vControl.CtrlLabel != null)
                    {
                        ((HtmlGenericControl)vControl.CtrlLabel).Style.Add("display", "inline-block");
                        ((HtmlGenericControl)vControl.CtrlLabel).Style.Add("padding-right", "10px");
                        ((HtmlGenericControl)vControl.CtrlLabel).Style.Add("text-align", "right");
                        ((HtmlGenericControl)vControl.CtrlLabel).Style.Add("width", "200px");

                        vControlHTML.Controls.Add(vControl.CtrlLabel);
                    }

                    vControlHTML.Controls.Add(vControl.CtrlControl);
                    divCamposExtras.Controls.Add(new LiteralControl("<div style='clear:both;'></div>"));
                    divCamposExtras.Controls.Add(vControlHTML);
                }
            }
        }

        private XElement generalXmlAdicionales()
        {

            if (vXmlCamposAdicionales != null)
            {
                XElement pXmlAdicionales = XElement.Parse(vXmlCamposAdicionales);

                foreach (XElement vXmlControl in pXmlAdicionales.Elements("CAMPO"))
                {
                    string vClTipoControl = vXmlControl.Attribute("CL_TIPO").Value;
                    string vIdControl = vXmlControl.Attribute("ID_CAMPO").Value;
                    string vNbControl = vXmlControl.Attribute("NB_CAMPO").Value;
                    string vNbValor = String.Empty;
                    string vDsValor = "";

                    bool vFgAsignarValor = true;
                    Control vControl = divCamposExtras.FindControl(vIdControl);

                    switch (vClTipoControl)
                    {
                        case "TEXTBOX":
                            vNbValor = ((RadTextBox)vControl).Text;
                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);

                            if (vFgAsignarValor)
                                UtilXML.AsignarValorAtributo(vXmlControl, "NB_VALOR", vNbValor);

                            break;

                        case "MASKBOX":
                            vNbValor = ((RadMaskedTextBox)vControl).Text;
                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);

                            if (vFgAsignarValor)
                                UtilXML.AsignarValorAtributo(vXmlControl, "NB_VALOR", vNbValor);

                            break;

                        case "DATEPICKER":
                            DateTime vFecha = ((RadDatePicker)vControl).SelectedDate ?? DateTime.Now;

                            vNbValor = vFecha.ToString("dd/MM/yyyy");
                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);

                            if (vFgAsignarValor)
                                UtilXML.AsignarValorAtributo(vXmlControl, "NB_VALOR", vNbValor);

                            break;

                        case "COMBOBOX":
                            vNbValor = ((RadComboBox)vControl).SelectedValue;

                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);

                            if (vFgAsignarValor)
                                UtilXML.AsignarValorAtributo(vXmlControl, "NB_VALOR", vNbValor);

                            //vXmlControl.Element("ITEMS").Remove();

                            break;

                        case "LISTBOX":
                            RadListBox vRadListBox = ((RadListBox)vControl);
                            string vClValor = String.Empty;

                            foreach (RadListBoxItem item in vRadListBox.SelectedItems)
                            {
                                vNbValor = item.Value;
                                vDsValor = item.Text;
                            }

                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
                            if (vFgAsignarValor)
                            {
                                UtilXML.AsignarValorAtributo(vXmlControl, "NB_VALOR", vNbValor);
                                UtilXML.AsignarValorAtributo(vXmlControl, "DS_VALOR", vDsValor);
                            }

                            break;
                    }
                }

                return pXmlAdicionales;
            }
            else
            {
                return null;
            }
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();

            vIdEvaluadoEvaluador = int.Parse(Request.Params["ID_EVALUADO_EVALUADOR"].ToString());

            var oCuestionario = nPeriodo.ObtieneCuestionario(pIdEvaluadoEvaluador: vIdEvaluadoEvaluador);

            if (oCuestionario != null)
            {
                vTieneCuestionarios = true;
                if (oCuestionario.XML_PREGUNTAS_ADICIONALES != null)
                {
                    vXmlCamposAdicionales = oCuestionario.XML_PREGUNTAS_ADICIONALES;
                    vXmlCamposAdicionalesCatalogos = oCuestionario.XML_PREGUNTAS_CATALOGOS_ADICIONALES;

                    CrearFormulario(XElement.Parse(oCuestionario.XML_PREGUNTAS_ADICIONALES));
                }
            }
            else 
            {
                vTieneCuestionarios = false;                
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = (ContextoUsuario.oUsuario != null) ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO";

            if (!vTieneCuestionarios)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "No están creados los cuestionarios para este evaluado.", E_TIPO_RESPUESTA_DB.WARNING_WITH_FUNCTION, pCallBackFunction: "closeWindow");
            }

            string vClModulo = "FORMACION";
            string vModulo = Request.QueryString["m"];
            if (vModulo != null)
                vClModulo = vModulo;

            cssModulo = Utileria.ObtenerCssModulo(vClModulo);

            if (!Page.IsPostBack)
            {
                if (Request.Params["ID_EVALUADOR"] != null)
                {
                    ClimaLaboralNegocio negocioClima = new ClimaLaboralNegocio();
                    
                    SPE_OBTIENE_EO_PERIODO_EVALUADOR_Result periodo = new SPE_OBTIENE_EO_PERIODO_EVALUADOR_Result();
                    SPE_OBTIENE_FYD_EVALUADO_Result evaluado = new SPE_OBTIENE_FYD_EVALUADO_Result();
                    List<SPE_OBTIENE_EO_PREGUNTAS_EVALUACION_Result> preguntas = new List<SPE_OBTIENE_EO_PREGUNTAS_EVALUACION_Result>();

                    pIdEvaluador = int.Parse((Request.QueryString["ID_EVALUADOR"]));
                    pIdEvaluadoEvaluador = int.Parse((Request.QueryString["ID_EVALUADO_EVALUADOR"]));

                    periodo = negocioClima.ObtenerPeriodoEvaluado(pIdEvaluador);
                    evaluado = negocio.ObtieneEvaluado(pIdEvaluadoEvaluador);
                    preguntas = negocio.ObtienePreguntasEO(pIdEvaluadoEvaluador);

                    pListPeguntas = (from c in preguntas
                                     select new E_PREGUNTAS
                                     {
                                         CL_CLASIFICACION = string.Empty,
                                         CL_COLOR = string.Empty,
                                         NB_CLASIFICACION_COMPETENCIA = string.Empty,
                                         DS_CLASIFICACION_COMPETENCIA = string.Empty,
                                         //NO_ORDEN = c.NO_ORDEN,
                                         ID_CUESTIONARIO_PREGUNTA = c.ID_CUESTIONARIO_PREGUNTA,
                                         NB_PREGUNTA = c.NB_PREGUNTA,
                                         DS_PREGUNTA = c.DS_PREGUNTA,
                                         NB_RESPUESTA = c.NB_RESPUESTA,
                                         NO_VALOR_RESPUESTA = c.NO_VALOR_RESPUESTA,
                                         NO_VALOR_PREGUNTA = c.NO_VALOR_PREGUNTA
                                         //DS_NIVEL0 = c.DS_NIVEL0,
                                         //DS_NIVEL1 = c.DS_NIVEL1,
                                         //DS_NIVEL2 = c.DS_NIVEL2,
                                         //DS_NIVEL3 = c.DS_NIVEL3,
                                         //DS_NIVEL4 = c.DS_NIVEL4,
                                         //DS_NIVEL5 = c.DS_NIVEL5
                                     }).ToList();

                    pListClasificacion = (from c in pListPeguntas
                                          group c by new { c.CL_CLASIFICACION, c.CL_COLOR, c.NO_ORDEN } into grp
                                          orderby grp.Key.NO_ORDEN ascending
                                          select new E_CLASIFICACION
                                          {
                                              CL_CLASIFICACION = grp.Key.CL_CLASIFICACION,
                                              CL_COLOR = grp.Key.CL_COLOR,
                                              NO_ORDEN = grp.Key.NO_ORDEN

                                          }).ToList();

                    if (pListClasificacion.Count > 0)
                    {
                        pClas = pListClasificacion.FirstOrDefault().CL_CLASIFICACION;
                        pOrden = pListClasificacion.FirstOrDefault().NO_ORDEN;
                        vClColor = pListClasificacion.FirstOrDefault().CL_COLOR;
                        //divColorClas.Style.Add("background", vClColor);
                    }


                    pIdIndex = 0;
                 
                    if (periodo != null)
                    {
                        vIdEvaluado = evaluado.ID_EVALUADO;
                        vIdPeriodo = periodo.ID_PERIODO;

                        txtNoPeriodo.InnerText = periodo.ID_PERIODO.ToString();
                        txtNbPeriodo.InnerText = periodo.NB_PERIODO;
                        txtEvaluador.InnerText = periodo.NB_EVALUADOR;

                        txtNombreEvaluado.InnerText = evaluado.NB_EVALUADO;
                        txtPuestoEvaluado.InnerText = evaluado.NB_PUESTO;
                        txtTipo.InnerText = evaluado.CL_ROL_EVALUADOR;
                        //btnAnt.Enabled = false;
                        ObtenerProcentaje();
                    }
                }
            }

            //if (pIdIndex == pListClasificacion.Count - 1 && vValidaCuestionario == false)
            //    btnNext.Enabled = false;
            //else
            //    btnNext.Enabled = true;
           
        }

        protected void dgvCompetencias_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (pListPeguntas.Count > 0)
            {
                var vlistaPreguntasPorAsignar = pListPeguntas.Take(3);
                dgvCompetencias.DataSource = vlistaPreguntasPorAsignar;//vlistaPreguntasPorAsignar.Take(3);//pListPeguntas.Where(l => l.CL_CLASIFICACION == pClas && l.NO_ORDEN == pOrden);
                //txtNbClasificacion.InnerHtml = pListPeguntas.Where(l => l.CL_CLASIFICACION == pClas && l.NO_ORDEN == pOrden).FirstOrDefault().NB_CLASIFICACION_COMPETENCIA;
                //txtDsSignificado.InnerHtml = pListPeguntas.Where(l => l.CL_CLASIFICACION == pClas && l.NO_ORDEN == pOrden).FirstOrDefault().DS_CLASIFICACION_COMPETENCIA;
            

            }
        }

        protected void dgvCompetencias_DataBound(object sender, EventArgs e)
        {
            //AsignarTooltip();
        }

        protected void btnAnt_Click(object sender, EventArgs e)
        {
            AsignarRespuestas();
            ObtenerProcentaje();

            if (ValidarPreguntas(true, true))
            {
                if (pIdIndex >= 1)
                {
                    pIdIndex = pIdIndex - 1;
                    pClas = pListClasificacion.ElementAt(pIdIndex).CL_CLASIFICACION;
                    pOrden = pListClasificacion.ElementAt(pIdIndex).NO_ORDEN;
                    vClColor = pListClasificacion.ElementAt(pIdIndex).CL_COLOR;
                    //divColorClas.Style.Add("background", vClColor);
                    dgvCompetencias.Rebind();
                    divCamposExtras.Style["display"] = "none";
                    divCompetencias.Style["display"] = "block";

                    //tdClasificacion.Attributes["class"] = "MostrarCelda";
                    //tdSignificado.Attributes["class"] = "MostrarCelda";

                    //if (pIdIndex == pListClasificacion.Count - 1)
                    //    btnNext.Enabled = false;
                    //else
                    //    btnNext.Enabled = true;
                }
                //else
                //    btnAnt.Enabled = false;
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            AsignarRespuestas();
            ObtenerProcentaje();

            if (ValidarPreguntas(true, true))
            {
                if (pIdIndex < pListClasificacion.Count - 1)
                {

                    pIdIndex = pIdIndex + 1;

                    //txtDsSignificado.Style["display"] = "block";
                    //txtNbClasificacion.Style["display"] = "block";
                    //divColorClas.Style["display"] = "block";

                    //tdClasificacion.Attributes["class"] = "MostrarCelda";
                    //tdSignificado.Attributes["class"] = "MostrarCelda";

                    pClas = pListClasificacion.ElementAt(pIdIndex).CL_CLASIFICACION;
                    pOrden = pListClasificacion.ElementAt(pIdIndex).NO_ORDEN;
                    vClColor = pListClasificacion.ElementAt(pIdIndex).CL_COLOR;
                    //divColorClas.Style.Add("background", vClColor);
                    dgvCompetencias.Rebind();
                    //if (pIdIndex == 0)
                    //    btnAnt.Enabled = false;
                    //else
                    //    btnAnt.Enabled = true;
                }
                else
                {
                    //if (pIdIndex == pListClasificacion.Count - 1)
                    //    btnNext.Enabled = false;
                    //else
                    //    btnNext.Enabled = true;

                    if (vXmlCamposAdicionales != null)
                    {
                        if (pIdIndex < pListClasificacion.Count)
                        {
                            pIdIndex++;
                        }

                        //txtNbClasificacion.InnerHtml = String.Empty;
                        //txtDsSignificado.InnerHtml = String.Empty;
                        //divColorClas.Style.Add("background", "White");
                        divCamposExtras.Style["display"] = "block";
                        divCompetencias.Style["display"] = "none";

                        //tdClasificacion.Attributes["class"] = "OcultarCelda";
                        //tdSignificado.Attributes["class"] = "OcultarCelda";

                        //txtDsSignificado.Style["display"] = "none";
                        //txtNbClasificacion.Style["display"] = "none";
                        //divColorClas.Style["display"] = "none";
                    }
                }
            }
            vValidaCuestionario = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //GuardarRespuestasBD();
            TerminarCuestionario(false);
            //dgvCompetencias.MasterTableView.FindItemByKeyValue("ID_COMUNICADO", vIdComunicado).Selected = true;

            //if(dgvCompetencias.MasterTableView.FindItemByKeyValue("ID_COMUNICADO", vIdComunicado) != null)
            //{
            //    dgvCompetencias.MasterTableView.FindItemByKeyValue("ID_COMUNICADO", vIdComunicado).Selected = true;
            //}
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            TerminarCuestionario(true);
        }

        protected void dgvCompetenciasfactorB_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (pListPeguntas.Count() > 5)
            {
                var vlistaPreguntasPorAsignar = pListPeguntas.GetRange(3, 3); //pListPeguntas.Skip(3).Take(3);
                dgvCompetenciasfactorB.DataSource = vlistaPreguntasPorAsignar;
            }
        }

        protected void dgvCompetenciasfactorC_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (pListPeguntas.Count() > 9)
            {
                var vlistaPreguntasPorAsignar = pListPeguntas.GetRange(6, 4);
                dgvCompetenciasfactorC.DataSource = vlistaPreguntasPorAsignar;
            }
        }

        protected void dgvCompetenciasfactorD_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (pListPeguntas.Count() > 11)
            {
                var vlistaPreguntasPorAsignar = pListPeguntas.GetRange(10, 2);
                //dgvCompetenciasfactorD.DataSource = vlistaPreguntasPorAsignar;
                calcularPorcetajeObtenido();
            }
        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            AsignarRespuestas();
           var vPreguntasRespondidas = ValidarPreguntas(false, true);

           if (vPreguntasRespondidas)
           {
               calcularPorcetajeObtenido();
           }
        }

        public void calcularPorcetajeObtenido()
        {
            var vListaConRespuestas = pListPeguntas.Take(12);
            decimal vValor;
            int i=1;
            decimal vResultado = 0;
            foreach(E_PREGUNTAS pregunta in vListaConRespuestas)
            {
                vValor = 0;
                
                if(pregunta.FG_VALOR1)
                    vValor = 20 * (decimal)pregunta.NO_VALOR_PREGUNTA;
                if (pregunta.FG_VALOR2)
                    vValor = 40 * (decimal)pregunta.NO_VALOR_PREGUNTA; 
                if (pregunta.FG_VALOR3)
                    vValor = 60 * (decimal)pregunta.NO_VALOR_PREGUNTA; 
                if (pregunta.FG_VALOR4)
                    vValor = 80 * (decimal)pregunta.NO_VALOR_PREGUNTA; 
                if (pregunta.FG_VALOR5)
                    vValor = 100 * (decimal)pregunta.NO_VALOR_PREGUNTA; 


                //if (i == 1 || i == 3)
                //    vResultado = vResultado + (vValor * 0.15);

                //if (i == 2 || i == 8 || i == 9 || i == 10)
                //    vResultado = vResultado + (vValor * 0.1);

                //if (i == 4 || i == 5 || i == 6 || i == 7 || i == 11 || i == 12)
                //    vResultado = vResultado + (vValor * 0.05);
                vResultado = vResultado + vValor;
                i++;
            }
            int vResultEtiqueta = (int)vResultado;
            lblResultado.InnerHtml = vResultEtiqueta.ToString() + "%";
        }
    }
}
