using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaDescriptivoPuesto : System.Web.UI.Page
    {
        #region Varianbles

        private string vClUsuario = string.Empty;
        private string vNbPrograma = string.Empty;

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

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
        private int? vIdDescriptivo
        {
            get { return (int?)ViewState["vs_vIdDescriptivo"]; }
            set { ViewState["vs_vIdDescriptivo"] = value; }
        }

        //GRID DE FUNCIONES GENERICAS
        private List<E_FUNCION_GENERICA> vListaFuncionesGenericas
        {
            get { return (List<E_FUNCION_GENERICA>)ViewState["vs_vListaFuncionesGenericas"]; }
            set { ViewState["vs_vListaFuncionesGenericas"] = value; }
        }
        private List<E_FUNCION_COMPETENCIA> vListaFuncionCompetencias
        {
            get { return (List<E_FUNCION_COMPETENCIA>)ViewState["vs_vListaFuncionCompetencias"]; }
            set { ViewState["vs_vListaFuncionCompetencias"] = value; }
        }
        private List<E_FUNCION_INDICADOR> vListaFuncionIndicadores
        {
            get { return (List<E_FUNCION_INDICADOR>)ViewState["vs_vListaFuncionIndicadores"]; }
            set { ViewState["vs_vListaFuncionIndicadores"] = value; }
        }

        private int vIdFunGen
        {
            get { return (int)ViewState["vs_idFunGen"]; }
            set { ViewState["vs_idFunGen"] = value; }
        }
        private int vIdCompEsp
        {
            get { return (int)ViewState["vs_idCompEsp"]; }
            set { ViewState["vs_idCompEsp"] = value; }
        }
        private int vIdIndi
        {
            get { return (int)ViewState["vs_idIndi"]; }
            set { ViewState["vs_idIndi"] = value; }
        }
        private string vXmlAdicionales
        {
            get { return (string)ViewState["vs_xmlAdicionales"]; }
            set { ViewState["vs_xmlAdicionales"] = value; }
        }
        private bool vIsCopy = false;
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

            //CrearFormulario(XElement.Parse(vXmlAdicionales));
            asignarValoresAdicionales(vDescriptivo.XML_CAMPOS_ADICIONALES);

            if (pIdDescriptivo != null)
            {
                txtNombreCorto.Text = vDescriptivo.CL_PUESTO;
                txtDescripcionPuesto.Text = vDescriptivo.NB_PUESTO;
                txtRangoEdadMin.Value = vDescriptivo.NO_EDAD_MINIMA;
                txtRangoEdadMax.Value = vDescriptivo.NO_EDAD_MAXIMA;

                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTO_ESCOLARIDAD).Elements("PUESTO_ESCOLARIDAD"))
                {
                    if (item.Attribute("CL_TIPO_ESCOLARIDAD").Value == E_CL_TIPO_ESCOLARIDAD.POSGRADO.ToString())
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
                    if (item.Attribute("CL_TIPO_ESCOLARIDAD").Value == E_CL_TIPO_ESCOLARIDAD.BACHILLERATO.ToString())
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

                radEditorRequerimientos.Content = vDescriptivo.XML_REQUERIMIENTOS;
                radEditorObservaciones.Content = vDescriptivo.XML_OBSERVACIONES;

                if (vDescriptivo.CL_TIPO_PUESTO == "DIRECTO")
                {
                    btnDirecto.Checked = true;
                }
                else
                {
                    btnIndirecto.Checked = true;
                }


                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTOS_RELACIONADOS).Elements("PUESTO_RELACIONADO"))
                {
                    if (item.Attribute("CL_TIPO_RELACION").Value == E_PUESTO_RELACION.JEFE.ToString())
                    {
                        lstJefeInmediato.Items.Clear();

                        RadListBoxItem i = new RadListBoxItem();

                        i.Text = item.Attribute("NB_PUESTO").Value;
                        i.Value = item.Attribute("ID_PUESTO_RELACIONADO").Value;
                        lstJefeInmediato.Items.Add(i);
                    }
                }


                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTOS_RELACIONADOS).Elements("PUESTO_RELACIONADO"))
                {
                    if (item.Attribute("CL_TIPO_RELACION").Value == E_PUESTO_RELACION.SUBORDINADO.ToString())
                    {
                        RadListBoxItem i = new RadListBoxItem();

                        i.Text = item.Attribute("NB_PUESTO").Value;
                        i.Value = item.Attribute("ID_PUESTO_RELACIONADO").Value;
                        lstPuestosSubordinado.Items.Add(i);
                    }

                }

                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTOS_RELACIONADOS).Elements("PUESTO_RELACIONADO"))
                {
                    if (item.Attribute("CL_TIPO_RELACION").Value == E_PUESTO_RELACION.INTERRELACIONADO.ToString())
                    {
                        RadListBoxItem i = new RadListBoxItem();

                        i.Text = item.Attribute("NB_PUESTO").Value;
                        i.Value = item.Attribute("ID_PUESTO_RELACIONADO").Value;
                        lstPuestosInterrelacionados.Items.Add(i);
                    }
                }

                if (vDescriptivo.CL_POSICION_ORGANIGRAMA == "LINEA")
                {
                    btnLinea.Checked = true;
                }
                else
                {
                    btnStaff.Checked = true;
                }

                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTOS_RELACIONADOS).Elements("PUESTO_RELACIONADO"))
                {
                    if (item.Attribute("CL_TIPO_RELACION").Value == E_PUESTO_RELACION.RUTAALTERNATIVA.ToString())
                    {
                        RadListBoxItem i = new RadListBoxItem();

                        i.Text = item.Attribute("NB_PUESTO").Value;
                        i.Value = item.Attribute("ID_PUESTO_RELACIONADO").Value;
                        lstAlternativa.Items.Add(i);
                    }
                }

                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTOS_RELACIONADOS).Elements("PUESTO_RELACIONADO"))
                {
                    if (item.Attribute("CL_TIPO_RELACION").Value == E_PUESTO_RELACION.RUTALATERAL.ToString())
                    {
                        RadListBoxItem i = new RadListBoxItem();

                        i.Text = item.Attribute("NB_PUESTO").Value;
                        i.Value = item.Attribute("ID_PUESTO_RELACIONADO").Value;
                        lstLateral.Items.Add(i);
                    }
                }

                radEditorResponsable.Content = vDescriptivo.XML_RESPONSABILIDAD;
                radEditorAutoridad.Content = vDescriptivo.XML_AUTORIDAD;

                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTO_FUNCION).Elements("PUESTO_FUNCION"))
                {
                    E_FUNCION_GENERICA fg = new E_FUNCION_GENERICA();

                    fg.ID_FUNCION_GENERICA = int.Parse(item.Attribute("ID_PUESTO_FUNCION").Value);
                    fg.NB_FUNCION_GENERICA = item.Attribute("NB_PUESTO_FUNCION").Value;
                    fg.DS_DETALLE = item.Attribute("XML_DETALLE").Value;
                    fg.DS_NOTAS = item.Attribute("XML_NOTAS").Value;

                    vListaFuncionesGenericas.Add(fg);
                }

                foreach (E_COMPETENCIAS item in vListaCompetencias.Where(n => n.ID_PUESTO_FUNCION != 0))
                {
                    E_FUNCION_COMPETENCIA fc = new E_FUNCION_COMPETENCIA();

                    fc.ID_COMPETENCIA = item.ID_COMPETENCIA.Value;
                    fc.ID_FUNCION_GENERICA = item.ID_PUESTO_FUNCION;
                    fc.ID_FUNCION_COMPETENCIA = item.ID_PUESTO_COMPETENCIA;
                    fc.NB_COMPETENCIA = item.NB_COMPETENCIA;
                    fc.NO_NIVEL = item.NO_VALOR_NIVEL;

                    vListaFuncionCompetencias.Add(fc);
                }

                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTO_INDICADOR).Elements("PUESTO_INDICADOR"))
                {
                    E_FUNCION_INDICADOR fi = new E_FUNCION_INDICADOR();

                    fi.ID_FUNCION_COMPETENCIA = int.Parse(item.Attribute("ID_PUESTO_COMPETENCIA").Value);
                    fi.ID_FUNCION_GENERICA = int.Parse(item.Attribute("ID_PUESTO_FUNCION").Value);
                    fi.ID_INDICADOR_DESEMPENO = int.Parse(item.Attribute("ID_INDICADOR").Value);
                    fi.NB_INDICADOR_DESEMPENO = item.Attribute("NB_INDICADOR").Value;

                    vListaFuncionIndicadores.Add(fi);
                }

                cmbTipoPrestaciones.SelectedValue = vDescriptivo.CL_TIPO_PRESTACIONES;
                radEditorPrestaciones.Content = vDescriptivo.XML_PRESTACIONES;

                if (ContextoApp.FgControlDocumentos)
                {
                    txtClaveDocumento.Text = vDescriptivo.CL_DOCUMENTO;
                    txtVersionDocumento.Text = vDescriptivo.CL_VERSION;
                    if (vDescriptivo.FE_ELABORACION.HasValue)
                    {
                        txtFeElabDocumento.Text = vDescriptivo.FE_ELABORACION.Value.ToShortDateString();
                    }
                    txtElaboroDocumento.Text = vDescriptivo.NB_ELABORO;

                    if (vDescriptivo.FE_REVISION.HasValue)
                    {
                        txtFeRevDocumento.Text = vDescriptivo.FE_REVISION.Value.ToShortDateString();
                    }
                    txtRevisoDocumento.Text = vDescriptivo.NB_REVISO;

                    if (vDescriptivo.FE_AUTORIZACION.HasValue)
                    {
                        txtFeAutorizoDocumento.Text = vDescriptivo.FE_AUTORIZACION.Value.ToShortDateString();
                    }
                    txtAutorizoDocumento.Text = vDescriptivo.NB_AUTORIZO;

                    txtControlCambios.Text = vDescriptivo.DS_CONTROL_CAMBIOS;
                }

                if (vIsCopy)
                {
                    vIdDescriptivo = null;
                }
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
        }

        public void ObtenerEstadosCiviles()
        {
            cmbEdoCivil.Items.AddRange(vListaEdoCivil.Select(s => new RadComboBoxItem(s.NB_CATALOGO_VALOR, s.CL_CATALOGO_VALOR)
            {
                Selected = s.FG_SELECCIONADO
            }));
        }

        public void ObtenerGeneros()
        {
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

        public void ObtenerAreas()
        {
            cmbAreas.Items.AddRange(vListaAreas.Select(s => new RadComboBoxItem(s.NB_DEPARTAMENTO, s.ID_DEPARTAMENTO.Value.ToString())
            {
                Selected = s.FG_SELECCIONADO
            }));
        }

        public void ObtenerCentroAdmvo()
        {
            cmbAdministrativo.Items.AddRange(vListaCentroAdmvo.Select(s => new RadComboBoxItem(s.NB_CENTRO_ADMVO, s.ID_CENTRO_ADMVO)
            {
                Selected = s.FG_SELECCIONADO
            }));
        }

        public void ObtenerCentroOptvo()
        {
            cmbOperativo.Items.AddRange(vListaCentroOptvo.Select(s => new RadComboBoxItem(s.NB_CENTRO_OPTVO, s.ID_CENTRO_OPTVO)
            {
                Selected = s.FG_SELECCIONADO
            }));
        }

        public void ObtenerPuestos(string pPuesto, RadComboBox radCmb)
        {
            radCmb.DataSource = vListaPuestos.Where(w => w.CL_TIPO_RELACION.Equals(pPuesto)).ToList();
            radCmb.DataTextField = "NB_PUESTO";
            radCmb.DataValueField = "ID_PUESTO";
            radCmb.DataBind();
        }

        public void insertarDatos(GridCommandEventArgs e)
        {
            GridEditFormInsertItem item = e.Item as GridEditFormInsertItem;

            switch (item.OwnerTableView.Name)
            {
                case "Funciones Genericas":

                    vIdFunGen++;

                    RadEditor notas = (item.FindControl("radEditorNotas") as RadEditor);
                    RadEditor funciones = (item.FindControl("radEditorFuncionesEspec") as RadEditor);

                    //notas.EditModes = notas.EditModes ^ Telerik.Web.UI.EditModes.Html;
                    //funciones.EditModes = funciones.EditModes ^ Telerik.Web.UI.EditModes.Html;

                    E_FUNCION_GENERICA funcGen = new E_FUNCION_GENERICA();
                    funcGen.NB_FUNCION_GENERICA = (item.FindControl("radTxtFuncionGenerica") as RadTextBox).Text;
                    funcGen.DS_NOTAS = notas.Content;
                    funcGen.DS_DETALLE = funciones.Content;
                    funcGen.ID_FUNCION_GENERICA = vIdFunGen;

                    vListaFuncionesGenericas.Add(funcGen);

                    item.Edit = false;
                    e.Canceled = true;

                    ObtenerFuncionesGenericas();

                    break;

                case "Competencias especificas":

                    vIdCompEsp++;

                    GridDataItem funcion = (GridDataItem)item.OwnerTableView.ParentItem;

                    E_FUNCION_COMPETENCIA comp = new E_FUNCION_COMPETENCIA();
                    comp.ID_COMPETENCIA = int.Parse((item.FindControl("cmbCompetenciaEspecificas") as RadComboBox).SelectedValue);
                    comp.NB_COMPETENCIA = (item.FindControl("cmbCompetenciaEspecificas") as RadComboBox).Text;
                    comp.NO_NIVEL = int.Parse((item.FindControl("RadListNinel") as RadSlider).SelectedValue.ToString());
                    comp.ID_FUNCION_GENERICA = int.Parse(funcion.OwnerTableView.DataKeyValues[funcion.ItemIndex]["ID_FUNCION_GENERICA"].ToString());
                    comp.ID_FUNCION_COMPETENCIA = vIdCompEsp;

                    vListaFuncionCompetencias.Add(comp);

                    item.Edit = false;
                    e.Canceled = true;

                    ObtenerFuncionesGenericas();

                    break;

                case "Indicadores":
                    vIdIndi++;

                    GridDataItem competencia = (GridDataItem)item.OwnerTableView.ParentItem;

                    E_FUNCION_INDICADOR indi = new E_FUNCION_INDICADOR();
                    indi.ID_FUNCION_COMPETENCIA = int.Parse(competencia.OwnerTableView.DataKeyValues[competencia.ItemIndex]["ID_FUNCION_COMPETENCIA"].ToString());
                    indi.ID_FUNCION_GENERICA = int.Parse(competencia.OwnerTableView.DataKeyValues[competencia.ItemIndex]["ID_FUNCION_GENERICA"].ToString());
                    indi.ID_FUNCION_INDICADOR = vIdIndi;
                    indi.ID_INDICADOR_DESEMPENO = int.Parse((item.FindControl("cmbIndicadores") as RadComboBox).SelectedValue);
                    indi.NB_INDICADOR_DESEMPENO = (item.FindControl("cmbIndicadores") as RadComboBox).Text;

                    vListaFuncionIndicadores.Add(indi);

                    item.Edit = false;
                    e.Canceled = true;

                    ObtenerFuncionesGenericas();

                    break;

                default:
                    break;
            }
        }

        public void ActualizarDatos(GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;

            if (item.IsInEditMode)
            {
                if (!(e.Item is IGridInsertItem))
                {
                    GridEditFormItem d = (GridEditFormItem)e.Item;

                    switch (item.OwnerTableView.Name)
                    {
                        case "Funciones Genericas":
                            int vIdFuncion = int.Parse(d.GetDataKeyValue("ID_FUNCION_GENERICA").ToString());

                            vListaFuncionesGenericas.Where(n => n.ID_FUNCION_GENERICA == vIdFuncion).FirstOrDefault().NB_FUNCION_GENERICA = (e.Item.FindControl("radTxtFuncionGenerica") as RadTextBox).Text;
                            vListaFuncionesGenericas.Where(n => n.ID_FUNCION_GENERICA == vIdFuncion).FirstOrDefault().DS_NOTAS = (e.Item.FindControl("radEditorNotas") as RadEditor).Content;
                            vListaFuncionesGenericas.Where(n => n.ID_FUNCION_GENERICA == vIdFuncion).FirstOrDefault().DS_DETALLE = (e.Item.FindControl("radEditorFuncionesEspec") as RadEditor).Content;
                            break;

                        case "Competencias especificas":

                            int vIdCompetencia = int.Parse(d.GetDataKeyValue("ID_FUNCION_COMPETENCIA").ToString());

                            vListaFuncionCompetencias.Where(n => n.ID_FUNCION_COMPETENCIA == vIdCompetencia).FirstOrDefault().ID_COMPETENCIA = int.Parse((e.Item.FindControl("cmbCompetenciaEspecificas") as RadComboBox).SelectedValue);
                            vListaFuncionCompetencias.Where(n => n.ID_FUNCION_COMPETENCIA == vIdCompetencia).FirstOrDefault().NB_COMPETENCIA = (e.Item.FindControl("cmbCompetenciaEspecificas") as RadComboBox).Text;
                            vListaFuncionCompetencias.Where(n => n.ID_FUNCION_COMPETENCIA == vIdCompetencia).FirstOrDefault().NO_NIVEL = int.Parse((e.Item.FindControl("RadListNinel") as RadSlider).Value.ToString());
                            break;

                        case "Indicadores":
                            int vIdDesempeno = int.Parse(d.GetDataKeyValue("ID_FUNCION_INDICADOR").ToString());

                            vListaFuncionIndicadores.Where(n => n.ID_FUNCION_INDICADOR == vIdDesempeno).FirstOrDefault().ID_INDICADOR_DESEMPENO = int.Parse((e.Item.FindControl("cmbIndicadores") as RadComboBox).SelectedValue);
                            vListaFuncionIndicadores.Where(n => n.ID_FUNCION_INDICADOR == vIdDesempeno).FirstOrDefault().NB_INDICADOR_DESEMPENO = (e.Item.FindControl("cmbIndicadores") as RadComboBox).Text;
                            break;
                    }

                    item.Edit = false;
                    e.Canceled = true;

                    ObtenerFuncionesGenericas();
                }
            }
        }

        public void agregarToolTip()
        {
            foreach (GridDataItem item in dgvCompetencias.MasterTableView.Items)
            {
                if (item.DataItem != null)
                {
                    E_COMPETENCIAS c = (E_COMPETENCIAS)item.DataItem;
                    RadSlider vRsFila = (RadSlider)item.FindControl("rsNivel1");

                    vRsFila.Items[0].ToolTip = c.DS_COMENTARIOS_NIVEL0;
                    vRsFila.Items[1].ToolTip = c.DS_COMENTARIOS_NIVEL1;
                    vRsFila.Items[2].ToolTip = c.DS_COMENTARIOS_NIVEL2;
                    vRsFila.Items[3].ToolTip = c.DS_COMENTARIOS_NIVEL3;
                    vRsFila.Items[4].ToolTip = c.DS_COMENTARIOS_NIVEL4;
                    vRsFila.Items[5].ToolTip = c.DS_COMENTARIOS_NIVEL5;
                }
            }
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

            //DATOS GENERALES DEL PUESTO
            XElement pDescriptivo = new XElement("DESCRIPTIVO",
                new XAttribute("ID_DESCRIPTIVO", vIdDescriptivo),
                new XAttribute("CL_PUESTO", txtNombreCorto.Text),
                new XAttribute("NB_PUESTO", txtDescripcionPuesto.Text),
                new XAttribute("NO_EDAD_MINIMA", txtRangoEdadMin.Value),
                new XAttribute("NO_EDAD_MAXIMA", txtRangoEdadMax.Value),
                new XAttribute("CL_GENERO", cmbGenero.SelectedValue),
                new XAttribute("CL_ESTADO_CIVIL", cmbEdoCivil.SelectedValue),
                new XAttribute("CL_TIPO_PUESTO", btnDirecto.Checked ? "DIRECTO" : "INDIRECTO"),
                new XAttribute("ID_CENTRO_ADMINISTRATIVO", cmbAdministrativo.SelectedValue),
                new XAttribute("ID_CENTRO_OPERATIVO", cmbOperativo.SelectedValue),
                new XAttribute("ID_DEPARTAMENTO", cmbAreas.SelectedValue),
                new XAttribute("CL_POSICION_ORGANIGRAMA", btnStaff.Checked ? "STAF" : "LINEA"),
                new XAttribute("CL_DOCUMENTO", ContextoApp.FgControlDocumentos ? txtClaveDocumento.Text : ""),
                new XAttribute("CL_VERSION", ContextoApp.FgControlDocumentos ? txtVersionDocumento.Text : ""),
                new XAttribute("FE_ELABORACION", ContextoApp.FgControlDocumentos ? txtFeElabDocumento.Text : ""),
                new XAttribute("NB_ELABORO", ContextoApp.FgControlDocumentos ? txtElaboroDocumento.Text : ""),
                new XAttribute("FE_REVISION", ContextoApp.FgControlDocumentos ? txtFeRevDocumento.Text : ""),
                new XAttribute("NB_REVISO", ContextoApp.FgControlDocumentos ? txtRevisoDocumento.Text : ""),
                new XAttribute("FE_AUTORIZACION", ContextoApp.FgControlDocumentos ? txtFeAutorizoDocumento.Text : ""),
                new XAttribute("NB_AUTORIZO", ContextoApp.FgControlDocumentos ? txtAutorizoDocumento.Text : ""),
                new XAttribute("DS_CONTROL_CAMBIOS", ContextoApp.FgControlDocumentos ? txtControlCambios.Text : ""),
                new XAttribute("CL_TIPO_PRESTACIONES", cmbTipoPrestaciones.SelectedValue),
                XElement.Parse(String.Format("<XML_PRESTACIONES>{0}</XML_PRESTACIONES>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorPrestaciones.Content)))),
                XElement.Parse(String.Format("<XML_REQUERIMIENTOS>{0}</XML_REQUERIMIENTOS>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorRequerimientos.Content)))),
                XElement.Parse(String.Format("<XML_OBSERVACIONES>{0}</XML_OBSERVACIONES>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorObservaciones.Content)))),
                XElement.Parse(String.Format("<XML_RESPONSABILIDAD>{0}</XML_RESPONSABILIDAD>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorResponsable.Content)))),
                XElement.Parse(String.Format("<XML_AUTORIDAD>{0}</XML_AUTORIDAD>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorAutoridad.Content)))),
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

            //LISTA DE RELACION DE PUESTOS

            XElement pPuestosRel = new XElement("PUESTOS_REL");

            //SUBORDINADOS
            foreach (RadListBoxItem item in lstPuestosSubordinado.Items)
            {
                pPuestosRel.Add(new XElement("PUESTO_REL",
                    new XAttribute("ID_PUESTO_REL", item.Value),
                    new XAttribute("CL_TIPO_RELACION", E_PUESTO_RELACION.SUBORDINADO.ToString())));
            }

            //INTERRELACIONADOS
            foreach (RadListBoxItem item in lstPuestosInterrelacionados.Items)
            {
                pPuestosRel.Add(new XElement("PUESTO_REL",
                    new XAttribute("ID_PUESTO_REL", item.Value),
                    new XAttribute("CL_TIPO_RELACION", E_PUESTO_RELACION.INTERRELACIONADO.ToString())));
            }

            //RUTA ALTERNATIVA
            foreach (RadListBoxItem item in lstAlternativa.Items)
            {
                pPuestosRel.Add(new XElement("PUESTO_REL",
                    new XAttribute("ID_PUESTO_REL", item.Value),
                    new XAttribute("CL_TIPO_RELACION", E_PUESTO_RELACION.RUTAALTERNATIVA.ToString())));
            }

            //RUTA LATERAL
            foreach (RadListBoxItem item in lstLateral.Items)
            {
                pPuestosRel.Add(new XElement("PUESTO_REL",
                    new XAttribute("ID_PUESTO_REL", item.Value),
                    new XAttribute("CL_TIPO_RELACION", E_PUESTO_RELACION.RUTALATERAL.ToString())));
            }

            //JEFE INMEDIATO
            RadListBoxItem vItem = lstJefeInmediato.Items[0];

            if (!String.IsNullOrWhiteSpace(vItem.Value))
                pPuestosRel.Add(new XElement("PUESTO_REL",
                    new XAttribute("ID_PUESTO_REL", vItem.Value),
                    new XAttribute("CL_TIPO_RELACION", E_PUESTO_RELACION.JEFE.ToString())));

            XElement pFunGen = new XElement("FUNCIONES");

            foreach (E_FUNCION_GENERICA item in vListaFuncionesGenericas)
            {
                pFunGen.Add(new XElement("FUNCION",
                       new XAttribute("ID_PUESTO_FUNCION", item.ID_FUNCION_GENERICA),
                       new XAttribute("NB_FUNCION", item.NB_FUNCION_GENERICA),
                       new XAttribute("DS_FUNCION", item.NB_FUNCION_GENERICA),
                       new XAttribute("DS_DETALLE", item.DS_DETALLE),
                       new XAttribute("DS_NOTAS", item.DS_NOTAS)));
            }

            XElement pFunComGen = new XElement("FUNCION_COMPETENCIAS");

            foreach (E_FUNCION_COMPETENCIA item in vListaFuncionCompetencias)
            {
                pFunComGen.Add(new XElement("FUNCION_COMPETENCIA",
                    new XAttribute("ID_COMPETENCIA", item.ID_COMPETENCIA),
                    new XAttribute("NO_NIVEL", item.NO_NIVEL),
                    new XAttribute("CL_TIPO_COMPETENCIA", "FUNCIONES"),
                    new XAttribute("ID_FUNCION", item.ID_FUNCION_GENERICA),
                    new XAttribute("ID_FUNCION_COMPETENCIA", item.ID_FUNCION_COMPETENCIA)));
            }

            XElement pFunIndicadores = new XElement("FUNCION_INDICADORES");

            foreach (E_FUNCION_INDICADOR item in vListaFuncionIndicadores)
            {
                pFunIndicadores.Add(new XElement("FUNCION_INDICADOR",
                    new XAttribute("ID_INDICADOR", item.ID_INDICADOR_DESEMPENO),
                    new XAttribute("ID_PUESTO_FUNCION", item.ID_FUNCION_GENERICA),
                    new XAttribute("ID_PUESTO_COMPETENCIA", item.ID_FUNCION_COMPETENCIA)));
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

        public void guardarpuesto()
        {
            if (validarControles())
            {
                XElement vXmlCA = generalXmlAdicionales();

                if (vXmlCA != null)
                {
                    string xml = generarXml(vXmlCA);
                    string tran = "";
                    PuestoNegocio neg = new PuestoNegocio();
                    E_PUESTO vPuesto = new E_PUESTO();

                    vPuesto.XML_PUESTO = xml;

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

                    E_RESULTADO vResultado = neg.InsertaActualiza_M_PUESTO(tran, vPuesto, vClUsuario, "Descriptivo");
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                    {
                        Response.Redirect("DescriptivoPuestos.aspx");
                    }
                    else
                    {
                        UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
                    }
                }
            }
        }

        public void ObtenerFuncionesGenericas()
        {
            radGridFuncGenericas.Rebind();
        }

        private void agregarItemLista(RadListBox lista, RadComboBox combo)
        {
            RadListBoxItem item = new RadListBoxItem();
            item.Value = combo.SelectedValue;
            item.Text = combo.Text;

            lista.Items.Add(item);
        }

        private bool validarControles()
        {
            bool vAceptado = true;
            string vMensaje = "";

            if (txtNombreCorto.Text == "" & vAceptado)
            {
                vMensaje = "El campo nombre corto es obligatorio";
                vAceptado = false;
            }

            if (txtDescripcionPuesto.Text == "" & vAceptado)
            {
                vMensaje = "El campo descripción es obligatorio";
                vAceptado = false;
            }

            if (txtRangoEdadMax.Text == "" & vAceptado)
            {
                vMensaje = "El rango de edad es obligatorio";
                vAceptado = false;
            }

            if (txtRangoEdadMin.Text == "" & vAceptado)
            {
                vMensaje = "El rango de edad es obligatorio";
                vAceptado = false;
            }

            if (cmbGenero.SelectedIndex == -1 & vAceptado)
            {
                vMensaje = "El género es obligatorio";
                vAceptado = false;
            }

            if (cmbEdoCivil.SelectedIndex == -1 & vAceptado)
            {
                vMensaje = "El estado civil es obligatorio";
                vAceptado = false;
            }

            if (lstCompetenciasEspecificas.Items.Count == 0 & vAceptado)
            {
                vMensaje = "Las competencias específicas del perfil de ingreso son obligatorias";
                vAceptado = false;
            }

            if (lstExperiencia.Items.Count == 0 & vAceptado)
            {
                vMensaje = "La experiencia es obligatoria";
                vAceptado = false;
            }

            if (radEditorRequerimientos.Content == "" & vAceptado)
            {
                vMensaje = "Los requerimientos son obligatorios";
                vAceptado = false;
            }

            if ((!btnDirecto.Checked & !btnIndirecto.Checked) & vAceptado)
            {
                vMensaje = "El tipo de puesto es obligatorio";
                vAceptado = false;
            }

            if (cmbAreas.SelectedIndex == -1 & vAceptado)
            {
                vMensaje = "El área es obligatoria";
                vAceptado = false;
            }

            if (cmbAdministrativo.SelectedIndex == -1 & vAceptado)
            {
                vMensaje = "El centro administrativo es obligatorio";
                vAceptado = false;
            }

            if (cmbOperativo.SelectedIndex == -1 & vAceptado)
            {
                vMensaje = "El centro operativo es obligatorio";
                vAceptado = false;
            }

            //if (lstJefeInmediato.Items[0].Value == "" & vAceptado)
            //{
            //    vMensaje = "El puesto del jefe inmediato es obligatorio";
            //    vAceptado = false;
            //}

            if ((!btnLinea.Checked & !btnStaff.Checked) & vAceptado)
            {
                vMensaje = "La posición en el organigrama es obligatoria";
                vAceptado = false;
            }

            if (radEditorResponsable.Content == "" & vAceptado)
            {
                vMensaje = "El campo \"Responsable de \" es obligatorio";
                vAceptado = false;
            }

            if (radEditorAutoridad.Content == "" & vAceptado)
            {
                vMensaje = "El campo \"Autoridad\" es obligatorio";
                vAceptado = false;
            }

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
                vIdFunGen = 0;
                vIdCompEsp = 0;
                vIdIndi = 0;

                vListaFuncionesGenericas = new List<E_FUNCION_GENERICA>();
                vListaFuncionCompetencias = new List<E_FUNCION_COMPETENCIA>();
                vListaFuncionIndicadores = new List<E_FUNCION_INDICADOR>();
                vListaExperiencia = new List<E_EXPERIENCIA>();

                if (!ContextoApp.FgControlDocumentos)
                {
                    slzOpciones.Visible = false;
                    rpnOpciones.Visible = false;
                }


                //Verificar si se va realizar es una insercion o modificacion
                if (Request.QueryString["pIdPuesto"] != null)
                {

                    if (Request.QueryString["pIsCopy"] != null)
                    {
                        vIsCopy = true;
                    }

                    vIdDescriptivo = int.Parse(Request.QueryString["pIdPuesto"]);
                    CargarDatos(vIdDescriptivo);
                }
                else
                {
                    CargarDatos(null);
                }

                ObtenerEstadosCiviles();
                ObtenerGeneros();

                //LLENAR LAS ESCOLARIDADES
                ObtenerEscolaridad(E_CL_TIPO_ESCOLARIDAD.POSGRADO.ToString(), radcmbPostgrados);
                ObtenerEscolaridad(E_CL_TIPO_ESCOLARIDAD.PROFESIONAL.ToString(), cmbCarreraProf);
                ObtenerEscolaridad(E_CL_TIPO_ESCOLARIDAD.BACHILLERATO.ToString(), cmbCarrTec);

                ObtenerCompetenciasEspecificas();
                ObtenerAreaInteres();

                ObtenerAreas();

                ObtenerCentroAdmvo();
                ObtenerCentroOptvo();

                ObtenerPuestos(E_PUESTO_RELACION.SUBORDINADO.ToString(), cmbPuestosSubordinado);
                ObtenerPuestos(E_PUESTO_RELACION.INTERRELACIONADO.ToString(), cmbPuestosInterrelacionados);
                ObtenerPuestos(E_PUESTO_RELACION.RUTAALTERNATIVA.ToString(), cmbAlternativa);
                ObtenerPuestos(E_PUESTO_RELACION.RUTALATERAL.ToString(), cmbLateral);

                lstExperiencia.DataSource = vListaExperiencia;
                lstExperiencia.DataValueField = "ID_AREA_INTERES";
                lstExperiencia.DataTextField = "NB_AREA_INTERES";
                lstExperiencia.DataBind();
            }

            vClUsuario = "Fhernandez";
            vNbPrograma = "Descriptivo de puestos";
        }

        protected void radGridFuncGenericas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                radGridFuncGenericas.DataSource = vListaFuncionesGenericas;
            }
        }

        protected void radGridFuncGenericas_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "Competencias especificas":

                    e.DetailTableView.DataSource = vListaFuncionCompetencias.Where(n => n.ID_FUNCION_GENERICA == int.Parse(dataItem.GetDataKeyValue("ID_FUNCION_GENERICA").ToString()));
                    break;

                case "Indicadores":
                    e.DetailTableView.DataSource = vListaFuncionIndicadores.Where(n => n.ID_FUNCION_COMPETENCIA == int.Parse(dataItem.GetDataKeyValue("ID_FUNCION_COMPETENCIA").ToString()));
                    break;
            }
        }

        protected void radGridFuncGenericas_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode)
            {
                GridEditableItem item = (GridEditableItem)e.Item;

                switch (item.OwnerTableView.Name)
                {
                    case "Indicadores":
                        RadComboBox cmbIndicadores = (RadComboBox)item.FindControl("cmbIndicadores");
                        cmbIndicadores.DataSource = vListaIndicadorDesempeno;
                        cmbIndicadores.DataTextField = "NB_INDICADOR";
                        cmbIndicadores.DataValueField = "ID_INDICADOR";
                        cmbIndicadores.DataBind();
                        break;

                    case "Competencias especificas":
                        RadComboBox cmbCompEsp = (RadComboBox)item.FindControl("cmbCompetenciaEspecificas");
                        cmbCompEsp.DataSource = vListaCatalogoCompetencias.Where(n => n.CL_TIPO_COMPETENCIA == "ESP");
                        cmbCompEsp.DataValueField = "ID_COMPETENCIA";
                        cmbCompEsp.DataTextField = "NB_COMPETENCIA";
                        cmbCompEsp.DataBind();
                        break;
                    default:
                        break;
                }

                if (!(e.Item is IGridInsertItem))
                {
                    switch (e.Item.OwnerTableView.Name)
                    {
                        case "Funciones Genericas":
                            (e.Item.FindControl("radTxtFuncionGenerica") as RadTextBox).Text = ((E_FUNCION_GENERICA)e.Item.DataItem).NB_FUNCION_GENERICA;
                            (e.Item.FindControl("radEditorFuncionesEspec") as RadEditor).Content = ((E_FUNCION_GENERICA)e.Item.DataItem).DS_DETALLE;
                            (e.Item.FindControl("radEditorNotas") as RadEditor).Content = ((E_FUNCION_GENERICA)e.Item.DataItem).DS_NOTAS;
                            break;

                        case "Competencias especificas":
                            (e.Item.FindControl("cmbCompetenciaEspecificas") as RadComboBox).SelectedValue = ((E_FUNCION_COMPETENCIA)e.Item.DataItem).ID_COMPETENCIA.ToString();
                            (e.Item.FindControl("RadListNinel") as RadSlider).Value = ((E_FUNCION_COMPETENCIA)e.Item.DataItem).NO_NIVEL;
                            break;

                        case "Indicadores":
                            (e.Item.FindControl("cmbIndicadores") as RadComboBox).SelectedValue = ((E_FUNCION_INDICADOR)e.Item.DataItem).ID_INDICADOR_DESEMPENO.ToString();
                            break;
                    }
                }
            }
        }

        protected void radGridFuncGenericas_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "PerformInsert":
                    insertarDatos(e);
                    break;
                case "Update":
                    ActualizarDatos(e);
                    break;
            }
        }

        protected void radGridFuncGenericas_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = (GridEditableItem)e.Item;
            int vId;

            switch (e.Item.OwnerTableView.Name)
            {
                case "Funciones Genericas":
                    vId = int.Parse(item.GetDataKeyValue("ID_FUNCION_GENERICA").ToString());

                    vListaFuncionesGenericas.RemoveAll(n => n.ID_FUNCION_GENERICA == vId);
                    vListaFuncionCompetencias.RemoveAll(n => n.ID_FUNCION_GENERICA == vId);
                    vListaFuncionIndicadores.RemoveAll(n => n.ID_FUNCION_GENERICA == vId);
                    break;

                case "Competencias especificas":
                    vId = int.Parse(item.GetDataKeyValue("ID_COMPETENCIA_ESPECIFICA").ToString());

                    vListaFuncionCompetencias.RemoveAll(n => n.ID_FUNCION_COMPETENCIA == vId);
                    vListaFuncionIndicadores.RemoveAll(n => n.ID_FUNCION_COMPETENCIA == vId);
                    break;

                case "Indicadores":
                    vId = int.Parse(item.GetDataKeyValue("ID_FUNCION_INDICADOR").ToString());

                    vListaFuncionIndicadores.RemoveAll(n => n.ID_FUNCION_INDICADOR == vId);
                    break;
            }

            ObtenerFuncionesGenericas();
        }

        protected void dgvCompetencias_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            dgvCompetencias.DataSource = vListaCompetencias.Where(n => n.CL_TIPO_COMPETENCIA != "ESP");
        }

        protected void dgvCompetencias_DataBound(object sender, EventArgs e)
        {
            agregarToolTip();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            guardarpuesto();
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

            if (btnRequerida.Checked)
            {
                ex.CL_TIPO_EXPERIENCIA = "Requerida";
            }
            else
            {
                ex.CL_TIPO_EXPERIENCIA = "Deseada";
            }

            vListaExperiencia.Add(ex);
            lstExperiencia.DataSource = vListaExperiencia;
            lstExperiencia.DataBind();
            divContenido.Style.Add("display", "none");

        }

        protected void radBtnAgregarPuestoSubordinado_Click(object sender, EventArgs e)
        {
            agregarItemLista(lstPuestosSubordinado, cmbPuestosSubordinado);
        }

        protected void btnInter_Click(object sender, EventArgs e)
        {
            agregarItemLista(lstPuestosInterrelacionados, cmbPuestosInterrelacionados);
        }

        protected void btnRutaAlter_Click(object sender, EventArgs e)
        {
            agregarItemLista(lstAlternativa, cmbAlternativa);
        }

        protected void btnLateral_Click(object sender, EventArgs e)
        {
            agregarItemLista(lstLateral, cmbLateral);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../IDP/DescriptivoPuestos.aspx");
        }
    }
}