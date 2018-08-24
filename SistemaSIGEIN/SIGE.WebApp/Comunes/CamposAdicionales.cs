using SIGE.Entidades;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.Comunes
{
    public class CamposAdicionales
    {
        public DataTable camposAdicionales<T>(IList<T> pEntidadDatos, string pNbCampoXML, RadGrid pCtrlGrid, string pClTabla)
        {
            CampoAdicionalNegocio camposAdicionales = new CampoAdicionalNegocio();
            List<SPE_OBTIENE_C_CAMPO_ADICIONAL_Result> vListCamposAdicionales = camposAdicionales.ObtieneCamposAdicionales(pClReferencia: pClTabla);
            DataTable tablaDatos = ConvertToDataTable(pEntidadDatos, vListCamposAdicionales, pNbCampoXML);

            foreach (SPE_OBTIENE_C_CAMPO_ADICIONAL_Result vCampoAdicional in vListCamposAdicionales)
            {
                GridBoundColumn vBoundColumn = new GridBoundColumn();
                vBoundColumn.DataField = vCampoAdicional.CL_CAMPO;
                vBoundColumn.UniqueName = vCampoAdicional.CL_CAMPO;
                vBoundColumn.HeaderText = vCampoAdicional.NB_CAMPO;
                vBoundColumn.FilterControlWidth = System.Web.UI.WebControls.Unit.Pixel(80);
                vBoundColumn.HeaderStyle.Width = System.Web.UI.WebControls.Unit.Pixel(150);
                vBoundColumn.AutoPostBackOnFilter = true;
                vBoundColumn.CurrentFilterFunction = GridKnownFunction.Contains;
                pCtrlGrid.MasterTableView.Columns.Add(vBoundColumn);
            }
            return tablaDatos;
        }

        public DataTable ConvertToDataTable<T>(IList<T> data, List<SPE_OBTIENE_C_CAMPO_ADICIONAL_Result> pLstCamposAdicionales, string pClCampoXmlAdicionales)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (SPE_OBTIENE_C_CAMPO_ADICIONAL_Result vCampoAdicional in pLstCamposAdicionales)
                table.Columns.Add(vCampoAdicional.CL_CAMPO);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    if (!prop.Name.Equals(pClCampoXmlAdicionales))
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    else
                    {
                        object oCamposAdicionales = prop.GetValue(item);
                        if (oCamposAdicionales != null)
                        {
                            XElement vXmlCamposAdicionales = XElement.Parse(oCamposAdicionales.ToString());
                            foreach (SPE_OBTIENE_C_CAMPO_ADICIONAL_Result vCampoAdicional in pLstCamposAdicionales)
                            {
                                XElement vXmlCampoAdicional = vXmlCamposAdicionales.Elements("CAMPO").FirstOrDefault(f => UtilXML.ValorAtributo<string>(f.Attribute("ID_CAMPO")) == vCampoAdicional.CL_CAMPO);
                                if (vXmlCampoAdicional != null)
                                    row[vCampoAdicional.CL_CAMPO] = UtilXML.ValorAtributo<string>(vXmlCampoAdicional.Attribute("NB_TEXTO"));
                            }
                        }
                    }
                }
                table.Rows.Add(row);
            }
            return table;
        }
    }
}