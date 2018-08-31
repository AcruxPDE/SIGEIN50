using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.TableroControl;
using SIGE.Negocio.TableroControl;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WebApp.Comunes;

namespace SIGE.WebApp.TC
{
    public partial class VentanaNuevoTableroControl : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vNbFirstRadEditorTagName = "p";

        private int? vIdPeriodo
        {
            get { return (int?)ViewState["vs_vIdPeriodo"]; }
            set { ViewState["vs_vIdPeriodo"] = value; }
        }

        private int? vIdEmpleado
        {
            get { return (int?)ViewState["vs_vIdEmpleado"]; }
            set { ViewState["vs_vIdEmpleado"] = value; }
        }

        private int? vIdPuesto
        {
            get { return (int?)ViewState["vs_vIdPuesto"]; }
            set { ViewState["vs_vIdPuesto"] = value; }
        }

        #endregion

        #region Funciones

        private void Guardar()
        {
            TableroControlNegocio nTableroControl = new TableroControlNegocio();

            if (string.IsNullOrWhiteSpace(txtNbPeriodo.Text))
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Indica el nombre del tablero", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDsPeriodo.Text))
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Indica la descripción del tablero", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
                return;
            }

            E_TABLERO_CONTROL vTableroControl = new E_TABLERO_CONTROL();
            XElement nodoPrincipal = Utileria.GuardarNotas(txtDsNotas.Content, "XML_NOTAS");
            vTableroControl.CL_PERIODO = txtNbPeriodo.Text;
            vTableroControl.DS_NOTAS = nodoPrincipal.ToString();
            vTableroControl.DS_PERIODO = txtDsPeriodo.Text;
            vTableroControl.FG_EVALUACION_CLIMA = btnClimaTrue.Checked;
            vTableroControl.FG_EVALUACION_DESEMPENO = btnDesempenoTrue.Checked;
            vTableroControl.FG_EVALUACION_FYD = btnEvaluacionCompetenciasTrue.Checked;
            vTableroControl.FG_EVALUACION_IDP = btnResultadoPruebasTrue.Checked;
            vTableroControl.FG_SITUACION_SALARIAL = btnSalarialTrue.Checked;
            vTableroControl.NB_PERIODO = txtNbPeriodo.Text;

            if (vIdPeriodo != null)
            {
                vTableroControl.ID_PERIODO = vIdPeriodo;
                var vResultado = nTableroControl.InsertarTableroControl(vTableroControl, vClUsuario, vNbPrograma);
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vResultado.MENSAJE[0].DS_MENSAJE.ToString(), vResultado.CL_TIPO_ERROR, 400, 150, "closeWindow");
            }
            else if (vIdEmpleado != null)
            {
                var vResultado = nTableroControl.InsertarTableroControl(vTableroControl, vClUsuario, vNbPrograma);
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vResultado.MENSAJE[0].DS_MENSAJE.ToString(), vResultado.CL_TIPO_ERROR, 400, 150, "");
                //resultado obtener el idCreado
                var idCreado = 0;
                var esNumero = int.TryParse(vResultado.MENSAJE.Where(x => x.CL_IDIOMA == "ID_PERIODO").FirstOrDefault().DS_MENSAJE, out idCreado);
                GuardarConfiguracion(idCreado);

            }
            else
            {
                var vResultado = nTableroControl.InsertarTableroControl(vTableroControl, vClUsuario, vNbPrograma);
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vResultado.MENSAJE[0].DS_MENSAJE.ToString(), vResultado.CL_TIPO_ERROR, 400, 150, "OncloseWindow");
            }
        }

        private void CargarDatos(int? pIdPeriodo)
        {
            TableroControlNegocio nTableroControl = new TableroControlNegocio();
            var vLstPeriodo = nTableroControl.ObtenerPeriodoTableroControl(pIdPeriodo: vIdPeriodo).FirstOrDefault();
            txtNbPeriodo.Text = vLstPeriodo.NB_PERIODO;
            txtDsPeriodo.Text = vLstPeriodo.DS_PERIODO;
            if (vLstPeriodo.DS_NOTAS != null)
            {
                if (vLstPeriodo.DS_NOTAS.Contains("DS_NOTA"))
                {
                    txtDsNotas.Content = Utileria.MostrarNotas(vLstPeriodo.DS_NOTAS);
                }
                else
                {
                    XElement vRequerimientos = XElement.Parse(vLstPeriodo.DS_NOTAS);
                    if (vRequerimientos != null)
                    {
                        vRequerimientos.Name = vNbFirstRadEditorTagName;
                        txtDsNotas.Content = vRequerimientos.ToString();
                    }
                }
            }
            btnResultadoPruebasTrue.Checked = vLstPeriodo.FG_EVALUACION_IDP == true;
            btnResultadoPruebasFalse.Checked = !vLstPeriodo.FG_EVALUACION_IDP == false;
            btnEvaluacionCompetenciasTrue.Checked = vLstPeriodo.FG_EVALUACION_FYD == true;
            btnEvaluacionCompetenciasFalse.Checked = !vLstPeriodo.FG_EVALUACION_FYD == false;
            btnDesempenoTrue.Checked = vLstPeriodo.FG_EVALUACION_DESEMPEÑO == true;
            btnDesempenoFalse.Checked = !vLstPeriodo.FG_EVALUACION_DESEMPEÑO == false;
            btnClimaTrue.Checked = vLstPeriodo.FG_EVALUACION_CLIMA_LABORAL == true;
            btnClimaFalse.Checked = !vLstPeriodo.FG_EVALUACION_CLIMA_LABORAL == false;
            btnSalarialTrue.Checked = vLstPeriodo.FG_SITUACION_SALARIAL == true;
            btnSalarialFalse.Checked = !vLstPeriodo.FG_SITUACION_SALARIAL == false;


            btnResultadoPruebasTrue.Enabled = false;
            btnResultadoPruebasFalse.Enabled = false;
            btnEvaluacionCompetenciasTrue.Enabled = false;
            btnEvaluacionCompetenciasFalse.Enabled = false;
            btnDesempenoTrue.Enabled = false;
            btnDesempenoFalse.Enabled = false;
            btnClimaTrue.Enabled = false;
            btnClimaFalse.Enabled = false;
            btnSalarialTrue.Enabled = false;
            btnSalarialFalse.Enabled = false;
        }

        private void GuardarConfiguracion(int pIdPeriodo)
        {
            TableroControlNegocio nTablero = new TableroControlNegocio();
            List<E_PERIODOS_EVALUADOS> vLstColumnas = nTablero.ObtenerPeriodosEvaluadosTableroControl(null, vIdEmpleado).ToList();
            XElement vXmlPeriodos = new XElement("PERIODOS", vLstColumnas.Select(s => new XElement("PERIODO", new XAttribute("ID_PERIODO", s.ID_PERIODO), new XAttribute("NB_PERIODO", (s.CL_PERIODO == null? s.CL_TABULADOR : s.CL_PERIODO)), new XAttribute("CL_TIPO_PERIODO", s.CL_TIPO_PERIODO_REFERENCIA))));
            var vResultado = nTablero.InsertarTableroControlSucesion(pIdPeriodo,vIdEmpleado, vIdPuesto, vXmlPeriodos.ToString(), vClUsuario, vNbPrograma);
            UtilMensajes.MensajeResultadoDB(rwmAlertas, vResultado.MENSAJE[0].DS_MENSAJE.ToString(), vResultado.CL_TIPO_ERROR, 400, 150, "OncloseWindow");
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                bool vFgPruebas = false;
                bool vFgCompetencias = false;
                bool vFgDesempeno = false;
                bool vFgClima = false;
                bool vFgSalarial = false;
                btnResultadoPruebasTrue.Checked = vFgPruebas;
                btnResultadoPruebasFalse.Checked = !vFgPruebas;
                btnEvaluacionCompetenciasTrue.Checked = vFgCompetencias;
                btnEvaluacionCompetenciasFalse.Checked = !vFgCompetencias;
                btnDesempenoTrue.Checked = vFgDesempeno;
                btnDesempenoFalse.Checked = !vFgDesempeno;
                btnClimaTrue.Checked = vFgClima;
                btnClimaFalse.Checked = !vFgClima;
                btnSalarialTrue.Checked = vFgSalarial;
                btnSalarialFalse.Checked = !vFgSalarial;

                if (Request.Params["pIdPeriodo"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["pIdPeriodo"]);
                    CargarDatos(vIdPeriodo);
                }

                if (Request.Params["idEmpleado"] != null)
                {
                    vIdEmpleado = int.Parse(Request.Params["idEmpleado"].ToString());
                    vIdPuesto = int.Parse(Request.Params["idPuesto"].ToString());

                    TableroControlNegocio nTablero = new TableroControlNegocio();
                    var vPeriodoTablero = nTablero.ObtenerPeriodoTableroControl(null, null, vIdEmpleado, vIdPuesto).FirstOrDefault();

                    btnResultadoPruebasTrue.Checked = (bool)vPeriodoTablero.FG_EVALUACION_IDP;
                    btnResultadoPruebasFalse.Checked = !(bool)vPeriodoTablero.FG_EVALUACION_IDP;
                    btnEvaluacionCompetenciasTrue.Checked = (bool)vPeriodoTablero.FG_EVALUACION_FYD;
                    btnEvaluacionCompetenciasFalse.Checked = !(bool)vPeriodoTablero.FG_EVALUACION_FYD;
                    btnDesempenoTrue.Checked = (bool)vPeriodoTablero.FG_EVALUACION_DESEMPEÑO;
                    btnDesempenoFalse.Checked = !(bool)vPeriodoTablero.FG_EVALUACION_DESEMPEÑO;
                    btnClimaTrue.Checked = (bool)vPeriodoTablero.FG_EVALUACION_CLIMA_LABORAL;
                    btnClimaFalse.Checked = !(bool)vPeriodoTablero.FG_EVALUACION_CLIMA_LABORAL;
                    btnSalarialTrue.Checked = (bool)vPeriodoTablero.FG_SITUACION_SALARIAL;
                    btnSalarialFalse.Checked = !(bool)vPeriodoTablero.FG_SITUACION_SALARIAL;


                    btnResultadoPruebasTrue.Enabled = false; ;
                    btnResultadoPruebasFalse.Enabled = false;
                    btnEvaluacionCompetenciasTrue.Enabled = false;
                    btnEvaluacionCompetenciasFalse.Enabled = false;
                    btnDesempenoTrue.Enabled = false;
                    btnDesempenoFalse.Enabled = false;
                    btnClimaTrue.Enabled = false;
                    btnClimaFalse.Enabled = false;
                    btnSalarialTrue.Enabled = false;
                    btnSalarialFalse.Enabled = false;
                }

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }
    }
}