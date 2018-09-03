using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.IntegracionDePersonal;
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
using System.Data;
using System.IO;
using SIGE.Entidades;



namespace SIGE.WebApp.IDP
{
    public partial class ProcesoSeleccion : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        List<SPE_OBTIENE_EST_SOC_DEPENDIENTE_Result> vlstDependiente = new List<SPE_OBTIENE_EST_SOC_DEPENDIENTE_Result>();

        //public int vIdProcesoSeleccion
        //{
        //    get { return (int)ViewState["vs_ps_id_proceso_seleccion"]; }
        //    set { ViewState["vs_ps_id_proceso_seleccion"] = value; }
        //}

        public int? vIdRequisicion
        {
            get { return (int?)ViewState["vs_ps_id_requisicion"]; }
            set { ViewState["vs_ps_id_requisicion"] = value; }
        }

        public bool? fgCopiaProceso
        {
            get { return (bool?)ViewState["vs_fgCopiaProceso"]; }
            set { ViewState["vs_fgCopiaProceso"] = value; }
        }

        public int? vIdCandidato
        {
            get { return (int?)ViewState["vs_ps_id_candidato"]; }
            set { ViewState["vs_ps_id_candidato"] = value; }
        }

        public int? vIdEmpleado
        {
            get { return (int?)ViewState["vs_vIdEmpleado"]; }
            set { ViewState["vs_vIdEmpleado"] = value; }
        }

        public int? vIdPuesto
        {
            get { return (int?)ViewState["vs_ps_id_puesto"]; }
            set { ViewState["vs_ps_id_puesto"] = value; }
        }

        public int? vIdBateria
        {
            get { return (int?)ViewState["vs_vIdBateria"]; }
            set { ViewState["vs_vIdBateria"] = value; }
        }

        public Guid vClToken
        {
            get { return (Guid)ViewState["vs_ClToken"]; }
            set { ViewState["vs_ClToken"] = value; }
        }

        public int vIdProcesoSeleccion
        {
            get { return (int)ViewState["vs_ps_id_proceso_seleccion"]; }
            set { ViewState["vs_ps_id_proceso_seleccion"] = value; }
        }

        private string vClVentana
        {
            get { return (string)ViewState["vs_ps_cl_ventana"]; }
            set { ViewState["vs_ps_cl_ventana"] = value; }
        }

        private List<E_REFERENCIA_CANDIDATO> vLstReferencias
        {
            get { return (List<E_REFERENCIA_CANDIDATO>)ViewState["vs_ps_lst_referencias"]; }
            set { ViewState["vs_ps_lst_referencias"] = value; }
        }

        private List<E_CLASIFICACION> vLstClasificacion
        {
            get { return (List<E_CLASIFICACION>)ViewState["vs_ps_lista_clasificacion"]; }
            set { ViewState["vs_ps_lista_clasificacion"] = value; }
        }

        private List<E_COMPETENCIAS_PROCESO_SELECCION> vLstCompetencias
        {
            get { return (List<E_COMPETENCIAS_PROCESO_SELECCION>)ViewState["vs_ps_lst_competencias"]; }
            set { ViewState["vs_ps_lst_competencias"] = value; }
        }

        private int vNoOrden
        {
            get { return (int)ViewState["vs_ps_no_orden"]; }
            set { ViewState["vs_ps_no_orden"] = value; }
        }

        private string vNbClasificacion
        {
            get { return (string)ViewState["vs_ps_nb_clasificacion"]; }
            set { ViewState["vs_ps_nb_clasificacion"] = value; }
        }

        private string vClColor
        {
            get { return (string)ViewState["vs_ps_cl_color"]; }
            set { ViewState["vs_ps_cl_color"] = value; }
        }

        private int vIdIndex
        {
            get { return (int)ViewState["vs_ps_id_index"]; }
            set { ViewState["vs_ps_id_index"] = value; }
        }

        List<E_DOCUMENTO> vLstDocumentos
        {
            get { return (List<E_DOCUMENTO>)ViewState["vs_LstDocumentos"]; }
            set { ViewState["vs_LstDocumentos"] = value; }
        }
        string vClRutaArchivosTemporales;
        string vXmlDocumentos;

        E_RESULTADO_MEDICO vResultadoMedico
        {
            get { return (E_RESULTADO_MEDICO)ViewState["vs_ps_resultado_medico"]; }
            set { ViewState["vs_ps_resultado_medico"] = value; }
        }


        E_ESTUDIO_SOCIOECONOMICO vEstudioSocioEconomico
        {
            get { return (E_ESTUDIO_SOCIOECONOMICO)ViewState["vs_ps_estudio_socioeconomico"]; }
            set { ViewState["vs_ps_estudio_socioeconomico"] = value; }
        }

        E_ES_DATOS_LABORALES vDatosLaborales   
        {
            get { return (E_ES_DATOS_LABORALES)ViewState["vs_ps_datos_laborales"]; }
            set { ViewState["vs_ps_datos_laborales"] = value; }
        }

        E_ES_DATOS_ECONOMICOS vDatosEconomicos
        {
            get { return (E_ES_DATOS_ECONOMICOS)ViewState["vs_ps_datos_economicos"]; }
            set { ViewState["vs_ps_datos_economicos"] = value; }
        }

        E_ES_DATOS_VIVIENDA vDatosVivienda
        {
            get { return (E_ES_DATOS_VIVIENDA)ViewState["vs_ps_datos_vivienda"]; }
            set { ViewState["vs_ps_datos_vivienda"] = value; }
        }

        #endregion

        #region Funciones

        private void CargarDatosIniciales()
        {
            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();

            if (vIdProcesoSeleccion == 0)
            {
                E_RESULTADO vResultado = nProcesoSeleccion.InsertaProcesoSeleccion(vIdCandidato,null, vIdRequisicion, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                    return;
                }
                else
                {
                    XElement vDatosRespuesta = vResultado.ObtieneDatosRespuesta();
                    XElement vNuevoProcesoSeleccion = vDatosRespuesta.Element("PROCESO_SELECCION");
                    vIdProcesoSeleccion = UtilXML.ValorAtributo<int>(vNuevoProcesoSeleccion.Attribute("ID_PROCESO_SELECCION"));
                }
            }

            var vProcesoSeleccion = nProcesoSeleccion.ObtieneProcesoSeleccion(pIdProcesoSeleccion: vIdProcesoSeleccion).FirstOrDefault();

            if (vProcesoSeleccion != null)
            {
                txtFolio.InnerText = vProcesoSeleccion.CL_SOLICITUD;
                txtCandidato.InnerText = vProcesoSeleccion.NB_CANDIDATO_COMPLETO;
                txtClaveRequisicion.InnerText = vProcesoSeleccion.NO_REQUISICION;
                txtPuestoAplicar.InnerText = vProcesoSeleccion.NB_PUESTO;
                txtFechaInicio.InnerText = vProcesoSeleccion.FE_INICIO_PROCESO.ToShortDateString();
                if (vProcesoSeleccion.FE_TERMINO_PROCESO != null)
                {
                    lbTerminoc.Visible = true;
                    txtFechaTerminoc.Visible = true;
                    txtFechaTermino.InnerText = vProcesoSeleccion.FE_TERMINO_PROCESO.Value.ToString("dd/MM/yyyy");
                }
                vIdPuesto = vProcesoSeleccion.ID_PUESTO;
                vIdProcesoSeleccion = vProcesoSeleccion.ID_PROCESO_SELECCION;
                reOBservacionProceso.Content = vProcesoSeleccion.DS_OBSERVACIONES_TERMINO_PROCESO;

                if (vProcesoSeleccion.CL_ESTADO.ToUpper() == "TERMINADO")
                {
                    btnTerminarProceso.Enabled = false;
                }

            }

            if (vProcesoSeleccion.FI_ARCHIVO != null)
            {
                rbiFotoEmpleado.DataValue = vProcesoSeleccion.FI_ARCHIVO;

            }

            // CargarCompetencias();
            CargarDatosExamenMedico(true);
            CargarDatosEstudioSocioEconomico(true);
            CargarDatosESDatosLaborales(true);
            CargarDatosESDatosEconomicos(true);
            CargarESDatosVivienda(true);
        }

        private void EnviarCorreo(bool pFgEnviarTodos)
        {
            ProcesoExterno pe = new ProcesoExterno();

            int vNoCorreosEnviados = 0;
            int vNoTotalCorreos = 0;
            string vClCorreo;
            string vNbEntrevistador;


            string myUrl = ResolveUrl("~/Logon.aspx?ClProceso=ENTREVISTA_SELECCION");
            string vUrl = ContextoUsuario.nbHost + myUrl;
            GridItemCollection oListaEvaluadores = new GridItemCollection();

            if (pFgEnviarTodos)
                oListaEvaluadores = rgEntrevistas.Items;
            else
                oListaEvaluadores = rgEntrevistas.SelectedItems;

            vNoTotalCorreos = oListaEvaluadores.Count;

            foreach (GridDataItem item in oListaEvaluadores)
            {
                string vMensaje = ContextoApp.IDP.MensajeCorreoEntrevista.dsMensaje;
                vClCorreo = item["CL_CORREO_ENTREVISTADOR"].Text;
                vNbEntrevistador = item["NB_ENTREVISTADOR"].Text;

                if (Utileria.ComprobarFormatoEmail(vClCorreo))
                {
                    if (item.GetDataKeyValue("FL_ENTREVISTA") != null)
                    {
                        vMensaje = vMensaje.Replace("[NB_ENTREVISTADOR]", vNbEntrevistador);
                        vMensaje = vMensaje.Replace("[URL]", vUrl + "&FlProceso=" + item.GetDataKeyValue("FL_ENTREVISTA").ToString());
                        vMensaje = vMensaje.Replace("[CONTRASENA]", item.GetDataKeyValue("CL_TOKEN").ToString());
                        vMensaje = vMensaje.Replace("[NB_CANDIDATO]", txtCandidato.InnerText);
                        vMensaje = vMensaje.Replace("[NB_PUESTO]", txtPuestoAplicar.InnerText);

                        bool vEstatusCorreo = pe.EnvioCorreo(vClCorreo, vNbEntrevistador, "Comentarios de entrevista", vMensaje);

                        if (vEstatusCorreo)
                        {
                            vNoCorreosEnviados++;
                        }
                    }
                }
            }

            if (vNoTotalCorreos == vNoCorreosEnviados)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Los correos han sido enviados con éxito.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: "");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Se enviaron " + vNoCorreosEnviados.ToString() + " correos de " + vNoTotalCorreos.ToString() + " en total.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: "");
            }
        }

        //private void CargarCompetencias()
        //{
        //ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();

        //vLstCompetencias = nProcesoSeleccion.ObtieneCompetenciasProcesoSeleccion(vIdCandidato, vIdPuesto);



        //vLstClasificacion = (from c in vLstCompetencias
        //                     group c by new { c.CL_CLASIFICACION, c.CL_COLOR, c.NO_ORDEN } into grp
        //                     orderby grp.Key.NO_ORDEN ascending
        //                     select new E_CLASIFICACION
        //                     {
        //                         CL_CLASIFICACION = grp.Key.CL_CLASIFICACION,
        //                         CL_COLOR = grp.Key.CL_COLOR,
        //                         NO_ORDEN = grp.Key.NO_ORDEN

        //                     }).ToList();

        //if (vLstClasificacion.Count > 0)
        //{
        //    vNbClasificacion = vLstClasificacion.FirstOrDefault().CL_CLASIFICACION;
        //    vNoOrden = vLstClasificacion.FirstOrDefault().NO_ORDEN;
        //    vClColor = vLstClasificacion.FirstOrDefault().CL_COLOR;
        //    //divColorClas.Style.Add("background", vClColor);
        //}

        //vIdIndex = 0;
        // }

        private void AsignarTooltip()
        {
            foreach (GridDataItem item in dgvCompetencias.MasterTableView.Items)
            {
                if (item.DataItem != null)
                {
                    E_COMPETENCIAS_PROCESO_SELECCION p = (E_COMPETENCIAS_PROCESO_SELECCION)item.DataItem;
                    //(item.FindControl("rbnivel0") as RadButton).ToolTip = p.DS_NIVEL0;
                    (item.FindControl("rbnivel0") as RadButton).ToolTip = p.DS_NIVEL0;
                    (item.FindControl("rbnivel1") as RadButton).ToolTip = p.DS_NIVEL1;
                    (item.FindControl("rbnivel2") as RadButton).ToolTip = p.DS_NIVEL2;
                    (item.FindControl("rbnivel3") as RadButton).ToolTip = p.DS_NIVEL3;
                    (item.FindControl("rbnivel4") as RadButton).ToolTip = p.DS_NIVEL4;
                    (item.FindControl("rbnivel5") as RadButton).ToolTip = p.DS_NIVEL5;
                }
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

        private void CargarDatosExamenMedico(bool pAsignarDatos)
        {
            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();

            vResultadoMedico = nProcesoSeleccion.ObtieneResultadoMedico(pIdProcesoSeleccion: vIdProcesoSeleccion, pIdCandidato: vIdCandidato);

            if (vResultadoMedico.ID_RESULTADO_MEDICO != 0 & pAsignarDatos)
            {
                AsignarDatosExamenMedico();
            }
            else if (pAsignarDatos)
            {
                chkEMEnfermedadNo.Checked = true;
                chkEMMedicamentosNo.Checked = true;
                chkEMAlergiasNo.Checked = true;
                chkEMAntecedentesNo.Checked = true;
                chkEMCirujiasNo.Checked = true;
                chkEMAdecuadoNo.Checked = true;
            }


        }

        private void AsignarDatosExamenMedico()
        {
            txtEMedad.Text = vResultadoMedico.NO_EDAD.ToString() == "0" ? "" : vResultadoMedico.NO_EDAD.ToString();
            txtEMtalla.Text = vResultadoMedico.NO_TALLA.ToString();
            txtEMPeso.Text = vResultadoMedico.NO_PESO.ToString();
            txtEMMasaCorporal.Text = vResultadoMedico.NO_INDICE_MASA_CORPORAL.ToString();
            txtEMPulso.Text = vResultadoMedico.NO_PULSO.ToString();
            txtEMPresionArterial.Text = vResultadoMedico.NO_PRESION_ARTERIAL;
            txtEMEmbarazos.Text = vResultadoMedico.NO_EMBARAZOS.ToString();
            txtEMHijos.Text = vResultadoMedico.NO_HIJOS.ToString();

            if (string.IsNullOrEmpty(vResultadoMedico.XML_ENFERMEDADES))
            {
                chkEMEnfermedadNo.Checked = true;
            }
            else
            {
                chkEMEnfermedadSi.Checked = true;
                txtEmEnfermedadComentario.Text = vResultadoMedico.XML_ENFERMEDADES;
            }

            if (string.IsNullOrEmpty(vResultadoMedico.XML_MEDICAMENTOS))
            {
                chkEMMedicamentosNo.Checked = true;
            }
            else
            {
                chkEMMedicamentosSi.Checked = true;
                txtEMMedicamentosComentarios.Text = vResultadoMedico.XML_MEDICAMENTOS;
            }

            if (string.IsNullOrEmpty(vResultadoMedico.XML_ALERGIAS))
            {
                chkEMAlergiasNo.Checked = true;
            }
            else
            {
                chkEMAlergiasSi.Checked = true;
                txtEMAlergiasComentarios.Text = vResultadoMedico.XML_ALERGIAS;
            }

            if (string.IsNullOrEmpty(vResultadoMedico.XML_ANTECEDENTES))
            {
                chkEMAntecedentesNo.Checked = true;
            }
            else
            {
                chkEMAntecedentesSi.Checked = true;
                txtEMAntecedentesComentarios.Text = vResultadoMedico.XML_ANTECEDENTES;
            }

            if (string.IsNullOrEmpty(vResultadoMedico.XML_INTERVENCIONES_QUIRURJICAS))
            {
                chkEMCirujiasNo.Checked = true;
            }
            else
            {
                chkEMCirujiasSi.Checked = true;
                txtEMCirujiasComentarios.Text = vResultadoMedico.XML_INTERVENCIONES_QUIRURJICAS;
            }

            reObservaciones.Content = vResultadoMedico.DS_OBSERVACIONES;
            if (vResultadoMedico.FG_ADECUADO.Value == true)
                chkEMAdecuadoSi.Checked = true;
            else
                chkEMAdecuadoNo.Checked = true;
        }
        
        private void GuardarDatosExamenMedico()
        {
            string vTipoAccion = "";
            int? vIdResultadoMedico;

            if (ValidarDatosExamenMedico())
            {

                if (vResultadoMedico.ID_RESULTADO_MEDICO != 0)
                {
                    vTipoAccion = E_TIPO_OPERACION_DB.A.ToString();
                    vIdResultadoMedico = vResultadoMedico.ID_RESULTADO_MEDICO;
                }
                else
                {
                    vTipoAccion = E_TIPO_OPERACION_DB.I.ToString();
                    vIdResultadoMedico = null;
                }

                vResultadoMedico.NO_EDAD = txtEMedad.Text != "" ? int.Parse(txtEMedad.Text) : 0;
                vResultadoMedico.NO_TALLA = txtEMtalla.Text;
                vResultadoMedico.NO_PESO = txtEMPeso.Text != "" ? decimal.Parse(txtEMPeso.Text) : 0;
                vResultadoMedico.NO_INDICE_MASA_CORPORAL = txtEMMasaCorporal.Text != "" ? decimal.Parse(txtEMMasaCorporal.Text) : 0;
                vResultadoMedico.NO_PULSO = txtEMPulso.Text != "" ? int.Parse(txtEMPulso.Text) : 0;
                vResultadoMedico.NO_PRESION_ARTERIAL = txtEMPresionArterial.Text;
                vResultadoMedico.NO_EMBARAZOS = txtEMEmbarazos.Text != "" ? int.Parse(txtEMEmbarazos.Text) : 0;
                vResultadoMedico.NO_HIJOS = txtEMHijos.Text != "" ? int.Parse(txtEMHijos.Text) : 0;

                if (chkEMEnfermedadSi.Checked)
                {
                    vResultadoMedico.XML_ENFERMEDADES = txtEmEnfermedadComentario.Text;
                }
                else
                {
                    vResultadoMedico.XML_ENFERMEDADES = null;
                }

                if (chkEMMedicamentosSi.Checked)
                {
                    vResultadoMedico.XML_MEDICAMENTOS = txtEMMedicamentosComentarios.Text;
                }
                else
                {
                    vResultadoMedico.XML_MEDICAMENTOS = null;
                }

                if (chkEMAlergiasSi.Checked)
                {
                    vResultadoMedico.XML_ALERGIAS = txtEMAlergiasComentarios.Text;
                }
                else
                {
                    vResultadoMedico.XML_ALERGIAS = null;
                }

                if (chkEMAntecedentesSi.Checked)
                {
                    vResultadoMedico.XML_ANTECEDENTES = txtEMAntecedentesComentarios.Text;
                }
                else
                {
                    vResultadoMedico.XML_ANTECEDENTES = null;
                }

                if (chkEMCirujiasSi.Checked)
                {
                    vResultadoMedico.XML_INTERVENCIONES_QUIRURJICAS = txtEMCirujiasComentarios.Text;
                }
                else
                {
                    vResultadoMedico.XML_INTERVENCIONES_QUIRURJICAS = null;
                }

                vResultadoMedico.DS_OBSERVACIONES = reObservaciones.Content;
                vResultadoMedico.FG_ADECUADO = chkEMAdecuadoSi.Checked;
                vResultadoMedico.ID_CANDIDATO = vIdCandidato;
                vResultadoMedico.ID_PROCESO_SELECCION = vIdProcesoSeleccion;


                ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
                E_RESULTADO vRespuesta = nProcesoSeleccion.InsertaActualizaResultadoMedico(vResultadoMedico, vNbPrograma, vClUsuario, vTipoAccion, vIdResultadoMedico);
                string vMensaje = vRespuesta.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vRespuesta.CL_TIPO_ERROR, pCallBackFunction: "");

                if (vTipoAccion == E_TIPO_OPERACION_DB.I.ToString())
                {
                    CargarDatosExamenMedico(false);
                }
            }
        }

        private bool ValidarDatosExamenMedico()
        {

            //if (string.IsNullOrEmpty(txtEMedad.Text))
            //{
            //    UtilMensajes.MensajeResultadoDB(rnMensaje, "Indica las edad", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            //    return false;
            //}

            if (chkEMEnfermedadSi.Checked & string.IsNullOrEmpty(txtEmEnfermedadComentario.Text))
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Indica las enfermedades actuales", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            if (chkEMMedicamentosSi.Checked & string.IsNullOrEmpty(txtEMMedicamentosComentarios.Text))
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Indica los medicamentos que toma", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            if (chkEMAlergiasSi.Checked & string.IsNullOrEmpty(txtEMAlergiasComentarios.Text))
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Indica las alergias", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            if (chkEMAntecedentesSi.Checked & string.IsNullOrEmpty(txtEMAntecedentesComentarios.Text))
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Indica los antecedentes familiares", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            if (chkEMCirujiasSi.Checked & string.IsNullOrEmpty(txtEMCirujiasComentarios.Text))
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Indica las cirujias", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            return true;
        }

        protected void CargarDocumentos()
        {
            ProcesoSeleccionNegocio nProceso = new ProcesoSeleccionNegocio();
            SPE_OBTIENE_DOCUMENTO_PROCESO_Result vDocumento = nProceso.ObtenieneDocumentoProceso(vIdCandidato);

            XElement vXmlDocumentos = vDocumento.XML_DOCUMENTOS != "" ? XElement.Parse(vDocumento.XML_DOCUMENTOS) : null;

            if (vLstDocumentos == null)
                vLstDocumentos = new List<E_DOCUMENTO>();

            if (vXmlDocumentos != null)
            {

                foreach (XElement item in (vXmlDocumentos.Elements("ITEM")))
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

        private void CargarCatalogos()
        {
            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();

            var vLstEstadoCivil = nProcesoSeleccion.ObtieneCatalogoValor(pIdCatalogoLista: ContextoApp.IdCatalogoEstadosCivil);

            cmbEstadoCivil.DataSource = vLstEstadoCivil;
            cmbEstadoCivil.DataTextField = "NB_CATALOGO_VALOR";
            cmbEstadoCivil.DataValueField = "CL_CATALOGO_VALOR";
            cmbEstadoCivil.DataBind();
            cmbEstadoCivil.EmptyMessage = "Seleccione...";

            var vLstParentesco = nProcesoSeleccion.ObtieneCatalogoValor(pIdCatalogoLista: ContextoApp.IdCatalogoParentescos);


            cmbParentesco.DataSource = vLstParentesco;
            cmbParentesco.DataTextField = "NB_CATALOGO_VALOR";
            cmbParentesco.DataValueField = "CL_CATALOGO_VALOR";
            cmbParentesco.DataBind();
            cmbParentesco.EmptyMessage = "Seleccione...";
           // cmbParentesco.SelectedIndex = 0;

            var vLstOcupacion = nProcesoSeleccion.ObtieneCatalogoValor(pIdCatalogoLista: ContextoApp.IdCatalogoOcupaciones);

            cmbOcupacion.DataSource = vLstOcupacion;
            cmbOcupacion.DataTextField = "NB_CATALOGO_VALOR";
            cmbOcupacion.DataValueField = "CL_CATALOGO_VALOR";
            cmbOcupacion.DataBind();
           // cmbOcupacion.SelectedIndex = 0;
            cmbOcupacion.EmptyMessage = "Seleccione...";

        }

        private void CargarDatosEstudioSocioEconomico(bool pAsignarDatos)
        {
            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();

            vEstudioSocioEconomico = nProcesoSeleccion.ObtieneEstudioSocioeconomico(pIdProcesoSeleccion: vIdProcesoSeleccion, pIdCandidato: vIdCandidato);

            if (vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO != 0 & pAsignarDatos)
            {
                AsignarDatosEstudioSocioEconomico();
            }

        }

        private void AsignarDatosEstudioSocioEconomico()
        {
            dtpESNacimiento.SelectedDate = vEstudioSocioEconomico.FE_NACIMIENTO;

            if (vEstudioSocioEconomico.NO_EDAD.HasValue)
            {
                txtESEdad.Text = vEstudioSocioEconomico.NO_EDAD.Value.ToString();
                txtEMedad.Text = vEstudioSocioEconomico.NO_EDAD.Value.ToString();
            }


            cmbEstadoCivil.SelectedValue = vEstudioSocioEconomico.CL_ESTADO_CIVIL;
            txtESrfc.Text = vEstudioSocioEconomico.CL_RFC;
            txtESCurp.Text = vEstudioSocioEconomico.CL_CURP;
            txtESSeguroSocial.Text = vEstudioSocioEconomico.CL_NSS;

            rlbEstado.Items.Clear();
            if (string.IsNullOrEmpty(vEstudioSocioEconomico.CL_ESTADO))
            {
                rlbEstado.Items.Add(new RadListBoxItem { Text = "No seleccionado", Value = "" });
            }
            else
            {
                rlbEstado.Items.Add(new RadListBoxItem { Text = vEstudioSocioEconomico.NB_ESTADO, Value = vEstudioSocioEconomico.CL_ESTADO });
                rlbEstado.SelectedIndex = 0;
            }

            rlbMunicipios.Items.Clear();
            if (string.IsNullOrEmpty(vEstudioSocioEconomico.CL_MUNICIPIO))
            {
                rlbMunicipios.Items.Add(new RadListBoxItem { Text = "No seleccionado", Value = "" });
            }
            else
            {
                rlbMunicipios.Items.Add(new RadListBoxItem { Text = vEstudioSocioEconomico.NB_MUNICIPIO, Value = vEstudioSocioEconomico.CL_MUNICIPIO });
                rlbMunicipios.SelectedIndex = 0;
            }


            rlbColonia.Items.Clear();
            if (string.IsNullOrEmpty(vEstudioSocioEconomico.CL_COLONIA))
            {
                rlbColonia.Items.Add(new RadListBoxItem { Text = "No seleccionado", Value = "" });
            }
            else
            {
                rlbColonia.Items.Add(new RadListBoxItem { Text = vEstudioSocioEconomico.NB_COLONIA, Value = vEstudioSocioEconomico.CL_COLONIA });
            }


            txtESCalle.Text = vEstudioSocioEconomico.NB_CALLE;
            txtESNoExt.Text = vEstudioSocioEconomico.NO_EXTERIOR;
            txtESNoInt.Text = vEstudioSocioEconomico.NO_INTERIOR;
            txtESCodigoPostal.Text = vEstudioSocioEconomico.CL_CODIGO_POSTAL;
            txtEsTiempoResidencia.Text = vEstudioSocioEconomico.NO_TIEMPO_RESIDENCIA.HasValue ? vEstudioSocioEconomico.NO_TIEMPO_RESIDENCIA.Value.ToString() : "";
            txtESTipoSanguineo.Text = vEstudioSocioEconomico.CL_TIPO_SANGUINEO;
            txtESIdentificacion.Text = vEstudioSocioEconomico.DS_IDENTIFICACION_OFICIAL;
            txtESIdentificacionFolio.Text = vEstudioSocioEconomico.CL_IDENTIFICACION_OFICIAL;

            cmbESServiciosMedicos.SelectedValue = vEstudioSocioEconomico.CL_SERVICIOS_MEDICOS;
            txtESServiciosMedicosComentarios.Text = vEstudioSocioEconomico.DS_SERVICIOS_MEDICOS;

            if (vEstudioSocioEconomico.XML_TELEFONOS != null)
            {
                XElement xmlTelefonos = XElement.Parse(vEstudioSocioEconomico.XML_TELEFONOS);

                var telefonos = xmlTelefonos.Elements("TELEFONO").Select(el => new E_TIPO_TELEFONO
                {
                    TELEFONO = el.Attribute("NO_TELEFONO").Value,
                    TIPO = el.Attribute("CL_TIPO").Value

                }).ToList();

                var telmovil = telefonos.Where(w => w.TIPO.Equals("MOVIL")).FirstOrDefault();
                var telfijo = telefonos.Where(w => w.TIPO.Equals("FIJO")).FirstOrDefault();

                txtESTelefonoMovil.Text = (telmovil != null) ? telmovil.TELEFONO : "";
                txtESTelefono.Text = (telfijo != null) ? telfijo.TELEFONO : "";

            }
        }

        private void GuardarDatosEstudioSocioEconomico()
        {
            string vTipoAccion = "";
            int? vIdEstudioSocioeconomico;

            if (vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO != 0)
            {
                vTipoAccion = E_TIPO_OPERACION_DB.A.ToString();
                vIdEstudioSocioeconomico = vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO;
            }
            else
            {
                vTipoAccion = E_TIPO_OPERACION_DB.I.ToString();
                vEstudioSocioEconomico.FE_REALIZACION = DateTime.Now;
                vIdEstudioSocioeconomico = null;
            }

            vEstudioSocioEconomico.FE_NACIMIENTO = dtpESNacimiento.SelectedDate;

            if (!string.IsNullOrEmpty(txtESEdad.Text))
                vEstudioSocioEconomico.NO_EDAD = byte.Parse(txtESEdad.Text);

            vEstudioSocioEconomico.CL_ESTADO_CIVIL = cmbEstadoCivil.SelectedValue;
            vEstudioSocioEconomico.CL_RFC = txtESrfc.Text;
            vEstudioSocioEconomico.CL_CURP = txtESCurp.Text;
            vEstudioSocioEconomico.CL_NSS = txtESSeguroSocial.Text;

            if (rlbEstado.Items[0].Value != "")
            {
                vEstudioSocioEconomico.NB_ESTADO = rlbEstado.Items[0].Text;
                vEstudioSocioEconomico.CL_ESTADO = rlbEstado.Items[0].Value;
            }
            else
            {
                vEstudioSocioEconomico.NB_ESTADO = "";
                vEstudioSocioEconomico.CL_ESTADO = "";
            }

            if (rlbMunicipios.Items[0].Value != "")
            {
                vEstudioSocioEconomico.NB_MUNICIPIO = rlbMunicipios.Items[0].Text;
                vEstudioSocioEconomico.CL_MUNICIPIO = rlbMunicipios.Items[0].Value;
            }
            else
            {
                vEstudioSocioEconomico.NB_MUNICIPIO = "";
                vEstudioSocioEconomico.CL_MUNICIPIO = "";
            }

            if (rlbColonia.Items[0].Value != "")
            {
                vEstudioSocioEconomico.NB_COLONIA = rlbColonia.Items[0].Text;
                vEstudioSocioEconomico.CL_COLONIA = rlbColonia.Items[0].Value;
            }
            else
            {
                vEstudioSocioEconomico.NB_COLONIA = "";
                vEstudioSocioEconomico.CL_COLONIA = "";
            }


            vEstudioSocioEconomico.NB_CALLE = txtESCalle.Text;
            vEstudioSocioEconomico.NO_EXTERIOR = txtESNoExt.Text;
            vEstudioSocioEconomico.NO_INTERIOR = txtESNoInt.Text;
            vEstudioSocioEconomico.CL_CODIGO_POSTAL = txtESCodigoPostal.Text;
            //TELEFONOS

            XElement vXmlElementTelefonos =
                   new XElement("TELEFONOS",
                        new XElement("TELEFONO",
                            new XAttribute("NO_TELEFONO", txtESTelefono.Text),
                            new XAttribute("CL_TIPO", "FIJO")),
                       new XElement("TELEFONO",
                            new XAttribute("NO_TELEFONO", txtESTelefonoMovil.Text),
                            new XAttribute("CL_TIPO", "MOVIL"))
                      );
            if (!string.IsNullOrEmpty(txtEsTiempoResidencia.Text))
                vEstudioSocioEconomico.NO_TIEMPO_RESIDENCIA = byte.Parse(txtEsTiempoResidencia.Text);


            vEstudioSocioEconomico.CL_SERVICIOS_MEDICOS = cmbESServiciosMedicos.SelectedValue;
            vEstudioSocioEconomico.DS_SERVICIOS_MEDICOS = txtESServiciosMedicosComentarios.Text;
            vEstudioSocioEconomico.CL_TIPO_SANGUINEO = txtESTipoSanguineo.Text;
            vEstudioSocioEconomico.DS_IDENTIFICACION_OFICIAL = txtESIdentificacion.Text;
            vEstudioSocioEconomico.CL_IDENTIFICACION_OFICIAL = txtESIdentificacionFolio.Text;

            vEstudioSocioEconomico.ID_CANDIDATO = vIdCandidato;
            vEstudioSocioEconomico.ID_PROCESO_SELECCION = vIdProcesoSeleccion;


            vEstudioSocioEconomico.XML_TELEFONOS = vXmlElementTelefonos.ToString();

            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
            E_RESULTADO vRespuesta = nProcesoSeleccion.InsertaActualizaEstudioSocioeconomico(vEstudioSocioEconomico, vClUsuario, vNbPrograma, vTipoAccion, vIdEstudioSocioeconomico);
            string vMensaje = vRespuesta.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vRespuesta.CL_TIPO_ERROR, pCallBackFunction: "");

            if (vTipoAccion == E_TIPO_OPERACION_DB.I.ToString())
            {
                CargarDatosEstudioSocioEconomico(false);
            }


        }

        private void CargarDatosESDatosLaborales(bool pAsignarDatos)
        {
            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();

            if (vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO != 0)
            {
                vDatosLaborales = nProcesoSeleccion.ObtieneESDatosLaborales(pIdEstudioSocioeconomico: vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO);

                if (vDatosLaborales.ID_DATO_LABORAL != 0 & pAsignarDatos)
                {
                    AsignarDatosESDatolLaborales();
                }
            }
        }

        private void AsignarDatosESDatolLaborales()
        {
            txtDLEmpresa.Text = vDatosLaborales.NB_EMPRESA;
            txtDLDomicilio.Text = vDatosLaborales.NB_DOMICILIO;
            txtDLEstado.Text = vDatosLaborales.NB_ESTADO;
            txtDLMunicipio.Text = vDatosLaborales.NB_MUNICIPIO;
            txtDLCodigoPostal.Text = vDatosLaborales.CL_CODIGO_POSTAL;
            txtDLPuesto.Text = vDatosLaborales.NB_PUESTO;
            txtDLSueldoInicial.Text = vDatosLaborales.MN_SALARIO_INICIAL.HasValue ? vDatosLaborales.MN_SALARIO_INICIAL.Value.ToString() : "";
            txtDLUltimoSueldo.Text = vDatosLaborales.MN_SALARIO_FINAL.HasValue ? vDatosLaborales.MN_SALARIO_FINAL.ToString() : "";
            cmbDLTipoEmpresa.SelectedValue = vDatosLaborales.CL_TIPO_EMPRESA;
            txtDLEspecificarTipoEmpresa.Text = vDatosLaborales.DS_TIPO_EMPRESA;
            txtDLAntiguedad.Text = vDatosLaborales.NO_ANTIGUEDAD_EMPRESA.HasValue ? vDatosLaborales.NO_ANTIGUEDAD_EMPRESA.Value.ToString() : "";
            rcmbTipoSuledo.SelectedValue = vDatosLaborales.CL_TIPO_SUELDO;
        }

        private void GuardarESDatosLaborales()
        {
            string vTipoAccion = "";
            int? vIdDatosLaborales;

            if (vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO != 0)
            {
                if (vDatosLaborales.ID_DATO_LABORAL != 0)
                {
                    vTipoAccion = E_TIPO_OPERACION_DB.A.ToString();
                    vIdDatosLaborales = vDatosLaborales.ID_DATO_LABORAL;
                }
                else
                {
                    vTipoAccion = E_TIPO_OPERACION_DB.I.ToString();
                    vIdDatosLaborales = null;
                }

                vDatosLaborales.CL_CODIGO_POSTAL = txtDLCodigoPostal.Text;
                vDatosLaborales.CL_TIPO_EMPRESA = cmbDLTipoEmpresa.SelectedValue;
                vDatosLaborales.DS_TIPO_EMPRESA = txtDLEspecificarTipoEmpresa.Text;
                vDatosLaborales.ID_ESTUDIO_SOCIOECONOMICO = vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO;

                vDatosLaborales.MN_SALARIO_FINAL = txtDLUltimoSueldo.Value.HasValue ? (decimal)txtDLUltimoSueldo.Value.Value : 0;
                vDatosLaborales.MN_SALARIO_INICIAL = txtDLSueldoInicial.Value.HasValue ? (decimal)txtDLSueldoInicial.Value.Value : 0;
                vDatosLaborales.NB_DOMICILIO = txtDLDomicilio.Text;
                vDatosLaborales.NB_EMPRESA = txtDLEmpresa.Text;
                vDatosLaborales.NB_ESTADO = txtDLEstado.Text;
                vDatosLaborales.NB_MUNICIPIO = txtDLMunicipio.Text;
                vDatosLaborales.NB_PUESTO = txtDLPuesto.Text;
                vDatosLaborales.NO_ANTIGUEDAD_EMPRESA = txtDLAntiguedad.Value.HasValue ? (decimal)txtDLAntiguedad.Value.Value : 0;
                vDatosLaborales.CL_TIPO_SUELDO = rcmbTipoSuledo.SelectedValue;
                vDatosLaborales.NB_TIPO_SUELDO = rcmbTipoSuledo.Text;


                ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
                E_RESULTADO vRespuesta = nProcesoSeleccion.InsertaActualizaESDatosLaborales(vDatosLaborales, vClUsuario, vNbPrograma, vTipoAccion, vIdDatosLaborales);
                string vMensaje = vRespuesta.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vRespuesta.CL_TIPO_ERROR, pCallBackFunction: "");

                if (vTipoAccion == E_TIPO_OPERACION_DB.I.ToString())
                {
                    CargarDatosESDatosLaborales(false);
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Completa la información principal: Datos personales, del estudio socioeconómico", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }
        }

        private void CargarDatosESDatosEconomicos(bool pAsignarDatos)
        {
            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();

            if (vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO != 0)
            {
                vDatosEconomicos = nProcesoSeleccion.ObtieneESDatosEconomicos(pIdEstudioSocioeconomico: vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO);

                if (vDatosEconomicos.ID_DATO_PROPIEDAD != 0 & pAsignarDatos)
                {
                    AsignarDatosESDatosEconomicos();
                }
            }

        }

        private void AsignarDatosESDatosEconomicos()
        {
            XElement vXmlIngresos;
            XElement vXmlEgresos;

            cmbDETipoPropiedad.SelectedValue = vDatosEconomicos.CL_TIPO_PROPIEDAD;
            cmbDEZona.SelectedValue = vDatosEconomicos.CL_TIPO_ZONA;
            cmbDEFormaAdquisicion.SelectedValue = vDatosEconomicos.CL_FORMA_ADQUISICION;
            txtDEFormaAdquisicionEspecificar.Text = vDatosEconomicos.DS_FORMA_ADQUISICION;

            string vClTipo = "";
            string vMnEgreso = "";
            string vMnIngreso = "";
            string vDSComentarios = "";
            string vNoIngreso = "";
            if (vEstudioSocioEconomico.XML_EGRESOS != null)
            {
                vXmlEgresos = XElement.Parse(vEstudioSocioEconomico.XML_EGRESOS);
                foreach (XElement item in vXmlEgresos.Elements("EGRESO"))
                {
                    vClTipo = item.Attribute("CL_TIPO").Value;
                    vMnEgreso = item.Attribute("MN_EGRESO").Value;

                    if (item.Attribute("DS_COMENTARIOS") != null)
                    {
                        vDSComentarios = item.Attribute("DS_COMENTARIOS").Value;
                    }

                    switch (vClTipo)
                    {
                        case "ALIMENTACION":
                            txtDEAlimentacion.Text = vMnEgreso;
                            break;

                        case "RENTA":
                            txtDERenta.Text = vMnEgreso;
                            break;

                        case "AGUA":
                            txtDEAgua.Text = vMnEgreso;
                            break;

                        case "LUZ":
                            txtDELuz.Text = vMnEgreso;
                            break;
                        case "TRANSPORTE":
                            txtDETransporte.Text = vMnEgreso;
                            break;

                        case "VESTIDO":
                            txtDEVestido.Text = vMnEgreso;
                            break;

                        case "DIVERSION":
                            txtDEDiversion.Text = vMnEgreso;
                            break;

                        case "PREDIAL":
                            txtDEPredial.Text = vMnEgreso;
                            break;

                        case "HIPOTECA":
                            txtDEHipoteca.Text = vMnEgreso;
                            break;

                        case "GASTOS_MEDICOS":
                            txtDEGastosMedicos.Text = vMnEgreso;
                            break;

                        case "GAS":
                            txtDEGas.Text = vMnEgreso;
                            break;

                        case "CABLE":
                            txtDECable.Text = vMnEgreso;
                            break;

                        case "TELEFONO":
                            txtDETelefono.Text = vMnEgreso;
                            break;

                        case "GASTOS_VEHICULO":
                            txtDEGastosVehiculo.Text = vMnEgreso;
                            break;

                        case "OTROS_GASTOS":
                            txtDEOtrosGastos.Text = vMnEgreso;
                            txtDEEspecificaOtrosGastos.Text = vDSComentarios;
                            break;
                    }
                }
            }
            if (vEstudioSocioEconomico.XML_INGRESOS != null)
            {
                vXmlIngresos = XElement.Parse(vEstudioSocioEconomico.XML_INGRESOS);
                foreach (var item in vXmlIngresos.Elements("INGRESO"))
                {
                    vNoIngreso = item.Attribute("NO_INGRESO").Value;
                    vMnIngreso = item.Attribute("MN_INGRESO").Value;

                    if (item.Attribute("DS_COMENTARIOS") != null)
                    {
                        vDSComentarios = item.Attribute("DS_COMENTARIOS").Value;
                    }

                    switch (vNoIngreso)
                    {
                        case "1":
                            txtDEIngreso1.Text = vMnIngreso;
                            txtDEIngreso1Comentarios.Text = vDSComentarios;
                            break;

                        case "2":
                            txtDEIngreso2.Text = vMnIngreso;
                            txtDEIngreso2Comentarios.Text = vDSComentarios;
                            break;

                        case "3":
                            txtDEIngreso3.Text = vMnIngreso;
                            txtDEIngreso3Comentarios.Text = vDSComentarios;
                            break;

                        case "4":
                            txtDEIngreso4.Text = vMnIngreso;
                            txtDEIngreso4Comentarios.Text = vDSComentarios;
                            break;
                    }
                }
            }
        }

        private void GuardarESDatosEconomicos()
        {
            string vTipoAccion = "";
            int? vIdDatosEconomicos;

            if (vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO != 0)
            {

                XElement vXmlEgresos = new XElement("EGRESOS");
                XElement vXmlIngresos = new XElement("INGRESOS");

                if (vDatosEconomicos.ID_DATO_PROPIEDAD != 0)
                {
                    vTipoAccion = E_TIPO_OPERACION_DB.A.ToString();
                    vIdDatosEconomicos = vDatosEconomicos.ID_DATO_PROPIEDAD;
                }
                else
                {
                    vTipoAccion = E_TIPO_OPERACION_DB.I.ToString();
                    vIdDatosEconomicos = null;
                }

                vDatosEconomicos.CL_TIPO_PROPIEDAD = cmbDETipoPropiedad.SelectedValue;
                vDatosEconomicos.CL_TIPO_ZONA = cmbDEZona.SelectedValue;
                vDatosEconomicos.CL_FORMA_ADQUISICION = cmbDEFormaAdquisicion.SelectedValue;
                vDatosEconomicos.DS_FORMA_ADQUISICION = txtDEFormaAdquisicionEspecificar.Text;


                if (txtDEAlimentacion.Value.HasValue)
                {
                    vXmlEgresos.Add(new XElement("EGRESO", new XAttribute("CL_TIPO", "ALIMENTACION"), new XAttribute("MN_EGRESO", txtDEAlimentacion.Text)));
                }

                if (txtDERenta.Value.HasValue)
                {
                    vXmlEgresos.Add(new XElement("EGRESO", new XAttribute("CL_TIPO", "RENTA"), new XAttribute("MN_EGRESO", txtDERenta.Text)));
                }

                if (txtDEAgua.Value.HasValue)
                {
                    vXmlEgresos.Add(new XElement("EGRESO", new XAttribute("CL_TIPO", "AGUA"), new XAttribute("MN_EGRESO", txtDEAgua.Text)));
                }

                if (txtDELuz.Value.HasValue)
                {
                    vXmlEgresos.Add(new XElement("EGRESO", new XAttribute("CL_TIPO", "LUZ"), new XAttribute("MN_EGRESO", txtDELuz.Text)));
                }

                if (txtDETransporte.Value.HasValue)
                {
                    vXmlEgresos.Add(new XElement("EGRESO", new XAttribute("CL_TIPO", "TRANSPORTE"), new XAttribute("MN_EGRESO", txtDETransporte.Text)));
                }

                if (txtDEVestido.Value.HasValue)
                {
                    vXmlEgresos.Add(new XElement("EGRESO", new XAttribute("CL_TIPO", "VESTIDO"), new XAttribute("MN_EGRESO", txtDEVestido.Text)));
                }

                if (txtDEDiversion.Value.HasValue)
                {
                    vXmlEgresos.Add(new XElement("EGRESO", new XAttribute("CL_TIPO", "DIVERSION"), new XAttribute("MN_EGRESO", txtDEDiversion.Text)));
                }

                if (txtDEPredial.Value.HasValue)
                {
                    vXmlEgresos.Add(new XElement("EGRESO", new XAttribute("CL_TIPO", "PREDIAL"), new XAttribute("MN_EGRESO", txtDEPredial.Text)));
                }

                if (txtDEHipoteca.Value.HasValue)
                {
                    vXmlEgresos.Add(new XElement("EGRESO", new XAttribute("CL_TIPO", "HIPOTECA"), new XAttribute("MN_EGRESO", txtDEHipoteca.Text)));
                }

                if (txtDEGastosMedicos.Value.HasValue)
                {
                    vXmlEgresos.Add(new XElement("EGRESO", new XAttribute("CL_TIPO", "GASTOS_MEDICOS"), new XAttribute("MN_EGRESO", txtDEGastosMedicos.Text)));
                }

                if (txtDEGas.Value.HasValue)
                {
                    vXmlEgresos.Add(new XElement("EGRESO", new XAttribute("CL_TIPO", "GAS"), new XAttribute("MN_EGRESO", txtDEGas.Text)));
                }

                if (txtDECable.Value.HasValue)
                {
                    vXmlEgresos.Add(new XElement("EGRESO", new XAttribute("CL_TIPO", "CABLE"), new XAttribute("MN_EGRESO", txtDECable.Text)));
                }

                if (txtDETelefono.Value.HasValue)
                {
                    vXmlEgresos.Add(new XElement("EGRESO", new XAttribute("CL_TIPO", "TELEFONO"), new XAttribute("MN_EGRESO", txtDETelefono.Text)));
                }

                if (txtDEGastosVehiculo.Value.HasValue)
                {
                    vXmlEgresos.Add(new XElement("EGRESO", new XAttribute("CL_TIPO", "GASTOS_VEHICULO"), new XAttribute("MN_EGRESO", txtDEGastosVehiculo.Text)));
                }

                if (txtDEOtrosGastos.Value.HasValue)
                {
                    vXmlEgresos.Add(new XElement("EGRESO", new XAttribute("CL_TIPO", "GASTOS_VEHICULO"), new XAttribute("MN_EGRESO", txtDEOtrosGastos.Text), new XAttribute("DS_COMENTARIOS", txtDEEspecificaOtrosGastos.Text)));
                }


                if (txtDEIngreso1.Value.HasValue)
                {
                    vXmlIngresos.Add(new XElement("INGRESO", new XAttribute("NO_INGRESO", "1"), new XAttribute("MN_INGRESO", txtDEIngreso1.Text), new XAttribute("DS_COMNETARIOS", txtDEIngreso1Comentarios.Text)));
                }

                if (txtDEIngreso2.Value.HasValue)
                {
                    vXmlIngresos.Add(new XElement("INGRESO", new XAttribute("NO_INGRESO", "2"), new XAttribute("MN_INGRESO", txtDEIngreso2.Text), new XAttribute("DS_COMNETARIOS", txtDEIngreso2Comentarios.Text)));
                }

                if (txtDEIngreso3.Value.HasValue)
                {
                    vXmlIngresos.Add(new XElement("INGRESO", new XAttribute("NO_INGRESO", "3"), new XAttribute("MN_INGRESO", txtDEIngreso3.Text), new XAttribute("DS_COMNETARIOS", txtDEIngreso3Comentarios.Text)));
                }

                if (txtDEIngreso4.Value.HasValue)
                {
                    vXmlIngresos.Add(new XElement("INGRESO", new XAttribute("NO_INGRESO", "3"), new XAttribute("MN_INGRESO", txtDEIngreso4.Text), new XAttribute("DS_COMNETARIOS", txtDEIngreso4Comentarios.Text)));
                }

                vDatosEconomicos.XML_EGRESOS = vXmlEgresos.ToString();
                vDatosEconomicos.XML_INGRESOS = vXmlIngresos.ToString();
                vDatosEconomicos.ID_ESTUDIO_SOCIOECONOMICO = vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO;

                ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
                E_RESULTADO vRespuesta = nProcesoSeleccion.InsertaActualizaESDatosEconomicos(vDatosEconomicos, vClUsuario, vNbPrograma, vTipoAccion, vIdDatosEconomicos);
                string vMensaje = vRespuesta.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vRespuesta.CL_TIPO_ERROR, pCallBackFunction: "");

                if (vTipoAccion == E_TIPO_OPERACION_DB.I.ToString())
                {
                    CargarDatosESDatosEconomicos(false);
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Completa la información principal: Datos personales, del estudio socioeconómico", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }
        }

        private void CargarESDatosVivienda(bool pAsignarDatos)
        {
            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();

            if (vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO != 0)
            {
                vDatosVivienda = nProcesoSeleccion.ObtieneESDatosVivienda(pIdEstudioSocioeconomico: vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO);

                if (vDatosEconomicos.ID_DATO_PROPIEDAD != 0 & pAsignarDatos)
                {
                    AsignarESDatosVivienda();
                }

            }
        }

        private void AsignarESDatosVivienda()
        {
            XElement vXmlServicio;
            XElement vXmlBienes;

            cmbDVTipoVivienda.SelectedValue = vDatosVivienda.CL_TIPO_VIVIENDA;
            cmbDVTipoConstruccion.SelectedValue = vDatosVivienda.CL_TIPO_CONSTRUCCION;

            if (vDatosVivienda.NO_HABITACIONES.HasValue)
                txtDVNoCuartos.Text = vDatosVivienda.NO_HABITACIONES.Value.ToString();

            if (vDatosVivienda.NO_BANIOS.HasValue)
                txtDVNoBaños.Text = vDatosVivienda.NO_BANIOS.Value.ToString();

            if (vDatosVivienda.NO_PATIOS.HasValue)
                txtDVNoPatios.Text = vDatosVivienda.NO_PATIOS.Value.ToString();

            if (vDatosVivienda.XML_SERVICIOS != null)
            {
                vXmlServicio = XElement.Parse(vDatosVivienda.XML_SERVICIOS);

                chkDVAgua.Checked = vXmlServicio.Elements("SERVICIO").Where(t => t.Attribute("CL_SERVICIO").Value == "AGUA").FirstOrDefault().Attribute("FG_SERVICIO").Value == "0" ? false : true;
                chkDVLuz.Checked = vXmlServicio.Elements("SERVICIO").Where(t => t.Attribute("CL_SERVICIO").Value == "LUZ").FirstOrDefault().Attribute("FG_SERVICIO").Value == "0" ? false : true;
                chkDVDrenaje.Checked = vXmlServicio.Elements("SERVICIO").Where(t => t.Attribute("CL_SERVICIO").Value == "DRENAJE").FirstOrDefault().Attribute("FG_SERVICIO").Value == "0" ? false : true;
                chkDVGas.Checked = vXmlServicio.Elements("SERVICIO").Where(t => t.Attribute("CL_SERVICIO").Value == "GAS").FirstOrDefault().Attribute("FG_SERVICIO").Value == "0" ? false : true;
                chkDVTelefono.Checked = vXmlServicio.Elements("SERVICIO").Where(t => t.Attribute("CL_SERVICIO").Value == "TELEFONO").FirstOrDefault().Attribute("FG_SERVICIO").Value == "0" ? false : true;
                chkDVOtros.Checked = vXmlServicio.Elements("SERVICIO").Where(t => t.Attribute("CL_SERVICIO").Value == "OTRO").FirstOrDefault().Attribute("FG_SERVICIO").Value == "0" ? false : true;
                txtDVOtrosServiciosComentarios.Text = vXmlServicio.Elements("SERVICIO").Where(t => t.Attribute("CL_SERVICIO").Value == "OTRO").FirstOrDefault().Attribute("DS_SERVICIO").Value;
            }

            if (vDatosVivienda.XML_BIENES_MUEBLES != null)
            {
                vXmlBienes = XElement.Parse(vDatosVivienda.XML_BIENES_MUEBLES);

                chkDVVehiculo.Checked = vXmlBienes.Elements("BIEN").Where(t => t.Attribute("CL_BIEN").Value == "VEHICULO").FirstOrDefault().Attribute("FG_BIEN").Value == "0" ? false : true;
                chkDVMotocicleta.Checked = vXmlBienes.Elements("BIEN").Where(t => t.Attribute("CL_BIEN").Value == "MOTOCICLETA").FirstOrDefault().Attribute("FG_BIEN").Value == "0" ? false : true;
                chkDVOtrosRelacionBienes.Checked = vXmlBienes.Elements("BIEN").Where(t => t.Attribute("CL_BIEN").Value == "OTRO_RELACION_BIENES").FirstOrDefault().Attribute("FG_BIEN").Value == "0" ? false : true;
                txtDVOtrosRelacionBienesComentarios.Text = vXmlBienes.Elements("BIEN").Where(t => t.Attribute("CL_BIEN").Value == "OTRO_RELACION_BIENES").FirstOrDefault().Attribute("DS_BIEN").Value;

                chkDVDvd.Checked = vXmlBienes.Elements("BIEN").Where(t => t.Attribute("CL_BIEN").Value == "DVD").FirstOrDefault().Attribute("FG_BIEN").Value == "0" ? false : true;
                chkDVRadio.Checked = vXmlBienes.Elements("BIEN").Where(t => t.Attribute("CL_BIEN").Value == "RADIO").FirstOrDefault().Attribute("FG_BIEN").Value == "0" ? false : true;
                chkDVLavadora.Checked = vXmlBienes.Elements("BIEN").Where(t => t.Attribute("CL_BIEN").Value == "LAVADORA").FirstOrDefault().Attribute("FG_BIEN").Value == "0" ? false : true;
                chkDVComputadora.Checked = vXmlBienes.Elements("BIEN").Where(t => t.Attribute("CL_BIEN").Value == "COMPUTADORA").FirstOrDefault().Attribute("FG_BIEN").Value == "0" ? false : true;
                chkDVTelevision.Checked = vXmlBienes.Elements("BIEN").Where(t => t.Attribute("CL_BIEN").Value == "TELEVISION").FirstOrDefault().Attribute("FG_BIEN").Value == "0" ? false : true;
                chkDVAlajas.Checked = vXmlBienes.Elements("BIEN").Where(t => t.Attribute("CL_BIEN").Value == "ALAJAS").FirstOrDefault().Attribute("FG_BIEN").Value == "0" ? false : true;
                chkDVEstufas.Checked = vXmlBienes.Elements("BIEN").Where(t => t.Attribute("CL_BIEN").Value == "ESTUFAS").FirstOrDefault().Attribute("FG_BIEN").Value == "0" ? false : true;
                chkDVSala.Checked = vXmlBienes.Elements("BIEN").Where(t => t.Attribute("CL_BIEN").Value == "SALA").FirstOrDefault().Attribute("FG_BIEN").Value == "0" ? false : true;
                chkDVComedor.Checked = vXmlBienes.Elements("BIEN").Where(t => t.Attribute("CL_BIEN").Value == "COMEDOR").FirstOrDefault().Attribute("FG_BIEN").Value == "0" ? false : true;
                chkDVRefrigerador.Checked = vXmlBienes.Elements("BIEN").Where(t => t.Attribute("CL_BIEN").Value == "REFRIGERADOR").FirstOrDefault().Attribute("FG_BIEN").Value == "0" ? false : true;
                chkDVHornos.Checked = vXmlBienes.Elements("BIEN").Where(t => t.Attribute("CL_BIEN").Value == "HORNOS").FirstOrDefault().Attribute("FG_BIEN").Value == "0" ? false : true;
                chkDVBienesRaicesOtros.Checked = vXmlBienes.Elements("BIEN").Where(t => t.Attribute("CL_BIEN").Value == "OTROS").FirstOrDefault().Attribute("FG_BIEN").Value == "0" ? false : true;
                txtDVOtrosBienesComentarios.Text = vXmlBienes.Elements("BIEN").Where(t => t.Attribute("CL_BIEN").Value == "OTROS").FirstOrDefault().Attribute("DS_BIEN").Value;
            }
        }

        private void GuardarESDatosVivienda()
        {
            string vTipoAccion = "";
            int? vIdDatosVivienda;

            if (vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO != 0)
            {
                XElement vXmlServicios = new XElement("SERVICIOS");
                XElement vXmlBienes = new XElement("BIENES");

                if (vDatosVivienda.ID_DATO_VIVIENDA != 0)
                {
                    vTipoAccion = E_TIPO_OPERACION_DB.A.ToString();
                    vIdDatosVivienda = vDatosVivienda.ID_DATO_VIVIENDA;
                }
                else
                {
                    vTipoAccion = E_TIPO_OPERACION_DB.I.ToString();
                    vIdDatosVivienda = null;
                }

                vDatosVivienda.CL_TIPO_VIVIENDA = cmbDVTipoVivienda.SelectedValue;
                vDatosVivienda.CL_TIPO_CONSTRUCCION = cmbDVTipoConstruccion.SelectedValue;

                if (txtDVNoCuartos.Value.HasValue)
                    vDatosVivienda.NO_HABITACIONES = (byte)txtDVNoCuartos.Value;

                if (txtDVNoBaños.Value.HasValue)
                    vDatosVivienda.NO_BANIOS = (byte)txtDVNoBaños.Value;

                if (txtDVNoPatios.Value.HasValue)
                    vDatosVivienda.NO_PATIOS = (byte)txtDVNoPatios.Value;

                vXmlServicios.Add(new XElement("SERVICIO", new XAttribute("CL_SERVICIO", "AGUA"), new XAttribute("FG_SERVICIO", chkDVAgua.Checked.Value ? "1" : "0")));
                vXmlServicios.Add(new XElement("SERVICIO", new XAttribute("CL_SERVICIO", "LUZ"), new XAttribute("FG_SERVICIO", chkDVLuz.Checked.Value ? "1" : "0")));
                vXmlServicios.Add(new XElement("SERVICIO", new XAttribute("CL_SERVICIO", "DRENAJE"), new XAttribute("FG_SERVICIO", chkDVDrenaje.Checked.Value ? "1" : "0")));
                vXmlServicios.Add(new XElement("SERVICIO", new XAttribute("CL_SERVICIO", "GAS"), new XAttribute("FG_SERVICIO", chkDVGas.Checked.Value ? "1" : "0")));
                vXmlServicios.Add(new XElement("SERVICIO", new XAttribute("CL_SERVICIO", "TELEFONO"), new XAttribute("FG_SERVICIO", chkDVTelefono.Checked.Value ? "1" : "0")));
                vXmlServicios.Add(new XElement("SERVICIO", new XAttribute("CL_SERVICIO", "OTRO"), new XAttribute("FG_SERVICIO", chkDVOtros.Checked.Value ? "1" : "0"), new XAttribute("DS_SERVICIO", txtDVOtrosServiciosComentarios.Text)));

                vXmlBienes.Add(new XElement("BIEN", new XAttribute("CL_BIEN", "VEHICULO"), new XAttribute("FG_BIEN", chkDVVehiculo.Checked.Value ? "1" : "0")));
                vXmlBienes.Add(new XElement("BIEN", new XAttribute("CL_BIEN", "MOTOCICLETA"), new XAttribute("FG_BIEN", chkDVMotocicleta.Checked.Value ? "1" : "0")));
                vXmlBienes.Add(new XElement("BIEN", new XAttribute("CL_BIEN", "OTRO_RELACION_BIENES"), new XAttribute("FG_BIEN", chkDVOtrosRelacionBienes.Checked.Value ? "1" : "0"), new XAttribute("DS_BIEN", txtDVOtrosRelacionBienesComentarios.Text)));


                vXmlBienes.Add(new XElement("BIEN", new XAttribute("CL_BIEN", "DVD"), new XAttribute("FG_BIEN", chkDVDvd.Checked.Value ? "1" : "0")));
                vXmlBienes.Add(new XElement("BIEN", new XAttribute("CL_BIEN", "RADIO"), new XAttribute("FG_BIEN", chkDVRadio.Checked.Value ? "1" : "0")));
                vXmlBienes.Add(new XElement("BIEN", new XAttribute("CL_BIEN", "LAVADORA"), new XAttribute("FG_BIEN", chkDVLavadora.Checked.Value ? "1" : "0")));
                vXmlBienes.Add(new XElement("BIEN", new XAttribute("CL_BIEN", "COMPUTADORA"), new XAttribute("FG_BIEN", chkDVComputadora.Checked.Value ? "1" : "0")));
                vXmlBienes.Add(new XElement("BIEN", new XAttribute("CL_BIEN", "TELEVISION"), new XAttribute("FG_BIEN", chkDVTelevision.Checked.Value ? "1" : "0")));
                vXmlBienes.Add(new XElement("BIEN", new XAttribute("CL_BIEN", "ALAJAS"), new XAttribute("FG_BIEN", chkDVAlajas.Checked.Value ? "1" : "0")));
                vXmlBienes.Add(new XElement("BIEN", new XAttribute("CL_BIEN", "ESTUFAS"), new XAttribute("FG_BIEN", chkDVEstufas.Checked.Value ? "1" : "0")));
                vXmlBienes.Add(new XElement("BIEN", new XAttribute("CL_BIEN", "SALA"), new XAttribute("FG_BIEN", chkDVSala.Checked.Value ? "1" : "0")));
                vXmlBienes.Add(new XElement("BIEN", new XAttribute("CL_BIEN", "COMEDOR"), new XAttribute("FG_BIEN", chkDVComedor.Checked.Value ? "1" : "0")));

                vXmlBienes.Add(new XElement("BIEN", new XAttribute("CL_BIEN", "REFRIGERADOR"), new XAttribute("FG_BIEN", chkDVRefrigerador.Checked.Value ? "1" : "0")));
                vXmlBienes.Add(new XElement("BIEN", new XAttribute("CL_BIEN", "ESTUFAS"), new XAttribute("FG_BIEN", chkDVHornos.Checked.Value ? "1" : "0")));
                vXmlBienes.Add(new XElement("BIEN", new XAttribute("CL_BIEN", "HORNOS"), new XAttribute("FG_BIEN", chkDVSala.Checked.Value ? "1" : "0")));
                vXmlBienes.Add(new XElement("BIEN", new XAttribute("CL_BIEN", "OTROS"), new XAttribute("FG_BIEN", chkDVBienesRaicesOtros.Checked.Value ? "1" : "0"), new XAttribute("DS_BIEN", txtDVOtrosBienesComentarios.Text)));

                vDatosVivienda.XML_BIENES_MUEBLES = vXmlBienes.ToString();
                vDatosVivienda.XML_SERVICIOS = vXmlServicios.ToString();
                vDatosVivienda.ID_ESTUDIO_SOCIOECONOMICO = vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO;

                ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
                E_RESULTADO vRespuesta = nProcesoSeleccion.InsertaActualizaESDatosVivienda(vDatosVivienda, vClUsuario, vNbPrograma, vTipoAccion, vIdDatosVivienda);
                string vMensaje = vRespuesta.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vRespuesta.CL_TIPO_ERROR, pCallBackFunction: "");

                if (vTipoAccion == E_TIPO_OPERACION_DB.I.ToString())
                {
                    CargarESDatosVivienda(false);
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Completa la información principal: Datos personales, del estudio socioeconómico", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClRutaArchivosTemporales = Server.MapPath(ContextoApp.ClRutaArchivosTemporales);

            if (!Page.IsPostBack)
            {
                vLstReferencias = new List<E_REFERENCIA_CANDIDATO>();
                vLstCompetencias = new List<E_COMPETENCIAS_PROCESO_SELECCION>();
                vLstClasificacion = new List<E_CLASIFICACION>();
                vLstDocumentos = new List<E_DOCUMENTO>();
                dtpESNacimiento.MinDate = new DateTime(1900, 01, 01);
                rdpFechaDependiente.MinDate = new DateTime(1900, 01, 01);

                vDatosLaborales = new E_ES_DATOS_LABORALES();
                vDatosEconomicos = new E_ES_DATOS_ECONOMICOS();
                vDatosVivienda = new E_ES_DATOS_VIVIENDA();

                divEstatus.Style.Add("display", "block");

                CargarCatalogos();

                if (Request.Params["IdProcesoSeleccion"] != null)
                {
                    vIdProcesoSeleccion = int.Parse(Request.Params["IdProcesoSeleccion"].ToString());
                }
                else
                {
                    vIdProcesoSeleccion = 0;
                }

                if (Request.Params["IdRequisicion"] != null)
                {
                    vIdRequisicion = int.Parse(Request.Params["IdRequisicion"].ToString());
                }
                else
                {
                    vIdRequisicion = null;
                }

                if (Request.Params["IdCandidato"] != null)
                {
                    vIdCandidato = int.Parse(Request.Params["IdCandidato"].ToString());
                }

                if (Request.Params["IdEmpleado"] != null)
                {
                    vIdEmpleado = int.Parse(Request.Params["IdEmpleado"].ToString());
                }

                if (Request.Params["IdBateria"] != null)
                {
                    vIdBateria = int.Parse(Request.Params["IdBateria"].ToString());
                }
                else
                {
                    vIdBateria = null;
                }

                if (Request.Params["clToken"] != null)
                {
                    vClToken = new Guid(Request.Params["clToken"]);
                }
                else
                {
                    vClToken = Guid.Empty;
                }

                if (Request.Params["ClVentana"] != null)
                {
                    vClVentana = Request.Params["ClVentana"].ToString();
                }

                if (Request.Params["Tipo"] != null)
                {
                    if (Request.Params["Tipo"] == "REV")
                    {
                        btnCopiarDatosProceso.Enabled = false;
                        btnagregarEntrevista.Enabled = false;
                        btnEditarEntrevista.Enabled = false;
                        btnEliminarEntrevista.Enabled = false;
                        btnEnviarTodos.Enabled = false;
                        btnEnvioCorreos.Enabled = false;
                        btnAgregarReferencia.Enabled = false;
                        btnGuardarExamenMedico.Enabled = false;
                        btnGuardarEstudio.Enabled = false;
                        btnGuardarESDatosVivienda.Enabled = false;
                        btnGuardarESDatosLaborales.Enabled = false;
                        btnGuardarESDatosEconomicos.Enabled = false;
                        btnDelDocumentos.Enabled = false;
                        btnGuardarDoc.Enabled = false;
                        btnAgregarDependiente.Enabled = false;
                        btnAgregar.Enabled = false;
                        btnCopiarDatos.Enabled = false;
                        btnTerminarProceso.Enabled = false;
                       
                    }
                }

                CargarDatosIniciales();
                CargarDocumentos();
            }
        }

        protected void rgEntrevistas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
            rgEntrevistas.DataSource = nProcesoSeleccion.ObtieneEntrevistaProcesoSeleccion(pIdProcesoSeleccion: vIdProcesoSeleccion);
        }

        protected void btnEliminarEntrevista_Click(object sender, EventArgs e)
        {
            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
            foreach (GridDataItem item in rgEntrevistas.SelectedItems)
            {
                var vIdEntrevista = (int.Parse(item.GetDataKeyValue("ID_ENTREVISTA").ToString()));

                E_RESULTADO vResultado = nProcesoSeleccion.EliminaEntrevista(vIdEntrevista);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "");
                rgEntrevistas.Rebind();
            }
        }

        protected void btnEnvioCorreos_Click(object sender, EventArgs e)
        {
            EnviarCorreo(false);

            //IntegracionDePersonal idp = ContextoApp.IDP;

            //idp.MensajeCorreoEntrevista.dsMensaje = "<p>Estimado(a) <b>[NB_ENTREVISTADOR]</b></p>" +
            //"<p>Por medio del presente te solicitamos que llenes los comentarios de la entrevista al candidato (a) <b>[NB_CANDIDATO]</b> que está aplicando para el puesto <b>[NB_PUESTO]</b>. </p>" +
            //"<p>Para complementar los comentarios de la entrevista, por favor da click <a href=\"[URL]\">aquí</a>. </p>" +
            //"<p>Tu contraseña de acceso es <b>[CONTRASENA]</b> . ¡Gracias por tu apoyo!";

            //ContextoApp.IDP = idp;
            //ContextoApp.SaveConfiguration("ADMIN", "ProcesoSeleccion.aspx");
        }

        protected void btnEnviarTodos_Click(object sender, EventArgs e)
        {
            EnviarCorreo(true);
        }


        //protected void btnAnt_Click(object sender, EventArgs e)
        //{
        //    if (vIdIndex >= 1)
        //    {
        //        vIdIndex = vIdIndex - 1;
        //        vNbClasificacion = vLstClasificacion.ElementAt(vIdIndex).CL_CLASIFICACION;
        //        vNoOrden = vLstClasificacion.ElementAt(vIdIndex).NO_ORDEN;
        //        vClColor = vLstClasificacion.ElementAt(vIdIndex).CL_COLOR;
        //        //divColorClas.Style.Add("background", vClColor);
        //        dgvCompetencias.Rebind();
        //        //divCamposExtras.Style["display"] = "none";
        //        //divCompetencias.Style["display"] = "block";

        //        //tdClasificacion.Attributes["class"] = "MostrarCelda";
        //        //tdSignificado.Attributes["class"] = "MostrarCelda";
        //    }
        //}

        //protected void btnNext_Click(object sender, EventArgs e)
        //{
        //    if (vIdIndex < vLstClasificacion.Count - 1)
        //    {
        //        vIdIndex = vIdIndex + 1;

        //        //tdClasificacion.Attributes["class"] = "MostrarCelda";
        //        //tdSignificado.Attributes["class"] = "MostrarCelda";

        //        vNbClasificacion = vLstClasificacion.ElementAt(vIdIndex).CL_CLASIFICACION;
        //        vNoOrden = vLstClasificacion.ElementAt(vIdIndex).NO_ORDEN;
        //        vClColor = vLstClasificacion.ElementAt(vIdIndex).CL_COLOR;
        //       // divColorClas.Style.Add("background", vClColor);
        //        dgvCompetencias.Rebind();
        //    }
        //}

        protected void dgvCompetencias_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {


            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();

            vLstCompetencias = nProcesoSeleccion.ObtieneCompetenciasProcesoSeleccion((int)vIdCandidato, vIdPuesto);
            //if (vLstCompetencias.Count > 0)
            //{
                dgvCompetencias.DataSource = vLstCompetencias;
                // txtNbClasificacion.InnerHtml = vLstCompetencias.Where(l => l.CL_CLASIFICACION == vNbClasificacion && l.NO_ORDEN == vNoOrden).FirstOrDefault().NB_CLASIFICACION_COMPETENCIA;
                // txtDsSignificado.InnerHtml = vLstCompetencias.Where(l => l.CL_CLASIFICACION == vNbClasificacion && l.NO_ORDEN == vNoOrden).FirstOrDefault().DS_CLASIFICACION_COMPETENCIA;
            //}
        }

        protected void dgvCompetencias_DataBound(object sender, EventArgs e)
        {
            AsignarTooltip();
        }

        protected void grdDocumentos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdDocumentos.DataSource = vLstDocumentos;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            AddDocumento(cmbTipoDocumento.SelectedValue, rauDocumento);
            grdDocumentos.Rebind();
        }

        protected void btnDelDocumentos_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem i in grdDocumentos.SelectedItems)
                EliminarDocumento(i.GetDataKeyValue("ID_ITEM").ToString());
        }

        protected void btnGuardarDoc_Click(object sender, EventArgs e)
        {
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

            ProcesoSeleccionNegocio nProceso = new ProcesoSeleccionNegocio();
            E_RESULTADO vResultado = nProceso.InsertaActualizaDocumentos((int)vIdCandidato, vLstArchivos, vLstDocumentos, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR,pCallBackFunction:"");


        }

        protected void btnGuardarExamenMedico_Click(object sender, EventArgs e)
        {
            GuardarDatosExamenMedico();
        }

        protected void ramEntrevista_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }

        protected void btnGuardarEstudio_Click(object sender, EventArgs e)
        {
            GuardarDatosEstudioSocioEconomico();
        }

        protected void rgdAplicacionPrueba_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ProcesoSeleccionNegocio nProceso = new ProcesoSeleccionNegocio();
            var vAplicacionPrueba = nProceso.ObtieneAplicacionPrueba(vIdCandidato);
            rgdAplicacionPrueba.DataSource = vAplicacionPrueba;
        }

        protected void rdgRegistroSolicitud_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ProcesoSeleccionNegocio nProceso = new ProcesoSeleccionNegocio();
            var vRegistroSolicitud = nProceso.ObtieneBitacoraSolicitud(vIdCandidato);
            rdgRegistroSolicitud.DataSource = vRegistroSolicitud;
        }

        protected void btnGuardarESDatosLaborales_Click(object sender, EventArgs e)
        {
            GuardarESDatosLaborales();
        }

        protected void btnGuardarESDatosVivienda_Click(object sender, EventArgs e)
        {
            GuardarESDatosVivienda();
        }

        protected void btnGuardarESDatosEconomicos_Click(object sender, EventArgs e)
        {
            GuardarESDatosEconomicos();
        }

        protected void btnAgregarDependiente_Click(object sender, EventArgs e)
        {

            if (vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO > 0)
            {

                if (string.IsNullOrEmpty(txtNombreDependiente.Text))
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Indica el nombre del pariente", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    return;
                }

                if (!rdpFechaDependiente.SelectedDate.HasValue)
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Selecciona la fecha de nacimiento", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    return;
                }


                string nb_pariente = txtNombreDependiente.Text;
                string parentesco = cmbParentesco.SelectedItem.Text;
                DateTime fe_nacimiento = (DateTime)rdpFechaDependiente.SelectedDate;
                string ocupacion = cmbOcupacion.SelectedItem.Text;
                bool fg_dependiente = chkDependienteSi.Checked;
                bool fg_activo = true;

                ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
                E_RESULTADO vRespuesta = nProcesoSeleccion.InsertaDependientes(vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO, nb_pariente, parentesco, null, fe_nacimiento, 0, ocupacion, fg_dependiente, fg_activo, vClUsuario, vNbPrograma);
                string vMensaje = vRespuesta.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vRespuesta.CL_TIPO_ERROR, pCallBackFunction: "");
                var vDependientes = nProcesoSeleccion.ObtieneDependientes(vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO);
                if (vRespuesta.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                {
                    txtNombreDependiente.Text = string.Empty;
                    txtEdadDependiente.Text = string.Empty;
                    
                    chkDependienteNo.Checked = true;
                    chkDependienteSi.Checked = false;
                    grdDatosFamiliares.Rebind();
                }
                
                
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Completa la información principal: Datos personales, del estudio socioeconómico", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }
        }

        protected void rdpFechaDependiente_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            //DateTime nacimiento = Convert.ToDateTime(rdpFechaDependiente.SelectedDate);
            //if (nacimiento < DateTime.Now)
            //{
            //    txtEdadDependiente.Text = (DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1).ToString();
            //}
            //else
            //{
            //    UtilMensajes.MensajeResultadoDB(rnMensaje, "Selecciona una fecha de nacimiento válida", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");

            //}

            CalcularEdad(sender, e);
        }

        protected void CalcularEdad(object sender, EventArgs e)
        {
            RadDatePicker vControlF = new RadDatePicker();
            vControlF = (RadDatePicker)sender;
            string value = vControlF.DateInput.InvalidTextBoxValue;
            if (value == string.Empty && !vControlF.SelectedDate.Equals(null))
            {
                txtEdadDependiente.Text = ObtenerEdad(((RadDatePicker)sender).SelectedDate.Value);
            }
        }

        protected string ObtenerEdad(DateTime pFeNacimiento)
        {
            DateTime vFechaObjetivo = pFeNacimiento;
            DateTime vFechaHoy = DateTime.Now;
            int vFactor = 1;

            if (vFechaObjetivo < vFechaHoy)
            {
                vFechaObjetivo = DateTime.Now;
                vFechaHoy = pFeNacimiento;
                vFactor = 0;
            }
            vFechaHoy = vFechaHoy.Date;
            vFechaObjetivo = vFechaObjetivo.Date;

            DateTime vDiferencia = new DateTime(vFechaObjetivo.Subtract(vFechaHoy).Ticks);
            int vNoAnios = vDiferencia.Year - 1;
            int vNoMeses = vDiferencia.Month - 1;
            int vNoDias = vDiferencia.Day - 1;

            if (vNoAnios != 0)
                return String.Format("{2}{0} año{1}", vNoAnios, vNoAnios != 1 ? "s" : String.Empty, vFactor == 0 ? "" : "En ");

            if (vNoMeses != 0)
                return String.Format("{2}{0} mes{1}", vNoMeses, vNoMeses != 1 ? "es" : String.Empty, vFactor == 0 ? "" : "En ");

            if (vNoDias != 0)
                return String.Format("{2}{0} día{1}", vNoDias, vNoDias != 1 ? "s" : String.Empty, vFactor == 0 ? "" : "En ");

            return "Hoy";
        }

        protected void grdDatosFamiliares_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ProcesoSeleccionNegocio nProceso = new ProcesoSeleccionNegocio();
            vlstDependiente = nProceso.ObtieneDependientes(vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO);
            grdDatosFamiliares.DataSource = vlstDependiente;
        }

        protected void grdDatosFamiliares_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridDataItem item = e.Item as GridDataItem;
                int idDependiente = int.Parse(item.GetDataKeyValue("ID_DATO_DEPENDIENTE").ToString());

                ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
                E_RESULTADO vRespuesta = nProcesoSeleccion.EliminaDependientes(idDependiente);
                string vMensaje = vRespuesta.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                var vDependientes = nProcesoSeleccion.ObtieneDependientes(vEstudioSocioEconomico.ID_ESTUDIO_SOCIOECONOMICO);
                grdDatosFamiliares.Rebind();
            }
        }

        protected void btnCopiarDatos_Click(object sender, EventArgs e)
        {
            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
            E_RESULTADO vRespuesta = nProcesoSeleccion.InsertaActualizaCopiaSocioEconomico(vIdCandidato, vIdProcesoSeleccion, vClUsuario, vNbPrograma);
            string vMensaje = vRespuesta.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vRespuesta.CL_TIPO_ERROR, pCallBackFunction: "");
            CargarDatosEstudioSocioEconomico(true);
            CargarDatosESDatosLaborales(true);
            CargarDatosESDatosEconomicos(true);
            CargarESDatosVivienda(true);
            grdDatosFamiliares.Rebind();
        }

        protected void dtpESNacimiento_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            DateTime nacimiento = Convert.ToDateTime(dtpESNacimiento.SelectedDate);
            if (nacimiento < DateTime.Now)
            {
                txtESEdad.Text = (DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1).ToString();
                txtEMedad.Text = (DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1).ToString();
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Selecciona una fecha de nacimiento válida", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");

            }
        }

        [Serializable]
        public class E_TIPO_TELEFONO
        {
            public string TELEFONO { get; set; }
            public string TIPO { get; set; }
        }

        protected void grdReferencias_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
            grdReferencias.DataSource = nProcesoSeleccion.ObtieneExperienciaLaboral(pIdCandidato: vIdCandidato);
        }

        protected void dgvCompetencias_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem gridItem = e.Item as GridDataItem;

                int vIdCompetencia = int.Parse(gridItem.GetDataKeyValue("ID_COMPETENCIA").ToString());
                string vClasificacion = vLstCompetencias.Where(t => t.ID_COMPETENCIA == vIdCompetencia).FirstOrDefault().NB_CLASIFICACION_COMPETENCIA;
                gridItem["CL_COLOR"].ToolTip = vClasificacion;
            }
        }

        protected void grdProcesoSeleccion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
            List<SPE_OBTIENE_PROCESO_SELECCION_Result> vLtsProcesosEval = new List<SPE_OBTIENE_PROCESO_SELECCION_Result>();
                vLtsProcesosEval = nProcesoSeleccion.ObtieneProcesoSeleccion(pIdCandidato: vIdCandidato, pIdProcesoSeleccionActual: vIdProcesoSeleccion);
                var vItem = vLtsProcesosEval.Where(w => w.ID_PROCESO_SELECCION == vIdProcesoSeleccion).FirstOrDefault();
                vLtsProcesosEval.Remove(vItem);
            grdProcesoSeleccion.DataSource = vLtsProcesosEval;
        }

        protected void btnTerminarProceso_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(reOBservacionProceso.Content))
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Indica una observación antes de terminar el proceso de selección.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return;
            }

            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
            E_RESULTADO vRespuesta = nProcesoSeleccion.TerminaProcesoSeleccion(vIdProcesoSeleccion, reOBservacionProceso.Content, vClUsuario, vNbPrograma);
            string vMensaje = vRespuesta.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vRespuesta.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vRespuesta.CL_TIPO_ERROR, pCallBackFunction: "closeWindow");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vRespuesta.CL_TIPO_ERROR, pCallBackFunction: "");
            }


        }

        protected void btnCopiarDatosProceso_Click(object sender, EventArgs e)
        {
            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
            foreach (GridDataItem item in grdProcesoSeleccion.SelectedItems)
            {
                var vIdProcesoSeleccionOrigen = (int.Parse(item.GetDataKeyValue("ID_PROCESO_SELECCION").ToString()));

                XElement vXmlConfiguracion = new XElement("CONFIG");

                vXmlConfiguracion.Add(new XAttribute("FG_COPIAR_ENTREVISTA", (chkEntrevista.Checked.Value ? "1" : "0")));
                vXmlConfiguracion.Add(new XAttribute("FG_COPIAR_EST_SOC", (chkSocioeconomico.Checked.Value ? "1" : "0")));
                vXmlConfiguracion.Add(new XAttribute("FG_COPIAR_RES_MED", (chkMedico.Checked.Value ? "1" : "0")));

                E_RESULTADO vResultado = nProcesoSeleccion.CopiaDatosProcesoSeleccion(vIdProcesoSeleccionOrigen, vIdProcesoSeleccion, vXmlConfiguracion.ToString(), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                {
                    rgEntrevistas.Rebind();
                    CargarDatosExamenMedico(true);
                    CargarDatosEstudioSocioEconomico(true);
                    CargarDatosESDatosLaborales(true);
                    CargarDatosESDatosEconomicos(true);
                    CargarESDatosVivienda(true);
                    grdDatosFamiliares.Rebind();
                }

                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "");


                //rgEntrevistas.Rebind();
            }
        }

        protected void grdProcesoSeleccion_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem gridItem = e.Item as GridDataItem;

                if (gridItem.GetDataKeyValue("ID_PROCESO_SELECCION").ToString() == vIdProcesoSeleccion.ToString())
                {
                    e.Item.Enabled = false;
                    gridItem.SelectableMode = GridItemSelectableMode.None;
                    gridItem.Font.Bold = true;
                }
            }
        }

        protected void rgdAplicacionPrueba_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgdAplicacionPrueba.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgdAplicacionPrueba.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgdAplicacionPrueba.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgdAplicacionPrueba.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgdAplicacionPrueba.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

 
    }
}