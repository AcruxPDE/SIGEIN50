using SIGE.Entidades.Externas;
using SIGE.Negocio.IntegracionDePersonal;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaProcesoSeleccionReferencias : System.Web.UI.Page
    {

        #region Variable

        private string vClUsuario;
        private string vNbPrograma;

        public int vIdExperiencia
        {
            get { return (int)ViewState["vs_ps_id_experiencia"]; }
            set { ViewState["vs_ps_id_experiencia"] = value; }
        }

        public int vIdCandidato
        {
            get { return (int)ViewState["vs_ps_id_candidato"]; }
            set { ViewState["vs_ps_id_candidato"] = value; }
        }

        private List<E_REFERENCIA_CANDIDATO> vLstReferencias
        {
            get { return (List<E_REFERENCIA_CANDIDATO>)ViewState["vs_ps_lst_referencias"]; }
            set { ViewState["vs_ps_lst_referencias"] = value; }
        }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;


            if (!IsPostBack)
            {
                if (Request.QueryString["IdExperiencia"] != null && Request.QueryString["idCandidato"] != null)
                {
                    vIdExperiencia = int.Parse((Request.QueryString["IdExperiencia"]));
                    vIdCandidato = int.Parse((Request.QueryString["idCandidato"]));
                    vLstReferencias = new List<E_REFERENCIA_CANDIDATO>();

                    ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
                    var referencias = nProcesoSeleccion.ObtieneExperienciaLaboral(pIdCandidato: vIdCandidato,pIdExperienciaLaboral:vIdExperiencia).FirstOrDefault();

                    vLstReferencias.Add(new E_REFERENCIA_CANDIDATO { ID_EXPERIENCIA_LABORAL = referencias.ID_EXPERIENCIA_LABORAL });
                    txtEmpresa.InnerText = referencias.NB_EMPRESA;
                    txtGiroEmpresa.InnerText = referencias.NB_GIRO;
                    txtInicio.InnerText = referencias.FE_INICIO;
                    txtFin.InnerText = referencias.FE_FIN;
                    txtPuestoDesempenado.InnerText = referencias.NB_PUESTO;
                    txtFuncionesDesempenadas.InnerText = referencias.DS_FUNCIONES;
                    txtNombreJefe.InnerText = referencias.NB_CONTACTO;
                    txtPuestoJefe.InnerText = referencias.NB_PUESTO_CONTACTO;
                    txtTelefonoJefe.InnerText = referencias.NO_TELEFONO_CONTACTO;
                    txtCorreoJefe.InnerText = referencias.CL_CORREO_ELECTRONICO;

                    txtNombreReferencia.Text = referencias.NB_REFERENCIA;
                    txtPuestoReferencia.Text = referencias.NB_PUESTO_REFERENCIA;
                    if (referencias.CL_INFORMACION_CONFIRMADA == "Sí")
                    {
                        chkInformacionConfirmadaSi.Checked = true;
                        chkInformacionConfirmadaNo.Checked = false;
                    }
                    else
                    {
                        chkInformacionConfirmadaNo.Checked = true;
                        chkInformacionConfirmadaSi.Checked = false;
                    }
                    txtDsNotas.Content = referencias.DS_COMENTARIOS;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
            XElement vXmlreferencias = new XElement("REFERENCIAS");

            if (vLstReferencias.Count >= 1)
            {
                vLstReferencias[0].NB_REFERENCIA = txtNombreReferencia.Text;
                vLstReferencias[0].NB_PUESTO_REFERENCIA = txtPuestoReferencia.Text;
                vLstReferencias[0].FG_INFORMACION_CONFIRMADA = chkInformacionConfirmadaSi.Checked;
                vLstReferencias[0].DS_COMENTARIOS = txtDsNotas.Text;
            }

            vXmlreferencias.Add(vLstReferencias.Select(t =>
                new XElement("REFERENCIA",
                    new XAttribute("ID_EXPERIENCIA_LABORAL", t.ID_EXPERIENCIA_LABORAL),
                    new XAttribute("NB_REFERENCIA", t.NB_REFERENCIA),
                    new XAttribute("NB_PUESTO_REFERENCIA", t.NB_PUESTO_REFERENCIA),
                    new XAttribute("FG_INFORMACION_PROPORCIONADA", t.FG_INFORMACION_CONFIRMADA),
                    new XAttribute("DS_COMENTARIOS", t.DS_COMENTARIOS))));

            var vRespuesta = nProcesoSeleccion.ActualizaReferenciasExperienciaLaboral(vXmlreferencias.ToString(), vClUsuario, vNbPrograma);
            UtilMensajes.MensajeResultadoDB(rnMensaje, vRespuesta.MENSAJE[0].DS_MENSAJE.ToString(), vRespuesta.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "generateDataForParent");
        }
    }
}