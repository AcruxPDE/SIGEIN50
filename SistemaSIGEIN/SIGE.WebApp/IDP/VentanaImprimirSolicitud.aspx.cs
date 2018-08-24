using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;
using WebApp.Comunes;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaImprimirSolicitud : System.Web.UI.Page
    {

        #region Variables

        List<E_DOCUMENTO> vLstDocumentos
        {
            get { return (List<E_DOCUMENTO>)ViewState["vs_LstDocumentos"]; }
            set { ViewState["vs_LstDocumentos"] = value; }
        }

        public string vXmlSolicitudPlantilla
        {
            get { return (string)ViewState["vs_vXmlSolicitudPlantilla"]; }
            set { ViewState["vs_vXmlSolicitudPlantilla"] = value; }
        }

        public int? vIdSolicitudVS
        {
            get { return (int?)ViewState["vs_vIdSolicitud"]; }
            set { ViewState["vs_vIdSolicitud"] = value; }
        }

        int? vIdPlantillaFormulario
        {
            get { return (int?)ViewState["vs_vFlPlantillaFormulario"]; }
            set { ViewState["vs_vFlPlantillaFormulario"] = value; }
        }

        Guid? vIdItemFotografia
        {
            get { return (Guid?)ViewState["vs_vIdItemFotografia"]; }
            set { ViewState["vs_vIdItemFotografia"] = value; }
        }

        string vFiLogotipo
        {
            get { return (string)ViewState["vs_vFiLogotipo"]; }
            set { ViewState["vs_vFiLogotipo"] = value; }
        }

        string vNbLogotipo
        {
            get { return (string)ViewState["vs_vNbLogotipo"]; }
            set { ViewState["vs_vNbLogotipo"] = value; }
        }

        public string cssModulo = String.Empty;
        string vXmlSolicitud;
        string vXmlValoresSolicitud;
        int? vIdSolicitud;
        Guid? vIdItemFoto;
        string vXmlDocumentos;

        #endregion

        #region Funciones

        protected void CargarDocumentos()
        {
            XElement x = XElement.Parse(vXmlDocumentos).Elements("VALOR").FirstOrDefault(f => UtilXML.ValorAtributo<string>(f.Attribute("ID_CAMPO")) == "LS_DOCUMENTOS");

            if (vLstDocumentos == null)
                vLstDocumentos = new List<E_DOCUMENTO>();

            if (x != null)
            {
                foreach (XElement item in (x.Element("ITEMS") ?? new XElement("ITEMS")).Elements("ITEM"))
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

        public void GenerarCtrlTxtDiv(string pNombreDiv, XElement pXmlCampo)
        {
            HtmlGenericControl vCtrlDiv = new HtmlGenericControl("div");
            HtmlGenericControl vCtrlLabel = new HtmlGenericControl("label");
            vCtrlLabel.Attributes.Add("style", "float: left; font-weight: bold;");
            HtmlGenericControl vCtrlDivValor = new HtmlGenericControl("div");
            vCtrlDivValor.Attributes.Add("style", "float: left;");
            vCtrlLabel.InnerHtml = UtilXML.ValorAtributo<string>(pXmlCampo.Attribute("NB_CAMPO")) + ":&nbsp;&nbsp;";
            vCtrlDivValor.InnerHtml = UtilXML.ValorAtributo<string>(pXmlCampo.Attribute("NB_VALOR"));
            vCtrlDiv.Controls.Add(vCtrlLabel);
            vCtrlDiv.Controls.Add(vCtrlDivValor);

            HtmlGenericControl vCtrlDivSalto = new HtmlGenericControl("div");
            vCtrlDivSalto.Attributes.Add("style", "clear: both; height: 10px");

            if (pNombreDiv == "PERSONAL")
            {
                pvwPersonal.Controls.Add(vCtrlDivSalto);
                pvwPersonal.Controls.Add(vCtrlDiv);
            }
            if (pNombreDiv == "FAMILIAR")
            {
                pvwFamiliar.Controls.Add(vCtrlDivSalto);
                pvwFamiliar.Controls.Add(vCtrlDiv);
            }
            if (pNombreDiv == "ACADEMICA")
            {
                pvwAcademica.Controls.Add(vCtrlDivSalto);
                pvwAcademica.Controls.Add(vCtrlDiv);
            }
            if (pNombreDiv == "LABORAL")
            {
                pvwLaboral.Controls.Add(vCtrlDivSalto);
                pvwLaboral.Controls.Add(vCtrlDiv);
            }
            if (pNombreDiv == "COMPETENCIAS")
            {
                pvwCompetencias.Controls.Add(vCtrlDivSalto);
                pvwCompetencias.Controls.Add(vCtrlDiv);
            }
            if (pNombreDiv == "ADICIONAL")
            {
                pvwAdicional.Controls.Add(vCtrlDivSalto);
                pvwAdicional.Controls.Add(vCtrlDiv);
            }
        }

        public void GenerarCtrlCheckDiv(string pNombreDiv, XElement pXmlCampo)
        {
            HtmlGenericControl vCtrlDiv = new HtmlGenericControl("div");
            HtmlGenericControl vCtrlLabel = new HtmlGenericControl("label");
            vCtrlLabel.Attributes.Add("style", "float: left; font-weight: bold;");
            HtmlGenericControl vCtrlDivValor = new HtmlGenericControl("div");
            vCtrlDivValor.Attributes.Add("style", "float: left;");
            vCtrlLabel.InnerHtml = UtilXML.ValorAtributo<string>(pXmlCampo.Attribute("NB_CAMPO")) + ":&nbsp;&nbsp;";
            vCtrlDivValor.InnerHtml = UtilXML.ValorAtributo<bool>(pXmlCampo.Attribute("NB_VALOR")) == true ? "Sí&nbsp;&nbsp;" : "No&nbsp;&nbsp;";
            vCtrlDiv.Controls.Add(vCtrlLabel);
            vCtrlDiv.Controls.Add(vCtrlDivValor);


            HtmlGenericControl vCtrlDivSalto = new HtmlGenericControl("div");
            vCtrlDivSalto.Attributes.Add("style", "clear: both; height: 10px");

            if (pNombreDiv == "PERSONAL")
            {
                pvwPersonal.Controls.Add(vCtrlDivSalto);
                pvwPersonal.Controls.Add(vCtrlDiv);
                pvwPersonal.Controls.Add(vCtrlDivSalto);
            }
            if (pNombreDiv == "FAMILIAR")
            {
                pvwFamiliar.Controls.Add(vCtrlDivSalto);
                pvwFamiliar.Controls.Add(vCtrlDiv);
                pvwPersonal.Controls.Add(vCtrlDivSalto);
            }
            if (pNombreDiv == "ACADEMICA")
            {
                pvwAcademica.Controls.Add(vCtrlDivSalto);
                pvwAcademica.Controls.Add(vCtrlDiv);
                pvwPersonal.Controls.Add(vCtrlDivSalto);
            }
            if (pNombreDiv == "LABORAL")
            {
                pvwLaboral.Controls.Add(vCtrlDivSalto);
                pvwLaboral.Controls.Add(vCtrlDiv);
                pvwPersonal.Controls.Add(vCtrlDivSalto);
            }
            if (pNombreDiv == "COMPETENCIAS")
            {
                pvwCompetencias.Controls.Add(vCtrlDivSalto);
                pvwCompetencias.Controls.Add(vCtrlDiv);
                pvwPersonal.Controls.Add(vCtrlDivSalto);
            }
            if (pNombreDiv == "ADICIONAL")
            {
                pvwAdicional.Controls.Add(vCtrlDivSalto);
                pvwAdicional.Controls.Add(vCtrlDiv);
                pvwPersonal.Controls.Add(vCtrlDivSalto);
            }
        }

        public void GenerarCtrlTabla(string pNombreDiv, XElement pXmlCampo)
        {
            string vNbTitulo = UtilXML.ValorAtributo<string>(pXmlCampo.Attribute("NB_CAMPO"));
            string vIdGrid = UtilXML.ValorAtributo<string>(pXmlCampo.Attribute("ID_CAMPO"));
            HtmlGenericControl vControlGrid = new HtmlGenericControl("div");

            HtmlGenericControl vGridLabel = new HtmlGenericControl("label");
            vGridLabel.Attributes.Add("style", "font-weight: bold;");
            vGridLabel.InnerText = vNbTitulo;
            vControlGrid.Controls.Add(vGridLabel);

            RadGrid vGrid = new RadGrid()
            {
                ID = vIdGrid,
                Width = (Unit)400,
                AutoGenerateColumns = false,
                BorderWidth = 1,
                BorderStyle = BorderStyle.Solid,
                GroupingEnabled = false

            };

            DataTable dataTable = new DataTable();
            List<string> lstDataKeyNames = new List<string>();
            foreach (XElement vXmlColumna in pXmlCampo.Element("GRID").Element("HEADER").Elements("COLUMNA"))
            {
                dataTable.Columns.Add(UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("ID_COLUMNA")), Utileria.ObtenerTipoDato(UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("CL_TIPO_DATO"))));

                GridBoundColumn vColumn = new GridBoundColumn();
                vColumn.ItemStyle.BorderWidth = 1;
                vColumn.ItemStyle.BorderStyle = BorderStyle.Solid;
                vColumn.HeaderStyle.BorderWidth = 1;
                vColumn.HeaderStyle.BorderStyle = BorderStyle.Solid;
                if (vIdGrid == "LS_EXPERIENCIA_LABORAL")
                {
                    vColumn.HeaderStyle.Font.Size = 7;
                    vColumn.ItemStyle.Font.Size = 7;
                }
                else
                {
                    vColumn.HeaderStyle.Font.Size = 9;
                    vColumn.ItemStyle.Font.Size = 9;
                }
                vColumn.HeaderText = UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("NB_COLUMNA"));
                vColumn.DataField = UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("ID_COLUMNA"));
                vColumn.HeaderStyle.Font.Bold = true;

                switch (UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("CL_TIPO_DATO")))
                {
                    case "DATE":
                        vColumn.DataFormatString = "{0:d}";
                        vColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                        break;
                    case "INT":
                        vColumn.DataFormatString = "{0:N}";
                        vColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                        break;
                }

                if (UtilXML.ValorAtributo<bool>(vXmlColumna.Attribute("FG_VISIBLE")))
                {
                    vColumn.Visible = true;
                }
                else
                {
                    vColumn.Visible = false;
                }

                vGrid.Columns.Add(vColumn);
            }

            vGrid.ClientSettings.Selecting.AllowRowSelect = false;
            vGrid.DataSource = dataTable;

            DataTable vDataTable = (DataTable)vGrid.DataSource;
            vDataTable.Clear();
            if (pXmlCampo.Element("GRID").Element("DATOS") != null)
                foreach (XElement vXmlFila in pXmlCampo.Element("GRID").Element("DATOS").Elements("ITEM"))
                {
                    DataRow row = vDataTable.NewRow();
                    foreach (XElement vXmlColumna in pXmlCampo.Element("GRID").Element("HEADER").Elements("COLUMNA"))
                        row[UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("ID_COLUMNA"))] = UtilXML.ValorAtributo(vXmlFila.Attribute(UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("ID_COLUMNA"))), Utileria.ObtenerEnumTipoDato(UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("CL_TIPO_DATO")))) ?? DBNull.Value;
                    vDataTable.Rows.Add(row);
                }
            string vOrdenamiento = UtilXML.ValorAtributo<string>(pXmlCampo.Element("GRID").Attribute("ORDERBY"));
            string vTipoOrdenamiento = UtilXML.ValorAtributo<string>(pXmlCampo.Element("GRID").Attribute("ORDERTYPE"));
            DataView dv;

            if (vOrdenamiento != null)
            {
                dv = vDataTable.DefaultView;
                dv.Sort = vOrdenamiento + " " + vTipoOrdenamiento;
                vDataTable = dv.ToTable();
            }

            vGrid.DataSource = vDataTable;
            vGrid.DataBind();

            vControlGrid.Controls.Add(vGrid);

            HtmlGenericControl vCtrlDivSalto = new HtmlGenericControl("div");
            vCtrlDivSalto.Attributes.Add("style", "clear: both; height: 10px");

            if (pNombreDiv == "PERSONAL")
            {
                pvwFamiliar.Controls.Add(vCtrlDivSalto);
                pvwPersonal.Controls.Add(vControlGrid);
                pvwFamiliar.Controls.Add(vCtrlDivSalto);
            }
            if (pNombreDiv == "FAMILIAR")
            {
                pvwFamiliar.Controls.Add(vCtrlDivSalto);
                pvwFamiliar.Controls.Add(vControlGrid);
                pvwFamiliar.Controls.Add(vCtrlDivSalto);
            }
            if (pNombreDiv == "ACADEMICA")
            {
                pvwFamiliar.Controls.Add(vCtrlDivSalto);
                pvwAcademica.Controls.Add(vControlGrid);
                pvwFamiliar.Controls.Add(vCtrlDivSalto);
            }
            if (pNombreDiv == "LABORAL")
            {
                pvwFamiliar.Controls.Add(vCtrlDivSalto);
                pvwLaboral.Controls.Add(vControlGrid);
                pvwFamiliar.Controls.Add(vCtrlDivSalto);
            }
            if (pNombreDiv == "COMPETENCIAS")
            {
                pvwFamiliar.Controls.Add(vCtrlDivSalto);
                pvwCompetencias.Controls.Add(vControlGrid);
                pvwFamiliar.Controls.Add(vCtrlDivSalto);
            }
            if (pNombreDiv == "ADICIONAL")
            {
                pvwFamiliar.Controls.Add(vCtrlDivSalto);
                pvwAdicional.Controls.Add(vControlGrid);
                pvwFamiliar.Controls.Add(vCtrlDivSalto);
            }
        }

        public void GenerarCtrlComboBox(string pNombreDiv, XElement pXmlCampo)
        {
            HtmlGenericControl vCtrlDiv = new HtmlGenericControl("div");
            HtmlGenericControl vCtrlLabel = new HtmlGenericControl("label");
            vCtrlLabel.Attributes.Add("style", "float: left; font-weight: bold;");
            HtmlGenericControl vCtrlDivValor = new HtmlGenericControl("div");
            vCtrlDivValor.Attributes.Add("style", "float: left;");
            vCtrlLabel.InnerHtml = UtilXML.ValorAtributo<string>(pXmlCampo.Attribute("NB_CAMPO")) + ":&nbsp;&nbsp;";

            if (pXmlCampo.Element("ITEMS") != null)
            {
                XElement vValor= pXmlCampo.Element("ITEMS").Elements("ITEM").Where(w => UtilXML.ValorAtributo<string>(w.Attribute("FG_SELECCIONADO")) == "1").FirstOrDefault();
                if (vValor != null)
                vCtrlDivValor.InnerHtml = UtilXML.ValorAtributo<string>(vValor.Attribute("NB_TEXTO")) + "&nbsp;&nbsp;";
            }

            vCtrlDiv.Controls.Add(vCtrlLabel);
            vCtrlDiv.Controls.Add(vCtrlDivValor);

            HtmlGenericControl vCtrlDivSalto = new HtmlGenericControl("div");
            vCtrlDivSalto.Attributes.Add("style", "clear: both; height: 10px");

            if (pNombreDiv == "PERSONAL")
            {
                pvwPersonal.Controls.Add(vCtrlDivSalto);
                pvwPersonal.Controls.Add(vCtrlDiv);
            }
            if (pNombreDiv == "FAMILIAR")
            {
                pvwFamiliar.Controls.Add(vCtrlDivSalto);
                pvwFamiliar.Controls.Add(vCtrlDiv);
            }
            if (pNombreDiv == "ACADEMICA")
            {
                pvwAcademica.Controls.Add(vCtrlDivSalto);
                pvwAcademica.Controls.Add(vCtrlDiv);
            }
            if (pNombreDiv == "LABORAL")
            {
                pvwLaboral.Controls.Add(vCtrlDivSalto);
                pvwLaboral.Controls.Add(vCtrlDiv);
            }
            if (pNombreDiv == "COMPETENCIAS")
            {
                pvwCompetencias.Controls.Add(vCtrlDivSalto);
                pvwCompetencias.Controls.Add(vCtrlDiv);
            }
            if (pNombreDiv == "ADICIONAL")
            {
                pvwAdicional.Controls.Add(vCtrlDivSalto);
                pvwAdicional.Controls.Add(vCtrlDiv);
            }
        }

        public void AgregarDiv(XElement pXmlCampo, string pNombreDiv)
        {
            foreach (XElement vXmlControl in pXmlCampo.Elements("CAMPO"))
            {
                string vNbControl = UtilXML.ValorAtributo<string>(vXmlControl.Attribute("CL_TIPO"));
                bool vfgHabilitado = UtilXML.ValorAtributo<bool>(vXmlControl.Attribute("FG_HABILITADO"));

                if (vfgHabilitado)
                {
                    switch (vNbControl)
                    {
                        case "TEXTBOX":
                            GenerarCtrlTxtDiv(pNombreDiv, vXmlControl);
                            break;
                        case "DATEPICKER":
                            GenerarCtrlTxtDiv(pNombreDiv, vXmlControl);
                            break;
                        case "DATEAGE":
                            GenerarCtrlTxtDiv(pNombreDiv, vXmlControl);
                            break;
                        case "TEXTBOXCP":
                            GenerarCtrlTxtDiv(pNombreDiv, vXmlControl);
                            break;
                        case "LISTBOX":
                            GenerarCtrlTxtDiv(pNombreDiv, vXmlControl);
                            break;
                        case "CHECKBOX":
                            GenerarCtrlCheckDiv(pNombreDiv, vXmlControl);
                            break;
                        case "GRID":
                            GenerarCtrlTabla(pNombreDiv, vXmlControl);
                            break;
                        case "NUMERICBOX":
                            GenerarCtrlTxtDiv(pNombreDiv, vXmlControl);
                            break;
                        case "COMBOBOX":
                            GenerarCtrlComboBox(pNombreDiv, vXmlControl);
                            break;
                    }
                }
            }
        }

        protected void CargarInformacion()
        {
            if (vXmlSolicitudPlantilla != null)
            {
                foreach (XElement vXmlContenedor in XElement.Parse(vXmlSolicitudPlantilla).Element("CONTENEDORES").Elements("CONTENEDOR"))
                {
                    if (vXmlContenedor != null)
                    {
                        string vNbContenedor = UtilXML.ValorAtributo<string>(vXmlContenedor.Attribute("ID_CONTENEDOR"));


                        switch (vNbContenedor)
                        {
                            case "PERSONAL":
                                AgregarDiv(vXmlContenedor, vNbContenedor);
                                break;
                            case "FAMILIAR":
                                AgregarDiv(vXmlContenedor, vNbContenedor);
                                break;
                            case "ACADEMICA":
                                AgregarDiv(vXmlContenedor, vNbContenedor);
                                break;
                            case "LABORAL":
                                AgregarDiv(vXmlContenedor, vNbContenedor);
                                break;
                            case "COMPETENCIAS":
                                AgregarDiv(vXmlContenedor, vNbContenedor);
                                break;
                            case "ADICIONAL":
                                AgregarDiv(vXmlContenedor, vNbContenedor);
                                break;

                        }
                    }
                }
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            string vClModulo = "INTEGRACION";
            string vModulo = Request.QueryString["m"];
            if (vModulo != null)
                vClModulo = vModulo;

            cssModulo = Utileria.ObtenerCssModulo(vClModulo);

            if (!Page.IsPostBack)
            {
                vIdSolicitudVS = vIdSolicitud;
                vXmlSolicitudPlantilla = vXmlSolicitud;
                vIdItemFotografia = vIdItemFoto;
                CargarDocumentos();
                CargarInformacion();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vIdSolicitud = null;
                int vIdSolicitudQS = -1;
                if (int.TryParse(Request.QueryString["SolicitudId"], out vIdSolicitudQS))
                    vIdSolicitud = vIdSolicitudQS;
            }

            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            SPE_OBTIENE_SOLICITUD_PLANTILLA_Result vSolicitud = new SPE_OBTIENE_SOLICITUD_PLANTILLA_Result();

            if (vIdPlantillaFormulario != null)
            {
                vSolicitud = nSolicitud.ObtenerSolicitudPlantilla(vIdPlantillaFormulario, null, null);
            }
            else
            {
                vSolicitud = nSolicitud.ObtenerSolicitudPlantilla(null, vIdSolicitud, null);
            }

            vXmlSolicitud = vSolicitud.XML_SOLICITUD_PLANTILLA;
            vXmlValoresSolicitud = vSolicitud.XML_VALORES;
            vXmlDocumentos = vSolicitud.XML_VALORES;
            vIdItemFoto = vSolicitud.ID_ITEM_FOTOGRAFIA;

            if (vSolicitud.FI_FOTOGRAFIA != null)
            {
                rbiFotoEmpleado.DataValue = vSolicitud.FI_FOTOGRAFIA;
            }
        }

        protected void grdDocumentos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdDocumentos.DataSource = vLstDocumentos;
        }
    }
}