using WebApp.Comunes;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Net;
using Telerik.Web.UI;
using SIGE.Entidades;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaCatalogoAreas : System.Web.UI.Page
    {

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int pID_DEPARTAMENTO
        {
            get { return (int)ViewState["vsID_DEPARTAMENTO"]; }
            set { ViewState["vsID_DEPARTAMENTO"] = value; }
        }

        private string ptipo
        {
            get { return (string)ViewState["vstipo"]; }
            set { ViewState["vstipo"] = value; }
        }


        private E_DEPARTAMENTO vArea
        {
            get { return (E_DEPARTAMENTO)ViewState["vsArea"]; }
            set { ViewState["vsArea"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            DepartamentoNegocio negocio = new DepartamentoNegocio();

            if (!IsPostBack)
            {

                ptipo = Request.QueryString["TIPO"];
                vArea = new E_DEPARTAMENTO();
                if (!ptipo.Equals("Agregar"))
                {
                    pID_DEPARTAMENTO = int.Parse((Request.QueryString["ID"]));

                    SPE_OBTIENE_M_DEPARTAMENTO_Result vObjetoArea = negocio.ObtieneDepartamentos(pIdDepartamento: pID_DEPARTAMENTO).FirstOrDefault();
                    vArea.CL_DEPARTAMENTO = vObjetoArea.CL_DEPARTAMENTO;
                    vArea.FE_INACTIVO = vObjetoArea.FE_INACTIVO;
                    vArea.FG_ACTIVO = vObjetoArea.FG_ACTIVO;
                    vArea.ID_DEPARTAMENTO = vObjetoArea.ID_DEPARTAMENTO;
                    vArea.NB_DEPARTAMENTO = vObjetoArea.NB_DEPARTAMENTO;
                    vArea.XML_CAMPOS_ADICIONALES = vObjetoArea.XML_CAMPOS_ADICIONALES;
                    vArea.ID_DEPARTAMENTO_PADRE = vObjetoArea.ID_DEPARTAMENTO_PADRE;
                    vArea.NB_DEPARTAMENTO_PADRE = vObjetoArea.NB_DEPARTAMENTO_PADRE;
                    vArea.CL_TIPO_DEPARTAMENTO = vObjetoArea.CL_TIPO_DEPARTAMENTO;

                    if (vArea != null)
                    {
                        txtNbCatalogo.Text = vArea.NB_DEPARTAMENTO;
                        txtClCatalogo.Text = vArea.CL_DEPARTAMENTO;
                        txtClCatalogo.ReadOnly = true;
                        chkActivo.Checked = vArea.FG_ACTIVO;
                       cmbTipoDepartamento.SelectedValue = vArea.CL_TIPO_DEPARTAMENTO;

                        RadListBoxItem vItem;
                        if (vArea.ID_DEPARTAMENTO_PADRE != null)
                            vItem = new RadListBoxItem(vArea.NB_DEPARTAMENTO_PADRE, vArea.ID_DEPARTAMENTO_PADRE.ToString());
                        else
                            vItem = new RadListBoxItem("No seleccionado", "");

                        lstDepartamentoJefe.Items.Clear();
                        lstDepartamentoJefe.Items.Add(vItem);
                    }
                }
                else
                {
                    chkActivo.Checked = false;
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "closeWindow(0);", true);
        }

        protected void btnSave_click(object sender, EventArgs e)
        {

            DepartamentoNegocio negocio = new DepartamentoNegocio();
            E_DEPARTAMENTO vAreaAgregar = new E_DEPARTAMENTO();
            if (!ptipo.Equals("Agregar"))
            {
                vArea.CL_DEPARTAMENTO = txtClCatalogo.Text;
                vArea.NB_DEPARTAMENTO = txtNbCatalogo.Text;
                vArea.FG_ACTIVO = chkActivo.Checked;
                vArea.CL_TIPO_DEPARTAMENTO = cmbTipoDepartamento.SelectedValue;
               // vArea.CL_TIPO_DEPARTAMENTO = "AREA";

                int vIdDepartamentoPadre = 0;
                if (int.TryParse(lstDepartamentoJefe.SelectedValue, out vIdDepartamentoPadre))
                    vArea.ID_DEPARTAMENTO_PADRE = vIdDepartamentoPadre;
                else
                    vArea.ID_DEPARTAMENTO_PADRE = null;

                E_RESULTADO vResultado = negocio.InsertaActualiza_M_DEPARTAMENTO(E_TIPO_OPERACION_DB.A.ToString(), vArea, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
            }
            else
            {
                vAreaAgregar.ID_DEPARTAMENTO = 1;
                vAreaAgregar.NB_DEPARTAMENTO = txtNbCatalogo.Text;
                vAreaAgregar.CL_DEPARTAMENTO = txtClCatalogo.Text;
                vAreaAgregar.FG_ACTIVO = chkActivo.Checked;
                vAreaAgregar.CL_TIPO_DEPARTAMENTO = cmbTipoDepartamento.SelectedValue;
               // vAreaAgregar.CL_TIPO_DEPARTAMENTO = "AREA";
                int vIdDepartamentoPadre = 0;
                if (int.TryParse(lstDepartamentoJefe.SelectedValue, out vIdDepartamentoPadre))
                    vAreaAgregar.ID_DEPARTAMENTO_PADRE = vIdDepartamentoPadre;
                else
                    vArea.ID_DEPARTAMENTO_PADRE = null;

                E_RESULTADO vResultado = negocio.InsertaActualiza_M_DEPARTAMENTO(E_TIPO_OPERACION_DB.I.ToString(), vAreaAgregar, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
            }
        }
    }
}