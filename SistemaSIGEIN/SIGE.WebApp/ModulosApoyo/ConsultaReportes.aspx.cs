using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.ModulosApoyo
{
    public partial class ConsultaReportes : System.Web.UI.Page
    {
        #region Eventos Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ruta = "" ;
                 ruta = HostingEnvironment.ApplicationPhysicalPath.ToString() + @"ModulosApoyo\";
                 DirectoryInfo di = new DirectoryInfo(ruta);
                 foreach (var archivo in di.GetFiles())
                 {
                     if (archivo.Name.EndsWith("mrt"))
                     {
                         Telerik.Web.UI.RadTreeNode nodo = new Telerik.Web.UI.RadTreeNode();                         
                         nodo.Text = archivo.Name;                         
                         nodo.Value=ruta + archivo.Name;
                         rtvReportes.Nodes.Add(nodo);
                     }
                 }
            }
        }

        #endregion

        #region Eventos

        protected void rtvReportes_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
        {
            string ruta;
            ruta=((Telerik.Web.UI.RadTreeView)(sender)).SelectedValue;
            if(ruta != string.Empty)
            {
                StiReport reporte = new StiReport();
                reporte.Load(ruta);
                reporte.Render(false);                
            }
        }

        #endregion
    }
}