using Newtonsoft.Json;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Negocio.MetodologiaCompensacion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.MPC
{
    public partial class ConsultaBono : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        int vAnterior = 1;
        int vActual = 1;

        public int vIdTabulador
        {
            get { return (int)ViewState["vsIdTabulador"]; }
            set { ViewState["vsIdTabulador"] = value; }
        }

        public List<E_SELECCION_PERIODOS_DESEMPENO> oLstPeriodos
        {
            get { return (List<E_SELECCION_PERIODOS_DESEMPENO>)ViewState["vs_lista_periodos"]; }
            set { ViewState["vs_lista_periodos"] = value; }
        }

        private void AgregarPeriodos(string pPeriodos)
        {
            List<E_SELECCION_PERIODOS_DESEMPENO> vLstPeriodos = JsonConvert.DeserializeObject<List<E_SELECCION_PERIODOS_DESEMPENO>>(pPeriodos);

            foreach (E_SELECCION_PERIODOS_DESEMPENO item in vLstPeriodos)
            {
                if (oLstPeriodos.Where(t => t.idPeriodo == item.idPeriodo).Count() == 0)
                {
                    oLstPeriodos.Add(new E_SELECCION_PERIODOS_DESEMPENO
                    {
                        idPeriodo = item.idPeriodo,
                        clPeriodo = item.clPeriodo,
                        nbPeriodo = item.nbPeriodo,
                        dsPeriodo = item.dsPeriodo
                    });

                    ContextoBono.oLstPeriodosBonos.Add(new E_SELECCION_PERIODOS_DESEMPENO
                    {
                        idPeriodo = item.idPeriodo,
                        clPeriodo = item.clPeriodo,
                        nbPeriodo = item.nbPeriodo,
                        dsPeriodo = item.dsPeriodo,
                        dsNotas = item.dsNotas,
                        feInicio = item.feInicio,
                        feTermino = item.feTermino
                    });
                }
            }
            rgComparativos.Rebind();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
             vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    vIdTabulador = int.Parse((Request.QueryString["ID"]));
                    TabuladoresNegocio nTabulador = new TabuladoresNegocio();
                    var vTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdTabulador).FirstOrDefault();
                    txtClaveTabulador.InnerText = vTabulador.CL_TABULADOR;
                    txtDescripción.InnerText = vTabulador.DS_TABULADOR;
                    txtVigencia.InnerText = vTabulador.FE_VIGENCIA.ToString("dd/MM/yyyy");
                    txtFecha.InnerText = vTabulador.FE_VIGENCIA.ToString("dd/MM/yyyy");
                    txtPuestos.InnerText = vTabulador.CL_TIPO_PUESTO;

 
                }

                oLstPeriodos = new List<E_SELECCION_PERIODOS_DESEMPENO>();
                ContextoBono.oLstPeriodosBonos = new List<E_SELECCION_PERIODOS_DESEMPENO>();
            }
        }

        protected void rgComparativos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgComparativos.DataSource = oLstPeriodos;
        }

        protected void rgComparativos_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item is GridEditableItem)
            {
                GridEditableItem item = (GridEditableItem)e.Item;

                int? vIdPeriodo = int.Parse(item.GetDataKeyValue("idPeriodo").ToString());

                if (vIdPeriodo != null)
                {
                    var vItem = oLstPeriodos.First(x => x.idPeriodo == vIdPeriodo);
                    oLstPeriodos.Remove(vItem);
                    var vItemContexto = ContextoBono.oLstPeriodosBonos.First(x => x.idPeriodo == vIdPeriodo);
                    ContextoBono.oLstPeriodosBonos.Remove(vItemContexto);
                    rgComparativos.Rebind();
                }
            }
        }

        protected void ramConsultas_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            E_ARREGLOS vSeleccion = new E_ARREGLOS();
            E_SELECTOR vSeleccionBono = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
            {
                vSeleccion = JsonConvert.DeserializeObject<E_ARREGLOS>(pParameter);
                vSeleccionBono = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);
            }

            if (vSeleccionBono.clTipo == "PERIODODESEMPENO")
            {
                AgregarPeriodos(vSeleccionBono.oSeleccion.ToString());
            }
        }

        protected void rgComparativos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgComparativos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgComparativos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgComparativos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgComparativos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgComparativos.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}