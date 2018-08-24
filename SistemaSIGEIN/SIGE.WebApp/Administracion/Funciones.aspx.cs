using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.Administracion
{
    public partial class Funciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void grdFunciones_NeedDataSource(object sender, Telerik.Web.UI.TreeListNeedDataSourceEventArgs e)
        {
            FuncionNegocio nFunciones = new FuncionNegocio();
            grdFunciones.DataSource = nFunciones.ObtieneFunciones(E_TIPO_FUNCION.TODOS);
        }
    }
}