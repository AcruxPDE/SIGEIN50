using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
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

namespace SIGE.WebApp.Administracion
{
    public partial class NuevaSolicitud : System.Web.UI.Page
    {

        private string vClUsuario = "jdiaz";
        private string vNbPrograma = "Departamentos";
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private string vClSecuencia
        {
            get { return (string)ViewState["vsClSecuencia"]; }
            set { ViewState["vsClSecuencia"] = value; }
        }
        private int vId_colonia
        {
            get { return (int)ViewState["vsId_colonia"]; }
            set { ViewState["vsId_colonia"] = value; }
        }
        private int vCONTADOR_FAMILIAR
        {
            get { return (int)ViewState["vsCONTADOR_FAMILIAR"]; }
            set { ViewState["vsCONTADOR_FAMILIAR"] = value; }
        }
        private int vID_CANDIDATO
        {
            get { return (int)ViewState["vsID_CANDIDATO"]; }
            set { ViewState["vsID_CANDIDATO"] = value; }
        }
        private int vID_SOLICITUD
        {
            get { return (int)ViewState["vsID_SOLICITUD"]; }
            set { ViewState["vsID_SOLICITUD"] = value; }
        }

        private int vID_FAMILIAR
        {
            get { return (int)ViewState["vsID_FAMILIAR"]; }
            set { ViewState["vsID_FAMILIAR"] = value; }
        }
        

        private List<E_DATOS_FAMILIARES> vFamiliares
        {
            get { return (List<E_DATOS_FAMILIARES>)ViewState["vsvFamiliares"]; }
            set { ViewState["vsvFamiliares"] = value; }
        }
        //private SPE_OBTIENE_C_CANDIDATO_Result vCandidatoEditar
        //{
        //    get { return (SPE_OBTIENE_C_CANDIDATO_Result)ViewState["vsCandidato"]; }
        //    set { ViewState["vsCandidato"] = value; }
        //}


         //
        public E_CANDIDATO E_Candidato;
        private List<E_PARENTESCO> vParentescos;

      
        protected void Page_Load(object sender, EventArgs e)
        {
            PlantillaFormularioNegocio nPlantillas = new PlantillaFormularioNegocio();
            CatalogoValorNegocio nValorGenerico = new CatalogoValorNegocio();
            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            CandidatoNegocio nCandidato = new CandidatoNegocio();

            if (!IsPostBack)
            {
                vCONTADOR_FAMILIAR = 0;
                //vID_CANDIDATO = -1;
                //vID_CANDIDATO = -1;
                vID_FAMILIAR = -1;

                vFamiliares = new List<E_DATOS_FAMILIARES>();
                //SecuenciaNegocio nSecuencia = new SecuenciaNegocio();
                //var vObjetoSecuencia = nSecuencia.Obtener_C_SECUENCIA().FirstOrDefault();
                //vClSecuencia = vObjetoSecuencia.CL_SECUENCIA;

                //var vUltiomFolio= nSecuencia.ObtieneFolioSecuencia(vClSecuencia).FirstOrDefault();
                //txtFolio.Text = vUltiomFolio.NO_SECUENCIA.ToString();



                /////////////////////////////////////INFORMACION PERSONAL CONTROLADORES/////////////////////////////////////////////


                var vPlantillas = nPlantillas.Obtener_C_PLANTILLA_FORMULARIO();
                cmbPlantillas.DataSource = vPlantillas;//LLENAMOS DE DATOS EL GRID
                cmbPlantillas.DataTextField = "CL_FORMULARIO";
                cmbPlantillas.DataTextField = "NB_PLANTILLA_SOLICITUD";
                cmbPlantillas.DataValueField = "ID_PLANTILLA_SOLICITUD";
                cmbPlantillas.DataBind();

                //EN DURO ID CATALOGO LISTA
                var vNacionalidades = nValorGenerico.Obtener_C_CATALOGO_VALOR(ID_CATALOGO_LISTA: ContextoApp.IdCatalogoNacionalidades);
                cmbNacionalidad.DataSource = vNacionalidades;//LLENAMOS DE DATOS EL GRID
                cmbNacionalidad.DataTextField = "CL_CATALOGO_VALOR";
                cmbNacionalidad.DataTextField = "NB_CATALOGO_VALOR";
                cmbNacionalidad.DataValueField = "CL_CATALOGO_VALOR";
                cmbNacionalidad.DataBind();


                var vGeneros = nValorGenerico.Obtener_C_CATALOGO_VALOR(ID_CATALOGO_LISTA: ContextoApp.IdCatalogoGeneros);
                cmbGeneros.DataSource = vGeneros;//LLENAMOS DE DATOS EL GRID
                cmbGeneros.DataTextField = "CL_CATALOGO_VALOR";
                cmbGeneros.DataTextField = "NB_CATALOGO_VALOR";
                cmbGeneros.DataValueField = "CL_CATALOGO_VALOR";
                cmbGeneros.DataBind();

                var vEstadosCivil = nValorGenerico.Obtener_C_CATALOGO_VALOR(ID_CATALOGO_LISTA: ContextoApp.IdCatalogoEstadosCivil);
                cmbEstadoCivil.DataSource = vEstadosCivil;//LLENAMOS DE DATOS EL GRID
                cmbEstadoCivil.DataTextField = "CL_CATALOGO_VALOR";
                cmbEstadoCivil.DataTextField = "NB_CATALOGO_VALOR";
                cmbEstadoCivil.DataValueField = "CL_CATALOGO_VALOR";
                cmbEstadoCivil.DataBind();



                ////////////////////////////////////////DATOS FAMILIARES CONTROLADORES/////////////////////////////////////////////////////////////

                CatalogoValorNegocio nValor = new CatalogoValorNegocio();
                var vParentescos=  nValor.Obtener_C_CATALOGO_VALOR(ID_CATALOGO_LISTA:ContextoApp.IdCatalogoParentescos);
                cmbParentesco.DataSource = vParentescos;
                //cmbParentesco.DataTextField = "CL_CATALOGO_VALOR";
                cmbParentesco.DataTextField = "NB_CATALOGO_VALOR";
                cmbParentesco.DataValueField = "CL_CATALOGO_VALOR";
                cmbParentesco.DataBind();


                /////////////////////////////////EDICION DE UNA SOLICITUD///////////////////////////////////////////// 
                if (Request.Params["ID"] != null)
                {

                    vID_SOLICITUD = int.Parse(Request.Params["ID"]);
                    var vSolicitud = nSolicitud.ObtieneSolicitudes(ID_SOLICITUD: vID_SOLICITUD).FirstOrDefault();
                    var vCandidatoEditar = nCandidato.Obtener_C_CANDIDATO(ID_CANDIDATO: vSolicitud.ID_CANDIDATO).FirstOrDefault();

                    vID_CANDIDATO = vCandidatoEditar.ID_CANDIDATO;
                    txtNombre.Text = vCandidatoEditar.NB_CANDIDATO;
                    txtApellidoP.Text = vCandidatoEditar.NB_APELLIDO_PATERNO;
                    txtApellidoM.Text = vCandidatoEditar.NB_APELLIDO_MATERNO;
                    cmbGeneros.SelectedValue = vCandidatoEditar.CL_GENERO;
                    txtRFC.Text = vCandidatoEditar.CL_RFC;
                    txtCURP.Text = vCandidatoEditar.CL_CURP;
                    cmbEstadoCivil.SelectedValue = vCandidatoEditar.CL_ESTADO_CIVIL;
                    txtNombreConyugue.Text = vCandidatoEditar.NB_CONYUGUE;
                    txtNSS.Text = vCandidatoEditar.CL_NSS;
                    //CL_TIPO_SANGUINEO = txt,
                    txtPais.Text = vCandidatoEditar.NB_PAIS;
                    txtEstado.Text = vCandidatoEditar.NB_ESTADO;
                    txtMunicipio.Text = vCandidatoEditar.NB_MUNICIPIO;

                    ColoniaNegocio nColonia = new ColoniaNegocio();
                    var vColoniaSeleccionada = nColonia.Obtener_C_COLONIA(NB_COLONIA: vCandidatoEditar.NB_COLONIA, CL_CODIGO_POSTAL: vCandidatoEditar.CL_CODIGO_POSTAL);
                    cmbColonias.DataSource = vColoniaSeleccionada;
                    cmbColonias.DataTextField = "CL_COLONIA";
                    cmbColonias.DataTextField = "NB_COLONIA";
                    cmbColonias.DataValueField = "ID_COLONIA";
                    cmbColonias.DataBind();
                    cmbColonias.SelectedValue = vColoniaSeleccionada.FirstOrDefault().ID_COLONIA + "";


                    txtCalle.Text = vCandidatoEditar.NB_CALLE;
                    txtNoInterior.Text = vCandidatoEditar.NO_INTERIOR;
                    txtNoExterior.Text = vCandidatoEditar.NO_EXTERIOR;
                    txtCP.Text = vCandidatoEditar.CL_CODIGO_POSTAL;
                    txtCorreoElectronico.Text = vCandidatoEditar.CL_CORREO_ELECTRONICO;
                    //FE_NACIMIENTO = Fe_Nacimient.SelectedDate,
                    txtLugarNac.Text = vCandidatoEditar.DS_LUGAR_NACIMIENTO;
                    //MN_SUELDO = txtmn,
                    cmbNacionalidad.SelectedValue = vCandidatoEditar.CL_NACIONALIDAD;
                    txtOtra.Text = vCandidatoEditar.DS_NACIONALIDAD;
                    txtLicenciaTipo.Text = vCandidatoEditar.NB_LICENCIA;
                    txtVehiculos.Text = vCandidatoEditar.DS_VEHICULO;
                    txtCartillaMilitar.Text = vCandidatoEditar.CL_CARTILLA_MILITAR;
                    txtCedulaProfesional.Text = vCandidatoEditar.CL_CEDULA_PROFESIONAL;
                    //Telefonos.ToString() = vCandidato.XML_TELEFONOS;

                    List<E_TELEFONOS> vTelefono = XElement.Parse(vCandidatoEditar.XML_TELEFONOS).Elements("TELEFONO").Select(el => new E_TELEFONOS
                    {
                        CELULAR = el.Attribute("CELULAR").Value,
                        CASA = el.Attribute("CASA").Value,
                        OTROS = el.Attribute("OTROS").Value
                    }).ToList();


                    txtCelula.Text = vTelefono.ElementAt(0).CELULAR;
                    txtTel.Text = vTelefono.ElementAt(0).CASA;
                    txtOtrosCelulares.Text = vTelefono.ElementAt(0).OTROS;



                    ParienteNegocio nPariente = new ParienteNegocio();
                    var vparientes = nPariente.Obtener_C_PARIENTE(ID_CANDIDATO:vID_CANDIDATO);
                   
                            grdDatosFamiliares.DataSource = vparientes;
                            grdDatosFamiliares.Rebind();
                    
                }


            }

            else { }
        }

        #region DATOS_FAMILIARES_NeedDataSource
        protected void grdDatosFamiliares_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdDatosFamiliares.DataSource = vFamiliares;
        }
        #endregion
        #region INFORMACION PERSONAL
        protected void btnGuardar_InformacionPersonal(object sender, EventArgs e)
        {

            XAttribute[] TELEFONOS = {
            new XAttribute("CELULAR", txtTel.Text),
            new XAttribute("CASA", txtCelula.Text),
            new XAttribute("OTROS", txtOtrosCelulares.Text)
            };
            XElement telefonos = 
                new XElement("TELEFONOS",
                new XElement("TELEFONO", TELEFONOS));

            E_Candidato = new E_CANDIDATO
            {
                ID_CANDIDATO = 1,
                NB_CANDIDATO = txtNombre.Text,
                NB_APELLIDO_PATERNO = txtApellidoP.Text,
                NB_APELLIDO_MATERNO = txtApellidoM.Text,
                CL_GENERO = cmbGeneros.SelectedItem.Text,
                CL_RFC = txtRFC.Text,
                CL_CURP = txtCURP.Text,
                CL_ESTADO_CIVIL = cmbEstadoCivil.SelectedItem.Text,
                NB_CONYUGUE = txtNombreConyugue.Text,
                CL_NSS = txtNSS.Text,
                //CL_TIPO_SANGUINEO = txt,
                NB_PAIS = txtPais.Text,
                NB_ESTADO = txtEstado.Text,
                NB_MUNICIPIO = txtMunicipio.Text,
                NB_COLONIA = cmbColonias.SelectedItem.Text,
                NB_CALLE = txtCalle.Text,
                NO_INTERIOR = txtNoInterior.Text,
                NO_EXTERIOR = txtNoExterior.Text,
                CL_CODIGO_POSTAL = txtCP.Text,
                CL_CORREO_ELECTRONICO = txtCorreoElectronico.Text,
                FE_NACIMIENTO = Fe_Nacimient.SelectedDate,
                DS_LUGAR_NACIMIENTO = txtLugarNac.Text,
                //MN_SUELDO = txtmn,
                CL_NACIONALIDAD = cmbNacionalidad.SelectedItem.Text,
                DS_NACIONALIDAD = txtOtra.Text,
                NB_LICENCIA = txtLicenciaTipo.Text,
                DS_VEHICULO = txtVehiculos.Text,
                CL_CARTILLA_MILITAR = txtCartillaMilitar.Text,
                CL_CEDULA_PROFESIONAL = txtCedulaProfesional.Text,
                XML_TELEFONOS = telefonos.ToString(),
                //XML_INGRESOS =,
                //XML_EGRESOS =,
                //XML_PATRIMONIO =,
                //DS_DISPONIBILIDAD =,
                //CL_DISPONIBILIDAD_VIAJE =,
                //XML_PERFIL_RED_SOCIAL =,
                //DS_COMENTARIO =,
                //FG_ACTIVO =,
            };

            CandidatoNegocio nCandidato = new CandidatoNegocio();
            E_RESULTADO vResultado = nCandidato.InsertaActualiza_C_CANDIDATO(tipo_transaccion: E_TIPO_OPERACION_DB.I.ToString(), V_C_CANDIDATO: E_Candidato, usuario: vClUsuario, programa: vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            //  UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR,400,150,null);

            int x;
            bool isNumeric = int.TryParse(vMensaje, out x);
            vID_CANDIDATO = x;
            if (isNumeric)
            {

                E_SOLICITUD vSolicitud = new E_SOLICITUD
              {
                  ID_SOLICITUD = 1,
                  //ID_EMPLEADO = 0,
                  //ID_REQUISICION =,
                  ID_CANDIDATO = vID_CANDIDATO,
                  ID_DESCRIPTIVO = 2, //OBSERVACION
                  ID_PLANTILLA_SOLICITUD = int.Parse(cmbPlantillas.SelectedValue),
                  //CL_SOLICITUD ="",
                  //CL_ACCESO_EVALUACION ="",
                  CL_SOLICITUD_ESTATUS = E_SOLICITUD_ESTATUS.INICIADA.ToString(),
                  FE_SOLICITUD = Fe_Solicitud.SelectedDate
              };

                SolicitudNegocio nSolicitud = new SolicitudNegocio();
                E_RESULTADO vResultadoSolicitud = nSolicitud.InsertaActualiza_K_SOLICITUD(tipo_transaccion: E_TIPO_OPERACION_DB.I.ToString(), usuario: vClUsuario, programa: vNbPrograma, V_K_SOLICITUD: vSolicitud);
                string vMensajeSolicitud = vResultadoSolicitud.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensajeSolicitud, vResultado.CL_TIPO_ERROR, 400, 150, null);

                if (vResultadoSolicitud.CL_TIPO_ERROR.ToString().Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL.ToString()))
                {
                    tbNuevaSolicitud.SelectedIndex = 1;
                    tbNuevaSolicitud.Enabled = true;
                    mpgNuevaSolicitud.SelectedIndex = 1;
                }
            }


        }
        protected void RadInput_TextChanged(object sender, EventArgs e)
        {
            ColoniaNegocio nColonia = new ColoniaNegocio();
            if (sender is RadTextBox)
            {
                if (txtCP.Text.Length > 2)
                {
                    var vColonias = nColonia.Obtener_C_COLONIA_CP(CL_CODIGO_POSTAL: txtCP.Text);
                    if (vColonias != null && vColonias.Count > 0)
                    {
                        cmbColonias.DataSource = vColonias;//LLENAMOS DE DATOS EL GRID
                        cmbColonias.DataTextField = "CL_COLONIA";
                        cmbColonias.DataTextField = "NB_COLONIA";
                        cmbColonias.DataValueField = "ID_COLONIA";
                        cmbColonias.DataBind();

                        var vDatosLocalizacion = vColonias.ElementAt(0);
                        txtPais.Text = vDatosLocalizacion.CL_PAIS.ToString();
                        txtMunicipio.Text = vDatosLocalizacion.CL_MUNICIPIO.ToString();
                        txtEstado.Text = vDatosLocalizacion.CL_ESTADO.ToString();
                    }
                }
                else
                {
                    txtPais.Text = "";
                    txtMunicipio.Text = "";
                    txtEstado.Text = "";
                    cmbColonias.Items.Clear();
                    cmbColonias.DataBind();
                }
            }
        }
        protected void cmbColonias_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            int vId_colonia = int.Parse(e.Value);
            ColoniaNegocio nColonia = new ColoniaNegocio();
            var vColoniaSeleccionada = nColonia.Obtener_C_COLONIA(ID_COLONIA: vId_colonia).FirstOrDefault();
            //txtPais.Text = vColoniaSeleccionada.CL_PAIS.ToString();
            //txtMunicipio.Text = vColoniaSeleccionada.CL_MUNICIPIO.ToString();
            //txtEstado.Text = vColoniaSeleccionada.CL_ESTADO.ToString();
        }
        #endregion


        #region DATOS FAMILIARES
        protected void btnAgregar_DatosFamiliares(object sender, EventArgs e)
        {
         


                    //ID_FAMILIAR = vID_FAMILIAR,
                    //NB_FAMILIAR = DFtxtNombre.Text,
                    //CL_PARENTESCO = cmbParentesco.SelectedItem.Text,
                    //FE_NACIMIENTO = DFFe_Nac.SelectedDate,
                    //NB_OCUPACION = DFtxtOcupacion.Text,
                    //FG_DEPENDIENTE_ECON = DFtxtDependienteEconomico.Checked,
                    //NB_DEPENDIENTE_ECON = nb_dependiente_econ = (DFtxtDependienteEconomico.Checked) ? "Sí" : "No"
            
            //vcandidatoUpdate.FirstOrDefault().XML_DATOS_FAMILIARES = FAMILIARES.ToString();
            //E_RESULTADO vResultado = nCandidato.InsertaActualiza_C_CANDIDATO(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), usuario: vClUsuario, programa: vNbPrograma, V_C_CANDIDATO: vcandidatoUpdate.FirstOrDefault());
            //string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            //UtilMensajes.MensajeResultadoDB(rnMensaje, "Proceso exitoso", vResultado.CL_TIPO_ERROR, 400, 150, null);
            //ActualizarDatosFamiliares();
            
        }

        protected void DFbtnSave_Click(object sender, EventArgs e)
        {

        }

        protected void DFbtnEditar_Click(object sender, EventArgs e)
        {

          
            foreach (GridDataItem item in grdDatosFamiliares.SelectedItems)
            {
                //vID_FAMILIAR = (int.Parse(item.GetDataKeyValue("ID_FAMILIAR").ToString()));
                //var vdata_familiar= vFamiliares.Where(x => x.ID_FAMILIAR == vID_FAMILIAR).ToList();
                //vID_FAMILIAR = vdata_familiar.FirstOrDefault().ID_FAMILIAR;
                //DFtxtNombre.Text = vdata_familiar.FirstOrDefault().NB_FAMILIAR;
                //cmbParentesco.SelectedValue = vdata_familiar.FirstOrDefault().CL_PARENTESCO;
                //DFFe_Nac.SelectedDate =vdata_familiar.FirstOrDefault().FE_NACIMIENTO;
                //DFtxtOcupacion.Text = vdata_familiar.FirstOrDefault().NB_OCUPACION;
                //DFtxtDependienteEconomico.Checked = vdata_familiar.FirstOrDefault().FG_DEPENDIENTE_ECON;
            
            }
        }
        protected void btnEliminar_click(object sender, EventArgs e)
        {
            CandidatoNegocio nCandidato = new CandidatoNegocio();
            foreach (GridDataItem item in grdDatosFamiliares.SelectedItems)
            {
                //vID_FAMILIAR = (int.Parse(item.GetDataKeyValue("ID_FAMILIAR").ToString()));
                //vFamiliares.RemoveAt(vID_FAMILIAR);
                //var vXelements = vFamiliares.Select(x =>
                //                                new XElement("FAMILIAR",
                //                                new XAttribute("ID_FAMILIAR", x.ID_FAMILIAR),
                //                                new XAttribute("NB_FAMILIAR", x.NB_FAMILIAR),
                //                                new XAttribute("CL_PARENTESCO", x.CL_PARENTESCO),
                //                                new XAttribute("FE_NACIMIENTO", x.FE_NACIMIENTO),
                //                                new XAttribute("NB_OCUPACION", x.NB_OCUPACION),
                //                                new XAttribute("FG_DEPENDIENTE_ECON", x.FG_DEPENDIENTE_ECON),
                //                                new XAttribute("NB_DEPENDIENTE_ECON", x.NB_DEPENDIENTE_ECON)
                //                    ));

                //XElement FAMILIARES =
                //new XElement("FAMILIARES", vXelements
                //);

                //var vCandidatoActualizar = nCandidato.Obtener_C_CANDIDATO(ID_CANDIDATO: vID_CANDIDATO);
                //List<E_CANDIDATO> vLlenarLista = new List<E_CANDIDATO>();
                //List<E_CANDIDATO> vcandidatoUpdate = ParsedListDatosFamiliares(vLlenarLista, vCandidatoActualizar);

                //vID_FAMILIAR = -1;
                //vcandidatoUpdate.FirstOrDefault().XML_DATOS_FAMILIARES = FAMILIARES.ToString();
                //E_RESULTADO vResultado = nCandidato.InsertaActualiza_C_CANDIDATO(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), usuario: vClUsuario, programa: vNbPrograma, V_C_CANDIDATO: vcandidatoUpdate.FirstOrDefault());
                //string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                //UtilMensajes.MensajeResultadoDB(rnMensaje, "Proceso exitoso", vResultado.CL_TIPO_ERROR, 400, 150, null);
                //ActualizarDatosFamiliares();
            }

        }
        public void ActualizarDatosFamiliares()
        {
            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            CandidatoNegocio nCandidato = new CandidatoNegocio();
            if (Request.Params["ID"] != null)
            {
                //var vSolicitud = nSolicitud.Obtener_K_SOLICITUD(ID_SOLICITUD: vID_SOLICITUD).FirstOrDefault();
                //var vCandidatoEditar = nCandidato.Obtener_C_CANDIDATO(ID_CANDIDATO: vSolicitud.ID_CANDIDATO).FirstOrDefault();
                //if (vCandidatoEditar.XML_DATOS_FAMILIARES != null)
                //{
                //    vFamiliares = XElement.Parse(vCandidatoEditar.XML_DATOS_FAMILIARES).Elements("FAMILIAR").Select(el => new E_DATOS_FAMILIARES
                //    {
                //        ID_FAMILIAR = (int)UtilXML.ValorAtributo(el.Attribute("ID_FAMILIAR"), E_TIPO_DATO.INT),
                //        NB_FAMILIAR = el.Attribute("NB_FAMILIAR").Value,
                //        CL_PARENTESCO = el.Attribute("CL_PARENTESCO").Value,
                //        FE_NACIMIENTO = (DateTime)UtilXML.ValorAtributo(el.Attribute("FE_NACIMIENTO"), E_TIPO_DATO.DATETIME),
                //        NB_OCUPACION = el.Attribute("NB_OCUPACION").Value,
                //        FG_DEPENDIENTE_ECON = (Boolean)UtilXML.ValorAtributo(el.Attribute("FG_DEPENDIENTE_ECON"), E_TIPO_DATO.BOOLEAN),
                //        NB_DEPENDIENTE_ECON = el.Attribute("NB_DEPENDIENTE_ECON").Value
                //    }).ToList();

                //        grdDatosFamiliares.DataSource = vFamiliares;
                //        grdDatosFamiliares.Rebind();
                  
                //}
            }
        }

        public List <E_CANDIDATO> ParsedListDatosFamiliares(List <E_CANDIDATO> x, List <SPE_OBTIENE_C_CANDIDATO_Result> y) 
        {
            x = y.Select(item => new E_CANDIDATO()
            {
                ID_CANDIDATO = y.FirstOrDefault().ID_CANDIDATO,
                NB_CANDIDATO = y.FirstOrDefault().NB_CANDIDATO,
                NB_APELLIDO_PATERNO = y.FirstOrDefault().NB_APELLIDO_PATERNO,
                NB_APELLIDO_MATERNO = y.FirstOrDefault().NB_APELLIDO_MATERNO,
                CL_GENERO = y.FirstOrDefault().CL_GENERO,
                CL_RFC = y.FirstOrDefault().CL_RFC,
                CL_CURP = y.FirstOrDefault().CL_CURP,
                CL_ESTADO_CIVIL = y.FirstOrDefault().CL_ESTADO_CIVIL,
                NB_CONYUGUE = y.FirstOrDefault().NB_CONYUGUE,
                CL_NSS = y.FirstOrDefault().CL_NSS,
                //CL_TIPO_SANGUINEO = txt,
                NB_PAIS = y.FirstOrDefault().NB_PAIS,
                NB_ESTADO = y.FirstOrDefault().NB_ESTADO,
                NB_MUNICIPIO = y.FirstOrDefault().NB_MUNICIPIO,
                NB_COLONIA = y.FirstOrDefault().NB_COLONIA,
                NB_CALLE = y.FirstOrDefault().NB_CALLE,
                NO_INTERIOR = y.FirstOrDefault().NO_INTERIOR,
                NO_EXTERIOR = y.FirstOrDefault().NO_EXTERIOR,
                CL_CODIGO_POSTAL = y.FirstOrDefault().CL_CODIGO_POSTAL,
                CL_CORREO_ELECTRONICO = y.FirstOrDefault().CL_CORREO_ELECTRONICO,
                FE_NACIMIENTO = y.FirstOrDefault().FE_NACIMIENTO,
                DS_LUGAR_NACIMIENTO = y.FirstOrDefault().DS_LUGAR_NACIMIENTO,
                //MN_SUELDO = txtmn,
                CL_NACIONALIDAD = y.FirstOrDefault().CL_NACIONALIDAD,
                DS_NACIONALIDAD = y.FirstOrDefault().DS_NACIONALIDAD,
                NB_LICENCIA = y.FirstOrDefault().NB_LICENCIA,
                DS_VEHICULO = y.FirstOrDefault().DS_VEHICULO,
                CL_CARTILLA_MILITAR = y.FirstOrDefault().CL_CARTILLA_MILITAR,
                CL_CEDULA_PROFESIONAL = y.FirstOrDefault().CL_CEDULA_PROFESIONAL,
                XML_TELEFONOS = y.FirstOrDefault().XML_TELEFONOS,
                //XML_INGRESOS =,
                //XML_EGRESOS =,
                //XML_PATRIMONIO =,
                //DS_DISPONIBILIDAD =,
                //CL_DISPONIBILIDAD_VIAJE =,
                //XML_PERFIL_RED_SOCIAL =,
                XML_DATOS_FAMILIARES = item.XML_DATOS_FAMILIARES,
                //DS_COMENTARIO =,
                //FG_ACTIVO =,

            }).ToList();


            return x;
        }


        protected void DFbtnAgregar_Familiar(object sender, EventArgs e)
        {
                tbNuevaSolicitud.SelectedIndex = 2;
                tbNuevaSolicitud.Enabled = true;
                mpgNuevaSolicitud.SelectedIndex = 2;
      
        }
     
        
        #endregion // FIN DE DATOS FAMILIARES

      
       
        #region FORMACION ACADEMICA
        #endregion

        #region EXPERIENCIA LABORAL
        #endregion

        #region INTERESES Y COMPETENCIAS
        #endregion

        #region INFORMACION ADICIONAL
        #endregion

        #region VISTA PREVIA
        #endregion
    }

    [Serializable]
    public class E_DATOS_FAMILIARES 
    {

        /* Nombre	Parentesco	Fecha de nacimiento	Ocupación  Dependiente económico*/
        public int ID_FAMILIAR { get; set; }
        public string NB_FAMILIAR { get; set; }
        public string CL_PARENTESCO { get; set; }
        public DateTime? FE_NACIMIENTO { get; set; }
        public string NB_OCUPACION { get; set; }
        public Boolean FG_DEPENDIENTE_ECON { get; set; }
        public string NB_DEPENDIENTE_ECON { get; set; }
    }
 
}