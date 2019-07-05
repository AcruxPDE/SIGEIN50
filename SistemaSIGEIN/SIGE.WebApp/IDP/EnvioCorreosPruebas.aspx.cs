using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class EnvioCorreosPruebas : System.Web.UI.Page
    {

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        //public Guid vIdCandidatosPruebas
        //{
        //    get { return (Guid)ViewState["vs_vIdCandidatosPruebas"]; }
        //    set { ViewState["vs_vIdCandidatosPruebas"] = value; }
        //}

        public List<E_BATERIA_PRUEBAS> Lstbaterias
        {
            get { return (List<E_BATERIA_PRUEBAS>)ViewState["vs_Lstbaterias"]; }
            set { ViewState["vs_Lstbaterias"] = value; }
        }

        protected void CargarDesdeContexto(List<E_CANDIDATO> LstCandidatos)
        {
             Lstbaterias = new List<E_BATERIA_PRUEBAS>();
             PruebasNegocio pruebas = new PruebasNegocio();

             foreach (var item in LstCandidatos)
             {
                 var vCandidatosPruebas = pruebas.ObtieneBateria(pIdCandidato: item.ID_CANDIDATO.ToString()).ToList().OrderByDescending(o => o.ID_BATERIA);
                 if (vCandidatosPruebas != null)
                 {
                     Lstbaterias.Add(new E_BATERIA_PRUEBAS
                         {
                             ID_BATERIA = vCandidatosPruebas.FirstOrDefault().ID_BATERIA,
                             ID_CANDIDATO = vCandidatosPruebas.FirstOrDefault().ID_CANDIDATO,
                             NB_CANDIDATO = vCandidatosPruebas.FirstOrDefault().NB_CANDIDATO,
                             CL_TOKEN = vCandidatosPruebas.FirstOrDefault().CL_TOKEN,
                             FL_BATERIA = vCandidatosPruebas.FirstOrDefault().FL_BATERIA,
                             CL_CORREO_ELECTRONICO = vCandidatosPruebas.FirstOrDefault().CL_CORREO_ELECTRONICO
                         });
                 }
             }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
             vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            if (!IsPostBack)
            {
                //if (Request.Params["pIdCandidatosPruebas"] != null)
                //{
                //    vIdCandidatosPruebas = Guid.Parse(Request.Params["pIdCandidatosPruebas"].ToString());
                //    if (ContextoCandidatosBateria.oCandidatosBateria.Where(w => w.vIdGeneraBaterias == vIdCandidatosPruebas).FirstOrDefault().vListaCandidatos.Count > 0)
                //        CargarDesdeContexto(ContextoCandidatosBateria.oCandidatosBateria.Where(w => w.vIdGeneraBaterias == vIdCandidatosPruebas).FirstOrDefault().vListaCandidatos);
                //}

                if (Request.Params["candidatos"] != null)
                {
                    List<E_CANDIDATO> ListaCandidatos = new List<E_CANDIDATO>();
                    ListaCandidatos = JsonConvert.DeserializeObject<List<E_CANDIDATO>>(Request.Params["candidatos"].ToString());

                    CargarDesdeContexto(ListaCandidatos);
                }

            }
        }

        protected void rgCandidatosBateria_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgCandidatosBateria.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgCandidatosBateria.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgCandidatosBateria.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgCandidatosBateria.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgCandidatosBateria.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void rgCandidatosBateria_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgCandidatosBateria.DataSource = Lstbaterias;
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {

            List<E_BATERIA_PRUEBAS> baterias = new List<E_BATERIA_PRUEBAS>();

            if (rgCandidatosBateria.Items.Count > 0)
            {
                GridDataItem items = (GridDataItem)rgCandidatosBateria.Items[0];
                if (items.GetDataKeyValue("ID_BATERIA") != null)
                {
                    foreach (GridDataItem item in rgCandidatosBateria.Items)
                    {
                        if (item.GetDataKeyValue("ID_BATERIA") != null)
                        {
                            string vMensaje = "<p>Estimado(a) <b>{0}:</b> </p> " +
                                " <p>Bienvenido al sistema automatizado para aplicación de pruebas psicométricas SIGEIN. </p>" +
                                " <p> Por favor da click <a href=\"{1}\">aquí</a> para comenzar tu aplicación de pruebas </p>" +
                                " <p>¡Te deseamos la mejor de las suertes! </p>";

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
                                mail.Send("Aplicación de psicométria", String.Format(vMensaje, vNbCandidato, url));
                            }
                        }
                    }
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "No se ha seleccionado una bateria.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
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
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Pruebas enviadas.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, 400, 150, "closeWindow");
            }
    
        }
    }
}