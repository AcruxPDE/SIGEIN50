using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.AdministracionSitio;
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
    public partial class VentanaInventarioPersonalNomina : System.Web.UI.Page
    {
        #region Variables

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        public string vClUsuario;
        string vNbPrograma;

        protected int? vIdEmpleado
        {
            get { return (int?)ViewState["vs_vIdEmpleado"]; }
            set { ViewState["vs_vIdEmpleado"] = value; }
        }

        protected string vClEstadoEmpleado
        {
            get { return (string)ViewState["vs_vClEstadoEmpleado"]; }
            set { ViewState["vs_vClEstadoEmpleado"] = value; }
        }

        #endregion

        #region Metodos

        protected bool ValidaControles()
        {
            if (txtClEmpleado.Text != null && txtClEmpleado.Text != "")
                if (txtNombre.Text != null && txtNombre.Text != "")
                    if (txtPaterno.Text != null && txtPaterno.Text != "")
                        return true;
                        //if (txtMaterno.Text != null && txtMaterno.Text != "")
                        //    return true;

            return false;
        }

        protected void HabilitarControles(bool? pFgNomina, bool? pFgDO)
        {
            if (pFgNomina == false)
            {
                btnNODOTrue.Enabled = false;
                btnNODOFalse.Enabled = false;
                cmbRazonSocial.Enabled = false;
                txtClEmpleadoNomina.Enabled = false;
            }

            if (pFgDO == false)
            {
                btnNODOTrue.Enabled = false;
                btnNODOFalse.Enabled = false;
                btnBuscarPuesto.Enabled = false;
                txtSueldo.Enabled = false;
                btnBonoFalse.Enabled = false;
                btnBonoTrue.Enabled = false;
                btnTabuladorFalse.Enabled = false;
                btnTabuladorTrue.Enabled = false;
                btnInventarioFalse.Enabled = false;
                btnInventarioTrue.Enabled = false;
                btnCapturarAlcance.Enabled = false;
            }
        }

        //protected string GenerarLista()
        //{

        //    XElement pEmpleado = null;

        //    var vXmlEmpleado = new XElement("EMPLEADO",
        //                                   new XAttribute("CL_EMPLEADO", txtClEmpleado.Text),
        //                                   new XAttribute("NB_EMPLEADO", txtNombre.Text),
        //                                   new XAttribute("NB_APELLIDO_PATERNO", txtPaterno.Text),
        //                                   new XAttribute("NB_APELLIDO_MATERNO", txtMaterno.Text),
        //                                   new XAttribute("FG_DO", btnDOTrue.Checked ? true : false),
        //                                   new XAttribute("FG_NOMINA", btnNOTrue.Checked ? true : false),
        //                                   new XAttribute("FG_NOMINA_DO", btnNODOTrue.Checked ? true : false),
        //                                   new XAttribute("ID_PLAZA_DO", lstPuesto.SelectedValue != null ? lstPuesto.SelectedValue.ToString() : ""),
        //                                   new XAttribute("ID_PUESTO_NOMINA", lstPuestoNomina.SelectedValue != null ? lstPuestoNomina.SelectedValue.ToString() : ""),
        //                                   new XAttribute("CL_EMPLEADO_NOMINA", txtClEmpleadoNomina.Text),
        //                                   new XAttribute("ID_RAZON_SOCIAL", cmbRazonSocial.SelectedValue),
        //                                   new XAttribute("SUELDO_MENSUAL", txtMnSueldoMensual.Text == "" ? 0 : decimal.Parse(txtMnSueldoMensual.Text)),
        //                                   new XAttribute("SUELDO_DIARIO", txtMnSueldoDiario.Text == "" ? 0 : decimal.Parse(txtMnSueldoDiario.Text)),
        //                                   new XAttribute("BASE_COTIZACION", txtMnSueldoBase.Text == "" ? 0 : decimal.Parse(txtMnSueldoBase.Text)),
        //                                   new XAttribute("SUELDO_DO", txtSueldo.Text == "" ? 0 : decimal.Parse(txtSueldo.Text)),
        //                                   new XAttribute("FG_SUELDO_INVENTARIO", btnInventarioTrue.Checked ? true : false),
        //                                   new XAttribute("FG_SUELDO_TABULADOR", btnTabuladorTrue.Checked ? true : false),
        //                                   new XAttribute("FG_SUELGO_BONO", btnBonoTrue.Checked ? true : false)
        //                                   );
        //    pEmpleado = new XElement("EMPLEADOS", vXmlEmpleado);

        //    return pEmpleado.ToString();

        //}

        protected void CargarDatos()
        {
            CamposNominaNegocio oNegocio = new CamposNominaNegocio();
            SPE_OBTIENE_EMPLEADOS_NOMINA_DO_Result vEmpleado = oNegocio.ObtieneEmpleadosNominaDo(pID_EMPLEADO_NOMINA_DO: vIdEmpleado).FirstOrDefault();

            txtClEmpleado.Text = vEmpleado.M_EMPLEADO_CL_EMPLEADO;
            txtNombre.Text = vEmpleado.NB_EMPLEADO;
            txtPaterno.Text = vEmpleado.NB_APELLIDO_PATERNO;
            txtMaterno.Text = vEmpleado.NB_APELLIDO_MATERNO;
            txtClEmpleadoNomina.Text = vEmpleado.CL_EMPLEADO_NOMINA;
            btnDOTrue.Checked = (bool)vEmpleado.FG_DO;
            btnDOFalse.Checked = !(bool)vEmpleado.FG_DO;
            btnNOTrue.Checked = (bool)vEmpleado.FG_NOMINA;
            btnNOFalse.Checked = !(bool)vEmpleado.FG_NOMINA;
            btnNODOTrue.Checked = (bool)vEmpleado.FG_NOMINA_DO;
            btnNODOFalse.Checked = !(bool)vEmpleado.FG_NOMINA_DO;
            txtSueldo.Text = vEmpleado.SUELDO_DO.ToString();
            btnTabuladorTrue.Checked = vEmpleado.FG_SUELDO_VISIBLE_TABULADOR;
            btnTabuladorFalse.Checked = !vEmpleado.FG_SUELDO_VISIBLE_TABULADOR;
            btnInventarioTrue.Checked = vEmpleado.FG_SUELDO_VISIBLE_INVENTARIO;
            btnInventarioFalse.Checked = !vEmpleado.FG_SUELDO_VISIBLE_INVENTARIO;
            btnBonoTrue.Checked = vEmpleado.FG_SUELDO_VISIBLE_BONO;
            btnBonoFalse.Checked = !vEmpleado.FG_SUELDO_VISIBLE_BONO;
            txtMnSueldoMensual.Text = vEmpleado.SUELDO_MENSUAL.ToString();
            txtMnSueldoDiario.Text = vEmpleado.SUELDO_DIARIO.ToString();
            txtMnSueldoBase.Text = vEmpleado.BASE_COTIZACION.ToString();


            HabilitarControles(vEmpleado.FG_NOMINA, vEmpleado.FG_DO);


            vClEstadoEmpleado = vEmpleado.M_EMPLEADO_CL_ESTADO_EMPLEADO;
            if (vEmpleado.FG_DO == true && vClEstadoEmpleado != "ALTA")
            {
                btnGuardar.Enabled = false;
            }

            if (vEmpleado.FG_NOMINA_DO == false )
            {
                if (ContextoApp.ANOM.LicenciaAccesoModulo.MsgActivo == "1")
                ClientScript.RegisterStartupScript(GetType(), "script", "MostrarDiv();", true);

                if (vEmpleado.ID_PUESTO_NOMINA != null)
                {
                    lstPuestoNomina.Items.Clear();
                    lstPuestoNomina.Items.Add(new RadListBoxItem(vEmpleado.NB_PUESTO_NOMINA, vEmpleado.ID_PUESTO_NOMINA.ToString()));
                    lstPuestoNomina.SelectedValue = vEmpleado.ID_PUESTO_NOMINA.ToString();
                }

            }

            if (vEmpleado.ID_PUESTO_DO != null)
            {
                if (vEmpleado.ID_PLAZA != null)
                {
                    lstPuesto.Items.Clear();
                    lstPuesto.Items.Add(new RadListBoxItem(vEmpleado.M_PUESTO_NB_PUESTO, vEmpleado.ID_PLAZA.ToString()));
                    lstPuesto.SelectedValue = vEmpleado.ID_PLAZA.ToString();
                }
            }
            if (vEmpleado.ID_RAZON_SOCIAL != null)
            {
                cmbRazonSocial.SelectedValue = vEmpleado.ID_RAZON_SOCIAL.ToString();
                cmbRazonSocial.Text = vEmpleado.CL_RAZON_SOCIAL;
            }

            //if (vEmpleado.XML_TELEFONOS != null)
            //{
            //    XElement VXmlTelefonos = XElement.Parse(vEmpleado.XML_TELEFONOS);
            //    foreach (XElement item in VXmlTelefonos.Elements("TELEFONO"))
            //        cmbTelefonos.Items.Add(new RadComboBoxItem()
            //        {
            //            Text = UtilXML.ValorAtributo<string>(item.Attribute("NO_TELEFONO")),
            //            Value = UtilXML.ValorAtributo<string>(item.Attribute("NO_TELEFONO")),
            //        });
            //}

            //if (vEmpleado.REDES_SOCIALES != null)
            //{

            //    XElement vXmlRedesSociales = XElement.Parse(vEmpleado.REDES_SOCIALES);
            //    foreach (XElement item in vXmlRedesSociales.Elements("RED_SOCIAL"))
            //        cmbRedesSociales.Items.Add(new RadComboBoxItem()
            //        {
            //            Text = UtilXML.ValorAtributo<string>(item.Attribute("CL_RED_SOCIAL")) + " - " + UtilXML.ValorAtributo<string>(item.Attribute("NB_PERFIL")),
            //            Value = UtilXML.ValorAtributo<string>(item.Attribute("CL_RED_SOCIAL")),
            //        });
            //}

            //if (vEmpleado.AREA_INTERES != null)
            //{
            //    XElement vXmlAreasInteres = XElement.Parse(vEmpleado.AREA_INTERES);
            //    foreach (XElement item in vXmlAreasInteres.Elements("AREA"))
            //        cmbAreaInteres.Items.Add(new RadComboBoxItem()
            //        {
            //            Text = UtilXML.ValorAtributo<string>(item.Attribute("NB_AREA_INTERES")),
            //            Value = UtilXML.ValorAtributo<string>(item.Attribute("NB_AREA_INTERES")),
            //        });
            //}

            //if (vEmpleado.IDIOMAS != null)
            //{
            //    XElement vXmlIdiomas = XElement.Parse(vEmpleado.IDIOMAS);
            //    foreach (XElement item in vXmlIdiomas.Elements("IDIOMA"))
            //        cmbIdiomas.Items.Add(new RadComboBoxItem()
            //        {
            //            Text = UtilXML.ValorAtributo<string>(item.Attribute("NB_IDIOMA")),
            //            Value = UtilXML.ValorAtributo<string>(item.Attribute("NB_IDIOMA")),
            //        });
            //}

            //if (vEmpleado.COMPETENCIAS != null)
            //{
            //    XElement vXmlCompetencias = XElement.Parse(vEmpleado.COMPETENCIAS);
            //    foreach (XElement item in vXmlCompetencias.Elements("COMPETENCIA"))
            //        cmbCompetencias.Items.Add(new RadComboBoxItem()
            //        {
            //            Text = UtilXML.ValorAtributo<string>(item.Attribute("NB_COMPETENCIA")),
            //            Value = UtilXML.ValorAtributo<string>(item.Attribute("NB_COMPETENCIA")),
            //        });
            //}

            //txtJefeDirecto.Text = vEmpleado.NB_JEFE;
            //txtRfc.Text = vEmpleado.CL_RFC;
            //txtCurp.Text = vEmpleado.CL_CURP;
            //txtNss.Text = vEmpleado.CL_NSS;
            //txtLugarNacimiento.Text = vEmpleado.DS_LUGAR_NACIMIENTO;
            //txtNacionalidad.Text = vEmpleado.DS_NACIONALIDAD;
            //txtFeNacimiento.Text = vEmpleado.FE_NACIMIENTO != null ? vEmpleado.FE_NACIMIENTO.Value.ToShortDateString() : "";
            //txtGenero.Text = vEmpleado.CL_GENERO;
            //txtEstadoCivil.Text = vEmpleado.CL_ESTADO_CIVIL;
            //txtCp.Text = vEmpleado.CL_CODIGO_POSTAL;
            //txtEstado.Text = vEmpleado.NB_ESTADO;
            //txtMunicipio.Text = vEmpleado.NB_MUNICIPIO;
            //txtColonia.Text = vEmpleado.NB_COLONIA;
            //txtCalle.Text = vEmpleado.NB_CALLE;
            //txtNoExterior.Text = vEmpleado.NO_EXTERIOR;
            //txtNoInterior.Text = vEmpleado.NO_INTERIOR;
            //txtEmail.Text = vEmpleado.CL_CORREO_ELECTRONICO;
            //txtCartillaMilitar.Text = vEmpleado.CL_CARTILLA_MILITAR;
            //txtEmpresa.Text = vEmpleado.M_EMPLEADO_NB_EMPRESA;
            //txtSueldoMensual.Text = vEmpleado.SUELDO_MENSUAL != null ? vEmpleado.SUELDO_MENSUAL.ToString() : "";
            //txtFolioSolicitud.Text = vEmpleado.CL_SOLICITUD;
            //txtCentroAdm.Text = vEmpleado.CL_CENTRO_ADMINISTRATIVO;
            //txtCentroOp.Text = vEmpleado.CL_OPERATIVO;
            //txtDispersionGasolina.Text = vEmpleado.FORMATO_DISPERSION_GASOLINA;
            //txtDispersionVales.Text = vEmpleado.FORMATO_DISPERSION_VALES;
            //txtDispersionNomina.Text = vEmpleado.FORMATO_DISPERSION_NOMINA;
            //txtFeIngreso.Text = vEmpleado.FECHA_INGRESO != null ? vEmpleado.FECHA_INGRESO.Value.ToShortDateString() : "";
            //txtFePlanta.Text = vEmpleado.FECHA_PLANTA != null ? vEmpleado.FECHA_PLANTA.Value.ToShortDateString() : "";
            //txtFeAntiguedad.Text = vEmpleado.FECHA_ANTIGUEDAD != null ? vEmpleado.FECHA_ANTIGUEDAD.Value.ToShortDateString() : "";
            //txtFeBaja.Text = vEmpleado.FECHA_BAJA != null ? vEmpleado.FECHA_BAJA.ToString() : "";
            //txtTipoNomina.Text = vEmpleado.TIPO_NOMINA;
            //txtFormaPago.Text = vEmpleado.FORMA_PAGO != null ? vEmpleado.FORMA_PAGO.ToString() : "";
            //txtBaseCotizacion.Text = vEmpleado.FACTOR_SALARIO_BASE_COTIZACION != null ? vEmpleado.FACTOR_SALARIO_BASE_COTIZACION.ToString() : "";
            //txtCotizacionFija.Text = vEmpleado.SALARIO_BASE_COTIZACION_FIJO != null ? vEmpleado.SALARIO_BASE_COTIZACION_FIJO.ToString() : "";
            //txtCotizacionVariable.Text = vEmpleado.SALARIO_BASE_COTIZACION_VARIABLE != null ? vEmpleado.SALARIO_BASE_COTIZACION_VARIABLE.ToString() : "";
            //txtCotizacionMaximo.Text = vEmpleado.SALARIO_BASE_COTIZACION_MAXIMO != null ? vEmpleado.SALARIO_BASE_COTIZACION_MAXIMO.ToString() : "";
            //txtCotizaImss.Text = vEmpleado.COTIZA_IMSS;
            //txtClBancaria.Text = vEmpleado.CLABE_BANCARIA;
            //txtClPago.Text = vEmpleado.CLABE_PAGO;
            //txtCtaVales.Text = vEmpleado.CUENTA_VALES_DESPENSA;
            //txtUltPrimaVacacional.Text = vEmpleado.FE_ULTIMA_PRIMA_VACACIONAL != null ? vEmpleado.FE_ULTIMA_PRIMA_VACACIONAL.Value.ToShortDateString() : "";
            //txtUltAguinaldo.Text = vEmpleado.FE_ULTIMO_AGUINALDO != null ? vEmpleado.FE_ULTIMO_AGUINALDO.Value.ToShortDateString() : "";
            //txtTipoSAT.Text = vEmpleado.CL_TIPO_CONTRATO_SAT;
            //txtJornadaSAT.Text = vEmpleado.TIPO_JORNADA_SAT;
            //txtRegPatronal.Text = vEmpleado.REGISTRO_PATRONAL;
            //txtUnidadFamiliar.Text = vEmpleado.UNIDAD_MEDICO_FAMILIAR;
            //txtLugarNacimiento.Text = vEmpleado.DS_LUGAR_NACIMIENTO;
            //txtEstadoNacimiento.Text = vEmpleado.ESTADO_NACIMIENTO;
            //txtTipoTrabajador.Text = vEmpleado.CL_TIPO_TRABAJADOR;
            //txtJornada.Text = vEmpleado.JORNADA;
            //txtUbicacion.Text = vEmpleado.UBICACION;
            //txtTipoSalario.Text = vEmpleado.TIPO_SALARIO != null ? vEmpleado.TIPO_SALARIO.ToString() : "";
            //txtContactoaccidente.Text = vEmpleado.CONTACTO_CASO_ACCIDENTES;
        }

        protected void CargarCombo()
        {
            CamposNominaNegocio nNomina = new CamposNominaNegocio();
            cmbRazonSocial.DataSource = nNomina.ObtenerRazonSocialNomina();
            cmbRazonSocial.DataValueField = "ID_RAZON_SOCIAL";
            cmbRazonSocial.DataTextField = "CL_RAZON_SOCIAL";
            cmbRazonSocial.DataBind();
        }

        protected void VerificarLicencias()
        {
            if (ContextoApp.ANOM.LicenciaAccesoModulo.MsgActivo != "1")
            {
                dvPuestoNoDo.Visible = false;
                dvRazonClave.Visible = false;
                btnNOFalse.Checked = true;
                btnNOTrue.Checked = false;
                btnNODOTrue.Checked = false;
                btnNODOFalse.Checked = true;
            }
            else if (ContextoApp.IDP.LicenciaIntegracion.MsgActivo != "1" && ContextoApp.FYD.LicenciaFormacion.MsgActivo != "1" && ContextoApp.EO.LicenciaCL.MsgActivo != "1" && ContextoApp.EO.LicenciaED.MsgActivo != "1"
                 && ContextoApp.EO.LicenciaRDP.MsgActivo != "1" && ContextoApp.MPC.LicenciaMetodologia.MsgActivo != "1" && ContextoApp.RP.LicenciaReportes.MsgActivo != "1" && ContextoApp.CI.LicenciaConsultasInteligentes.MsgActivo != "1"
                   && ContextoApp.PDE.LicenciaPuntoEncuentro.MsgActivo != "1")
            {
                dvPuestoNoDo.Visible = false;
                dvPuestoDO.Visible = false;
                dvSueldosDo.Visible = false;
                btnDOFalse.Checked = true;
                btnDOTrue.Checked = false;
                btnNODOTrue.Checked = false;
                btnNODOFalse.Checked = true;
                btnCapturarAlcance.Enabled = false;
                ClientScript.RegisterStartupScript(GetType(), "script", "MostrarDiv();", true);
            }

        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int vIdEmpleadoRq = 0;
                if (int.TryParse(Request.Params["pIdEmpleado"], out vIdEmpleadoRq))
                    vIdEmpleado = vIdEmpleadoRq;

                CargarCombo();

                if (vIdEmpleado != null)
                {
                    CargarDatos();
                }
                else
                {
                    btnDOTrue.Checked = true;
                    btnNOTrue.Checked = true;
                    btnNODOTrue.Checked = true;
                    btnTabuladorTrue.Checked = true;
                    btnInventarioTrue.Checked = true;
                    btnBonoTrue.Checked = true;
                }

                VerificarLicencias();
            }

            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO.ToString();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidaControles())
            {
               // string vXmlValores = GenerarLista();

                SPE_OBTIENE_EMPLEADOS_NOMINA_DO_Result vEmpleadoNominaDo = new SPE_OBTIENE_EMPLEADOS_NOMINA_DO_Result();

                vEmpleadoNominaDo.M_EMPLEADO_CL_EMPLEADO = txtClEmpleado.Text;
                vEmpleadoNominaDo.NB_EMPLEADO = txtNombre.Text;
                vEmpleadoNominaDo.NB_APELLIDO_PATERNO = txtPaterno.Text;
                vEmpleadoNominaDo.NB_APELLIDO_MATERNO = txtMaterno.Text;
                vEmpleadoNominaDo.FG_DO = btnDOTrue.Checked ? true : false;
                vEmpleadoNominaDo.FG_NOMINA = btnNOTrue.Checked ? true : false;
                vEmpleadoNominaDo.FG_NOMINA_DO = btnNODOTrue.Checked ? true : false;

                if (!btnDOTrue.Checked)
                {
                    vEmpleadoNominaDo.FG_NOMINA_DO = false;
                }
               // int? vIdPlaza = null;

                if (btnDOTrue.Checked && lstPuesto.SelectedValue == "" )
                {
                        UtilMensajes.MensajeResultadoDB(rwMensaje, "El campo puesto DO es requerido si el empleado esta disponible en DO.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, pCallBackFunction: "");
                        return;
                }

                if(lstPuesto.SelectedValue != "")
                vEmpleadoNominaDo.ID_PLAZA = int.Parse(lstPuesto.SelectedValue);

               // int? vIdPuestoNomina = null;
                if(lstPuestoNomina.SelectedValue != "")
                vEmpleadoNominaDo.ID_PUESTO_NOMINA = int.Parse(lstPuestoNomina.SelectedValue);

                vEmpleadoNominaDo.CL_EMPLEADO_NOMINA = txtClEmpleadoNomina.Text;

               // System.Guid? vClRazonSocial = null;
                if (cmbRazonSocial.SelectedValue != "")
                vEmpleadoNominaDo.ID_RAZON_SOCIAL = System.Guid.Parse(cmbRazonSocial.SelectedValue);

                vEmpleadoNominaDo.SUELDO_MENSUAL = txtMnSueldoMensual.Text == "" ? 0 : decimal.Parse(txtMnSueldoMensual.Text);
                vEmpleadoNominaDo.SUELDO_DIARIO = txtMnSueldoDiario.Text == "" ? 0 : decimal.Parse(txtMnSueldoDiario.Text);
                vEmpleadoNominaDo.BASE_COTIZACION = txtMnSueldoBase.Text == "" ? 0 : decimal.Parse(txtMnSueldoBase.Text);
                vEmpleadoNominaDo.SUELDO_DO = txtSueldo.Text == "" ? 0 : decimal.Parse(txtSueldo.Text);
                vEmpleadoNominaDo.FG_SUELDO_VISIBLE_INVENTARIO = btnInventarioTrue.Checked ? true : false;
                vEmpleadoNominaDo.FG_SUELDO_VISIBLE_TABULADOR = btnTabuladorTrue.Checked ? true : false;
                vEmpleadoNominaDo.FG_SUELDO_VISIBLE_BONO = btnBonoTrue.Checked ? true : false;

                string vClTipoTransaccion = vIdEmpleado != null ? "A" : "I";


                if (btnNOFalse.Checked && btnDOFalse.Checked)
                {
                    UtilMensajes.MensajeResultadoDB(rwMensaje, "El empleado debe de estar disponible en nómina o/y DO.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                    return;
                }

                if (vClTipoTransaccion == "I")
                {
                    LicenciaNegocio oNegocio = new LicenciaNegocio();
                    var vEmpleados = oNegocio.ObtenerLicenciaVolumen(pFG_ACTIVO: true).FirstOrDefault();
                    if (vEmpleados != null)
                    {
                        if (vEmpleados.NO_TOTAL_ALTA >= ContextoApp.InfoEmpresa.Volumen)
                        {
                            UtilMensajes.MensajeResultadoDB(rwMensaje, "Se ha alcanzado el máximo número de empleados para la licencia y no es posible agregar más.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                            return;
                        }
                    }
                }
 

                CamposNominaNegocio cNegocio = new CamposNominaNegocio();
                E_RESULTADO vResultado = cNegocio.InsertaActualizaEmpleado(pIdEmpleado: vIdEmpleado, pEmpleado: vEmpleadoNominaDo, pClUsuario: vClUsuario, pNbPrograma: vNbPrograma, pClTipoTransaccion: vClTipoTransaccion);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "onCloseWindows");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwMensaje, "Los campos No. de empleado, nombre y apellido paterno son requeridos.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, pCallBackFunction: "");
            }
        }

        protected void btnCapturarAlcance_Click(object sender, EventArgs e)
        {
            if (vClEstadoEmpleado != null && vClEstadoEmpleado != "ALTA")
            {
                ClientScript.RegisterStartupScript(GetType(), "script", "AbrirInventario(" + vIdEmpleado + ");", true);
            }
            else
            {
                if (ValidaControles())
                {

                    SPE_OBTIENE_EMPLEADOS_NOMINA_DO_Result vEmpleadoNominaDo = new SPE_OBTIENE_EMPLEADOS_NOMINA_DO_Result();

                    vEmpleadoNominaDo.M_EMPLEADO_CL_EMPLEADO = txtClEmpleado.Text;
                    vEmpleadoNominaDo.NB_EMPLEADO = txtNombre.Text;
                    vEmpleadoNominaDo.NB_APELLIDO_PATERNO = txtPaterno.Text;
                    vEmpleadoNominaDo.NB_APELLIDO_MATERNO = txtMaterno.Text;
                    vEmpleadoNominaDo.FG_DO = btnDOTrue.Checked ? true : false;
                    vEmpleadoNominaDo.FG_NOMINA = btnNOTrue.Checked ? true : false;
                    vEmpleadoNominaDo.FG_NOMINA_DO = btnNODOTrue.Checked ? true : false;
                    if (btnDOTrue.Checked && lstPuesto.SelectedValue == "")
                    {
                        UtilMensajes.MensajeResultadoDB(rwMensaje, "El campo puesto DO es requerido si el empleado esta disponible en DO.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, pCallBackFunction: "");
                        return;
                    }

                    if (lstPuesto.SelectedValue != "")
                        vEmpleadoNominaDo.ID_PLAZA = int.Parse(lstPuesto.SelectedValue);

                    if (lstPuestoNomina.SelectedValue != "")
                        vEmpleadoNominaDo.ID_PUESTO_NOMINA = int.Parse(lstPuestoNomina.SelectedValue);

                    vEmpleadoNominaDo.CL_EMPLEADO_NOMINA = txtClEmpleadoNomina.Text;

                    if (cmbRazonSocial.SelectedValue != "")
                        vEmpleadoNominaDo.ID_RAZON_SOCIAL = System.Guid.Parse(cmbRazonSocial.SelectedValue);

                    vEmpleadoNominaDo.SUELDO_MENSUAL = txtMnSueldoMensual.Text == "" ? 0 : decimal.Parse(txtMnSueldoMensual.Text);
                    vEmpleadoNominaDo.SUELDO_DIARIO = txtMnSueldoDiario.Text == "" ? 0 : decimal.Parse(txtMnSueldoDiario.Text);
                    vEmpleadoNominaDo.BASE_COTIZACION = txtMnSueldoBase.Text == "" ? 0 : decimal.Parse(txtMnSueldoBase.Text);
                    vEmpleadoNominaDo.SUELDO_DO = txtSueldo.Text == "" ? 0 : decimal.Parse(txtSueldo.Text);
                    vEmpleadoNominaDo.FG_SUELDO_VISIBLE_INVENTARIO = btnInventarioTrue.Checked ? true : false;
                    vEmpleadoNominaDo.FG_SUELDO_VISIBLE_TABULADOR = btnTabuladorTrue.Checked ? true : false;
                    vEmpleadoNominaDo.FG_SUELDO_VISIBLE_BONO = btnBonoTrue.Checked ? true : false;

                    string vClTipoTransaccion = vIdEmpleado != null ? "A" : "I";

                    if (vClTipoTransaccion == "I")
                    {
                        LicenciaNegocio oNegocio = new LicenciaNegocio();
                        var vEmpleados = oNegocio.ObtenerLicenciaVolumen(pFG_ACTIVO: true).FirstOrDefault();
                        if (vEmpleados != null)
                        {
                            if (vEmpleados.NO_TOTAL_ALTA >= ContextoApp.InfoEmpresa.Volumen)
                            {
                                UtilMensajes.MensajeResultadoDB(rwMensaje, "Se ha alcanzado el máximo número de empleados para la licencia y no es posible agregar más.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                                return;
                            }
                        }
                    }

                    CamposNominaNegocio cNegocio = new CamposNominaNegocio();
                    E_RESULTADO vResultado = cNegocio.InsertaActualizaEmpleado(pIdEmpleado: vIdEmpleado, pEmpleado: vEmpleadoNominaDo, pClUsuario: vClUsuario, pNbPrograma: vNbPrograma, pClTipoTransaccion: vClTipoTransaccion);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                    {
                        var idEmpleado = 0;
                        var esNumero = int.TryParse(vResultado.MENSAJE.Where(x => x.CL_IDIOMA == "ID_EMPLEADO").FirstOrDefault().DS_MENSAJE, out idEmpleado);
                        vIdEmpleado = idEmpleado;

                        ClientScript.RegisterStartupScript(GetType(), "script", "AbrirInventario(" + vIdEmpleado + ");", true);
                    }
                    else
                    {
                        UtilMensajes.MensajeResultadoDB(rwMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "onCloseWindows");
                    }

                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwMensaje, "Los campos No. de empleado, nombre y apellido paterno son requeridos.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, pCallBackFunction: "");
                }
            }
        }

    }
}