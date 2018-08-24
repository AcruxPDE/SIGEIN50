using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Negocio.EvaluacionOrganizacional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using WebApp.Comunes;
using System.Xml;
using System.Web.UI.HtmlControls;
using SIGE.Entidades;

namespace SIGE.WebApp.EO.Cuestionarios
{
    public partial class CuestionarioClimaLaboralConfidencial : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        public string cssModulo = String.Empty;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_vIdPeriodo"]; }
            set { ViewState["vs_vIdPeriodo"] = value; }
        }

        public int vIdEvaluado
        {
            get { return (int)ViewState["vs_vIdEvaluado"]; }
            set { ViewState["vs_vIdEvaluado"] = value; }
        }

        public string xmlRespuestasCuestionario
        {
            get { return (string)ViewState["vs_xmlRespuestasCuestionario"]; }
            set { ViewState["vs_xmlRespuestasCuestionario"] = value; }
        }

        public List<E_RESPUESTA> vlstRespuestas
        {
            get { return (List<E_RESPUESTA>)ViewState["vs_vlstRespuestas"]; }
            set { ViewState["vs_vlstRespuestas"] = value; }
        }

        public string xmlPreguntasAbiertas
        {
            get { return (string)ViewState["vs_xmlPreguntasAbiertas"]; }
            set { ViewState["vs_xmlPreguntasAbiertas"] = value; }
        }

        public string xmlDatosEvaluador
        {
            get { return (string)ViewState["vs_xmlDatosEvaluador"]; }
            set { ViewState["vs_xmlDatosEvaluador"] = value; }
        }

        public string xmlDatosAdicionales
        {
            get { return (string)ViewState["vs_xmlDatosAdicionales"]; }
            set { ViewState["vs_xmlDatosAdicionales"] = value; }
        }

        public List<E_PREGUNTAS_CUESTIONARIO_CLIMA> vlstCuestionarios
        {
            get { return (List<E_PREGUNTAS_CUESTIONARIO_CLIMA>)ViewState["vs_vlstCuestionarios"]; }
            set { ViewState["vs_vlstCuestionarios"] = value; }
        }

        public List<E_RESPUESTA_PREGUNTAS_ABIERTAS> vlstPreguntasAbiertas
        {
            get { return (List<E_RESPUESTA_PREGUNTAS_ABIERTAS>)ViewState["vs_vlstPreguntasAbiertas"]; }
            set { ViewState["vs_vlstPreguntasAbiertas"] = value; }
        }

        public List<E_PREGUNTAS_CUESTIONARIO_CLIMA> lstCuestionario
        {
            get { return (List<E_PREGUNTAS_CUESTIONARIO_CLIMA>)ViewState["vs_lstCuestionario"]; }
            set { ViewState["vs_lstCuestionario"] = value; }
        }

        private List<E_ADICIONALES_SELECCIONADOS> vLstAdicionales
        {
            get { return (List<E_ADICIONALES_SELECCIONADOS>)ViewState["vs_vLstAdicionales"]; }
            set { ViewState["vs_vLstAdicionales"] = value; }
        }

        private List<E_ADICIONALES_SELECCIONADOS> vLstValorAdicional
        {
            get { return (List<E_ADICIONALES_SELECCIONADOS>)ViewState["vs_vLstValorAdicional"]; }
            set { ViewState["vs_vLstValorAdicional"] = value; }
        }

        public List<E_CAMPOS_ADICIONALES> vLstCamposAdicionales
        {
            get { return (List<E_CAMPOS_ADICIONALES>)ViewState["vs_vLstCamposAdicionales"]; }
            set { ViewState["vs_vLstCamposAdicionales"] = value; }
        }

        public bool vFgHabilitado
        {
            get { return (bool)ViewState["vs_fg_habilitado"]; }
            set { ViewState["vs_fg_habilitado"] = value; }
        }

        private List<E_DEPARTAMENTOS> vLstDepartamentos
        {
            get { return (List<E_DEPARTAMENTOS>)ViewState["vs_vLstDepartamentos"]; }
            set { ViewState["vs_vLstDepartamentos"] = value; }
        }

        #endregion

        #region Metodos

        protected void ValorRespuestas()
        {
            int vIdPregunta;
            decimal vNoRespuesta = 0;
            vlstRespuestas = new List<E_RESPUESTA>();
            vlstPreguntasAbiertas = new List<E_RESPUESTA_PREGUNTAS_ABIERTAS>();
            bool vFgPrimer = false;

            foreach (GridDataItem item in rgCuestionario.MasterTableView.Items)
            {
                vIdPregunta = int.Parse(item.GetDataKeyValue("ID_CUESTIONARIO_PREGUNTA").ToString());
                RadButton rbTotalmenteAcuerdo = (RadButton)item.FindControl("rbTotalmenteAcuerdo");
                RadButton rbCasiAcuerdo = (RadButton)item.FindControl("rbCasiAcuerdo");
                RadButton rbCasiDesacuerdo = (RadButton)item.FindControl("rbCasiDesacuerdo");
                RadButton rbTotalmenteDesacuerdo = (RadButton)item.FindControl("rbTotalmenteDesacuerdo");

                if (rbTotalmenteAcuerdo.Checked == true)
                    vNoRespuesta = 4;
                else if (rbCasiAcuerdo.Checked == true)
                    vNoRespuesta = 3;
                else if (rbCasiDesacuerdo.Checked == true)
                    vNoRespuesta = 2;
                else if (rbTotalmenteDesacuerdo.Checked == true)
                    vNoRespuesta = 1;
                else { vNoRespuesta = 0; }

                if (vNoRespuesta != 0)
                {
                    vlstRespuestas.Add(new E_RESPUESTA { ID_PREGUNTA_RESPUESTA = vIdPregunta, NO_VALOR = vNoRespuesta });
                    item.BackColor = System.Drawing.Color.White;
                }
                else
                {
                    item.BackColor = System.Drawing.Color.Gold;
                    if (vFgPrimer == false)
                    {
                        rbTotalmenteAcuerdo.Focus();
                        rbTotalmenteAcuerdo.BorderWidth = 2;
                        vFgPrimer = true;
                    }
                }
            }

            var vXelements = vlstRespuestas.Select(x =>
                                         new XElement("RESPUESTA",
                                         new XAttribute("ID_CUESTIONARIO_PREGUNTA", x.ID_PREGUNTA_RESPUESTA),
                                         new XAttribute("NO_VALOR", x.NO_VALOR)
                              ));
            XElement vXmlRespuestas = new XElement("RESPUESTAS", vXelements);
            xmlRespuestasCuestionario = vXmlRespuestas.ToString();

            foreach (GridDataItem item in rgPreguntasAbiertas.MasterTableView.Items)
            {
                vIdPregunta = int.Parse(item.GetDataKeyValue("ID_PREGUNTA").ToString());
                RadTextBox txtRespuesta = (RadTextBox)item.FindControl("txtRespuesta");
                if (txtRespuesta != null)
                    vlstPreguntasAbiertas.Add(new E_RESPUESTA_PREGUNTAS_ABIERTAS { ID_PREGUNTA_RESPUESTA = vIdPregunta, NB_RESPUESTA = txtRespuesta.Text });
            }

            var vXelementos = vlstPreguntasAbiertas.Select(x =>
                                                           new XElement("RESPUESTA",
                                                               new XAttribute("ID_PREGUNTA", x.ID_PREGUNTA_RESPUESTA),
                                                               new XAttribute("RESPUESTA", x.NB_RESPUESTA)));
            XElement vXmlPreguntasAbiertas = new XElement("RESPUESTAS", vXelementos);

            xmlPreguntasAbiertas = vXmlPreguntasAbiertas.ToString();
        }

        protected bool DatosEvaluador()
        {
            bool vValidacion = true;
            decimal? vEdad = null;
            int? vAntiguedad = null;
            string vClGenero = "";
            int? vIdDepartamento = null;
            string vNbDepartamento = "";

            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            int countFiltros = nClima.ObtenerFiltrosEvaluadores(vIdPeriodo).Count;
            if (countFiltros > 0)
            {
                var vFiltros = nClima.ObtenerParametrosFiltros(vIdPeriodo).FirstOrDefault();
                if (vFiltros != null)
                {
                    if (vFiltros.EDAD_INICIO != null)
                    {
                        vEdad = decimal.Parse(rntEdad.Text);
                    }
                    if (vFiltros.ANTIGUEDAD_INICIO != null)
                    {
                        if (rdpIngreso.SelectedDate == null)
                        {
                            UtilMensajes.MensajeResultadoDB(rwmMensaje, "Ingrese la fecha de ingreso.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
                            vValidacion = false;
                            return vValidacion;
                        }
                        else
                        {
                            DateTime vFechaActual = DateTime.Now;
                            DateTime vFechaIngreso = (DateTime)rdpIngreso.SelectedDate;
                            vAntiguedad = (int)((vFechaActual - vFechaIngreso).TotalDays);
                            // vAntiguedad = decimal.Parse(rntAntiguedad.Text);
                        }
                    }
                    if (vFiltros.CL_GENERO != null)
                    {
                        if (cmbGenero.SelectedValue == "")
                        {
                            UtilMensajes.MensajeResultadoDB(rwmMensaje, "Ingrese el género.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
                            vValidacion = false;
                            return vValidacion;
                        }
                        else
                        {
                            if (cmbGenero.SelectedValue == "Masculino")
                                vClGenero = "M";
                            else if (cmbGenero.SelectedValue == "Femenino")
                                vClGenero = "F";
                            else
                                vClGenero = cmbGenero.SelectedValue;
                        }
                    }
                    if (vFiltros.XML_DEPARTAMENTOS != null)
                    {
                        if (rcbArea.SelectedValue == "")
                        {
                            UtilMensajes.MensajeResultadoDB(rwmMensaje, "Ingrese el área.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
                            vValidacion = false;
                            return vValidacion;
                        }
                        else
                        vIdDepartamento = int.Parse(rcbArea.SelectedValue) ;
                        vNbDepartamento = rcbArea.Text;
                    }

                    XElement vXmlDatosEvaluador = new XElement("EVALUADOR", 
                                                              new XElement("DATOS",
                                                              new XAttribute("ID_DEPARTAMENTO", vIdDepartamento == null? "": vIdDepartamento.ToString()),
                                                              new XAttribute("NB_DEPARTAMENTO", vNbDepartamento),
                                                              new XAttribute("CL_GENERO", vClGenero),
                                                              new XAttribute("NO_EDAD", vEdad == null? "": vEdad.ToString()),
                                                              new XAttribute("NO_ANTIGUEDAD_EMPRESA", vAntiguedad == null? "":vAntiguedad.ToString())
                                                              ));
                    xmlDatosEvaluador = vXmlDatosEvaluador.ToString();

                    XElement vXmlCamposExtra= new XElement("CAMPOS");
                    if (vFiltros.XML_CAMPOS_ADICIONALES != null)
                    {
                                   RotacionPersonalNegocio negocio = new RotacionPersonalNegocio();
                      foreach (E_CAMPOS_ADICIONALES item in vLstCamposAdicionales)
                      {
                          XElement xXmlCampo = new XElement("CAMPO");
                          var ListaAdscripcion = negocio.ObtieneCatalogoAdscripciones(item.ID_CATALOGO_LISTA).FirstOrDefault();
                          Control vControl = dvCamposExtra.FindControl(ListaAdscripcion.CL_CAMPO);
                          if (vControl != null)
                          {
                              if (((RadComboBox)vControl).SelectedValue == "")
                              {
                                  UtilMensajes.MensajeResultadoDB(rwmMensaje, "Ingrese " + ListaAdscripcion.NB_CAMPO, E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
                                  vValidacion = false;
                                  return vValidacion;
                              }
                              else
                              {
                              string vNbValor = ((RadComboBox)vControl).SelectedValue;
                              string vNbTexto = ((RadComboBox)vControl).Text;
                              xXmlCampo.Add(new XAttribute("ID_CAMPO", ListaAdscripcion.CL_CAMPO), new XAttribute("NB_VALOR", vNbValor), new XAttribute("NB_TEXTO", vNbTexto));
                          }
                          }
                          vXmlCamposExtra.Add(xXmlCampo);
                      }

                      xmlDatosAdicionales = vXmlCamposExtra.ToString();
                    }

                    }
                }

            return vValidacion;
        }

        protected void ObtieneDepartamentos(string pXmlDepartamentos)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(pXmlDepartamentos);
            XmlNodeList departamentos = xml.GetElementsByTagName("ITEMS");
            vLstDepartamentos = new List<E_DEPARTAMENTOS>();

            XmlNodeList lista =
            ((XmlElement)departamentos[0]).GetElementsByTagName("ITEM");

            foreach (XmlElement nodo in lista)
            {
                E_DEPARTAMENTOS f = new E_DEPARTAMENTOS
                {
                    ID_DEPARTAMENTO = nodo.GetAttribute("ID_DEPARTAMENTO"),
                    NB_DEPARTAMENTO = nodo.GetAttribute("NB_DEPARTAMENTO")
                };
                vLstDepartamentos.Add(f);
            }

        }

        protected void ObtieneAdicionales(string pXmlAdicionales)
        {
            int vIdCatalogo;
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(pXmlAdicionales);
            XmlNodeList departamentos = xml.GetElementsByTagName("ITEMS");
            vLstAdicionales = new List<E_ADICIONALES_SELECCIONADOS>();
            vLstCamposAdicionales = new List<E_CAMPOS_ADICIONALES>();

            XmlNodeList lista =
            ((XmlElement)departamentos[0]).GetElementsByTagName("ITEM");

            foreach (XmlElement nodo in lista)
            {
                E_ADICIONALES_SELECCIONADOS f = new E_ADICIONALES_SELECCIONADOS
                {
                    ID_CATALOGO_LISTA = nodo.GetAttribute("ID_CATALOGO_LISTA"),
                    CL_CAMPO = nodo.GetAttribute("CL_CAMPO"),
                    NB_CAMPO = nodo.GetAttribute("NB_CAMPO")
                };
                vLstAdicionales.Add(f);

                vIdCatalogo = int.Parse(nodo.GetAttribute("ID_CATALOGO_LISTA"));

                bool exist = vLstCamposAdicionales.Exists(e => e.ID_CATALOGO_LISTA == vIdCatalogo);
                if (!exist)
                    vLstCamposAdicionales.Add(new E_CAMPOS_ADICIONALES { ID_CATALOGO_LISTA = vIdCatalogo });
            }
        }

        protected void ObtieneValoresAdicionales(string pXmlAdicionales)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(pXmlAdicionales);
            XmlNodeList valorAdicionales = xml.GetElementsByTagName("CAMPOS");
            vLstValorAdicional = new List<E_ADICIONALES_SELECCIONADOS>();

            XmlNodeList lista =
            ((XmlElement)valorAdicionales[0]).GetElementsByTagName("CAMPO");

            foreach (XmlElement nodo in lista)
            {
                E_ADICIONALES_SELECCIONADOS f = new E_ADICIONALES_SELECCIONADOS
                {
                    ID_CATALOGO_LISTA = nodo.GetAttribute("ID_CAMPO"),
                    CL_CAMPO = nodo.GetAttribute("NB_VALOR"),
                    NB_CAMPO = nodo.GetAttribute("NB_TEXTO")
                };
                vLstValorAdicional.Add(f);
            }
        }

        protected void CargarCombos()
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            int countFiltros = nClima.ObtenerFiltrosEvaluadores(vIdPeriodo).Count;
            if (countFiltros > 0)
            {
                var vFiltros = nClima.ObtenerParametrosFiltros(vIdPeriodo).FirstOrDefault();
                if (vFiltros != null)
                {
                    if (vFiltros.EDAD_INICIO != null)
                    {
                        lbEdad.Visible = true;
                        rntEdad.Visible = true;
                        rntEdad.Value = vFiltros.EDAD_INICIO;
                        rntEdad.MinValue = (double)vFiltros.EDAD_INICIO;
                        rntEdad.MaxValue = (double)vFiltros.EDAD_FINAL;
                    }
                    if (vFiltros.ANTIGUEDAD_INICIO != null)
                    {
                        Label1.Visible = true;
                        rdpIngreso.Visible = true;
                        //rntAntiguedad.Value = vFiltros.ANTIGUEDAD_INICIO;
                        //rntAntiguedad.MinValue = (double)vFiltros.ANTIGUEDAD_INICIO;
                        //rntAntiguedad.MaxValue = (double)vFiltros.ANTIGUEDAD_FINAL;
                    }

                    if (vFiltros.CL_GENERO != null)
                    {
                        lbGenero.Visible = true;
                        cmbGenero.Visible = true;
                    //    List<E_GENERO> vLstGenero = new List<E_GENERO>();
                    //    //cmbGenero.Text = vFiltros.CL_GENERO;
                    //    if (vFiltros.CL_GENERO == "Masculino")
                    //    {
                    //        E_GENERO g = new E_GENERO
                    //        {
                    //            NB_GENERO = "Masculino"
                    //        };
                    //        vLstGenero.Add(g);
                    //    }
                    //    else
                    //    {
                    //        E_GENERO g = new E_GENERO
                    //        {
                    //            NB_GENERO = "Femenino"
                    //        };
                    //        vLstGenero.Add(g);
                    //    }

                    //    cmbGenero.DataSource = vLstGenero;
                    //    cmbGenero.DataTextField = "NB_GENERO";
                    //    cmbGenero.DataValueField = "NB_GENERO";
                    //    cmbGenero.DataBind();
                    //}
                    //else
                    //{
                        List<E_GENERO> vLstGenero = new List<E_GENERO>();
                        E_GENERO g = new E_GENERO
                        {
                            NB_GENERO = "Masculino"
                        };
                        vLstGenero.Add(g);
                        E_GENERO f = new E_GENERO
                        {
                            NB_GENERO = "Femenino"
                        };
                        vLstGenero.Add(f);

                        cmbGenero.DataSource = vLstGenero;
                        cmbGenero.DataTextField = "NB_GENERO";
                        cmbGenero.DataValueField = "NB_GENERO";
                        cmbGenero.DataBind();
                    }


                    if (vFiltros.XML_DEPARTAMENTOS != null)
                    {
                        lbArea.Visible = true;
                        rcbArea.Visible = true;
                        ObtieneDepartamentos(vFiltros.XML_DEPARTAMENTOS);
                        rcbArea.DataSource = vLstDepartamentos;
                        rcbArea.DataTextField = "NB_DEPARTAMENTO";
                        rcbArea.DataValueField = "ID_DEPARTAMENTO";
                        rcbArea.DataBind();
                    }

                    if (vFiltros.XML_CAMPOS_ADICIONALES != null)
                    {
                    ObtieneAdicionales(vFiltros.XML_CAMPOS_ADICIONALES);
                    RotacionPersonalNegocio negocio = new RotacionPersonalNegocio();
                    foreach (E_CAMPOS_ADICIONALES item in vLstCamposAdicionales)
                    {
                        HtmlGenericControl vDiv = new HtmlGenericControl("div");
                        vDiv.Attributes.Add("class", "ctrlBasico");
                        SPE_OBTIENE_ADSCRIPCIONES_Result ListaAdscripcion = negocio.ObtieneCatalogoAdscripciones(item.ID_CATALOGO_LISTA).FirstOrDefault();
                        List<E_ADICIONALES_SELECCIONADOS> LstValores = vLstAdicionales.Where(w => w.ID_CATALOGO_LISTA == item.ID_CATALOGO_LISTA.ToString()).ToList();

                        RadLabel vControlLabel = new RadLabel();
                        vControlLabel.Text = ListaAdscripcion.NB_CAMPO + ": ";
                        vControlLabel.Font.Bold = true;

                        Control vControl = new RadComboBox()
                        {
                            ID = ListaAdscripcion.CL_CAMPO,
                            Width = 250,
                            Filter = RadComboBoxFilter.Contains,
                            EmptyMessage = "Selecciona",
                        };

                        foreach (var itemValue in LstValores)
                        {
                            ((RadComboBox)vControl).Items.Add(new RadComboBoxItem()
                            {
                                Text = itemValue.NB_CAMPO,
                                Value = itemValue.CL_CAMPO,
                            });
                        }


                        vDiv.Controls.Add(vControlLabel);
                        vDiv.Controls.Add(vControl);
                        dvCamposExtra.Controls.Add(vDiv);
                    }

                    }
                }
            }
        }

        protected void CargarCombosDatos()
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            int countFiltros = nClima.ObtenerFiltrosEvaluadores(vIdPeriodo).Count;
            if (countFiltros > 0)
            {
                var vFiltros = nClima.ObtenerParametrosFiltros(vIdPeriodo).FirstOrDefault();

               SPE_OBTIENE_DATOS_EVALUADORES_CLIMA_Result vDatosEvaluador = nClima.ObtenerValoresDatos(vIdPeriodo,vIdEvaluado).FirstOrDefault();
                if (vFiltros != null)
                {
                    if (vFiltros.EDAD_INICIO != null)
                    {
                        lbEdad.Visible = true;
                        rntEdad.Visible = true;
                        rntEdad.Value = (double)vDatosEvaluador.NO_EDAD;
                    }
                    if (vFiltros.ANTIGUEDAD_INICIO != null)
                    {
                        Label1.Visible = true;
                        rdpIngreso.Visible = true;
                        rdpIngreso.SelectedDate = vDatosEvaluador.FECHA_INGRESO;
                    }

                    if (vFiltros.CL_GENERO != null)
                    {
                        lbGenero.Visible = true;
                        cmbGenero.Visible = true;
                        cmbGenero.Text = vDatosEvaluador.NB_GENERO;
                    }


                    if (vFiltros.XML_DEPARTAMENTOS != null)
                    {
                        lbArea.Visible = true;
                        rcbArea.Visible = true;
                        rcbArea.Text = vDatosEvaluador.NB_DEPARTAMENTO;
                    }

                    if (vFiltros.XML_CAMPOS_ADICIONALES != null)
                    {
                        ObtieneAdicionales(vFiltros.XML_CAMPOS_ADICIONALES);
                        ObtieneValoresAdicionales(vDatosEvaluador.XML_CAMPOS_ADICIONALES);
                        RotacionPersonalNegocio negocio = new RotacionPersonalNegocio();
                        foreach (E_CAMPOS_ADICIONALES item in vLstCamposAdicionales)
                        {
                            HtmlGenericControl vDiv = new HtmlGenericControl("div");
                            vDiv.Attributes.Add("class", "ctrlBasico");
                            SPE_OBTIENE_ADSCRIPCIONES_Result ListaAdscripcion = negocio.ObtieneCatalogoAdscripciones(item.ID_CATALOGO_LISTA).FirstOrDefault();
                            List<E_ADICIONALES_SELECCIONADOS> LstValores = vLstAdicionales.Where(w => w.ID_CATALOGO_LISTA == item.ID_CATALOGO_LISTA.ToString()).ToList();

                            RadLabel vControlLabel = new RadLabel();
                            vControlLabel.Text = ListaAdscripcion.NB_CAMPO + ": ";
                            vControlLabel.Font.Bold = true;

                            Control vControl = new RadComboBox()
                            {
                                ID = ListaAdscripcion.CL_CAMPO,
                                Width = 250,
                                Filter = RadComboBoxFilter.Contains,
                            };

                            if (ListaAdscripcion != null)
                            {
                                ((RadComboBox)vControl).Items.Add(new RadComboBoxItem()
                                {
                                    Text = vLstValorAdicional.Where(w => w.ID_CATALOGO_LISTA == ListaAdscripcion.CL_CAMPO).FirstOrDefault().NB_CAMPO,
                                    Value = vLstValorAdicional.Where(w => w.ID_CATALOGO_LISTA == ListaAdscripcion.CL_CAMPO).FirstOrDefault().CL_CAMPO,
                                });
                            }


                            vDiv.Controls.Add(vControlLabel);
                            vDiv.Controls.Add(vControl);
                            dvCamposExtra.Controls.Add(vDiv);
                        }

                    }
                }
            }
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {

                if (Request.QueryString["ID_PERIODO"] != null)
                {
                    vIdPeriodo = int.Parse(Request.QueryString["ID_PERIODO"]);
                }
                if (Request.QueryString["ID_EVALUADOR"] != null)
                {
                    vIdEvaluado = int.Parse(Request.QueryString["ID_EVALUADOR"]);
                    CargarCombosDatos();
                }
                else
                {
                    vIdEvaluado = 0;
                    CargarCombos();
                }
                if (Request.Params["FG_HABILITADO"] != null)
                {
                    vFgHabilitado = bool.Parse(Request.Params["FG_HABILITADO"].ToString());
                }
                else
                {
                    vFgHabilitado = true;
                }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO; ;
            vNbPrograma = ContextoUsuario.nbPrograma;

            string vClModulo = "EVALUACION";
            string vModulo = Request.QueryString["m"];
            if (vModulo != null)
                vClModulo = vModulo;

            cssModulo = Utileria.ObtenerCssModulo(vClModulo);


            if (!IsPostBack)
            {
                //if (Request.QueryString["ID_PERIODO"] != null)
                //{
                //    vIdPeriodo = int.Parse(Request.QueryString["ID_PERIODO"]);
                //}
                //if (Request.Params["FG_HABILITADO"] != null)
                //{
                //    vFgHabilitado = bool.Parse(Request.Params["FG_HABILITADO"].ToString());
                //}
                //else
                //{
                //    vFgHabilitado = true;
                //}

                ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
                var vPeriodoClima = nClima.ObtienePeriodosClima(pIdPerido: vIdPeriodo).FirstOrDefault();
                txtNoPeriodo.InnerText = vPeriodoClima.NB_PERIODO.ToString();

                rgCuestionario.Enabled = vFgHabilitado;
                rgPreguntasAbiertas.Enabled = vFgHabilitado;
                btnFinalizar.Enabled = vFgHabilitado;
                //CargarCombos();
            }
        }

        protected void rgCuestionario_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (vIdEvaluado != 0)
            {
                ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
                vlstCuestionarios = nClima.ObtieneCuestionario(pID_EVALUADOR: vIdEvaluado, pID_PERIODO: vIdPeriodo).Select(s => new E_PREGUNTAS_CUESTIONARIO_CLIMA
                {
                    ID_CUESTIONARIO = s.ID_CUESTIONARIO,
                    ID_CUESTIONARIO_PREGUNTA = s.ID_CUESTIONARIO_PREGUNTA,
                    ID_EVALUADOR = s.ID_EVALUADOR,
                    NB_PREGUNTA = s.NB_PREGUNTA,
                    NO_SECUENCIA = s.NO_SECUENCIA,
                    NO_VALOR_RESPUESTA = s.NO_VALOR_RESPUESTA,
                    FG_VALOR1 = s.NO_VALOR_RESPUESTA == 4 ? true : false,
                    FG_VALOR2 = s.NO_VALOR_RESPUESTA == 3 ? true : false,
                    FG_VALOR3 = s.NO_VALOR_RESPUESTA == 2 ? true : false,
                    FG_VALOR4 = s.NO_VALOR_RESPUESTA == 1 ? true : false,
                }).ToList();
                rgCuestionario.DataSource = vlstCuestionarios;
            }
            else
            {
                ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
                int vCantPreguntas = nClima.ObtienePreguntasPeriodo(pID_PERIODO: vIdPeriodo).Count();

                lstCuestionario = nClima.ObtienePreguntasPeriodo(pID_PERIODO: vIdPeriodo).Select(s => new E_PREGUNTAS_CUESTIONARIO_CLIMA
               {
                   ID_CUESTIONARIO_PREGUNTA = s.ID_PREGUNTA,
                   NB_PREGUNTA = s.NB_PREGUNTA,
                   NO_SECUENCIA = s.NO_SECUENCIA,
                   FG_VALOR1 = false,
                   FG_VALOR2 = false,
                   FG_VALOR3 = false,
                   FG_VALOR4 = false,
               }).ToList();
                rgCuestionario.DataSource = lstCuestionario;
            }
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (DatosEvaluador())
            {
                ValorRespuestas();
                string vMensaje;
                ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();

                if (lstCuestionario.Count == vlstRespuestas.Count)
                {
                    var vResultado = nClima.InsertarCuestionarioConfiable(vIdPeriodo, xmlRespuestasCuestionario, xmlPreguntasAbiertas, xmlDatosEvaluador, xmlDatosAdicionales, vClUsuario, vNbPrograma);
                    vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
                }
                else
                {
                    vMensaje = "No se puede guardar el cuestionario por que hay preguntas sin responder.";
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
                }
            }
        }

        protected void rgPreguntasAbiertas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            if (vIdEvaluado != 0)
            {
                ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
                int vCount = nClima.ObtenerCuestionarioPreAbiertas(pID_EVALUADOR: vIdEvaluado, pID_PERIODO: vIdPeriodo).Count;
                if (vCount > 0)
                    rgPreguntasAbiertas.DataSource = nClima.ObtenerCuestionarioPreAbiertas(pID_EVALUADOR: vIdEvaluado, pID_PERIODO: vIdPeriodo).ToList();
                else
                    rgPreguntasAbiertas.Visible = false;
            }
            else
            {
                ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
                int vCount = nClima.ObtenerPreguntasAbiertas(vIdPeriodo, null).Count;
                if (vCount > 0)
                    rgPreguntasAbiertas.DataSource = nClima.ObtenerPreguntasAbiertas(vIdPeriodo, null).ToList();
                else
                    rgPreguntasAbiertas.Visible = false;
            }
        }
    }

    [Serializable]
    public class E_GENERO
    {
        public string CL_GENERO { get; set; }
        public string NB_GENERO { get; set; }
    }

}