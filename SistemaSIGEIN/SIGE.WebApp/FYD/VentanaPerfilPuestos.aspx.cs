using SIGE.Entidades;
using SIGE.Negocio.FormacionDesarrollo;
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
    public partial class VentanaPerfilPuestos : System.Web.UI.Page
    {
        #region Variables

        public int vIdEmpleado
        {
            get { return (int)ViewState["vs_vpvc_id_empleado"]; }
            set { ViewState["vs_vpvc_id_empleado"] = value; }
        }

        public string PuestosComparacion
        {
            get { return (string)ViewState["vs_vpvc_puesto_comparacion"]; }
            set { ViewState["vs_vpvc_puesto_comparacion"] = value; }
        }

        #endregion

        #region Funciones

        private string ConvertToHTMLTable(XElement source)
        {
            string Table = "<Table border=\"1\">";
            string aux = "";
            bool alternateColor = false;

            if (source.Elements("EXP").Count() > 1)
            {
                foreach (XElement item in source.Elements())
                {

                    if (alternateColor)
                    {
                        aux = "<tr style=\"padding: 5px; background-color:#E6E6FA\">";
                    }
                    else
                    {
                        aux = "<tr style=\"padding: 5px;\">";
                    }



                    foreach (XAttribute attr in item.Attributes())
                    {
                        if (attr.Name != "FG_ENCABEZADO")
                        {
                            if (item.Attribute("FG_ENCABEZADO").Value == "1")
                            {
                                aux = aux + "<th style=\"padding: 5px;\">" + attr.Value + "</th>";
                            }
                            else
                            {
                                aux = aux + "<td style=\"padding: 5px;\">" + attr.Value + "</td>";
                            }
                        }
                    }

                    alternateColor = !alternateColor;
                    aux = aux + "</tr>";
                    Table = Table + aux;
                }

                Table = Table + "</Table>";
            }
            else
            {
                Table = "";
            }

            return Table;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                vIdEmpleado = 0;

                if (Request.Params["idEmpleado"] != null)
                {
                    SPE_OBTIENE_M_EMPLEADO_Result emp = new SPE_OBTIENE_M_EMPLEADO_Result();
                    vIdEmpleado = int.Parse(Request.Params["idEmpleado"]);
                }

                if (Request.Params["Puestos"] != null)
                {
                    XElement prueba = new XElement("PUESTOS");
                    string[] aux = Request.Params["Puestos"].ToString().Split(',');

                    foreach (string item in aux)
                    {
                        prueba.Add(new XElement("PUESTO", new XAttribute("ID", item)));
                    }

                    PuestosComparacion = prueba.ToString();

                }
            }
        }

        protected void grdpuestos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            int? id = null;

            if (vIdEmpleado != 0)
            {
                id = vIdEmpleado;
            }

            PlanSucesionNegocio neg = new PlanSucesionNegocio();
            grdpuestos.DataSource = neg.obtenerComparacionPuestos(PuestosComparacion, id);
        }

        protected void grdpuestos_ColumnCreated(object sender, Telerik.Web.UI.GridColumnCreatedEventArgs e)
        {
            if (e.Column.UniqueName == "Item")
            {
                e.Column.HeaderStyle.Width = Unit.Pixel(20);
            }
            else
            {
                e.Column.HeaderStyle.Width = Unit.Pixel(150);
            }
        }

        protected void grdpuestos_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = e.Item as GridDataItem;

                if (item.ItemIndex == 9)
                {
                    for (int i = 3; i <= item.Cells.Count - 1; i++)
                    {
                        item.Cells[i].Text = ConvertToHTMLTable(XElement.Parse(item.Cells[i].Text));
                    }
                }
            }
        }
    }
}