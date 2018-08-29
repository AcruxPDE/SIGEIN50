using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaAplicarPruebas : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private XElement vSeleccionXml { get; set; }
        List<SPE_OBTIENE_CANDIDATOS_BATERIA_Result> lstCandidatos = new List<SPE_OBTIENE_CANDIDATOS_BATERIA_Result>();
        public List<E_CANDIDATO> lstCandidatoS
        {
            set { ViewState["lstCandidatoS"] = value; }
            get { return (List<E_CANDIDATO>)ViewState["lstCandidatoS"]; }
        }

        public Guid vIdCandidatosPruebas
        {
            get { return (Guid)ViewState["vs_vIdCandidatosPruebas"]; }
            set { ViewState["vs_vIdCandidatosPruebas"] = value; }
        }

        private int? vIdBateria
        {
            get { return (int?)ViewState["vs_vIdBateria"]; }
            set { ViewState["vs_vIdBateria"] = value; }
        }

        public int? vIdCandidatoBateria
        {
            get { return (int?)ViewState["vs_vIdCandidatoBateria"]; }
            set { ViewState["vs_vIdCandidatoBateria"] = value; }
        }

        public int? vFlBateria
        {
            get { return (int?)ViewState["vs_vFlBateria"]; }
            set { ViewState["vs_vFlBateria"] = value; }
        }

        public Guid? vClToken
        {
            get { return (Guid?)ViewState["vs_vClToken"]; }
            set { ViewState["vs_vClToken"] = value; }
        }

        #endregion

        #region Funciones
     
        protected void CargarDesdeContexto(List<int> pIdCandidatos)
        {

            foreach (int item in pIdCandidatos)
            {
                E_CANDIDATO f = new E_CANDIDATO
                {
                    ID_CANDIDATO = item
                };

                lstCandidatoS.Add(f);
            }


            var vXelementsCandidato = lstCandidatoS.Select(x =>
                                                        new XElement("CANDIDATO",
                                                        new XAttribute("ID_CANDIDATO", x.ID_CANDIDATO))
                                             ).Distinct();
            XElement xmlCandidatos = new XElement("CANDIDATOS", vXelementsCandidato);

            CandidatoNegocio nCandidato = new CandidatoNegocio();
            lstCandidatos = nCandidato.ObtieneCandidatosBateria(xmlCandidatos);

            lstCandidatoS = new List<E_CANDIDATO>();
            foreach (var item in lstCandidatos)
            {
                E_CANDIDATO f = new E_CANDIDATO
                {
                    CL_SOLICITUD = item.CL_SOLICITUD,
                    NB_CANDIDATO = item.NB_CANDIDATO_COMPLETO,
                    ID_CANDIDATO = item.ID_CANDIDATO,
                    FL_BATERIA = ((item.FOLIO_BATERIA != null) ? (item.FOLIO_BATERIA) : ""),
                    ID_BATERIA = ((item.ID_BATERIA != null) ? ((int)item.ID_BATERIA) : 0),
                    CL_TOKEN = item.CL_TOKEN
                };

                lstCandidatoS.Add(f);
            }
        }

        protected void GenerarBaterias(string clTipoAplicacion)
        {
          if (clTipoAplicacion == "EXTERNA")
          ClientScript.RegisterStartupScript(GetType(), "script", "OpenEnviarCorreos();", true);
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            List<SPE_OBTIENE_CANDIDATOS_BATERIA_Result> lstCandidatos = new List<SPE_OBTIENE_CANDIDATOS_BATERIA_Result>();

            if (!IsPostBack)
            {

                lstCandidatoS = new List<E_CANDIDATO>();
                PuestoNegocio negocio = new PuestoNegocio();

                if (Request.Params["pIdCandidatosPruebas"] != null)
                {
                    vIdCandidatosPruebas = Guid.Parse(Request.Params["pIdCandidatosPruebas"].ToString());
                    if (ContextoCandidatosBateria.oCandidatosBateria.Where(w => w.vIdGeneraBaterias == vIdCandidatosPruebas).FirstOrDefault().vListaCandidatos.Count > 0)
                        CargarDesdeContexto(ContextoCandidatosBateria.oCandidatosBateria.Where(w => w.vIdGeneraBaterias == vIdCandidatosPruebas).FirstOrDefault().vListaCandidatos);
                }

                if (Request.Params["pIdBateria"] != null)
                {
                    vIdBateria = int.Parse(Request.Params["pIdBateria"]);
                    if (vIdBateria != null)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "script", "EditPruebas();", true);

                        PruebasNegocio pruebas = new PruebasNegocio();
                        List<SPE_OBTIENE_K_PRUEBA_Result> vLstPruebas = new List<SPE_OBTIENE_K_PRUEBA_Result>();
                        vLstPruebas = pruebas.Obtener_K_PRUEBA(pIdBateria: vIdBateria, pFgAsignada: true);
                    }
                }

            }
        }
      
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/IDP/AplicacionPruebas.aspx");
        }

        protected void btnDelCandidato_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grdCandidatos.SelectedItems)
            {

                int dataKey = int.Parse(item.GetDataKeyValue("ID_CANDIDATO").ToString());
                E_CANDIDATO es = lstCandidatoS.Where(t => t.ID_CANDIDATO == dataKey).FirstOrDefault();

                if (es != null)
                {
                    lstCandidatoS.Remove(es);
                    ContextoCandidatosBateria.oCandidatosBateria.Where(w=> w.vIdGeneraBaterias == vIdCandidatosPruebas).FirstOrDefault().vListaCandidatos.RemoveAll(w => w == es.ID_CANDIDATO);
                }
            }
            grdCandidatos.Rebind();
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

            string pParameter = e.Argument;
            if (pParameter.Equals("Puesto"))
            {
                //Pruebas();
            }
            else
            {

                E_SELECTOR vSeleccion = new E_SELECTOR();
                if (pParameter != null)
                    vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);
                List<E_SELECTOR_CANDIDATO> vSeleccionados = JsonConvert.DeserializeObject<List<E_SELECTOR_CANDIDATO>>(vSeleccion.oSeleccion.ToString());

                if (vSeleccionados.Count > 0)
                {
                    foreach (var item in vSeleccionados)
                    {
                        E_CANDIDATO f = new E_CANDIDATO
                        {
                            ID_CANDIDATO = item.idCandidato
                        };

                        lstCandidatoS.Add(f);
                    }

                    var vXelementsCandidato = lstCandidatoS.Select(x =>
                                                                new XElement("CANDIDATO",
                                                                new XAttribute("ID_CANDIDATO", x.ID_CANDIDATO))
                                                     ).Distinct();
                    XElement xmlCandidatos = new XElement("CANDIDATOS", vXelementsCandidato);

                    CandidatoNegocio nCandidato = new CandidatoNegocio();
                    lstCandidatos = nCandidato.ObtieneCandidatosBateria(xmlCandidatos);

                    lstCandidatoS = new List<E_CANDIDATO>();
                    foreach (var item in lstCandidatos)
                    {
                        E_CANDIDATO f = new E_CANDIDATO
                        {
                            CL_SOLICITUD = item.CL_SOLICITUD,
                            NB_CANDIDATO = item.NB_CANDIDATO_COMPLETO,
                            ID_CANDIDATO = item.ID_CANDIDATO,
                            FL_BATERIA = ((item.FOLIO_BATERIA != null) ? (item.FOLIO_BATERIA) : ""),
                            ID_BATERIA = ((item.ID_BATERIA != null) ? ((int)item.ID_BATERIA) : 0)
                        };

                        lstCandidatoS.Add(f);
                    }

                    grdCandidatos.Rebind();

                }
            }

        }

        protected void grdCandidatos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdCandidatos.DataSource = lstCandidatoS;
            if (lstCandidatoS.Count > 1)
            {
                btnAplicacionInterna.Enabled = false;
            }
        }

        protected void grdCandidatos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridDataItem item = e.Item as GridDataItem;
                var id_bateria = item.GetDataKeyValue("ID_BATERIA").ToString();
                if (id_bateria != "0")
                {
                    int vIdBateria = int.Parse(id_bateria);
                    //UtilMensajes.MensajeResultadoDB(rwmAlertas, "FALTA FUNCIONALIDAD", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");

                    PruebasNegocio nPruebas = new PruebasNegocio();
                    var resultado = nPruebas.EliminaRespuestasBaterias(vIdBateria, vClUsuario, vNbPrograma);
                    string vMensaje = resultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    if (resultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                    {
                        UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, resultado.CL_TIPO_ERROR, pCallBackFunction: "");
                    }
                    else
                    {
                        UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                    }

                }
                else
                {

                    UtilMensajes.MensajeResultadoDB(rwmAlertas, "No cuenta con baterías creadas", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                }
            }
        }

        protected void btnAplicacionExterna_Click(object sender, EventArgs e)
        {
            GenerarBaterias("EXTERNA");
        }

    }
}