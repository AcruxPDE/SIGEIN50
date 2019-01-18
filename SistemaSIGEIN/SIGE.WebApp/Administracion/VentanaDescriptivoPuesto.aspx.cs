using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Entidades.SecretariaTrabajoPrevisionSocial;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.SecretariaTrabajoPrevisionSocial;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using WebApp.Comunes;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaDescriptivoPuesto : System.Web.UI.Page
    {
        #region Varianbles

        private string vClUsuario = string.Empty;
        private string vNbPrograma = string.Empty;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vNbFirstRadEditorTagName = "p";

        List<E_DOCUMENTO> vLstDocumentos
        {
            get { return (List<E_DOCUMENTO>)ViewState["vs_LstDocumentos"]; }
            set { ViewState["vs_LstDocumentos"] = value; }
        }

        string vXmlDocumentos;

        Guid? vIdItemFotografia
        {
            get { return (Guid?)ViewState["vs_vIdItemFotografia"]; }
            set { ViewState["vs_vIdItemFotografia"] = value; }
        }

        string vClRutaArchivosTemporales;

        private List<E_ESCOLARIDADES> vListaEscoloridad
        {
            get { return (List<E_ESCOLARIDADES>)ViewState["vs_vListaEscoloridad"]; }
            set { ViewState["vs_vListaEscoloridad"] = value; }
        }

        private List<E_CATALOGO_CATALOGOS> vListaGenero
        {
            get { return (List<E_CATALOGO_CATALOGOS>)ViewState["vs_vListaGenero"]; }
            set { ViewState["vs_vListaGenero"] = value; }
        }

        private List<E_CATALOGO_CATALOGOS> vListaEdoCivil
        {
            get { return (List<E_CATALOGO_CATALOGOS>)ViewState["vs_vListaEdoCivil"]; }
            set { ViewState["vs_vListaEdoCivil"] = value; }
        }

        private List<E_COMPETENCIAS> vListaCompetencias
        {
            get { return (List<E_COMPETENCIAS>)ViewState["vs_vListaCompetencias"]; }
            set { ViewState["vs_vListaCompetencias"] = value; }
        }

        private List<E_COMPETENCIAS> vListaCatalogoCompetencias
        {
            get { return (List<E_COMPETENCIAS>)ViewState["vs_vListaCatalogoCompetencias"]; }
            set { ViewState["vs_vListaCatalogoCompetencias"] = value; }
        }

        private List<E_AREAS_INTERES> vListaAreasInteres
        {
            get { return (List<E_AREAS_INTERES>)ViewState["vs_vListaAreasInteres"]; }
            set { ViewState["vs_vListaAreasInteres"] = value; }
        }

        private List<E_CENTRO_ADMVO> vListaCentroAdmvo
        {
            get { return (List<E_CENTRO_ADMVO>)ViewState["vs_vListaCentroAdmvo"]; }
            set { ViewState["vs_vListaCentroAdmvo"] = value; }
        }

        private List<E_CENTRO_OPTVO> vListaCentroOptvo
        {
            get { return (List<E_CENTRO_OPTVO>)ViewState["vs_vListaCentroOptvo"]; }
            set { ViewState["vs_vListaCentroOptvo"] = value; }
        }

        private List<E_AREAS> vListaAreas
        {
            get { return (List<E_AREAS>)ViewState["vs_vListaAreas"]; }
            set { ViewState["vs_vListaAreas"] = value; }
        }

        private List<E_PUESTOS> vListaPuestos
        {
            get { return (List<E_PUESTOS>)ViewState["vs_vListaPuestos"]; }
            set { ViewState["vs_vListaPuestos"] = value; }
        }

        private List<E_INDICADOR_DESEMPENO> vListaIndicadorDesempeno
        {
            get { return (List<E_INDICADOR_DESEMPENO>)ViewState["vs_vListaIndicadorDesempeno"]; }
            set { ViewState["vs_vListaIndicadorDesempeno"] = value; }
        }

        private List<E_EXPERIENCIA> vListaExperiencia
        {
            get { return (List<E_EXPERIENCIA>)ViewState["vs_vListaExperiencia"]; }
            set { ViewState["vs_vListaExperiencia"] = value; }
        }

        public int? vIdDescriptivo
        {
            get { return (int?)ViewState["vs_vIdDescriptivo"]; }
            set { ViewState["vs_vIdDescriptivo"] = value; }
        }

        private string vXmlAdicionales
        {
            get { return (string)ViewState["vs_xmlAdicionales"]; }
            set { ViewState["vs_xmlAdicionales"] = value; }
        }

        public int? vNoPlazas
        {
            get { return (int?)ViewState["vs_vNoPlazas"]; }
            set { ViewState["vs_vNoPlazas"] = value; }
        }

        public int? vNoMinimoPlazas
        {
            get { return (int?)ViewState["vs_vNoMinimoPlazas"]; }
            set { ViewState["vs_vNoMinimoPlazas"] = value; }
        }

        private E_FUNCION_GENERICA vFuncionGenericaABC
        {
            get { return (E_FUNCION_GENERICA)ViewState["vs_vFuncionGenericaABC"]; }
            set { ViewState["vs_vFuncionGenericaABC"] = value; }
        }

        private List<E_FUNCION_COMPETENCIA> vLstFuncionCompetenciaABC
        {
            get { return (List<E_FUNCION_COMPETENCIA>)ViewState["vs_vLstFuncionCompetenciaABC"]; }
            set { ViewState["vs_vLstFuncionCompetenciaABC"] = value; }
        }

        private List<E_FUNCION_INDICADOR> vLstFuncionCompetenciaIndicadorABC
        {
            get { return (List<E_FUNCION_INDICADOR>)ViewState["vs_vLstFuncionCompetenciaIndicadorABC"]; }
            set { ViewState["vs_vLstFuncionCompetenciaIndicadorABC"] = value; }
        }

        private List<E_FUNCION_GENERICA> vFuncionesGenericas
        {
            get { return (List<E_FUNCION_GENERICA>)ViewState["vs_vFuncionesGenericas"]; }
            set { ViewState["vs_vFuncionesGenericas"] = value; }
        }

        private List<E_FUNCION_COMPETENCIA> vLstFuncionCompetencia
        {
            get { return (List<E_FUNCION_COMPETENCIA>)ViewState["vs_vLstFuncionCompetencia"]; }
            set { ViewState["vs_vLstFuncionCompetencia"] = value; }
        }

        private List<E_FUNCION_INDICADOR> vLstFuncionCompetenciaIndicador
        {
            get { return (List<E_FUNCION_INDICADOR>)ViewState["vs_vLstFuncionCompetenciaIndicador"]; }
            set { ViewState["vs_vLstFuncionCompetenciaIndicador"] = value; }
        }

        private List<E_PUESTOS> vLstInterrelacionados
        {
            get { return (List<E_PUESTOS>)ViewState["vs_vLstInterrelacionados"]; }
            set { ViewState["vs_vLstInterrelacionados"] = value; }
        }

        private List<E_PUESTOS> vLstLaterales
        {
            get { return (List<E_PUESTOS>)ViewState["vs_vLstLaterales"]; }
            set { ViewState["vs_vLstLaterales"] = value; }
        }

        private List<E_PUESTOS> vLstAlternativa
        {
            get { return (List<E_PUESTOS>)ViewState["vs_vLstAlternativa"]; }
            set { ViewState["vs_vLstAlternativa"] = value; }
        }

        private Guid? vIdEditingItem
        {
            get { return (Guid?)ViewState["vs_vIdEditingItem"]; }
            set { ViewState["vs_vIdEditingItem"] = value; }
        }


        //Se agrega al viewState
        //   private bool vIsCopy = false;
        private bool vIsCopy
        {
            get { return (bool)ViewState["vs_vIsCopy"]; }
            set { ViewState["vs_vIsCopy"] = value; }
        }

        OcupacionesNegocio negocio = new OcupacionesNegocio();

        public List<SPE_OBTIENE_AREA_OCUPACION_Result> listaAreasO;
        public List<SPE_OBTIENE_SUBAREA_OCUPACION_Result> listaSubareas;
        public List<SPE_OBTIENE_MODULO_OCUPACION_Result> listaModulos;
        public List<SPE_OBTIENE_OCUPACIONES_Result> listaOcupaciones;

        #endregion

        #region Metodos

        public void CargarDatos(int? pIdDescriptivo)
        {
            DescriptivoNegocio nDescriptivo = new DescriptivoNegocio();
            E_DESCRIPTIVO vDescriptivo = nDescriptivo.ObtieneDescriptivo(pIdDescriptivo);
            vListaEscoloridad = vDescriptivo.LST_ESCOLARIDADES;
            vListaGenero = vDescriptivo.LST_CATALOGO_GENERO;
            vListaEdoCivil = vDescriptivo.LST_CATALOGO_EDOCIVIL;
            vListaCompetencias = vDescriptivo.LST_CATALOGO_COMPETENCIAS_ESP;
            vListaCatalogoCompetencias = vDescriptivo.LST_CATALOGO_COMPETENCIAS;
            vListaAreasInteres = vDescriptivo.LST_AREAS_INTERES;
            vListaAreas = vDescriptivo.LST_AREAS;
            vListaCentroAdmvo = vDescriptivo.LST_CENTRO_ADMVO;
            vListaCentroOptvo = vDescriptivo.LST_CENTRO_OPTVO;
            vListaPuestos = vDescriptivo.LST_PUESTOS;
            vListaIndicadorDesempeno = vDescriptivo.LST_INDICADORES;
            vXmlAdicionales = vDescriptivo.XML_CODIGO_CAMPOS_ADICIONALES;
            vNoPlazas = vDescriptivo.NO_PLAZAS;
            vNoMinimoPlazas = vDescriptivo.NO_MINIMO_PLAZAS;

            vXmlDocumentos = vDescriptivo.XML_DOCUMENTOS;

            txtNoPlazas.MinValue = (double)vNoMinimoPlazas;

            vFuncionesGenericas = new List<E_FUNCION_GENERICA>();
            vLstFuncionCompetencia = new List<E_FUNCION_COMPETENCIA>();
            vLstFuncionCompetenciaIndicador = new List<E_FUNCION_INDICADOR>();

            if (vDescriptivo.NO_NIVEL_ORGANIGRAMA != null)
                txtNivelOrg.Text = vDescriptivo.NO_NIVEL_ORGANIGRAMA.ToString();

            asignarValoresAdicionales(vDescriptivo.XML_CAMPOS_ADICIONALES);

            if (vDescriptivo.LST_OCUPACION_PUESTO != null)
            {
                btnEliminarOcupacionPuesto.Visible = true;
                cmbAreaO.SelectedValue = vDescriptivo.LST_OCUPACION_PUESTO.CL_AREA.ToString();

                List<SPE_OBTIENE_SUBAREA_OCUPACION_Result> vSubarea = negocio.Obtener_SUBAREA_OCUPACION(PIN_CL_AREA: vDescriptivo.LST_OCUPACION_PUESTO.CL_AREA.ToString());
                cmbSubarea.DataValueField = "CL_SUBAREA";
                cmbSubarea.DataTextField = "NB_SUBAREA";
                cmbSubarea.DataSource = vSubarea;
                cmbSubarea.DataBind();
                cmbSubarea.SelectedValue = vDescriptivo.LST_OCUPACION_PUESTO.CL_SUBAREA.ToString();

                List<SPE_OBTIENE_MODULO_OCUPACION_Result> vModulo = negocio.Obtener_MODULO_OCUPACION(PIN_CL_AREA: vDescriptivo.LST_OCUPACION_PUESTO.CL_AREA.ToString(), PIN_CL_SUBAREA: vDescriptivo.LST_OCUPACION_PUESTO.CL_SUBAREA.ToString());
                cmbModulo.DataValueField = "CL_MODULO";
                cmbModulo.DataTextField = "NB_MODULO";
                cmbModulo.DataSource = vModulo;
                cmbModulo.DataBind();
                cmbModulo.SelectedValue = vDescriptivo.LST_OCUPACION_PUESTO.CL_MODULO;

                List<SPE_OBTIENE_OCUPACIONES_Result> vOcupacion = negocio.Obtener_OCUPACIONES(PIN_CL_AREA: vDescriptivo.LST_OCUPACION_PUESTO.CL_AREA.ToString(), PIN_CL_SUBAREA: vDescriptivo.LST_OCUPACION_PUESTO.CL_SUBAREA.ToString(), PIN_CL_MODULO: vDescriptivo.LST_OCUPACION_PUESTO.CL_MODULO.ToString());
                cmbOcupaciones.DataValueField = "CL_OCUPACION";
                cmbOcupaciones.DataTextField = "NB_OCUPACION";
                cmbOcupaciones.DataSource = vOcupacion;
                cmbOcupaciones.DataBind();
                cmbOcupaciones.SelectedValue = vDescriptivo.LST_OCUPACION_PUESTO.CL_OCUPACION;

                lblClOcupación.Text = vDescriptivo.LST_OCUPACION_PUESTO.CL_OCUPACION;
                lblOcupacionSeleccionada.Text = vDescriptivo.LST_OCUPACION_PUESTO.NB_OCUPACION;
            }
            else
            {
                btnEliminarOcupacionPuesto.Visible = false;
            }


            if (pIdDescriptivo != null)
            {
                txtNombreCorto.Text = vDescriptivo.CL_PUESTO;
                txtDescripcionPuesto.Text = vDescriptivo.NB_PUESTO;
                txtNoPlazas.Value = (double?)vDescriptivo.NO_PLAZAS;

                txtRangoEdadMin.Value = vDescriptivo.NO_EDAD_MINIMA;
                txtRangoEdadMax.Value = vDescriptivo.NO_EDAD_MAXIMA;
               // txtCompetenciasRequeridas.Text = vDescriptivo.DS_COMPETENCIAS_REQUERIDAS;

                var xmlPuestoEscolaridad = XElement.Parse(vDescriptivo.XML_PUESTO_ESCOLARIDAD).Element("OTRO_PUESTO_ESCOLARIDAD");

                if (xmlPuestoEscolaridad.HasAttributes)
                    txtOtroNivelEst.Text = xmlPuestoEscolaridad.Attribute("DS_OTRO_NIVEL_ESCOLARIDAD").Value;

                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTO_ESCOLARIDAD).Elements("PUESTO_ESCOLARIDAD"))
                {
                    if (item.Attribute("CL_TIPO_ESCOLARIDAD").Value == E_CL_TIPO_ESCOLARIDAD.POSTGRADO.ToString())
                    {
                        RadListBoxItem i = new RadListBoxItem();

                        i.Text = item.Attribute("NB_ESCOLARIDAD").Value;
                        i.Value = item.Attribute("ID_ESCOLARIDAD").Value;
                        lstPostgrados.Items.Add(i);
                    }
                }

                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTO_ESCOLARIDAD).Elements("PUESTO_ESCOLARIDAD"))
                {
                    if (item.Attribute("CL_TIPO_ESCOLARIDAD").Value == E_CL_TIPO_ESCOLARIDAD.PROFESIONAL.ToString())
                    {
                        RadListBoxItem i = new RadListBoxItem();

                        i.Text = item.Attribute("NB_ESCOLARIDAD").Value;
                        i.Value = item.Attribute("ID_ESCOLARIDAD").Value;
                        lstCarreraprof.Items.Add(i);
                    }
                }

                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTO_ESCOLARIDAD).Elements("PUESTO_ESCOLARIDAD"))
                {
                    if (item.Attribute("CL_TIPO_ESCOLARIDAD").Value == E_CL_TIPO_ESCOLARIDAD.TECNICA.ToString())
                    {
                        RadListBoxItem i = new RadListBoxItem();

                        i.Text = item.Attribute("NB_ESCOLARIDAD").Value;
                        i.Value = item.Attribute("ID_ESCOLARIDAD").Value;
                        lstCarreraTec.Items.Add(i);
                    }
                }

                foreach (E_COMPETENCIAS item in vListaCompetencias.Where(n => n.CL_PUESTO_TIPO_COMPETENCIA == "PERFIL"))
                {
                    RadListBoxItem i = new RadListBoxItem();

                    i.Text = item.NB_COMPETENCIA;
                    i.Value = item.ID_COMPETENCIA.ToString();
                    lstCompetenciasEspecificas.Items.Add(i);
                }


                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTO_EXPERIENCIA).Elements("PUESTO_EXPERIENCIA"))
                {
                    E_EXPERIENCIA ep = new E_EXPERIENCIA();
                    ep.CL_TIPO_EXPERIENCIA = item.Attribute("CL_NIVEL_REQUERIDO").Value;
                    ep.ID_AREA_INTERES = int.Parse(item.Attribute("ID_AREA_INTERES").Value);
                    ep.NB_AREA_INTERES = item.Attribute("NB_AREA_INTERES").Value;
                    ep.NO_TIEMPO = decimal.Parse(item.Attribute("NO_TIEMPO").Value);

                    vListaExperiencia.Add(ep);
                }

                lstExperiencia.DataSource = vListaExperiencia;
                lstExperiencia.DataBind();

                if (vDescriptivo.XML_REQUERIMIENTOS != null && vDescriptivo.XML_REQUERIMIENTOS != "")
                {
                    if (vDescriptivo.XML_REQUERIMIENTOS.Contains("DS_NOTA"))
                    {
                        radEditorRequerimientos.Content = Utileria.MostrarNotas(vDescriptivo.XML_REQUERIMIENTOS);
                    }
                    else
                    {
                        XElement vRequerimientos = XElement.Parse(vDescriptivo.XML_REQUERIMIENTOS);
                        if (vRequerimientos != null)
                        {
                            vRequerimientos.Name = vNbFirstRadEditorTagName;
                            radEditorRequerimientos.Content = vRequerimientos.ToString();
                        }
                    }
                }

                if (vDescriptivo.XML_OBSERVACIONES != null && vDescriptivo.XML_OBSERVACIONES != "")
                {

                    if (vDescriptivo.XML_OBSERVACIONES.Contains("DS_NOTA"))
                    {
                        radEditorObservaciones.Content = Utileria.MostrarNotas(vDescriptivo.XML_OBSERVACIONES);
                    }
                    else
                    {
                        XElement vObservaciones = XElement.Parse(vDescriptivo.XML_OBSERVACIONES);
                        if (vObservaciones != null)
                        {
                            vObservaciones.Name = vNbFirstRadEditorTagName;
                            radEditorObservaciones.Content = vObservaciones.ToString();
                        }
                    }
                }

                btnDirecto.Checked = vDescriptivo.CL_TIPO_PUESTO == "DIRECTO";
                btnIndirecto.Checked = vDescriptivo.CL_TIPO_PUESTO == "INDIRECTO";

                //foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTOS_RELACIONADOS).Elements("PUESTO_RELACIONADO"))
                //{
                //    if (item.Attribute("CL_TIPO_RELACION").Value == E_PUESTO_RELACION.JEFE.ToString())
                //    {
                //        lstJefeInmediato.Items.Clear();

                //        RadListBoxItem i = new RadListBoxItem();

                //        i.Text = item.Attribute("NB_PUESTO").Value;
                //        i.Value = item.Attribute("ID_PUESTO_RELACIONADO").Value;
                //        lstJefeInmediato.Items.Add(i);
                //    }
                //}

                //foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTOS_RELACIONADOS).Elements("PUESTO_RELACIONADO"))
                //{
                //    if (item.Attribute("CL_TIPO_RELACION").Value == E_PUESTO_RELACION.SUBORDINADO.ToString())
                //    {
                //        RadListBoxItem i = new RadListBoxItem();

                //        i.Text = item.Attribute("NB_PUESTO").Value;
                //        i.Value = item.Attribute("ID_PUESTO_RELACIONADO").Value;
                //        lstPuestosSubordinado.Items.Add(i);
                //    }
                //}

                //foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTOS_RELACIONADOS).Elements("PUESTO_RELACIONADO"))
                //{
                //    if (item.Attribute("CL_TIPO_RELACION").Value == E_PUESTO_RELACION.INTERRELACIONADO.ToString())
                //    {
                //        RadListBoxItem i = new RadListBoxItem();

                //        i.Text = item.Attribute("NB_PUESTO").Value;
                //        i.Value = item.Attribute("ID_PUESTO_RELACIONADO").Value;
                //        lstPuestosInterrelacionados.Items.Add(i);
                //    }
                //}
                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTOS_RELACIONADOS).Elements("PUESTO_RELACIONADO"))
                {
                    if (item.Attribute("CL_TIPO_RELACION").Value == E_PUESTO_RELACION.INTERRELACIONADO.ToString())
                    {
                        vLstInterrelacionados.Add(new E_PUESTOS
                        {
                            ID_PUESTO = int.Parse(item.Attribute("ID_PUESTO").Value),
                            ID_PUESTO_RELACION = int.Parse(item.Attribute("ID_PUESTO_RELACIONADO").Value),
                            CL_PUESTO = item.Attribute("CL_PUESTO").Value,
                            NB_PUESTO = item.Attribute("NB_PUESTO").Value,
                            CL_TIPO_RELACION = item.Attribute("CL_TIPO_RELACION").Value
                        });
                    }
                }


                btnLinea.Checked = vDescriptivo.CL_POSICION_ORGANIGRAMA == "LINEA";
                btnStaff.Checked = vDescriptivo.CL_POSICION_ORGANIGRAMA == "STAFF";

                //foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTOS_RELACIONADOS).Elements("PUESTO_RELACIONADO"))
                //{
                //    if (item.Attribute("CL_TIPO_RELACION").Value == E_PUESTO_RELACION.RUTAALTERNATIVA.ToString())
                //    {
                //        RadListBoxItem i = new RadListBoxItem();

                //        i.Text = item.Attribute("NB_PUESTO").Value;
                //        i.Value = item.Attribute("ID_PUESTO_RELACIONADO").Value;
                //        lstAlternativa.Items.Add(i);
                //    }
                //}
                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTOS_RELACIONADOS).Elements("PUESTO_RELACIONADO"))
                {
                    if (item.Attribute("CL_TIPO_RELACION").Value == E_PUESTO_RELACION.RUTAALTERNATIVA.ToString())
                    {
                        vLstAlternativa.Add(new E_PUESTOS
                        {
                            ID_PUESTO = int.Parse(item.Attribute("ID_PUESTO").Value),
                            ID_PUESTO_RELACION = int.Parse(item.Attribute("ID_PUESTO_RELACIONADO").Value),
                            CL_PUESTO = item.Attribute("CL_PUESTO").Value,
                            NB_PUESTO = item.Attribute("NB_PUESTO").Value,
                            CL_TIPO_RELACION = item.Attribute("CL_TIPO_RELACION").Value
                        });
                    }
                }

                //foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTOS_RELACIONADOS).Elements("PUESTO_RELACIONADO"))
                //{
                //    if (item.Attribute("CL_TIPO_RELACION").Value == E_PUESTO_RELACION.RUTALATERAL.ToString())
                //    {
                //        RadListBoxItem i = new RadListBoxItem();

                //        i.Text = item.Attribute("NB_PUESTO").Value;
                //        i.Value = item.Attribute("ID_PUESTO_RELACIONADO").Value;
                //        lstLateral.Items.Add(i);
                //    }
                //}
                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTOS_RELACIONADOS).Elements("PUESTO_RELACIONADO"))
                {
                    if (item.Attribute("CL_TIPO_RELACION").Value == E_PUESTO_RELACION.RUTALATERAL.ToString())
                    {
                        vLstLaterales.Add(new E_PUESTOS
                        {
                            ID_PUESTO = int.Parse(item.Attribute("ID_PUESTO").Value),
                            ID_PUESTO_RELACION = int.Parse(item.Attribute("ID_PUESTO_RELACIONADO").Value),
                            CL_PUESTO = item.Attribute("CL_PUESTO").Value,
                            NB_PUESTO = item.Attribute("NB_PUESTO").Value,
                            CL_TIPO_RELACION = item.Attribute("CL_TIPO_RELACION").Value
                        });
                    }
                }

                if (vDescriptivo.XML_RESPONSABILIDAD != null && vDescriptivo.XML_RESPONSABILIDAD != "")
                {
                    if (vDescriptivo.XML_RESPONSABILIDAD.Contains("DS_NOTA"))
                    {
                        radEditorResponsable.Content = Utileria.MostrarNotas(vDescriptivo.XML_RESPONSABILIDAD);
                    }
                    else
                    {
                        XElement vResponsabilidad = XElement.Parse(vDescriptivo.XML_RESPONSABILIDAD);
                        if (vResponsabilidad != null)
                        {
                            vResponsabilidad.Name = vNbFirstRadEditorTagName;
                            radEditorResponsable.Content = vResponsabilidad.ToString();
                        }
                    }
                }

                if (vDescriptivo.XML_AUTORIDAD != null && vDescriptivo.XML_AUTORIDAD != "")
                {
                    if (vDescriptivo.XML_AUTORIDAD.Contains("DS_NOTA"))
                    {
                        radEditorAutoridad.Content = Utileria.MostrarNotas(vDescriptivo.XML_AUTORIDAD);
                    }
                    else
                    {
                        XElement vAutoridad = XElement.Parse(vDescriptivo.XML_AUTORIDAD);
                        if (vAutoridad != null)
                        {
                            vAutoridad.Name = vNbFirstRadEditorTagName;
                            radEditorAutoridad.Content = vAutoridad.ToString();
                        }
                    }
                }

                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTO_FUNCION).Elements("PUESTO_FUNCION"))
                {
                    E_FUNCION_GENERICA fg = new E_FUNCION_GENERICA();
                    if (item.Element("XML_DETALLE") != null)
                    {

                        XElement dsDetalle = item.Element("XML_DETALLE").Element("DS_DETALLE");
                        dsDetalle.Name = vNbFirstRadEditorTagName;

                        if (item.Element("XML_NOTAS") != null)
                        {
                            XElement dsNotas = item.Element("XML_NOTAS").Element("DS_NOTAS");
                            dsNotas.Name = vNbFirstRadEditorTagName;
                            fg.DS_NOTAS = dsNotas.ToString();
                        }

                        fg.ID_FUNCION_GENERICA = int.Parse(item.Attribute("ID_PUESTO_FUNCION").Value);
                        fg.NB_FUNCION_GENERICA = item.Attribute("NB_PUESTO_FUNCION").Value;
                        fg.DS_DETALLE = dsDetalle.ToString();


                        vFuncionesGenericas.Add(fg);
                    }
                }

                foreach (E_COMPETENCIAS item in vListaCompetencias.Where(n => n.ID_PUESTO_FUNCION != 0))
                {
                    E_FUNCION_COMPETENCIA fc = new E_FUNCION_COMPETENCIA();

                    fc.ID_COMPETENCIA = item.ID_COMPETENCIA.Value;
                    fc.ID_FUNCION_GENERICA = item.ID_PUESTO_FUNCION;
                    fc.ID_FUNCION_COMPETENCIA = item.ID_PUESTO_COMPETENCIA;
                    fc.NB_COMPETENCIA = item.NB_COMPETENCIA;
                    fc.DS_COMPETENCIA = item.DS_COMPETENCIA;
                    fc.NO_NIVEL = item.NO_VALOR_NIVEL;
                    fc.NB_NIVEL = CrearNivelCompetencia(null).FirstOrDefault(f => f.NO_VALOR.Equals(item.NO_VALOR_NIVEL)).NB_NIVEL;
                    if (vFuncionesGenericas.Count > 0)
                        if (vFuncionesGenericas.Exists(x => x.ID_FUNCION_GENERICA.Equals(item.ID_PUESTO_FUNCION)))
                        fc.ID_PARENT_ITEM = vFuncionesGenericas.FirstOrDefault(f => item.ID_PUESTO_FUNCION.Equals(f.ID_FUNCION_GENERICA)).ID_ITEM;

                    vLstFuncionCompetencia.Add(fc);
                }

                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTO_INDICADOR).Elements("PUESTO_INDICADOR"))
                {
                    E_FUNCION_INDICADOR fi = new E_FUNCION_INDICADOR();

                    fi.ID_FUNCION_COMPETENCIA = UtilXML.ValorAtributo<int>(item.Attribute("ID_PUESTO_COMPETENCIA"));
                    fi.ID_FUNCION_INDICADOR = UtilXML.ValorAtributo<int>(item.Attribute("ID_FUNCION_INDICADOR"));
                    fi.ID_FUNCION_GENERICA = UtilXML.ValorAtributo<int>(item.Attribute("ID_PUESTO_FUNCION"));
                    fi.ID_INDICADOR_DESEMPENO = UtilXML.ValorAtributo<int>(item.Attribute("ID_INDICADOR"));
                    fi.NB_INDICADOR_DESEMPENO = UtilXML.ValorAtributo<string>(item.Attribute("NB_INDICADOR"));

                    fi.ID_PARENT_ITEM = vLstFuncionCompetencia.FirstOrDefault(f => UtilXML.ValorAtributo<int>(item.Attribute("ID_PUESTO_COMPETENCIA")).Equals(f.ID_FUNCION_COMPETENCIA)).ID_ITEM;

                    vLstFuncionCompetenciaIndicador.Add(fi);
                }

                vLstFuncionCompetencia.ForEach(f => CrearDsIndicadores(f, vLstFuncionCompetenciaIndicador));
                cmbTipoPrestaciones.SelectedValue = vDescriptivo.CL_TIPO_PRESTACIONES;

                if (vDescriptivo.XML_PRESTACIONES != null && vDescriptivo.XML_PRESTACIONES != "")
                {
                    if (vDescriptivo.XML_PRESTACIONES.Contains("DS_NOTA"))
                    {
                        radEditorPrestaciones.Content = Utileria.MostrarNotas(vDescriptivo.XML_PRESTACIONES);
                    }
                    else
                    {
                        XElement vPrestaciones = XElement.Parse(vDescriptivo.XML_PRESTACIONES);
                        if (vPrestaciones != null)
                        {
                            vPrestaciones.Name = vNbFirstRadEditorTagName;
                            radEditorPrestaciones.Content = vPrestaciones.ToString();
                        }
                    }
                }

                if (ContextoApp.FgControlDocumentos)
                {
                    txtClaveDocumento.Text = vDescriptivo.CL_DOCUMENTO;
                    txtVersionDocumento.Text = vDescriptivo.CL_VERSION;
                    DateTime vFechaRev = new DateTime(1900, 01, 01);
                    if (vDescriptivo.FE_ELABORACION.HasValue)
                    {
                        int result = DateTime.Compare(vDescriptivo.FE_ELABORACION.Value, vFechaRev);
                        if (result <= 0)
                        {
                            rdtFeElabDocumento.SelectedDate = null;
                        }
                        else
                        {
                            rdtFeElabDocumento.SelectedDate = vDescriptivo.FE_ELABORACION.Value;
                        }
                    }
                    txtElaboroDocumento.Text = vDescriptivo.NB_ELABORO;

                    if (vDescriptivo.FE_REVISION.HasValue)
                    {
                        int result = DateTime.Compare(vDescriptivo.FE_REVISION.Value, vFechaRev);
                        if (result <= 0)
                        {
                            rdtFeRevDocumento.SelectedDate = null;

                        }
                        else
                        {
                            rdtFeRevDocumento.SelectedDate = vDescriptivo.FE_REVISION.Value;
                        }
                    }

                    txtRevisoDocumento.Text = vDescriptivo.NB_REVISO;

                    if (vDescriptivo.FE_AUTORIZACION.HasValue)
                    {
                        int result = DateTime.Compare(vDescriptivo.FE_AUTORIZACION.Value, vFechaRev);
                        if (result <= 0)
                        {
                            rdtFeAutorizoDocumento.SelectedDate = null;

                        }
                        else
                        {

                            rdtFeAutorizoDocumento.SelectedDate = vDescriptivo.FE_AUTORIZACION.Value;
                        }
                    }
                    txtAutorizoDocumento.Text = vDescriptivo.NB_AUTORIZO;
                    txtControlCambios.Text = vDescriptivo.DS_CONTROL_CAMBIOS;
                }

                if (vIsCopy)
                    vIdDescriptivo = null;
            }
        }

        public void ObtenerAreaInteres()
        {
            cmbExperiencias.DataSource = vListaAreasInteres;
            cmbExperiencias.DataTextField = "NB_AREA_INTERES";
            cmbExperiencias.DataValueField = "ID_AREA_INTERES";
            cmbExperiencias.DataBind();
        }

        public void ObtenerCompetenciasEspecificas()
        {
            cmbCompetenciaEspecificas.DataSource = vListaCatalogoCompetencias.Where(n => n.CL_TIPO_COMPETENCIA == "ESP");
            cmbCompetenciaEspecificas.DataTextField = "NB_COMPETENCIA";
            cmbCompetenciaEspecificas.DataValueField = "ID_COMPETENCIA";
            cmbCompetenciaEspecificas.DataBind();

            cmbCompetenciaEspecifica.DataSource = vListaCatalogoCompetencias.Where(n => n.CL_TIPO_COMPETENCIA == "ESP");
            cmbCompetenciaEspecifica.DataTextField = "NB_COMPETENCIA";
            cmbCompetenciaEspecifica.DataValueField = "ID_COMPETENCIA";
            cmbCompetenciaEspecifica.DataBind();
        }

        public void ObtenerEstadosCiviles()
        {
            cmbEdoCivil.Items.Add(new RadComboBoxItem("Indistinto", "0") { Selected = !vListaEdoCivil.Any(a => a.FG_SELECCIONADO) });
            cmbEdoCivil.Items.AddRange(vListaEdoCivil.Select(s => new RadComboBoxItem(s.NB_CATALOGO_VALOR, s.CL_CATALOGO_VALOR)
            {
                Selected = s.FG_SELECCIONADO
            }));
        }

        public void ObtenerGeneros()
        {
            cmbGenero.Items.Add(new RadComboBoxItem("Indistinto", "0") { Selected = !vListaGenero.Any(a => a.FG_SELECCIONADO) });
            cmbGenero.Items.AddRange(vListaGenero.Select(s => new RadComboBoxItem(s.NB_CATALOGO_VALOR, s.CL_CATALOGO_VALOR)
            {
                Selected = s.FG_SELECCIONADO
            }));
        }

        public void ObtenerEscolaridad(string pNivelEscolaridad, RadComboBox radCmb)
        {
            radCmb.DataSource = vListaEscoloridad.Where(w => w.CL_NB_NIVEL_ESCOLARIDAD.Equals(pNivelEscolaridad) && w.FG_ACTIVO.Equals(true)).ToList();
            radCmb.DataTextField = "NB_ESCOLARIDAD";
            radCmb.DataValueField = "ID_ESCOLARIDAD";
            radCmb.DataBind();
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

                vIdItemFotografia = vDocumento.ID_ITEM;
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
            XElement x = XElement.Parse(vXmlDocumentos);

            if (vLstDocumentos == null)
                vLstDocumentos = new List<E_DOCUMENTO>();

            foreach (XElement item in (x.Elements("ITEM")))
                vLstDocumentos.Add(new E_DOCUMENTO()
                {
                    ID_ITEM = new Guid(UtilXML.ValorAtributo<string>(item.Attribute("ID_ITEM"))),
                    NB_DOCUMENTO = UtilXML.ValorAtributo<string>(item.Attribute("NB_DOCUMENTO")),
                    ID_DOCUMENTO = UtilXML.ValorAtributo<int>(item.Attribute("ID_DOCUMENTO")),
                    ID_ARCHIVO = UtilXML.ValorAtributo<int>(item.Attribute("ID_ARCHIVO")),
                    CL_TIPO_DOCUMENTO = UtilXML.ValorAtributo<string>(item.Attribute("CL_TIPO_DOCUMENTO"))
                });
        }

        //public void ObtenerAreas()
        //{
        //    cmbAreas.Items.AddRange(vListaAreas.Select(s => new RadComboBoxItem(s.NB_DEPARTAMENTO, s.ID_DEPARTAMENTO.Value.ToString())
        //    {
        //        Selected = s.FG_SELECCIONADO
        //    }));
        //}

        //public void ObtenerCentroAdmvo()
        //{
        //    cmbAdministrativo.Items.AddRange(vListaCentroAdmvo.Select(s => new RadComboBoxItem(s.NB_CENTRO_ADMVO, s.ID_CENTRO_ADMVO)
        //    {
        //        Selected = s.FG_SELECCIONADO
        //    }));

        //    if (cmbAdministrativo.Items.Count == 0)
        //    {
        //        cmbAdministrativo.Enabled = false;
        //    }

        //}

        //public void ObtenerCentroOptvo()
        //{
        //    cmbOperativo.Items.AddRange(vListaCentroOptvo.Select(s => new RadComboBoxItem(s.NB_CENTRO_OPTVO, s.ID_CENTRO_OPTVO)
        //    {
        //        Selected = s.FG_SELECCIONADO
        //    }));

        //    if (cmbOperativo.Items.Count == 0)
        //    {
        //        cmbOperativo.Enabled = false;
        //    }

        //}

        public void ObtenerPuestos(string pPuesto, RadComboBox radCmb)
        {
            //radCmb.DataSource = vListaPuestos.Where(w => w.CL_TIPO_RELACION.Equals(pPuesto)).ToList();
            radCmb.DataSource = vListaPuestos;
            radCmb.DataTextField = "NB_PUESTO";
            radCmb.DataValueField = "ID_PUESTO";
            radCmb.DataBind();
        }

        public void agregarToolTip()
        {
            foreach (GridDataItem item in dgvCompetencias.MasterTableView.Items)
            {
                if (item.DataItem != null)
                {
                    E_COMPETENCIAS c = (E_COMPETENCIAS)item.DataItem;
                    RadSlider vRsFila = (RadSlider)item.FindControl("rsNivel1");

                    vRsFila.Items[0].ToolTip = StripHtml(c.DS_COMENTARIOS_NIVEL0);
                    vRsFila.Items[1].ToolTip = StripHtml(c.DS_COMENTARIOS_NIVEL1);
                    vRsFila.Items[2].ToolTip = StripHtml(c.DS_COMENTARIOS_NIVEL2);
                    vRsFila.Items[3].ToolTip = StripHtml(c.DS_COMENTARIOS_NIVEL3);
                    vRsFila.Items[4].ToolTip = StripHtml(c.DS_COMENTARIOS_NIVEL4);
                    vRsFila.Items[5].ToolTip = StripHtml(c.DS_COMENTARIOS_NIVEL5);
                }
            }
        }

        private string StripHtml(string source)
        {
            string output;

            //get rid of HTML tags
            output = Regex.Replace(source, "<[^>]*>", string.Empty);

            //get rid of multiple blank lines
            output = Regex.Replace(output, @"^\s*$\n", string.Empty, RegexOptions.Multiline);

            return output;
        }

        public string ocupaciones(string clOcupacion)
        {
            XElement vPuestoOcupacion = new XElement("PUESTOSOCUPACIONES");
            if (vIdDescriptivo == null)
            {
                vIdDescriptivo = 0;
            }
            if (clOcupacion != "")
            {
                var ocupacion = negocio.Obtener_OCUPACIONES(PIN_CL_OCUPACION: clOcupacion).FirstOrDefault();

                vPuestoOcupacion.Add((new XElement("OCUPACION",
                                            new XAttribute("ID_PUESTO", vIdDescriptivo),
                                            new XAttribute("ID_OCUPACION", ocupacion.ID_OCUPACION),
                                            new XAttribute("CL_OCUPACION", ocupacion.CL_OCUPACION),
                                            new XAttribute("CL_MODULO", ocupacion.CL_MODULO),
                                            new XAttribute("CL_SUBAREA", ocupacion.CL_SUBAREA),
                                            new XAttribute("CL_AREA", ocupacion.CL_AREA))));
            }
            return vPuestoOcupacion.ToString();
        }

        public string generarXml(XElement pXmlAdicionales)
        {
            XElement pPuesto = new XElement("PUESTO");

            if (vIdDescriptivo == null)
            {
                vIdDescriptivo = 0;
            }

            //XElement vXmlPrestaciones = XElement.Parse(String.Format("<XML_PRESTACIONES>{0}</XML_PRESTACIONES>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorPrestaciones.Content))));
            //XElement vXmlRequerimientos = XElement.Parse(String.Format("<XML_REQUERIMIENTOS>{0}</XML_REQUERIMIENTOS>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorRequerimientos.Content))));
            //XElement vXmlObservaciones = XElement.Parse(String.Format("<XML_OBSERVACIONES>{0}</XML_OBSERVACIONES>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorObservaciones.Content))));
            //XElement vXmlResponsabilidad = XElement.Parse(String.Format("<XML_RESPONSABILIDAD>{0}</XML_RESPONSABILIDAD>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorResponsable.Content))));
            //XElement vXmlAutoridad = XElement.Parse(String.Format("<XML_AUTORIDAD>{0}</XML_AUTORIDAD>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorAutoridad.Content))));

            string vFechaElabora = "";
            string vFechaRevisa = "";
            string vFechaAutoriza = "";

            if (rdtFeElabDocumento.SelectedDate != null)
            {
                vFechaElabora = rdtFeElabDocumento.SelectedDate.Value.ToString("yyyy/MM/dd");
            }

            if (rdtFeRevDocumento.SelectedDate != null)
            {
                vFechaRevisa = rdtFeRevDocumento.SelectedDate.Value.ToString("yyyy/MM/dd");
            }
            if (rdtFeAutorizoDocumento.SelectedDate != null)
            {
                vFechaAutoriza = rdtFeAutorizoDocumento.SelectedDate.Value.ToString("yyyy/MM/dd");
            }

            int? vNoPlazas = 0;
            if ((int?)txtNoPlazas.Value != null)
                vNoPlazas = (int?)txtNoPlazas.Value;
            else
                vNoPlazas = 1;


            //DATOS GENERALES DEL PUESTO
            XElement pDescriptivo = new XElement("DESCRIPTIVO",
                new XAttribute("ID_DESCRIPTIVO", vIdDescriptivo),
                new XAttribute("CL_PUESTO", txtNombreCorto.Text),
                new XAttribute("NB_PUESTO", txtDescripcionPuesto.Text),
                new XAttribute("NO_EDAD_MINIMA", (txtRangoEdadMin.Value == null) ? 18.0 : txtRangoEdadMin.Value),
                new XAttribute("NO_EDAD_MAXIMA", (txtRangoEdadMax.Value == null) ? 65.0 : txtRangoEdadMax.Value),
                new XAttribute("CL_GENERO", cmbGenero.SelectedValue),
                new XAttribute("CL_ESTADO_CIVIL", cmbEdoCivil.SelectedValue),
                new XAttribute("CL_TIPO_PUESTO", btnDirecto.Checked ? "DIRECTO" : "INDIRECTO"),
                //new XAttribute("ID_CENTRO_ADMINISTRATIVO", cmbAdministrativo.SelectedValue),
                //new XAttribute("ID_CENTRO_OPERATIVO", cmbOperativo.SelectedValue),
              //  new XAttribute("ID_DEPARTAMENTO", cmbAreas.SelectedValue),
                new XAttribute("CL_POSICION_ORGANIGRAMA", btnStaff.Checked ? "STAFF" : "LINEA"),
                new XAttribute("NO_NIVEL_ORGANIGRAMA", txtNivelOrg.Text),
                new XAttribute("CL_DOCUMENTO", ContextoApp.FgControlDocumentos ? txtClaveDocumento.Text : ""),
                new XAttribute("CL_VERSION", ContextoApp.FgControlDocumentos ? txtVersionDocumento.Text : ""),
                new XAttribute("FE_ELABORACION", ContextoApp.FgControlDocumentos ? vFechaElabora : ""),
                new XAttribute("NB_ELABORO", ContextoApp.FgControlDocumentos ? txtElaboroDocumento.Text : ""),
                new XAttribute("FE_REVISION", ContextoApp.FgControlDocumentos ? vFechaRevisa : ""),
                new XAttribute("NB_REVISO", ContextoApp.FgControlDocumentos ? txtRevisoDocumento.Text : ""),
                new XAttribute("FE_AUTORIZACION", ContextoApp.FgControlDocumentos ? vFechaAutoriza : ""),
                new XAttribute("NB_AUTORIZO", ContextoApp.FgControlDocumentos ? txtAutorizoDocumento.Text : ""),
                new XAttribute("DS_CONTROL_CAMBIOS", ContextoApp.FgControlDocumentos ? txtControlCambios.Text : ""),
                new XAttribute("CL_TIPO_PRESTACIONES", cmbTipoPrestaciones.SelectedValue),
                new XAttribute("NO_PLAZAS", vNoPlazas),
               // new XAttribute("DS_COMPETENCIAS_REQUERIDAS", txtCompetenciasRequeridas.Text),
                new XAttribute("DS_COMPETENCIAS_REQUERIDAS", ""),

               Utileria.GuardarNotas(radEditorPrestaciones.Content, "NOTAS_PRESTACIONES"), // EditorContentToXml("XML_PRESTACIONES", radEditorPrestaciones.Content.Replace("&lt;",""), vNbFirstRadEditorTagName),
               Utileria.GuardarNotas(radEditorRequerimientos.Content, "NOTAS_REQUERIMIENTOS"),// EditorContentToXml("XML_REQUERIMIENTOS", radEditorRequerimientos.Content.Replace("&lt;",""), vNbFirstRadEditorTagName),
               Utileria.GuardarNotas(radEditorObservaciones.Content, "NOTAS_OBSERVACIONES"),// EditorContentToXml("XML_OBSERVACIONES", radEditorObservaciones.Content.Replace("&lt;",""), vNbFirstRadEditorTagName),
               Utileria.GuardarNotas(radEditorResponsable.Content, "NOTAS_RESPONSABILIDAD"),// EditorContentToXml("XML_RESPONSABILIDAD", radEditorResponsable.Content.Replace("&lt;",""), vNbFirstRadEditorTagName),
               Utileria.GuardarNotas(radEditorAutoridad.Content, "NOTAS_AUTORIDAD"),// EditorContentToXml("XML_AUTORIDAD", radEditorAutoridad.Content.Replace("&lt;",""), vNbFirstRadEditorTagName),

                //XElement.Parse(String.Format("<XML_PRESTACIONES>{0}</XML_PRESTACIONES>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorPrestaciones.Content)))),
                //XElement.Parse(String.Format("<XML_REQUERIMIENTOS>{0}</XML_REQUERIMIENTOS>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorRequerimientos.Content)))),
                //XElement.Parse(String.Format("<XML_OBSERVACIONES>{0}</XML_OBSERVACIONES>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorObservaciones.Content)))),
                //XElement.Parse(String.Format("<XML_RESPONSABILIDAD>{0}</XML_RESPONSABILIDAD>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorResponsable.Content)))),
                //XElement.Parse(String.Format("<XML_AUTORIDAD>{0}</XML_AUTORIDAD>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorAutoridad.Content)))),
                new XElement(pXmlAdicionales));

            //LISTA DE ESCOLARIDADES DEL PUESTO
            XElement pEscolaridad = new XElement("ESCOLARIDADES");

            foreach (RadListBoxItem item in lstPostgrados.Items)
            {
                pEscolaridad.Add(new XElement("ESCOLARIDAD",
                    new XAttribute("ID_ESCOLARIDAD", item.Value)));
            }

            foreach (RadListBoxItem item in lstCarreraprof.Items)
            {
                pEscolaridad.Add(new XElement("ESCOLARIDAD",
                    new XAttribute("ID_ESCOLARIDAD", item.Value)));
            }

            foreach (RadListBoxItem item in lstCarreraTec.Items)
            {
                pEscolaridad.Add(new XElement("ESCOLARIDAD",
                    new XAttribute("ID_ESCOLARIDAD", item.Value)));
            }

            pEscolaridad.Add(new XElement("OTRO",
                    new XAttribute("DS_OTRO_NIVEL_ESCOLARIDAD", (txtOtroNivelEst.Text == null) ? "" : txtOtroNivelEst.Text)));

            //LISTA DE RELACION DE PUESTOS

            XElement pPuestosRel = new XElement("PUESTOS_REL");

            //SUBORDINADOS
            //foreach (RadListBoxItem item in lstPuestosSubordinado.Items)
            //{
            //    pPuestosRel.Add(new XElement("PUESTO_REL",
            //        new XAttribute("ID_PUESTO_REL", item.Value),
            //        new XAttribute("CL_TIPO_RELACION", E_PUESTO_RELACION.SUBORDINADO.ToString())));
            //}

            //INTERRELACIONADOS
            foreach (GridDataItem item in rgInterrelacionados.MasterTableView.Items)
            {
                pPuestosRel.Add(new XElement("PUESTO_REL",
                    new XAttribute("ID_PUESTO_REL", item.GetDataKeyValue("ID_PUESTO_RELACION").ToString()),
                    new XAttribute("CL_TIPO_RELACION", E_PUESTO_RELACION.INTERRELACIONADO.ToString())));
            }

            //RUTA ALTERNATIVA
            foreach (GridDataItem item in rgAlternativa.MasterTableView.Items)
            {
                pPuestosRel.Add(new XElement("PUESTO_REL",
                    new XAttribute("ID_PUESTO_REL", item.GetDataKeyValue("ID_PUESTO_RELACION").ToString()),
                    new XAttribute("CL_TIPO_RELACION", E_PUESTO_RELACION.RUTAALTERNATIVA.ToString())));
            }

            //RUTA LATERAL
            foreach (GridDataItem item in rgLateral.MasterTableView.Items)
            {
                pPuestosRel.Add(new XElement("PUESTO_REL",
                    new XAttribute("ID_PUESTO_REL", item.GetDataKeyValue("ID_PUESTO_RELACION").ToString()),
                    new XAttribute("CL_TIPO_RELACION", E_PUESTO_RELACION.RUTALATERAL.ToString())));
            }


            //JEFE INMEDIATO
            //RadListBoxItem vItem = lstJefeInmediato.Items[0];

            //if (!String.IsNullOrWhiteSpace(vItem.Value))
            //    pPuestosRel.Add(new XElement("PUESTO_REL",
            //        new XAttribute("ID_PUESTO_REL", vItem.Value),
            //        new XAttribute("CL_TIPO_RELACION", E_PUESTO_RELACION.JEFE.ToString())));


            //COMPETENCIAS ESPECIFICAS
            XElement pCompEsp = new XElement("COMPETENCIAS");

            foreach (RadListBoxItem item in lstCompetenciasEspecificas.Items)
            {
                pCompEsp.Add(new XElement("COMPETENCIA",
                    new XAttribute("ID_COMPETENCIA", item.Value),
                    new XAttribute("CL_TIPO_COMPETENCIA", "PERFIL")));
            }

            //LISTA DE EXPERIENCIA
            XElement pExperiencia = new XElement("EXPERIENCIA");

            foreach (E_EXPERIENCIA item in vListaExperiencia)
            {
                pExperiencia.Add(new XElement("EXP",
                    new XAttribute("ID_AREA_INTERES", item.ID_AREA_INTERES),
                    new XAttribute("NO_TIEMPO", item.NO_TIEMPO),
                    new XAttribute("CL_NIVEL_REQUERIDO", item.CL_TIPO_EXPERIENCIA)));

            }



            XElement pFunGen = new XElement("FUNCIONES");

            if (vIsCopy)
            {
                foreach (E_FUNCION_GENERICA item in vFuncionesGenericas)
                {

                    pFunGen.Add(new XElement("FUNCION",
                        new XAttribute("ID_ITEM", item.ID_ITEM),
                        new XAttribute("ID_PUESTO_FUNCION", 0),
                        new XAttribute("NB_FUNCION", item.NB_FUNCION_GENERICA),
                        new XAttribute("DS_FUNCION", item.NB_FUNCION_GENERICA),
                        EditorContentToXml("DS_DETALLE", item.DS_DETALLE, vNbFirstRadEditorTagName),
                        EditorContentToXml("DS_NOTAS", item.DS_NOTAS, vNbFirstRadEditorTagName)
                    ));
                }
            }
            else
            {

                foreach (E_FUNCION_GENERICA item in vFuncionesGenericas)
                {

                    pFunGen.Add(new XElement("FUNCION",
                        new XAttribute("ID_ITEM", item.ID_ITEM),
                        new XAttribute("ID_PUESTO_FUNCION", item.ID_FUNCION_GENERICA),
                        new XAttribute("NB_FUNCION", item.NB_FUNCION_GENERICA),
                        new XAttribute("DS_FUNCION", item.NB_FUNCION_GENERICA),
                        EditorContentToXml("DS_DETALLE", item.DS_DETALLE, vNbFirstRadEditorTagName),
                        EditorContentToXml("DS_NOTAS", item.DS_NOTAS, vNbFirstRadEditorTagName)
                    ));
                }
            }

            XElement pFunComGen = new XElement("FUNCION_COMPETENCIAS");

            if (vIsCopy)
            {
                foreach (E_FUNCION_COMPETENCIA item in vLstFuncionCompetencia)
                {
                    pFunComGen.Add(new XElement("FUNCION_COMPETENCIA",
                        new XAttribute("ID_ITEM", item.ID_ITEM),
                        new XAttribute("ID_PARENT_ITEM", item.ID_PARENT_ITEM),
                        new XAttribute("ID_COMPETENCIA", item.ID_COMPETENCIA),
                        new XAttribute("NO_NIVEL", item.NO_NIVEL),
                        new XAttribute("CL_TIPO_COMPETENCIA", "FUNCIONES"),
                        new XAttribute("ID_FUNCION", 0),
                        new XAttribute("ID_FUNCION_COMPETENCIA", 0)));
                }
            }
            else
            {
                foreach (E_FUNCION_COMPETENCIA item in vLstFuncionCompetencia)
                {
                    pFunComGen.Add(new XElement("FUNCION_COMPETENCIA",
                        new XAttribute("ID_ITEM", item.ID_ITEM),
                        new XAttribute("ID_PARENT_ITEM", item.ID_PARENT_ITEM),
                        new XAttribute("ID_COMPETENCIA", item.ID_COMPETENCIA),
                        new XAttribute("NO_NIVEL", item.NO_NIVEL),
                        new XAttribute("CL_TIPO_COMPETENCIA", "FUNCIONES"),
                        new XAttribute("ID_FUNCION", item.ID_FUNCION_GENERICA),
                        new XAttribute("ID_FUNCION_COMPETENCIA", item.ID_FUNCION_COMPETENCIA)));
                }
            }

            XElement pFunIndicadores = new XElement("FUNCION_INDICADORES");

            if (vIsCopy)
            {
                foreach (E_FUNCION_INDICADOR item in vLstFuncionCompetenciaIndicador)
                {
                    pFunIndicadores.Add(new XElement("FUNCION_INDICADOR",
                        new XAttribute("ID_ITEM", item.ID_ITEM),
                        new XAttribute("ID_PARENT_ITEM", item.ID_PARENT_ITEM),
                        new XAttribute("NB_INDICADOR", item.NB_INDICADOR_DESEMPENO),
                        new XAttribute("ID_INDICADOR", 0),
                        new XAttribute("ID_FUNCION_INDICADOR", 0),
                        new XAttribute("ID_PUESTO_FUNCION", 0),
                        new XAttribute("ID_PUESTO_COMPETENCIA", 0)));
                }
            }
            else
            {
                foreach (E_FUNCION_INDICADOR item in vLstFuncionCompetenciaIndicador)
                {
                    pFunIndicadores.Add(new XElement("FUNCION_INDICADOR",
                        new XAttribute("ID_ITEM", item.ID_ITEM),
                        new XAttribute("ID_PARENT_ITEM", item.ID_PARENT_ITEM),
                        new XAttribute("NB_INDICADOR", item.NB_INDICADOR_DESEMPENO),
                        new XAttribute("ID_INDICADOR", item.ID_INDICADOR_DESEMPENO),
                        new XAttribute("ID_FUNCION_INDICADOR", item.ID_FUNCION_INDICADOR),
                        new XAttribute("ID_PUESTO_FUNCION", item.ID_FUNCION_GENERICA),
                        new XAttribute("ID_PUESTO_COMPETENCIA", item.ID_FUNCION_COMPETENCIA)));
                }
            }


            XElement pCompetenciasGenericas = new XElement("COMPETENCIAS_GEN");

            foreach (GridDataItem item in dgvCompetencias.Items)
            {
                E_COMPETENCIAS c = (E_COMPETENCIAS)item.DataItem;
                int NO_NIVEL;
                int ID_NIVEL_DESEADO = 0;
                string ID_NIVEL = "ID_NIVEL";

                NO_NIVEL = int.Parse((item.FindControl("rsNivel1") as RadSlider).Value.ToString());
                ID_NIVEL = ID_NIVEL + NO_NIVEL.ToString();
                ID_NIVEL_DESEADO = int.Parse(item.GetDataKeyValue(ID_NIVEL).ToString());

                pCompetenciasGenericas.Add(new XElement("COMPETENCIA_GEN",
                    new XAttribute("ID_COMPETENCIA", item.GetDataKeyValue("ID_COMPETENCIA")),
                    new XAttribute("ID_NIVEL_DESEADO", ID_NIVEL_DESEADO),
                    new XAttribute("CL_TIPO_COMPETENCIA", "COMPETENCIA GENERICA")));
            }

            pPuesto.Add(pDescriptivo);
            pPuesto.Add(pEscolaridad);
            pPuesto.Add(pCompEsp);
            pPuesto.Add(pExperiencia);
            pPuesto.Add(pPuestosRel);
            pPuesto.Add(pFunGen);
            pPuesto.Add(pFunComGen);
            pPuesto.Add(pFunIndicadores);
            pPuesto.Add(pCompetenciasGenericas);
            //txtPruebaXml.Text = pPuesto.ToString();
            return pPuesto.ToString();

        }

        public void GuardarPuesto(bool pFgCerrarVentana)
        {
            if (validarControles())
            {
                string xmlOcupacion;
                if (cmbOcupaciones.SelectedValue != null)
                {
                    xmlOcupacion = ocupaciones(cmbOcupaciones.SelectedValue.ToString());
                }
                else
                {
                    xmlOcupacion = null;
                }
                XElement vXmlCA = generalXmlAdicionales();

                if (vXmlCA != null)
                {
                    string xml = generarXml(vXmlCA);
                    string tran = "";
                    PuestoNegocio neg = new PuestoNegocio();
                    E_PUESTO vPuesto = new E_PUESTO();

                    vPuesto.XML_PUESTO = xml;


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


                    if (vIdDescriptivo == 0)
                    {
                        tran = "I";
                        vPuesto.ID_PUESTO = null;
                    }
                    else
                    {
                        tran = "A";
                        vPuesto.ID_PUESTO = vIdDescriptivo;
                    }

                    E_RESULTADO vResultado = neg.InsertaActualizaPuesto(tran, vPuesto, vLstArchivos, vLstDocumentos, vClUsuario, "Descriptivo", xmlOcupacion);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL) && tran.Equals("I"))
                    {
                        XElement vDatosRespuesta = vResultado.ObtieneDatosRespuesta();
                        XElement vPuestoInsertado = vDatosRespuesta.Element("PUESTO");
                        vIdDescriptivo = UtilXML.ValorAtributo<int>(vPuestoInsertado.Attribute("ID_PUESTO"));
                    }

                    // Se cierra la ventana únicamente cuando el mensaje sea exitoso y se haya dado la instrucción por parámetro de cerrar la venana
                    bool vCerrarVentana = (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL)) && pFgCerrarVentana;
                    string vCallBackFunction = vCerrarVentana ? "OnCloseUpdate" : null;

                    UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: vCallBackFunction);
                }
            }
        }

        private XElement EditorContentToXml(string pNbNodoRaiz, string pDsContenido, string pNbTag)
        {
            return XElement.Parse(EncapsularRadEditorContent(XElement.Parse(String.Format("<{1}>{0}</{1}>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(pDsContenido)), pNbNodoRaiz)), pNbNodoRaiz));
        }

        private string EncapsularRadEditorContent(XElement nodo, string nbNodo)
        {
            //Se comenta el código anterior ya que no es optimo y generaba errores. En casi de requerirse comentar el return nodo.ToString();
            //XElement vNuevoNodo = nodo;
            //if (vNuevoNodo.Elements().Count() == 1)
            //{
            //    return EncapsularRadEditorContent((XElement)vNuevoNodo.FirstNode, nbNodo);
            //}
            //else
            //{
            //    nodo.Name = nbNodo;
            //    return nodo.ToString();
            //}
            return nodo.ToString();
        }

        private void agregarItemLista(RadListBox lista, RadComboBox combo)
        {
            RadListBoxItem item = new RadListBoxItem();
            if (combo.SelectedValue != "")
            {
                item.Value = combo.SelectedValue;
                item.Text = combo.Text;
                int vRepetido = 0;
                if (lista.Items.Count > 0)
                {
                    foreach (RadListBoxItem items in lista.Items)
                    {
                        if (items.Value == item.Value)
                        {
                            vRepetido = 1;
                        }

                    }
                    if (vRepetido != 1)
                    {
                        lista.Items.Add(item);
                    }
                }
                else
                {
                    lista.Items.Add(item);
                }
            }
        }

        private bool validarControles()
        {
            bool vAceptado = true;
            string vMensaje = "";

            if (txtNombreCorto.Text == "" & vAceptado)
            {
                vMensaje = "El campo clave es obligatorio";
                vAceptado = false;
            }

            if (txtDescripcionPuesto.Text == "" & vAceptado)
            {
                vMensaje = "El campo nombre es obligatorio";
                vAceptado = false;
            }

            //if (txtRangoEdadMax.Text == "" & vAceptado)
            //{
            //    vMensaje = "El rango de edad es obligatorio";
            //    vAceptado = false;
            //}

            //if (txtRangoEdadMin.Text == "" & vAceptado)
            //{
            //    vMensaje = "El rango de edad es obligatorio";
            //    vAceptado = false;
            //}
            //if (txtNoPlazas.Text == "" & vAceptado)
            //{
            //    vMensaje = "El número de plazas es  obligatorio";
            //    vAceptado = false;
            //}

            //if (cmbGenero.SelectedIndex == -1 & vAceptado)
            //{
            //    vMensaje = "El género es obligatorio";
            //    vAceptado = false;
            //}

            //if (cmbEdoCivil.SelectedIndex == -1 & vAceptado)
            //{
            //    vMensaje = "El estado civil es obligatorio";
            //    vAceptado = false;
            //}

            //if (lstCompetenciasEspecificas.Items.Count == 0 & vAceptado)
            //{
            //    vMensaje = "Las competencias específicas del perfil de ingreso son obligatorias";
            //    vAceptado = false;
            //}

            //if (lstExperiencia.Items.Count == 0 & vAceptado)
            //{
            //    vMensaje = "La experiencia es obligatoria";
            //    vAceptado = false;
            //}

            //if (radEditorRequerimientos.Content == "" & vAceptado)
            //{
            //    vMensaje = "Los requerimientos son obligatorios";
            //    vAceptado = false;
            //}

            //if ((!btnDirecto.Checked & !btnIndirecto.Checked) & vAceptado)
            //{
            //    vMensaje = "El tipo de puesto es obligatorio";
            //    vAceptado = false;
            //}

            //if (cmbAreas.SelectedIndex == -1 & vAceptado)
            //{
            //    vMensaje = "El área es obligatoria";
            //    vAceptado = false;
            //}

            //if (cmbAdministrativo.SelectedIndex == -1 & vAceptado)
            //{
            //    vMensaje = "El centro administrativo es obligatorio";
            //    vAceptado = false;
            //}

            //if (cmbOperativo.SelectedIndex == -1 & vAceptado)
            //{
            //    vMensaje = "El centro operativo es obligatorio";
            //    vAceptado = false;
            //}

            //if ((!btnLinea.Checked & !btnStaff.Checked) & vAceptado)
            //{
            //    vMensaje = "La posición en el organigrama es obligatoria";
            //    vAceptado = false;
            //}

            //if (radEditorResponsable.Content == "" & vAceptado)
            //{
            //    vMensaje = "El campo \"Responsable de \" es obligatorio";
            //    vAceptado = false;
            //}

            //if (radEditorAutoridad.Content == "" & vAceptado)
            //{
            //    vMensaje = "El campo \"Autoridad\" es obligatorio";
            //    vAceptado = false;
            //}

            if (!vAceptado)
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
            }

            return vAceptado;
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

        private XElement generalXmlAdicionales()
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
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensajes, E_TIPO_RESPUESTA_DB.WARNING);
                return null;
            }
        }

        private void asignarValoresAdicionales(string pXmlvalores)
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
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void SeguridadProcesos()
        {
            btnGuardar.Enabled = ContextoUsuario.oUsuario.TienePermiso("C.G");
            btnGuardarCerrar.Enabled = ContextoUsuario.oUsuario.TienePermiso("C.G");
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            CampoAdicionalNegocio neg = new CampoAdicionalNegocio();
            CrearFormulario(XElement.Parse(neg.obtieneCampoAdicionalXml("M_PUESTO")));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                traerAreas();
          
                vListaExperiencia = new List<E_EXPERIENCIA>();
                rdtFeElabDocumento.SelectedDate = DateTime.Now.Date;
                if (!ContextoApp.FgControlDocumentos)
                {
                    slzOpciones.Visible = false;
                    rpnOpciones.Visible = false;
                }


                //Verificar si se va realizar es una insercion o modificacion
                int idDescriptivo = 0;
                if (int.TryParse((string)Request.QueryString["PuestoId"], out idDescriptivo))
                {
                    vIdDescriptivo = idDescriptivo;
                }

                vIsCopy = (Request.QueryString["pIsCopy"] != null);

                //ObtenerPuestos(E_PUESTO_RELACION.SUBORDINADO.ToString(), cmbPuestosSubordinado);
                //ObtenerPuestos(E_PUESTO_RELACION.INTERRELACIONADO.ToString(), cmbPuestosInterrelacionados);
                vLstInterrelacionados = new List<E_PUESTOS>();
                //ObtenerPuestos(E_PUESTO_RELACION.RUTAALTERNATIVA.ToString(), cmbAlternativa);
                vLstAlternativa = new List<E_PUESTOS>();
                //ObtenerPuestos(E_PUESTO_RELACION.RUTALATERAL.ToString(), cmbLateral);
                vLstLaterales = new List<E_PUESTOS>();

                CargarDatos(vIdDescriptivo);

                ObtenerEstadosCiviles();
                ObtenerGeneros();

                //LLENAR LAS ESCOLARIDADES
                ObtenerEscolaridad(E_CL_TIPO_ESCOLARIDAD.POSTGRADO.ToString(), radcmbPostgrados);
                ObtenerEscolaridad(E_CL_TIPO_ESCOLARIDAD.PROFESIONAL.ToString(), cmbCarreraProf);
                ObtenerEscolaridad(E_CL_TIPO_ESCOLARIDAD.TECNICA.ToString(), cmbCarrTec);

                ObtenerCompetenciasEspecificas();
                ObtenerAreaInteres();
                CargarDocumentos();
                //ObtenerAreas();

                //ObtenerCentroAdmvo();
                //ObtenerCentroOptvo();

                lstExperiencia.DataSource = vListaExperiencia;
                lstExperiencia.DataValueField = "ID_AREA_INTERES";
                lstExperiencia.DataTextField = "NB_AREA_INTERES";
                lstExperiencia.DataBind();

                SeguridadProcesos();

                MnsAutoridadPoliticaIntegral.Visible = ContextoApp.ADM.fgVisibleMensajes;
                MnsAutoridad.Visible = ContextoApp.ADM.fgVisibleMensajes;
                lblPoliticaIntegral.Visible = ContextoApp.ADM.fgVisibleMensajes;
                if (ContextoApp.ADM.fgVisibleMensajes)
                {
                    MnsAutoridadPoliticaIntegral.Text = ContextoApp.ADM.AutoridadPoliticaIntegral.dsMensaje;
                }

            }
            vClRutaArchivosTemporales = Server.MapPath(ContextoApp.ClRutaArchivosTemporales);
            winFuncionesGenericas.VisibleOnPageLoad = false;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }

        protected void dgvCompetencias_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            dgvCompetencias.DataSource = vListaCompetencias.Where(n => n.CL_TIPO_COMPETENCIA != "ESP");
        }

        protected void dgvCompetencias_DataBound(object sender, EventArgs e)
        {
            agregarToolTip();
        }

        protected void btnagregarPostgrado_Click(object sender, EventArgs e)
        {
            agregarItemLista(lstPostgrados, radcmbPostgrados);
        }

        protected void btnAgregarCarreProfe_Click(object sender, EventArgs e)
        {
            agregarItemLista(lstCarreraprof, cmbCarreraProf);
        }

        protected void radbtnAgregarCarreraTec_Click(object sender, EventArgs e)
        {
            agregarItemLista(lstCarreraTec, cmbCarrTec);
        }

        protected void btnCompEsp_Click(object sender, EventArgs e)
        {
            agregarItemLista(lstCompetenciasEspecificas, cmbCompetenciaEspecificas);
        }

        protected void radBtnAgregarExperiencia_Click(object sender, EventArgs e)
        {
            E_EXPERIENCIA ex = new E_EXPERIENCIA();
           
            ex.ID_AREA_INTERES = int.Parse(cmbExperiencias.SelectedValue);
            ex.NB_AREA_INTERES = cmbExperiencias.Text;
            ex.NO_TIEMPO = int.Parse(txtTiempo.Text);
            ex.CL_TIPO_EXPERIENCIA = btnRequerida.Checked ? "Requerida" : "Deseada";
            int vRepetido = 0;
            if (vListaExperiencia.Count > 0)
            {
                foreach (var vExperiancia in vListaExperiencia)
                {
                    if (vExperiancia.ID_AREA_INTERES == ex.ID_AREA_INTERES)
                    {
                        vRepetido = 1;
                    }
                }
                if (vRepetido != 1)
                {
                    vListaExperiencia.Add(ex);
                    lstExperiencia.DataSource = vListaExperiencia;
                    lstExperiencia.DataBind();
                    divContenido.Style.Add("display", "none");
                }
            }
            else
            {
                vListaExperiencia.Add(ex);
                lstExperiencia.DataSource = vListaExperiencia;
                lstExperiencia.DataBind();
                divContenido.Style.Add("display", "none");
            }
        }

        protected void radBtnAgregarPuestoSubordinado_Click(object sender, EventArgs e)
        {
            //agregarItemLista(lstPuestosSubordinado, cmbPuestosSubordinado);
        }

        protected void AgregarInterrelacionado(string pInterrelacionados)
        {
            List<E_PUESTOS> vPuestosSeleccionados = JsonConvert.DeserializeObject<List<E_SELECCION_PUESTOS>>(pInterrelacionados).Select(s => new E_PUESTOS { ID_PUESTO = s.idPuesto, CL_PUESTO = s.clPuesto, NB_PUESTO = s.nbPuesto, ID_PUESTO_RELACION = s.idPuesto }).ToList();
            foreach (E_PUESTOS item in vPuestosSeleccionados)
            {
                if (!vLstInterrelacionados.Exists(e => e.ID_PUESTO == item.ID_PUESTO))
                {
                    vLstInterrelacionados.Add(new E_PUESTOS
                    {
                        ID_PUESTO = item.ID_PUESTO,
                        ID_PUESTO_RELACION = item.ID_PUESTO_RELACION,
                        CL_PUESTO = item.CL_PUESTO,
                        NB_PUESTO = item.NB_PUESTO
                    });
                }
            }

            rgInterrelacionados.Rebind();
        }

        protected void AgregarLateral(string pLateral)
        {
            List<E_PUESTOS> vPuestosSeleccionados = JsonConvert.DeserializeObject<List<E_SELECCION_PUESTOS>>(pLateral).Select(s => new E_PUESTOS { ID_PUESTO = s.idPuesto, CL_PUESTO = s.clPuesto, NB_PUESTO = s.nbPuesto, ID_PUESTO_RELACION = s.idPuesto }).ToList();

            foreach (E_PUESTOS item in vPuestosSeleccionados)
            {
                if (!vLstLaterales.Exists(e => e.ID_PUESTO == item.ID_PUESTO))
                {
                    vLstLaterales.Add(new E_PUESTOS
                    {
                        ID_PUESTO = item.ID_PUESTO,
                        ID_PUESTO_RELACION = item.ID_PUESTO_RELACION,
                        CL_PUESTO = item.CL_PUESTO,
                        NB_PUESTO = item.NB_PUESTO
                    });
                }
            }

            rgLateral.Rebind();
        }

        protected void AgregarAlternativa(string pAlternativa)
        {
            List<E_PUESTOS> vPuestosSeleccionados = JsonConvert.DeserializeObject<List<E_SELECCION_PUESTOS>>(pAlternativa).Select(s => new E_PUESTOS { ID_PUESTO = s.idPuesto, CL_PUESTO = s.clPuesto, NB_PUESTO = s.nbPuesto, ID_PUESTO_RELACION = s.idPuesto }).ToList();
            foreach (E_PUESTOS item in vPuestosSeleccionados)
            {
                if (!vLstAlternativa.Exists(e => e.ID_PUESTO == item.ID_PUESTO))
                {
                    vLstAlternativa.Add(new E_PUESTOS
                    {
                        ID_PUESTO = item.ID_PUESTO,
                        ID_PUESTO_RELACION = item.ID_PUESTO_RELACION,
                        CL_PUESTO = item.CL_PUESTO,
                        NB_PUESTO = item.NB_PUESTO
                    });
                }
            }

            rgAlternativa.Rebind();
        }

        // protected void btnInter_Click(object sender, EventArgs e)
        // {
        // agregarItemLista(lstPuestosInterrelacionados, cmbPuestosInterrelacionados);
        // }

        protected void btnRutaAlter_Click(object sender, EventArgs e)
        {
            //agregarItemLista(lstAlternativa, cmbAlternativa);
        }

        protected void btnLateral_Click(object sender, EventArgs e)
        {
            //  agregarItemLista(lstLateral, cmbLateral);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../IDP/DescriptivoPuestos.aspx");
        }

        protected void btnAgregarFuncionGenerica_Click(object sender, EventArgs e)
        {
            vFuncionGenericaABC = new E_FUNCION_GENERICA();
            vLstFuncionCompetenciaABC = new List<E_FUNCION_COMPETENCIA>();
            vLstFuncionCompetenciaIndicadorABC = new List<E_FUNCION_INDICADOR>();
            CargarFormularioFuncionesGenericas();
        }

        protected void btnEditarFuncionGenerica_Click(object sender, EventArgs e)
        {
            if (grdFuncionesGenericas.SelectedItems.Count > 0)
            {
                foreach (GridDataItem item in grdFuncionesGenericas.SelectedItems)
                {
                    Guid vIdItemFuncionGenerica = new Guid(item.GetDataKeyValue("ID_ITEM").ToString());
                    vFuncionGenericaABC = vFuncionesGenericas.FirstOrDefault(f => f.ID_ITEM.Equals(vIdItemFuncionGenerica));
                    vLstFuncionCompetenciaABC = vLstFuncionCompetencia.Where(w => w.ID_PARENT_ITEM.Equals(vIdItemFuncionGenerica)).ToList();
                    vLstFuncionCompetenciaIndicadorABC = vLstFuncionCompetenciaIndicador.Where(w => vLstFuncionCompetenciaABC.Any(a => a.ID_ITEM.Equals(w.ID_PARENT_ITEM))).ToList();
                    CargarFormularioFuncionesGenericas();
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Selecciona una función genérica.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
            }
        }

        protected void CargarFormularioFuncionesGenericas()
        {
            txtNbFuncion.Text = vFuncionGenericaABC.NB_FUNCION_GENERICA;
            txtDetalleFuncion.Content = vFuncionGenericaABC.DS_DETALLE;
            txtNotasFuncion.Content = vFuncionGenericaABC.DS_NOTAS;
            cmbCompetenciaEspecifica.ClearSelection();
            rtsFuncionGenerica.SelectedIndex = 0;
            rmpFuncionGenerica.SelectedIndex = 0;
            txtIndicadorDesempeno.Text = String.Empty;

            grdFuncionCompetencias.Rebind();
            winFuncionesGenericas.VisibleOnPageLoad = true;
        }

        protected void grdFuncionCompetencias_ItemCommand(object sender, GridCommandEventArgs e)
        {
            winFuncionesGenericas.VisibleOnPageLoad = true;
        }

        protected void grdFuncionCompetencias_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            Guid CustomerID = new Guid(dataItem.GetDataKeyValue("ID_ITEM").ToString());
            e.DetailTableView.DataSource = vLstFuncionCompetenciaIndicadorABC.Where(w => w.ID_PARENT_ITEM.Equals(CustomerID));
        }

        protected void cmbCompetenciaEspecifica_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CargarNivelesCompetencias();
        }

        protected List<E_NIVEL_COMPETENCIA> CrearNivelCompetencia(E_COMPETENCIAS pCompetencia = null)
        {
            if (pCompetencia == null)
                pCompetencia = new E_COMPETENCIAS();

            List<E_NIVEL_COMPETENCIA> vCompetencias = new List<E_NIVEL_COMPETENCIA>();
            vCompetencias.Add(new E_NIVEL_COMPETENCIA() { NO_VALOR = 0, NB_NIVEL = "0 - No lo necesita", DS_NIVEL = pCompetencia.DS_COMENTARIOS_NIVEL0 });
            vCompetencias.Add(new E_NIVEL_COMPETENCIA() { NO_VALOR = 1, NB_NIVEL = "1 - Lo necesita poco", DS_NIVEL = pCompetencia.DS_COMENTARIOS_NIVEL1 });
            vCompetencias.Add(new E_NIVEL_COMPETENCIA() { NO_VALOR = 2, NB_NIVEL = "2 - Lo necesita medio bajo", DS_NIVEL = pCompetencia.DS_COMENTARIOS_NIVEL2 });
            vCompetencias.Add(new E_NIVEL_COMPETENCIA() { NO_VALOR = 3, NB_NIVEL = "3 - Lo necesita medio alto", DS_NIVEL = pCompetencia.DS_COMENTARIOS_NIVEL3 });
            vCompetencias.Add(new E_NIVEL_COMPETENCIA() { NO_VALOR = 4, NB_NIVEL = "4 - Lo necesita de manera importante", DS_NIVEL = pCompetencia.DS_COMENTARIOS_NIVEL4 });
            vCompetencias.Add(new E_NIVEL_COMPETENCIA() { NO_VALOR = 5, NB_NIVEL = "5 - Lo necesita de manera imprescindible", DS_NIVEL = pCompetencia.DS_COMENTARIOS_NIVEL5 });
            return vCompetencias;
        }

        protected void CargarNivelesCompetencias()
        {
            List<E_NIVEL_COMPETENCIA> vLstNivelesCompetencia = new List<E_NIVEL_COMPETENCIA>();
            int vIdCompenteciaSeleccionada = 0;
            if (int.TryParse(cmbCompetenciaEspecifica.SelectedValue, out vIdCompenteciaSeleccionada))
            {
                E_COMPETENCIAS competencia = vListaCompetencias.FirstOrDefault(f => f.ID_COMPETENCIA.Equals(vIdCompenteciaSeleccionada));
                if (competencia == null)
                    competencia = new E_COMPETENCIAS();

                vLstNivelesCompetencia.AddRange(CrearNivelCompetencia(competencia));

                grdNivelCompetenciaEspecifica.DataSource = vLstNivelesCompetencia;
            }
            grdNivelCompetenciaEspecifica.DataBind();
            btnAgregarCompetenciaEspecifica.Enabled = true;
        }

        protected void grdFuncionCompetencias_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdFuncionCompetencias.DataSource = vLstFuncionCompetenciaABC;
        }

        protected void btnEliminarCompetencia_Click(object sender, EventArgs e)
        {
            bool vFgRebindGrid = false;
            if (grdFuncionCompetencias.SelectedItems.Count > 0)
            {
                foreach (GridDataItem item in grdFuncionCompetencias.SelectedItems)
                {
                    vLstFuncionCompetenciaIndicadorABC.RemoveAll(r => r.ID_PARENT_ITEM.Equals(new Guid(item.GetDataKeyValue("ID_ITEM").ToString())));
                    vLstFuncionCompetenciaABC.RemoveAll(r => r.ID_ITEM.Equals(new Guid(item.GetDataKeyValue("ID_ITEM").ToString())));
                    vFgRebindGrid = true | vFgRebindGrid;
                }

                if (vFgRebindGrid)
                    grdFuncionCompetencias.Rebind();
            }
            else
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Selecciona una competencia.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
        }

        protected void btnAgregarCompetenciaEspecifica_Click(object sender, EventArgs e)
        {
            int vNoNivel = 0;
            bool vFgRebindGrid = false;
            foreach (GridDataItem item in grdNivelCompetenciaEspecifica.SelectedItems)
            {
                if (int.TryParse(item.GetDataKeyValue("NO_VALOR").ToString(), out vNoNivel))
                {
                    E_FUNCION_COMPETENCIA vCompetencia = vLstFuncionCompetenciaABC.FirstOrDefault(f => f.ID_ITEM == (vIdEditingItem ?? Guid.NewGuid()));

                    if (vCompetencia == null)
                    {
                        vLstFuncionCompetenciaABC.Add(new E_FUNCION_COMPETENCIA()
                        {
                            ID_PARENT_ITEM = vFuncionGenericaABC.ID_ITEM,
                            NB_COMPETENCIA = cmbCompetenciaEspecifica.SelectedItem.Text,
                            ID_COMPETENCIA = int.Parse(cmbCompetenciaEspecifica.SelectedItem.Value),
                            NO_NIVEL = vNoNivel,
                            NB_NIVEL = item["NB_NIVEL"].Text
                        });
                    }
                    else
                    {
                        vCompetencia.NB_COMPETENCIA = cmbCompetenciaEspecifica.SelectedItem.Text;
                        vCompetencia.ID_COMPETENCIA = int.Parse(cmbCompetenciaEspecifica.SelectedItem.Value);
                        vCompetencia.NO_NIVEL = vNoNivel;
                        vCompetencia.NB_NIVEL = item["NB_NIVEL"].Text;
                    }
                    vFgRebindGrid = true | vFgRebindGrid;
                }
            }

            if (vFgRebindGrid)
                grdFuncionCompetencias.Rebind();
        }

        protected void btnAgregarIndicadorDesempeno_Click(object sender, EventArgs e)
        {
            bool vFgRebindGrid = false;
            foreach (GridDataItem item in grdFuncionCompetencias.SelectedItems)
            {
                E_FUNCION_INDICADOR vIndicador = vLstFuncionCompetenciaIndicadorABC.FirstOrDefault(f => f.ID_ITEM == (vIdEditingItem ?? Guid.NewGuid()));

                Guid vIdCompetencia = new Guid(item.GetDataKeyValue("ID_ITEM").ToString());

                if (vIndicador == null)
                {
                    vLstFuncionCompetenciaIndicadorABC.Add(new E_FUNCION_INDICADOR()
                    {
                        ID_PARENT_ITEM = vIdCompetencia,
                        NB_INDICADOR_DESEMPENO = txtIndicadorDesempeno.Text
                    });
                }
                else
                {
                    vIdCompetencia = vIndicador.ID_PARENT_ITEM;
                    vIndicador.NB_INDICADOR_DESEMPENO = txtIndicadorDesempeno.Text;
                }

                CrearDsIndicadores(vLstFuncionCompetenciaABC.FirstOrDefault(f => f.ID_ITEM.Equals(vIdCompetencia)), vLstFuncionCompetenciaIndicadorABC);
                vFgRebindGrid = true | vFgRebindGrid;
            }

            if (vFgRebindGrid)
                grdFuncionCompetencias.Rebind();
        }

        protected void btnEliminarIndicador_Click(object sender, EventArgs e)
        {
            bool vFgRebindGrid = false;
            if (grdFuncionCompetencias.SelectedItems.Count > 0)
            {
                foreach (GridDataItem item in grdFuncionCompetencias.SelectedItems)
                {
                    Guid vIdIndicador = new Guid(item.GetDataKeyValue("ID_ITEM").ToString());
                    Guid vIdCompetencia = (vLstFuncionCompetenciaIndicadorABC.FirstOrDefault(f => f.ID_ITEM.Equals(vIdIndicador)) ?? new E_FUNCION_INDICADOR()).ID_PARENT_ITEM;
                    vLstFuncionCompetenciaIndicadorABC.RemoveAll(r => r.ID_ITEM.Equals(vIdIndicador));
                    CrearDsIndicadores(vLstFuncionCompetenciaABC.FirstOrDefault(f => f.ID_ITEM.Equals(vIdCompetencia)), vLstFuncionCompetenciaIndicadorABC);
                    vFgRebindGrid = true | vFgRebindGrid;
                }

                if (vFgRebindGrid)
                    grdFuncionCompetencias.Rebind();
            }
            else
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Selecciona un indicador.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
        }

        protected void CrearDsIndicadores(E_FUNCION_COMPETENCIA pFuncionCompetencia, List<E_FUNCION_INDICADOR> pLstIndicadores)
        {
            if (pFuncionCompetencia != null)
                pFuncionCompetencia.DS_INDICADORES = new XElement("ul", pLstIndicadores.Where(w => w.ID_PARENT_ITEM.Equals(pFuncionCompetencia.ID_ITEM)).Select(s => new XElement("li", s.NB_INDICADOR_DESEMPENO))).ToString();
        }

        protected void btnGuardarFuncionGenerica_Click(object sender, EventArgs e)
        {
            vFuncionGenericaABC.NB_FUNCION_GENERICA = txtNbFuncion.Text;
            vFuncionGenericaABC.DS_DETALLE = txtDetalleFuncion.Content;
            vFuncionGenericaABC.DS_NOTAS = txtNotasFuncion.Content;

            if (vFuncionesGenericas == null)
                vFuncionesGenericas = new List<E_FUNCION_GENERICA>();

            if (vLstFuncionCompetencia == null)
                vLstFuncionCompetencia = new List<E_FUNCION_COMPETENCIA>();

            if (vLstFuncionCompetenciaIndicador == null)
                vLstFuncionCompetenciaIndicador = new List<E_FUNCION_INDICADOR>();

            E_FUNCION_GENERICA vFuncionGenerica = vFuncionesGenericas.FirstOrDefault(f => f.ID_ITEM.Equals(vFuncionGenericaABC.ID_ITEM));

            if (vFuncionGenerica == null)
                vFuncionesGenericas.Add(vFuncionGenericaABC);
            else
            {
                vFuncionGenerica.NB_FUNCION_GENERICA = vFuncionGenericaABC.NB_FUNCION_GENERICA;
                vFuncionGenerica.DS_DETALLE = vFuncionGenericaABC.DS_DETALLE;
                vFuncionGenerica.DS_NOTAS = vFuncionGenericaABC.DS_NOTAS;
            }


            foreach (E_FUNCION_COMPETENCIA vCompetenciaABC in vLstFuncionCompetenciaABC)
            {
                E_FUNCION_COMPETENCIA vCompetencia = vLstFuncionCompetencia.FirstOrDefault(f => f.ID_ITEM.Equals(vCompetenciaABC.ID_ITEM));

                if (vCompetencia == null)
                    vLstFuncionCompetencia.Add(vCompetenciaABC);
                else
                {
                    vCompetencia.NB_COMPETENCIA = vCompetenciaABC.NB_COMPETENCIA;
                    vCompetencia.NO_NIVEL = vCompetenciaABC.NO_NIVEL;
                    vCompetencia.NB_NIVEL = vCompetenciaABC.NB_NIVEL;
                    vCompetencia.DS_INDICADORES = vCompetenciaABC.DS_INDICADORES;
                }
            }

            foreach (E_FUNCION_INDICADOR vIndicadorABC in vLstFuncionCompetenciaIndicadorABC)
            {
                E_FUNCION_INDICADOR vIndicador = vLstFuncionCompetenciaIndicador.FirstOrDefault(f => f.ID_ITEM.Equals(vIndicadorABC.ID_ITEM));

                if (vIndicador == null)
                    vLstFuncionCompetenciaIndicador.Add(vIndicadorABC);
                else
                {
                    vIndicador.NB_INDICADOR_DESEMPENO = vIndicadorABC.NB_INDICADOR_DESEMPENO;
                }
            }

            List<E_FUNCION_INDICADOR> vLstIndicadoresToRemove = new List<E_FUNCION_INDICADOR>();
            List<E_FUNCION_COMPETENCIA> vLstCompetenciasToRemove = new List<E_FUNCION_COMPETENCIA>();
            foreach (E_FUNCION_COMPETENCIA competencia in vLstFuncionCompetencia.Where(w => w.ID_PARENT_ITEM.Equals(vFuncionGenericaABC.ID_ITEM)))
            {
                if (!vLstFuncionCompetenciaABC.Any(a => a.ID_ITEM.Equals(competencia.ID_ITEM)))
                    vLstCompetenciasToRemove.Add(competencia);

                foreach (E_FUNCION_INDICADOR indicador in vLstFuncionCompetenciaIndicador.Where(w => w.ID_PARENT_ITEM.Equals(competencia.ID_ITEM)))
                    if (!vLstFuncionCompetenciaIndicadorABC.Any(a => a.ID_ITEM.Equals(indicador.ID_ITEM)))
                        vLstIndicadoresToRemove.Add(indicador);
            }

            vLstFuncionCompetenciaIndicador.RemoveAll(r => vLstIndicadoresToRemove.Any(a => a.ID_ITEM.Equals(r.ID_ITEM)));
            vLstFuncionCompetencia.RemoveAll(r => vLstCompetenciasToRemove.Any(a => a.ID_ITEM.Equals(r.ID_ITEM)));

            grdFuncionesGenericas.Rebind();
        }

        protected void grdFuncionesGenericas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdFuncionesGenericas.DataSource = vFuncionesGenericas;
        }

        protected void grdFuncionesGenericas_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                RadGrid grd = (item["Funciones"].FindControl("grdCompetencias") as RadGrid);
                grd.DataSource = vLstFuncionCompetencia.Where(w => w.ID_PARENT_ITEM.Equals(new Guid(item.GetDataKeyValue("ID_ITEM").ToString())));
                grd.DataBind();
            }
        }

        protected void btnEliminarFuncionGenerica_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grdFuncionesGenericas.SelectedItems)
            {
                Guid vIdItemFuncionGenerica = new Guid(item.GetDataKeyValue("ID_ITEM").ToString());
                IEnumerable<E_FUNCION_COMPETENCIA> vLstCompentenciasToDelete = vLstFuncionCompetencia.Where(w => w.ID_PARENT_ITEM.Equals(vIdItemFuncionGenerica));

                vLstFuncionCompetenciaIndicador.RemoveAll(r => vLstCompentenciasToDelete.Any(a => a.ID_ITEM.Equals(r.ID_PARENT_ITEM)));
                vLstFuncionCompetencia.RemoveAll(r => r.ID_PARENT_ITEM.Equals(vIdItemFuncionGenerica));
                vFuncionesGenericas.RemoveAll(r => r.ID_ITEM.Equals(vIdItemFuncionGenerica));

                grdFuncionesGenericas.Rebind();
            }
        }

        protected void btnEditarIndicador_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grdFuncionCompetencias.SelectedItems)
            {
                vIdEditingItem = Guid.Parse(item.GetDataKeyValue("ID_ITEM").ToString());
                E_FUNCION_INDICADOR indicador = vLstFuncionCompetenciaIndicadorABC.FirstOrDefault(f => f.ID_ITEM.Equals(vIdEditingItem));
                if (indicador == null)
                    indicador = new E_FUNCION_INDICADOR();

                txtIndicadorDesempeno.Text = indicador.NB_INDICADOR_DESEMPENO;
            }
        }

        protected void btnEditarCompetencia_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grdFuncionCompetencias.SelectedItems)
            {
                vIdEditingItem = Guid.Parse(item.GetDataKeyValue("ID_ITEM").ToString());
                E_FUNCION_COMPETENCIA competencia = vLstFuncionCompetenciaABC.FirstOrDefault(f => f.ID_ITEM.Equals(vIdEditingItem));
                if (competencia == null)
                    competencia = new E_FUNCION_COMPETENCIA();

                cmbCompetenciaEspecifica.SelectedValue = competencia.ID_COMPETENCIA.ToString();
                CargarNivelesCompetencias();
            }
        }

        protected void grdNivelCompetenciaEspecifica_PreRender(object sender, EventArgs e)
        {
            if (vLstFuncionCompetenciaABC != null)
            {
                E_FUNCION_COMPETENCIA competencia = vLstFuncionCompetenciaABC.FirstOrDefault(f => f.ID_ITEM.Equals(vIdEditingItem));
                if (competencia != null)
                {
                    GridDataItem selectItem = grdNivelCompetenciaEspecifica.MasterTableView.FindItemByKeyValue("NO_VALOR", competencia.NO_NIVEL);
                    if (selectItem != null)
                        selectItem.Selected = true;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarPuesto(false);
        }

        protected void btnGuardarCerrar_Click(object sender, EventArgs e)
        {
            GuardarPuesto(true);
        }

        //000000000000000000000000000000000000000000 CATALOGO DE OCUPACIONES NACIONALES 0000000000000000000000000000000000000

        public void traerAreas()
        {
            listaAreasO = negocio.Obtener_AREA_OCUPACION();
            cmbAreaO.DataSource = listaAreasO;
            cmbAreaO.DataTextField = "NB_AREA";
            cmbAreaO.DataValueField = "CL_AREA";
            cmbAreaO.DataBind();
        }

        public void traerSubAreas(string clArea)
        {
            cmbSubarea.Text = string.Empty;
            listaSubareas = negocio.Obtener_SUBAREA_OCUPACION(PIN_CL_AREA: clArea);
            cmbSubarea.DataSource = listaSubareas;
            cmbSubarea.DataTextField = "NB_SUBAREA";
            cmbSubarea.DataValueField = "CL_SUBAREA";
            cmbSubarea.DataBind();
        }

        public void traerModulos(string clSubarea)
        {
            cmbModulo.Text = string.Empty;
            listaModulos = negocio.Obtener_MODULO_OCUPACION(PIN_CL_SUBAREA: clSubarea);
            cmbModulo.DataSource = listaModulos;
            cmbModulo.DataTextField = "NB_MODULO";
            cmbModulo.DataValueField = "CL_MODULO";
            cmbModulo.DataBind();
        }

        public void traerOcupaciones(string clModulo)
        {
            cmbOcupaciones.Text = string.Empty;
            listaOcupaciones = negocio.Obtener_OCUPACIONES(PIN_CL_MODULO: clModulo);
            cmbOcupaciones.DataSource = listaOcupaciones;
            cmbOcupaciones.DataTextField = "NB_OCUPACION";
            cmbOcupaciones.DataValueField = "CL_OCUPACION";
            cmbOcupaciones.DataBind();
        }

        protected void cmbAreaO_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmbSubarea.DataSource = new string[] { };
            cmbSubarea.DataBind();
            traerSubAreas(e.Value);
        }

        protected void cmbSubarea_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmbModulo.DataSource = new string[] { };
            cmbModulo.DataBind();
            traerModulos(e.Value);
        }

        protected void cmbModulo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmbOcupaciones.DataSource = new string[] { };
            cmbOcupaciones.DataBind();
            traerOcupaciones(e.Value);
        }

        protected void cmbOcupaciones_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var ocupacion = negocio.Obtener_OCUPACIONES(PIN_CL_OCUPACION: e.Value.ToString()).FirstOrDefault();
            lblClOcupación.Text = ocupacion.CL_OCUPACION;
            lblOcupacionSeleccionada.Text = ocupacion.NB_OCUPACION;
        }

        protected void btnEliminarOcupacionPuesto_Click(object sender, EventArgs e)
        {
            DescriptivoNegocio nDescriptivo = new DescriptivoNegocio();
            E_DESCRIPTIVO vDescriptivo = nDescriptivo.ObtieneDescriptivo(vIdDescriptivo);

            if (vDescriptivo.LST_OCUPACION_PUESTO != null)
            {
                if (vDescriptivo.LST_OCUPACION_PUESTO.ID_OCUPACION != 0)
                {
                    E_RESULTADO vResultado = negocio.Elimina_K_PUESTO_OCUPACION(vDescriptivo.LST_OCUPACION_PUESTO.ID_OCUPACION, vIdDescriptivo, vClUsuario, vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, null);
                    if (vMensaje == "Proceso exitoso" || vMensaje == "Successful Process")
                    {
                        vDescriptivo.LST_OCUPACION_PUESTO.ID_OCUPACION = 0;
                        vDescriptivo.LST_OCUPACION_PUESTO.CL_OCUPACION = "";
                        vDescriptivo.LST_OCUPACION_PUESTO.CL_MODULO = "";
                        vDescriptivo.LST_OCUPACION_PUESTO.CL_SUBAREA = "";
                        vDescriptivo.LST_OCUPACION_PUESTO.CL_AREA = "";
                        cmbAreaO.ClearSelection();
                        cmbAreaO.Text = string.Empty;
                        cmbSubarea.ClearSelection();
                        cmbSubarea.Text = string.Empty;
                        cmbModulo.ClearSelection();
                        cmbModulo.Text = string.Empty;
                        cmbOcupaciones.ClearSelection();
                        cmbOcupaciones.Text = string.Empty;

                        lblClOcupación.Text = "";
                        lblOcupacionSeleccionada.Text = "";
                    }
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "No hay ocupación en el puesto", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, null);
            }
        }

        protected void lstExperiencia_Deleted(object sender, RadListBoxEventArgs e)
        {
            int Id = int.Parse(e.Items[0].Value);
            E_EXPERIENCIA ItemElim = new E_EXPERIENCIA();

            foreach (E_EXPERIENCIA item in vListaExperiencia)
            {
                if (item.ID_AREA_INTERES == Id)
                {
                    ItemElim = item;
                }
            }
            if (ItemElim != null)
            {
                vListaExperiencia.Remove(ItemElim);
            }
        }

        protected void grdCompetencias_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem gridItem = e.Item as GridDataItem;

                int vIdCompetencia = int.Parse(gridItem.GetDataKeyValue("ID_COMPETENCIA").ToString());
                string vDsCompetencia = vLstFuncionCompetencia.Where(t => t.ID_COMPETENCIA == vIdCompetencia).FirstOrDefault().DS_COMPETENCIA;
                //string vClasificacion = vListaDetallada.Where(t => t.ID_COMPETENCIA == vIdCompetencia).FirstOrDefault().NB_CLASIFICACION_COMPETENCIA;

                gridItem["NB_COMPETENCIA"].ToolTip = vDsCompetencia;
                //gridItem["CL_COLOR"].ToolTip = vClasificacion;
            }
        }

        protected void rgJefesInmediatos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DescriptivoNegocio vNeg = new DescriptivoNegocio();
            rgJefesInmediatos.DataSource = vNeg.ObtenerJefesDescriptivo(vIdDescriptivo).ToList();
        }

        protected void rgSubordinados_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DescriptivoNegocio vNeg = new DescriptivoNegocio();
            rgSubordinados.DataSource = vNeg.ObtenerSubordinadosDescriptivo(vIdDescriptivo).ToList();
        }

        protected void rgInterrelacionados_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgInterrelacionados.DataSource = vLstInterrelacionados;
        }

        protected void rgLateral_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //List<E_JEFE_DIRECTO> vLstLateral = new List<E_JEFE_DIRECTO>();
            //vLstLateral.Add(new E_JEFE_DIRECTO() { ID_PUESTO = 1, CL_PUESTO = "CH000", NB_PUESTO = "Jefe de capital humano", NUM_PLAZAS = 4 });
            //vLstLateral.Add(new E_JEFE_DIRECTO() { ID_PUESTO = 2, CL_PUESTO = "D0000", NB_PUESTO = "Gerente de capital humano", NUM_PLAZAS = 2 });
            //vLstLateral.Add(new E_JEFE_DIRECTO() { ID_PUESTO = 3, CL_PUESTO = "DO001", NB_PUESTO = "Director de capital humano", NUM_PLAZAS = 1 });

            rgLateral.DataSource = vLstLaterales;
        }

        protected void rgAlternativa_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgAlternativa.DataSource = vLstAlternativa;
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            E_SELECTOR vLstDatos = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
            {
                vLstDatos = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

                if (vLstDatos.clTipo == "INTERRELACIONADO")
                {
                    AgregarInterrelacionado(vLstDatos.oSeleccion.ToString());
                }

                if (vLstDatos.clTipo == "LATERAL")
                {
                    AgregarLateral(vLstDatos.oSeleccion.ToString());
                }

                if (vLstDatos.clTipo == "ALTERNATIVO")
                {
                    AgregarAlternativa(vLstDatos.oSeleccion.ToString());
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in rgInterrelacionados.SelectedItems)
            {
                int vIdPuesto = int.Parse(item.GetDataKeyValue("ID_PUESTO").ToString());
                E_PUESTOS vItem = vLstInterrelacionados.Where(w => w.ID_PUESTO == vIdPuesto).FirstOrDefault();

                if (vItem != null)
                {
                    vLstInterrelacionados.Remove(vItem);
                    rgInterrelacionados.Rebind();
                }

            }
        }

        protected void btnEliminarLateral_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in rgLateral.SelectedItems)
            {
                int vIdPuesto = int.Parse(item.GetDataKeyValue("ID_PUESTO").ToString());
                E_PUESTOS vItem = vLstLaterales.Where(w => w.ID_PUESTO == vIdPuesto).FirstOrDefault();

                if (vItem != null)
                {
                    vLstLaterales.Remove(vItem);
                    rgLateral.Rebind();
                }

            }
        }

        protected void btnEliminarAlternativa_OnClick(object sender, EventArgs e)
        {
            foreach (GridDataItem item in rgAlternativa.SelectedItems)
            {
                int vIdPuesto = int.Parse(item.GetDataKeyValue("ID_PUESTO").ToString());
                E_PUESTOS vItem = vLstAlternativa.Where(w => w.ID_PUESTO == vIdPuesto).FirstOrDefault();

                if (vItem != null)
                {
                    vLstAlternativa.Remove(vItem);
                    rgAlternativa.Rebind();
                }

            }
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
