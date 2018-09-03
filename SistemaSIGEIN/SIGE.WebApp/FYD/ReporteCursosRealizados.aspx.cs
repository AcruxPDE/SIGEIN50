using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
{
    public partial class ReporteCursosRealizados : System.Web.UI.Page
    {
        #region Variables
        private string vXmlCatalogos
        {
            get { return (string)ViewState["vs_cf_xml_catalogos"]; }
            set { ViewState["vs_cf_xml_catalogos"] = value; }

        }
        #endregion

        #region Funciones

        private List<E_SELECTOR_GENERICO> ObtieneDatosCatalogo(string CL_CATALOGO)
        {
            XElement xmlCatalogos = XElement.Parse(vXmlCatalogos);

            List<E_SELECTOR_GENERICO> oCatalogo = new List<E_SELECTOR_GENERICO>();

            switch (CL_CATALOGO)
            {
                case "CURSOS":
                    if (xmlCatalogos.Element("CURSOS") != null)
                    {
                        oCatalogo.AddRange(xmlCatalogos.Element("CURSOS").Elements("CURSO").Select(t => new E_SELECTOR_GENERICO
                        {
                            ID_ENTIDAD = UtilXML.ValorAtributo<int>(t.Attribute("ID_ENTIDAD")),
                            CL_ENTIDAD = UtilXML.ValorAtributo<string>(t.Attribute("CL_ENTIDAD")),
                            NB_ENTIDAD = UtilXML.ValorAtributo<string>(t.Attribute("NB_ENTIDAD"))
                        }));
                    }

                    break;

                case "INSTRUCTORES":


                    if (xmlCatalogos.Element("INSTRUCTORES") != null)
                    {
                        oCatalogo.AddRange(xmlCatalogos.Element("INSTRUCTORES").Elements("INSTRUCTOR").Select(t => new E_SELECTOR_GENERICO
                        {
                            ID_ENTIDAD = UtilXML.ValorAtributo<int>(t.Attribute("ID_ENTIDAD")),
                            CL_ENTIDAD = UtilXML.ValorAtributo<string>(t.Attribute("CL_ENTIDAD")),
                            NB_ENTIDAD = UtilXML.ValorAtributo<string>(t.Attribute("NB_ENTIDAD"))
                        }));
                    }


                    break;
                case "COMPETENCIAS":

                    if (xmlCatalogos.Element("COMPETENCIAS") != null)
                    {
                        oCatalogo.AddRange(xmlCatalogos.Element("COMPETENCIAS").Elements("COMPETENCIA").Select(t => new E_SELECTOR_GENERICO
                        {
                            ID_ENTIDAD = UtilXML.ValorAtributo<int>(t.Attribute("ID_ENTIDAD")),
                            CL_ENTIDAD = UtilXML.ValorAtributo<string>(t.Attribute("CL_ENTIDAD")),
                            NB_ENTIDAD = UtilXML.ValorAtributo<string>(t.Attribute("NB_ENTIDAD"))
                        }));
                    }

                    break;
                case "PARTICIPANTES":

                    if (xmlCatalogos.Element("PARTICIPANTES") != null)
                    {
                        oCatalogo.AddRange(xmlCatalogos.Element("PARTICIPANTES").Elements("PARTICIPANTE").Select(t => new E_SELECTOR_GENERICO
                        {
                            ID_ENTIDAD = UtilXML.ValorAtributo<int>(t.Attribute("ID_ENTIDAD")),
                            CL_ENTIDAD = UtilXML.ValorAtributo<string>(t.Attribute("CL_ENTIDAD")),
                            NB_ENTIDAD = UtilXML.ValorAtributo<string>(t.Attribute("NB_ENTIDAD"))
                        }));
                    }


                    break;
                case "EVENTOS":

                    if (xmlCatalogos.Element("EVENTOS") != null)
                    {
                        oCatalogo.AddRange(xmlCatalogos.Element("EVENTOS").Elements("EVENTO").Select(t => new E_SELECTOR_GENERICO
                        {
                            ID_ENTIDAD = UtilXML.ValorAtributo<int>(t.Attribute("ID_ENTIDAD")),
                            CL_ENTIDAD = UtilXML.ValorAtributo<string>(t.Attribute("CL_ENTIDAD")),
                            NB_ENTIDAD = UtilXML.ValorAtributo<string>(t.Attribute("NB_ENTIDAD"))
                        }));
                    }

                    break;
                default:
                    oCatalogo = new List<E_SELECTOR_GENERICO>();
                    break;
            }

            return oCatalogo;
        }

        private List<int> obtieneIdentificadores(RadGrid rgSource)
        {
            List<int> lista = new List<int>();
            lista = rgSource.MasterTableView.GetSelectedItems().Select(t => int.Parse(t.GetDataKeyValue("ID_ENTIDAD").ToString())).ToList();
            return lista;
        }

        private void AbrirReporte(Guid pIdReoprte, string pMetodo)
        {

            if (ContextoReportes.oReporteFyd == null)
            {
                ContextoReportes.oReporteFyd = new List<E_REPORTE_FYD>();
            }

            E_REPORTE_FYD oReporte = new E_REPORTE_FYD();

            oReporte.ID_REPORTE_FYD = pIdReoprte;

            if (btnInterno.Checked)
            {
                oReporte.CL_TIPO_CURSO = "INTERNO";
            }
            else if (btnExterno.Checked)
            {
                oReporte.CL_TIPO_CURSO = "EXTERNO";
            }
            else if (btnAmbos.Checked)
            {
                oReporte.CL_TIPO_CURSO = null;
            }

            oReporte.FE_FINAL = dtpTermino.SelectedDate.Value;
            oReporte.FE_INICIO = dtpInicial.SelectedDate.Value;

            if (rbComSeleccion.Checked)
            {
                oReporte.LISTA_COMPETENCIAS = obtieneIdentificadores(GridCompetencias);
            }

            if (rbCursosSeleccion.Checked)
            {
                oReporte.LISTA_CURSOS = obtieneIdentificadores(GridCursos);
            }

            //if (rbEventoSeleccion.Checked)
            //{
            //    oReporte.LISTA_EVENTOS = obtieneIdentificadores(GridEventos);
            //}

            if (rbInsSeleccion.Checked)
            {
                oReporte.LISTA_INSTRUCTORES = obtieneIdentificadores(GridInstructores);
            }

            if (rbParSeleccion.Checked)
            {
                oReporte.LISTA_PARTICIPANTES = obtieneIdentificadores(GridParticipantes);
            }

            ContextoReportes.oReporteFyd.Add(oReporte);

            string script = string.Format(pMetodo, pIdReoprte.ToString());// "OpenCursosRealizadosWindow(\"" + oReporte.ID_REPORTE_FYD.ToString() + "\");";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

        }

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ConsultasFYDNegocio neg = new ConsultasFYDNegocio();
                vXmlCatalogos = neg.ObtenerCatalogos();

                dtpInicial.SelectedDate = new DateTime(DateTime.Now.Year, 1, 1);
                dtpTermino.SelectedDate = DateTime.Now;
            }
        }

        protected void GridCursos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridCursos.DataSource = ObtieneDatosCatalogo("CURSOS");
        }

        protected void GridInstructores_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridInstructores.DataSource = ObtieneDatosCatalogo("INSTRUCTORES");
        }

        protected void GridCompetencias_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridCompetencias.DataSource = ObtieneDatosCatalogo("COMPETENCIAS");
        }

        protected void GridParticipantes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridParticipantes.DataSource = ObtieneDatosCatalogo("PARTICIPANTES");
        }

        //protected void GridEventos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    GridEventos.DataSource = ObtieneDatosCatalogo("EVENTOS");
        //}

        protected void btnReporteCursos_Click(object sender, EventArgs e)
        {
            AbrirReporte(Guid.NewGuid(), "OpenCursosRealizadosWindow(\"{0}\");");
        }

    }
}