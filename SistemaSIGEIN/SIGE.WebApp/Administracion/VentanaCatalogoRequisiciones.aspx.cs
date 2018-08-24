using WebApp.Comunes;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.IntegracionDePersonal;
using SIGE.WebApp.Comunes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using SIGE.Negocio.Utilerias;
using System.Net;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaCatalogoRequisiciones : System.Web.UI.Page
    {
        private string vClUsuario = "admin";
        private string vNbPrograma = "VentanaCatalogoRequisiciones.aspx";
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private string vClEstadoMail
        {
            get { return (string)ViewState["vsClEstadoMail"]; }
            set { ViewState["vsClEstadoMail"] = value; }
        }


        private int pID_REQUISICION
        {
            get { return (int)ViewState["vsID_DEPARTAMENTO"]; }
            set { ViewState["vsID_DEPARTAMENTO"] = value; }
        }
        private int pID_PUESTO
        {
            get { return (int)ViewState["vsID_PUESTO"]; }
            set { ViewState["vsID_PUESTO"] = value; }
        }

        private string ptipo
        {
            get { return (string)ViewState["vstipo"]; }
            set { ViewState["vstipo"] = value; }
        }


        private SPE_OBTIENE_SUELDO_PROMEDIO_PUESTO_Result Vsueldo
        {
            get { return (SPE_OBTIENE_SUELDO_PROMEDIO_PUESTO_Result)ViewState["vsVsueldo"]; }
            set { ViewState["vsVsueldo"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DepartamentoNegocio negocio = new DepartamentoNegocio();
            RequisicionNegocio nRequisicion = new RequisicionNegocio();
            if (!IsPostBack)
            {
                CatalogoListaNegocio nlista = new CatalogoListaNegocio();
                var vCatalogoVacantes = nlista.Obtener_C_CATALOGO_LISTA(ID_CATALOGO_LISTA:ContextoApp.IdCatalogoCausaVacantes).FirstOrDefault();

                if (vCatalogoVacantes != null)
                {
                    CatalogoValorNegocio nvalor = new CatalogoValorNegocio();
                    var vCausas = nvalor.Obtener_C_CATALOGO_VALOR(ID_CATALOGO_LISTA: vCatalogoVacantes.ID_CATALOGO_LISTA);
                    
                    if (vCausas != null)
                    {
                        cmbCausas.DataSource = vCausas;
                        cmbCausas.DataTextField = "NB_CATALOGO_VALOR";
                        cmbCausas.DataValueField = "CL_CATALOGO_VALOR";
                        cmbCausas.DataBind();
                    }
                }
               
             
                EmpleadoNegocio nEmpleado = new EmpleadoNegocio();
                var vEmpleados = nEmpleado.Obtener_M_EMPLEADO();
                if (vEmpleados != null)
                {
                    cmbAutoriza.DataSource = vEmpleados;
                    cmbAutoriza.DataTextField = "NB_EMPLEADO_COMPLETO";
                    cmbAutoriza.DataValueField = "ID_EMPLEADO";
                    cmbAutoriza.DataBind();
                    txtSolicitado.Text = vClUsuario.ToString();
                    txtSolicitado.ReadOnly = true;
                }

                PuestoNegocio nPuestos = new PuestoNegocio();
                var Vpuestos = nPuestos.Obtener_M_PUESTO();
                if (Vpuestos != null)
                {
                    cmbPuestos.DataSource = Vpuestos;//LLENAMOS DE DATOS EL GRID
                    cmbPuestos.DataTextField = "CL_PUESTO";
                    cmbPuestos.DataTextField = "NB_PUESTO";
                    cmbPuestos.DataValueField = "ID_PUESTO";
                    cmbPuestos.DataBind();
                }

                if (Request.Params["ID"] != null)
                {
                    pID_REQUISICION = int.Parse(Request.Params["ID"]);
                    var vRequisicion = nRequisicion.Obtener_K_REQUISICION(ID_REQUISICION: pID_REQUISICION).FirstOrDefault();

                        txtNo_requisicion.Text = vRequisicion.NO_REQUISICION;
                        Fe_solicitud.SelectedDate = vRequisicion.FE_SOLICITUD;
                        Fe_Requerimiento.SelectedDate = vRequisicion.FE_REQUERIMIENTO;
                        cmbPuestos.SelectedValue = vRequisicion.ID_PUESTO + "";
                        // VRequisicionAgregar.CL_ESTADO = "VIGENTE";
                      
                        cmbCausas.SelectedValue = vRequisicion.CL_CAUSA;
                        txtEspecifique.Text = vRequisicion.DS_CAUSA;
                        PuestoNegocio npuesto = new PuestoNegocio();
                        var vpuesto = npuesto.Obtener_M_PUESTO(ID_PUESTO: vRequisicion.ID_PUESTO).FirstOrDefault();
                        txtArea.Text = vpuesto.NB_DEPARTAMENTO;
                        cmbPuestos.SelectedValue = vpuesto.ID_PUESTO.ToString();
                        txtClPuesto.Text = vpuesto.CL_PUESTO.ToString();
                        
                        SPE_OBTIENE_SUELDO_PROMEDIO_PUESTO_Result Vsueldo = nRequisicion.Obtener_Sueldo_Promedio_Puesto(ID_PUESTO: int.Parse(cmbPuestos.SelectedValue));
                        txtSueldo.Text = Vsueldo.MN_SUELDO_PROMEDIO+"";
                        Fe_Requerimiento.SelectedDate=vRequisicion.FE_REQUERIMIENTO;
                        Fe_solicitud.SelectedDate = vRequisicion.FE_SOLICITUD;
                        cmbAutoriza.SelectedValue = vRequisicion.ID_AUTORIZA.ToString();
                        txtVistoBueno.Text = "";
                    
                }
            
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
        }


        protected void btnNotificar_Click(object sender, EventArgs e)
        {
            Mail mail = new Mail(ContextoApp.mailConfiguration);


            mail.addToAddress("david.rodriguez@acrux.mx", cmbAutoriza.SelectedItem.Text);
            vClEstadoMail = "SendMail";

            try
            {
                switch (vClEstadoMail)
                {
                    case "SendMail":
                        mail.Send("Notificación al área de RR HH", String.Format(" <html>" +
                                " <head>" +
                                " <title>Notificación</title>" +
                                " <meta charset=\"utf-8\"> " +
                                " </head>" +
                                " <body>" +

                                "<table border=\" 0 solid #fff;\">" +
                                " <thead >" +
                                " <th >Notificación de autorización de requisición </th>" +
                                " </thead>" +
                                " <tr>" +
                                " <td colspan=\"3\"> Estimado (a): <strong>{0}</strong> </td>                                                          \n" +
                                " </tr>" +
                                " <tr>" +
                                " <td colspan=\"3\">" +
                                " <p>" +
                                " Por medio del presente te informamos que <strong>{1}</strong> creó una requisición de personal<br>" +
                                " con la clave <strong>{2}</strong> para cubrir el puesto <strong>{3}</strong> por <strong>{4}</strong>; para autorizar esta <br>" +
                                " vacante y que el área de RR HH inicie el proceso de reclutamiento y selección, por favor da click<br>" +
                                " en el siguiente enlace: " +
                                " </p>" +
                                " </td>" +
                                " </tr>" +
                                " <tr>" +
                                " <td colspan=\"3\"> <a href=\"http://localhost:7192/Administracion/CatalogoRequisiciones.aspx\">Requisiciones</a> </td>" +
                                " </tr>" +
                                " <tr>" +
                                " <td></td>" +
                                " <td></td>" +
                                " <td> <p>¡Gracias!<br>Departamento de RR HH   </p></td>" +
                                " </tr>" +
                                " </table>" +
                                " </body>" +
                                " </html>",
                                 cmbAutoriza.SelectedItem.Text,
                                 txtSolicitado.Text,
                                 txtNo_requisicion.Text,
                                 cmbPuestos.SelectedItem.Text,
                                 cmbCausas.SelectedItem.Text));
                        break;
                    case "OTHER":
                       break;
                }
            
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Se mando el correo exitosamente", E_TIPO_RESPUESTA_DB.SUCCESSFUL);
            }
            catch (Exception)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Error al procesar el Email", E_TIPO_RESPUESTA_DB.ERROR);
            }
        }
        


        protected void btnSave_click(object sender, EventArgs e)
        {
            E_REQUISICION VRequisicionAgregar = new E_REQUISICION();
            RequisicionNegocio nrequisicion = new RequisicionNegocio();

            ptipo = E_TIPO_OPERACION_DB.I.ToString();
            VRequisicionAgregar.NO_REQUISICION = txtNo_requisicion.Text;
            VRequisicionAgregar.FE_SOLICITUD = Fe_solicitud.SelectedDate;
            VRequisicionAgregar.FE_REQUERIMIENTO = Fe_Requerimiento.SelectedDate;
            VRequisicionAgregar.ID_PUESTO = int.Parse(cmbPuestos.SelectedValue);
            VRequisicionAgregar.CL_ESTADO = "VIGENTE";
            VRequisicionAgregar.CL_CAUSA = cmbCausas.SelectedValue;
            VRequisicionAgregar.DS_CAUSA = txtEspecifique.Text;
            DepartamentoNegocio ndepartamento = new DepartamentoNegocio();
            var vdepartamento = ndepartamento.Obtener_M_DEPARTAMENTO(NB_DEPARTAMENTO: txtArea.Text).FirstOrDefault();

            /*Se encarga de dar el ID_AUTORIZA y el ID_SOLICITANTE*/
            EmpleadoNegocio nempleado = new EmpleadoNegocio();
            var vEmpleado = nempleado.Obtener_M_EMPLEADO(int.Parse(cmbAutoriza.SelectedValue)).FirstOrDefault();
            VRequisicionAgregar.ID_AUTORIZA = vEmpleado.ID_EMPLEADO;
            VRequisicionAgregar.ID_EMPRESA = vEmpleado.ID_EMPRESA;
           // VRequisicionAgregar.ID_VISTO_BUENO = vEmpleado.ID_EMPLEADO;
            UsuarioNegocio nUsuario = new UsuarioNegocio();
            var vusuarioSolicita =nUsuario.Obtener_C_USUARIO(CL_USUARIO:vClUsuario.ToString()).FirstOrDefault();
            if(vusuarioSolicita != null)
            {
            VRequisicionAgregar.ID_SOLICITANTE = vusuarioSolicita.ID_EMPLEADO;
            }

            if (Request.Params["ID"] != null)
            {
                pID_REQUISICION = int.Parse(Request.Params["ID"]);
                ptipo = E_TIPO_OPERACION_DB.A.ToString();
                VRequisicionAgregar.ID_REQUISICION = pID_REQUISICION;
            }
            else 
            {
                VRequisicionAgregar.ID_REQUISICION = 0;
                VRequisicionAgregar.CL_ESTADO = "VIGENTE";
            }

            if(VRequisicionAgregar != null)
            {
                     E_RESULTADO vResultado = nrequisicion.InsertaActualiza_K_REQUISICION(tipo_transaccion: ptipo, programa: vNbPrograma, usuario: vClUsuario, V_K_REQUISICION: VRequisicionAgregar);
                     string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                     UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
            }  
        }

        //OBTENER LA SELECCION DEL COMBO
        protected void cmbPuestos_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RequisicionNegocio nrequisicion = new RequisicionNegocio();
            PuestoNegocio nPuestos = new PuestoNegocio();
            pID_PUESTO = int.Parse(e.Value);
            var vpuesto = nPuestos.Obtener_M_PUESTO(ID_PUESTO: pID_PUESTO).FirstOrDefault();
            txtClPuesto.Text =vpuesto.CL_PUESTO ;
            txtArea.Text = vpuesto.NB_DEPARTAMENTO;
            SPE_OBTIENE_SUELDO_PROMEDIO_PUESTO_Result Vsueldo = nrequisicion.Obtener_Sueldo_Promedio_Puesto(ID_PUESTO: int.Parse(cmbPuestos.SelectedValue));
            txtSueldo.Text = Vsueldo.MN_SUELDO_PROMEDIO+"";
        }
        protected void cmbCausas_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
          string pvalue = e.Value;
          if (pvalue.Equals("OTRA"))
          {
              txtEspecifique.ReadOnly = false;
          }
          else 
          {
              txtEspecifique.ReadOnly = true;
              txtEspecifique.Text="";
          }            
        }
    }
}