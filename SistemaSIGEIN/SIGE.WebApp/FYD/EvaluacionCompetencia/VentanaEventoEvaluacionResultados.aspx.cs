using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using WebApp.Comunes;

namespace SIGE.WebApp.FYD.EvaluacionCompetencia
{
    public partial class VentanaEventoEvaluacionResultados : System.Web.UI.Page
    {

        #region Variables

        public string cssModulo = String.Empty;
        public string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private int? vIdRol;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vIdEvento {
            get { return (int)ViewState["vs_veer_id_evento"]; }
            set { ViewState["vs_veer_id_evento"] = value; }
        }

        #endregion

        #region Funciones

        private void guardarEvaluacion()
        {
            XElement evaluaciones = new XElement("CALIFICACIONES");

            foreach (GridDataItem item in rgResultados.MasterTableView.Items)
            {

                int vNoCalificacion = -1;

                if ((item.FindControl("rbNivel0") as RadButton).Checked)
                {
                    vNoCalificacion = 0;
                }

                if ((item.FindControl("rbNivel1") as RadButton).Checked)
                {
                    vNoCalificacion = 1;
                }

                if ((item.FindControl("rbNivel2") as RadButton).Checked)
                {
                    vNoCalificacion = 2;
                }

                if ((item.FindControl("rbNivel3") as RadButton).Checked)
                {
                    vNoCalificacion = 3;
                }

                if ((item.FindControl("rbNivel4") as RadButton).Checked)
                {
                    vNoCalificacion = 4;
                }

                if ((item.FindControl("rbNivel5") as RadButton).Checked)
                {
                    vNoCalificacion = 5;
                }
                
                XElement calificacion = new XElement("CALIFICACION",
                    new XAttribute("ID_EVENTO_PARTICIPANTE_COMPETENCIA", item.GetDataKeyValue("ID_EVENTO_PARTICIPANTE_COMPETENCIA").ToString()),
                    new XAttribute("NO_CALIFICACION", vNoCalificacion.ToString()));

                evaluaciones.Add(calificacion);
            }

            EventoCapacitacionNegocio neg = new EventoCapacitacionNegocio();
            E_RESULTADO vResultado = neg.ActualizaEvaluacionCompetencias(evaluaciones.ToString(), vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vClUsuario != "INVITADO")
            {
                UtilMensajes.MensajeResultadoDB(rwmResultados, vMensaje, vResultado.CL_TIPO_ERROR);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmResultados, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                string myUrl = ResolveUrl("~/Logon.aspx");
                Response.Redirect(ContextoUsuario.nbHost + myUrl);
            }

            
        }

        private void AsignarTooltip()
        {
            foreach (GridDataItem item in rgResultados.MasterTableView.Items)
            {
                if (item.DataItem != null)
                {
                    SPE_OBTIENE_EVENTO_PARTICIPANTE_COMPETENCIA_Result c = (SPE_OBTIENE_EVENTO_PARTICIPANTE_COMPETENCIA_Result)item.DataItem;

                    //RadSlider vRsFila = (RadSlider)item.FindControl("rsNivel1");
                    //vRsFila.Items[0].ToolTip = c.DS_NIVEL0;
                    //vRsFila.Items[1].ToolTip = c.DS_NIVEL1;
                    //vRsFila.Items[2].ToolTip = c.DS_NIVEL2;
                    //vRsFila.Items[3].ToolTip = c.DS_NIVEL3;
                    //vRsFila.Items[4].ToolTip = c.DS_NIVEL4;
                    //vRsFila.Items[5].ToolTip = c.DS_NIVEL5;

                    (item.FindControl("rbNivel0") as RadButton).ToolTip = c.DS_NIVEL0;
                    (item.FindControl("rbNivel1") as RadButton).ToolTip = c.DS_NIVEL1;
                    (item.FindControl("rbNivel2") as RadButton).ToolTip = c.DS_NIVEL2;
                    (item.FindControl("rbNivel3") as RadButton).ToolTip = c.DS_NIVEL3;
                    (item.FindControl("rbNivel4") as RadButton).ToolTip = c.DS_NIVEL4;
                    (item.FindControl("rbNivel5") as RadButton).ToolTip = c.DS_NIVEL5;

                }
               
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = (ContextoUsuario.oUsuario != null) ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO";
            vIdEmpresa = (ContextoUsuario.oUsuario != null) ? ContextoUsuario.oUsuario.ID_EMPRESA : null;
            vIdRol = (ContextoUsuario.oUsuario != null) ? ContextoUsuario.oUsuario.oRol.ID_ROL : null;
            //vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;

            if (vClUsuario == "INVITADO")
            {
                btnCancelar.Visible = false;
                btnCancelarInvitado.Visible = true;
            }

            string vClModulo = "FORMACION";
            string vModulo = Request.QueryString["m"];
            if (vModulo != null)
                vClModulo = vModulo;

            cssModulo = Utileria.ObtenerCssModulo(vClModulo);

            if (!Page.IsPostBack)
            {
                if (Request.Params["IdEvento"] != null)
                {
                    vIdEvento = int.Parse(Request.Params["IdEvento"]);
                }
            }
        }

        protected void rgResultados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            EventoCapacitacionNegocio neg = new EventoCapacitacionNegocio();
            rgResultados.DataSource = neg.ObtieneEventoParticipanteCompetencia(ID_EVENTO: vIdEvento, ID_EMPRESA:vIdEmpresa, pID_ROL:vIdRol);


              GridGroupByField field = new GridGroupByField();
              field.FieldName = "ID_PARTICIPANTE";
              //field.FieldName ="NB_PARTICIPANTE";
              //field.HeaderText = "";
            //field.FormatString = "{0}";
            
            GridGroupByExpression ex = new GridGroupByExpression();
            ex.GroupByFields.Add(field);
            ex.SelectFields.Add(field);
            rgResultados.MasterTableView.GroupByExpressions.Add(ex);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            guardarEvaluacion();
        }

        protected void rgResultados_DataBound(object sender, EventArgs e)
        {
            AsignarTooltip();
        }

        protected void rgResultados_ItemDataBound(object sender, GridItemEventArgs e)
        {
    
            if (e.Item is GridGroupHeaderItem)
            {
                GridGroupHeaderItem item = (GridGroupHeaderItem)e.Item;
                if (item.DataCell.Text != null && item.DataCell.Text != "")
                {
                    EventoCapacitacionNegocio neg = new EventoCapacitacionNegocio();
                    var vParticipante = neg.ObtieneEventoParticipanteCompetencia(ID_PARTICIPANTE: int.Parse(item.DataCell.Text.Substring(16)), ID_EMPRESA: vIdEmpresa).FirstOrDefault();
                    if (vParticipante != null)
                    {
                        item.DataCell.Text = "<strong>Participante: </strong>" + vParticipante.CL_PARTICIPANTE + " - " + vParticipante.NB_PARTICIPANTE;
                    }
                }
            }
        }

        protected void RadButton1_Click(object sender, EventArgs e)
        {
            string myUrl = ResolveUrl("~/Logon.aspx");
            Response.Redirect(ContextoUsuario.nbHost + myUrl);
        }
    }
}