using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Negocio.Administracion;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using Telerik.Web.UI;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;
using System.Net;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaValoresGenericos : System.Web.UI.Page
    {

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        public int vIdCatalogoValor
        {
            set { ViewState["vs_vIdCatalogoValor"] = value; }
            get { return (int)ViewState["vs_vIdCatalogoValor"]; }
        }

        public int vIdCatalogoLista
        {
            set { ViewState["vs_vIdCatalogoLista"] = value; }
            get { return (int)ViewState["vs_vIdCatalogoLista"]; }
        }

        //public string usuario = "";
        //public string programa = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {

                if (Request.Params["ID"] != null)
                {
                    vIdCatalogoValor = int.Parse(Request.Params["ID"]);
                    vIdCatalogoLista = int.Parse(Request.Params["ID_LISTA"]);

                    if (vIdCatalogoValor != 0)
                    {
                        SPE_OBTIENE_C_CATALOGO_VALOR_Result catalogo = new SPE_OBTIENE_C_CATALOGO_VALOR_Result();

                        catalogo = getValor(Request.Params["ID"]);

                        txtClValor.Text = catalogo.CL_CATALOGO_VALOR;
                        txtDsValor.Text = catalogo.DS_CATALOGO_VALOR;
                        txtNbValor.Text = catalogo.NB_CATALOGO_VALOR;

                        txtClValor.Enabled = false;
                    }
                }
            }

        }

        protected void btnGuardarRegistro_Click(object sender, EventArgs e)
        {
            SPE_OBTIENE_C_CATALOGO_VALOR_Result catalogo = new SPE_OBTIENE_C_CATALOGO_VALOR_Result();


            string tipoTransaccion = "I";

            if (vIdCatalogoValor != 0)
            {
                tipoTransaccion = "A";
                catalogo = getValor(vIdCatalogoValor.ToString());
            }

            catalogo.CL_CATALOGO_VALOR  = txtClValor.Text;
            catalogo.DS_CATALOGO_VALOR  = txtDsValor.Text;
            catalogo.NB_CATALOGO_VALOR = txtNbValor.Text;
            catalogo.ID_CATALOGO_LISTA = Convert.ToInt32(vIdCatalogoLista);

            CatalogoValorNegocio operaciones = new CatalogoValorNegocio();
            E_RESULTADO vResultado = operaciones.InsertaActualiza_C_CATALOGO_VALOR(tipoTransaccion, catalogo, vClUsuario, vNbPrograma);


           //E_RESULTADO vResultado = negocio.InsertaActualiza_C_AREA_INTERES(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), usuario: vClUsuario, programa: vNbPrograma, v_c_area_interes: vExperienciaProfesional);
            // = nRol.InsertaActualizaRoles(vClOperacion, vRol, vFunciones, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 180);
        }

        protected SPE_OBTIENE_C_CATALOGO_VALOR_Result getValor(string idValorCatalogo) {

            CatalogoValorNegocio negocio = new CatalogoValorNegocio();

            List<SPE_OBTIENE_C_CATALOGO_VALOR_Result> lista = negocio.Obtener_C_CATALOGO_VALOR(ID_CATALOGO_VALOR: vIdCatalogoValor);

            var q = from o in lista
                    where o.ID_CATALOGO_VALOR == int.Parse(idValorCatalogo)
                    select new SPE_OBTIENE_C_CATALOGO_VALOR_Result
                    {
                        ID_CATALOGO_LISTA = o.ID_CATALOGO_LISTA
                        ,NB_CATALOGO_VALOR = o.NB_CATALOGO_VALOR
                        ,DS_CATALOGO_VALOR = o.DS_CATALOGO_VALOR
                        ,ID_CATALOGO_VALOR = o.ID_CATALOGO_VALOR
                        ,CL_CATALOGO_VALOR = o.CL_CATALOGO_VALOR
                    };

            return q.FirstOrDefault();

        }

        
    }
}