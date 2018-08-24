using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class AplicacionPruebas : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        List<SPE_OBTIENE_K_PRUEBA_Result> vPruebasCandidatos;
        private XElement BATERIASSELECCIONADAS { get; set; }

        private string vIdBateriaSeleccionada {
            get { return (string)ViewState["vs_ap_id_bateria_seleccionada"]; }
            set { ViewState["vs_ap_id_bateria_seleccionada"] = value; }
        }

        #endregion

        #region Funciones

        private void EnvioCorreoPruebas()
        {

            List<E_BATERIA_PRUEBAS> baterias = new List<E_BATERIA_PRUEBAS>();

            if (grdPruebas.SelectedItems.Count > 0)
            {
                GridDataItem items = (GridDataItem)grdPruebas.SelectedItems[0];
                if (items.GetDataKeyValue("ID_BATERIA") != null)
                {
                    foreach (GridDataItem item in grdPruebas.SelectedItems)
                    {
                        if (item.GetDataKeyValue("ID_BATERIA") != null)
                        {
                            string vMensaje = "Estimado(a) {0}: <br/><br>Bienvenido al sistema automatizado para aplicación de pruebas psicométricas SIGEIN por medio de Internet.<br/><br/> Por favor haz clic en la siguiente liga para comenzar tu aplicación de pruebas via electrónica: <br/><br/>" +
                                                "<a href=\"{1}\">Aplicación de pruebas psicométricas </a> <br/><br/>" +
                                                "¡Te deseamos la mejor de las suertes!";

                            string vIdBateria = item.GetDataKeyValue("ID_BATERIA").ToString();
                            string vClToken = item.GetDataKeyValue("CL_TOKEN").ToString();
                            string vIdCandidato = item.GetDataKeyValue("ID_CANDIDATO").ToString();

                            string vClCorreoElectronico = (item.FindControl("txtCorreo") as RadTextBox).Text;
                            string vNbCandidato = item["NB_CANDIDATO"].Text.ToString();
                            string vNbHost = ContextoUsuario.nbHost;// HttpContext.Current.Request.Url.Host;

                            //string pagina = "Default.aspx";
                            string pagina = "PruebaBienvenida.aspx";

                            if (vClCorreoElectronico != "" && vClCorreoElectronico != "&nbsp;")
                            {
                                //add item para actualizar
                                E_BATERIA_PRUEBAS bat = new E_BATERIA_PRUEBAS();
                                bat.ID_BATERIA = int.Parse(vIdBateria);
                                bat.CL_CORREO_ELECTRONICO = vClCorreoElectronico;
                                baterias.Add(bat);
                                //  string url = String.Format("http://{0}/IDP/Pruebas/{1}?ty=sig&ID={2}&T={3}", host, pagina, vIdBateria, vClToken);
                                string myUrl = ResolveUrl("~/IDP/Pruebas/");
                                string url = String.Format("{0}{5}{1}?ID={2}&T={3}&idCandidato={4}", vNbHost, pagina, vIdBateria, vClToken, vIdCandidato, myUrl);
                                Mail mail = new Mail(ContextoApp.mailConfiguration);
                                mail.addToAddress(vClCorreoElectronico, null);
                                //mail.Send("Pruebas SIGEIN", String.Format("Estimado(a) {0},<br/><br/>Para realizar las prueba click en el siguiente enlace: <br/><br/>{1}<br/><br/>Saludos cordiales.", vNbCandidato, url));
                                mail.Send("Aplicación de pruebas psicométricas", String.Format(vMensaje, vNbCandidato, url));
                            }
                        }
                    }
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "No se ha seleccionado una bateria.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
            }

            if (baterias.Count() > 0)
            {
                //Actualizar baterias a estatus enviadas
                var vXelementsBateria = baterias.Select(x =>
                                         new XElement("BATERIA",
                                         new XAttribute("ID_BATERIA", x.ID_BATERIA),
                                         new XAttribute("CL_CORREO_ELECTRONICO", x.CL_CORREO_ELECTRONICO))
                             );
                XElement xmlPruebas = new XElement("BATERIAS", vXelementsBateria);
                PruebasNegocio pruebas = new PruebasNegocio();
                pruebas.registra_EnvioBateria(xmlPruebas, vClUsuario, vNbPrograma);
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Pruebas enviadas.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, 400, 150, "");
            }
        }

        private void EliminarRespuestaBateria(int pIdBateria)
        {
            PruebasNegocio nPruebas = new PruebasNegocio();
            var vResultado = nPruebas.EliminaRespuestasBaterias(pIdBateria, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                //grdPruebas.Rebind();
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
            }
        }

        private void EliminarRespuestaPrueba(int pIdPrueba)
        {
            PruebasNegocio nPruebas = new PruebasNegocio();
            var vResultado = nPruebas.EliminaRespuestasPrueba(pIdPrueba, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                //grdPruebas.Rebind();
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = (ContextoUsuario.oUsuario != null ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO");
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {

            }

        }

        //protected void btnAddTest_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/IDP/AgregarPruebas.aspx");

        //}

        protected void grdPruebas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            vPruebasCandidatos = new List<SPE_OBTIENE_K_PRUEBA_Result>();
            PruebasNegocio pruebas = new PruebasNegocio();
            var vCandidatosPruebas = pruebas.ObtieneBateria();

            grdPruebas.DataSource = vCandidatosPruebas;
            //grdPruebas.Rebind();
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            EnvioCorreoPruebas();
            grdPruebas.Rebind();
        }

        protected void grdPruebas_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            PruebasNegocio pruebas = new PruebasNegocio();
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            string V_ID_BATERIA = dataItem.GetDataKeyValue("ID_BATERIA").ToString();
            var vCandidatosPruebas = pruebas.Obtener_K_PRUEBA(pIdBateria: int.Parse(V_ID_BATERIA),pFgAsignada: true);
            e.DetailTableView.DataSource = vCandidatosPruebas;
        }

        protected void grdPruebas_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExpandCollapseCommandName)
            {
                foreach (GridItem item in e.Item.OwnerTableView.Items)
                {
                    if (item.Expanded && item != e.Item)
                    {
                        item.Expanded = false;
                    }
                }
            }

            if (e.CommandName == "DeleteBateria")
            {
                GridDataItem vItem = e.Item as GridDataItem;

                if (vItem.GetDataKeyValue("ID_BATERIA") != null)
                {
                    EliminarRespuestaBateria(int.Parse(vItem.GetDataKeyValue("ID_BATERIA").ToString()));
                }

                vItem.OwnerTableView.Rebind(); 
            }

            if (e.CommandName == "DeletePrueba")
            {
                GridDataItem vItem = e.Item as GridDataItem;

                if (vItem.GetDataKeyValue("ID_PRUEBA") != null)
                {
                    EliminarRespuestaPrueba(int.Parse(vItem.GetDataKeyValue("ID_PRUEBA").ToString()));
                }

                vItem.OwnerTableView.Rebind(); 
            }

            //if (e.CommandName == RadGrid.SelectCommandName && e.Item is GridDataItem)
            //{
            //    GridDataItem dataItem = (GridDataItem)e.Item;
            //    vIdBateriaSeleccionada = dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["ID_BATERIA"].ToString();                
            //}

            //if (e.CommandName == RadGrid.DeselectCommandName && e.Item is GridDataItem)
            //{
            //    //GridDataItem dataItem = (GridDataItem)e.Item;
            //    vIdBateriaSeleccionada = null;                
            //}


        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
                List<E_BATERIA_PRUEBAS> vLstBaterias = new List<E_BATERIA_PRUEBAS>();
              

                foreach (GridDataItem item in grdPruebas.SelectedItems)
               {
                   int vIdBateria = int.Parse(item.GetDataKeyValue("ID_BATERIA").ToString());
                   vLstBaterias.Add( new E_BATERIA_PRUEBAS
                    {
                        ID_BATERIA = vIdBateria
                    }
                       );
               }
            var vXelements = vLstBaterias.Select(x => new XElement("BATERIAS",
                                                        new XAttribute ("ID_BATERIA", x.ID_BATERIA)));

            BATERIASSELECCIONADAS = new XElement("SELECCIONADOS", vXelements);

            PruebasNegocio Negocio = new PruebasNegocio();
            var vResultado = Negocio.EliminaBateriaPruebas(BATERIASSELECCIONADAS.ToString(), vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindow");
        }

    }
}