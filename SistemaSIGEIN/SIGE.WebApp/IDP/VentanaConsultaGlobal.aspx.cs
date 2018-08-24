using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.IntegracionDePersonal;
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
using WebApp.Comunes;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaConsultaGlobal : System.Web.UI.Page
    {
        #region Variabes
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vClUsuario { get; set; }
        private string vNbPrograma { get; set; }

        public int vIdPuesto
        {
            get { return (int)ViewState["vs_vcg_id_puesto"]; }
            set { ViewState["vs_vcg_id_puesto"] = value; }
        }

        public int vIdCandidato
        {
            get { return (int)ViewState["vs_vcg_id_candidato"]; }
            set { ViewState["vs_vcg_id_candidato"] = value; }
        }

        public List<E_CONSULTA_GLOBAL> vLstCalificaciones
        {
            get { return (List<E_CONSULTA_GLOBAL>)ViewState["vs_vcg_lst_calificaciones"]; }
            set { ViewState["vs_vcg_lst_calificaciones"] = value; }
        }

        public decimal vPrPsicometria
        {
            get { return (int)ViewState["vs_vcg_pr_psicometria"]; }
            set { ViewState["vs_vcg_pr_psicometria"] = value; }
        }

        public decimal vPrIngles
        {
            get { return (int)ViewState["vs_vcg_pr_ingles"]; }
            set { ViewState["vs_vcg_pr_ingles"] = value; }
        }

        private int vIdConsultaGlobal
        {
            get { return (int)ViewState["vs_vcg_id_consulta_global"]; }
            set { ViewState["vs_vcg_id_consulta_global"] = value; }
        }

        #endregion

        #region Funciones

        private void CargarDatos()
        {
            CandidatoNegocio nCandidato = new CandidatoNegocio();
            DescriptivoNegocio nDescriptivo = new DescriptivoNegocio();

            var vCandidato = nCandidato.ObtieneCandidato(pIdCandidato: vIdCandidato).FirstOrDefault();

            if (vCandidato != null)
            {
                txtClSolicitud.InnerText = vCandidato.CL_SOLICITUD;
                txtNbCandidato.InnerText = vCandidato.NB_CANDIDATO_COMPLETO;
            }

            var vPuesto = nDescriptivo.ObtieneDescriptivo(vIdPuesto);

            if (vPuesto != null)
            {
                txtNbPuesto.InnerText = vPuesto.CL_PUESTO +" - "+ vPuesto.NB_PUESTO;
            }

            CargarCalificaciones();

        }

        private void CargarCalificaciones()
        {
            ConsultasComparativasNegocio nComparativas = new ConsultasComparativasNegocio();

            E_RESULTADO vResultado = nComparativas.InsertaConsultaGlobalCalificaciones(vIdCandidato, vIdPuesto, vClUsuario, vNbPrograma);

            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
            {
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
            }
            else
            {
                vLstCalificaciones = nComparativas.ObtieneConsultaGlobalCalificaciones(vIdCandidato, vIdPuesto);
                vIdConsultaGlobal = vLstCalificaciones[0].ID_CONSULTA_GLOBAL;

                if (vLstCalificaciones[0].DS_COMENTARIOS != null)
                {
                    reComentarios.Content = Utileria.MostrarNotas(vLstCalificaciones[0].DS_COMENTARIOS);
                    txtComentarios.InnerHtml = Utileria.MostrarNotas(vLstCalificaciones[0].DS_COMENTARIOS);
                }

                decimal vPrtoTotalPonderacion = vLstCalificaciones.Sum(t => t.PR_FACTOR);

                if (vPrtoTotalPonderacion < 100)
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "La ponderación para este puesto no esta establecida.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    btnGuardar.Enabled = false;
                    btnRecalcular.Enabled = false;
                }

            }
        }

        private void Recalcular()
        {
            int vIdConsultaglobalCalificacion = 0;
            decimal vPrValor = 0;

            foreach (GridDataItem item in rgConsultaglobal.MasterTableView.Items)
            {
                vIdConsultaglobalCalificacion = int.Parse(item.GetDataKeyValue("ID_CONSULTA_GLOBAL_CALIFICACION").ToString());

                var vFactor = vLstCalificaciones.Where(t => t.ID_CONSULTA_GLOBAL_CALIFICACION == vIdConsultaglobalCalificacion).FirstOrDefault();

                if (vFactor != null)
                {
                    RadNumericTextBox txtCalificacion = (item.FindControl("txtCalificacion") as RadNumericTextBox);

                    if (txtCalificacion.Value.HasValue)
                    {
                        vFactor.PR_CALIFICACION = (decimal)txtCalificacion.Value;

                        if (vFactor.PR_CALIFICACION > 0)
                        {
                            vPrValor = (vFactor.PR_CALIFICACION * vFactor.PR_FACTOR) / 100;
                        }
                        else
                        {
                            vPrValor = 0;
                        }


                        vFactor.PR_VALOR = vPrValor;
                    }
                    else
                    {
                        txtCalificacion.Value = 0;
                        vFactor.PR_CALIFICACION = 0;
                        vFactor.PR_VALOR = 0;
                    }
                }
            }

            rgConsultaglobal.Rebind();
        }

        public bool HabilitarTextbox(string NoFactor, string AsociadoIngles)
        {
            bool vResultado;

            if (NoFactor.Equals("1") || AsociadoIngles.Equals("True"))
            {
                vResultado = false;
            }
            else
            {
                vResultado = true;
            }

            return vResultado;
        }

        private void Guardar()
        {
            XElement vXmlValores = new XElement("GLOBAL");
            ConsultasComparativasNegocio nComparativas = new ConsultasComparativasNegocio();

            XElement vXmlComentarios = Utileria.GuardarNotas(reComentarios.Content, "XML_NOTAS");

            vXmlValores.Add(vLstCalificaciones.Select(t => new XElement("FACTOR",
                new XAttribute("ID_CONSULTA_GLOBAL_CALIFICACION", t.ID_CONSULTA_GLOBAL_CALIFICACION),
                new XAttribute("PR_CALIFICACION", t.PR_CALIFICACION),
                new XAttribute("PR_VALOR", t.PR_VALOR))));

            E_RESULTADO vResultado = nComparativas.ActualizarCalificacionConsultaGlobal(vIdCandidato, vIdPuesto, vIdConsultaGlobal, vXmlComentarios.ToString(), vXmlValores.ToString(), vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
        }

        protected void GraficaConsultaGlobal(List<E_CONSULTA_GLOBAL> plstConsultaGlobal)
        {
            rhcConsultaGlobal.PlotArea.Series.Clear();
            rhcConsultaGlobal.PlotArea.XAxis.Items.Clear();
            ColumnSeries vSolicitud = new ColumnSeries();
            ColumnSeries vPuesto = new ColumnSeries();

            //CandidatoNegocio nCandidato = new CandidatoNegocio();
            //DescriptivoNegocio nDescriptivo = new DescriptivoNegocio();
            //var vCandidato = nCandidato.ObtieneCandidato(pIdCandidato: vIdCandidato).FirstOrDefault();
            //var vPuestoConsulta = nDescriptivo.ObtieneDescriptivo(vIdPuesto);

            foreach (var i in plstConsultaGlobal)
            {
                decimal vTotal;
                vSolicitud.SeriesItems.Add(new CategorySeriesItem(i.PR_FACTOR));
                vSolicitud.TooltipsAppearance.DataFormatString = "{0:N2}%";
                vSolicitud.LabelsAppearance.Visible = false;
                vSolicitud.Name = "Puesto";
                vTotal = decimal.Round((i.PR_CALIFICACION * i.PR_FACTOR / 100), 2, MidpointRounding.AwayFromZero);
                vPuesto.SeriesItems.Add(new CategorySeriesItem(vTotal));
                vPuesto.TooltipsAppearance.DataFormatString = "{0:N2}%";
                vPuesto.LabelsAppearance.Visible = false;
                vPuesto.Name = "Solicitud";
                rhcConsultaGlobal.PlotArea.XAxis.Items.Add(i.NB_FACTOR);
                rhcConsultaGlobal.PlotArea.XAxis.LabelsAppearance.RotationAngle = 270;
            }
            
            rhcConsultaGlobal.PlotArea.Series.Add(vSolicitud);
            rhcConsultaGlobal.PlotArea.Series.Add(vPuesto);
        }

        private void CargarReporte()
        {
            CargarDatos();
            GraficaConsultaGlobal(vLstCalificaciones);
            rgdCompatibilidad.Rebind();
            if (vLstCalificaciones[0].DS_COMENTARIOS != null)
            {
                txtComentarios.InnerHtml = Utileria.MostrarNotas(vLstCalificaciones[0].DS_COMENTARIOS);
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                vLstCalificaciones = new List<E_CONSULTA_GLOBAL>();

                if (Request.Params["IdPuesto"] != null)
                {
                    vIdPuesto = int.Parse(Request.Params["IdPuesto"].ToString());
                }

                if (Request.Params["IdCandidato"] != null)
                {
                    vIdCandidato = int.Parse(Request.Params["IdCandidato"].ToString());
                }

                rtsConsultas.Tabs[1].Enabled = false;

                CargarDatos();
                GraficaConsultaGlobal(vLstCalificaciones);
            }
        }

        protected void btnRecalcular_Click(object sender, EventArgs e)
        {
            Recalcular();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //if (reComentarios.Content != "")
            //{
                Recalcular();
                Guardar();
                CargarReporte();
                rtsConsultas.Tabs[1].Enabled = true;
            //}
            //else
            //{
            //    UtilMensajes.MensajeResultadoDB(rnMensaje, "El campo Comentarios es obligatorio.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            //}
        }

        protected void rgConsultaglobal_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgConsultaglobal.DataSource = vLstCalificaciones;
        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            CargarReporte();
        }

        protected void rgdCompatibilidad_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgdCompatibilidad.DataSource = vLstCalificaciones;
            //rgdCompatibilidad.DataBind();
        }

    }
}