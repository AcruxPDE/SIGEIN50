using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;


namespace SIGE.WebApp.Administracion
{
    public partial class VentanaRadDock : System.Web.UI.Page
    {

        #region Variable

        private string vClUsuario = "admin";
        private string vNbPrograma = "VentanaCatalogoCompetencias.aspx";
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private string ptipo
        {
            get { return (string)ViewState["vstipo"]; }
            set { ViewState["vstipo"] = value; }
        }

        private int vIdCompetencia
        {
            get { return (int)ViewState["vsID_CLASIFICACION_COMPETENCIA"]; }
            set { ViewState["vsID_CLASIFICACION_COMPETENCIA"] = value; }
        }

        private string pClaveTipoCompetencia
        {
            get { return (string)ViewState["vsClaveTipoCompetencia"]; }
            set { ViewState["vsClaveTipoCompetencia"] = value; }
        }

        private string pClaveClasificacion
        {
            get { return (string)ViewState["vsClaveClasificacion"]; }
            set { ViewState["vsClaveClasificacion"] = value; }
        }


        #endregion

        #region Funciones

        private void CargarCatalogos()
        {
            ClasificacionCompetenciaNegocio nCompetenciaClasificacion = new ClasificacionCompetenciaNegocio();
            var vClasificaciones = nCompetenciaClasificacion.ObtieneClasificacionCompetencia();
            var vEspecificas = nCompetenciaClasificacion.ObtieneClasificacionCompetencia(4).FirstOrDefault(); ;
            if (vClasificaciones != null)
            {

                cmbClasificaciones.DataSource = vClasificaciones;
                cmbClasificaciones.DataTextField = "NB_CLASIFICACION_COMPETENCIA";
                cmbClasificaciones.DataValueField = "CL_CLASIFICACION";
                cmbClasificaciones.DataBind();
                cmbClasificaciones.SelectedValue = vEspecificas.CL_CLASIFICACION;
                cmbClasificaciones.Text = vEspecificas.NB_CLASIFICACION_COMPETENCIA;
            }

            TipoCompetenciaNegocio nTipoCompetencia = new TipoCompetenciaNegocio();
            var vTipoCompetencia = nTipoCompetencia.ObtieneTipoCompetencia();
            if (vTipoCompetencia != null)
            {
                cmbCategoria.DataSource = vTipoCompetencia;
                cmbCategoria.DataTextField = "NB_TIPO_COMPETENCIA";
                cmbCategoria.DataValueField = "CL_TIPO_COMPETENCIA";
                cmbCategoria.DataBind();
                //cmbCategoria.SelectedValue = vc_competencia.CL_TIPO_COMPETENCIA;
            }
        }

        private void CargarDatos()
        {
            CompetenciaNegocio nCompetencia = new CompetenciaNegocio();
            var vCompetencia = nCompetencia.ObtieneCompetencias(pIdCompetencia: vIdCompetencia).FirstOrDefault();

            CompetenciaNivelNegocio nCompetenciaNivel = new CompetenciaNivelNegocio();
            List<SPE_OBTIENE_C_COMPETENCIA_NIVEL_Result> vCompetenciaNivel = nCompetenciaNivel.ObtieneCompetenciaNivel(pIdCompetencia: vIdCompetencia);

            foreach (SPE_OBTIENE_C_COMPETENCIA_NIVEL_Result vobjetoCompetenciaNivel in vCompetenciaNivel)
            {
                if (vobjetoCompetenciaNivel.CL_NIVEL_COMPETENCIA.Equals("NIVEL_00"))
                {
                    radEditorPersona0.Content = vobjetoCompetenciaNivel.DS_NIVEL_COMPETENCIA_PERSONA;
                    radEditorPuesto0.Content = vobjetoCompetenciaNivel.DS_NIVEL_COMPETENCIA_PUESTO;
                }
                if (vobjetoCompetenciaNivel.CL_NIVEL_COMPETENCIA.Equals("NIVEL_01"))
                {
                    radEditorPersona1.Content = vobjetoCompetenciaNivel.DS_NIVEL_COMPETENCIA_PERSONA;
                    radEditorPuesto1.Content = vobjetoCompetenciaNivel.DS_NIVEL_COMPETENCIA_PUESTO;
                }
                if (vobjetoCompetenciaNivel.CL_NIVEL_COMPETENCIA.Equals("NIVEL_02"))
                {
                    radEditorPersona2.Content = vobjetoCompetenciaNivel.DS_NIVEL_COMPETENCIA_PERSONA;
                    radEditorPuesto2.Content = vobjetoCompetenciaNivel.DS_NIVEL_COMPETENCIA_PUESTO;
                }
                if (vobjetoCompetenciaNivel.CL_NIVEL_COMPETENCIA.Equals("NIVEL_03"))
                {
                    radEditorPersona3.Content = vobjetoCompetenciaNivel.DS_NIVEL_COMPETENCIA_PERSONA;
                    radEditorPuesto3.Content = vobjetoCompetenciaNivel.DS_NIVEL_COMPETENCIA_PUESTO;
                }
                if (vobjetoCompetenciaNivel.CL_NIVEL_COMPETENCIA.Equals("NIVEL_04"))
                {
                    radEditorPersona4.Content = vobjetoCompetenciaNivel.DS_NIVEL_COMPETENCIA_PERSONA;
                    radEditorPuesto4.Content = vobjetoCompetenciaNivel.DS_NIVEL_COMPETENCIA_PUESTO;
                }
                if (vobjetoCompetenciaNivel.CL_NIVEL_COMPETENCIA.Equals("NIVEL_05"))
                {
                    radEditorPersona5.Content = vobjetoCompetenciaNivel.DS_NIVEL_COMPETENCIA_PERSONA;
                    radEditorPuesto5.Content = vobjetoCompetenciaNivel.DS_NIVEL_COMPETENCIA_PUESTO;
                }
            }

            cmbClasificaciones.SelectedValue = vCompetencia.CL_CLASIFICACION;
            cmbClasificaciones.Text = vCompetencia.NB_CLASIFICACION_COMPETENCIA;
            cmbCategoria.SelectedValue = vCompetencia.CL_TIPO_COMPETENCIA;
            cmbCategoria.Text = vCompetencia.NB_TIPO_COMPETENCIA;
            chkActivo.Checked = vCompetencia.FG_ACTIVO;
            txtDescripcion.Text = vCompetencia.DS_COMPETENCIA;
            txtClave.Text = vCompetencia.CL_COMPETENCIA;
            txtNbCompetencia.Text = vCompetencia.NB_COMPETENCIA;
        }

        private bool ValidarDatos()
        {
            if (cmbCategoria.SelectedIndex == -1)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Indica la categoría", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            if (cmbClasificaciones.SelectedIndex == -1)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Indica la clasificación", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            if (string.IsNullOrEmpty(txtClave.Text))
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Indica la clave", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            if (string.IsNullOrEmpty(txtNbCompetencia.Text))
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Indica el nombre", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Indica la descripción de la competencia", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            if (string.IsNullOrEmpty(radEditorPuesto0.Content) || string.IsNullOrEmpty(radEditorPuesto1.Content) || string.IsNullOrEmpty(radEditorPuesto2.Content) || string.IsNullOrEmpty(radEditorPuesto3.Content) || string.IsNullOrEmpty(radEditorPuesto4.Content) || string.IsNullOrEmpty(radEditorPuesto5.Content))
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Indica las definiciones de los niveles por puesto", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            if (string.IsNullOrEmpty(radEditorPersona0.Content) || string.IsNullOrEmpty(radEditorPersona1.Content) || string.IsNullOrEmpty(radEditorPersona2.Content) || string.IsNullOrEmpty(radEditorPersona3.Content) || string.IsNullOrEmpty(radEditorPersona4.Content) || string.IsNullOrEmpty(radEditorPersona5.Content))
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Indica las definiciones de los niveles por persona", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }

            return true;
        }

        //private void GuardarDatos()
        //{
        //    string vTipotransaccion = "";
        //    CompetenciaNegocio nCompetencia = new CompetenciaNegocio();
        //    E_COMPETENCIA_NIVEL vCompetencia = new E_COMPETENCIA_NIVEL();

        //    //string clave = txtClave.Text;
        //    //string nbclasificacion = txtNbCompetencia.Text;
        //    //string descripcion = txtDescripcion.Text;

        //    if (ValidarDatos())
        //    {

        //        radEditorPersona0.EditModes = radEditorPersona0.EditModes ^ Telerik.Web.UI.EditModes.Html;
        //        radEditorPuesto0.EditModes = radEditorPuesto0.EditModes ^ Telerik.Web.UI.EditModes.Html;
        //        radEditorPersona1.EditModes = radEditorPersona1.EditModes ^ Telerik.Web.UI.EditModes.Html;
        //        radEditorPuesto1.EditModes = radEditorPuesto1.EditModes ^ Telerik.Web.UI.EditModes.Html;
        //        radEditorPersona2.EditModes = radEditorPersona2.EditModes ^ Telerik.Web.UI.EditModes.Html;
        //        radEditorPuesto2.EditModes = radEditorPuesto2.EditModes ^ Telerik.Web.UI.EditModes.Html;
        //        radEditorPersona3.EditModes = radEditorPersona3.EditModes ^ Telerik.Web.UI.EditModes.Html;
        //        radEditorPuesto3.EditModes = radEditorPuesto3.EditModes ^ Telerik.Web.UI.EditModes.Html;
        //        radEditorPersona4.EditModes = radEditorPersona4.EditModes ^ Telerik.Web.UI.EditModes.Html;
        //        radEditorPuesto4.EditModes = radEditorPuesto4.EditModes ^ Telerik.Web.UI.EditModes.Html;
        //        radEditorPersona5.EditModes = radEditorPersona5.EditModes ^ Telerik.Web.UI.EditModes.Html;
        //        radEditorPuesto5.EditModes = radEditorPuesto5.EditModes ^ Telerik.Web.UI.EditModes.Html;


        //        vCompetencia.CL_COMPETENCIA = txtClave.Text;
        //        vCompetencia.NB_COMPETENCIA = txtNbCompetencia.Text;
        //        vCompetencia.DS_COMPETENCIA = txtDescripcion.Text;
        //        vCompetencia.CL_TIPO_COMPETENCIA = cmbCategoria.SelectedValue;
        //        vCompetencia.CL_CLASIFICACION = cmbClasificaciones.SelectedValue;
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PERSONA_N0 = radEditorPersona0.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N0 = radEditorPuesto0.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PERSONA_N1 = radEditorPersona1.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N1 = radEditorPuesto1.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PERSONA_N2 = radEditorPersona2.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N2 = radEditorPuesto2.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PERSONA_N3 = radEditorPersona3.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N3 = radEditorPuesto3.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PERSONA_N4 = radEditorPersona4.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N4 = radEditorPuesto4.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PERSONA_N5 = radEditorPersona5.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N5 = radEditorPuesto5.Content.Replace("&lt;", "");
        //        vCompetencia.XML_CAMPOS_ADICIONALES = null;
        //        vCompetencia.FG_ACTIVO = chkActivo.Checked;


        //        if (ptipo.Equals("Agregar"))
        //        {
        //            vTipotransaccion = E_TIPO_OPERACION_DB.I.ToString();
        //            vCompetencia.ID_COMPETENCIA = 1;
        //        }
        //        else
        //        {
        //            vCompetencia.ID_COMPETENCIA = vIdCompetencia;
        //            vTipotransaccion = E_TIPO_OPERACION_DB.A.ToString();
        //        }


        //        E_RESULTADO vResultado = nCompetencia.InsertaActualizaCompetencia(pTipoTransaccion: vTipotransaccion, pClUsuario: vClUsuario, pNbPrograma: vNbPrograma, pCompetencia: vCompetencia);
        //        string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
        //        UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);

        //    }
        //    //EDITAR
        //    if (ptipo.Equals("Agregar"))
        //    {
        //        vTipotransaccion = E_TIPO_OPERACION_DB.I.ToString();
        //        vCompetencia.ID_COMPETENCIA = 1;
        //        if (ptipo.Equals("Editar"))
        //        {
        //            vCompetencia.ID_COMPETENCIA = vIdCompetencia;
        //            vTipotransaccion = E_TIPO_OPERACION_DB.A.ToString();
        //        }
        //        else
        //        {                    
        //        }                
        //        if (vCompetencia != null)
        //        {
        //            E_RESULTADO vResultado = nCompetencia.InsertaActualizaCompetencia(pTipoTransaccion: vTipotransaccion, pClUsuario: vClUsuario, pNbPrograma: vNbPrograma, pCompetencia: vCompetencia);
        //            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
        //            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
        //        }
        //    }
        //    else
        //    {
        //        //AGREGAR               
        //        vCompetencia.ID_COMPETENCIA = 1;
        //        vCompetencia.CL_COMPETENCIA = txtClave.Text;
        //        vCompetencia.NB_COMPETENCIA = txtNbCompetencia.Text;
        //        vCompetencia.DS_COMPETENCIA = txtDescripcion.Text;
        //        vCompetencia.CL_TIPO_COMPETENCIA = cmbCategoria.SelectedValue;
        //        vCompetencia.CL_CLASIFICACION = cmbClasificaciones.SelectedValue;
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PERSONA_N0 = radEditorPersona0.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N0 = radEditorPuesto0.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PERSONA_N1 = radEditorPersona1.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N1 = radEditorPuesto1.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PERSONA_N2 = radEditorPersona2.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N2 = radEditorPuesto2.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PERSONA_N3 = radEditorPersona3.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N3 = radEditorPuesto3.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PERSONA_N4 = radEditorPersona4.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N4 = radEditorPuesto4.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PERSONA_N5 = radEditorPersona5.Content.Replace("&lt;", "");
        //        vCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N5 = radEditorPuesto5.Content.Replace("&lt;", "");
        //        vCompetencia.XML_CAMPOS_ADICIONALES = null;
        //        vCompetencia.FG_ACTIVO = chkActivo.Checked;
        //        if (vCompetencia != null)
        //        {
        //            E_RESULTADO vResultado = nCompetencia.InsertaActualizaCompetencia(pTipoTransaccion: E_TIPO_OPERACION_DB.I.ToString(), pClUsuario: vClUsuario, pNbPrograma: vNbPrograma, pCompetencia: vCompetencia);
        //            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
        //            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
        //        }
        //    }
        //}

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {

                ptipo = Request.QueryString["TIPO"];
                vIdCompetencia = int.Parse((Request.QueryString["ID"]));

                CargarCatalogos();

                if (!ptipo.Equals("Agregar"))
                {
                    CargarDatos();
                }

                //else
                //{
                //    //Nos traemos el objeto del ID_CLASIFICACION_COMPETENCIA
                //    ClasificacionCompetenciaNegocio ncompetencia_clasificacion = new ClasificacionCompetenciaNegocio();
                //    var vclasificacion_competencias = ncompetencia_clasificacion.ObtieneClasificacionCompetencia();
                //    if (vclasificacion_competencias != null)
                //    {
                //        cmbClasificaciones.DataSource = vclasificacion_competencias;
                //        cmbClasificaciones.DataTextField = "NB_CLASIFICACION_COMPETENCIA";
                //        cmbClasificaciones.DataValueField = "CL_CLASIFICACION";
                //        cmbClasificaciones.DataBind();
                //    }
                //    TipoCompetenciaNegocio ntipocompetencia = new TipoCompetenciaNegocio();
                //    var vclasificacion_tipocompetencias = ntipocompetencia.ObtieneTipoCompetencia();
                //    if (vclasificacion_tipocompetencias != null)
                //    {
                //        cmbCategoria.DataSource = vclasificacion_tipocompetencias;
                //        cmbCategoria.DataTextField = "NB_TIPO_COMPETENCIA";
                //        cmbCategoria.DataValueField = "CL_TIPO_COMPETENCIA";
                //        cmbCategoria.DataBind();
                //    }
                //}
            }
        }        

        protected void btnSave_click(object sender, EventArgs e)
        {
            //GuardarDatos();

            string tipo_transaccion = "";
            CompetenciaNegocio nccompetencia = new CompetenciaNegocio();
            E_COMPETENCIA_NIVEL vObjetoAgregar = new E_COMPETENCIA_NIVEL();

            string clave = txtClave.Text;
            string nbclasificacion = txtNbCompetencia.Text;
            string descripcion = txtDescripcion.Text;


            //EDITAR
            if (!ptipo.Equals("Agregar"))
            {

                if (ptipo.Equals("Editar"))
                {
                    vObjetoAgregar.ID_COMPETENCIA = vIdCompetencia;
                    tipo_transaccion = E_TIPO_OPERACION_DB.A.ToString();
                }
                else
                {
                    tipo_transaccion = E_TIPO_OPERACION_DB.I.ToString();
                    vObjetoAgregar.ID_COMPETENCIA = 1;
                }



                vObjetoAgregar.CL_COMPETENCIA = txtClave.Text;
                vObjetoAgregar.NB_COMPETENCIA = txtNbCompetencia.Text;
                vObjetoAgregar.DS_COMPETENCIA = txtDescripcion.Text;
                vObjetoAgregar.CL_TIPO_COMPETENCIA = cmbCategoria.SelectedValue;
                vObjetoAgregar.CL_CLASIFICACION = cmbClasificaciones.SelectedValue;
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PERSONA_N0 = radEditorPersona0.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PUESTO_N0 = radEditorPuesto0.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PERSONA_N1 = radEditorPersona1.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PUESTO_N1 = radEditorPuesto1.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PERSONA_N2 = radEditorPersona2.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PUESTO_N2 = radEditorPuesto2.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PERSONA_N3 = radEditorPersona3.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PUESTO_N3 = radEditorPuesto3.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PERSONA_N4 = radEditorPersona4.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PUESTO_N4 = radEditorPuesto4.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PERSONA_N5 = radEditorPersona5.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PUESTO_N5 = radEditorPuesto5.Content.Replace("&lt;", "");
                vObjetoAgregar.XML_CAMPOS_ADICIONALES = null;
                vObjetoAgregar.FG_ACTIVO = chkActivo.Checked;

                if (vObjetoAgregar.CL_COMPETENCIA != "" && vObjetoAgregar.NB_COMPETENCIA != "")
                {

                    E_RESULTADO vResultado = nccompetencia.InsertaActualizaCompetencia(pTipoTransaccion: tipo_transaccion, pClUsuario: vClUsuario, pNbPrograma: vNbPrograma, pCompetencia: vObjetoAgregar);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);

                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Los campos Clave y Competencia no pueden ser vacios.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                }
            }

            else
            {

                //AGREGAR



                radEditorPersona0.EditModes = radEditorPersona0.EditModes ^ Telerik.Web.UI.EditModes.Html;
                radEditorPuesto0.EditModes = radEditorPuesto0.EditModes ^ Telerik.Web.UI.EditModes.Html;
                radEditorPersona1.EditModes = radEditorPersona1.EditModes ^ Telerik.Web.UI.EditModes.Html;
                radEditorPuesto1.EditModes = radEditorPuesto1.EditModes ^ Telerik.Web.UI.EditModes.Html;
                radEditorPersona2.EditModes = radEditorPersona2.EditModes ^ Telerik.Web.UI.EditModes.Html;
                radEditorPuesto2.EditModes = radEditorPuesto2.EditModes ^ Telerik.Web.UI.EditModes.Html;
                radEditorPersona3.EditModes = radEditorPersona3.EditModes ^ Telerik.Web.UI.EditModes.Html;
                radEditorPuesto3.EditModes = radEditorPuesto3.EditModes ^ Telerik.Web.UI.EditModes.Html;
                radEditorPersona4.EditModes = radEditorPersona4.EditModes ^ Telerik.Web.UI.EditModes.Html;
                radEditorPuesto4.EditModes = radEditorPuesto4.EditModes ^ Telerik.Web.UI.EditModes.Html;
                radEditorPersona5.EditModes = radEditorPersona5.EditModes ^ Telerik.Web.UI.EditModes.Html;
                radEditorPuesto5.EditModes = radEditorPuesto5.EditModes ^ Telerik.Web.UI.EditModes.Html;


                vObjetoAgregar.ID_COMPETENCIA = 1;

                vObjetoAgregar.CL_COMPETENCIA = txtClave.Text;
                vObjetoAgregar.NB_COMPETENCIA = txtNbCompetencia.Text;
                vObjetoAgregar.DS_COMPETENCIA = txtDescripcion.Text;
                vObjetoAgregar.CL_TIPO_COMPETENCIA = cmbCategoria.SelectedValue;
                vObjetoAgregar.CL_CLASIFICACION = cmbClasificaciones.SelectedValue;
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PERSONA_N0 = radEditorPersona0.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PUESTO_N0 = radEditorPuesto0.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PERSONA_N1 = radEditorPersona1.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PUESTO_N1 = radEditorPuesto1.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PERSONA_N2 = radEditorPersona2.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PUESTO_N2 = radEditorPuesto2.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PERSONA_N3 = radEditorPersona3.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PUESTO_N3 = radEditorPuesto3.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PERSONA_N4 = radEditorPersona4.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PUESTO_N4 = radEditorPuesto4.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PERSONA_N5 = radEditorPersona5.Content.Replace("&lt;", "");
                vObjetoAgregar.DS_NIVEL_COMPETENCIA_PUESTO_N5 = radEditorPuesto5.Content.Replace("&lt;", "");
                vObjetoAgregar.XML_CAMPOS_ADICIONALES = null;
                vObjetoAgregar.FG_ACTIVO = chkActivo.Checked;

                if (vObjetoAgregar.CL_COMPETENCIA != "" && vObjetoAgregar.NB_COMPETENCIA != "")
                {
                    E_RESULTADO vResultado = nccompetencia.InsertaActualizaCompetencia(pTipoTransaccion: E_TIPO_OPERACION_DB.I.ToString(), pClUsuario: vClUsuario, pNbPrograma: vNbPrograma, pCompetencia: vObjetoAgregar);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);

                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Los campos Clave y Competencia no pueden ser vacios.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                }
            }

        }
    }
}