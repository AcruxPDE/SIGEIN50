using SIGE.Entidades;
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

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaDescriptivoFactores : System.Web.UI.Page
    {
        /*
         * Creada por Gabriel Vázquez
         * Fecha de creación: 12/10/2016
         * Descripción: Pantalla para la definición de los factores de puestos.
         * Precondición: Tener un puesto seleccionado en la pantalla anterior.
         * Tablas afectadas: ADM.C_PUESTO_FACTOR
        */

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
           private XElement SELECCIONPUESTOS { get; set; }

        private int vIdPuesto
        {
            get { return (int)ViewState["vs_vdf_id_puesto"]; }
            set { ViewState["vs_vdf_id_puesto"] = value; }
        }

        public Guid vIdConsultaGlobal
        {
            get { return (Guid)ViewState["vs_vIdConsultaGlobal"]; }
            set { ViewState["vs_vIdConsultaGlobal"] = value; }
        }

        private List<E_PUESTOS_CONSULTA> vLstPuestos
        {
            get { return (List<E_PUESTOS_CONSULTA>)ViewState["vs_vLstPuestos"]; }
            set { ViewState["vs_vLstPuestos"] = value; }
        }

        #endregion

        #region Funciones

        private void CargarDatos()
        {
            DescriptivoNegocio nPuesto = new DescriptivoNegocio();
            E_DESCRIPTIVO vPuesto = nPuesto.ObtieneDescriptivo(vIdPuesto);

            if (vPuesto != null)
            {
                txtPuesto.InnerText = vPuesto.CL_PUESTO +" - "+ vPuesto.NB_PUESTO;

                List<SPE_OBTIENE_PUESTO_FACTOR_Result> vListaFactores = new List<SPE_OBTIENE_PUESTO_FACTOR_Result>();

                vListaFactores = nPuesto.ObtieneFactoresPuestos(vIdPuesto);

                if (vListaFactores.Count > 0)
                {

                    var vPrimerFactor = vListaFactores.Where(t => t.NO_FACTOR == 1).FirstOrDefault();

                    if (vPrimerFactor != null)
                    {
                        chkHabilitarF1.Checked = vPrimerFactor.FG_ACTIVO;
                        txtNombreF1.Text = vPrimerFactor.NB_FACTOR;
                        txtPonderacionF1.Value = (double)vPrimerFactor.PR_FACTOR;
                        //rbInglesF1.Checked = vPrimerFactor.FG_ASOCIADO_INGLES;
                    }

                    var vSegundoFactor = vListaFactores.Where(t => t.NO_FACTOR == 2).FirstOrDefault();

                    if (vSegundoFactor != null)
                    {
                        chkHabilitarF2.Checked = vSegundoFactor.FG_ACTIVO;
                        txtNombreF2.Text = vSegundoFactor.NB_FACTOR;
                        txtPonderacionF2.Value = (double)vSegundoFactor.PR_FACTOR;
                        rbInglesF2.Checked = vSegundoFactor.FG_ASOCIADO_INGLES;
                    }

                    var vTercerFactor = vListaFactores.Where(t => t.NO_FACTOR == 3).FirstOrDefault();

                    if (vTercerFactor != null)
                    {
                        chkHabilitarF3.Checked = vTercerFactor.FG_ACTIVO;
                        txtNombreF3.Text = vTercerFactor.NB_FACTOR;
                        txtPonderacionF3.Value = (double)vTercerFactor.PR_FACTOR;
                        rbInglesF3.Checked = vTercerFactor.FG_ASOCIADO_INGLES;
                    }

                    var vCuartoFactor = vListaFactores.Where(t => t.NO_FACTOR == 4).FirstOrDefault();

                    if (vCuartoFactor != null)
                    {
                        chkHabilitarF4.Checked = vCuartoFactor.FG_ACTIVO;
                        txtNombreF4.Text = vCuartoFactor.NB_FACTOR;
                        txtPonderacionF4.Value = (double)vCuartoFactor.PR_FACTOR;
                        rbInglesF4.Checked = vCuartoFactor.FG_ASOCIADO_INGLES;
                    }

                    var vQuintoFactor = vListaFactores.Where(t => t.NO_FACTOR == 5).FirstOrDefault();

                    if (vQuintoFactor != null)
                    {
                        chkHabilitarF5.Checked = vQuintoFactor.FG_ACTIVO;
                        txtNombreF5.Text = vQuintoFactor.NB_FACTOR;
                        txtPonderacionF5.Value = (double)vQuintoFactor.PR_FACTOR;
                        rbInglesF5.Checked = vQuintoFactor.FG_ASOCIADO_INGLES;
                    }

                    var vSextoFactor = vListaFactores.Where(t => t.NO_FACTOR == 6).FirstOrDefault();

                    if (vSextoFactor != null)
                    {
                        chkHabilitarF6.Checked = vSextoFactor.FG_ACTIVO;
                        txtNombreF6.Text = vSextoFactor.NB_FACTOR;
                        txtPonderacionF6.Value = (double)vSextoFactor.PR_FACTOR;
                        rbInglesF6.Checked = vSextoFactor.FG_ASOCIADO_INGLES;
                    }

                    rbSinIngles.Checked = vListaFactores.Count(t => t.FG_ASOCIADO_INGLES == true) == 0;

                    decimal vPrtoTotalPonderacion = vListaFactores.Sum(t => t.PR_FACTOR);
                    if (vPrtoTotalPonderacion < 100)
                    {
                        txtPonderacionF1.Value = 16.67;
                        txtPonderacionF2.Value = 16.67;
                        txtPonderacionF3.Value = 16.67;
                        txtPonderacionF4.Value = 16.67;
                        txtPonderacionF5.Value = 16.66;
                        txtPonderacionF6.Value = 16.66;
                    }
                }
                else
                {
                    E_RESULTADO vResultado = nPuesto.InsertaPuestoFactor(vIdPuesto, vClUsuario, vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    if (vResultado.CL_TIPO_ERROR != E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                    {
                        UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                    }
                    else
                    {
                        CargarDatos();
                    }
                }
            }
        }

        private void GuardarDatos()
        {
            string vMensajeValidacion = Validar();

            if (string.IsNullOrEmpty(vMensajeValidacion))
            {

                XElement vXmlFactores = new XElement("FACTORES");

                XElement vXmlPrimerFactor = new XElement("FACTOR",
                    new XAttribute("NO_FACTOR", "1"),
                    new XAttribute("FG_ACTIVO", chkHabilitarF1.Checked.Value),
                    new XAttribute("NB_FACTOR", txtNombreF1.Text),
                    new XAttribute("PR_FACTOR", chkHabilitarF1.Checked.Value ? txtPonderacionF1.Value : 0),
                    new XAttribute("FG_ASOCIADO_INGLES", false));

                XElement vXmlSegundoFactor = new XElement("FACTOR",
                    new XAttribute("NO_FACTOR", "2"),
                    new XAttribute("FG_ACTIVO", chkHabilitarF2.Checked.Value),
                    new XAttribute("NB_FACTOR", txtNombreF2.Text),
                    new XAttribute("PR_FACTOR", chkHabilitarF2.Checked.Value ? txtPonderacionF2.Value : 0),
                    new XAttribute("FG_ASOCIADO_INGLES", rbInglesF2.Checked));

                XElement vXmlTercerFactor = new XElement("FACTOR",
                    new XAttribute("NO_FACTOR", "3"),
                    new XAttribute("FG_ACTIVO", chkHabilitarF3.Checked.Value),
                    new XAttribute("NB_FACTOR", txtNombreF3.Text),
                    new XAttribute("PR_FACTOR", chkHabilitarF3.Checked.Value ? txtPonderacionF3.Value : 0),
                    new XAttribute("FG_ASOCIADO_INGLES", rbInglesF3.Checked));

                XElement vXmlCuartoFactor = new XElement("FACTOR",
                    new XAttribute("NO_FACTOR", "4"),
                    new XAttribute("FG_ACTIVO", chkHabilitarF4.Checked.Value),
                    new XAttribute("NB_FACTOR", txtNombreF4.Text),
                    new XAttribute("PR_FACTOR", chkHabilitarF4.Checked.Value ? txtPonderacionF4.Value : 0),
                    new XAttribute("FG_ASOCIADO_INGLES", rbInglesF4.Checked));

                XElement vXmlQuintoFactor = new XElement("FACTOR",
                    new XAttribute("NO_FACTOR", "5"),
                    new XAttribute("FG_ACTIVO", chkHabilitarF5.Checked.Value),
                    new XAttribute("NB_FACTOR", txtNombreF5.Text),
                    new XAttribute("PR_FACTOR", chkHabilitarF5.Checked.Value ? txtPonderacionF5.Value : 0),
                    new XAttribute("FG_ASOCIADO_INGLES", rbInglesF5.Checked));

                XElement vXmlSextoFactor = new XElement("FACTOR",
                    new XAttribute("NO_FACTOR", "6"),
                    new XAttribute("FG_ACTIVO", chkHabilitarF6.Checked.Value),
                    new XAttribute("NB_FACTOR", txtNombreF6.Text),
                    new XAttribute("PR_FACTOR", chkHabilitarF6.Checked.Value ? txtPonderacionF6.Value : 0),
                    new XAttribute("FG_ASOCIADO_INGLES", rbInglesF6.Checked));

                vXmlFactores.Add(vXmlPrimerFactor);
                vXmlFactores.Add(vXmlSegundoFactor);
                vXmlFactores.Add(vXmlTercerFactor);
                vXmlFactores.Add(vXmlCuartoFactor);
                vXmlFactores.Add(vXmlQuintoFactor);
                vXmlFactores.Add(vXmlSextoFactor);

                var vXelements = vLstPuestos.Select(x =>
                                                   new XElement("PUESTOS",
                                                       new XAttribute("ID_PUESTO", x.ID_PUESTO)
                                                       ));

                SELECCIONPUESTOS = new XElement("SELECCION", vXelements
              );

                DescriptivoNegocio nDescriptivo = new DescriptivoNegocio();

                E_RESULTADO vResultado = nDescriptivo.ActualizarPuestoFactor(SELECCIONPUESTOS.ToString(), vXmlFactores.ToString(), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR);
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensajeValidacion, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }
        }

        private string Validar()
        {
            string vMensajeValidacion = "";
            double vPorcentajeTotal = 0;
            //Primer validacion: que todos los factores que esten activados tengan un valor.

            if (chkHabilitarF1.Checked.Value & !txtPonderacionF1.Value.HasValue)
            {
                vMensajeValidacion = "El primer factor no tiene ponderación";
                return vMensajeValidacion;
            }

            if (chkHabilitarF2.Checked.Value & !txtPonderacionF2.Value.HasValue)
            {
                vMensajeValidacion = "El segundo factor no tiene ponderación";
                return vMensajeValidacion;
            }

            if (chkHabilitarF3.Checked.Value & !txtPonderacionF3.Value.HasValue)
            {
                vMensajeValidacion = "El tercer factor no tiene ponderación";
                return vMensajeValidacion;
            }

            if (chkHabilitarF4.Checked.Value & !txtPonderacionF4.Value.HasValue)
            {
                vMensajeValidacion = "El cuarto factor no tiene ponderación";
                return vMensajeValidacion;
            }

            if (chkHabilitarF5.Checked.Value & !txtPonderacionF5.Value.HasValue)
            {
                vMensajeValidacion = "El quinto factor no tiene ponderación";
                return vMensajeValidacion;
            }

            if (chkHabilitarF6.Checked.Value & !txtPonderacionF6.Value.HasValue)
            {
                vMensajeValidacion = "El sexto factor no tiene ponderación";
                return vMensajeValidacion;
            }

            //Tercera validación: que todos los factores que esten activados tengan un valor mayor a 0

            if (chkHabilitarF1.Checked.Value)
            if (txtPonderacionF1.Value.Value == 0)
            {
                vMensajeValidacion = "El primer factor debe ser mayor a 0";
                return vMensajeValidacion;
            }

            if (chkHabilitarF2.Checked.Value)
                if (txtPonderacionF2.Value.Value == 0)
            {
                vMensajeValidacion = "El segundo factor debe ser mayor a 0";
                return vMensajeValidacion;
            }

            if (chkHabilitarF3.Checked.Value)
                if (txtPonderacionF3.Value.Value == 0)
            {
                vMensajeValidacion = "El tercer factor debe ser mayor a 0";
                return vMensajeValidacion;
            }

            if (chkHabilitarF4.Checked.Value)
                if (txtPonderacionF4.Value.Value == 0)
            {
                vMensajeValidacion = "El cuarto factor debe ser mayor a 0";
                return vMensajeValidacion;
            }

            if (chkHabilitarF5.Checked.Value)
                if (txtPonderacionF5.Value.Value == 0)
            {
                vMensajeValidacion = "El quinto factor debe ser mayor a 0";
                return vMensajeValidacion;
            }

            if (chkHabilitarF6.Checked.Value)
                if (txtPonderacionF6.Value.Value == 0)
            {
                vMensajeValidacion = "El sexto factor debe ser mayor a 0";
                return vMensajeValidacion;
            }

            //Tercer validación: Que todos los factores que esten activos sumen 100%
            if (chkHabilitarF1.Checked.Value)
                vPorcentajeTotal = vPorcentajeTotal + txtPonderacionF1.Value.Value;


            if (chkHabilitarF2.Checked.Value)
                vPorcentajeTotal = vPorcentajeTotal + txtPonderacionF2.Value.Value;


            if (chkHabilitarF3.Checked.Value)
                vPorcentajeTotal = vPorcentajeTotal + txtPonderacionF3.Value.Value;


            if (chkHabilitarF4.Checked.Value)
                vPorcentajeTotal = vPorcentajeTotal + txtPonderacionF4.Value.Value;


            if (chkHabilitarF5.Checked.Value)
                vPorcentajeTotal = vPorcentajeTotal + txtPonderacionF5.Value.Value;


            if (chkHabilitarF6.Checked.Value)
                vPorcentajeTotal = vPorcentajeTotal + txtPonderacionF6.Value.Value;


            if (vPorcentajeTotal != 100)
            {
                vMensajeValidacion = "La suma de la ponderación debe de ser 100%";
                return vMensajeValidacion;
            }


            //Cuarta validación: Que todos los factores que esten activos tengan un nombre
            if (chkHabilitarF1.Checked.Value & string.IsNullOrEmpty(txtNombreF1.Text))
            {
                vMensajeValidacion = "El primer factor no tiene nombre";
                return vMensajeValidacion;
            }

            if (chkHabilitarF2.Checked.Value & string.IsNullOrEmpty(txtNombreF2.Text))
            {
                vMensajeValidacion = "El segundo factor no tiene nombre";
                return vMensajeValidacion;
            }

            if (chkHabilitarF3.Checked.Value & string.IsNullOrEmpty(txtNombreF3.Text))
            {
                vMensajeValidacion = "El tercer factor no tiene nombre";
                return vMensajeValidacion;
            }

            if (chkHabilitarF4.Checked.Value & string.IsNullOrEmpty(txtNombreF4.Text))
            {
                vMensajeValidacion = "El cuarto factor no tiene nombre";
                return vMensajeValidacion;
            }

            if (chkHabilitarF5.Checked.Value & string.IsNullOrEmpty(txtNombreF5.Text))
            {
                vMensajeValidacion = "El quinto factor no tiene nombre";
                return vMensajeValidacion;
            }

            if (chkHabilitarF6.Checked.Value & string.IsNullOrEmpty(txtNombreF6.Text))
            {
                vMensajeValidacion = "El sexto factor no tiene nombre";
                return vMensajeValidacion;
            }


            return vMensajeValidacion;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                vLstPuestos = new List<E_PUESTOS_CONSULTA>();

                if (Request.Params["ConsultaGlobalId"].ToString() != null)
                {
                      vIdConsultaGlobal = Guid.Parse(Request.Params["ConsultaGlobalId"].ToString());
                }

                
                if (ContextoConsultaGlobal.oPuestosConfiguracion != null)
                {
                    E_PUESTOS_CONSULTA_GLOBAL vLstContextoConsulta = ContextoConsultaGlobal.oPuestosConfiguracion.Where(w => w.vIdParametroConfiguracionConsulta == vIdConsultaGlobal ).FirstOrDefault();
                    vLstPuestos = vLstContextoConsulta.vListaPuestos;
                  

                    if (vLstPuestos.Count > 1)
                    {
                        txtPuesto.Visible = false;
                        lbPuesto.Visible = false;
                        tabla.Visible = false;

                        vIdPuesto = vLstPuestos.FirstOrDefault().ID_PUESTO;
                       CargarDatos();
                    }
                    else
                    {

                        foreach (E_PUESTOS_CONSULTA item in vLstPuestos)
                        {
                            vIdPuesto = item.ID_PUESTO;
                        }
                    rtsConsultaGlobal.Tabs[0].Visible = false;
                    rtsConsultaGlobal.Tabs[1].Visible = false;
                    rmpConsultaGlobal.PageViews[0].Visible = false;
                    rmpConsultaGlobal.PageViews[1].Selected = true;
                       
                        CargarDatos();
                    }

                }
            

                //if (Request.Params["PuestoId"] != null)
                //{
                //    vIdPuesto = int.Parse(Request.Params["PuestoId"].ToString());
                    //CargarDatos();
               // }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }

        protected void grdPuestos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdPuestos.DataSource = vLstPuestos;
        }
    }
}