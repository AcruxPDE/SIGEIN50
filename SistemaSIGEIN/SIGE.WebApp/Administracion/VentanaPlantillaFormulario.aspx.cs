using SIGE.Entidades;
using SIGE.Entidades.Externas;
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
    public partial class VentanaPlantillaFormulario : System.Web.UI.Page
    {
        string vClTipoPlantilla
        {
            get { return (string)ViewState["vs_vClTipoPlantilla"]; }
            set { ViewState["vs_vClTipoPlantilla"] = value; }
        }

        int? vIdCampo
        {
            get { return (int?)ViewState["vs_vIdCampo"]; }
            set { ViewState["vs_vIdCampo"] = value; }
        }

        int? vIdPlantilla
        {
            get { return (int?)ViewState["vs_vIdPlantilla"]; }
            set { ViewState["vs_vIdPlantilla"] = value; }
        }

        string vClAccion
        {
            get { return (string)ViewState["vs_vClAccion"]; }
            set { ViewState["vs_vClAccion"] = value; }
        }

        string vClTipoTransaccion
        {
            get { return (string)ViewState["vs_vClTipoTransaccion"]; }
            set { ViewState["vs_vClTipoTransaccion"] = value; }
        }

        E_PLANTILLA vPlantilla
        {
            get { return (E_PLANTILLA)ViewState["vs_vPlantilla"]; }
            set { ViewState["vs_vPlantilla"] = value; }
        }
        List<E_CAMPO> listacambios
        {
            get { return (List<E_CAMPO>)ViewState["vs_vlistacambios"]; }
            set { ViewState["vs_vlistacambios"] = value; }
        }

        private XElement vLstCambiosModificados { get; set; }

        public string vClickedItemEventName = "ClickedItemEvent";
        string vClUsuario;
        string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vClAccion = (string)Request.QueryString["AccionCl"];
                vClTipoPlantilla = (string)Request.QueryString["PlantillaTipoCl"];
   
                int idPlantilla = 0;
                if (int.TryParse((string)Request.QueryString["PlantillaId"], out idPlantilla))
                    vIdPlantilla = idPlantilla;
                listacambios = new List<E_CAMPO>();
                PlantillaFormularioNegocio nPlantilla = new PlantillaFormularioNegocio();
                vPlantilla = nPlantilla.ObtienePlantilla(vIdPlantilla, vClTipoPlantilla);

                //vXmlCampos = XElement.Parse(vPlantilla.XML_PLANTILLA_FORMULARIO);

                switch (vClAccion)
                {
                    case "edit":
                        txtNbPlantilla.Text = vPlantilla.NB_PLANTILLA;
                        txtDsPlantilla.Text = vPlantilla.DS_PLANTILLA;
                        cmbTipoExposición.SelectedValue = vPlantilla.CL_EXPOSICION;
                        vClTipoTransaccion = "A";
                        break;
                    case "copy":
                        txtNbPlantilla.EmptyMessage = vPlantilla.NB_PLANTILLA;
                        txtDsPlantilla.EmptyMessage = vPlantilla.DS_PLANTILLA;
                        cmbTipoExposición.SelectedValue = vPlantilla.CL_EXPOSICION;
                        vClTipoTransaccion = "I";
                        break;
                    default:
                        cmbTipoExposición.SelectedValue = "INTERIOR";
                        break;
                }

                if (vPlantilla.CL_FORMULARIO == "SOLICITUD")
                    cmbTipoExposición.Enabled = true;


                CargarLista("PERSONAL", lstInformacionGeneral);
                CargarLista("ACADEMICA", lstFormacionAcademica);
                CargarLista("FAMILIAR", lstDatosFamiliares);
                CargarLista("LABORAL", lstExperienciaLaboral);
                CargarLista("COMPETENCIAS", lstInteresesCompetencias);
                CargarLista("ADICIONAL", lstInformacionAdicional);
                CargarLista(null, lstCamposDisponibles);
            }

            DespacharEventos(Request.Params.Get("__EVENTTARGET"), Request.Params.Get("__EVENTARGUMENT"));

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }

        protected void CargarLista(string pClContenedor, RadListBox pLista)
        {
            pLista.DataSource = vPlantilla.LST_CAMPOS.Where(w => w.CL_CONTENEDOR == pClContenedor).OrderBy(o => o.NO_ORDEN);//.Select(s => new RadListBoxItem(s.NB_CAMPO, s.ID_CAMPO.ToString()));
            pLista.DataValueField = "ID_CAMPO";
            pLista.DataTextField = "NB_CAMPO";
            pLista.DataBind();
        }

        protected void DespacharEventos(string pControlName, string pParameter)
        {
            if (pControlName == vClickedItemEventName)
                CargarDatosControl(pParameter);
        }

        protected void CargarDatosControl(string pIdCampo)
        {
            E_CAMPO vCampo = vPlantilla.LST_CAMPOS.FirstOrDefault(f => f.ID_CAMPO.ToString() == pIdCampo);

            if (vCampo != null)
            {
                vIdCampo = vCampo.ID_CAMPO;
                txtIdCampo.Text = vCampo.CL_CAMPO;
                txtNbCampo.Text = vCampo.NB_CAMPO;
                txtDsTooltip.Text = vCampo.DS_CAMPO;
                chkHabilitado.Checked = vCampo.FG_HABILITADO;
                chkRequerido.Checked = vCampo.FG_REQUERIDO;
                btnAplicar.Enabled = true;
            }
        }

        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            E_CAMPO vCampo = vPlantilla.LST_CAMPOS.FirstOrDefault(f => f.ID_CAMPO == vIdCampo);

            vCampo.NB_CAMPO = txtNbCampo.Text;
            vCampo.DS_CAMPO = txtDsTooltip.Text;
            vCampo.FG_REQUERIDO = chkRequerido.Checked;
            vCampo.FG_HABILITADO = chkHabilitado.Checked;

            XElement vXmlCampo = XElement.Parse(vCampo.XML_CAMPO);
            vXmlCampo.SetAttributeValue("NB_CAMPO", vCampo.NB_CAMPO);
            vXmlCampo.SetAttributeValue("FG_REQUERIDO", vCampo.FG_REQUERIDO ? "1" : "0");
            vXmlCampo.SetAttributeValue("FG_HABILITADO", vCampo.FG_HABILITADO ? "1" : "0");
            vXmlCampo.SetAttributeValue("NB_TOOLTIP", vCampo.DS_CAMPO);

            vCampo.XML_CAMPO = vXmlCampo.ToString();
            E_CAMPO cambiosCampo = new E_CAMPO();
            cambiosCampo.NB_CAMPO = vCampo.NB_CAMPO;
            cambiosCampo.DS_CAMPO = vCampo.DS_CAMPO;
            cambiosCampo.ID_CAMPO = vCampo.ID_CAMPO;

            listacambios.Add(cambiosCampo);

            UtilMensajes.MensajeResultadoDB(rwmAlertas, "Cambios aplicados correctamente.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: null);
            CargarLista("PERSONAL", lstInformacionGeneral);
            CargarLista("ACADEMICA", lstFormacionAcademica);
            CargarLista("FAMILIAR", lstDatosFamiliares);
            CargarLista("LABORAL", lstExperienciaLaboral);
            CargarLista("COMPETENCIAS", lstInteresesCompetencias);
            CargarLista("ADICIONAL", lstInformacionAdicional);
            CargarLista(null, lstCamposDisponibles);
            txtIdCampo.Text = "";
            txtNbCampo.Text = "";
            txtDsTooltip.Text = "";
            chkHabilitado.Checked = false;
            chkRequerido.Checked = false;


        }

        protected XElement BuscarXmlControl(string pIdCampo, XElement pXmlPlantilla)
        {
            foreach (XElement vXmlContenedor in pXmlPlantilla.Element("CONTENEDORES").Elements("CONTENEDOR"))
            {
                XElement vXmlControl = vXmlContenedor.Elements("CAMPO").FirstOrDefault(f => UtilXML.ValorAtributo<string>(f.Attribute("ID_CAMPO")) == pIdCampo);
                if (vXmlControl != null)
                    return vXmlControl;
            }
            return null;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            XElement vXmlPlantilla = new XElement("PLANTILLA");
            XElement vXmlContenedores = new XElement("CONTENEDORES");
            List<E_CAMPO> vLstCampos = new List<E_CAMPO>();

            XElement vXmlPersonal = new XElement("CONTENEDOR", new XAttribute("ID_CONTENEDOR", "PERSONAL"));
            vXmlPersonal.Add(lstInformacionGeneral.Items.Select(s => XElement.Parse(vPlantilla.LST_CAMPOS.FirstOrDefault(f => f.ID_CAMPO.ToString() == s.Value).XML_CAMPO)));
            vXmlContenedores.Add(vXmlPersonal);
            vLstCampos.AddRange(lstInformacionGeneral.Items.Select(s => vPlantilla.LST_CAMPOS.FirstOrDefault(f => f.ID_CAMPO.ToString() == s.Value)).Select(s => { s.CL_CONTENEDOR = "PERSONAL"; return s; }));

            XElement vXmlFamiliar = new XElement("CONTENEDOR", new XAttribute("ID_CONTENEDOR", "FAMILIAR"));
            vXmlFamiliar.Add(lstDatosFamiliares.Items.Select(s => XElement.Parse(vPlantilla.LST_CAMPOS.FirstOrDefault(f => f.ID_CAMPO.ToString() == s.Value).XML_CAMPO)));
            vXmlContenedores.Add(vXmlFamiliar);
            vLstCampos.AddRange(lstDatosFamiliares.Items.Select(s => vPlantilla.LST_CAMPOS.FirstOrDefault(f => f.ID_CAMPO.ToString() == s.Value)).Select(s => { s.CL_CONTENEDOR = "FAMILIAR"; return s; }));

            XElement vXmlAcademica = new XElement("CONTENEDOR", new XAttribute("ID_CONTENEDOR", "ACADEMICA"));
            vXmlAcademica.Add(lstFormacionAcademica.Items.Select(s => XElement.Parse(vPlantilla.LST_CAMPOS.FirstOrDefault(f => f.ID_CAMPO.ToString() == s.Value).XML_CAMPO)));
            vXmlContenedores.Add(vXmlAcademica);
            vLstCampos.AddRange(lstFormacionAcademica.Items.Select(s => vPlantilla.LST_CAMPOS.FirstOrDefault(f => f.ID_CAMPO.ToString() == s.Value)).Select(s => { s.CL_CONTENEDOR = "ACADEMICA"; return s; }));

            XElement vXmlLaboral = new XElement("CONTENEDOR", new XAttribute("ID_CONTENEDOR", "LABORAL"));
            vXmlLaboral.Add(lstExperienciaLaboral.Items.Select(s => XElement.Parse(vPlantilla.LST_CAMPOS.FirstOrDefault(f => f.ID_CAMPO.ToString() == s.Value).XML_CAMPO)));
            vXmlContenedores.Add(vXmlLaboral);
            vLstCampos.AddRange(lstExperienciaLaboral.Items.Select(s => vPlantilla.LST_CAMPOS.FirstOrDefault(f => f.ID_CAMPO.ToString() == s.Value)).Select(s => { s.CL_CONTENEDOR = "LABORAL"; return s; }));

            XElement vXmlCompetencias = new XElement("CONTENEDOR", new XAttribute("ID_CONTENEDOR", "COMPETENCIAS"));
            vXmlCompetencias.Add(lstInteresesCompetencias.Items.Select(s => XElement.Parse(vPlantilla.LST_CAMPOS.FirstOrDefault(f => f.ID_CAMPO.ToString() == s.Value).XML_CAMPO)));
            vXmlContenedores.Add(vXmlCompetencias);
            vLstCampos.AddRange(lstInteresesCompetencias.Items.Select(s => vPlantilla.LST_CAMPOS.FirstOrDefault(f => f.ID_CAMPO.ToString() == s.Value)).Select(s => { s.CL_CONTENEDOR = "COMPETENCIAS"; return s; }));

            XElement vXmlAdicional = new XElement("CONTENEDOR", new XAttribute("ID_CONTENEDOR", "ADICIONAL"));
            vXmlAdicional.Add(lstInformacionAdicional.Items.Select(s => XElement.Parse(vPlantilla.LST_CAMPOS.FirstOrDefault(f => f.ID_CAMPO.ToString() == s.Value).XML_CAMPO)));
            vXmlContenedores.Add(vXmlAdicional);
            vLstCampos.AddRange(lstInformacionAdicional.Items.Select(s => vPlantilla.LST_CAMPOS.FirstOrDefault(f => f.ID_CAMPO.ToString() == s.Value)).Select(s => { s.CL_CONTENEDOR = "ADICIONAL"; return s; }));

            vXmlPlantilla.Add(vXmlContenedores);

            vPlantilla.ID_PLANTILLA = vIdPlantilla ?? 0;
            vPlantilla.CL_FORMULARIO = vClTipoPlantilla;
            vPlantilla.NB_PLANTILLA = txtNbPlantilla.Text;
            vPlantilla.DS_PLANTILLA = txtDsPlantilla.Text;
            vPlantilla.CL_EXPOSICION = cmbTipoExposición.SelectedValue;
            vPlantilla.XML_PLANTILLA_FORMULARIO = vXmlPlantilla.ToString();
            vPlantilla.LST_CAMPOS = vLstCampos;

            if (listacambios != null)
            {
                var vXmlCampos = listacambios.Select(x =>
                                         new XElement("CAMPO",
                                         new XAttribute("ID_CAMPO_FORMULARIO", x.ID_CAMPO),
                                         new XAttribute("NB_CAMPO_FORMULARIO", x.NB_CAMPO),
                                         new XAttribute("DS_CAMPO_FORMULARIO", x.DS_CAMPO)
                              ));
                vLstCambiosModificados =
                new XElement("CAMPOS", vXmlCampos
                );

            }
            else
            {
                vLstCambiosModificados = new XElement("CAMPOS");

            }

            vPlantilla.XML_CAMPOS = vLstCambiosModificados.ToString();
            PlantillaFormularioNegocio nPlantilla = new PlantillaFormularioNegocio();
            E_RESULTADO vResultado = nPlantilla.InsertaActualizaPlantillaFormulario(vClTipoTransaccion, vPlantilla, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR);
        }
    }
}