using SIGE.Negocio.AdministracionSitio;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.PDE
{
    public partial class VentanaMenuInformacion : System.Web.UI.Page
    {

        public string vTipoTransaccion
        {
            set { ViewState["vs_vTipoTransaccion"] = value; }
            get { return (string)ViewState["vs_vTipoTransaccion"]; }
        }
     
        public int vIdComunicado
        {
            set { ViewState["vs_vIdComunicado"] = value; }
            get { return (int)ViewState["vs_vIdComunicado"]; }
        }

       
        protected void Page_Load(object sender, EventArgs e)
        {
            rlMensajePrivado.Visible = false;
            AdscripcionesNegocio negocioa = new AdscripcionesNegocio();
            string adscripcionVisible = negocioa.SeleccionaAdscripcion().ToString();
            if (adscripcionVisible != "No")
            {
                vTipoTransaccion = "49";

            }
            else
            {
                vTipoTransaccion="50";
            }

            if (adscripcionVisible != "No")
            {
                var vIdEmpleado = Request.Params["IdEmpleadoMenuInformacion"];
                var vIdPuesto = Request.Params["IdPuestoMenuInformacion"];
              
                HttpCookie myCookie = new HttpCookie("IdEmpleado");
                myCookie.Value = vIdEmpleado.ToString();
                Response.Cookies.Add(myCookie);

                HttpCookie myCookie2 = new HttpCookie("IdPuesto");
                myCookie2.Value = vIdPuesto.ToString();
                Response.Cookies.Add(myCookie2);
            }
            else
            {
                var vIdEmpleado = int.Parse(Request.Params["IdEmpleadoMenuInformacion"]);
                var vIdPuesto = int.Parse(Request.Params["IdPuestoMenuInformacion"]);
             
            }
            vIdComunicado = int.Parse(Request.Params["IdComunicado"]);
           
            var vTipoComunicado = Request.Params["TipoComunicadoMenuInformacion"];
            var vTipoAccion = Request.Params["TipoAccionMenuInformacion"];
            var vEstatus = Request.Params["Estatus"];
            if (vTipoComunicado == "I") {
                inventario.Visible = true;
                descriptivo.Visible = false;
            }
            else if (vTipoComunicado == "D")
            {
                inventario.Visible = false;
                descriptivo.Visible = true;
            }
            else
            {
                inventario.Visible = false;
                descriptivo.Visible = false;
            }


            if (vTipoAccion == "L")
            {
                RadMenuItem item3 = new RadMenuItem();
                item3.Text = "Ver";
                item3.Value = "VerIP";
                ContextMenu1.Items.Add(item3);

                RadMenuItem item4 = new RadMenuItem();
                item4.Text = "Ver";
                item4.Value = "VerPP";
                ContextMenu2.Items.Add(item4);
            }
            else if (vTipoAccion == "E")
            {
                RadMenuItem item3 = new RadMenuItem();
                item3.Text = "Editar";
                item3.Value = "EditarIP";
                ContextMenu1.Items.Add(item3);

                RadMenuItem item4 = new RadMenuItem();
                item4.Text = "Editar";
                item4.Value = "EditarPP";
                ContextMenu2.Items.Add(item4);           

            }
            //else if (vTipoAccion == "A")
            //{
            //    RadMenuItem item3 = new RadMenuItem();
            //    item3.Text = "Ver";
            //    item3.Value = "VerIP";
            //    ContextMenu1.Items.Add(item3);
            //    RadMenuItem item4 = new RadMenuItem();
            //    item4.Text = "Editar";
            //    item4.Value = "EditarIP";
            //    ContextMenu1.Items.Add(item4);

            //    RadMenuItem item5 = new RadMenuItem();
            //    item5.Text = "Ver";
            //    item5.Value = "VerPP";
            //    ContextMenu2.Items.Add(item5);
            //    RadMenuItem item6 = new RadMenuItem();
            //    item6.Text = "Editar";
            //    item6.Value = "EditarPP";
            //    ContextMenu2.Items.Add(item6);
            //}
            if (vEstatus == "Pendiente" || vEstatus == "Autorizada")
            {
                inventario.Visible = false;
                descriptivo.Visible = false;
                rlMensajePrivado.Visible = true;
            }
         
         
   
        }
    }
}