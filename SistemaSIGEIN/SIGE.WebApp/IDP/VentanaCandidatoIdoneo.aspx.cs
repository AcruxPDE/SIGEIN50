using Newtonsoft.Json;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.IntegracionDePersonal;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaCandidatoIdoneo : System.Web.UI.Page
    {
        #region Variables

        public int vIdRequisicion
        {
            get { return (int)ViewState["vs_vci_id_requisicion"]; }
            set { ViewState["vs_vci_id_requisicion"] = value; }
        }

        private string vLstCandidatos
        {
            get { return (string)ViewState["vs_vci_xml_candidatos"]; }
            set { ViewState["vs_vci_xml_candidatos"] = value; }
        }

        private XElement vXmlCandidatos;

        private decimal vPrCompatibilidad
        {
            get { return (decimal)ViewState["vs_vci_pr_compatibilidad"]; }
            set { ViewState["vs_vci_pr_compatibilidad"] = value; }
        }

        private int vNoCandidatos
        {
            get { return (int)ViewState["vs_vci_no_candidatos"]; }
            set { ViewState["vs_vci_no_candidatos"] = value; }
        }

        private List<E_CANDIDATO_IDONEO> vlstCandidatoIdoneo
        {
            get { return (List<E_CANDIDATO_IDONEO>)ViewState["vs_vlstCandidatoIdoneo"]; }
            set { ViewState["vs_vlstCandidatoIdoneo"] = value; }
        }

        public Guid vIdBusquedaCandidato
        {
            get { return (Guid)ViewState["vs_vIdBusquedaCandidato"]; }
            set { ViewState["vs_vIdBusquedaCandidato"] = value; }
        }

        public int vIdPuesto
        {
            get { return (int)ViewState["vs_vci_id_puesto"]; }
            set { ViewState["vs_vci_id_puesto"] = value; }
        }

        #endregion

        #region Funciones

        //protected void AgregarCandidatos(string pCandidatos)
        //{
        //    List<E_SELECTOR_CANDIDATO> vCandidatos = JsonConvert.DeserializeObject<List<E_SELECTOR_CANDIDATO>>(pCandidatos);

        //    if (vXmlCandidatos == null & string.IsNullOrEmpty(vLstCandidatos))
        //    {
        //        vXmlCandidatos = new XElement("CANDIDATOS");
        //    }
        //    else
        //    {
        //        vXmlCandidatos = XElement.Parse(vLstCandidatos);
        //    }


        //    if (vCandidatos.Count > 0)
        //    {
        //        if (vXmlCandidatos.HasElements)
        //        {
        //            foreach (E_SELECTOR_CANDIDATO item in vCandidatos)
        //            {
        //                var vCandidato = vXmlCandidatos.Elements("CANDIDATO").Where(t => t.Attribute("ID_CANDIDATO").Value.Equals(item.idCandidato));

        //                if (vCandidato == null)
        //                {
        //                    vXmlCandidatos.Add(new XElement("CANDIDATO", new XAttribute("ID_CANDIDATO", item.idCandidato)));
        //                }
        //            }
        //        }
        //        else
        //        {
        //            vXmlCandidatos.Add(vCandidatos.Select(s => new XElement("CANDIDATO", new XAttribute("ID_CANDIDATO", s.idCandidato))));
        //        }
        //        CargaCandidatoCompetencia(vXmlCandidatos.ToString());
        //    }
        //}

        //protected void CargaCandidatoCompetencia(string pLstCandidatos)
        //{
        //    CandidatoNegocio nCandidato = new CandidatoNegocio();
        //    vlstCandidatoIdoneo = nCandidato.ObtieneCandidatoIdoneo(vIdRequisicion, pLstCandidatos, vPrCompatibilidad, vNoCandidatos, true).Select(s => new E_CANDIDATO_IDONEO
        //    {
        //        CL_SOLICITUD = s.CL_SOLICITUD,
        //        CL_TOKEN = s.CL_TOKEN,
        //        DS_AREAS_INTERES = s.DS_AREAS_INTERES,
        //        DS_COMPETENCIAS_LABORALES = s.DS_COMPETENCIAS_LABORALES,
        //        DS_POSTGRADOS = s.DS_COMPETENCIAS_LABORALES,
        //        DS_PROFESIONAL = s.DS_PROFESIONAL,
        //        DS_TECNICA = s.DS_TECNICA,
        //        FI_FOTOGRAFIA = s.FI_FOTOGRAFIA,
        //        ID_BATERIA = s.ID_BATERIA,
        //        ID_CANDIDATO = s.ID_CANDIDATO,
        //        ID_SOLICITUD = s.ID_SOLICITUD,
        //        NB_CANDIDATO = s.NB_CANDIDATO,
        //        NO_EDAD = s.NO_EDAD,
        //        PR_CUMPLIMIENTO = s.PR_CUMPLIMIENTO.ToString() + "%",
        //    }).ToList();
            
        //    grdCandidato.Rebind();
        //}

        //protected void CargaCandidatoPerfil()
        //{
        //    CandidatoNegocio nCandidato = new CandidatoNegocio();
        //    vlstCandidatoIdoneo = nCandidato.ObtieneCandidatoIdoneoPerfil(vIdRequisicion, vNoCandidatos, null, true).Select(s => new E_CANDIDATO_IDONEO
        //    {
        //        CL_SOLICITUD = s.CL_SOLICITUD,
        //        CL_TOKEN = s.CL_TOKEN,
        //        DS_AREAS_INTERES = s.DS_AREAS_INTERES,
        //        DS_COMPETENCIAS_LABORALES = s.DS_COMPETENCIAS_LABORALES,
        //        DS_POSTGRADOS = s.DS_POSTGRADOS,
        //        DS_PROFESIONAL = s.DS_PROFESIONAL,
        //        DS_TECNICA = s.DS_TECNICA,
        //        FI_FOTOGRAFIA = s.FI_FOTOGRAFIA,
        //        ID_BATERIA = s.ID_BATERIA,
        //        ID_CANDIDATO = s.ID_CANDIDATO,
        //        ID_SOLICITUD = s.ID_SOLICITUD,
        //        NB_CANDIDATO = s.NB_CANDIDATO,
        //        NO_EDAD = s.NO_EDAD,
        //        PR_CUMPLIMIENTO = "NA",
        //    }).ToList();
            
        //    grdCandidato.Rebind();
        //}

        private void BuscarCandidatoRequisicion()
        {
            XElement vParametrosBusqueda = new XElement("PARAMETROS");
            XElement vParametrosPerfil = new XElement("PARAMETROS_PERFIL");
            XElement vParametrosCompetencias = new XElement("PARAMETROS_COMPETENCIAS");

            RequisicionNegocio nRequisicion = new RequisicionNegocio();

            if (dtpInicial.SelectedDate.HasValue)
            {
                vParametrosBusqueda.Add(new XAttribute("FE_INICIAL", dtpInicial.SelectedDate.Value.ToString("MM/dd/yyyy")));
            }

            if (dtpFinal.SelectedDate.HasValue)
            {
                vParametrosBusqueda.Add(new XAttribute("FE_FINAL", dtpFinal.SelectedDate.Value.ToString("MM/dd/yyyy")));
            }


            if (chkPerfil.Checked)
            {
                ContextoBusquedaCandidato.oPuestoVsCandidatos.Where(t => t.vIdParametroBusqueda == vIdBusquedaCandidato).FirstOrDefault().vFgBusquedaPerfil = true;
                grdCandidato.Columns[4].Visible = true;
                bool buscarperfil = false;
                string xmlParameter = "";
                vParametrosBusqueda.Add(new XAttribute("FG_PERFIL", "1"));

                vParametrosPerfil.Add(new XAttribute("FG_EDAD", chkParametroEdad.Checked.Value ? "1" : "0"));
                ContextoBusquedaCandidato.oPuestoVsCandidatos.Where(t => t.vIdParametroBusqueda == vIdBusquedaCandidato).FirstOrDefault().vFgEdad = chkParametroEdad.Checked.Value;

                vParametrosPerfil.Add(new XAttribute("FG_GENERO", chkParametroGenero.Checked.Value ? "1" : "0"));
                ContextoBusquedaCandidato.oPuestoVsCandidatos.Where(t => t.vIdParametroBusqueda == vIdBusquedaCandidato).FirstOrDefault().vFgGenero = chkParametroGenero.Checked.Value;

                vParametrosPerfil.Add(new XAttribute("FG_EDO_CIVIL", chkParametroEstadoCivil.Checked.Value ? "1" : "0"));
                ContextoBusquedaCandidato.oPuestoVsCandidatos.Where(t => t.vIdParametroBusqueda == vIdBusquedaCandidato).FirstOrDefault().vFgEdoCivil = chkParametroEstadoCivil.Checked.Value;

                vParametrosPerfil.Add(new XAttribute("FG_ESCOLARIDAD", chkParametroEscolaridad.Checked.Value ? "1" : "0"));
                ContextoBusquedaCandidato.oPuestoVsCandidatos.Where(t => t.vIdParametroBusqueda == vIdBusquedaCandidato).FirstOrDefault().vFgEscolaridad = chkParametroEscolaridad.Checked.Value;

                vParametrosPerfil.Add(new XAttribute("FG_COMP_ESP", chkParametroCompEsp.Checked.Value ? "1" : "0"));
                ContextoBusquedaCandidato.oPuestoVsCandidatos.Where(t => t.vIdParametroBusqueda == vIdBusquedaCandidato).FirstOrDefault().vFgComEsp = chkParametroCompEsp.Checked.Value;

                vParametrosPerfil.Add(new XAttribute("FG_AREA_INTERES", chkAreasInteres.Checked.Value ? "1" : "0"));
                ContextoBusquedaCandidato.oPuestoVsCandidatos.Where(t => t.vIdParametroBusqueda == vIdBusquedaCandidato).FirstOrDefault().vFgAreaInteres = chkAreasInteres.Checked.Value;

                xmlParameter = vParametrosPerfil.ToString();
                buscarperfil = xmlParameter.Contains("1");
                if (buscarperfil == true)
                {
                    vParametrosBusqueda.Add(vParametrosPerfil);
                }
            }
            else
            {
                grdCandidato.Columns[4].Visible = false;
                vParametrosBusqueda.Add(new XAttribute("FG_PERFIL", "0"));
                ContextoBusquedaCandidato.oPuestoVsCandidatos.Where(t => t.vIdParametroBusqueda == vIdBusquedaCandidato).FirstOrDefault().vFgBusquedaPerfil = false;
            }


            if (chkCompetencias.Checked)
            {
                grdCandidato.Columns[5].Visible = true;
                vParametrosBusqueda.Add(new XAttribute("FG_COMPETENCIAS", "1"));
                vParametrosCompetencias.Add(new XAttribute("PR_COMPATIBILIDAD", txtPrMinimo.Text));
                vParametrosBusqueda.Add(vParametrosCompetencias);
                ContextoBusquedaCandidato.oPuestoVsCandidatos.Where(t => t.vIdParametroBusqueda == vIdBusquedaCandidato).FirstOrDefault().vFgBusquedaCompetencias = true;
            }
            else
            {
                grdCandidato.Columns[5].Visible = false;
                vParametrosBusqueda.Add(new XAttribute("FG_COMPETENCIAS", "0"));
                ContextoBusquedaCandidato.oPuestoVsCandidatos.Where(t => t.vIdParametroBusqueda == vIdBusquedaCandidato).FirstOrDefault().vFgBusquedaCompetencias = false;
            }

            vlstCandidatoIdoneo = nRequisicion.BuscaCandidatoRequisicion(vIdRequisicion, vParametrosBusqueda.ToString());

            rtsBuscarCandidato.Tabs[1].Selected = true;
            rtsBuscarCandidato.Tabs[1].Enabled = true;
            rtsBuscarCandidato.SelectedIndex = 1;
            rmpBuscarCandidato.SelectedIndex = 1;

            grdCandidato.Rebind();
        }

        private void CargarDatos()
        {
            RequisicionNegocio nRequisicion = new RequisicionNegocio();

            var vRequisicion = nRequisicion.ObtieneRequisicion(pIdRequisicion: vIdRequisicion).FirstOrDefault();

            if (vRequisicion != null)
            {
                txtClaveRequisicion.InnerText = vRequisicion.NO_REQUISICION;
                txtPuestoEnlace.InnerText = vRequisicion.NB_PUESTO;
                vIdPuesto = vRequisicion.ID_PUESTO.Value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                vIdBusquedaCandidato = Guid.NewGuid();

                if (ContextoBusquedaCandidato.oPuestoVsCandidatos == null)
                {
                    ContextoBusquedaCandidato.oPuestoVsCandidatos = new List<E_PARAMETROS_BUSQUEDA_CANDIDATO>();
                }

                ContextoBusquedaCandidato.oPuestoVsCandidatos.Add(new E_PARAMETROS_BUSQUEDA_CANDIDATO { vIdParametroBusqueda = vIdBusquedaCandidato });

                vXmlCandidatos = new XElement("CANDIDATOS");

                if (Request.Params["RequisicionId"] != null)
                {
                    vIdRequisicion = int.Parse(Request.Params["RequisicionId"].ToString());
                    CargarDatos();
                }

                chkParametroEdad.Enabled = false;
                chkParametroGenero.Enabled = false;
                chkParametroEstadoCivil.Enabled = false;
                chkParametroEscolaridad.Enabled = false;
                chkParametroCompEsp.Enabled = false;
                chkAreasInteres.Enabled = false;
                txtPrMinimo.Enabled = false;

                rtsBuscarCandidato.Tabs[1].Enabled = false;

                vlstCandidatoIdoneo = new List<E_CANDIDATO_IDONEO>();

            }
            vNoCandidatos = 200;
            vPrCompatibilidad = (txtPrMinimo.Text == "" ? decimal.Parse("1") : decimal.Parse(txtPrMinimo.Text));
        }

        protected void ramCandidato_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
                //grdCandidato.Rebind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            /*
            if (!dtpInicial.SelectedDate.HasValue)
            {
                UtilMensajes.MensajeResultadoDB(rwMensajes, "Indica la fecha inicial", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                return;
            }

            if (!dtpFinal.SelectedDate.HasValue)
            {
                UtilMensajes.MensajeResultadoDB(rwMensajes, "Indica la fecha final", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                return;
            }
            */

            if (dtpInicial.SelectedDate.HasValue & dtpFinal.SelectedDate.HasValue)
            {
                if (dtpInicial.SelectedDate.Value > dtpFinal.SelectedDate.Value)
                {
                    UtilMensajes.MensajeResultadoDB(rwMensajes, "El rango de fechas no es correcto", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                    return;
                }

                if (!chkPerfil.Checked & !chkCompetencias.Checked)
                {
                    UtilMensajes.MensajeResultadoDB(rwMensajes, "Selecciona un tipo de busqueda", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                    return;
                }
            }

            BuscarCandidatoRequisicion();

            //if (chkPerfil.Checked == true)
            //{
            //    CargaCandidatoPerfil();
            //}

            //if (chkCompetencias.Checked == true)
            //{
            //    CargaCandidatoCompetencia(null);
            //}
        }

        protected void grdCandidato_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdCandidato.DataSource = vlstCandidatoIdoneo;
        }

        protected void ftGrdCandidatos_ApplyExpressions(object sender, RadFilterApplyExpressionsEventArgs e)
        {

            RadFilterSqlQueryProvider queryProvider = new RadFilterSqlQueryProvider();
            queryProvider.ProcessGroup(e.ExpressionRoot);

            grdCandidato.MasterTableView.FilterExpression = queryProvider.Result;
            grdCandidato.Rebind();

        }

        protected void ftGrdCandidatos_ExpressionItemCreated(object sender, RadFilterExpressionItemCreatedEventArgs e)
        {
            RadFilterSingleExpressionItem singleItem = e.Item as RadFilterSingleExpressionItem;

            if (singleItem != null)
            {
                switch (singleItem.FieldName)
                {
                    case "CL_SOLICITUD_ESTATUS":
                        RadDropDownList dropDownList = singleItem.InputControl as RadDropDownList;

                        List<E_GENERICA> vLstElementos = new List<E_GENERICA>();

                        vLstElementos.Add(new E_GENERICA { CL_GENERICA = "CREADA", NB_GENERICO = "CREADA" });
                        vLstElementos.Add(new E_GENERICA { CL_GENERICA = "En Proceso", NB_GENERICO = "En Proceso" });

                        dropDownList.DataSource = vLstElementos;
                        dropDownList.DataBind();
                        break;

                    case "CL_ESTADO":
                        RadComboBox dropDownEstado = singleItem.InputControl as RadComboBox;
                        EstadoNegocio nEstado = new EstadoNegocio();
                        List<E_GENERICA> vLstEstados = new List<E_GENERICA>();

                        vLstEstados = nEstado.ObtieneEstados().Select(t => new E_GENERICA { CL_GENERICA = t.CL_ESTADO, NB_GENERICO = t.NB_ESTADO }).ToList();

                        dropDownEstado.Filter = RadComboBoxFilter.Contains;
                        dropDownEstado.MaxHeight = Unit.Pixel(350);
                        dropDownEstado.DataSource = vLstEstados;
                        dropDownEstado.DataBind();

                        break;

                    case "CL_MUNICIPIO":
                        RadComboBox dropDownMunicipio = singleItem.InputControl as RadComboBox;
                        MunicipioNegocio nMunicipio = new MunicipioNegocio();
                        List<E_GENERICA> vLstMunicipio = new List<E_GENERICA>();

                        vLstMunicipio = nMunicipio.ObtieneMunicipios().Select(t => new E_GENERICA { CL_GENERICA = t.CL_MUNICIPIO, NB_GENERICO = t.DS_FILTRO }).ToList();

                        dropDownMunicipio.Filter = RadComboBoxFilter.Contains;
                        dropDownMunicipio.MaxHeight = Unit.Pixel(350);
                        dropDownMunicipio.DataSource = vLstMunicipio;
                        dropDownMunicipio.DataBind();

                        break;

                    case "FE_NACIMIENTO":

                        RadDatePicker dtpNacimiento = singleItem.InputControl as RadDatePicker;

                        dtpNacimiento.DateInput.DateFormat = "dd/MM/yyyy";
                        dtpNacimiento.DateInput.DisplayDateFormat = "dd/MM/yyyy";

                        break;

                    //case "CL_COLONIA":
                    //    RadComboBox dropDownColonia = singleItem.InputControl as RadComboBox;
                    //    ColoniaNegocio nColonia = new ColoniaNegocio();
                    //    List<E_GENERICA> vLstColonias = new List<E_GENERICA>();
                    //    vLstColonias = nColonia.ObtieneColonias().Select(t => new E_GENERICA { CL_GENERICA = t.CL_COLONIA, NB_GENERICO = t.DS_FILTRO }).ToList();
                    //    dropDownColonia.Filter = RadComboBoxFilter.Contains;
                    //    //dropDownMunicipio.DropDownWidth = Unit.Pixel(250);
                    //    dropDownColonia.MaxHeight = Unit.Pixel(350);
                    //    dropDownColonia.DataSource = vLstColonias;
                    //    dropDownColonia.DataBind();
                    //    break;


                    default:
                        break;
                }

            }
        }

        protected void chkPerfil_CheckedChanged(object sender, EventArgs e)
        {
            if(chkPerfil.Checked == false)
            {
                chkParametroEdad.Enabled = false;
                chkParametroGenero.Enabled = false;
                chkParametroEstadoCivil.Enabled = false;
                chkParametroEscolaridad.Enabled = false;
                chkParametroCompEsp.Enabled = false;
                chkAreasInteres.Enabled = false;

                chkParametroEdad.Checked = false;
                chkParametroGenero.Checked = false;
                chkParametroEstadoCivil.Checked = false;
                chkParametroEscolaridad.Checked = false;
                chkParametroCompEsp.Checked = false;
                chkAreasInteres.Checked = false;
            }

            if (chkPerfil.Checked == true)
            {
                chkParametroEdad.Enabled = true;
                chkParametroGenero.Enabled = true;
                chkParametroEstadoCivil.Enabled = true;
                chkParametroEscolaridad.Enabled = true;
                chkParametroCompEsp.Enabled = true;
                chkAreasInteres.Enabled = true;

                chkParametroEdad.Checked = true;
                chkParametroGenero.Checked = true;
                chkParametroEstadoCivil.Checked = true;
                chkParametroEscolaridad.Checked = true;
                chkParametroCompEsp.Checked = true;
                chkAreasInteres.Checked = true;
            }
        }

        protected void chkCompetencias_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCompetencias.Checked == true)
            {
                txtPrMinimo.Enabled = true;
            }
            if (chkCompetencias.Checked == false)
            {
                txtPrMinimo.Enabled = false;
            }
        }

        protected void rgProcesos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RequisicionNegocio nReq = new RequisicionNegocio();
            rgProcesos.DataSource = nReq.ObtenerCandidatosPorRequisicion(pIdRequisicion: vIdRequisicion);
        }

        protected void grdCandidato_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grdCandidato.SelectedItems)
            {
                if (item.GetDataKeyValue("ID_PROCESO_SELECCION").ToString() != "0" && item.GetDataKeyValue("ID_REQUISICION").ToString() != "0")
                {
                    if (int.Parse(item.GetDataKeyValue("ID_REQUISICION").ToString()) == vIdRequisicion && (item.GetDataKeyValue("CL_ESTADO_PROCESO").ToString() != "TERMINADO" && item.GetDataKeyValue("CL_ESTADO_PROCESO").ToString() != "Terminado"))
                    {
                        btnSeleccion.Text = "Continuar con proceso de evaluación";
                        lbMensaje.Visible = false;
                    }
                    else if (int.Parse(item.GetDataKeyValue("ID_REQUISICION").ToString()) == vIdRequisicion && item.GetDataKeyValue("CL_ESTADO_PROCESO").ToString() == "TERMINADO" || item.GetDataKeyValue("CL_ESTADO_PROCESO").ToString() == "Terminado")
                    {
                        btnSeleccion.Text = "Iniciar otro proceso de evaluación";
                        lbMensaje.Visible = true;
                    }
                    else
                    {
                        btnSeleccion.Text = "Iniciar proceso de evaluación";
                        lbMensaje.Visible = false;
                    }
                }
                else
                {
                    btnSeleccion.Text = "Iniciar proceso de evaluación";
                    lbMensaje.Visible = false;
                }
            }
        }

        protected void rgProcesos_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridDataItem item in rgProcesos.SelectedItems)
            {
                if (item.GetDataKeyValue("CL_ESTADO").ToString() != null)
                {
                    if (item.GetDataKeyValue("CL_ESTADO").ToString() == "Terminado" || item.GetDataKeyValue("CL_ESTADO").ToString() == "TERMINADO")
                    {
                        btnVerProceso.Visible = true;
                        btnProceso.Visible = false;
                    }
                    else
                    {
                        btnVerProceso.Visible = false;
                        btnProceso.Visible = true;
                    }
                }
                else
                {
                    btnVerProceso.Visible = false;
                    btnProceso.Visible = true;
                }
            }
        }
    }
}

/*
* 
•	No. Solicitud
•	Candidato
•	Estatus
•	% de perfil
•	% de competencias
•	Pais – Dropdown con catalgo
•	Estado Dropdown con catalogo
•	Municipio – Dropdown con catalogo
•	Colonia – Dropdown con catalogo
•	Fecha de nacimiento  - Calendario
•	Nacionalidad – Texto
•	Disponibilidad para viajar – Check
•	Disponiblidad de horario – Texto
 
 */
