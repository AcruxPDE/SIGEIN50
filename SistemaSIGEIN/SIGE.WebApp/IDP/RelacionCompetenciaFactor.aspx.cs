using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using SIGE.Negocio.Administracion;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Entidades.Administracion;
using Telerik.Web.UI;
using System.Xml.Linq;
using SIGE.WebApp.Comunes;
using SIGE.Entidades.Externas;


namespace SIGE.WebApp.IDP
{
    public partial class RelacionCompetenciaFactor : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private List<E_SELECCIONADOS> vlstFactores
        {
            get { return (List<E_SELECCIONADOS>)ViewState["vs_vlstFactores"]; }
            set { ViewState["vs_vlstFactores"] = value; }
        }

        private List<E_SELECCIONADOS> vlstCompetencias
        {
            get { return (List<E_SELECCIONADOS>)ViewState["vs_vlstCompetencias"]; }
            set { ViewState["vs_vlstCompetencias"] = value; }
        }

        private int vIdFactor
        {
            get { return (int)ViewState["vs_vIdFactor"]; }
            set { ViewState["vs_vIdFactor"] = value; }
        }

        private int vIdPrueba
        {
            get { return (int)ViewState["vs_vIdPrueba"]; }
            set { ViewState["vs_vIdPrueba"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                vlstFactores = new List<E_SELECCIONADOS>();
                vlstCompetencias = new List<E_SELECCIONADOS>();
            }
        }

        protected void rgdPruebas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PruebasNegocio nPruebasNegocio = new PruebasNegocio();
            var vPruebas = nPruebasNegocio.Obtener_C_PRUEBA();
            rgdPruebas.DataSource = vPruebas;
        }

        protected void rgdFactores_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgdFactores.DataSource = vlstFactores;
        }

        protected void rgdCompetencias_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgdCompetencias.DataSource = vlstCompetencias;
        }

        protected void rgdPruebas_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridDataItem item = (GridDataItem)rgdPruebas.SelectedItems[0];
            vIdPrueba = int.Parse(item.GetDataKeyValue("ID_PRUEBA").ToString());
            PruebasNegocio nPruebasNegocio = new PruebasNegocio();
            vlstFactores = nPruebasNegocio.ObtienePruebasFactores(pID_SELECCION: vIdPrueba, pCL_SELECCION: "FACTORES").Select(s => new E_SELECCIONADOS
            { 
            ID_SELECCION = s.ID_SELECCION,
            NB_SELECCION = s.NB_SELECCION
            }).ToList();
            rgdFactores.Rebind();
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = false;
        }
       
        protected void rgdFactores_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridDataItem item = (GridDataItem)rgdFactores.SelectedItems[0];
            vIdFactor = int.Parse(item.GetDataKeyValue("ID_SELECCION").ToString());
            CargaCompetencias();
            btnAgregar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        protected void CargaCompetencias() {
            PruebasNegocio nPruebasNegocio = new PruebasNegocio();
            vlstCompetencias = nPruebasNegocio.ObtienePruebasFactores(pID_SELECCION: vIdFactor, pCL_SELECCION: "COMPETENCIAS").Select(s => new E_SELECCIONADOS
            {
                ID_SELECCION = s.ID_SELECCION,
                CL_SELECCION = s.CL_SELECCION,
                NB_SELECCION = s.NB_SELECCION,
                DS_SELECCION = s.DS_SELECCION
            }).ToList();
            rgdCompetencias.Rebind();
        }

        protected void ramRelacion_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            E_SELECCIONES vSeleccion = new E_SELECCIONES();
            string pParameter = e.Argument;

            if (pParameter != null)
                vSeleccion = JsonConvert.DeserializeObject<E_SELECCIONES>(pParameter);

            InsertarCompetencias(vSeleccion.arrCompetencia);
        }

        protected void InsertarCompetencias(List<int> pIdsSeleccionados)
        {
            var vXelements = pIdsSeleccionados.Select(x =>
                                         new XElement("COMPETENCIA",
                                         new XAttribute("ID_COMPETENCIA", x)
                              ));
            XElement SELECCIONADOS =
            new XElement("COMPETENCIAS", vXelements
                );

            PruebasNegocio nPruebas = new PruebasNegocio();
            E_RESULTADO vResultado = nPruebas.InsertaCompetenciaFactor(SELECCIONADOS.ToString(), vIdFactor, vIdPrueba, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseWindowC");
            CargaCompetencias();
        }

        //protected void rgdCompetencias_ItemCommand(object sender, GridCommandEventArgs e)
        //{
        //    if (e.CommandName == "Delete")
        //    {
        //        GridDataItem item = e.Item as GridDataItem;
        //        EliminarCompetencia(int.Parse(item.GetDataKeyValue("ID_SELECCION").ToString()));
        //    }
        //}

        protected void EliminarCompetencia(XElement pCompetencias)
        {
            PruebasNegocio nPruebas = new PruebasNegocio();
            E_RESULTADO vResultado = nPruebas.EliminaCompetenciaFactor(vIdFactor, pCompetencias.ToString(), vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseWindowC");
            CargaCompetencias();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

            XElement vXmlCompetencias = new XElement("COMPETENCIAS");
            foreach (GridDataItem item in rgdCompetencias.SelectedItems)
                vXmlCompetencias.Add(new XElement("COMPETENCIA",
                                  new XAttribute("ID_COMPETENCIA", item.GetDataKeyValue("ID_SELECCION").ToString())));
            EliminarCompetencia(vXmlCompetencias);
        }
      
    }

    public class E_SELECCIONES
    {
        public string clTipo { set; get; }
        public List<int> arrCompetencia { set; get; }
    }

}