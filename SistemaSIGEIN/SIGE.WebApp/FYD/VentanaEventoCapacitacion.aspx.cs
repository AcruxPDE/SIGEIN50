
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using SIGE.Negocio.FormacionDesarrollo;
using Telerik.Web.UI;
using SIGE.WebApp.Comunes;
using SIGE.Entidades;
using SIGE.Entidades.FormacionDesarrollo;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using SIGE.Negocio.Utilerias;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaEventoCapacitacion : System.Web.UI.Page
    {
        #region variables

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private int? vIdRol;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private E_TIPO_OPERACION_DB vClOperacion
        {
            get { return (E_TIPO_OPERACION_DB)ViewState["vs_vClOperacion"]; }
            set { ViewState["vs_vClOperacion"] = value; }
        }
   


        private List<E_EVENTO_PARTICIPANTE> ListaEmpleados
        {
            get { return (List<E_EVENTO_PARTICIPANTE>)ViewState["vs_vec_lista_empleados"]; }
            set { ViewState["vs_vec_lista_empleados"] = value; }
        }

        public string vXmlEmpleadosAgregados
        {
            get { return (string)ViewState["vs_vec_xml_empleados_agregados"]; }
            set { ViewState["vs_vec_xml_empleados_agregados"] = value; }
        }

        public int vIdEvento
        {
            get { return (int)ViewState["vs_vec_id_evento"]; }
            set { ViewState["vs_vec_id_evento"] = value; }
        }
        public int vIdCurso
        {
            get { return (int)ViewState["vs_vec_vIdCurso"]; }
            set { ViewState["vs_vec_vIdCurso"] = value; }
        }
        public int vIdPrograma
        {
            get { return (int)ViewState["vs_vIdPrograma"]; }
            set { ViewState["vs_vIdPrograma"] = value; }
        }
        public List<E_EVENTO_CALENDARIO> FechasEventos
        {
            get { return (List<E_EVENTO_CALENDARIO>)ViewState["vs_vec_fechas_evento"]; }
            set { ViewState["vs_vec_fechas_evento"] = value; }
        }

        public E_SELECTOR_CURSO oCurso
        {
            get { return (E_SELECTOR_CURSO)ViewState["vs_vec_curso"]; }
            set { ViewState["vs_vec_curso"] = value; }
        }
        public E_SELECTOR_PROGRAMA oPrograma
        {
            get { return (E_SELECTOR_PROGRAMA)ViewState["vs_vec_programa"]; }
            set { ViewState["vs_vec_programa"] = value; }
        }

        private string vXmlAdicionales
        {
            get { return (string)ViewState["vs_xml_campos_adicionales"]; }
            set { ViewState["vs_xml_campos_adicionales"] = value; }
        }


        #endregion

        #region Funciones

        private void cargarEvento()
        {
            EventoCapacitacionNegocio neg = new EventoCapacitacionNegocio();

            E_EVENTO evento = new E_EVENTO();
            evento = neg.ObtieneEventos(vIdEvento).FirstOrDefault();

            txtClave.Text = evento.CL_EVENTO;
            txtNombre.Text = evento.NB_EVENTO;

            txtEvento.InnerText = evento.CL_EVENTO;
            txtDescripcionEvento.InnerText = evento.NB_EVENTO;

            if (evento.ID_PROGRAMA.HasValue)
            {
                rbVinculado.Checked = true;
                rlbPrograma.Items.Clear();
                rlbPrograma.Items.Add(new RadListBoxItem(evento.NB_PROPGRAMA, evento.ID_PROGRAMA.ToString()));
                vIdPrograma = Convert.ToInt32(evento.ID_PROGRAMA);
            }
            else
            {
                rbNoVinculado.Checked = true;
                btnAgregarParticipantes.Enabled = true;
                btnBuscarPrograma.Enabled = false;
            }

            dtpInicial.SelectedDate = evento.FE_INICIO;
            dtpFinal.SelectedDate = evento.FE_TERMINO;

            if (evento.ID_CURSO != null)
            {
                rlbCurso.Items.Clear();
                rlbCurso.Items.Add(new RadListBoxItem(evento.NB_CURSO, evento.ID_CURSO.ToString()));
                vIdCurso =  Convert.ToInt32(evento.ID_CURSO);
            }

            if (evento.ID_INSTRUCTOR != null)
            {
                rlbInstructor.Items.Clear();
                rlbInstructor.Items.Add(new RadListBoxItem(evento.NB_INSTRUCTOR, evento.ID_INSTRUCTOR.ToString()));
            }

            cmbTipo.SelectedValue = evento.CL_TIPO_CURSO;
            cmbEstado.SelectedValue = evento.CL_ESTADO;

            if (evento.ID_EMPLEADO_EVALUADOR != null)
            {
                rlbEvaluador.Items.Clear();
                rlbEvaluador.Items.Add(new RadListBoxItem(evento.NB_EVALUADOR, evento.ID_EMPLEADO_EVALUADOR.ToString()));
            }

            dtpEvaluacion.SelectedDate = evento.FE_EVALUACION;
            txtLugarEvento.Text = evento.DS_LUGAR;
            txtRefrigerio.Text = evento.DS_REFRIGERIO;
            txtCostoDirecto.Text = evento.MN_COSTO_DIRECTO.ToString();
            txtCostoIndirecto.Text = evento.MN_COSTO_INDIRECTO.ToString();
            chkIncluirResultados.Checked = evento.FG_INCLUIR_EN_PLANTILLA;

            vXmlEmpleadosAgregados = evento.XML_PARTICIPANTES;

            FechasEventos = neg.ObtieneEventoCalendario(ID_EVENTO: vIdEvento);

            oCurso = new E_SELECTOR_CURSO();

            if (evento.ID_CURSO != null)
            oCurso.idCurso = evento.ID_CURSO.Value;

            oCurso.noDuracion = Convert.ToInt32(evento.NO_DURACION_CURSO);
            oCurso.nbCurso = evento.NB_CURSO;
            ListaEmpleados = neg.ObtieneParticipanteEvento(ID_EVENTO: vIdEvento, pID_ROL: vIdRol);

            txtHorasCurso.Text = oCurso.noDuracion.ToString();

            AsignarValoresAdicionales(evento.XML_CAMPOS_ADICIONALES);
        }

        private void eliminarEmpleado(int vIdEmpleado)
        {
            XElement xml_seleccion = XElement.Parse(vXmlEmpleadosAgregados);

            xml_seleccion.Element("FILTRO").Elements().Where(t => t.Attribute("ID_EMPLEADO").Value == vIdEmpleado.ToString()).Remove();
            vXmlEmpleadosAgregados = xml_seleccion.ToString();

            var empleado = ListaEmpleados.Where(t => t.ID_EMPLEADO == vIdEmpleado).FirstOrDefault();
            ListaEmpleados.Remove(empleado);
        }

        private void eliminarFechaCalendario(int index)
        {
            FechasEventos.RemoveAt(index);
        }

        private void agregarEmpleados(List<E_SELECTOR_EMPLEADO> ListaSelector)
        {
            EventoCapacitacionNegocio neg = new EventoCapacitacionNegocio();
            XElement xml_seleccion = XElement.Parse(vXmlEmpleadosAgregados);

            foreach (E_SELECTOR_EMPLEADO str in ListaSelector)
            {
                var elemento = xml_seleccion.Element("FILTRO").Elements().Where(t => t.Attribute("ID_EMPLEADO").Value == str.idEmpleado.Value.ToString()).FirstOrDefault();

                if (elemento == null)
                {
                    xml_seleccion.Element("FILTRO").Add(new XElement("EMP", new XAttribute("ID_EMPLEADO", str.idEmpleado)));
                }
            }
            vXmlEmpleadosAgregados = xml_seleccion.ToString();

            List<SPE_OBTIENE_EMPLEADOS_Result> Lista = neg.ObtenerEmpleados(XElement.Parse(vXmlEmpleadosAgregados));
            ListaEmpleados = (from a in Lista
                              select new E_EVENTO_PARTICIPANTE
                              {
                                  CL_PARTICIPANTE = a.M_EMPLEADO_CL_EMPLEADO,
                                  ID_EMPLEADO = a.M_EMPLEADO_ID_EMPLEADO,
                                  NB_DEPARTAMENTO = a.M_DEPARTAMENTO_NB_DEPARTAMENTO,
                                  NB_PARTICIPANTE = a.M_EMPLEADO_NB_EMPLEADO_COMPLETO,
                                  NB_PUESTO = a.M_PUESTO_NB_PUESTO,
                                  ID_EVENTO = vIdEvento,
                                  ID_EVENTO_PARTICIPANTE = 0
                              }).ToList();


        }

        private bool validarControles()
        {
            bool vAceptado = true;
            string vMensaje = "";

            if (txtClave.Text == "" & vAceptado)
            {
                vMensaje = "El campo clave es obligatorio";
                UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                vAceptado = false;
                return vAceptado;
            }

            if (txtNombre.Text == "" & vAceptado)
            {
                vMensaje = "El campo nombre es obligatorio";
                UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                vAceptado = false;
                return vAceptado;
            }

            if (rbVinculado.Checked & rlbPrograma.Items[0].Value == "" & vAceptado)
            {
                vMensaje = "El campo programa de capacitación es obligatorio por que el evento está vinculado a un programa";
                UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                vAceptado = false;
                return vAceptado;
            }

            if (rlbCurso.Items[0].Value == "" & vAceptado)
            {
                vMensaje = "El campo curso es obligatorio";
                UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                vAceptado = false;
                return vAceptado;
            }

            if (!dtpInicial.SelectedDate.HasValue & vAceptado)
            {
                vMensaje = "El campo fecha inicial es obligatorio";
                UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                vAceptado = false;
                return vAceptado;
            }

            if (!dtpFinal.SelectedDate.HasValue & vAceptado)
            {
                vMensaje = "El campo fecha final es obligatorio";
                UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                vAceptado = false;
                return vAceptado;
            }

            if (!dtpEvaluacion.SelectedDate.HasValue & vAceptado)
            {
                vMensaje = "La fecha de evaluación es obligatoria";
                UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                vAceptado = false;
                return vAceptado;
            }

            //if ((dtpEvaluacion.SelectedDate.Value > dtpFinal.SelectedDate.Value) | (dtpEvaluacion.SelectedDate.Value < dtpInicial.SelectedDate.Value))
            //{
            //    vMensaje = "La fecha de evaluación debe de estar dentro de la fecha del evento.";
            //    UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
            //    vAceptado = false;
            //    return vAceptado;
            //}

            if (rlbEvaluador.Items[0].Value == "" & vAceptado)
            {
                vMensaje = "El evaluador es obligatorio";
                UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                vAceptado = false;
                return vAceptado;
            }

            if (txtCostoDirecto.Text == "" & vAceptado)
            {
                vMensaje = "El campo costo directo es obligatorio";
                UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                vAceptado = false;
                return vAceptado;
            }

            if (txtCostoIndirecto.Text == "" & vAceptado)
            {
                vMensaje = "El campo costo indirecto es obligatorio";
                UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                vAceptado = false;
                return vAceptado;
            }

            if (rgParticipantes.MasterTableView.Items.Count == 0 & vAceptado)
            {
                vMensaje = "Debe de haber al menos un participante en el evento";
                UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                vAceptado = false;
                return vAceptado;
            }

            if (oCurso == null)
            {
                vMensaje = "El curso es obligatorio";
                UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                vAceptado = false;
                return vAceptado;
            }

            //if (FechasEventos.Sum(t => t.NO_HORAS) != oCurso.noDuracion)
            //{
            //    vMensaje = "El total de horas efectivas en el calendario deben ser " + oCurso.noDuracion.ToString() + " y ha capturado " + FechasEventos.Sum(t => t.NO_HORAS).ToString();
            //    UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
            //    vAceptado = false;
            //    return vAceptado;
            //}

            return vAceptado;
        }

        private void agregarFechas()
        {

            DateTime fe_inicial;
            DateTime fe_final;

            if (!dtpInicial.SelectedDate.HasValue | !dtpFinal.SelectedDate.HasValue)
            {
                UtilMensajes.MensajeResultadoDB(rwmEvento, "Indique la fecha inicial y final del evento", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                return;
            }

            if (dtpFinal.SelectedDate.Value < dtpInicial.SelectedDate.Value)
            {
                UtilMensajes.MensajeResultadoDB(rwmEvento, "El rango de fechas del evento no es el correcto", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                return;
            }

            if (!dpFecha.SelectedDate.HasValue)
            {
                UtilMensajes.MensajeResultadoDB(rwmEvento, "Indique el dia para el calendario", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                return;
            }

            if (dpFecha.SelectedDate.Value < dtpInicial.SelectedDate.Value | dpFecha.SelectedDate.Value > dtpFinal.SelectedDate.Value)
            {
                UtilMensajes.MensajeResultadoDB(rwmEvento, "La fecha que quiere agregar no está dentro de la fecha del evento", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                return;
            }

            if (!tpHoraInicial.SelectedDate.HasValue | !tpHorafinal.SelectedDate.HasValue)
            {
                UtilMensajes.MensajeResultadoDB(rwmEvento, "Indique la hora inicial y final del calendario del evento", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                return;
            }

            if (tpHorafinal.SelectedDate.Value < tpHoraInicial.SelectedDate.Value)
            {
                UtilMensajes.MensajeResultadoDB(rwmEvento, "El rango de horas no es el correcto", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                return;
            }

            fe_inicial = new DateTime(dpFecha.SelectedDate.Value.Year, dpFecha.SelectedDate.Value.Month, dpFecha.SelectedDate.Value.Day, tpHoraInicial.SelectedTime.Value.Hours, tpHoraInicial.SelectedTime.Value.Minutes, tpHoraInicial.SelectedTime.Value.Seconds);
            fe_final = new DateTime(dpFecha.SelectedDate.Value.Year, dpFecha.SelectedDate.Value.Month, dpFecha.SelectedDate.Value.Day, tpHorafinal.SelectedTime.Value.Hours, tpHorafinal.SelectedTime.Value.Minutes, tpHorafinal.SelectedTime.Value.Seconds);

            if (FechasEventos.Count > 0)
            {
                var item = FechasEventos.Where(t => ((fe_inicial > t.FE_INICIAL & fe_inicial < t.FE_FINAL) | (fe_final > t.FE_INICIAL & fe_final < t.FE_FINAL))).FirstOrDefault();

                if (item != null)
                {
                    UtilMensajes.MensajeResultadoDB(rwmEvento, "Ya está agregado el día y hora al calendario del evento", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                    return;
                }
            }

            if (txtHoras.Text == "")
            {
                UtilMensajes.MensajeResultadoDB(rwmEvento, "Indique las horas efectivas del día", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                return;
            }

            E_EVENTO_CALENDARIO fechas = new E_EVENTO_CALENDARIO();

            fechas.ID_EVENTO_CALENDARIO = -1;
            fechas.FE_FINAL = fe_final;
            fechas.FE_INICIAL = fe_inicial;
            fechas.NO_HORAS = byte.Parse(txtHoras.Text);


            dpFecha.SelectedDate = null;
            tpHorafinal.SelectedDate = null;
            tpHoraInicial.SelectedDate = null;
            txtHoras.Text = "";

            FechasEventos.Add(fechas);
            rgCalendario.Rebind();
        }

        private string convertCalendarioToXml()
        {
            XElement calendario = new XElement("CALENDARIO");

            foreach (E_EVENTO_CALENDARIO item in FechasEventos)
            {
                calendario.Add(
                    new XElement("FECHA",
                        new XAttribute("ID_EVENTO_CALENDARIO", item.ID_EVENTO_CALENDARIO),
                        new XAttribute("FE_INICIAL", item.FE_INICIAL),
                        new XAttribute("FE_FINAL", item.FE_FINAL),
                        new XAttribute("NO_HORAS", item.NO_HORAS)));
            }

            return calendario.ToString();

        }

        private void guardarEvento()
        {
            E_EVENTO evento = new E_EVENTO();
            EventoCapacitacionNegocio neg = new EventoCapacitacionNegocio();

            if (validarControles())
            {
                XElement vXmlCA = GeneralXmlAdicionales();

                    evento.CL_EVENTO = txtClave.Text;
                    evento.DS_EVENTO = txtNombre.Text;

                evento.CL_ESTADO = cmbEstado.SelectedValue;        
                evento.CL_TIPO_CURSO = cmbTipo.SelectedValue;
                evento.DS_LUGAR = txtLugarEvento.Text;
                evento.DS_REFRIGERIO = txtRefrigerio.Text;
                evento.XML_CAMPOS_ADICIONALES = vXmlCA.ToString();

                if (dtpEvaluacion.SelectedDate.HasValue)
                {
                    evento.FE_EVALUACION = dtpEvaluacion.SelectedDate.Value;
                }

                evento.FE_INICIO = dtpInicial.SelectedDate.Value;
                evento.FE_TERMINO = dtpFinal.SelectedDate.Value;
                evento.FG_INCLUIR_EN_PLANTILLA = chkIncluirResultados.Checked.Value;

                if (rlbCurso.Items[0].Value != "")
                {
                    evento.ID_CURSO = int.Parse(rlbCurso.Items[0].Value);
                    evento.NB_CURSO = rlbCurso.Items[0].Text;
                }

                if (rlbEvaluador.Items[0].Value != "")
                {
                    evento.ID_EMPLEADO_EVALUADOR = int.Parse(rlbEvaluador.Items[0].Value);
                }

                if (rlbInstructor.Items[0].Value != "" && rlbInstructor.Items[0].Text != "No Seleccionado")
                {
                    evento.ID_INSTRUCTOR = int.Parse(rlbInstructor.Items[0].Value);
                    evento.NB_INSTRUCTOR = rlbInstructor.Items[0].Text;
                }

                if (rbVinculado.Checked)
                {
                    evento.ID_PROGRAMA = int.Parse(rlbPrograma.Items[0].Value);
                }

                evento.MN_COSTO_DIRECTO = decimal.Parse(txtCostoDirecto.Value.ToString());
                evento.MN_COSTO_INDIRECTO = decimal.Parse(txtCostoIndirecto.Value.ToString());
                evento.NB_EVENTO = txtNombre.Text;
                evento.XML_PARTICIPANTES = vXmlEmpleadosAgregados;
                evento.XML_CALENDARIO = convertCalendarioToXml();

                if (vClOperacion == E_TIPO_OPERACION_DB.A)
                {
                    evento.ID_EVENTO = vIdEvento;
                }
                else
                {
                    evento.CL_TOKEN = quitarCararcteresNoAlfanumericos(Membership.GeneratePassword(8, 0));
                    evento.FL_EVENTO = Guid.NewGuid();
                }

                E_RESULTADO msj = neg.InsertaActualizaEvento(vClOperacion.ToString(), evento, vClUsuario, vNbPrograma);
                string vMensaje = msj.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                if(vClOperacion == E_TIPO_OPERACION_DB.A)
                    UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, msj.CL_TIPO_ERROR, pCallBackFunction: "ReturnDataToParentEdit");
                else
                    UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, msj.CL_TIPO_ERROR, pCallBackFunction: "ReturnDataToParent");               
            }
        }

        public string quitarCararcteresNoAlfanumericos(string newPassword)
        {
            String vPassword = "";
            Random rnd = new Random();
            vPassword = Regex.Replace(newPassword, @"[^a-zA-Z0-9]", m => rnd.Next(1, 10) + "");
            return vPassword;
        }

        protected void CrearFormulario(XElement pXmlPlantilla)
        {
            foreach (XElement vXmlControl in pXmlPlantilla.Elements("CAMPO"))
            {
                int vDefaultWidth = 200;
                int vDefaultLabelWidth = 150;

                HtmlGenericControl vControlHTML;
                ControlDinamico vControl = new ControlDinamico(vXmlControl, true, vDefaultWidth, vDefaultLabelWidth);

                if (vControl.CtrlControl != null)
                {
                    vControlHTML = new HtmlGenericControl("div");
                    vControlHTML.Attributes.Add("class", "ctrlBasico");

                    if (vControl.CtrlLabel != null)
                    {
                        ((HtmlGenericControl)vControl.CtrlLabel).Style.Add("display", "inline-block");
                        ((HtmlGenericControl)vControl.CtrlLabel).Style.Add("padding-right", "10px");
                        ((HtmlGenericControl)vControl.CtrlLabel).Style.Add("text-align", "right");
                        ((HtmlGenericControl)vControl.CtrlLabel).Style.Add("width", "200px");

                        vControlHTML.Controls.Add(vControl.CtrlLabel);
                    }

                    vControlHTML.Controls.Add(vControl.CtrlControl);
                    pvwCamposExtras.Controls.Add(new LiteralControl("<div style='clear:both;'></div>"));
                    pvwCamposExtras.Controls.Add(vControlHTML);
                }
            }
        }

        private void AsignarValoresAdicionales(string pXmlvalores)
        {
            if (pXmlvalores != null & pXmlvalores != "")
            {
                XElement pXmlAdicionales = XElement.Parse(vXmlAdicionales);
                XElement pXmlValoresAdicionales = XElement.Parse(pXmlvalores);

                bool vFgAsignarXML = true;
                string vMensajes = String.Empty;

                foreach (XElement vXmlControl in pXmlAdicionales.Elements("CAMPO"))
                {
                    string vClTipoControl = vXmlControl.Attribute("CL_TIPO").Value;
                    string vIdControl = vXmlControl.Attribute("ID_CAMPO").Value;
                    string vNbValor = null;
                    Control vControl = pvwCamposExtras.FindControl(vIdControl);

                    if (pXmlValoresAdicionales.Elements("CAMPO").Where(n => n.Attribute("ID_CAMPO").Value.ToString() == vIdControl).FirstOrDefault() != null)
                    {
                        vNbValor = pXmlValoresAdicionales.Elements("CAMPO").Where(n => n.Attribute("ID_CAMPO").Value.ToString() == vIdControl).FirstOrDefault().Attribute("NO_VALOR").Value.ToString();
                    }

                    if (vNbValor != null)
                    {
                        switch (vClTipoControl)
                        {
                            case "TEXTBOX":
                                ((RadTextBox)vControl).Text = vNbValor;
                                break;
                            case "MASKBOX":
                                ((RadMaskedTextBox)vControl).Text = vNbValor;
                                break;
                            case "DATEPICKER":
                                //DateTime vFecha =  ?? DateTime.Now;
                                ((RadDatePicker)vControl).SelectedDate = DateTime.Parse(vNbValor);
                                break;
                            case "COMBOBOX":
                                ((RadComboBox)vControl).SelectedValue = vNbValor;
                                break;
                            case "LISTBOX":
                                RadListBox vRadListBox = ((RadListBox)vControl);
                                vRadListBox.SelectedValue = vNbValor;
                                break;
                            case "NUMERICBOX":
                                RadNumericTextBox vRadNumeric = (RadNumericTextBox)vControl;
                                vRadNumeric.Text = vNbValor;
                                break;
                            case "CHECKBOX":
                                RadButton vRadButton = (RadButton)vControl;
                                vRadButton.Checked = vNbValor=="1"? true:false;
                                break;
                            case "DATEAGE":
                                RadDatePicker vControlF = (RadDatePicker)vControl;
                                vControlF.SelectedDate = DateTime.Parse(vNbValor);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private XElement GeneralXmlAdicionales()
        {
            XElement pXmlAdicionales = XElement.Parse(vXmlAdicionales);
            XElement pXmlValoresAdicionales = new XElement("CAMPOS");
            bool vFgAsignarXML = true;
            string vMensajes = String.Empty;

            foreach (XElement vXmlControl in pXmlAdicionales.Elements("CAMPO"))
            {
                string vClTipoControl = vXmlControl.Attribute("CL_TIPO").Value;
                string vIdControl = vXmlControl.Attribute("ID_CAMPO").Value;
                string vNbControl = vXmlControl.Attribute("NB_CAMPO").Value;
                string vNbValor = String.Empty;
                string vDsValor = "";


                bool vFgAsignarValor = true;
                Control vControl = pvwCamposExtras.FindControl(vIdControl);

                switch (vClTipoControl)
                {
                    case "TEXTBOX":
                        vNbValor = ((RadTextBox)vControl).Text;
                        //vDsValor = ((RadTextBox)vControl).Text;

                        if ((bool?)UtilXML.ValorAtributo(vXmlControl.Attribute("FG_REQUERIDO"), E_TIPO_DATO.BOOLEAN) ?? false)
                        {
                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
                            vFgAsignarXML = vFgAsignarXML && vFgAsignarValor;
                            if (!vFgAsignarValor)
                                vMensajes += String.Format("El campo {0} es requerido.<br />", vNbControl);
                        }
                        break;
                    case "MASKBOX":
                        vNbValor = ((RadMaskedTextBox)vControl).Text;
                        //vDsValor = ((RadMaskedTextBox)vControl).Text;
                        if ((bool?)UtilXML.ValorAtributo(vXmlControl.Attribute("FG_REQUERIDO"), E_TIPO_DATO.BOOLEAN) ?? false)
                        {
                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
                            vFgAsignarXML = vFgAsignarXML && vFgAsignarValor;
                            if (!vFgAsignarValor)
                                vMensajes += String.Format("El campo {0} es requerido.<br />", vNbControl);
                        }
                        break;
                    case "DATEPICKER":
                        DateTime vFecha = ((RadDatePicker)vControl).SelectedDate ?? DateTime.Now;
                        vNbValor = vFecha.ToString("dd/MM/yyyy");
                        //vDsValor = vFecha.ToString("dd/MM/yyyy");
                        if ((bool?)UtilXML.ValorAtributo(vXmlControl.Attribute("FG_REQUERIDO"), E_TIPO_DATO.BOOLEAN) ?? false)
                        {
                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
                            vFgAsignarXML = vFgAsignarXML && vFgAsignarValor;
                            if (!vFgAsignarValor)
                                vMensajes += String.Format("El campo {0} es requerido.<br />", vNbControl);
                        }
                        break;
                    case "COMBOBOX":
                        vNbValor = ((RadComboBox)vControl).SelectedValue;
                        //vDsValor = ((RadComboBox)vControl).Text;
                        if ((bool?)UtilXML.ValorAtributo(vXmlControl.Attribute("FG_REQUERIDO"), E_TIPO_DATO.BOOLEAN) ?? false)
                        {
                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
                            vFgAsignarXML = vFgAsignarXML && vFgAsignarValor;
                            if (!vFgAsignarValor)
                                vMensajes += String.Format("El campo {0} es requerido.<br />", vNbControl);
                        }
                        break;
                    case "LISTBOX":
                        RadListBox vRadListBox = ((RadListBox)vControl);
                        string vClValor = String.Empty;

                        foreach (RadListBoxItem item in vRadListBox.SelectedItems)
                        {
                            vNbValor = item.Value;
                            vDsValor = item.Text;
                        }

                        if ((bool?)UtilXML.ValorAtributo(vXmlControl.Attribute("FG_REQUERIDO"), E_TIPO_DATO.BOOLEAN) ?? false)
                        {
                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
                            vFgAsignarXML = vFgAsignarXML && vFgAsignarValor;
                            if (!vFgAsignarValor)
                                vMensajes += String.Format("El campo {0} es requerido.<br />", vNbControl);
                        }
                        break;
                    default:
                        vFgAsignarValor = false;
                        break;

                    case "NUMERICBOX":
                        vNbValor = ((RadNumericTextBox)vControl).Text;
                        vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);

                        if (vFgAsignarValor)
                            UtilXML.AsignarValorAtributo(vXmlControl, "NB_VALOR", vNbValor);
                        break;
                    case "CHECKBOX":
                        vNbValor = (((RadButton)vControl).Checked) ? "1" : "0";
                        UtilXML.AsignarValorAtributo(vXmlControl, "NB_VALOR", vNbValor);
                        break;
                    case "DATEAGE":
                        RadDatePicker vControlF = new RadDatePicker();
                        vControlF = (RadDatePicker)vControl;
                        string value = vControlF.DateInput.InvalidTextBoxValue;

                        if (value == string.Empty)
                        {
                            vFecha = ((RadDatePicker)vControl).SelectedDate ?? DateTime.Now;

                            if (vControlF.SelectedDate < vControlF.MinDate)
                            {
                                vFecha = DateTime.Now;
                            }
                            vNbValor = vFecha.ToString("dd/MM/yyyy");
                            vFgAsignarValor = !String.IsNullOrWhiteSpace(vNbValor);
                            if (vFgAsignarValor)
                                UtilXML.AsignarValorAtributo(vXmlControl, "NB_VALOR", vNbValor);
                        }


                        break;
                }
                if (vFgAsignarValor)
                {
                    XElement xXmlCampo = new XElement("CAMPO");

                    xXmlCampo.Add(new XAttribute("ID_CAMPO", vIdControl), new XAttribute("NO_VALOR", vNbValor), new XAttribute("DS_VALOR", vDsValor));
                    pXmlValoresAdicionales.Add(xXmlCampo);
                }
            }
            if (vFgAsignarXML)
                return pXmlValoresAdicionales;
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmEvento, vMensajes, E_TIPO_RESPUESTA_DB.WARNING);
                return null;
            }
        }

        private void SeguridadProcesos()
        {
            btnGuardarEvento.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.B.A.A");
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            EventoCapacitacionNegocio nCurso = new EventoCapacitacionNegocio();
            CrearFormulario(XElement.Parse(nCurso.ObtieneCampoAdicionalXml("C_EVENTO")));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;
            vIdCurso = 0;
            vIdPrograma = 0;
            if (!Page.IsPostBack)
            {
                vIdEvento = 0;
                FechasEventos = new List<E_EVENTO_CALENDARIO>();
                ListaEmpleados = new List<E_EVENTO_PARTICIPANTE>();

                XElement xml_seleccion;
                EventoCapacitacionNegocio nEvento = new EventoCapacitacionNegocio();

                vXmlAdicionales = nEvento.ObtieneCampoAdicionalXml("C_EVENTO");

                xml_seleccion = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "EMPLEADO")));
                vXmlEmpleadosAgregados = xml_seleccion.ToString();

                vClOperacion = E_TIPO_OPERACION_DB.I;

                if (Request.Params["EventoId"] != null)
                {
                    vClOperacion = E_TIPO_OPERACION_DB.A;
                    vIdEvento = int.Parse(Request.Params["EventoId"].ToString());
                    cargarEvento();
                }

                if (Request.Params["EventoIdCopia"] != null)
                {
                    dvContextoEvento.Visible = false;
                    dvDatosEvento.Visible = true;
                    vIdEvento = int.Parse(Request.Params["EventoIdCopia"].ToString());
                    cargarEvento();
                }

                SeguridadProcesos();

                if (Request.Params["clOrigen"] != null)
                {
                    if (Request.Params["clOrigen"] == "AVANCE")
                    {
                        btnGuardarEvento.Enabled = false;
                        btnAgregarFecha.Enabled = false;
                    }
                }

            } 
         
        }

        protected void rgParticipantes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            //rgParticipantes.DataSource = 
            //rgParticipantes.DataSource = neg.obtieneParticipanteEvento(ID_EVENTO: vIdEvento);
            rgParticipantes.DataSource = ListaEmpleados;
        }

        protected void ramEventos_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            E_SELECTOR vSeleccion = new E_SELECTOR();
            E_SELECTOR_PROGRAMA vSeleccionPrograma = new E_SELECTOR_PROGRAMA();
            string pParameter = e.Argument;

            if (pParameter != null)
            {
                vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

                if (vSeleccion.clTipo == "PARTICIPANTE")
                {
                    List<E_SELECTOR_EMPLEADO> listaEmpleados = new List<E_SELECTOR_EMPLEADO>();
                    listaEmpleados = JsonConvert.DeserializeObject<List<E_SELECTOR_EMPLEADO>>(vSeleccion.oSeleccion.ToString());
                    agregarEmpleados(listaEmpleados);
                    rgParticipantes.Rebind();
                }

                if (vSeleccion.clTipo == "CURSO")
                {
                    oCurso = (JsonConvert.DeserializeObject<List<E_SELECTOR_CURSO>>(vSeleccion.oSeleccion.ToString())).FirstOrDefault();
                    
                }
                if (vSeleccion.clTipo == "PROGRAMA")
                {
                    ProgramaNegocio nPrograma = new ProgramaNegocio();
                    oPrograma = (JsonConvert.DeserializeObject<List<E_SELECTOR_PROGRAMA>>(vSeleccion.oSeleccion.ToString())).FirstOrDefault();
                    List<E_SELECTOR_EMPLEADO> vProgramaParticipantes = nPrograma.ObtieneEmpleadosParticipantes(pID_PROGRAMA: oPrograma.idPrograma,pID_EMPRESA:vIdEmpresa).Select(s => new E_SELECTOR_EMPLEADO
                    {
                        clEmpleado = s.CL_EMPLEADO,
                        idEmpleado = s.ID_EMPLEADO,
                        nbEmpleado = s.NB_EMPLEADO
                    }).ToList();
                    agregarEmpleados(vProgramaParticipantes);
                    rgParticipantes.Rebind();
                }

            }

        }

        protected void rgParticipantes_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridDataItem item = e.Item as GridDataItem;
                EventoCapacitacionNegocio neg = new EventoCapacitacionNegocio();

                int vIdEmpleado = int.Parse(item.GetDataKeyValue("ID_EMPLEADO").ToString());
                int vIdParticipante = int.Parse(item.GetDataKeyValue("ID_EVENTO_PARTICIPANTE").ToString());

                eliminarEmpleado(vIdEmpleado);

                if (vClOperacion == E_TIPO_OPERACION_DB.A)
                {

                    E_RESULTADO msj = neg.EliminaEventoParticipante(vIdParticipante);

                    string vMensaje = msj.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    if (msj.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
                    {
                        UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, msj.CL_TIPO_ERROR);
                    }
                }
            }
        }

        protected void btnGuardarEvento_Click(object sender, EventArgs e)
        {
            guardarEvento();
        }

        protected void rgCalendario_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgCalendario.DataSource = FechasEventos;
        }

        protected void btnAgregarFecha_Click(object sender, EventArgs e)
        {
            agregarFechas();
        }

        protected void rgCalendario_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridDataItem item = e.Item as GridDataItem;
                EventoCapacitacionNegocio neg = new EventoCapacitacionNegocio();

                int vIdEventoCalendario = int.Parse(item.GetDataKeyValue("ID_EVENTO_CALENDARIO").ToString());

                eliminarFechaCalendario(item.ItemIndex);

                if (vClOperacion == E_TIPO_OPERACION_DB.A)
                {
                    E_RESULTADO msj = neg.EliminaEventoCalendario(vIdEventoCalendario);

                    string vMensaje = msj.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    if (msj.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
                    {
                        UtilMensajes.MensajeResultadoDB(rwmEvento, vMensaje, msj.CL_TIPO_ERROR);
                    }
                }
            }
        }

        protected void rbVinculado_Click(object sender, EventArgs e)
        {
      
            btnBuscarPrograma.Enabled = true;
            rlbCurso.Items[0].Value = "No seleccionado";
            rlbCurso.Items[0].Value = "";
            rlbInstructor.Items[0].Value = "";
            rlbCurso.Items[0].Text = "No Seleccionado";
            rlbInstructor.Items[0].Text = "No Seleccionado";
            btnAgregarParticipantes.Enabled = false;

        }

        protected void rbNoVinculado_Click(object sender, EventArgs e)
        {
           
            btnBuscarPrograma.Enabled = false;
            rlbCurso.Items[0].Value = "";
            rlbInstructor.Items[0].Value = "";
            rlbCurso.Items[0].Text = "No Seleccionado";
            rlbInstructor.Items[0].Text = "No Seleccionado";
            btnAgregarParticipantes.Enabled = true;
        }
    }
}