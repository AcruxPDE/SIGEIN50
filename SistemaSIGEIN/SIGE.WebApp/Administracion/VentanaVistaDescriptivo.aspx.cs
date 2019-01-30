using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
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
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Entidades;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaVistaDescriptivo : System.Web.UI.Page
    {
        #region Varianbles

        private string vClUsuario = string.Empty;
        private string vNbPrograma = string.Empty;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vNbFirstRadEditorTagName = "p";

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

        private Guid? vIdEditingItem
        {
            get { return (Guid?)ViewState["vs_vIdEditingItem"]; }
            set { ViewState["vs_vIdEditingItem"] = value; }
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
            vNoPlazas = vDescriptivo.NO_PLAZAS;
            vNoMinimoPlazas = vDescriptivo.NO_MINIMO_PLAZAS;

            //txtNoPlazas.MinValue = (double)vNoMinimoPlazas;

            if (vDescriptivo.LST_OCUPACION_PUESTO != null)
            {
                lblClOcupación.Text = vDescriptivo.LST_OCUPACION_PUESTO.CL_OCUPACION;
                lblOcupacionSeleccionada.Text = vDescriptivo.LST_OCUPACION_PUESTO.NB_OCUPACION;
            }

            vFuncionesGenericas = new List<E_FUNCION_GENERICA>();
            vLstFuncionCompetencia = new List<E_FUNCION_COMPETENCIA>();
            vLstFuncionCompetenciaIndicador = new List<E_FUNCION_INDICADOR>();

            asignarValoresAdicionales(vDescriptivo.XML_CAMPOS_ADICIONALES);

            if (pIdDescriptivo != null)
            {
                txtNombreCorto.InnerText = vDescriptivo.CL_PUESTO;
                txtDescripcionPuesto.InnerText = vDescriptivo.NB_PUESTO;
                txtNoPlazas.InnerText = vDescriptivo.NO_PLAZAS.Value.ToString();

                txtRangoEdadMin.InnerText = vDescriptivo.NO_EDAD_MINIMA.ToString();
                txtRangoEdadMax.InnerText = vDescriptivo.NO_EDAD_MAXIMA.ToString();

                var xmlPuestoEscolaridad = XElement.Parse(vDescriptivo.XML_PUESTO_ESCOLARIDAD).Element("OTRO_PUESTO_ESCOLARIDAD");

                if (xmlPuestoEscolaridad.HasAttributes)
                    txtOtroNivelEst.Text = xmlPuestoEscolaridad.Attribute("DS_OTRO_NIVEL_ESCOLARIDAD").Value;

               // txtCompetenciasRequeridas.InnerHtml = vDescriptivo.DS_COMPETENCIAS_REQUERIDAS;
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

                if (vDescriptivo.XML_REQUERIMIENTOS != null)
                {
                    XElement vRequerimientos = XElement.Parse(vDescriptivo.XML_REQUERIMIENTOS);
                    if (vRequerimientos != null)
                    {
                        //vRequerimientos.Name = vNbFirstRadEditorTagName;
                        txtRequerimientos.InnerHtml = validarDsNotas(vRequerimientos.ToString());
                    }
                }

                if (vDescriptivo.XML_OBSERVACIONES != null)
                {
                    XElement vObservaciones = XElement.Parse(vDescriptivo.XML_OBSERVACIONES);
                    if (vObservaciones != null)
                    {
                        //vObservaciones.Name = vNbFirstRadEditorTagName;
                        txtObservaciones.InnerHtml = validarDsNotas(vObservaciones.ToString());
                    }
                }

                txtTipoPuesto.InnerText = vDescriptivo.CL_TIPO_PUESTO;
                //btnDirecto.Checked = vDescriptivo.CL_TIPO_PUESTO == "DIRECTO";
                //btnIndirecto.Checked = vDescriptivo.CL_TIPO_PUESTO == "INDIRECTO";


                //foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTOS_RELACIONADOS).Elements("PUESTO_RELACIONADO"))
                //{
                //    if (item.Attribute("CL_TIPO_RELACION").Value == E_PUESTO_RELACION.JEFE.ToString())
                //    {
                //        lstJefesInmediatos.InnerText = item.Attribute("NB_PUESTO").Value;
                //    }
                //}

                DescriptivoNegocio vNeg = new DescriptivoNegocio();
                List<SPE_OBTIENE_JEFES_DESCRIPTIVO_Result> vLstJefes = vNeg.ObtenerJefesDescriptivo(vIdDescriptivo).ToList();
                foreach (SPE_OBTIENE_JEFES_DESCRIPTIVO_Result item in vLstJefes)
                {
                    RadListBoxItem i = new RadListBoxItem();
                    i.Text = item.CL_PUESTO + " - " + item.NB_PUESTO;
                    i.Value = item.ID_PUESTO.ToString();
                    lstJefesInmediatos.Items.Add(i);
                }

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

                List<SPE_OBTIENE_SUBORDINADOS_DESCRIPTIVO_Result> vLstSubordinados = vNeg.ObtenerSubordinadosDescriptivo(vIdDescriptivo).ToList();
                foreach (SPE_OBTIENE_SUBORDINADOS_DESCRIPTIVO_Result item in vLstSubordinados)
                {
                        RadListBoxItem i = new RadListBoxItem();
                        i.Text = item.CL_PUESTO +" - "+ item.NB_PUESTO;
                        i.Value = item.ID_PUESTO.ToString();
                        lstPuestosSubordinado.Items.Add(i);
                }

                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTOS_RELACIONADOS).Elements("PUESTO_RELACIONADO"))
                {
                    if (item.Attribute("CL_TIPO_RELACION").Value == E_PUESTO_RELACION.INTERRELACIONADO.ToString())
                    {
                        RadListBoxItem i = new RadListBoxItem();

                        i.Text = item.Attribute("CL_PUESTO").Value +" - "+ item.Attribute("NB_PUESTO").Value;
                        i.Value = item.Attribute("ID_PUESTO_RELACIONADO").Value;
                        lstPuestosInterrelacionados.Items.Add(i);
                    }
                }

                //btnLinea.Checked = vDescriptivo.CL_POSICION_ORGANIGRAMA == "LINEA";
                //btnStaff.Checked = vDescriptivo.CL_POSICION_ORGANIGRAMA == "STAFF";

                txtPosicionOrganigrama.InnerText = vDescriptivo.CL_POSICION_ORGANIGRAMA;

                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTOS_RELACIONADOS).Elements("PUESTO_RELACIONADO"))
                {
                    if (item.Attribute("CL_TIPO_RELACION").Value == E_PUESTO_RELACION.RUTAALTERNATIVA.ToString())
                    {
                        RadListBoxItem i = new RadListBoxItem();

                        i.Text = item.Attribute("CL_PUESTO").Value +" - "+ item.Attribute("NB_PUESTO").Value;
                        i.Value = item.Attribute("ID_PUESTO_RELACIONADO").Value;
                        lstAlternativa.Items.Add(i);
                    }
                }

                foreach (XElement item in XElement.Parse(vDescriptivo.XML_PUESTOS_RELACIONADOS).Elements("PUESTO_RELACIONADO"))
                {
                    if (item.Attribute("CL_TIPO_RELACION").Value == E_PUESTO_RELACION.RUTALATERAL.ToString())
                    {
                        RadListBoxItem i = new RadListBoxItem();

                        i.Text = item.Attribute("CL_PUESTO").Value +" - "+ item.Attribute("NB_PUESTO").Value;
                        i.Value = item.Attribute("ID_PUESTO_RELACIONADO").Value;
                        lstLateral.Items.Add(i);
                    }
                }

                if (vDescriptivo.XML_RESPONSABILIDAD != null)
                {
                    XElement vResponsabilidad = XElement.Parse(vDescriptivo.XML_RESPONSABILIDAD);
                    if (vResponsabilidad != null)
                    {
                        //vResponsabilidad.Name = vNbFirstRadEditorTagName;
                        txtResponsable.InnerHtml = validarDsNotas( vResponsabilidad.ToString());
                    }
                }

                if (vDescriptivo.XML_AUTORIDAD != null)
                {
                    XElement vAutoridad = XElement.Parse(vDescriptivo.XML_AUTORIDAD);
                    if (vAutoridad != null)
                    {
                        //vAutoridad.Name = vNbFirstRadEditorTagName;
                        txtAutoridad.InnerHtml = validarDsNotas(vAutoridad.ToString());
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

                if (vDescriptivo.CL_TIPO_PRESTACIONES != "")
                {
                    txtTipoPrestaciones.InnerText = vDescriptivo.CL_TIPO_PRESTACIONES;
                }
                else
                {
                    txtTipoPrestaciones.InnerText = "No seleccionado";
                }

                if (vDescriptivo.XML_PRESTACIONES != null && vDescriptivo.XML_PRESTACIONES != "")
                {
                    XElement vPrestaciones = XElement.Parse(vDescriptivo.XML_PRESTACIONES);
                    if (vPrestaciones != null)
                    {
                        txtPrestaciones.InnerHtml = validarDsNotas(vPrestaciones.ToString());
                    }
                }

                if (ContextoApp.FgControlDocumentos)
                {
                    txtCDClaveDocumento.InnerText = vDescriptivo.CL_DOCUMENTO;
                    txtCDVersion.InnerText = vDescriptivo.CL_VERSION;

                    if (vDescriptivo.FE_ELABORACION.HasValue)
                        txtCDFechaElaboracion.InnerText = vDescriptivo.FE_ELABORACION.Value.ToShortDateString();

                    txtCDElaboro.InnerText = vDescriptivo.NB_ELABORO;

                    if (vDescriptivo.FE_REVISION.HasValue)
                        txtCDFechaRevision.InnerText = vDescriptivo.FE_REVISION.Value.ToShortDateString();

                    txtCDRevision.InnerText = vDescriptivo.NB_REVISO;

                    if (vDescriptivo.FE_AUTORIZACION.HasValue)
                        txtCDFechaAutorizacion.InnerText = vDescriptivo.FE_AUTORIZACION.Value.ToShortDateString();

                    txtCDAutorizo.InnerText = vDescriptivo.NB_AUTORIZO;
                    txtCDControlCambios.InnerText = vDescriptivo.DS_CONTROL_CAMBIOS;
                }

                if (vIsCopy)
                    vIdDescriptivo = null;
            }
        }


        public string validarDsNotas(string vdsNotas)
        {
            E_NOTA pNota = null;
            if (vdsNotas != null)
            {
                XElement vNotas = XElement.Parse(vdsNotas.ToString());
                if (ValidarRamaXml(vNotas, "NOTA"))
                {

                    pNota = vNotas.Elements("NOTA").Select(el => new E_NOTA
                    {
                        DS_NOTA = UtilXML.ValorAtributo<string>(el.Attribute("DS_NOTA")),
                        FE_NOTA = (DateTime?)UtilXML.ValorAtributo(el.Attribute("FE_NOTA"), E_TIPO_DATO.DATETIME),
                    }).FirstOrDefault();

                }
                if (pNota != null)
                {
                    if (pNota.DS_NOTA != null)
                    {
                        return pNota.DS_NOTA.ToString();
                    }
                    else return "";
                }
                else return "";
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



        public void ObtenerAreaInteres()
        {
            //cmbExperiencias.DataSource = vListaAreasInteres;
            //cmbExperiencias.DataTextField = "NB_AREA_INTERES";
            //cmbExperiencias.DataValueField = "ID_AREA_INTERES";
            //cmbExperiencias.DataBind();
        }

        public void ObtenerCompetenciasEspecificas()
        {
            //cmbCompetenciaEspecificas.DataSource = vListaCatalogoCompetencias.Where(n => n.CL_TIPO_COMPETENCIA == "ESP");
            //cmbCompetenciaEspecificas.DataTextField = "NB_COMPETENCIA";
            //cmbCompetenciaEspecificas.DataValueField = "ID_COMPETENCIA";
            //cmbCompetenciaEspecificas.DataBind();

            //cmbCompetenciaEspecifica.DataSource = vListaCatalogoCompetencias.Where(n => n.CL_TIPO_COMPETENCIA == "ESP");
            //cmbCompetenciaEspecifica.DataTextField = "NB_COMPETENCIA";
            //cmbCompetenciaEspecifica.DataValueField = "ID_COMPETENCIA";
            //cmbCompetenciaEspecifica.DataBind();
        }

        public void ObtenerEstadosCiviles()
        {
            string vNbEstadoCivil;
            var vEstadoCivil = vListaEdoCivil.Where(t => t.FG_SELECCIONADO == true).FirstOrDefault();

            if (vEstadoCivil != null)
            {
                vNbEstadoCivil = vEstadoCivil.NB_CATALOGO_VALOR;
            }
            else
            {
                vNbEstadoCivil = "Indistinto";
            }

            cmbEdoCivil.InnerText = vNbEstadoCivil;
        }

        public void ObtenerGeneros()
        {
            string vNbGenero;

            var vGenero = vListaGenero.Where(t => t.FG_SELECCIONADO).FirstOrDefault();

            if (vGenero != null)
            {
                vNbGenero = vGenero.NB_CATALOGO_VALOR;
            }
            else
            {
                vNbGenero = "Indistinto";
            }

            cmbGenero.InnerText = vNbGenero;
        }

        public void ObtenerEscolaridad(string pNivelEscolaridad, RadComboBox radCmb)
        {
            radCmb.DataSource = vListaEscoloridad.Where(w => w.CL_NB_NIVEL_ESCOLARIDAD.Equals(pNivelEscolaridad) && w.FG_ACTIVO.Equals(true)).ToList();
            radCmb.DataTextField = "NB_ESCOLARIDAD";
            radCmb.DataValueField = "ID_ESCOLARIDAD";
            radCmb.DataBind();
        }

        //public void ObtenerAreas()
        //{
        //    string vNbArea;

        //    var vArea = vListaAreas.Where(t => t.FG_SELECCIONADO).FirstOrDefault();

        //    if (vArea != null)
        //    {
        //        vNbArea = vArea.NB_DEPARTAMENTO;
        //    }
        //    else
        //    {
        //        vNbArea = "No seleccionado";
        //    }

        //  //  txtArea.InnerText = vNbArea;
        //}

        //public void ObtenerCentroAdmvo()
        //{
        //    string vNbCentroAdmvo;

        //    var vCentroAdmvo = vListaCentroAdmvo.Where(t => t.FG_SELECCIONADO).FirstOrDefault();

        //    if (vCentroAdmvo != null)
        //    {
        //        vNbCentroAdmvo = vCentroAdmvo.NB_CENTRO_ADMVO;
        //    }
        //    else
        //    {
        //        vNbCentroAdmvo = "No Seleccionado";
        //    }

        //    txtCentroAdmin.InnerText = vNbCentroAdmvo;
        //}

        //public void ObtenerCentroOptvo()
        //{
        //    string vNbCentroOptvo;

        //    var vCentroOptvo = vListaCentroOptvo.Where(t => t.FG_SELECCIONADO).FirstOrDefault();

        //    if (vCentroOptvo != null)
        //    {
        //        vNbCentroOptvo = vCentroOptvo.NB_CENTRO_OPTVO;
        //    }
        //    else
        //    {
        //        vNbCentroOptvo = "No Seleccionado";
        //    }

        //    txtCentroOptvo.InnerText = vNbCentroOptvo;

        //}

        public void ObtenerPuestos(string pPuesto, RadComboBox radCmb)
        {
            //radCmb.DataSource = vListaPuestos.Where(w => w.CL_TIPO_RELACION.Equals(pPuesto)).ToList();
            //radCmb.DataSource = vListaPuestos;
            //radCmb.DataTextField = "NB_PUESTO";
            //radCmb.DataValueField = "ID_PUESTO";
            //radCmb.DataBind();
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

        //public string generarXml(XElement pXmlAdicionales)
        //{
        //    XElement pPuesto = new XElement("PUESTO");

        //    if (vIdDescriptivo == null)
        //    {
        //        vIdDescriptivo = 0;
        //    }

        //    //XElement vXmlPrestaciones = XElement.Parse(String.Format("<XML_PRESTACIONES>{0}</XML_PRESTACIONES>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorPrestaciones.Content))));
        //    //XElement vXmlRequerimientos = XElement.Parse(String.Format("<XML_REQUERIMIENTOS>{0}</XML_REQUERIMIENTOS>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorRequerimientos.Content))));
        //    //XElement vXmlObservaciones = XElement.Parse(String.Format("<XML_OBSERVACIONES>{0}</XML_OBSERVACIONES>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorObservaciones.Content))));
        //    //XElement vXmlResponsabilidad = XElement.Parse(String.Format("<XML_RESPONSABILIDAD>{0}</XML_RESPONSABILIDAD>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorResponsable.Content))));
        //    //XElement vXmlAutoridad = XElement.Parse(String.Format("<XML_AUTORIDAD>{0}</XML_AUTORIDAD>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorAutoridad.Content))));

        //    //DATOS GENERALES DEL PUESTO
        //    XElement pDescriptivo = new XElement("DESCRIPTIVO",
        //        new XAttribute("ID_DESCRIPTIVO", vIdDescriptivo),
        //        new XAttribute("CL_PUESTO", txtNombreCorto.Text),
        //        new XAttribute("NB_PUESTO", txtDescripcionPuesto.Text),
        //        new XAttribute("NO_EDAD_MINIMA", txtRangoEdadMin.Value),
        //        new XAttribute("NO_EDAD_MAXIMA", txtRangoEdadMax.Value),
        //        new XAttribute("CL_GENERO", cmbGenero.SelectedValue),
        //        new XAttribute("CL_ESTADO_CIVIL", cmbEdoCivil.SelectedValue),
        //        new XAttribute("CL_TIPO_PUESTO", btnDirecto.Checked ? "DIRECTO" : "INDIRECTO"),
        //        new XAttribute("ID_CENTRO_ADMINISTRATIVO", cmbAdministrativo.SelectedValue),
        //        new XAttribute("ID_CENTRO_OPERATIVO", cmbOperativo.SelectedValue),
        //        new XAttribute("ID_DEPARTAMENTO", cmbAreas.SelectedValue),
        //        new XAttribute("CL_POSICION_ORGANIGRAMA", btnStaff.Checked ? "STAFF" : "LINEA"),
        //        new XAttribute("CL_DOCUMENTO", ContextoApp.FgControlDocumentos ? txtClaveDocumento.Text : ""),
        //        new XAttribute("CL_VERSION", ContextoApp.FgControlDocumentos ? txtVersionDocumento.Text : ""),
        //        new XAttribute("FE_ELABORACION", ContextoApp.FgControlDocumentos ? txtFeElabDocumento.Text : ""),
        //        new XAttribute("NB_ELABORO", ContextoApp.FgControlDocumentos ? txtElaboroDocumento.Text : ""),
        //        new XAttribute("FE_REVISION", ContextoApp.FgControlDocumentos ? txtFeRevDocumento.Text : ""),
        //        new XAttribute("NB_REVISO", ContextoApp.FgControlDocumentos ? txtRevisoDocumento.Text : ""),
        //        new XAttribute("FE_AUTORIZACION", ContextoApp.FgControlDocumentos ? txtFeAutorizoDocumento.Text : ""),
        //        new XAttribute("NB_AUTORIZO", ContextoApp.FgControlDocumentos ? txtAutorizoDocumento.Text : ""),
        //        new XAttribute("DS_CONTROL_CAMBIOS", ContextoApp.FgControlDocumentos ? txtControlCambios.Text : ""),
        //        new XAttribute("CL_TIPO_PRESTACIONES", cmbTipoPrestaciones.SelectedValue),
        //        new XAttribute("NO_PLAZAS", (int)txtNoPlazas.Value),
        //        EditorContentToXml("XML_PRESTACIONES", radEditorPrestaciones.Content, vNbFirstRadEditorTagName),
        //        EditorContentToXml("XML_REQUERIMIENTOS", radEditorRequerimientos.Content, vNbFirstRadEditorTagName),
        //        EditorContentToXml("XML_OBSERVACIONES", radEditorObservaciones.Content, vNbFirstRadEditorTagName),
        //        EditorContentToXml("XML_RESPONSABILIDAD", radEditorResponsable.Content, vNbFirstRadEditorTagName),
        //        EditorContentToXml("XML_AUTORIDAD", radEditorAutoridad.Content, vNbFirstRadEditorTagName),

        //        //XElement.Parse(String.Format("<XML_PRESTACIONES>{0}</XML_PRESTACIONES>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorPrestaciones.Content)))),
        //        //XElement.Parse(String.Format("<XML_REQUERIMIENTOS>{0}</XML_REQUERIMIENTOS>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorRequerimientos.Content)))),
        //        //XElement.Parse(String.Format("<XML_OBSERVACIONES>{0}</XML_OBSERVACIONES>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorObservaciones.Content)))),
        //        //XElement.Parse(String.Format("<XML_RESPONSABILIDAD>{0}</XML_RESPONSABILIDAD>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorResponsable.Content)))),
        //        //XElement.Parse(String.Format("<XML_AUTORIDAD>{0}</XML_AUTORIDAD>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(radEditorAutoridad.Content)))),
        //        new XElement(pXmlAdicionales));

        //    //LISTA DE ESCOLARIDADES DEL PUESTO
        //    XElement pEscolaridad = new XElement("ESCOLARIDADES");

        //    foreach (RadListBoxItem item in lstPostgrados.Items)
        //    {
        //        pEscolaridad.Add(new XElement("ESCOLARIDAD",
        //            new XAttribute("ID_ESCOLARIDAD", item.Value)));
        //    }

        //    foreach (RadListBoxItem item in lstCarreraprof.Items)
        //    {
        //        pEscolaridad.Add(new XElement("ESCOLARIDAD",
        //            new XAttribute("ID_ESCOLARIDAD", item.Value)));
        //    }

        //    foreach (RadListBoxItem item in lstCarreraTec.Items)
        //    {
        //        pEscolaridad.Add(new XElement("ESCOLARIDAD",
        //            new XAttribute("ID_ESCOLARIDAD", item.Value)));
        //    }

        //    //COMPETENCIAS ESPECIFICAS
        //    XElement pCompEsp = new XElement("COMPETENCIAS");

        //    foreach (RadListBoxItem item in lstCompetenciasEspecificas.Items)
        //    {
        //        pCompEsp.Add(new XElement("COMPETENCIA",
        //            new XAttribute("ID_COMPETENCIA", item.Value),
        //            new XAttribute("CL_TIPO_COMPETENCIA", "PERFIL")));
        //    }

        //    //LISTA DE EXPERIENCIA
        //    XElement pExperiencia = new XElement("EXPERIENCIA");

        //    foreach (E_EXPERIENCIA item in vListaExperiencia)
        //    {
        //        pExperiencia.Add(new XElement("EXP",
        //            new XAttribute("ID_AREA_INTERES", item.ID_AREA_INTERES),
        //            new XAttribute("NO_TIEMPO", item.NO_TIEMPO),
        //            new XAttribute("CL_NIVEL_REQUERIDO", item.CL_TIPO_EXPERIENCIA)));

        //    }

        //    //LISTA DE RELACION DE PUESTOS

        //    XElement pPuestosRel = new XElement("PUESTOS_REL");

        //    //SUBORDINADOS
        //    foreach (RadListBoxItem item in lstPuestosSubordinado.Items)
        //    {
        //        pPuestosRel.Add(new XElement("PUESTO_REL",
        //            new XAttribute("ID_PUESTO_REL", item.Value),
        //            new XAttribute("CL_TIPO_RELACION", E_PUESTO_RELACION.SUBORDINADO.ToString())));
        //    }

        //    //INTERRELACIONADOS
        //    foreach (RadListBoxItem item in lstPuestosInterrelacionados.Items)
        //    {
        //        pPuestosRel.Add(new XElement("PUESTO_REL",
        //            new XAttribute("ID_PUESTO_REL", item.Value),
        //            new XAttribute("CL_TIPO_RELACION", E_PUESTO_RELACION.INTERRELACIONADO.ToString())));
        //    }

        //    //RUTA ALTERNATIVA
        //    foreach (RadListBoxItem item in lstAlternativa.Items)
        //    {
        //        pPuestosRel.Add(new XElement("PUESTO_REL",
        //            new XAttribute("ID_PUESTO_REL", item.Value),
        //            new XAttribute("CL_TIPO_RELACION", E_PUESTO_RELACION.RUTAALTERNATIVA.ToString())));
        //    }

        //    //RUTA LATERAL
        //    foreach (RadListBoxItem item in lstLateral.Items)
        //    {
        //        pPuestosRel.Add(new XElement("PUESTO_REL",
        //            new XAttribute("ID_PUESTO_REL", item.Value),
        //            new XAttribute("CL_TIPO_RELACION", E_PUESTO_RELACION.RUTALATERAL.ToString())));
        //    }

        //    //JEFE INMEDIATO
        //    RadListBoxItem vItem = lstJefeInmediato.Items[0];

        //    if (!String.IsNullOrWhiteSpace(vItem.Value))
        //        pPuestosRel.Add(new XElement("PUESTO_REL",
        //            new XAttribute("ID_PUESTO_REL", vItem.Value),
        //            new XAttribute("CL_TIPO_RELACION", E_PUESTO_RELACION.JEFE.ToString())));

        //    XElement pFunGen = new XElement("FUNCIONES");

        //    foreach (E_FUNCION_GENERICA item in vFuncionesGenericas)
        //    {

        //        pFunGen.Add(new XElement("FUNCION",
        //            new XAttribute("ID_ITEM", item.ID_ITEM),
        //            new XAttribute("ID_PUESTO_FUNCION", item.ID_FUNCION_GENERICA),
        //            new XAttribute("NB_FUNCION", item.NB_FUNCION_GENERICA),
        //            new XAttribute("DS_FUNCION", item.NB_FUNCION_GENERICA),
        //            EditorContentToXml("DS_DETALLE", item.DS_DETALLE, vNbFirstRadEditorTagName),
        //            EditorContentToXml("DS_NOTAS", item.DS_NOTAS, vNbFirstRadEditorTagName)
        //        ));
        //    }

        //    XElement pFunComGen = new XElement("FUNCION_COMPETENCIAS");

        //    foreach (E_FUNCION_COMPETENCIA item in vLstFuncionCompetencia)
        //    {
        //        pFunComGen.Add(new XElement("FUNCION_COMPETENCIA",
        //            new XAttribute("ID_ITEM", item.ID_ITEM),
        //            new XAttribute("ID_PARENT_ITEM", item.ID_PARENT_ITEM),
        //            new XAttribute("ID_COMPETENCIA", item.ID_COMPETENCIA),
        //            new XAttribute("NO_NIVEL", item.NO_NIVEL),
        //            new XAttribute("CL_TIPO_COMPETENCIA", "FUNCIONES"),
        //            new XAttribute("ID_FUNCION", item.ID_FUNCION_GENERICA),
        //            new XAttribute("ID_FUNCION_COMPETENCIA", item.ID_FUNCION_COMPETENCIA)));
        //    }

        //    XElement pFunIndicadores = new XElement("FUNCION_INDICADORES");

        //    foreach (E_FUNCION_INDICADOR item in vLstFuncionCompetenciaIndicador)
        //    {
        //        pFunIndicadores.Add(new XElement("FUNCION_INDICADOR",
        //            new XAttribute("ID_ITEM", item.ID_ITEM),
        //            new XAttribute("ID_PARENT_ITEM", item.ID_PARENT_ITEM),
        //            new XAttribute("NB_INDICADOR", item.NB_INDICADOR_DESEMPENO),
        //            new XAttribute("ID_INDICADOR", item.ID_INDICADOR_DESEMPENO),
        //            new XAttribute("ID_FUNCION_INDICADOR", item.ID_FUNCION_INDICADOR),
        //            new XAttribute("ID_PUESTO_FUNCION", item.ID_FUNCION_GENERICA),
        //            new XAttribute("ID_PUESTO_COMPETENCIA", item.ID_FUNCION_COMPETENCIA)));
        //    }


        //    XElement pCompetenciasGenericas = new XElement("COMPETENCIAS_GEN");

        //    foreach (GridDataItem item in dgvCompetencias.Items)
        //    {
        //        E_COMPETENCIAS c = (E_COMPETENCIAS)item.DataItem;
        //        int NO_NIVEL;
        //        int ID_NIVEL_DESEADO = 0;
        //        string ID_NIVEL = "ID_NIVEL";

        //        NO_NIVEL = int.Parse((item.FindControl("rsNivel1") as RadSlider).Value.ToString());
        //        ID_NIVEL = ID_NIVEL + NO_NIVEL.ToString();
        //        ID_NIVEL_DESEADO = int.Parse(item.GetDataKeyValue(ID_NIVEL).ToString());

        //        pCompetenciasGenericas.Add(new XElement("COMPETENCIA_GEN",
        //            new XAttribute("ID_COMPETENCIA", item.GetDataKeyValue("ID_COMPETENCIA")),
        //            new XAttribute("ID_NIVEL_DESEADO", ID_NIVEL_DESEADO),
        //            new XAttribute("CL_TIPO_COMPETENCIA", "COMPETENCIA GENERICA")));
        //    }

        //    pPuesto.Add(pDescriptivo);
        //    pPuesto.Add(pEscolaridad);
        //    pPuesto.Add(pCompEsp);
        //    pPuesto.Add(pExperiencia);
        //    pPuesto.Add(pPuestosRel);
        //    pPuesto.Add(pFunGen);
        //    pPuesto.Add(pFunComGen);
        //    pPuesto.Add(pFunIndicadores);
        //    pPuesto.Add(pCompetenciasGenericas);
        //    //txtPruebaXml.Text = pPuesto.ToString();
        //    return pPuesto.ToString();

        //}

        //public void GuardarPuesto(bool pFgCerrarVentana)
        //{
        //    if (validarControles())
        //    {
        //        XElement vXmlCA = generalXmlAdicionales();

        //        if (vXmlCA != null)
        //        {
        //            string xml = generarXml(vXmlCA);
        //            string tran = "";
        //            PuestoNegocio neg = new PuestoNegocio();
        //            E_PUESTO vPuesto = new E_PUESTO();

        //            vPuesto.XML_PUESTO = xml;

        //            if (vIdDescriptivo == 0)
        //            {
        //                tran = "I";
        //                vPuesto.ID_PUESTO = null;
        //            }
        //            else
        //            {
        //                tran = "A";
        //                vPuesto.ID_PUESTO = vIdDescriptivo;
        //            }

        //            E_RESULTADO vResultado = neg.InsertaActualizaPuesto(tran, vPuesto, vClUsuario, "Descriptivo");
        //            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

        //            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL) && tran.Equals("I"))
        //            {
        //                XElement vDatosRespuesta = vResultado.ObtieneDatosRespuesta();
        //                XElement vPuestoInsertado = vDatosRespuesta.Element("PUESTO");
        //                vIdDescriptivo = UtilXML.ValorAtributo<int>(vPuestoInsertado.Attribute("ID_PUESTO"));
        //            }

        //            // Se cierra la ventana únicamente cuando el mensaje sea exitoso y se haya dado la instrucción por parámetro de cerrar la venana
        //            bool vCerrarVentana = (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL)) && pFgCerrarVentana;
        //            string vCallBackFunction = vCerrarVentana ? "closeWindow" : null;

        //            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: vCallBackFunction);
        //        }
        //    }
        //}

        private XElement EditorContentToXml(string pNbNodoRaiz, string pDsContenido, string pNbTag)
        {
            return XElement.Parse(EncapsularRadEditorContent(XElement.Parse(String.Format("<{1}>{0}</{1}>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(pDsContenido)), pNbNodoRaiz)), pNbNodoRaiz));
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

        private void agregarItemLista(RadListBox lista, RadComboBox combo)
        {
            RadListBoxItem item = new RadListBoxItem();
            item.Value = combo.SelectedValue;
            item.Text = combo.Text;

            lista.Items.Add(item);
        }

        //private bool validarControles()
        //{
        //    bool vAceptado = true;
        //    string vMensaje = "";

        //    if (txtNombreCorto.Text == "" & vAceptado)
        //    {
        //        vMensaje = "El campo clave es obligatorio";
        //        vAceptado = false;
        //    }

        //    if (txtDescripcionPuesto.Text == "" & vAceptado)
        //    {
        //        vMensaje = "El campo nombre es obligatorio";
        //        vAceptado = false;
        //    }

        //    if (txtRangoEdadMax.Text == "" & vAceptado)
        //    {
        //        vMensaje = "El rango de edad es obligatorio";
        //        vAceptado = false;
        //    }

        //    if (txtRangoEdadMin.Text == "" & vAceptado)
        //    {
        //        vMensaje = "El rango de edad es obligatorio";
        //        vAceptado = false;
        //    }

        //    if (cmbGenero.SelectedIndex == -1 & vAceptado)
        //    {
        //        vMensaje = "El género es obligatorio";
        //        vAceptado = false;
        //    }

        //    if (cmbEdoCivil.SelectedIndex == -1 & vAceptado)
        //    {
        //        vMensaje = "El estado civil es obligatorio";
        //        vAceptado = false;
        //    }

        //    //if (lstCompetenciasEspecificas.Items.Count == 0 & vAceptado)
        //    //{
        //    //    vMensaje = "Las competencias específicas del perfil de ingreso son obligatorias";
        //    //    vAceptado = false;
        //    //}

        //    //if (lstExperiencia.Items.Count == 0 & vAceptado)
        //    //{
        //    //    vMensaje = "La experiencia es obligatoria";
        //    //    vAceptado = false;
        //    //}

        //    if (radEditorRequerimientos.Content == "" & vAceptado)
        //    {
        //        vMensaje = "Los requerimientos son obligatorios";
        //        vAceptado = false;
        //    }

        //    if ((!btnDirecto.Checked & !btnIndirecto.Checked) & vAceptado)
        //    {
        //        vMensaje = "El tipo de puesto es obligatorio";
        //        vAceptado = false;
        //    }

        //    if (cmbAreas.SelectedIndex == -1 & vAceptado)
        //    {
        //        vMensaje = "El área es obligatoria";
        //        vAceptado = false;
        //    }

        //    //if (cmbAdministrativo.SelectedIndex == -1 & vAceptado)
        //    //{
        //    //    vMensaje = "El centro administrativo es obligatorio";
        //    //    vAceptado = false;
        //    //}

        //    //if (cmbOperativo.SelectedIndex == -1 & vAceptado)
        //    //{
        //    //    vMensaje = "El centro operativo es obligatorio";
        //    //    vAceptado = false;
        //    //}

        //    if ((!btnLinea.Checked & !btnStaff.Checked) & vAceptado)
        //    {
        //        vMensaje = "La posición en el organigrama es obligatoria";
        //        vAceptado = false;
        //    }

        //    if (radEditorResponsable.Content == "" & vAceptado)
        //    {
        //        vMensaje = "El campo \"Responsable de \" es obligatorio";
        //        vAceptado = false;
        //    }

        //    if (radEditorAutoridad.Content == "" & vAceptado)
        //    {
        //        vMensaje = "El campo \"Autoridad\" es obligatorio";
        //        vAceptado = false;
        //    }

        //    if (!vAceptado)
        //    {
        //        UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
        //    }

        //    return vAceptado;
        //}

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
                    //divCamposExtras.Controls.Add(new LiteralControl("<div style='clear:both;'></div>"));
                    //divCamposExtras.Controls.Add(vControlHTML);
                }
            }
        }

        //private XElement generalXmlAdicionales()
        //{
        //    XElement pXmlAdicionales = XElement.Parse(vXmlAdicionales);
        //    XElement pXmlValoresAdicionales = new XElement("CAMPOS");
        //    bool vFgAsignarXML = true;
        //    string vMensajes = String.Empty;

        //    foreach (XElement vXmlControl in pXmlAdicionales.Elements("CAMPO"))
        //    {
        //        string vClTipoControl = vXmlControl.Attribute("CL_TIPO").Value;
        //        string vIdControl = vXmlControl.Attribute("ID_CAMPO").Value;
        //        string vNbControl = vXmlControl.Attribute("NB_CAMPO").Value;
        //        string vNbValor = String.Empty;
        //        string vDsValor = "";


        //        bool vFgAsignarValor = true;
        //        Control vControl = pvwCamposExtras.FindControl(vIdControl);

        //        switch (vClTipoControl)
        //        {
        //            case "TEXTBOX":
        //                vNbValor = ((RadTextBox)vControl).Text;
        //                //vDsValor = ((RadTextBox)vControl).Text;

        //                if ((bool?)UtilXML.ValorAtributo(vXmlControl.Attribute("FG_REQUERIDO"), E_TIPO_DATO.BOOLEAN) ?? false)
        //                {
        //                    vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
        //                    vFgAsignarXML = vFgAsignarXML && vFgAsignarValor;
        //                    if (!vFgAsignarValor)
        //                        vMensajes += String.Format("El campo {0} es requerido.<br />", vNbControl);
        //                }
        //                break;
        //            case "MASKBOX":
        //                vNbValor = ((RadMaskedTextBox)vControl).Text;
        //                //vDsValor = ((RadMaskedTextBox)vControl).Text;
        //                if ((bool?)UtilXML.ValorAtributo(vXmlControl.Attribute("FG_REQUERIDO"), E_TIPO_DATO.BOOLEAN) ?? false)
        //                {
        //                    vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
        //                    vFgAsignarXML = vFgAsignarXML && vFgAsignarValor;
        //                    if (!vFgAsignarValor)
        //                        vMensajes += String.Format("El campo {0} es requerido.<br />", vNbControl);
        //                }
        //                break;
        //            case "DATEPICKER":
        //                DateTime vFecha = ((RadDatePicker)vControl).SelectedDate ?? DateTime.Now;
        //                vNbValor = vFecha.ToString("dd/MM/yyyy");
        //                //vDsValor = vFecha.ToString("dd/MM/yyyy");
        //                if ((bool?)UtilXML.ValorAtributo(vXmlControl.Attribute("FG_REQUERIDO"), E_TIPO_DATO.BOOLEAN) ?? false)
        //                {
        //                    vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
        //                    vFgAsignarXML = vFgAsignarXML && vFgAsignarValor;
        //                    if (!vFgAsignarValor)
        //                        vMensajes += String.Format("El campo {0} es requerido.<br />", vNbControl);
        //                }
        //                break;
        //            case "COMBOBOX":
        //                vNbValor = ((RadComboBox)vControl).SelectedValue;
        //                //vDsValor = ((RadComboBox)vControl).Text;
        //                if ((bool?)UtilXML.ValorAtributo(vXmlControl.Attribute("FG_REQUERIDO"), E_TIPO_DATO.BOOLEAN) ?? false)
        //                {
        //                    vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
        //                    vFgAsignarXML = vFgAsignarXML && vFgAsignarValor;
        //                    if (!vFgAsignarValor)
        //                        vMensajes += String.Format("El campo {0} es requerido.<br />", vNbControl);
        //                }
        //                break;
        //            case "LISTBOX":
        //                RadListBox vRadListBox = ((RadListBox)vControl);
        //                string vClValor = String.Empty;

        //                foreach (RadListBoxItem item in vRadListBox.SelectedItems)
        //                {
        //                    vNbValor = item.Value;
        //                    vDsValor = item.Text;
        //                }

        //                if ((bool?)UtilXML.ValorAtributo(vXmlControl.Attribute("FG_REQUERIDO"), E_TIPO_DATO.BOOLEAN) ?? false)
        //                {
        //                    vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
        //                    vFgAsignarXML = vFgAsignarXML && vFgAsignarValor;
        //                    if (!vFgAsignarValor)
        //                        vMensajes += String.Format("El campo {0} es requerido.<br />", vNbControl);
        //                }
        //                break;
        //            default:
        //                vFgAsignarValor = false;
        //                break;
        //        }
        //        if (vFgAsignarValor)
        //        {
        //            XElement xXmlCampo = new XElement("CAMPO");

        //            xXmlCampo.Add(new XAttribute("ID_CAMPO", vIdControl), new XAttribute("NO_VALOR", vNbValor), new XAttribute("DS_VALOR", vDsValor));
        //            pXmlValoresAdicionales.Add(xXmlCampo);
        //        }
        //    }
        //    if (vFgAsignarXML)
        //        return pXmlValoresAdicionales;
        //    else
        //    {
        //        UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensajes, E_TIPO_RESPUESTA_DB.WARNING);
        //        return null;
        //    }
        //}

        private void asignarValoresAdicionales(string pXmlvalores)
        {
            /*
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
                    //Control vControl = divCamposExtras.FindControl(vIdControl);

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
              */
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            //CampoAdicionalNegocio neg = new CampoAdicionalNegocio();
            //CrearFormulario(XElement.Parse(neg.obtieneCampoAdicionalXml("M_PUESTO")));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = (ContextoUsuario.oUsuario != null) ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO";
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                vListaExperiencia = new List<E_EXPERIENCIA>();

                //if (!ContextoApp.FgControlDocumentos)
                //{
                //    slzOpciones.Visible = false;
                //    rpnOpciones.Visible = false;
                //}


                //Verificar si se va realizar es una insercion o modificacion
                int idDescriptivo = 0;
                if (int.TryParse((string)Request.QueryString["PuestoId"], out idDescriptivo))
                {
                    vIdDescriptivo = idDescriptivo;
                }

                vIsCopy = (Request.QueryString["pIsCopy"] != null);

                CargarDatos(vIdDescriptivo);

                ObtenerEstadosCiviles();
                ObtenerGeneros();


                //LLENAR LAS ESCOLARIDADES
                //ObtenerEscolaridad(E_CL_TIPO_ESCOLARIDAD.POSTGRADO.ToString(), radcmbPostgrados);
                //ObtenerEscolaridad(E_CL_TIPO_ESCOLARIDAD.PROFESIONAL.ToString(), cmbCarreraProf);
                //ObtenerEscolaridad(E_CL_TIPO_ESCOLARIDAD.TECNICA.ToString(), cmbCarrTec);

                ObtenerCompetenciasEspecificas();
                ObtenerAreaInteres();

                //ObtenerAreas();

                //ObtenerCentroAdmvo();
                //ObtenerCentroOptvo();

                //ObtenerPuestos(E_PUESTO_RELACION.SUBORDINADO.ToString(), cmbPuestosSubordinado);
                //ObtenerPuestos(E_PUESTO_RELACION.INTERRELACIONADO.ToString(), cmbPuestosInterrelacionados);
                //ObtenerPuestos(E_PUESTO_RELACION.RUTAALTERNATIVA.ToString(), cmbAlternativa);
                //ObtenerPuestos(E_PUESTO_RELACION.RUTALATERAL.ToString(), cmbLateral);

                lstExperiencia.DataSource = vListaExperiencia;
                lstExperiencia.DataValueField = "ID_AREA_INTERES";
                lstExperiencia.DataTextField = "NB_AREA_INTERES";
                lstExperiencia.DataBind();

                MnsAutoridadPoliticaIntegral.Visible = ContextoApp.ADM.fgVisibleMensajes;
                MnsAutoridad.Visible = ContextoApp.ADM.fgVisibleMensajes;
                lblPoliticaIntegral.Visible = ContextoApp.ADM.fgVisibleMensajes;

                if (ContextoApp.ADM.fgVisibleMensajes)
                {
                    MnsAutoridadPoliticaIntegral.Text = ContextoApp.ADM.AutoridadPoliticaIntegral.dsMensaje;
                }

            }

            //winFuncionesGenericas.VisibleOnPageLoad = false;
           
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
            //agregarItemLista(lstPostgrados, radcmbPostgrados);
        }

        protected void btnAgregarCarreProfe_Click(object sender, EventArgs e)
        {
            //agregarItemLista(lstCarreraprof, cmbCarreraProf);
        }

        protected void radbtnAgregarCarreraTec_Click(object sender, EventArgs e)
        {
            //agregarItemLista(lstCarreraTec, cmbCarrTec);
        }

        protected void btnCompEsp_Click(object sender, EventArgs e)
        {
            //agregarItemLista(lstCompetenciasEspecificas, cmbCompetenciaEspecificas);
        }

        protected void radBtnAgregarExperiencia_Click(object sender, EventArgs e)
        {
            //E_EXPERIENCIA ex = new E_EXPERIENCIA();
            //ex.ID_AREA_INTERES = int.Parse(cmbExperiencias.SelectedValue);
            //ex.NB_AREA_INTERES = cmbExperiencias.Text;
            //ex.NO_TIEMPO = int.Parse(txtTiempo.Text);

            //ex.CL_TIPO_EXPERIENCIA = btnRequerida.Checked ? "Requerida" : "Deseada";

            //vListaExperiencia.Add(ex);
            //lstExperiencia.DataSource = vListaExperiencia;
            //lstExperiencia.DataBind();
            //divContenido.Style.Add("display", "none");

        }

        protected void radBtnAgregarPuestoSubordinado_Click(object sender, EventArgs e)
        {
            //agregarItemLista(lstPuestosSubordinado, cmbPuestosSubordinado);
        }

        protected void btnInter_Click(object sender, EventArgs e)
        {
            //agregarItemLista(lstPuestosInterrelacionados, cmbPuestosInterrelacionados);
        }

        protected void btnRutaAlter_Click(object sender, EventArgs e)
        {
            //agregarItemLista(lstAlternativa, cmbAlternativa);
        }

        protected void btnLateral_Click(object sender, EventArgs e)
        {
            //agregarItemLista(lstLateral, cmbLateral);
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
            //CargarFormularioFuncionesGenericas();
        }

        protected void btnEditarFuncionGenerica_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grdFuncionesGenericas.SelectedItems)
            {
                Guid vIdItemFuncionGenerica = new Guid(item.GetDataKeyValue("ID_ITEM").ToString());
                vFuncionGenericaABC = vFuncionesGenericas.FirstOrDefault(f => f.ID_ITEM.Equals(vIdItemFuncionGenerica));
                vLstFuncionCompetenciaABC = vLstFuncionCompetencia.Where(w => w.ID_PARENT_ITEM.Equals(vIdItemFuncionGenerica)).ToList();
                vLstFuncionCompetenciaIndicadorABC = vLstFuncionCompetenciaIndicador.Where(w => vLstFuncionCompetenciaABC.Any(a => a.ID_ITEM.Equals(w.ID_PARENT_ITEM))).ToList();
                //CargarFormularioFuncionesGenericas();
            }
        }

        //protected void CargarFormularioFuncionesGenericas()
        //{
        //    txtNbFuncion.Text = vFuncionGenericaABC.NB_FUNCION_GENERICA;
        //    txtDetalleFuncion.Content = vFuncionGenericaABC.DS_DETALLE;
        //    txtNotasFuncion.Content = vFuncionGenericaABC.DS_NOTAS;
        //    cmbCompetenciaEspecifica.ClearSelection();
        //    rtsFuncionGenerica.SelectedIndex = 0;
        //    rmpFuncionGenerica.SelectedIndex = 0;
        //    txtIndicadorDesempeno.Text = String.Empty;

        //    grdFuncionCompetencias.Rebind();
        //    winFuncionesGenericas.VisibleOnPageLoad = true;
        //}

        //protected void grdFuncionCompetencias_ItemCommand(object sender, GridCommandEventArgs e)
        //{
        //    winFuncionesGenericas.VisibleOnPageLoad = true;
        //}

        //protected void grdFuncionCompetencias_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        //{
        //    GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
        //    Guid CustomerID = new Guid(dataItem.GetDataKeyValue("ID_ITEM").ToString());
        //    e.DetailTableView.DataSource = vLstFuncionCompetenciaIndicadorABC.Where(w => w.ID_PARENT_ITEM.Equals(CustomerID));
        //}

        //protected void cmbCompetenciaEspecifica_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    CargarNivelesCompetencias();
        //}

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

        //protected void CargarNivelesCompetencias()
        //{
        //    List<E_NIVEL_COMPETENCIA> vLstNivelesCompetencia = new List<E_NIVEL_COMPETENCIA>();
        //    int vIdCompenteciaSeleccionada = 0;
        //    if (int.TryParse(cmbCompetenciaEspecifica.SelectedValue, out vIdCompenteciaSeleccionada))
        //    {
        //        E_COMPETENCIAS competencia = vListaCompetencias.FirstOrDefault(f => f.ID_COMPETENCIA.Equals(vIdCompenteciaSeleccionada));
        //        if (competencia == null)
        //            competencia = new E_COMPETENCIAS();

        //        vLstNivelesCompetencia.AddRange(CrearNivelCompetencia(competencia));

        //        grdNivelCompetenciaEspecifica.DataSource = vLstNivelesCompetencia;
        //    }
        //    grdNivelCompetenciaEspecifica.DataBind();
        //    btnAgregarCompetenciaEspecifica.Enabled = true;
        //}

        //protected void grdFuncionCompetencias_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    grdFuncionCompetencias.DataSource = vLstFuncionCompetenciaABC;
        //}

        //protected void btnEliminarCompetencia_Click(object sender, EventArgs e)
        //{
        //    bool vFgRebindGrid = false;
        //    foreach (GridDataItem item in grdFuncionCompetencias.SelectedItems)
        //    {
        //        vLstFuncionCompetenciaIndicadorABC.RemoveAll(r => r.ID_PARENT_ITEM.Equals(new Guid(item.GetDataKeyValue("ID_ITEM").ToString())));
        //        vLstFuncionCompetenciaABC.RemoveAll(r => r.ID_ITEM.Equals(new Guid(item.GetDataKeyValue("ID_ITEM").ToString())));
        //        vFgRebindGrid = true | vFgRebindGrid;
        //    }

        //    if (vFgRebindGrid)
        //        grdFuncionCompetencias.Rebind();
        //}

        //protected void btnAgregarCompetenciaEspecifica_Click(object sender, EventArgs e)
        //{
        //    int vNoNivel = 0;
        //    bool vFgRebindGrid = false;
        //    foreach (GridDataItem item in grdNivelCompetenciaEspecifica.SelectedItems)
        //    {
        //        if (int.TryParse(item.GetDataKeyValue("NO_VALOR").ToString(), out vNoNivel))
        //        {
        //            E_FUNCION_COMPETENCIA vCompetencia = vLstFuncionCompetenciaABC.FirstOrDefault(f => f.ID_ITEM == (vIdEditingItem ?? Guid.NewGuid()));

        //            if (vCompetencia == null)
        //            {
        //                vLstFuncionCompetenciaABC.Add(new E_FUNCION_COMPETENCIA()
        //                {
        //                    ID_PARENT_ITEM = vFuncionGenericaABC.ID_ITEM,
        //                    NB_COMPETENCIA = cmbCompetenciaEspecifica.SelectedItem.Text,
        //                    ID_COMPETENCIA = int.Parse(cmbCompetenciaEspecifica.SelectedItem.Value),
        //                    NO_NIVEL = vNoNivel,
        //                    NB_NIVEL = item["NB_NIVEL"].Text
        //                });
        //            }
        //            else
        //            {
        //                vCompetencia.NB_COMPETENCIA = cmbCompetenciaEspecifica.SelectedItem.Text;
        //                vCompetencia.ID_COMPETENCIA = int.Parse(cmbCompetenciaEspecifica.SelectedItem.Value);
        //                vCompetencia.NO_NIVEL = vNoNivel;
        //                vCompetencia.NB_NIVEL = item["NB_NIVEL"].Text;
        //            }
        //            vFgRebindGrid = true | vFgRebindGrid;
        //        }
        //    }

        //    if (vFgRebindGrid)
        //        grdFuncionCompetencias.Rebind();
        //}

        //protected void btnAgregarIndicadorDesempeno_Click(object sender, EventArgs e)
        //{
        //    bool vFgRebindGrid = false;
        //    foreach (GridDataItem item in grdFuncionCompetencias.SelectedItems)
        //    {
        //        E_FUNCION_INDICADOR vIndicador = vLstFuncionCompetenciaIndicadorABC.FirstOrDefault(f => f.ID_ITEM == (vIdEditingItem ?? Guid.NewGuid()));

        //        Guid vIdCompetencia = new Guid(item.GetDataKeyValue("ID_ITEM").ToString());

        //        if (vIndicador == null)
        //        {
        //            vLstFuncionCompetenciaIndicadorABC.Add(new E_FUNCION_INDICADOR()
        //            {
        //                ID_PARENT_ITEM = vIdCompetencia,
        //                NB_INDICADOR_DESEMPENO = txtIndicadorDesempeno.Text
        //            });
        //        }
        //        else
        //        {
        //            vIdCompetencia = vIndicador.ID_PARENT_ITEM;
        //            vIndicador.NB_INDICADOR_DESEMPENO = txtIndicadorDesempeno.Text;
        //        }

        //        CrearDsIndicadores(vLstFuncionCompetenciaABC.FirstOrDefault(f => f.ID_ITEM.Equals(vIdCompetencia)), vLstFuncionCompetenciaIndicadorABC);
        //        vFgRebindGrid = true | vFgRebindGrid;
        //    }

        //    if (vFgRebindGrid)
        //        grdFuncionCompetencias.Rebind();
        //}

        //protected void btnEliminarIndicador_Click(object sender, EventArgs e)
        //{
        //    bool vFgRebindGrid = false;
        //    foreach (GridDataItem item in grdFuncionCompetencias.SelectedItems)
        //    {
        //        Guid vIdIndicador = new Guid(item.GetDataKeyValue("ID_ITEM").ToString());
        //        Guid vIdCompetencia = (vLstFuncionCompetenciaIndicadorABC.FirstOrDefault(f => f.ID_ITEM.Equals(vIdIndicador)) ?? new E_FUNCION_INDICADOR()).ID_PARENT_ITEM;
        //        vLstFuncionCompetenciaIndicadorABC.RemoveAll(r => r.ID_ITEM.Equals(vIdIndicador));
        //        CrearDsIndicadores(vLstFuncionCompetenciaABC.FirstOrDefault(f => f.ID_ITEM.Equals(vIdCompetencia)), vLstFuncionCompetenciaIndicadorABC);
        //        vFgRebindGrid = true | vFgRebindGrid;
        //    }

        //    if (vFgRebindGrid)
        //        grdFuncionCompetencias.Rebind();
        //}

        protected void CrearDsIndicadores(E_FUNCION_COMPETENCIA pFuncionCompetencia, List<E_FUNCION_INDICADOR> pLstIndicadores)
        {
            if (pFuncionCompetencia != null)
                pFuncionCompetencia.DS_INDICADORES = new XElement("ul", pLstIndicadores.Where(w => w.ID_PARENT_ITEM.Equals(pFuncionCompetencia.ID_ITEM)).Select(s => new XElement("li", s.NB_INDICADOR_DESEMPENO))).ToString();
        }

        //protected void btnGuardarFuncionGenerica_Click(object sender, EventArgs e)
        //{
        //    vFuncionGenericaABC.NB_FUNCION_GENERICA = txtNbFuncion.Text;
        //    vFuncionGenericaABC.DS_DETALLE = txtDetalleFuncion.Content;
        //    vFuncionGenericaABC.DS_NOTAS = txtNotasFuncion.Content;

        //    if (vFuncionesGenericas == null)
        //        vFuncionesGenericas = new List<E_FUNCION_GENERICA>();

        //    if (vLstFuncionCompetencia == null)
        //        vLstFuncionCompetencia = new List<E_FUNCION_COMPETENCIA>();

        //    if (vLstFuncionCompetenciaIndicador == null)
        //        vLstFuncionCompetenciaIndicador = new List<E_FUNCION_INDICADOR>();

        //    E_FUNCION_GENERICA vFuncionGenerica = vFuncionesGenericas.FirstOrDefault(f => f.ID_ITEM.Equals(vFuncionGenericaABC.ID_ITEM));

        //    if (vFuncionGenerica == null)
        //        vFuncionesGenericas.Add(vFuncionGenericaABC);
        //    else
        //    {
        //        vFuncionGenerica.NB_FUNCION_GENERICA = vFuncionGenericaABC.NB_FUNCION_GENERICA;
        //        vFuncionGenerica.DS_DETALLE = vFuncionGenericaABC.DS_DETALLE;
        //        vFuncionGenerica.DS_NOTAS = vFuncionGenericaABC.DS_NOTAS;
        //    }


        //    foreach (E_FUNCION_COMPETENCIA vCompetenciaABC in vLstFuncionCompetenciaABC)
        //    {
        //        E_FUNCION_COMPETENCIA vCompetencia = vLstFuncionCompetencia.FirstOrDefault(f => f.ID_ITEM.Equals(vCompetenciaABC.ID_ITEM));

        //        if (vCompetencia == null)
        //            vLstFuncionCompetencia.Add(vCompetenciaABC);
        //        else
        //        {
        //            vCompetencia.NB_COMPETENCIA = vCompetenciaABC.NB_COMPETENCIA;
        //            vCompetencia.NO_NIVEL = vCompetenciaABC.NO_NIVEL;
        //            vCompetencia.NB_NIVEL = vCompetenciaABC.NB_NIVEL;
        //            vCompetencia.DS_INDICADORES = vCompetenciaABC.DS_INDICADORES;
        //        }
        //    }

        //    foreach (E_FUNCION_INDICADOR vIndicadorABC in vLstFuncionCompetenciaIndicadorABC)
        //    {
        //        E_FUNCION_INDICADOR vIndicador = vLstFuncionCompetenciaIndicador.FirstOrDefault(f => f.ID_ITEM.Equals(vIndicadorABC.ID_ITEM));

        //        if (vIndicador == null)
        //            vLstFuncionCompetenciaIndicador.Add(vIndicadorABC);
        //        else
        //        {
        //            vIndicador.NB_INDICADOR_DESEMPENO = vIndicadorABC.NB_INDICADOR_DESEMPENO;
        //        }
        //    }

        //    List<E_FUNCION_INDICADOR> vLstIndicadoresToRemove = new List<E_FUNCION_INDICADOR>();
        //    List<E_FUNCION_COMPETENCIA> vLstCompetenciasToRemove = new List<E_FUNCION_COMPETENCIA>();
        //    foreach (E_FUNCION_COMPETENCIA competencia in vLstFuncionCompetencia.Where(w => w.ID_PARENT_ITEM.Equals(vFuncionGenericaABC.ID_ITEM)))
        //    {
        //        if (!vLstFuncionCompetenciaABC.Any(a => a.ID_ITEM.Equals(competencia.ID_ITEM)))
        //            vLstCompetenciasToRemove.Add(competencia);

        //        foreach (E_FUNCION_INDICADOR indicador in vLstFuncionCompetenciaIndicador.Where(w => w.ID_PARENT_ITEM.Equals(competencia.ID_ITEM)))
        //            if (!vLstFuncionCompetenciaIndicadorABC.Any(a => a.ID_ITEM.Equals(indicador.ID_ITEM)))
        //                vLstIndicadoresToRemove.Add(indicador);
        //    }

        //    vLstFuncionCompetenciaIndicador.RemoveAll(r => vLstIndicadoresToRemove.Any(a => a.ID_ITEM.Equals(r.ID_ITEM)));
        //    vLstFuncionCompetencia.RemoveAll(r => vLstCompetenciasToRemove.Any(a => a.ID_ITEM.Equals(r.ID_ITEM)));

        //    grdFuncionesGenericas.Rebind();
        //}

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

        //protected void btnEditarIndicador_Click(object sender, EventArgs e)
        //{
        //    foreach (GridDataItem item in grdFuncionCompetencias.SelectedItems)
        //    {
        //        vIdEditingItem = Guid.Parse(item.GetDataKeyValue("ID_ITEM").ToString());
        //        E_FUNCION_INDICADOR indicador = vLstFuncionCompetenciaIndicadorABC.FirstOrDefault(f => f.ID_ITEM.Equals(vIdEditingItem));
        //        if (indicador == null)
        //            indicador = new E_FUNCION_INDICADOR();

        //        txtIndicadorDesempeno.Text = indicador.NB_INDICADOR_DESEMPENO;
        //    }
        //}

        //protected void btnEditarCompetencia_Click(object sender, EventArgs e)
        //{
        //    foreach (GridDataItem item in grdFuncionCompetencias.SelectedItems)
        //    {
        //        vIdEditingItem = Guid.Parse(item.GetDataKeyValue("ID_ITEM").ToString());
        //        E_FUNCION_COMPETENCIA competencia = vLstFuncionCompetenciaABC.FirstOrDefault(f => f.ID_ITEM.Equals(vIdEditingItem));
        //        if (competencia == null)
        //            competencia = new E_FUNCION_COMPETENCIA();

        //        cmbCompetenciaEspecifica.SelectedValue = competencia.ID_COMPETENCIA.ToString();
        //        CargarNivelesCompetencias();
        //    }
        //}

        //protected void grdNivelCompetenciaEspecifica_PreRender(object sender, EventArgs e)
        //{
        //    if (vLstFuncionCompetenciaABC != null)
        //    {
        //        E_FUNCION_COMPETENCIA competencia = vLstFuncionCompetenciaABC.FirstOrDefault(f => f.ID_ITEM.Equals(vIdEditingItem));
        //        if (competencia != null)
        //        {
        //            GridDataItem selectItem = grdNivelCompetenciaEspecifica.MasterTableView.FindItemByKeyValue("NO_VALOR", competencia.NO_NIVEL);
        //            if (selectItem != null)
        //                selectItem.Selected = true;
        //        }
        //    }
        //}

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //GuardarPuesto(false);
        }

        protected void btnGuardarCerrar_Click(object sender, EventArgs e)
        {
            //GuardarPuesto(true);
        }
    }
}