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

namespace SIGE.WebApp.IDP
{
    public partial class ConsultasComparativas : System.Web.UI.Page
    {
        private List<int> vCandidatos
        {
            get { return (List<int>)ViewState["vs_vTabuladores"]; }
            set { ViewState["vs_vTabuladores"] = value; }
        }

        private List<E_CANDIDATO> vlstCandidato
        {
            get { return (List<E_CANDIDATO>)ViewState["vs_vlstCandidato"]; }
            set { ViewState["vs_vlstCandidato"] = value; }
        }

        private List<E_PUESTO> vlstPuesto
        {
            get { return (List<E_PUESTO>)ViewState["vs_vlstPuestos"]; }
            set { ViewState["vs_vlstPuestos"] = value; }
        }

        public Guid? vIdPuestoVsCandidatos
        {
            get { return (Guid?)ViewState["vs_vIdPersonaVsCandidatos"]; }
            set { ViewState["vs_vIdPersonaVsCandidatos"] = value; }
        }

         public Guid? vIdCandidatoVsPuestos
        {
            get { return (Guid?)ViewState["vs_vIdCandidatoVsPuestos"]; }
            set { ViewState["vs_vIdCandidatoVsPuestos"] = value; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vlstCandidato = new List<E_CANDIDATO>();
                vlstPuesto = new List<E_PUESTO>();


                if(Request.Params["pClTipoConsulta"] != null)
                    if (Request.Params["pClTipoConsulta"] == "GLOBAL")
                    {
                        rpvConsultaGlobal.Selected = true;

                        if (Request.Params["pIdCandidato"] != null)
                        {
                            CandidatoNegocio nCandidato = new CandidatoNegocio();
                            var vCandidatos = nCandidato.ObtieneCandidato(pIdCandidato: int.Parse(Request.Params["pIdCandidato"].ToString())).ToList();
                            if (vCandidatos != null)
                            {

                                rlbCandidatoGlobal.DataSource = vCandidatos;
                                rlbCandidatoGlobal.DataTextField = "NB_CANDIDATO_COMPLETO";
                                rlbCandidatoGlobal.DataValueField = "ID_CANDIDATO";
                                rlbCandidatoGlobal.DataBind();

                                rlbCandidatoGlobal.SelectedValue = vCandidatos.FirstOrDefault().ID_CANDIDATO.ToString();

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "setValueVariable('" + vCandidatos.FirstOrDefault().ID_CANDIDATO.ToString() + "');", true);
                            }

                        }
                            
                    }
                    else if (Request.Params["pClTipoConsulta"] == "PSVP")
                    {
                        rpvPuestoPersonas.Selected = true;

                        //if (Request.Params["pIdPuestoVsCandidatos"].ToString() != null)
                        //    vIdPuestoVsCandidatos = Guid.Parse(Request.Params["pIdPuestoVsCandidatos"].ToString());

                        //vIdCandidatoVsPuestos = Guid.NewGuid();
                        List<E_CANDIDATO> ListaCandidatos = new List<E_CANDIDATO>();

                        if (Request.Params["candidatos"] != null)
                        {
                            ListaCandidatos = JsonConvert.DeserializeObject<List<E_CANDIDATO>>(Request.Params["candidatos"].ToString());
                        }

                         CandidatoNegocio nCandidato = new CandidatoNegocio();
                         var vCandidatos = nCandidato.ObtieneCandidato();
                        // var vCandidatosSeleccionados = vCandidatos.Where(w => ContextoConsultasComparativas.oPuestoVsCandidatos.Where(t => t.vIdPuestoVsCandidatos == vIdPuestoVsCandidatos).FirstOrDefault().vListaCandidatos.Contains(w.ID_CANDIDATO)).ToList();
                         var vCandidatosSeleccionados = vCandidatos.Where(w => ListaCandidatos.Select(s=>s.ID_CANDIDATO).Contains(w.ID_CANDIDATO)).ToList();


                         foreach (var item in vCandidatosSeleccionados)
                         {
                             if (vlstCandidato.Where(w => w.ID_CANDIDATO == item.ID_CANDIDATO).Count() == 0)
                             {
                                 vlstCandidato.Add(new E_CANDIDATO { NB_CANDIDATO = item.NB_CANDIDATO + " " + item.NB_APELLIDO_PATERNO + " " + item.NB_APELLIDO_MATERNO, CL_SOLICITUD = item.CL_SOLICITUD, CL_CORREO_ELECTRONICO = item.CL_CORREO_ELECTRONICO, ID_CANDIDATO = item.ID_CANDIDATO });
                             }
                         }
                         ScriptManager.RegisterStartupScript(this, this.GetType(), "", "setValueVariable2('" + vCandidatosSeleccionados.FirstOrDefault().ID_CANDIDATO.ToString() + "');", true);
                    }
                    else if (Request.Params["pClTipoConsulta"] == "PVPS")
                    {
                        vIdPuestoVsCandidatos = Guid.NewGuid();
                        vIdCandidatoVsPuestos = Guid.NewGuid();
                        // rtsConsultas.Tabs[0].Selected = true;

                        if (ContextoConsultasComparativas.oPuestoVsCandidatos == null)
                        {
                            ContextoConsultasComparativas.oPuestoVsCandidatos = new List<E_PUESTO_VS_CANDIDATOS>();
                        }

                        if (ContextoConsultasComparativas.oCandidatoVsPuestos == null)
                        {
                            ContextoConsultasComparativas.oCandidatoVsPuestos = new List<E_CANDIDATO_VS_PUESTOS>();
                        }

                        ContextoConsultasComparativas.oPuestoVsCandidatos.Add(new E_PUESTO_VS_CANDIDATOS { vIdPuestoVsCandidatos = (Guid)vIdPuestoVsCandidatos });
                        ContextoConsultasComparativas.oCandidatoVsPuestos.Add(new E_CANDIDATO_VS_PUESTOS { vIdCandidatoVsPuestos = (Guid)vIdCandidatoVsPuestos });

                        rpvPersonaPuestos.Selected = true;

                        if (Request.Params["pIdCandidato"] != null)
                        {
                            CandidatoNegocio nCandidato = new CandidatoNegocio();
                            var vCandidatos = nCandidato.ObtieneCandidato(pIdCandidato: int.Parse(Request.Params["pIdCandidato"].ToString())).ToList();
                            if (vCandidatos != null)
                            {

                                lstCandidato.DataSource = vCandidatos;
                                lstCandidato.DataTextField = "NB_CANDIDATO_COMPLETO";
                                lstCandidato.DataValueField = "ID_CANDIDATO";
                                lstCandidato.DataBind();

                                lstCandidato.SelectedValue = vCandidatos.FirstOrDefault().ID_CANDIDATO.ToString();

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "setValueVariable3('" + vCandidatos.FirstOrDefault().ID_CANDIDATO.ToString() + "');", true);
                            }

                        }

                    }
                    else
                    {
                        
                vIdPuestoVsCandidatos = Guid.NewGuid();
                vIdCandidatoVsPuestos = Guid.NewGuid();
               // rtsConsultas.Tabs[0].Selected = true;

                if (ContextoConsultasComparativas.oPuestoVsCandidatos == null)
                {
                    ContextoConsultasComparativas.oPuestoVsCandidatos = new List<E_PUESTO_VS_CANDIDATOS>();
                }

                if (ContextoConsultasComparativas.oCandidatoVsPuestos == null)
                {
                    ContextoConsultasComparativas.oCandidatoVsPuestos = new List<E_CANDIDATO_VS_PUESTOS>();
                }

                ContextoConsultasComparativas.oPuestoVsCandidatos.Add(new E_PUESTO_VS_CANDIDATOS { vIdPuestoVsCandidatos = (Guid)vIdPuestoVsCandidatos });
                ContextoConsultasComparativas.oCandidatoVsPuestos.Add(new E_CANDIDATO_VS_PUESTOS { vIdCandidatoVsPuestos = (Guid)vIdCandidatoVsPuestos });

                    }

            }
        }

        protected void CargarDatosCandidatos(List<int> pIdsSeleccionados)
        {
            CandidatoNegocio nCandidato = new CandidatoNegocio();
            var vCandidatos = nCandidato.ObtieneCandidato();
            var vCandidatosSeleccionados = vCandidatos.Where(w => pIdsSeleccionados.Contains(w.ID_CANDIDATO)).ToList();
            foreach (var item in vCandidatosSeleccionados)
                {
                    if (vlstCandidato.Where(w => w.ID_CANDIDATO == item.ID_CANDIDATO).Count() == 0)
                        {
                            vlstCandidato.Add(new E_CANDIDATO { NB_CANDIDATO = item.NB_CANDIDATO +" " + item.NB_APELLIDO_PATERNO + " "+ item.NB_APELLIDO_MATERNO, CL_SOLICITUD = item.CL_SOLICITUD, CL_CORREO_ELECTRONICO = item.CL_CORREO_ELECTRONICO, ID_CANDIDATO = item.ID_CANDIDATO });
                          //  ContextoConsultasComparativas.oPuestoVsCandidatos.Where(t => t.vIdPuestoVsCandidatos == vIdPuestoVsCandidatos).FirstOrDefault().vListaCandidatos.Add(item.ID_CANDIDATO);
                        }
               }
            rgdCandidatos.Rebind();
        }

        protected void CargarDatosPuestos(List<E_SELECCION_PUESTO> pPuestos) {

            foreach (var item in pPuestos)
            {
                if (vlstPuesto.Where(w => w.ID_PUESTO == item.idPuesto).Count() == 0)
                {
                    vlstPuesto.Add(new E_PUESTO
                    {
                        ID_PUESTO = item.idPuesto,
                        CL_PUESTO = item.clPuesto,
                        NB_PUESTO = item.nbPuesto
                    });
                    ContextoConsultasComparativas.oCandidatoVsPuestos.Where(w => w.vIdCandidatoVsPuestos == vIdCandidatoVsPuestos).FirstOrDefault().vListaPuestos.Add(item.idPuesto);
                }
            }

            rgdPuestos.Rebind();
        }

        protected void ramConsultas_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
         {

            E_ARREGLOS vSeleccion = new E_ARREGLOS();
            string pParameter = e.Argument;

            if (pParameter != null)
                vSeleccion = JsonConvert.DeserializeObject<E_ARREGLOS>(pParameter);

            if (vSeleccion.clTipo == "CANDIDATO")
            {
                vCandidatos = vSeleccion.arrCandidatos;
                CargarDatosCandidatos(vSeleccion.arrCandidatos);
            }
            if (vSeleccion.clTipo == "PUESTO_CANDIDATOS")
            {
                List<E_SELECCION_PUESTO> vLstPuestos = JsonConvert.DeserializeObject<List<E_SELECCION_PUESTO>>(vSeleccion.oPuestos.ToString());
                CargarDatosPuestos(vLstPuestos);
            }
        }

        protected void rgdCandidatos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgdCandidatos.DataSource = vlstCandidato;
        }

        protected void rgdCandidatos_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridDataItem item = e.Item as GridDataItem;
                EliminarCandidato(int.Parse(item.GetDataKeyValue("ID_CANDIDATO").ToString()));
            }
        }

        protected void EliminarCandidato( int pIdCandidato) {
            //ContextoConsultasComparativas.oPuestoVsCandidatos.Where(w => w.vIdPuestoVsCandidatos == vIdPuestoVsCandidatos).FirstOrDefault().vListaCandidatos.Remove(pIdCandidato);
            E_CANDIDATO vCandidato = vlstCandidato.Where(w => w.ID_CANDIDATO == pIdCandidato).FirstOrDefault();

            if (vCandidato != null)
            {
                vlstCandidato.Remove(vCandidato);
            }
        }

        protected void EliminarPuesto(int pIdPuesto) {
            ContextoConsultasComparativas.oCandidatoVsPuestos.Where(w => w.vIdCandidatoVsPuestos == vIdCandidatoVsPuestos).FirstOrDefault().vListaPuestos.Remove(pIdPuesto);
            E_PUESTO vPuesto = vlstPuesto.Where(w => w.ID_PUESTO == pIdPuesto).FirstOrDefault();

            if (vPuesto != null)
            {
                vlstPuesto.Remove(vPuesto);
            }
        }

        protected void rgdPuestos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgdPuestos.DataSource = vlstPuesto;
        }

        protected void rgdPuestos_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridDataItem item = e.Item as GridDataItem;
                EliminarPuesto(int.Parse(item.GetDataKeyValue("ID_PUESTO").ToString()));
            }
        }

        protected void rgdCandidatos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void rgdPuestos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgdPuestos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgdPuestos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgdPuestos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgdPuestos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgdPuestos.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }

    public class E_ARREGLOS
    {
        public string clTipo { set; get; }
        public List<int> arrCandidatos { set; get; }
        public object oPuestos { set; get; }
    }
 //[Serializable]
    public class E_SELECCION_PUESTO
    {
        public int idPuesto { get; set; }
        public string clPuesto { get; set; }
        public string nbPuesto { get; set; }
    }
}