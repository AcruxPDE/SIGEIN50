using SIGE.Entidades.Externas;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.Entidades.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.WebApp.Comunes;
using SIGE.Entidades;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using Telerik.Web.UI;
using Newtonsoft.Json;
using System.IO;
using System.Web.UI.HtmlControls;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaInstructores : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vInstructorId
        {
            get { return (int)ViewState["vsID_INSTRUCTOR"]; }
            set { ViewState["vsID_INSTRUCTOR"] = value; }
        }

        public Guid vIdInstructor
        {
            get { return (Guid)ViewState["vs_vi_id_instructor"]; }
            set { ViewState["vs_vi_id_instructor"] = value; }
        }

        private E_TIPO_OPERACION_DB vClOperacion
        {
            get { return (E_TIPO_OPERACION_DB)ViewState["vs_vClOperacion"]; }
            set { ViewState["vs_vClOperacion"] = value; }
        }

        private E_INSTRUCTOR vInstructor
        {
            get { return ContextoInstructor.oInstructores.Where(t => t.ID_ITEM == vIdInstructor).FirstOrDefault(); }
        }

        private List<E_INSTRUCTOR_CURSO> vLstCurso
        {
            get { return (List<E_INSTRUCTOR_CURSO>)ViewState["vs_vLstCurso"]; }
            set { ViewState["vs_vLstCurso"] = value; }
        }

        //private List<E_INSTRUCTOR_COMPETENCIA> vLstCompetencia
        //{
        //    get { return (List<E_INSTRUCTOR_COMPETENCIA>)ViewState["vs_vLstCompetencia"]; }
        //    set { ViewState["vs_vLstCompetencia"] = value; }
        //}

        private List<E_TIPO_TELEFONO> vLstTipoTelefono
        {
            get { return (List<E_TIPO_TELEFONO>)ViewState["vs_vLstTipoTelefono"]; }
            set { ViewState["vs_vLstTipoTelefono"] = value; }
        }

        private List<E_TELEFONO> vLstTelefono
        {
            get { return (List<E_TELEFONO>)ViewState["vs_vLstTelefono"]; }
            set { ViewState["vs_vLstTelefono"] = value; }
        }

        List<E_DOCUMENTO> vLstDocumentos
        {
            get { return (List<E_DOCUMENTO>)ViewState["vs_vi_lista_documentos"]; }
            set { ViewState["vs_vi_lista_documentos"] = value; }
        }

        private string vXmlDocumentos
        {
            get { return (string)ViewState["vs_vi_xml_documentos"]; }
            set { ViewState["vs_vi_xml_documentos"] = value; }
        }

        private string vClRutaArchivosTemporales
        {
            get { return (string)ViewState["vs_vi_cl_ruta_archivos"]; }
            set { ViewState["vs_vi_cl_ruta_archivos"] = value; }
        }

        private string vXmlAdicionales
        {
            get { return (string)ViewState["vs_xmlAdicionales"]; }
            set { ViewState["vs_xmlAdicionales"] = value; }
        }

        #endregion

        #region Metodos

        protected void CargarDatosEmpleado(int pEmpleadoId)
        {
            InstructorNegocio nEmpleado = new InstructorNegocio();
            SPE_OBTIENE_M_EMPLEADO_Result empleado = nEmpleado.ObtieneEmpleado(pEmpleadoId).FirstOrDefault();

            if (empleado != null)
            {
                txtClave.Text = empleado.CL_EMPLEADO;
                txtNombre.Text = empleado.NB_EMPLEADO_COMPLETO;
                txtRFC.Text = empleado.CL_RFC;
                txtCURP.Text = empleado.CL_CURP;
                txtCP.Text = empleado.CL_CODIGO_POSTAL;
                // txtPais.Text = empleado.NB_PAIS;
                txtCalle.Text = empleado.NB_CALLE;
                txtNoexterior.Text = empleado.NO_EXTERIOR;
                txtNointerior.Text = empleado.NO_INTERIOR;
                txtFeNacimiento.SelectedDate = empleado.FE_NACIMIENTO;
                txtEmail.Text = empleado.CL_CORREO_ELECTRONICO;

                if (empleado.CL_ESTADO != null)
                {
                    rlbEstado.Items.Clear();
                    rlbEstado.Items.Add(new RadListBoxItem(empleado.NB_ESTADO, empleado.CL_ESTADO));
                }

                if (empleado.CL_MUNICIPIO != null)
                {
                    rlbMunicipio.Items.Clear();
                    rlbMunicipio.Items.Add(new RadListBoxItem(empleado.NB_MUNICIPIO, empleado.CL_MUNICIPIO));
                }

                if (empleado.CL_COLONIA != null)
                {
                    rlbcolonia.Items.Clear();
                    rlbcolonia.Items.Add(new RadListBoxItem(empleado.NB_COLONIA, empleado.CL_COLONIA));
                }

                if (empleado.XML_TELEFONOS != null)
                {
                    vLstTelefono = XElement.Parse(empleado.XML_TELEFONOS).Elements("TELEFONO").Select(el => new E_TELEFONO
                    {
                        NB_TELEFONO = UtilXML.ValorAtributo<string>(el.Attribute("NO_TELEFONO")),
                        CL_TIPO = UtilXML.ValorAtributo<string>(el.Attribute("CL_TIPO_TELEFONO")),
                        NB_TIPO = (vLstTipoTelefono.FirstOrDefault(f => f.NB_VALOR.Equals(UtilXML.ValorAtributo<string>(el.Attribute("CL_TIPO_TELEFONO")))) ?? new E_TIPO_TELEFONO()).NB_TEXTO
                    }).ToList();
                    grdTelefono.Rebind();
                }
            }
        }

        protected void LlenaGridCompetencia(string cCompetencias)
        {
            if (cCompetencias != null & cCompetencias != "")
            {
                List<E_INSTRUCTOR_COMPETENCIA> competencias = new List<E_INSTRUCTOR_COMPETENCIA>();
                competencias = JsonConvert.DeserializeObject<List<E_INSTRUCTOR_COMPETENCIA>>(cCompetencias);
                vInstructor.LstCompetencias.AddRange(competencias.Where(w => !vInstructor.LstCompetencias.Any(a => a.ID_COMPETENCIA == w.ID_COMPETENCIA)));
                grdInstructorCompetencia.Rebind();
            }
        }

        protected void LlenaGridCurso(string cCursos)
        {
            if (cCursos != null & cCursos != "")
            {
                List<E_INSTRUCTOR_CURSO> curso = new List<E_INSTRUCTOR_CURSO>();
                curso = JsonConvert.DeserializeObject<List<E_INSTRUCTOR_CURSO>>(cCursos);
                vLstCurso.AddRange(curso.Where(w => !vLstCurso.Any(a => a.ID_CURSO == w.ID_CURSO)));
                grdInstructorCurso.Rebind();
            }
        }

        protected void CargarDatos(int? pInstructorId)
        {
            InstructorNegocio nInstructorCurso = new InstructorNegocio();
            SPE_OBTIENE_INSTRUCTORES_Result lista = nInstructorCurso.ObtieneInstructor(pInstructorId).FirstOrDefault();

            rbInstInterno.Checked = lista.CL_TIPO_INSTRUCTOR == "INTERNO";
            rbInstExterno.Checked = lista.CL_TIPO_INSTRUCTOR == "EXTERNO";

            HabilitaCampos(rbInstExterno.Checked, rbInstInterno.Checked);

            txtClave.Text = lista.CL_INTRUCTOR;
            txtNombre.Text = lista.NB_INSTRUCTOR;
            txtNombreValIns.Text = lista.NB_VALIDADOR;
            txtRFC.Text = lista.CL_RFC;
            txtCURP.Text = lista.CL_CURP;
            txtRegistro.Text = lista.CL_STPS;
            //txtPais.Text = lista.NB_PAIS;
            txtCP.Text = lista.CL_CODIGO_POSTAL;
            txtCalle.Text = lista.NB_CALLE;
            txtNoexterior.Text = lista.NO_EXTERIOR;
            txtNointerior.Text = lista.NO_INTERIOR;
            txtEscolaridad.Text = lista.DS_ESCOLARIDAD;
            txtFeNacimiento.SelectedDate = lista.FE_NACIMIENTO;
            txtEmail.Text = lista.CL_CORREO_ELECTRONICO;
            txtCostoHora.Value = (double?)lista.MN_COSTO_HORA;
            txtCostoPart.Value = (double?)lista.MN_COSTO_PARTICIPANTE;
            txtEvidencia.Text = lista.DS_EVIDENCIA_COMPETENCIA;

            Telerik.Web.UI.RadListBoxItem vItmEstado = new RadListBoxItem("No seleccionado", String.Empty);
            if (lista.CL_ESTADO != null)
            {
                vItmEstado = new RadListBoxItem(lista.NB_ESTADO, lista.CL_ESTADO);
            }

            rlbEstado.Items.Add(vItmEstado);

            Telerik.Web.UI.RadListBoxItem vItmMunicipio = new RadListBoxItem("No seleccionado", String.Empty);
            if (lista.CL_MUNICIPIO != null)
            {
                vItmMunicipio = new RadListBoxItem(lista.NB_MUNICIPIO, lista.CL_MUNICIPIO);
            }
            rlbMunicipio.Items.Add(vItmMunicipio);

            Telerik.Web.UI.RadListBoxItem vItmColonia = new RadListBoxItem("No seleccionado", String.Empty);
            if (lista.CL_COLONIA != null)
            {
                vItmColonia = new RadListBoxItem(lista.NB_COLONIA, lista.CL_COLONIA);
            }
            rlbcolonia.Items.Add(vItmColonia);

            if (lista.XML_CURSOS != null)
                vLstCurso = XElement.Parse(lista.XML_CURSOS).Elements("CURSO").Select(el => new E_INSTRUCTOR_CURSO
                {
                    ID_INSTRUCTOR_CURSO = UtilXML.ValorAtributo<int>(el.Attribute("ID_INSTRUCTOR_CURSO")),
                    ID_CURSO = UtilXML.ValorAtributo<int>(el.Attribute("ID_CURSO")),
                    CL_CURSO = UtilXML.ValorAtributo<string>(el.Attribute("CL_CURSO")),
                    NB_CURSO = UtilXML.ValorAtributo<string>(el.Attribute("NB_CURSO")),
                }).ToList();

            if (lista.XML_COMPETENCIAS != null)
                vInstructor.LstCompetencias = XElement.Parse(lista.XML_COMPETENCIAS).Elements("COMPETENCIA").Select(el => new E_INSTRUCTOR_COMPETENCIA
                {
                    ID_INSTRUCTOR_COMPETENCIA = UtilXML.ValorAtributo<int>(el.Attribute("ID_INSTRUCTOR_COMPETENCIA")),
                    ID_COMPETENCIA = UtilXML.ValorAtributo<int>(el.Attribute("ID_COMPETENCIA")),
                    CL_COMPETENCIA = UtilXML.ValorAtributo<string>(el.Attribute("CL_COMPETENCIA")),
                    NB_COMPETENCIA = UtilXML.ValorAtributo<string>(el.Attribute("NB_COMPETENCIA")),
                }).ToList();

            CargaTipoTelefono(lista.XML_NO_TELEFONO_TIPOS);

            if (lista.XML_TELEFONOS != null & lista.XML_TELEFONOS != "")
                vLstTelefono = XElement.Parse(lista.XML_TELEFONOS).Elements("TELEFONO").Select(el => new E_TELEFONO
                {
                    NB_TELEFONO = UtilXML.ValorAtributo<string>(el.Attribute("NO_TELEFONO")),
                    CL_TIPO = UtilXML.ValorAtributo<string>(el.Attribute("CL_TIPO")),
                    NB_TIPO = (vLstTipoTelefono.FirstOrDefault(f => f.NB_VALOR.Equals(UtilXML.ValorAtributo<string>(el.Attribute("CL_TIPO")))) ?? new E_TIPO_TELEFONO()).NB_TEXTO
                }).ToList();

            vXmlDocumentos = lista.XML_DOCUMENTOS;
            //vXmlAdicionales = lista.XML_PLANTILLA_CAMPOS_ADICIONALES;

            AsignarValoresAdicionales(lista.XML_CAMPOS_ADICIONALES);
        }

        protected void CargaTipoTelefono(string tipos)
        {
            vLstTipoTelefono = XElement.Parse(tipos).Elements("ITEM").Select(el => new E_TIPO_TELEFONO
            {
                NB_TEXTO = UtilXML.ValorAtributo<String>(el.Attribute("NB_TEXTO")),
                NB_VALOR = UtilXML.ValorAtributo<String>(el.Attribute("NB_VALOR"))
            }).ToList();

            cmbIdTipoTelefono.DataSource = vLstTipoTelefono;
            cmbIdTipoTelefono.DataTextField = "NB_TEXTO";
            cmbIdTipoTelefono.DataValueField = "NB_VALOR";
            cmbIdTipoTelefono.DataBind();
        }

        protected void DespacharEventos(string pCatalogo, string pSeleccionados)
        {
            if (pCatalogo == "COMPETENCIA")
                LlenaGridCompetencia(pSeleccionados);
            if (pCatalogo == "CURSO")
                LlenaGridCurso(pSeleccionados);
            if (pCatalogo == "EMPLEADO")
                CargarDatosEmpleado(Convert.ToInt32(pSeleccionados));
        }

        protected void HabilitaCampos(bool vFgHabilitarCampoInterno, bool vFgHabilitarCampoExterno)
        {
            radBtnBuscarclave.Enabled = vFgHabilitarCampoExterno;
            txtClave.Enabled = vFgHabilitarCampoInterno;
            txtNombre.Enabled = vFgHabilitarCampoInterno;
            txtRFC.Enabled = vFgHabilitarCampoInterno;
            txtCURP.Enabled = vFgHabilitarCampoInterno;

            
            
            //txtCalle.Enabled = vFgHabilitarCampoInterno;
            //txtCP.Enabled = vFgHabilitarCampoInterno;
            //radBtnBuscaCP.Enabled = vFgHabilitarCampoInterno;
            //txtNoexterior.Enabled = vFgHabilitarCampoInterno;
            //txtNointerior.Enabled = vFgHabilitarCampoInterno;

            //txtPais.Enabled = vFgHabilitarCampoInterno;
            txtCP.Enabled = vFgHabilitarCampoInterno;
            txtCalle.Enabled = vFgHabilitarCampoInterno;
            txtCP.Enabled = vFgHabilitarCampoInterno;
            txtNoexterior.Enabled = vFgHabilitarCampoInterno;
            txtNointerior.Enabled = vFgHabilitarCampoInterno;



            //txtCalle.Enabled = vFgHabilitarCampoInterno;
            //txtCP.Enabled = vFgHabilitarCampoInterno;
            //radBtnBuscaCP.Enabled = vFgHabilitarCampoInterno;
            //txtNoexterior.Enabled = vFgHabilitarCampoInterno;
            //txtNointerior.Enabled = vFgHabilitarCampoInterno;

            txtFeNacimiento.Enabled = vFgHabilitarCampoInterno;
            //txtEmail.Enabled = vFgHabilitarCampoInterno;
            //radBtnBuscarestado.Enabled = vFgHabilitarCampoInterno;
            //radBtnBuscarmunicipio.Enabled = vFgHabilitarCampoInterno;
            //radBtnBuscarcolonia.Enabled = vFgHabilitarCampoInterno;
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

                //vIdItemFotografia = vDocumento.ID_ITEM;
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

                    if (vControl.ClTipoControl == "TEXTBOX")
                    pvwCamposExtras.Controls.Add(new LiteralControl("<div style='Height: 110px; clear:both;'></div>"));
                    else
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
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensajes, E_TIPO_RESPUESTA_DB.WARNING);
                return null;
            }
        }

        #endregion

        protected void SeguridadProcesos()
        {
            radBtnGuardar.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.B.D");
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            InstructorNegocio neg = new InstructorNegocio();
            CrearFormulario(XElement.Parse(neg.ObtieneCampoAdicionalXml("C_INSTRUCTOR")));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;

            if (!Page.IsPostBack)
            {
                InstructorNegocio nInstructor = new InstructorNegocio();
                int vInstructorIdQS = -1;
                vClOperacion = E_TIPO_OPERACION_DB.I;
                vIdInstructor = Guid.NewGuid();
                vClRutaArchivosTemporales = Server.MapPath(ContextoApp.ClRutaArchivosTemporales);

                //vLstCompetencia = new List<E_INSTRUCTOR_COMPETENCIA>();
                vLstCurso = new List<E_INSTRUCTOR_CURSO>();
                vLstTelefono = new List<E_TELEFONO>();
                vLstDocumentos = new List<E_DOCUMENTO>();
                vXmlAdicionales = nInstructor.ObtieneCampoAdicionalXml("C_INSTRUCTOR");

                if (ContextoInstructor.oInstructores == null)
                {
                    ContextoInstructor.oInstructores = new List<E_INSTRUCTOR>();
                }

                ContextoInstructor.oInstructores.Add(new E_INSTRUCTOR { ID_ITEM = vIdInstructor });

                if (int.TryParse(Request.QueryString["InstructorId"], out vInstructorIdQS))
                {
                    vInstructorId = vInstructorIdQS;
                    vClOperacion = E_TIPO_OPERACION_DB.A;

                    CargarDatos(vInstructorId);
                    CargarDocumentos();

                    
                }
                else
                {

                    HabilitaCampos(false, true);

                    RadListBoxItem vItmEstado = new RadListBoxItem("No seleccionado", String.Empty);
                    rlbEstado.Items.Add(vItmEstado);

                    RadListBoxItem vItmMunicipio = new RadListBoxItem("No seleccionado", String.Empty);
                    rlbMunicipio.Items.Add(vItmMunicipio);

                    RadListBoxItem vItmColonia = new RadListBoxItem("No seleccionado", String.Empty);
                    rlbcolonia.Items.Add(vItmColonia);

                    grdInstructorCurso.DataSource = vLstCurso;
                    grdInstructorCurso.DataBind();

                    grdInstructorCompetencia.DataSource = vInstructor.LstCompetencias;
                    grdInstructorCompetencia.DataBind();

                    InstructorNegocio nTipoTelefono = new InstructorNegocio();
                    List<SPE_OBTIENE_C_CATALOGO_VALOR_Result> lista = nTipoTelefono.ObtieneTiposTelefono("TELEFONO_TIPOS");

                    vLstTipoTelefono = lista.Select(el => new E_TIPO_TELEFONO
                    {
                        NB_TEXTO = el.NB_CATALOGO_VALOR,
                        NB_VALOR = el.CL_CATALOGO_VALOR
                    }).ToList();

                    cmbIdTipoTelefono.DataSource = vLstTipoTelefono;
                    cmbIdTipoTelefono.DataTextField = "NB_TEXTO";
                    cmbIdTipoTelefono.DataValueField = "NB_VALOR";
                    cmbIdTipoTelefono.DataBind();
                }

                SeguridadProcesos();
            }

            DespacharEventos(Request.Params.Get("__EVENTTARGET"), Request.Params.Get("__EVENTARGUMENT"));
        }

        protected void grdInstructorCurso_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdInstructorCurso.DataSource = vLstCurso;
        }

        protected void grdInstructorCompetencia_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdInstructorCompetencia.DataSource = vInstructor.LstCompetencias;
        }

        protected void grdTelefono_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdTelefono.DataSource = vLstTelefono;
        }

        protected void rbInstInterno_Click(object sender, EventArgs e)
        {
            HabilitaCampos(false, true);
            limpiarCampos();
        }

        protected void rbInstExterno_Click(object sender, EventArgs e)
        {
            HabilitaCampos(true, false);
            limpiarCampos();
        }

        private void limpiarCampos()
        {
            List<string> vEstado = new List<string>();
            List<string> vMunicipio = new List<string>();
            List<string> vColonia = new List<string>();
            vLstTelefono = new List<E_TELEFONO>();

            vEstado.Clear();
            vEstado.Add("No seleccionado");
            rlbEstado.DataSource = vEstado;
            rlbEstado.SelectedIndex = 0;
            rlbEstado.DataBind();

            vMunicipio.Clear();
            vMunicipio.Add("No seleccionado");
            rlbcolonia.DataSource = vEstado;
            rlbcolonia.SelectedIndex = 0;
            rlbcolonia.DataBind();

            vColonia.Clear();
            vColonia.Add("No seleccionado");
            rlbMunicipio.DataSource = vEstado;
            rlbMunicipio.SelectedIndex = 0;
            rlbMunicipio.DataBind();

            txtClave.Text = "";
            txtNombre.Text = "";
            txtNombreValIns.Text = "";
            txtRFC.Text = "";
            txtCURP.Text = "";
            txtRegistro.Text = "";
            txtCP.Text = "";
            txtCalle.Text = "";
            txtNoexterior.Text = "";
            txtNointerior.Text = "";
            txtEscolaridad.Text = "";
            txtFeNacimiento.SelectedDate = null;
            txtEmail.Text = "";
            txtCostoHora.Text = "";
            txtCostoPart.Text = "";
            txtEvidencia.Text = "";

            grdTelefono.Rebind();
        }

        protected void radBtnEliminarCompetencia_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem i in grdInstructorCompetencia.SelectedItems)
                vInstructor.LstCompetencias.RemoveAll(r => r.ID_ITEM.Equals(new Guid(i.GetDataKeyValue("ID_ITEM").ToString())));

            grdInstructorCompetencia.Rebind();
        }

        protected void radBtnEliminarCurso_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem i in grdInstructorCurso.SelectedItems)
                vLstCurso.RemoveAll(r => r.ID_ITEM.Equals(new Guid(i.GetDataKeyValue("ID_ITEM").ToString())));

            grdInstructorCurso.Rebind();
        }

        protected void radBtnAgregarTelefono_Click(object sender, EventArgs e)
        {
            var nbTelefono = mtxtTelParticular.Text;
            var clTipo = cmbIdTipoTelefono.SelectedValue;
            var nbTipo = cmbIdTipoTelefono.Text;

            if (nbTelefono != null & nbTelefono != "")
            {
                E_TELEFONO telefono = new E_TELEFONO();
                telefono.NB_TELEFONO = nbTelefono;
                telefono.CL_TIPO = clTipo;
                telefono.NB_TIPO = nbTipo;

                vLstTelefono.Add(telefono);
                grdTelefono.Rebind();
            }
            mtxtTelParticular.Text = "";
        }

        protected void radBtnEliminarTelefono_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem i in grdTelefono.SelectedItems)
                vLstTelefono.RemoveAll(r => r.ID_ITEM.Equals(new Guid(i.GetDataKeyValue("ID_ITEM").ToString())));

            grdTelefono.Rebind();
        }

        protected void radBtnGuardar_Click(object sender, EventArgs e)
        {
            XElement vXmlCA = GeneralXmlAdicionales();

            E_INSTRUCTOR VInstructorAgregar = new E_INSTRUCTOR();
            InstructorNegocio nInstructor = new InstructorNegocio();

            var ptipo = vClOperacion.ToString();

            if (rbInstInterno.Checked)
                VInstructorAgregar.CL_TIPO_INSTRUCTOR = "INTERNO";
            else
                VInstructorAgregar.CL_TIPO_INSTRUCTOR = "EXTERNO";

            VInstructorAgregar.CL_INTRUCTOR = txtClave.Text;
            VInstructorAgregar.NB_INSTRUCTOR = txtNombre.Text;
            VInstructorAgregar.NB_VALIDADOR = txtNombreValIns.Text;
            VInstructorAgregar.CL_RFC = txtRFC.Text;
            VInstructorAgregar.CL_CURP = txtCURP.Text;
            VInstructorAgregar.CL_STPS = txtRegistro.Text;
            VInstructorAgregar.CL_PAIS = null;
            VInstructorAgregar.NB_PAIS = null;
            foreach (RadListBoxItem item in rlbEstado.Items)
            {
                VInstructorAgregar.CL_ESTADO = item.Value;
                VInstructorAgregar.NB_ESTADO = item.Text;
            }
            foreach (RadListBoxItem item in rlbMunicipio.Items)
            {
                VInstructorAgregar.CL_MUNICIPIO = item.Value;
                VInstructorAgregar.NB_MUNICIPIO = item.Text;
            }
            foreach (RadListBoxItem item in rlbcolonia.Items)
            {
                VInstructorAgregar.CL_COLONIA = item.Value;
                VInstructorAgregar.NB_COLONIA = item.Text;
            }
            VInstructorAgregar.NB_CALLE = txtCalle.Text;
            VInstructorAgregar.NO_INTERIOR = txtNointerior.Text;
            VInstructorAgregar.NO_EXTERIOR = txtNoexterior.Text;
            VInstructorAgregar.CL_CODIGO_POSTAL = txtCP.Text;
            VInstructorAgregar.DS_ESCOLARIDAD = txtEscolaridad.Text;
            VInstructorAgregar.FE_NACIMIENTO = txtFeNacimiento.SelectedDate;
            VInstructorAgregar.CL_CORREO_ELECTRONICO = txtEmail.Text;
            VInstructorAgregar.MN_COSTO_HORA = (decimal?)(txtCostoHora.Value);
            VInstructorAgregar.MN_COSTO_PARTICIPANTE = (decimal?)txtCostoPart.Value;
            VInstructorAgregar.DS_EVIDENCIA_COMPETENCIA = txtEvidencia.Text;

            XElement vTelefonos = new XElement("TELEFONOS");
            if (vLstTelefono.Count > 0)
            {
                vTelefonos.Add(vLstTelefono.Select(i => new XElement("TELEFONO", new XAttribute("NO_TELEFONO", i.NB_TELEFONO), new XAttribute("CL_TIPO", (i.CL_TIPO==null?string.Empty:i.CL_TIPO)))));
            }


            XElement vCompetencias = new XElement("COMPETENCIAS");
            vCompetencias.Add(vInstructor.LstCompetencias.Select(i => new XElement("COMPETENCIA", new XAttribute("ID_COMPETENCIA", i.ID_COMPETENCIA))));

            XElement vCursos = new XElement("CURSOS");
            vCursos.Add(vLstCurso.Select(i => new XElement("CURSO", new XAttribute("ID_CURSO", i.ID_CURSO))));


            VInstructorAgregar.XML_TELEFONOS = vTelefonos.ToString();
            //VInstructorAgregar.XML_CURSOS = null; vLstCursos
            //VInstructorAgregar.XML_COMPETENCIAS = null; vLstCompetencia

            List<UDTT_ARCHIVO> vLstArchivos = new List<UDTT_ARCHIVO>();
            foreach (E_DOCUMENTO d in vLstDocumentos)
            {
                string vFilePath = Server.MapPath(Path.Combine(ContextoApp.ClRutaArchivosTemporales, d.GetDocumentFileName()));
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
                VInstructorAgregar.ID_INSTRUCTOR = vInstructorId;
            }
            //Guarda instructor
            E_RESULTADO vResultado = nInstructor.InsertaActualizaInstructor(ptipo, VInstructorAgregar, vCompetencias, vCursos, vLstArchivos, vLstDocumentos, vXmlCA, vClUsuario, vNbPrograma); // usuario: vClUsuario, programa: vNbPrograma, V_C_INSTRUCTOR: VInstructorAgregar, competencias: vCompetencias, cursos: vCursos);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);

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

    }

}