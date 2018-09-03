using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.FYD
{
    public partial class ToolTipEmpleado : System.Web.UI.UserControl
    {
        #region Propieades

        public string vClEmpleado
        {
            get
            {
                return (string)ViewState["vs_tte_cl_empleado"];
            }
            set
            {
                ViewState["vs_tte_cl_empleado"] = value;
            }
        }

        public List<E_COMPARACION_COMPETENCIA> ListaDatos { 
            get {
                return (List<E_COMPARACION_COMPETENCIA>)ViewState["vs_tte_lista_datos"];
            }
            set {                
                ViewState["vs_tte_lista_datos"] = value;
            }
        }

        #endregion

        #region Metodos
      
        private void cargarGrid()
        {
            rgDatos.DataSource = ListaDatos;
            rgDatos.DataBind();
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //lblPrueba.InnerText = vClEmpleado;            
        }

        protected void rgDatos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {            
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            cargarGrid();
        }


    }
}