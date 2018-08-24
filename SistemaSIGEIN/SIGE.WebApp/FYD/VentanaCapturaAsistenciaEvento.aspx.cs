using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaCapturaAsistenciaEvento : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vIdEvento {
            get { return (int)ViewState["vs_vcae_id_evento"]; }
            set { ViewState["vs_vcae_id_evento"] = value; }
        }

        private List<E_EVENTO_PARTICIPANTE> vListaParticipantes {
            get { return (List<E_EVENTO_PARTICIPANTE>)ViewState["vs_vcae_lista_participante"]; }
            set { ViewState["vs_vcae_lista_participante"] = value; }
        }

        private E_EVENTO oEvento {
            get { return (E_EVENTO)ViewState["vs_vcae_evento"]; }
            set { ViewState["vs_vcae_evento"] = value; }
        }

        #endregion

        #region Funciones

        private void calcularAsistenciaPromedio()
        {
            decimal SumaHoras = 0;

            foreach (E_EVENTO_PARTICIPANTE item in vListaParticipantes)
            {
                if (item.PR_CUMPLIMIENTO.HasValue)
                {
                    SumaHoras = SumaHoras + item.PR_CUMPLIMIENTO.Value;
                }
                else
                {
                    SumaHoras = SumaHoras + 0;
                }
            }

            if (vListaParticipantes.Count > 0)
            txtAsistenciaGrupal.Text = (SumaHoras / (decimal)vListaParticipantes.Count).ToString();
        }

        private void calcularAsistenciaGrid()
        {
            decimal vHorasCumplidas = 0;
            int vSumaHoras = 0;
            int vIdEventoParticipante = 0;
            decimal vPrCumplimiento;
            //decimal vPrAsistenciaEvento = 0;

            foreach (GridDataItem item in rgAsistencia.MasterTableView.Items)
            {
                vIdEventoParticipante = int.Parse(item.GetDataKeyValue("ID_EVENTO_PARTICIPANTE").ToString());

                if ((item.FindControl("txtTiempo") as RadNumericTextBox).Value.HasValue)
                {
                    vHorasCumplidas = (decimal)(item.FindControl("txtTiempo") as RadNumericTextBox).Value.Value;

                    if (vHorasCumplidas > oEvento.NO_DURACION_CURSO)
                    {
                        vHorasCumplidas = oEvento.NO_DURACION_CURSO;
                        (item.FindControl("txtTiempo") as RadNumericTextBox).Text = oEvento.NO_DURACION_CURSO.ToString();
                    }
                }
                else
                {
                    (item.FindControl("txtTiempo") as RadNumericTextBox).Text = "0";
                    vHorasCumplidas = 0;
                }
                
                vSumaHoras = vSumaHoras + (int)vHorasCumplidas;                
                vPrCumplimiento = (vHorasCumplidas / oEvento.NO_DURACION_CURSO) * (decimal)100;
                item["PR_CUMPLIMIENTO"].Text = vPrCumplimiento.ToString("N2");

                vListaParticipantes.Where(t => t.ID_EVENTO_PARTICIPANTE == vIdEventoParticipante).FirstOrDefault().PR_CUMPLIMIENTO = vPrCumplimiento;
            }

            calcularAsistenciaPromedio();
            //vPrAsistenciaEvento = ((vSumaHoras / rgAsistencia.MasterTableView.Items.Count)); /// oEvento.NO_DURACION_CURSO) * 100;
            //txtAsistenciaGrupal.Text = vPrAsistenciaEvento.ToString();
        }

        private void guardarAsistencias()
        {
            XElement participantes = new XElement("PARTICIPANTES");

            foreach (GridDataItem item in rgAsistencia.MasterTableView.Items)
            {

                XElement part = new XElement("PARTICIPANTE",
                    new XAttribute("ID_EVENTO_PARTICIPANTE", item.GetDataKeyValue("ID_EVENTO_PARTICIPANTE").ToString()),
                    new XAttribute("NO_TIEMPO", (item.FindControl("txtTiempo") as RadNumericTextBox).Value),
                    new XAttribute("PR_CUMPLIMIENTO", item["PR_CUMPLIMIENTO"].Text));
                participantes.Add(part);
            }

            EventoCapacitacionNegocio neg = new EventoCapacitacionNegocio();
            E_RESULTADO msj = neg.ActualizaEventoCalendario(participantes.ToString(), vClUsuario, vNbPrograma);

            string vMensaje = msj.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmAsistencia, vMensaje, msj.CL_TIPO_ERROR);
        }       

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                if (Request.Params["IdEvento"] != null)
                {
                    vIdEvento = int.Parse(Request.Params["IdEvento"].ToString());
                    EventoCapacitacionNegocio neg = new EventoCapacitacionNegocio();
                    oEvento = neg.ObtieneEventos(ID_EVENTO: vIdEvento).FirstOrDefault();

                    if (oEvento != null)
                    {
                        txtEvento.InnerText = oEvento.NB_EVENTO;
                        txtHorasCurso.InnerText = oEvento.NO_DURACION_CURSO.ToString();

                        vListaParticipantes = neg.ObtieneParticipanteEvento(ID_EVENTO: vIdEvento);
                        calcularAsistenciaPromedio();    
                    }

                    
                }
            }
        }

        protected void rgAsistencia_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgAsistencia.DataSource = vListaParticipantes;         
        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {            
            calcularAsistenciaGrid();
        }
      
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            guardarAsistencias();
        }
      
    }
}