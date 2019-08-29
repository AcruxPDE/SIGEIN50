using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using Telerik.Web.UI;
using Stimulsoft.Base.Json;
using SIGE.WebApp.Comunes;
using SIGE.Negocio.Administracion;
using SIGE.Entidades;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using System.IO;
using System.Web.UI.HtmlControls;
using SIGE.Entidades.SecretariaTrabajoPrevisionSocial;
using SIGE.Negocio.SecretariaTrabajoPrevisionSocial;
using WebApp.Comunes;
using SIGE.Negocio.FormacionDesarrollo;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaCursos : System.Web.UI.Page
    {
        #region Varibales

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vNbFirstRadEditorTagName = "p";

        public int vCursoId
        {
            get { return (int)ViewState["vsID_CURSO"]; }
            set { ViewState["vsID_CURSO"] = value; }
        }

        private E_TIPO_OPERACION_DB vClOperacion
        {
            get { return (E_TIPO_OPERACION_DB)ViewState["vs_vClOperacion"]; }
            set { ViewState["vs_vClOperacion"] = value; }
        }

        private E_CURSO vCurso
        {
            get { return ContextoCurso.oCursos.Where(t => t.ID_ITEM == vIdListaCurso).FirstOrDefault(); }
        }

        public Guid vIdListaCurso
        {
            get { return (Guid)ViewState["vs_id_lista_curso"]; }
            set { ViewState["vs_id_lista_curso"] = value; }
        }

        List<E_DOCUMENTO> vLstDocumentos
        {
            get { return (List<E_DOCUMENTO>)ViewState["vs_vc_lista_documentos"]; }
            set { ViewState["vs_vc_lista_documentos"] = value; }
        }

        private string vClRutaArchivosTemporales
        {
            get { return (string)ViewState["vs_vi_cl_ruta_archivos"]; }
            set { ViewState["vs_vi_cl_ruta_archivos"] = value; }
        }

        private string vXmlDocumentos
        {
            get { return (string)ViewState["vs_vi_xml_documentos"]; }
            set { ViewState["vs_vi_xml_documentos"] = value; }
        }

        private string vXmlAdicionales
        {
            get { return (string)ViewState["vs_xml_campos_adicionales"]; }
            set { ViewState["vs_xml_campos_adicionales"] = value; }
        }

        AreaTematicaNegocio negocio = new AreaTematicaNegocio();
        public List<SPE_OBTIENE_C_AREA_TEMATICA_Result> listaAreasT;

        #endregion

        #region Funciones

        protected void CargarDatos()
        {
            CursoNegocio nCurso = new CursoNegocio();
            E_CURSO oCurso = nCurso.ObtieneCurso(vCursoId);

            ContextoCurso.oCursos.Add(oCurso);

            vIdListaCurso = oCurso.ID_ITEM;
            txtClave.Text = vCurso.CL_CURSO;
            txtNombre.Text = vCurso.NB_CURSO;
            //txtDsNotas.Content = vCurso.DS_NOTAS;

            if (!String.IsNullOrEmpty(vCurso.DS_NOTAS))
            {
                if (vCurso.DS_NOTAS.Contains("DS_NOTA"))
                {
                    txtDsNotas.Content = Utileria.MostrarNotas(vCurso.DS_NOTAS);
                }
                else
                {
                    XElement vRequerimientos = XElement.Parse(vCurso.DS_NOTAS);
                    if (vRequerimientos != null)
                    {
                        vRequerimientos.Name = vNbFirstRadEditorTagName;
                        txtDsNotas.Content = vRequerimientos.ToString();
                    }
                }
            }

            txtDuracion.Text = vCurso.NO_DURACION_CURSO.ToString();

            if (vCurso.LS_AREAS_TEMATICAS.CL_AREA_TEMATICA != null)
            {
                btnEliminarAreaTCurso.Visible = true;
                cmbAreaT.SelectedValue = vCurso.LS_AREAS_TEMATICAS.CL_AREA_TEMATICA.ToString();
                lblClAreaT.Text = vCurso.LS_AREAS_TEMATICAS.CL_AREA_TEMATICA;
                lblAreaT.Text = vCurso.LS_AREAS_TEMATICAS.NB_AREA_TEMATICA;
            }
            else {
                btnEliminarAreaTCurso.Visible = false;
            }
            SPE_OBTIENE_M_PUESTO_Result puesto = new SPE_OBTIENE_M_PUESTO_Result();

            PuestoNegocio neg = new PuestoNegocio();
            if (vCurso.ID_PUESTO_OBJETIVO != null)
            {
                puesto = neg.ObtienePuestos(vCurso.ID_PUESTO_OBJETIVO).FirstOrDefault();
                Telerik.Web.UI.RadListBoxItem vItmPuestoObjetivo = new RadListBoxItem(puesto.NB_PUESTO, puesto.ID_PUESTO.ToString());
                rlbPuesto.Items.Clear();
                rlbPuesto.Items.Add(vItmPuestoObjetivo);
            }

            vXmlDocumentos = vCurso.XML_DOCUMENTOS;
            AsignarValoresAdicionales(vCurso.XML_CAMPOS_ADICIONALES);

            ContextoCurso.oCursos.Add(vCurso);

        }

        protected void DespacharEventos(string pCatalogo, string pSeleccionados)
        {
            if (pCatalogo == "PUESTO")
                LlenaGridCompetenciaPuesto(pSeleccionados);

            if (pCatalogo == "COMPETENCIA")
                LlenaGridCompetencia(pSeleccionados);

            if (pCatalogo == "INSTRUCTOR")
                LlenaGridInstructor(pSeleccionados);

            if (pCatalogo == "TEMA")
                LlenaGridTema(pSeleccionados);
        }

        protected void LlenaGridCompetenciaPuesto(string cIdPuesto)
        {
            if (cIdPuesto != null & cIdPuesto != "")
            {
                var idPuesto = Convert.ToInt32(cIdPuesto);
                CursoNegocio nPuestoCompetencia = new CursoNegocio();

                vCurso.LS_COMPETENCIAS = nPuestoCompetencia.ObtienePuestoCompetencia(idPuesto, null).Select(el => new E_CURSO_COMPETENCIA
                {
                    ID_CURSO_COMPETENCIA = 0,
                    ID_COMPETENCIA = el.ID_COMPETENCIA,
                    CL_TIPO_COMPETENCIA = el.CL_TIPO_COMPETENCIA,
                    NB_COMPETENCIA = el.NB_COMPETENCIA
                }).ToList();
                grdCursoCompetencia.DataSource = vCurso.LS_COMPETENCIAS;
                grdCursoCompetencia.Rebind();

                radBtnGuardarCompetencia.Enabled = false;
                radBtnEliminaCompetencia.Enabled = false;
            }
        }

        protected void LlenaGridTema(string cTemas)
        {
            if (cTemas != null & cTemas != "")
            {
                grdCursoTema.Rebind();
            }
        }

        protected void LlenaGridCompetencia(string cCompetencias)
        {
            if (cCompetencias != null & cCompetencias != "")
            {
                List<E_CURSO_COMPETENCIA> competencias = new List<E_CURSO_COMPETENCIA>();
                competencias = JsonConvert.DeserializeObject<List<E_CURSO_COMPETENCIA>>(cCompetencias);
                vCurso.LS_COMPETENCIAS.AddRange(competencias.Where(w => !vCurso.LS_COMPETENCIAS.Any(a => a.ID_COMPETENCIA == w.ID_COMPETENCIA)));
                grdCursoCompetencia.Rebind();
            }
        }

        protected void LlenaGridInstructor(string cInstructores)
        {
            if (cInstructores != null & cInstructores != "")
            {
                List<E_CURSO_INSTRUCTOR> instructor = new List<E_CURSO_INSTRUCTOR>();
                instructor = JsonConvert.DeserializeObject<List<E_CURSO_INSTRUCTOR>>(cInstructores);
                vCurso.LS_INSTRUCTORES.AddRange(instructor.Where(w => !vCurso.LS_INSTRUCTORES.Any(a => a.ID_INSTRUCTOR == w.ID_INSTRUCTOR)));
                grdCursoInstructor.Rebind();
            }
        }

        protected void LlenaComboAreas()
        {

            if (cmbAreaT.SelectedValue != "")
            {
                var AreaT = negocio.Obtener_C_AREA_TEMATICA(CL_AREA_TEMATICA: cmbAreaT.SelectedValue.ToString()).FirstOrDefault();

                E_CURSO_AREA_TEMATICA areas = new E_CURSO_AREA_TEMATICA()
                {
                    ID_AREA_TEMATICA = AreaT.ID_AREA_TEMATICA,
                    CL_AREA_TEMATICA = AreaT.CL_AREA_TEMATICA
                };
                vCurso.LS_AREAS_TEMATICAS = areas;
            }
        }

        protected void AddDocumento(string pClTipoDocumento, RadAsyncUpload pFiDocumentos)
        {
            foreach (UploadedFile f in pFiDocumentos.UploadedFiles)
            {
                E_DOCUMENTO vDocumento = new E_DOCUMENTO()
                {
                    ID_ITEM = Guid.NewGuid(),
                    CL_TIPO_DOCUMENTO = pClTipoDocumento,
                    NB_DOCUMENTO = f.FileName,
                    FE_CREATED_DATE = DateTime.Now
                };

                vLstDocumentos.Add(vDocumento);

                f.InputStream.Close();
                f.SaveAs(String.Format(@"{0}\{1}", vClRutaArchivosTemporales, vDocumento.GetDocumentFileName()), true);
            }

            if (vLstDocumentos == null)
                vLstDocumentos = new List<E_DOCUMENTO>();
        }

        protected void EliminarDocumento(string pIdItemDocumento)
        {
            E_DOCUMENTO d = vLstDocumentos.FirstOrDefault(f => f.ID_ITEM.ToString().Equals(pIdItemDocumento));

            if (d != null)
            {
                string vClRutaArchivo = Path.Combine(vClRutaArchivosTemporales, d.GetDocumentFileName());
                if (File.Exists(vClRutaArchivo))
                    File.Delete(vClRutaArchivo);
            }

            vLstDocumentos.Remove(d);
            grdDocumentos.Rebind();
        }

        protected void CargarDocumentos()
        {
            if (vLstDocumentos == null)
                vLstDocumentos = new List<E_DOCUMENTO>();

            if (vXmlDocumentos != null)
            {
                XElement x = XElement.Parse(vXmlDocumentos);

                foreach (XElement item in x.Elements("DOCUMENTO"))
                    vLstDocumentos.Add(new E_DOCUMENTO()
                    {
                        ID_ITEM = new Guid(UtilXML.ValorAtributo<string>(item.Attribute("ID_ITEM"))),
                        NB_DOCUMENTO = UtilXML.ValorAtributo<string>(item.Attribute("NB_DOCUMENTO")),
                        ID_DOCUMENTO = UtilXML.ValorAtributo<int>(item.Attribute("ID_DOCUMENTO")),
                        ID_ARCHIVO = UtilXML.ValorAtributo<int>(item.Attribute("ID_ARCHIVO")),
                        CL_TIPO_DOCUMENTO = UtilXML.ValorAtributo<string>(item.Attribute("CL_TIPO_DOCUMENTO"))
                    });
            }
        }

        private string EncapsularRadEditorContent(XElement nodo, string nbNodo)
        {
            if (nodo.Elements().Count() == 1)
                return EncapsularRadEditorContent((XElement)nodo.FirstNode, nbNodo);
            else
            {
                nodo.Name = nbNodo;
                return nodo.ToString();
            }
        }

        private XElement EditorContentToXml(string pNbNodoRaiz, string pDsContenido, string pNbTag)
        {
            return XElement.Parse(EncapsularRadEditorContent(XElement.Parse(String.Format("<{1}>{0}</{1}>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(pDsContenido)), pNbNodoRaiz)), pNbNodoRaiz));
        }

        private void GuardarDatos()
        {
            string vDsNotas;
            CursoNegocio nCurso = new CursoNegocio();
            var ptipo = vClOperacion.ToString();
            vCurso.CL_CURSO = txtClave.Text;
            vCurso.NB_CURSO = txtNombre.Text;
            vCurso.CL_TIPO_CURSO = cmbIdTipoCurso.SelectedValue;
            vCurso.NO_DURACION_CURSO = decimal.Parse(txtDuracion.Text);

            XElement vXmlCA = GeneralXmlAdicionales();

            foreach (RadListBoxItem item in rlbPuesto.Items)
            {
                if (item.Value != null & item.Value != "")
                    vCurso.ID_PUESTO_OBJETIVO = Convert.ToInt32(item.Value);
                else
                    vCurso.ID_PUESTO_OBJETIVO = 0;
            }

             //vDsNotas = txtDsNotas.Content;
             XElement nodoPrincipal = Utileria.GuardarNotas(txtDsNotas.Content, "XML_NOTAS"); //new XElement("XML_NOTAS", EditorContentToXml("DS_NOTAS", vDsNotas, vNbFirstRadEditorTagName));
            vCurso.DS_NOTAS = nodoPrincipal.ToString();

            XElement vCursosInstructores = new XElement("CURSOINSTRUCTORES");
            if(vCurso.LS_INSTRUCTORES.Count > 0)
            vCursosInstructores.Add(vCurso.LS_INSTRUCTORES.Select(i => new XElement("INSTRUCTOR", new XAttribute("ID_INSTRUCTOR", i.ID_INSTRUCTOR))));

            XElement vCursoCompetencias = new XElement("CURSOCOMPETENCIAS");
            if (vCurso.LS_COMPETENCIAS.Count > 0)
                vCursoCompetencias.Add(vCurso.LS_COMPETENCIAS.Select(i => new XElement("COMPETENCIA", new XAttribute("ID_COMPETENCIA", i.ID_COMPETENCIA))));


            XElement vCursosAreas = new XElement("CURSOAREASTEMATICAS");
            if (vCurso.LS_AREAS_TEMATICAS.ID_AREA_TEMATICA != 0 && vCurso.LS_AREAS_TEMATICAS.CL_AREA_TEMATICA != null)
            {
                vCursosAreas.Add((new XElement("AREATEMATICA",
                                            new XAttribute("ID_AREA_TEMATICA", vCurso.LS_AREAS_TEMATICAS.ID_AREA_TEMATICA),
                                            new XAttribute("CL_AREA_TEMATICA", vCurso.LS_AREAS_TEMATICAS.CL_AREA_TEMATICA))));
            }
            XElement vTemas = new XElement("TEMAS");
            XElement vTemaCompetencias = new XElement("TEMACOMPETENCIAS");
            XElement vTemaMateriales = new XElement("TEMAMATERIALES");

            foreach (E_TEMA item in vCurso.LS_TEMAS)
            {
                if (item.ID_ITEM != null)
                {
                vTemas.Add(
                    new XElement("TEMA",
                    new XAttribute("ID_TEMA", item.ID_TEMA),
                    new XAttribute("ID_TEMA_ITEM", item.ID_ITEM),
                    new XAttribute("CL_TEMA", item.CL_TEMA),
                    new XAttribute("NB_TEMA", item.NB_TEMA),
                    new XAttribute("NO_DURACION", item.NO_DURACION),
                    new XAttribute("DS_DESCRIPCION", item.DS_DESCRIPCION)));

                vTemaMateriales.Add(item.LS_MATERIALES.Select(i =>
                    new XElement("MATERIAL",
                    new XAttribute("ID_TEMA", item.ID_TEMA),
                    new XAttribute("ID_TEMA_ITEM", item.ID_ITEM),
                    new XAttribute("ID_MATERIAL_ITEM", i.ID_ITEM),
                    new XAttribute("CL_MATERIAL", i.CL_MATERIAL),
                    new XAttribute("NB_MATERIAL", i.NB_MATERIAL),
                    new XAttribute("MN_MATERIAL", i.MN_MATERIAL))));

                vTemaCompetencias.Add(item.LS_COMPETENCIAS.Select(i =>
                    new XElement("COMPETENCIA",
                    new XAttribute("ID_TEMA", item.ID_TEMA),
                    new XAttribute("ID_TEMA_ITEM", item.ID_ITEM),
                    new XAttribute("ID_COMPETENCIA_ITEM", i.ID_ITEM),
                    new XAttribute("ID_COMPETENCIA", i.ID_COMPETENCIA))));
                }
            }


            List<UDTT_ARCHIVO> vLstArchivos = new List<UDTT_ARCHIVO>();
            foreach (E_DOCUMENTO d in vLstDocumentos)
            {
               string vFilePath = Server.MapPath(Path.Combine(ContextoApp.ClRutaArchivosTemporales, d.GetDocumentFileName()));
               // string vFilePath = vClRutaArchivosTemporales +"\\"+ d.GetDocumentFileName();
                if (File.Exists(vFilePath))
                {
                    vLstArchivos.Add(new UDTT_ARCHIVO()
                    {
                        ID_ITEM = d.ID_ITEM,
                        ID_ARCHIVO = d.ID_ARCHIVO,
                        NB_ARCHIVO = d.NB_DOCUMENTO,
                        FI_ARCHIVO = File.ReadAllBytes(vFilePath)
                    });
                }
            }

            if (!ptipo.Equals("I"))
            {
                vCurso.ID_CURSO = vCursoId;
            }

            E_RESULTADO vResultado = nCurso.InsertaActualizaCurso(ptipo, vClUsuario, vNbPrograma, vCurso, vLstArchivos, vLstDocumentos, vCursosInstructores, vCursoCompetencias, vTemas, vTemaMateriales, vTemaCompetencias, vXmlCA, vCursosAreas);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(RadWindowManager1, vMensaje, vResultado.CL_TIPO_ERROR);
        }

        protected void CrearFormulario(XElement pXmlPlantilla)
        {
            foreach (XElement vXmlControl in pXmlPlantilla.Elements("CAMPO"))
            {
                int vDefaultWidth = 200;
                int vDefaultLabelWidth = 150;

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
                    pvwCamposExtras.Controls.Add(new LiteralControl("<div style='clear:both;'></div>"));
                    pvwCamposExtras.Controls.Add(vControlHTML);
                }
            }
        }

        private void AsignarValoresAdicionales(string pXmlvalores)
        {
            if (pXmlvalores != null & pXmlvalores != "")
            {
                XElement pXmlAdicionales = XElement.Parse(vXmlAdicionales);
                XElement pXmlValoresAdicionales = XElement.Parse(pXmlvalores);

                bool vFgAsignarXML = true;
                string vMensajes = String.Empty;

                foreach (XElement vXmlControl in pXmlAdicionales.Elements("CAMPO"))
                {
                    string vClTipoControl = vXmlControl.Attribute("CL_TIPO").Value;
                    string vIdControl = vXmlControl.Attribute("ID_CAMPO").Value;
                    string vNbValor = null;
                    Control vControl = pvwCamposExtras.FindControl(vIdControl);

                    if (pXmlValoresAdicionales.Elements("CAMPO").Where(n => n.Attribute("ID_CAMPO").Value.ToString() == vIdControl).FirstOrDefault() != null)
                    {
                        vNbValor = pXmlValoresAdicionales.Elements("CAMPO").Where(n => n.Attribute("ID_CAMPO").Value.ToString() == vIdControl).FirstOrDefault().Attribute("NO_VALOR").Value.ToString();
                    }

                    if (vNbValor != null)
                    {
                        switch (vClTipoControl)
                        {
                            case "TEXTBOX":
                                ((RadTextBox)vControl).Text = vNbValor;
                                break;
                            case "MASKBOX":
                                ((RadMaskedTextBox)vControl).Text = vNbValor;
                                break;
                            case "DATEPICKER":
                                //DateTime vFecha =  ?? DateTime.Now;
                                ((RadDatePicker)vControl).SelectedDate = DateTime.Parse(vNbValor);
                                break;
                            case "COMBOBOX":
                                ((RadComboBox)vControl).SelectedValue = vNbValor;
                                break;
                            case "LISTBOX":
                                RadListBox vRadListBox = ((RadListBox)vControl);
                                vRadListBox.SelectedValue = vNbValor;
                                break;
                            case "NUMERICBOX":
                                RadNumericTextBox vRadNumeric = (RadNumericTextBox)vControl;
                                vRadNumeric.Text = vNbValor;
                                break;
                            case "CHECKBOX":
                                RadButton vRadButton = (RadButton)vControl;
                                vRadButton.Checked = vNbValor == "1" ? true : false;
                                break;
                            case "DATEAGE":
                                RadDatePicker vControlF = (RadDatePicker)vControl;
                                vControlF.SelectedDate = DateTime.Parse(vNbValor);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private XElement GeneralXmlAdicionales()
        {
            XElement pXmlAdicionales = XElement.Parse(vXmlAdicionales);
            XElement pXmlValoresAdicionales = new XElement("CAMPOS");
            bool vFgAsignarXML = true;
            string vMensajes = String.Empty;

            foreach (XElement vXmlControl in pXmlAdicionales.Elements("CAMPO"))
            {
                string vClTipoControl = vXmlControl.Attribute("CL_TIPO").Value;
                string vIdControl = vXmlControl.Attribute("ID_CAMPO").Value;
                string vNbControl = vXmlControl.Attribute("NB_CAMPO").Value;
                string vNbValor = String.Empty;
                string vDsValor = "";


                bool vFgAsignarValor = true;
                Control vControl = pvwCamposExtras.FindControl(vIdControl);

                switch (vClTipoControl)
                {
                    case "TEXTBOX":
                        vNbValor = ((RadTextBox)vControl).Text;
                        //vDsValor = ((RadTextBox)vControl).Text;

                        if ((bool?)UtilXML.ValorAtributo(vXmlControl.Attribute("FG_REQUERIDO"), E_TIPO_DATO.BOOLEAN) ?? false)
                        {
                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
                            vFgAsignarXML = vFgAsignarXML && vFgAsignarValor;
                            if (!vFgAsignarValor)
                                vMensajes += String.Format("El campo {0} es requerido.<br />", vNbControl);
                        }
                        break;
                    case "MASKBOX":
                        vNbValor = ((RadMaskedTextBox)vControl).Text;
                        //vDsValor = ((RadMaskedTextBox)vControl).Text;
                        if ((bool?)UtilXML.ValorAtributo(vXmlControl.Attribute("FG_REQUERIDO"), E_TIPO_DATO.BOOLEAN) ?? false)
                        {
                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
                            vFgAsignarXML = vFgAsignarXML && vFgAsignarValor;
                            if (!vFgAsignarValor)
                                vMensajes += String.Format("El campo {0} es requerido.<br />", vNbControl);
                        }
                        break;
                    case "DATEPICKER":
                        DateTime vFecha = ((RadDatePicker)vControl).SelectedDate ?? DateTime.Now;
                        vNbValor = vFecha.ToString("dd/MM/yyyy");
                        //vDsValor = vFecha.ToString("dd/MM/yyyy");
                        if ((bool?)UtilXML.ValorAtributo(vXmlControl.Attribute("FG_REQUERIDO"), E_TIPO_DATO.BOOLEAN) ?? false)
                        {
                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
                            vFgAsignarXML = vFgAsignarXML && vFgAsignarValor;
                            if (!vFgAsignarValor)
                                vMensajes += String.Format("El campo {0} es requerido.<br />", vNbControl);
                        }
                        break;
                    case "COMBOBOX":
                        vNbValor = ((RadComboBox)vControl).SelectedValue;
                        //vDsValor = ((RadComboBox)vControl).Text;
                        if ((bool?)UtilXML.ValorAtributo(vXmlControl.Attribute("FG_REQUERIDO"), E_TIPO_DATO.BOOLEAN) ?? false)
                        {
                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
                            vFgAsignarXML = vFgAsignarXML && vFgAsignarValor;
                            if (!vFgAsignarValor)
                                vMensajes += String.Format("El campo {0} es requerido.<br />", vNbControl);
                        }
                        break;
                    case "LISTBOX":
                        RadListBox vRadListBox = ((RadListBox)vControl);
                        string vClValor = String.Empty;

                        foreach (RadListBoxItem item in vRadListBox.SelectedItems)
                        {
                            vNbValor = item.Value;
                            vDsValor = item.Text;
                        }

                        if ((bool?)UtilXML.ValorAtributo(vXmlControl.Attribute("FG_REQUERIDO"), E_TIPO_DATO.BOOLEAN) ?? false)
                        {
                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
                            vFgAsignarXML = vFgAsignarXML && vFgAsignarValor;
                            if (!vFgAsignarValor)
                                vMensajes += String.Format("El campo {0} es requerido.<br />", vNbControl);
                        }
                        break;
                    case "NUMERICBOX":
                        vNbValor = ((RadNumericTextBox)vControl).Text;
                        vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);

                        if (vFgAsignarValor)
                            UtilXML.AsignarValorAtributo(vXmlControl, "NB_VALOR", vNbValor);
                        break;
                    case "CHECKBOX":
                        vNbValor = (((RadButton)vControl).Checked) ? "1" : "0";
                        UtilXML.AsignarValorAtributo(vXmlControl, "NB_VALOR", vNbValor);
                        break;
                    case "DATEAGE":
                          DateTime vFechaEdad = ((RadDatePicker)vControl).SelectedDate ?? DateTime.Now;
                          vNbValor = vFechaEdad.ToString("dd/MM/yyyy");
                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
                            if (vFgAsignarValor)
                                UtilXML.AsignarValorAtributo(vXmlControl, "NB_VALOR", vNbValor);
                        break;
                    default:
                        vFgAsignarValor = false;
                        break;

                }
                if (vFgAsignarValor)
                {
                    XElement xXmlCampo = new XElement("CAMPO");

                    xXmlCampo.Add(new XAttribute("ID_CAMPO", vIdControl), new XAttribute("NO_VALOR", vNbValor), new XAttribute("DS_VALOR", vDsValor));
                    pXmlValoresAdicionales.Add(xXmlCampo);
                }
            }
            if (vFgAsignarXML)
                return pXmlValoresAdicionales;
            else
            {
                UtilMensajes.MensajeResultadoDB(RadWindowManager1, vMensajes, E_TIPO_RESPUESTA_DB.WARNING);
                return null;
            }
        }

        private void SeguridadProcesos()
        {
            radBtnGuardar.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.C.D");
        }

        #endregion


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////77777777

        protected void Page_Init(object sender, EventArgs e)
        {
            CursoNegocio nCurso = new CursoNegocio();
            CrearFormulario(XElement.Parse(nCurso.ObtieneCampoAdicionalXml("C_CURSO")));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                traerAreasTematicas();
                int vCursoIdQS = -1;
                vClOperacion = E_TIPO_OPERACION_DB.I;

                CursoNegocio nCurso = new CursoNegocio();

                RadListBoxItem vItmPuesto = new RadListBoxItem("Ninguno", String.Empty);
                rlbPuesto.Items.Add(vItmPuesto);
                vClRutaArchivosTemporales = Server.MapPath(ContextoApp.ClRutaArchivosTemporales);
                vLstDocumentos = new List<E_DOCUMENTO>();
                vXmlAdicionales = nCurso.ObtieneCampoAdicionalXml("C_CURSO");

                if (ContextoCurso.oCursos == null)
                {
                    ContextoCurso.oCursos = new List<E_CURSO>();
                }

                if (int.TryParse(Request.QueryString["CursoId"], out vCursoIdQS))
                {
                    vCursoId = vCursoIdQS;
                    vClOperacion = E_TIPO_OPERACION_DB.A;
                    CargarDatos();
                    CargarDocumentos();
                }
                else
                {
                    vCursoId = 0;
                    vIdListaCurso = Guid.NewGuid();
                    ContextoCurso.oCursos.Add(new E_CURSO { ID_ITEM = vIdListaCurso });
                }

                if(Request.QueryString["Action"] != null)
                {
                    if (Request.QueryString["Action"].ToString() == "Consult")
                    {
                        radBtnGuardar.Visible = false;
                        radBtnCancelar.Visible = false;
                        btnAgregarDocumento.Visible = false;
                        radBtnGuardarCompetencia.Visible = false;
                        btnEliminarAreaTCurso.Visible = false;
                        radBtnEliminaCompetencia.Visible = false;
                        btnEliminarPuestoObjetivo.Visible = false;
                        radBtnEliminarInstructor.Visible = false;
                        radBtnEliminaTema.Visible = false;
                        btnDelDocumentos.Visible = false;
                        radBtnEditarTema.Visible = false;
                        radBtnGuardarTema.Visible = false;
                        radBtnGuardarInstructor.Visible = false;
                        radBtnBuscarPuesto.Visible = false;
                        rauDocumento.Enabled = false;
                    }
                }
                    SeguridadProcesos();
            }
            LlenaComboAreas();
            DespacharEventos(Request.Params.Get("__EVENTTARGET"), Request.Params.Get("__EVENTARGUMENT"));
        }

        public void traerAreasTematicas()
        {
            listaAreasT = negocio.Obtener_C_AREA_TEMATICA();
            cmbAreaT.DataSource = listaAreasT;
            cmbAreaT.DataTextField = "NB_AREA_TEMATICA";
            cmbAreaT.DataValueField = "CL_AREA_TEMATICA";
            cmbAreaT.DataBind();
        }

        protected void grdCursoCompetencia_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdCursoCompetencia.DataSource = vCurso.LS_COMPETENCIAS;
        }

        protected void grdCursoInstructor_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdCursoInstructor.DataSource = vCurso.LS_INSTRUCTORES;
        }

        protected void grdCursoTema_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdCursoTema.DataSource = vCurso.LS_TEMAS;
        }

        protected void radBtnEliminarCompetencia_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem i in grdCursoCompetencia.SelectedItems)
                vCurso.LS_COMPETENCIAS.RemoveAll(r => r.ID_ITEM.Equals(new Guid(i.GetDataKeyValue("ID_ITEM").ToString())));

            grdCursoCompetencia.Rebind();
        }

        protected void radBtnEliminarInstructor_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem i in grdCursoInstructor.SelectedItems)
                vCurso.LS_INSTRUCTORES.RemoveAll(r => r.ID_ITEM.Equals(new Guid(i.GetDataKeyValue("ID_ITEM").ToString())));

            grdCursoInstructor.Rebind();
        }

        protected void BtnEliminarTema_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem i in grdCursoTema.SelectedItems)
            {
                vCurso.LS_TEMAS.RemoveAll(r => r.ID_ITEM.Equals(new Guid(i.GetDataKeyValue("ID_ITEM").ToString())));
            }
            grdCursoTema.Rebind();
        }

        protected void radBtnGuardar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }

        protected void btnAgregarDocumento_Click(object sender, EventArgs e)
        {
            AddDocumento(cmbTipoDocumento.SelectedValue, rauDocumento);
            grdDocumentos.Rebind();
        }

        protected void grdDocumentos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdDocumentos.DataSource = vLstDocumentos;
        }

        protected void btnDelDocumentos_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem i in grdDocumentos.SelectedItems)
                EliminarDocumento(i.GetDataKeyValue("ID_ITEM").ToString());
        }

        protected void cmbAreaT_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var AreaT = negocio.Obtener_C_AREA_TEMATICA(CL_AREA_TEMATICA: e.Value.ToString()).FirstOrDefault();
            lblAreaT.Text = AreaT.NB_AREA_TEMATICA;
            lblClAreaT.Text = AreaT.CL_AREA_TEMATICA;
        }

        protected void btnEliminarAreaTCurso_Click(object sender, EventArgs e)
        {

            if (vCurso.LS_AREAS_TEMATICAS.ID_AREA_TEMATICA != 0)
            {

                E_RESULTADO vResultado = negocio.Elimina_K_AREA_TEMATICA_CURSO(vCurso.LS_AREAS_TEMATICAS.ID_AREA_TEMATICA, vCursoId, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(RadWindowManager1, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, null);
                if (vMensaje == "Proceso exitoso" || vMensaje == "Successful Process")
                {
                   vCurso.LS_AREAS_TEMATICAS.ID_AREA_TEMATICA = 0;
                   vCurso.LS_AREAS_TEMATICAS.CL_AREA_TEMATICA = "";
                    cmbAreaT.ClearSelection();
                    cmbAreaT.Text = string.Empty;
                    lblAreaT.Text = "";
                    lblClAreaT.Text = "";
                }
            }
            else {
                UtilMensajes.MensajeResultadoDB(RadWindowManager1, "No hay área temática en el curso", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, null);
            }
        }
    }
}