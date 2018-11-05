using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Administracion;
using SIGE.WebApp.IDP.Solicitud;
using SIGE.WebApp.PDE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using WebApp.Comunes;

namespace SIGE.WebApp.Comunes
{
    public class FormularioDinamico
    {
        public static Control ObtenerContenedor(string pNbContenedor, List<Contenedor> pLstContenedores)
        {
            Contenedor vContenedor = pLstContenedores.FirstOrDefault(f => f.NbContenedor.Equals(pNbContenedor));
            return (vContenedor != null) ? vContenedor.CtrlContenedor : null;
        }
    }

    public class Contenedor
    {
        public string NbContenedor { get; set; }
        public Control CtrlContenedor { get; set; }
    }

    public class Plantilla
    {
        public Contenedor ctrlPlantilla { get; set; }

        public List<Contenedor> lstContenedores { get; set; }

        public string xmlPlantilla { get; set; }

        public void CrearFormulario(bool pFgAddValue)
        {
            foreach (XElement vXmlContenedor in XElement.Parse(xmlPlantilla).Element("CONTENEDORES").Elements("CONTENEDOR"))
            {
                string vNbContenedor = UtilXML.ValorAtributo<string>(vXmlContenedor.Attribute("ID_CONTENEDOR"));
                RadPageView vPageView = (RadPageView)FormularioDinamico.ObtenerContenedor(vNbContenedor, lstContenedores);

                if (vPageView != null)
                {
                    foreach (XElement vXmlControl in vXmlContenedor.Elements("CAMPO"))
                    {
                        HtmlGenericControl vControlHTML;
                        ControlDinamico vControl = new ControlDinamico(vXmlControl, pFgAddValue, pPageView:vPageView);

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
                            vPageView.Controls.Add(vControlHTML);
                            vPageView.Controls.Add(new LiteralControl("<div style='clear:both;'></div>"));

                            switch (UtilXML.ValorAtributo<string>(vXmlControl.Attribute("CL_TIPO")))
                            {
                                case "GRID":
                                    RadGrid vGrid = (RadGrid)vControl.CtrlControl.FindControl(UtilXML.ValorAtributo<string>(vXmlControl.Attribute("ID_CAMPO")));

                                    Control vBotonAgregar = vPageView.FindControl(String.Format("btnAdd{0}", UtilXML.ValorAtributo<string>(vXmlControl.Attribute("ID_CAMPO"))));
                                    if (vBotonAgregar is RadButton)
                                        ((RadButton)vBotonAgregar).Click += new EventHandler(AgregarGridItem);

                                    Control vBotonEliminar = vPageView.FindControl(String.Format("btnDel{0}", UtilXML.ValorAtributo<string>(vXmlControl.Attribute("ID_CAMPO"))));
                                    if (vBotonEliminar is RadButton)
                                        ((RadButton)vBotonEliminar).Click += new EventHandler(EliminarGridItem);

                                    Control vBotonEditar = vPageView.FindControl(string.Format("btnEdit{0}", UtilXML.ValorAtributo<string>(vXmlControl.Attribute("ID_CAMPO"))));
                                    if (vBotonEditar is RadButton)
                                        ((RadButton)vBotonEditar).Click += new EventHandler(EditarGridItem);

                                    Control vBotonCancelar = vPageView.FindControl(string.Format("btnCancelar{0}", UtilXML.ValorAtributo<string>(vXmlControl.Attribute("ID_CAMPO"))));
                                    if (vBotonCancelar is RadButton)
                                        ((RadButton)vBotonCancelar).Click += new EventHandler(CancelarEdicionGridItem);

                                    Control vBotonAceptar = vPageView.FindControl(string.Format("btnGuardar{0}", UtilXML.ValorAtributo<string>(vXmlControl.Attribute("ID_CAMPO"))));
                                    if (vBotonAceptar is RadButton)
                                        ((RadButton)vBotonAceptar).Click += new EventHandler(AceptarEdicionGridItem);

                                    foreach (XElement vCtrlFormulario in vXmlControl.Element("FORMULARIO").Elements("CAMPO"))
                                    {
                                        switch (UtilXML.ValorAtributo<string>(vCtrlFormulario.Attribute("CL_TIPO")))
                                        {
                                            case "COMBOBOX":
                                                XElement vXmlCtrlFormularioDependientes = vCtrlFormulario.Element("DEPENDENCIAS");
                                                string g = vCtrlFormulario.Attribute("ID_CAMPO").Value;
                                                if (vXmlCtrlFormularioDependientes != null)
                                                {
                                                    List<string> vControlesDependientes = new List<string>();
                                                    foreach (XElement vXmlControlDependiente in vXmlCtrlFormularioDependientes.Elements("CAMPO_DEPENDIENTE"))
                                                        vControlesDependientes.Add(UtilXML.ValorAtributo<string>(vXmlControlDependiente.Attribute("ID_CAMPO_DEPENDIENTE")));

                                                    RadComboBox vRadComboBox = (RadComboBox)vControl.CtrlControl.FindControl(UtilXML.ValorAtributo<string>(vCtrlFormulario.Attribute("ID_CAMPO")));
                                                    vRadComboBox.AutoPostBack = true;
                                                    vRadComboBox.SelectedIndexChanged += (sender, e) => ActualizarControlDependiente(sender, e, vControlesDependientes);
                                                    vRadComboBox.PreRender += (sender, e) => ActualizarControlDependiente(sender, e, vControlesDependientes);
                                                }
                                                break;
                                        }
                                    }

                                    break;
                                case "COMBOBOX":
                                    XElement vXmlDependientes = vXmlControl.Element("DEPENDENCIAS");
                                    string f = vXmlControl.Attribute("ID_CAMPO").Value;
                                    if (vXmlDependientes != null)
                                    {
                                        List<string> vControlesDependientes = new List<string>();
                                        foreach (XElement vXmlControlDependiente in vXmlDependientes.Elements("CAMPO_DEPENDIENTE"))
                                            vControlesDependientes.Add(UtilXML.ValorAtributo<string>(vXmlControlDependiente.Attribute("ID_CAMPO_DEPENDIENTE")));

                                        ((RadComboBox)vControl.CtrlControl).AutoPostBack = true;
                                        ((RadComboBox)vControl.CtrlControl).SelectedIndexChanged += (sender, e) => ActualizarControlDependiente(sender, e, vControlesDependientes);
                                        ((RadComboBox)vControl.CtrlControl).PreRender += (sender, e) => ActualizarControlDependiente(sender, e, vControlesDependientes);
                                    }

                                    break;
                            }
                        }
                    }
                }
            }
        }

        public void ActualizarControlDependiente(object sender, EventArgs e, List<string> pIdControlesDependientes)
        {
            Control vCtrlPadre = (Control)sender;
            string vNbValor = String.Empty;
            //XElement pXmlPlantilla = XElement.Parse(vXmlSolicitudPlantilla);

            if (vCtrlPadre is RadComboBox)
                foreach (RadComboBoxItem item in ((RadComboBox)vCtrlPadre).Items.Where(w => w.Selected))
                    vNbValor = item.Value;

            if (string.IsNullOrEmpty(vNbValor))
                vNbValor = ((RadComboBox)vCtrlPadre).Items.Select(w => w.Text).FirstOrDefault().ToString();

            foreach (string vIdControlDependiente in pIdControlesDependientes)
            {
                Control vCtrlDependiente = ctrlPlantilla.CtrlContenedor.FindControl(vIdControlDependiente);

                XElement vXmlCtrlDependiente = null;
                RadPageView vPageView = null;
                foreach (XElement vXmlContenedor in XElement.Parse(xmlPlantilla).Element("CONTENEDORES").Elements("CONTENEDOR"))
                {
                    vPageView = (RadPageView)FormularioDinamico.ObtenerContenedor(UtilXML.ValorAtributo<string>(vXmlContenedor.Attribute("ID_CONTENEDOR")), lstContenedores);
                    vXmlCtrlDependiente = vXmlContenedor.Elements("CAMPO").FirstOrDefault(f => UtilXML.ValorAtributo<string>(f.Attribute("ID_CAMPO")) == vIdControlDependiente);

                    if (vXmlCtrlDependiente == null)
                    {
                        foreach (XElement vXmlGrid in vXmlContenedor.Elements("CAMPO").Where(w => UtilXML.ValorAtributo<string>(w.Attribute("CL_TIPO")) == "GRID"))
                        {
                            vXmlCtrlDependiente = vXmlGrid.Element("FORMULARIO").Elements("CAMPO").FirstOrDefault(f => UtilXML.ValorAtributo<string>(f.Attribute("ID_CAMPO")) == vIdControlDependiente);
                            if (vXmlCtrlDependiente != null)
                                break;
                        }
                    }

                    if (vXmlCtrlDependiente != null)
                    {
                        string vValorSeleccionado = null;

                        if (vPageView.Page is Solicitud)
                        {
                            E_GENERICA vSeleccion = ((Solicitud)vPageView.Page).vControlDependienteSeleccion.Where(t => t.CL_GENERICA.Equals(vIdControlDependiente)).FirstOrDefault();
                            if (vSeleccion != null)
                            {
                                vValorSeleccionado = vSeleccion.NB_GENERICO;
                            }
                        }

                        if (vPageView.Page is Empleado)
                        {
                            E_GENERICA vSeleccion = ((Empleado)vPageView.Page).vControlDependienteSeleccion.Where(t => t.CL_GENERICA.Equals(vIdControlDependiente)).FirstOrDefault();
                            if (vSeleccion != null)
                            {
                                vValorSeleccionado = vSeleccion.NB_GENERICO;
                            }
                        }

                        switch (UtilXML.ValorAtributo<string>(vXmlCtrlDependiente.Attribute("CL_TIPO")))
                        {
                            case "COMBOBOX":
                                RadComboBox vCtrlControlDependiente = (RadComboBox)vPageView.FindControl(vIdControlDependiente);
                                vCtrlControlDependiente.Items.Clear();
                                if (vXmlCtrlDependiente.Element("ITEMS") != null)
                                    foreach (XElement item in vXmlCtrlDependiente.Element("ITEMS").Elements("ITEM").Where(w => UtilXML.ValorAtributo<string>(w.Attribute("NB_VALOR_PADRE")).ToUpper() == vNbValor.ToUpper()))
                                        vCtrlControlDependiente.Items.Add(new RadComboBoxItem()
                                        {
                                            Text = UtilXML.ValorAtributo<string>(item.Attribute("NB_TEXTO")),
                                            Value = UtilXML.ValorAtributo<string>(item.Attribute("NB_VALOR")),
                                            //Selected = UtilXML.ValorAtributo<bool>(item.Attribute("FG_SELECCIONADO"))
                                            Selected = UtilXML.ValorAtributo<string>(item.Attribute("NB_VALOR")).Equals(vValorSeleccionado) ? true : false
                                        });

                                break;
                        }
                    }
                }
            }
        }

        public void EliminarGridItem(object sender, EventArgs e)
        {
            RadButton vBoton = (RadButton)sender;
            string vIdControlGrid = vBoton.ID.Replace("btnDel", String.Empty);
            XElement vXmlPlantilla = XElement.Parse(xmlPlantilla);

            XElement vXmlGrid = null;
            RadPageView vPageView = null;

            foreach (XElement vXmlContenedor in vXmlPlantilla.Element("CONTENEDORES").Elements("CONTENEDOR"))
            {
                vPageView = (RadPageView)FormularioDinamico.ObtenerContenedor(UtilXML.ValorAtributo<string>(vXmlContenedor.Attribute("ID_CONTENEDOR")), lstContenedores);
                vXmlGrid = vXmlContenedor.Elements("CAMPO").FirstOrDefault(f => UtilXML.ValorAtributo<string>(f.Attribute("ID_CAMPO")).Equals(vIdControlGrid));
                if (vXmlGrid != null)
                    break;
            }

            if (vXmlGrid != null)
            {
                RadGrid vGrid = (RadGrid)vPageView.FindControl(vIdControlGrid);
                foreach (GridDataItem i in vGrid.SelectedItems)
                {
                    string vIdItem = (string)i.GetDataKeyValue("ID_ITEM");
                    vXmlGrid.Element("GRID").Element("DATOS").Elements("ITEM").Where(w => UtilXML.ValorAtributo<string>(w.Attribute("ID_ITEM")) == vIdItem).Remove();
                }
            }

            xmlPlantilla = vXmlPlantilla.ToString();

            if (vBoton.Page is Solicitud)
                ((Solicitud)vBoton.Page).vXmlSolicitudPlantilla = xmlPlantilla;

            if (vBoton.Page is Empleado)
                ((Empleado)vBoton.Page).vXmlEmpleadoPlantilla = xmlPlantilla;

            if (vBoton.Page is VentanaInventarioPersonal)
                ((VentanaInventarioPersonal)vBoton.Page).vXmlEmpleadoPlantilla = xmlPlantilla;

            if (vBoton.Page is VentanaInventarioPersonalAdmin)
                ((VentanaInventarioPersonalAdmin)vBoton.Page).vXmlEmpleadoPlantilla = xmlPlantilla;

            //Solicitud p = (Solicitud)vBoton.Page;
            //xmlPlantilla = vXmlPlantilla.ToString();
            //p.vXmlSolicitudPlantilla = xmlPlantilla;
            CargarGrid();
        }

        public void AgregarGridItem(object sender, EventArgs e)
        {
            RadButton vBoton = (RadButton)sender;
            string vIdControlGrid = vBoton.ID.Replace("btnAdd", String.Empty);
            XElement vXmlPlantilla = XElement.Parse(xmlPlantilla);
            XElement vXmlGrid = null;
            RadPageView vPageView = null;
            

            foreach (XElement vXmlContenedor in vXmlPlantilla.Element("CONTENEDORES").Elements("CONTENEDOR"))
            {
                vPageView = (RadPageView)FormularioDinamico.ObtenerContenedor(UtilXML.ValorAtributo<string>(vXmlContenedor.Attribute("ID_CONTENEDOR")), lstContenedores);
                vXmlGrid = vXmlContenedor.Elements("CAMPO").FirstOrDefault(f => UtilXML.ValorAtributo<string>(f.Attribute("ID_CAMPO")).Equals(vIdControlGrid));
                if (vXmlGrid != null)
                    break;
            }

            bool vFgRequerido = false;
            string vNbControl = string.Empty;
            string vValorControl = string.Empty;
         

            if (vXmlGrid != null)
            {

                //Aquí vamos a poner la busqueda del elemento en la pagina, si existe algun elemento, lo borramos para que se vuelva a agregar
                if (vPageView.Page is Empleado)
                {
                    if (((Empleado)vPageView.Page).vDatosModificar.Exists(t => t.Key.Equals(vIdControlGrid)))
                    {
                        string vIdItem = ((Empleado)vPageView.Page).vDatosModificar.Where(t => t.Key.Equals(vIdControlGrid)).FirstOrDefault().Value;
                        vXmlGrid.Element("GRID").Element("DATOS").Elements("ITEM").Where(w => UtilXML.ValorAtributo<string>(w.Attribute("ID_ITEM")) == vIdItem).Remove();
                    }
                }

                if (vPageView.Page is Solicitud)
                {
                    if (((Solicitud)vPageView.Page).vDatosModificar.Exists(t => t.Key.Equals(vIdControlGrid)))
                    {
                        string vIdItem = ((Solicitud)vPageView.Page).vDatosModificar.Where(t => t.Key.Equals(vIdControlGrid)).FirstOrDefault().Value;
                        vXmlGrid.Element("GRID").Element("DATOS").Elements("ITEM").Where(w => UtilXML.ValorAtributo<string>(w.Attribute("ID_ITEM")) == vIdItem).Remove();
                    }
                }

                RadGrid vGrid = (RadGrid)vPageView.FindControl(vIdControlGrid);
                XElement vItem = new XElement("ITEM", new XAttribute("ID_ITEM", Guid.NewGuid()));
                var vMensajes = string.Empty;

                foreach (XElement vXmlControl in vXmlGrid.Element("FORMULARIO").Elements("CAMPO"))
                {
                    string vIdControl = UtilXML.ValorAtributo<string>(vXmlControl.Attribute("ID_CAMPO"));
                    string vIdColumnaValor = UtilXML.ValorAtributo<string>(vXmlControl.Attribute("ID_COLUMNA_VALOR"));
                    string vIdColumnaTexto = UtilXML.ValorAtributo<string>(vXmlControl.Attribute("ID_COLUMNA_TEXTO"));
                    vFgRequerido = (UtilXML.ValorAtributo<bool?>(vXmlControl.Attribute("FG_REQUERIDO")) ?? false);

                    vNbControl = UtilXML.ValorAtributo<string>(vXmlControl.Attribute("NB_CAMPO"));
                    switch (UtilXML.ValorAtributo<string>(vXmlControl.Attribute("CL_TIPO")))
                    {
                        case "TEXTBOX":
                            RadTextBox vRadTextBox = (RadTextBox)vPageView.FindControl(vIdControl);
                            vItem.Add(new XAttribute(vIdColumnaValor, vRadTextBox.Text));
                            vValorControl = vRadTextBox.Text;
                           // vRadTextBox.Text = String.Empty;
                            break;
                        case "MASKBOX":
                            RadMaskedTextBox vRadMaskedTextBox = (RadMaskedTextBox)vPageView.FindControl(vIdControl);
                            vItem.Add(new XAttribute(vIdColumnaValor, vRadMaskedTextBox.Text));
                            vValorControl = vRadMaskedTextBox.Text;
                          //  vRadMaskedTextBox.Text = String.Empty;
                            break;
                        case "NUMERICBOX":
                            RadNumericTextBox vRadNumericTextBox = (RadNumericTextBox)vPageView.FindControl(vIdControl);
                            vItem.Add(new XAttribute(vIdColumnaValor, vRadNumericTextBox.Value ?? 0));
                            vValorControl = (vRadNumericTextBox.Value).ToString();
                          //  vRadNumericTextBox.Value = 0;
                            break;
                        case "COMBOBOX":
                            RadComboBox vRadComboBox = (RadComboBox)vPageView.FindControl(vIdControl);
                            RadComboBoxItem vRadComboBoxItem = vRadComboBox.SelectedItem;
                            vItem.Add(new XAttribute(vIdColumnaValor, (vRadComboBoxItem != null) ? vRadComboBoxItem.Value : string.Empty));
                            vItem.Add(new XAttribute(vIdColumnaTexto, (vRadComboBoxItem != null) ? vRadComboBoxItem.Text : string.Empty));
                            vValorControl = vRadComboBoxItem != null ? vRadComboBoxItem.Text : string.Empty;
                          //  vRadComboBox.ClearSelection();
                            break;
                        case "DATEPICKER":
                            RadDatePicker vRadDatePicker = (RadDatePicker)vPageView.FindControl(vIdControl);
                           // DateTime vFecha = vRadDatePicker.SelectedDate ?? DateTime.Now;
                            if (vRadDatePicker.SelectedDate != null)
                            {
                                DateTime vFecha = vRadDatePicker.SelectedDate.Value;
                                vItem.Add(new XAttribute(vIdColumnaValor, vFecha.ToString("dd/MM/yyyy")));
                                vValorControl = vFecha.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                vValorControl= "";
                            }
                           // vRadDatePicker.SelectedDate = null;
                            break;
                        case "DATEAGE":
                            RadDatePicker vRadDateAgePicker = (RadDatePicker)vPageView.FindControl(vIdControl);
                            DateTime vFechaNacimiento = vRadDateAgePicker.SelectedDate ?? DateTime.Now;
                            vItem.Add(new XAttribute(vIdColumnaValor, vFechaNacimiento.ToString("dd/MM/yyyy")));
                            vValorControl = vFechaNacimiento.ToString("dd/MM/yyyy");
                            vRadDateAgePicker.SelectedDate = null;
                            RadTextBox vTxtEdad = (RadTextBox)vPageView.FindControl(string.Format("txt{0}", vIdControl));
                          //  vTxtEdad.Text = string.Empty;
                            break;
                        case "CHECKBOX":
                            RadButton vRadCheckBox = (RadButton)vPageView.FindControl(vIdControl);
                            vItem.Add(new XAttribute(vIdColumnaValor, vRadCheckBox.Checked ? "1" : "0"));
                            if (vRadCheckBox.Checked)
                                vItem.Add(new XAttribute(vIdColumnaTexto, vRadCheckBox.SelectedToggleState.Text));
                            vValorControl = vRadCheckBox.SelectedToggleState.Text;
                            if (!vRadCheckBox.Checked)
                                vItem.Add(new XAttribute(vIdColumnaTexto, "No"));
                            vValorControl = "No";
                            break;
                    }

                    if (vFgRequerido && vValorControl == "")
                    {
                        vMensajes += String.Format("El campo {0} es requerido.<br />", vNbControl);
                    }
                }

                //if (vIdControlGrid == "LS_PARIENTES")
                //{
                //    if (string.IsNullOrEmpty(vMensajes))
                //    {
                //        //if (UtilXML.ValorAtributo<string>(vItem.Attribute("NB_PARIENTE")).ToString() != "")
                //        vXmlGrid.Element("GRID").Element("DATOS").Add(vItem);
                //        LimpiarControles(vIdControlGrid);
                //    }
                //    else
                //    {
                //        if (vPageView.Page is Empleado)
                //        {
                //            RadWindowManager rwmAlertas = (RadWindowManager)(((Empleado)vPageView.Page).FindControl("rwmAlertas"));
                //            RadWindowManager vRw = (RadWindowManager)vPageView.FindControl("rwmAlertas");
                //            if (vRw != null)
                //                vRw.RadAlert(vMensajes, 350, 150, "Mensaje", null);
                //        }
                //        if (vPageView.Page is Solicitud)
                //        {
                //            RadWindowManager rwmAlertas = (RadWindowManager)(((Solicitud)vPageView.Page).FindControl("rwmAlertas"));
                //            RadWindowManager vRw = (RadWindowManager)vPageView.FindControl("rwmAlertas");
                //            if (vRw != null)
                //                vRw.RadAlert(vMensajes, 350, 150, "Mensaje", null);
                //        }
                //    }
                //}
                //else
                //{
                    if (string.IsNullOrEmpty(vMensajes))
                    {
                        vXmlGrid.Element("GRID").Element("DATOS").Add(vItem);
                        LimpiarControles(vIdControlGrid);
                    }
                    else
                    {
                        if (vPageView.Page is Empleado)
                        {
                            RadWindowManager rwmAlertas = (RadWindowManager)(((Empleado)vPageView.Page).FindControl("rwmAlertas"));
                            RadWindowManager vRw = (RadWindowManager)vPageView.FindControl("rwmAlertas");
                            if (vRw != null)
                                vRw.RadAlert(vMensajes, 350, 150, "Mensaje", null);
                        }
                        if(vPageView.Page is Solicitud)
                        {
                            RadWindowManager rwmAlertas = (RadWindowManager)(((Solicitud)vPageView.Page).FindControl("rwmAlertas"));
                            RadWindowManager vRw = (RadWindowManager)vPageView.FindControl("rwmAlertas");
                            if (vRw != null)
                                vRw.RadAlert(vMensajes, 350, 150, "Mensaje", null);
                        }

                    }
    
              //  }

                xmlPlantilla = vXmlPlantilla.ToString();

                //Si la instrucción viene de la página de solicitudes
                if (vBoton.Page is Solicitud)
                {
                    ((Solicitud)vBoton.Page).vXmlSolicitudPlantilla = xmlPlantilla;

                    if (((Solicitud)vBoton.Page).vDatosModificar.Exists(t => t.Key.Equals(vIdControlGrid)))
                    {
                        ((Solicitud)vBoton.Page).vDatosModificar.RemoveAll(t => t.Key.Equals(vIdControlGrid));
                    }
                }

                //Si la instrucción viene de la página de empleados
                if (vBoton.Page is Empleado)
                {
                    ((Empleado)vBoton.Page).vXmlEmpleadoPlantilla = xmlPlantilla;

                    if (((Empleado)vBoton.Page).vDatosModificar.Exists(t => t.Key.Equals(vIdControlGrid)))
                    {
                        ((Empleado)vBoton.Page).vDatosModificar.RemoveAll(t => t.Key.Equals(vIdControlGrid));
                    }
                }


                //Si la instrucción viene del inventario de personal de PDE
                if (vBoton.Page is VentanaInventarioPersonal)
                    ((VentanaInventarioPersonal)vBoton.Page).vXmlEmpleadoPlantilla = xmlPlantilla;


                //Si la instruccion viene de inventario de personal de PDE
                if (vBoton.Page is VentanaInventarioPersonalAdmin)
                    ((VentanaInventarioPersonalAdmin)vBoton.Page).vXmlEmpleadoPlantilla = xmlPlantilla;

                RadButton vBtnAgregar = (RadButton)vPageView.FindControl("btnAdd" + vIdControlGrid);
                RadButton vBtnCancelar = (RadButton)vPageView.FindControl("btnCancelar" + vIdControlGrid);

                if (vBtnAgregar != null)
                {
                    vBtnAgregar.Text = "Agregar";
                }

                if (vBtnCancelar != null)
                {
                    vBtnCancelar.Text = "Limpiar";
                }

                CargarGrid();

            }
        }

        public void EditarGridItem(object sender, EventArgs e)
        {
            RadButton vBoton = (RadButton)sender;

            //obtenemos el id del control del grid
            string vIdControlGrid = vBoton.ID.Replace("btnEdit", string.Empty);
            string vIdItem;

            //obtenemos el page donde nos cneonctramos y el XML correspondiente al grid que tiene la información que se quiere modificar
            XElement vXmlPlantilla = XElement.Parse(xmlPlantilla);
            XElement vXmlGrid = null;
            RadPageView vPageView = null;
            List<E_GENERICA> vControlesDependientes = new List<E_GENERICA>();

            foreach (XElement vXmlContenedor in vXmlPlantilla.Element("CONTENEDORES").Elements("CONTENEDOR"))
            {
                vPageView = (RadPageView)FormularioDinamico.ObtenerContenedor(UtilXML.ValorAtributo<string>(vXmlContenedor.Attribute("ID_CONTENEDOR")), lstContenedores);
                vXmlGrid = vXmlContenedor.Elements("CAMPO").FirstOrDefault(f => UtilXML.ValorAtributo<string>(f.Attribute("ID_CAMPO")).Equals(vIdControlGrid));
                if (vXmlGrid != null)
                    break;
            }

            if (vXmlGrid != null)
            {
                RadGrid vGrid = (RadGrid)vPageView.FindControl(vIdControlGrid);
                //GridDataItem vItemSeleccionado = (GridDataItem)vGrid.SelectedItems[0].DataItem;

                //Recorremos los item seleccionados
                foreach (GridDataItem item in vGrid.SelectedItems)
                {
                    vIdItem = item.GetDataKeyValue("ID_ITEM").ToString();

                    //Recorremos todos los controles asociados al grid
                    foreach (XElement vXmlCampo in vXmlGrid.Element("FORMULARIO").Elements("CAMPO"))
                    {
                        //Buscamos el ID de columna relacionada con el campo del formulario para extraer la información y ponerla en el control
                        string vIdColumna = UtilXML.ValorAtributo<string>(vXmlCampo.Attribute("ID_COLUMNA_VALOR"));
                        string vClTipoControl = UtilXML.ValorAtributo<string>(vXmlCampo.Attribute("CL_TIPO"));
                        string vIdCampoControl = UtilXML.ValorAtributo<string>(vXmlCampo.Attribute("ID_CAMPO"));


                        /*Aquí vamos a buscar las dependencias */
                        //vamos a buscar si tiene un campo dependiente el control                        
                        XElement vXmlCtrlFormularioDependientes = vXmlCampo.Element("DEPENDENCIAS");

                        if (vXmlCtrlFormularioDependientes != null)
                        {
                            foreach (XElement vXmlControlDependiente in vXmlCtrlFormularioDependientes.Elements("CAMPO_DEPENDIENTE"))
                                vControlesDependientes.Add(new E_GENERICA() { CL_GENERICA = UtilXML.ValorAtributo<string>(vXmlControlDependiente.Attribute("ID_CAMPO_DEPENDIENTE")) });
                        }
                        /* Aqui termina */


                        // buscamos si el valor, se obtendra del DataKeyName o del valor de la celda
                        bool vFgDataKeyValue = UtilXML.ValorAtributo<bool>(vXmlGrid.Element("GRID").Element("HEADER").Elements("COLUMNA").Where(t => UtilXML.ValorAtributo<string>(t.Attribute("ID_COLUMNA")).Equals(vIdColumna)).FirstOrDefault().Attribute("FG_DATAKEYVALUE"));

                        //Obtenemos el valor
                        string vNbValor = ""; ;





                        if (vFgDataKeyValue)
                        {
                            vNbValor = item.GetDataKeyValue(vIdColumna).ToString();
                        }
                        else
                        {
                            if (item[vIdColumna].Text != "&nbsp;")
                            {
                                vNbValor = item[vIdColumna].Text;
                            }

                        }

                        // dependiendo del tipo de control, se obtiene el control del PageView y se asigna el dato
                        switch (vClTipoControl)
                        {
                            case "TEXTBOX":
                                RadTextBox vRadTextBox = (RadTextBox)vPageView.FindControl(vIdCampoControl);
                                vRadTextBox.Text = vNbValor;
                                break;
                            case "MASKBOX":
                                RadMaskedTextBox vRadMaskedTextBox = (RadMaskedTextBox)vPageView.FindControl(vIdCampoControl);
                                vRadMaskedTextBox.Text = vNbValor;
                                break;
                            case "NUMERICBOX":
                                RadNumericTextBox vRadNumericTextBox = (RadNumericTextBox)vPageView.FindControl(vIdCampoControl);

                                double vDblValor;

                                if (double.TryParse(vNbValor, out vDblValor))
                                {
                                    vRadNumericTextBox.Value = double.Parse(vNbValor);
                                }

                                break;
                            case "COMBOBOX":
                                RadComboBox vRadComboBox = (RadComboBox)vPageView.FindControl(vIdCampoControl);
                                vRadComboBox.SelectedValue = vNbValor;

                                if (vControlesDependientes.Exists(t => t.CL_GENERICA.Equals(vIdCampoControl)))
                                {
                                    vControlesDependientes.Where(t => t.CL_GENERICA.Equals(vIdCampoControl)).FirstOrDefault().NB_GENERICO = vNbValor;
                                }

                                break;
                            case "DATEPICKER":
                            case "DATEAGE":
                                if (vNbValor != "")
                                {
                                    RadDatePicker vRadDatePicker = ((RadDatePicker)vPageView.FindControl(vIdCampoControl));
                                    vRadDatePicker.SelectedDate = DateTime.Parse(vNbValor);
                                }
                                break;
                            case "CHECKBOX":
                                RadButton vRadCheckBox = (RadButton)vPageView.FindControl(vIdCampoControl);
                                vRadCheckBox.Checked = bool.Parse(vNbValor);
                                break;
                        }
                    }

                    //Como nada mas va a estar seleccionado un solo registro, al final del foreach, ocultar y mostrar los botones necesarios

                    RadButton vBtnAgregar = (RadButton)vPageView.FindControl("btnAdd" + vIdControlGrid);
                    RadButton vBtnCancelar = (RadButton)vPageView.FindControl("btnCancelar" + vIdControlGrid);

                    if (vBtnAgregar != null)
                    {
                        vBtnAgregar.Text = "Aceptar";
                    }

                    if (vBtnCancelar != null)
                    {
                        vBtnCancelar.Text = "Cancelar";
                    }


                    if (vPageView.Page is Solicitud)
                    {
                        List<KeyValuePair<string, string>> vListaDatosModificar = ((Solicitud)vPageView.Page).vDatosModificar;

                        if (vListaDatosModificar.Exists(t => t.Key.Equals(vIdControlGrid)))
                        {
                            vListaDatosModificar.RemoveAll(t => t.Key.Equals(vIdControlGrid));
                        }

                        ((Solicitud)vPageView.Page).vControlDependienteSeleccion.AddRange(vControlesDependientes);
                        vListaDatosModificar.Add(new KeyValuePair<string, string>(vIdControlGrid, vIdItem));
                        ((Solicitud)vPageView.Page).vDatosModificar = vListaDatosModificar;

                    }

                    if (vPageView.Page is Empleado)
                    {
                        List<KeyValuePair<string, string>> vListaDatosModificar = ((Empleado)vPageView.Page).vDatosModificar;

                        if (vListaDatosModificar.Exists(t => t.Key.Equals(vIdControlGrid)))
                        {
                            vListaDatosModificar.RemoveAll(t => t.Key.Equals(vIdControlGrid));
                        }

                        //Agregamos los controles dependientes que se tendrán que actualizar con el
                        ((Empleado)vPageView.Page).vControlDependienteSeleccion.AddRange(vControlesDependientes);
                        vListaDatosModificar.Add(new KeyValuePair<string, string>(vIdControlGrid, vIdItem));
                        ((Empleado)vPageView.Page).vDatosModificar = vListaDatosModificar;
                    }

                    //if (vPageView.Page is VentanaInventarioPersonal)
                    //    ((VentanaInventarioPersonal)vPageView.Page).vXmlEmpleadoPlantilla = xmlPlantilla;

                    //if (vPageView.Page is VentanaInventarioPersonalAdmin)
                    //    ((VentanaInventarioPersonalAdmin)vPageView.Page).vXmlEmpleadoPlantilla = xmlPlantilla;

                }
            }
        }

        public void AceptarEdicionGridItem(object sender, EventArgs e)
        {
            //RadButton vBoton = (RadButton)sender;
            //string vIdControlGrid = vBoton.ID.Replace("btnGuardar", String.Empty);
            //string vIdItem = "";

            //XElement vXmlPlantilla = XElement.Parse(xmlPlantilla);
            //XElement vXmlGrid = null;
            //RadPageView vPageView = null;

            //foreach (XElement vXmlContenedor in vXmlPlantilla.Element("CONTENEDORES").Elements("CONTENEDOR"))
            //{
            //    vPageView = (RadPageView)FormularioDinamico.ObtenerContenedor(UtilXML.ValorAtributo<string>(vXmlContenedor.Attribute("ID_CONTENEDOR")), lstContenedores);
            //    vXmlGrid = vXmlContenedor.Elements("CAMPO").FirstOrDefault(f => UtilXML.ValorAtributo<string>(f.Attribute("ID_CAMPO")).Equals(vIdControlGrid));
            //    if (vXmlGrid != null)
            //        break;
            //}

            //bool vFgRequerido = false;
            //string vNbControl = string.Empty;

            ////if (vPageView.Page is Solicitud)
            ////    ((Solicitud)vPageView.Page).vXmlSolicitudPlantilla = xmlPlantilla;

            //if (vPageView.Page is Empleado)
            //    vIdItem = ((Empleado)vPageView.Page).vDatosModificar.Value; //= new KeyValuePair<string, string>(vIdControlGrid, vIdItem);

            ////if (vPageView.Page is VentanaInventarioPersonal)
            ////    ((VentanaInventarioPersonal)vPageView.Page).vXmlEmpleadoPlantilla = xmlPlantilla;

            ////if (vPageView.Page is VentanaInventarioPersonalAdmin)
            ////    ((VentanaInventarioPersonalAdmin)vPageView.Page).vXmlEmpleadoPlantilla = xmlPlantilla;

            //if (vXmlGrid != null)
            //{
            //    RadGrid vGrid = (RadGrid)vPageView.FindControl(vIdControlGrid);
            //    XElement vItem = vXmlGrid.Element("GRID").Element("DATOS").Elements("ITEM").Where(t => UtilXML.ValorAtributo<string>(t.Attribute("ID_ITEM")).Equals(vIdItem)).FirstOrDefault();  //new XElement("ITEM", new XAttribute("ID_ITEM", Guid.NewGuid()));

            //    var vMensajes = string.Empty;

            //    foreach (XElement vXmlControl in vXmlGrid.Element("FORMULARIO").Elements("CAMPO"))
            //    {
            //        string vIdControl = UtilXML.ValorAtributo<string>(vXmlControl.Attribute("ID_CAMPO"));
            //        string vIdColumnaValor = UtilXML.ValorAtributo<string>(vXmlControl.Attribute("ID_COLUMNA_VALOR"));
            //        string vIdColumnaTexto = UtilXML.ValorAtributo<string>(vXmlControl.Attribute("ID_COLUMNA_TEXTO"));

            //        vFgRequerido = (UtilXML.ValorAtributo<bool?>(vXmlControl.Attribute("FG_REQUERIDO")) ?? false);
            //        //vNbControl = UtilXML.ValorAtributo<string>(vXmlControl.Attribute("NB_CAMPO"));

            //        switch (UtilXML.ValorAtributo<string>(vXmlControl.Attribute("CL_TIPO")))
            //        {
            //            case "TEXTBOX":
            //                RadTextBox vRadTextBox = (RadTextBox)vPageView.FindControl(vIdControl);
            //                UtilXML.AsignarValorAtributo(vItem, vIdColumnaValor, vRadTextBox.Text); //vItem.Add(new XAttribute(vIdColumnaValor, vRadTextBox.Text));
            //                vRadTextBox.Text = String.Empty;
            //                break;
            //            case "MASKBOX":
            //                RadMaskedTextBox vRadMaskedTextBox = (RadMaskedTextBox)vPageView.FindControl(vIdControl);
            //                UtilXML.AsignarValorAtributo(vItem, vIdColumnaValor, vRadMaskedTextBox.Text); //vItem.Add(new XAttribute(vIdColumnaValor, vRadMaskedTextBox.Text));
            //                vRadMaskedTextBox.Text = String.Empty;
            //                break;
            //            case "NUMERICBOX":
            //                RadNumericTextBox vRadNumericTextBox = (RadNumericTextBox)vPageView.FindControl(vIdControl);

            //                string vNbValor = vRadNumericTextBox.Value.HasValue ? vRadNumericTextBox.Value.Value.ToString() : "0";
            //                UtilXML.AsignarValorAtributo(vItem, vIdColumnaValor, vNbValor); //vItem.Add(new XAttribute(vIdColumnaValor, vRadNumericTextBox.Value ?? 0));
            //                vRadNumericTextBox.Value = 0;

            //                break;
            //            case "COMBOBOX":
            //                RadComboBox vRadComboBox = (RadComboBox)vPageView.FindControl(vIdControl);
            //                RadComboBoxItem vRadComboBoxItem = vRadComboBox.SelectedItem;

            //                UtilXML.AsignarValorAtributo(vItem, vIdColumnaValor, (vRadComboBoxItem != null) ? vRadComboBoxItem.Value : string.Empty);
            //                UtilXML.AsignarValorAtributo(vItem, vIdColumnaTexto, (vRadComboBoxItem != null) ? vRadComboBoxItem.Text : string.Empty);

            //                vRadComboBox.SelectedValue = String.Empty;
            //                break;
            //            case "DATEPICKER":
            //            case "DATEAGE":
            //                DateTime vFecha = ((RadDatePicker)vPageView.FindControl(vIdControl)).SelectedDate ?? DateTime.Now;
            //                UtilXML.AsignarValorAtributo(vItem, vIdColumnaValor, vFecha.ToString("dd/MM/yyyy"));
            //                //vItem.Add(new XAttribute(vIdColumnaValor, vFecha.ToString("dd/MM/yyyy")));

            //                break;
            //            case "CHECKBOX":
            //                RadButton vRadCheckBox = (RadButton)vPageView.FindControl(vIdControl);
            //                UtilXML.AsignarValorAtributo(vItem, vIdColumnaValor, vRadCheckBox.Checked ? "1" : "0"); //vItem.Add(new XAttribute(vIdColumnaValor, vRadCheckBox.Checked ? "1" : "0"));
            //                UtilXML.AsignarValorAtributo(vItem, vIdColumnaTexto, vRadCheckBox.SelectedToggleState.Text); //vItem.Add(new XAttribute(vIdColumnaTexto, vRadCheckBox.SelectedToggleState.Text));
            //                break;
            //        }
            //        if (vFgRequerido)
            //        {
            //            vMensajes += String.Format("El campo {0} es requerido.<br />", vNbControl);
            //        }
            //    }

            //    if (vIdControlGrid == "LS_PARIENTES")
            //    {
            //        if (string.IsNullOrEmpty(vMensajes))
            //        {
            //            if (UtilXML.ValorAtributo<string>(vItem.Attribute("NB_PARIENTE")).ToString() != "")
            //            {
            //                vXmlGrid.Element("GRID").Element("DATOS").Elements("ITEM").Where(w => UtilXML.ValorAtributo<string>(w.Attribute("ID_ITEM")) == vIdItem).Remove();
            //                vXmlGrid.Element("GRID").Element("DATOS").Add(vItem);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (string.IsNullOrEmpty(vMensajes))
            //        {
            //            vXmlGrid.Element("GRID").Element("DATOS").Elements("ITEM").Where(w => UtilXML.ValorAtributo<string>(w.Attribute("ID_ITEM")) == vIdItem).Remove();
            //            vXmlGrid.Element("GRID").Element("DATOS").Add(vItem);

            //        }
            //    }


            //    //Como nada mas va a estar seleccionado un solo registro, al final del foreach, ocultar y mostrar los botones necesarios
            //    RadButton vBtnAgregar = (RadButton)vPageView.FindControl("btnAdd" + vIdControlGrid);
            //    RadButton vBtnEliminar = (RadButton)vPageView.FindControl("btnDel" + vIdControlGrid);
            //    RadButton vBtnEditar = (RadButton)vPageView.FindControl("btnEdit" + vIdControlGrid);
            //    RadButton vBtnCancelar = (RadButton)vPageView.FindControl("btnCancelar" + vIdControlGrid);
            //    RadButton vBtnGuardar = (RadButton)vPageView.FindControl("btnGuardar" + vIdControlGrid);

            //    vBtnAgregar.Visible = true;
            //    vBtnEliminar.Visible = true;
            //    vBtnEditar.Visible = true;
            //    vBtnCancelar.Visible = false;
            //    vBtnGuardar.Visible = false;

            //    xmlPlantilla = vXmlPlantilla.ToString();

            //    //if (vBoton.Page is Solicitud)
            //    //    ((Solicitud)vBoton.Page).vXmlSolicitudPlantilla = xmlPlantilla;

            //    if (vPageView.Page is Empleado)
            //        ((Empleado)vPageView.Page).vDatosModificar = new KeyValuePair<string, string>("", "");

            //    //if (vBoton.Page is VentanaInventarioPersonal)
            //    //    ((VentanaInventarioPersonal)vBoton.Page).vXmlEmpleadoPlantilla = xmlPlantilla;

            //    //if (vBoton.Page is VentanaInventarioPersonalAdmin)
            //    //    ((VentanaInventarioPersonalAdmin)vBoton.Page).vXmlEmpleadoPlantilla = xmlPlantilla;

            //    CargarGrid();
            //}
        }

        public void CancelarEdicionGridItem(object sender, EventArgs e)
        {
            RadButton vBoton = (RadButton)sender;

            //obtenemos el id del control del grid
            string vIdControlGrid = vBoton.ID.Replace("btnCancelar", string.Empty);

            //obtenemos el page donde nos cneonctramos y el XML correspondiente al grid que tiene la información que se quiere modificar
            XElement vXmlPlantilla = XElement.Parse(xmlPlantilla);
            XElement vXmlGrid = null;
            RadPageView vPageView = null;

            foreach (XElement vXmlContenedor in vXmlPlantilla.Element("CONTENEDORES").Elements("CONTENEDOR"))
            {
                vPageView = (RadPageView)FormularioDinamico.ObtenerContenedor(UtilXML.ValorAtributo<string>(vXmlContenedor.Attribute("ID_CONTENEDOR")), lstContenedores);
                vXmlGrid = vXmlContenedor.Elements("CAMPO").FirstOrDefault(f => UtilXML.ValorAtributo<string>(f.Attribute("ID_CAMPO")).Equals(vIdControlGrid));
                if (vXmlGrid != null)
                    break;
            }

            if (vXmlGrid != null)
            {
                foreach (XElement vXmlCampo in vXmlGrid.Element("FORMULARIO").Elements("CAMPO"))
                {
                    string vClTipoControl = UtilXML.ValorAtributo<string>(vXmlCampo.Attribute("CL_TIPO"));
                    string vIdCampoControl = UtilXML.ValorAtributo<string>(vXmlCampo.Attribute("ID_CAMPO"));

                    // dependiendo del tipo de control, se obtiene el control del PageView y se asigna el dato
                    switch (vClTipoControl)
                    {
                        case "TEXTBOX":
                            RadTextBox vRadTextBox = (RadTextBox)vPageView.FindControl(vIdCampoControl);
                            vRadTextBox.Text = "";
                            break;
                        case "MASKBOX":
                            RadMaskedTextBox vRadMaskedTextBox = (RadMaskedTextBox)vPageView.FindControl(vIdCampoControl);
                            vRadMaskedTextBox.Text = "";
                            break;
                        case "NUMERICBOX":
                            RadNumericTextBox vRadNumericTextBox = (RadNumericTextBox)vPageView.FindControl(vIdCampoControl);
                            vRadNumericTextBox.Value = null;

                            break;
                        case "COMBOBOX":
                            RadComboBox vRadComboBox = (RadComboBox)vPageView.FindControl(vIdCampoControl);
                            vRadComboBox.ClearSelection();
                            break;
                        case "DATEPICKER":
                        case "DATEAGE":
                            RadDatePicker vRadDatePicker = ((RadDatePicker)vPageView.FindControl(vIdCampoControl));
                            vRadDatePicker.SelectedDate = null;
                            break;
                        case "CHECKBOX":
                            RadButton vRadCheckBox = (RadButton)vPageView.FindControl(vIdCampoControl);
                            vRadCheckBox.Checked = false;
                            break;
                    }

                }

                RadButton vBtnAgregar = (RadButton)vPageView.FindControl("btnAdd" + vIdControlGrid);
                RadButton vBtnCancelar = (RadButton)vPageView.FindControl("btnCancelar" + vIdControlGrid);

                if (vBtnAgregar != null)
                {
                    vBtnAgregar.Text = "Agregar";
                }

                if (vBtnCancelar != null)
                {
                    vBtnCancelar.Text = "Limpiar";
                }

                if (vPageView.Page is Solicitud)
                {
                    List<KeyValuePair<string, string>> vListaDatosModificar = ((Solicitud)vPageView.Page).vDatosModificar;
                    vListaDatosModificar.RemoveAll(t => t.Key.Equals(vIdControlGrid));
                    ((Solicitud)vPageView.Page).vDatosModificar = vListaDatosModificar;
                }

                if (vPageView.Page is Empleado)
                {
                    List<KeyValuePair<string, string>> vListaDatosModificar = ((Empleado)vPageView.Page).vDatosModificar;
                    vListaDatosModificar.RemoveAll(t => t.Key.Equals(vIdControlGrid));
                    ((Empleado)vPageView.Page).vDatosModificar = vListaDatosModificar;
                }


            }


        }

        public void CargarGrid()
        {
            foreach (XElement vXmlContenedor in XElement.Parse(xmlPlantilla).Element("CONTENEDORES").Elements("CONTENEDOR"))
            {
                RadPageView vPageView = (RadPageView)FormularioDinamico.ObtenerContenedor(UtilXML.ValorAtributo<string>(vXmlContenedor.Attribute("ID_CONTENEDOR")), lstContenedores);
                if (vPageView != null)
                {
                    foreach (XElement vXmlGrid in vXmlContenedor.Elements("CAMPO").Where(w => UtilXML.ValorAtributo<string>(w.Attribute("CL_TIPO")) == "GRID"))
                    {
                        RadGrid vRadGrid = (RadGrid)vPageView.FindControl(UtilXML.ValorAtributo<string>(vXmlGrid.Attribute("ID_CAMPO")));
                        DataTable vDataTable = (DataTable)vRadGrid.DataSource;
                        vDataTable.Clear();
                        if (vXmlGrid.Element("GRID").Element("DATOS") != null)
                            foreach (XElement vXmlFila in vXmlGrid.Element("GRID").Element("DATOS").Elements("ITEM"))
                            {
                                DataRow row = vDataTable.NewRow();
                                foreach (XElement vXmlColumna in vXmlGrid.Element("GRID").Element("HEADER").Elements("COLUMNA"))
                                    row[UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("ID_COLUMNA"))] = UtilXML.ValorAtributo(vXmlFila.Attribute(UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("ID_COLUMNA"))), Utileria.ObtenerEnumTipoDato(UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("CL_TIPO_DATO")))) ?? DBNull.Value;
                                vDataTable.Rows.Add(row);
                            }
                        string vOrdenamiento = UtilXML.ValorAtributo<string>(vXmlGrid.Element("GRID").Attribute("ORDERBY"));
                        string vTipoOrdenamiento = UtilXML.ValorAtributo<string>(vXmlGrid.Element("GRID").Attribute("ORDERTYPE"));
                        DataView dv;

                        if (vOrdenamiento != null)
                        {
                            dv = vDataTable.DefaultView;
                            dv.Sort = vOrdenamiento + " " + vTipoOrdenamiento;
                            vDataTable = dv.ToTable();
                        }

                        vRadGrid.DataSource = vDataTable;
                        vRadGrid.DataBind();
                    }
                }
            }
        }

        public XElement GuardarFormulario()
        {
            XElement vXmlPlantilla = XElement.Parse(xmlPlantilla);
            bool vFgAsignarXML = true;
            int vNoMensajes = 0;
            string vMensajes = String.Empty;
            XElement vXmlMensajes = new XElement("MENSAJES");
            foreach (XElement vXmlContenedor in vXmlPlantilla.Element("CONTENEDORES").Elements("CONTENEDOR"))
            {
                string vNbContenedor = vXmlContenedor.Attribute("ID_CONTENEDOR").Value;
                RadPageView vPageView = (RadPageView)FormularioDinamico.ObtenerContenedor(vNbContenedor, lstContenedores);

                if (vPageView != null)
                {
                    foreach (XElement vXmlControl in vXmlContenedor.Elements("CAMPO"))
                    {
                        string vClTipoControl = UtilXML.ValorAtributo<string>(vXmlControl.Attribute("CL_TIPO"));
                        string vIdControl = UtilXML.ValorAtributo<string>(vXmlControl.Attribute("ID_CAMPO"));
                        string vNbControl = UtilXML.ValorAtributo<string>(vXmlControl.Attribute("NB_CAMPO"));
                        string vNbValor = String.Empty;
                        string vNbTexto = String.Empty;

                        bool vFgRequerido = (UtilXML.ValorAtributo<bool?>(vXmlControl.Attribute("FG_REQUERIDO")) ?? false);

                        bool vFgTieneValor = true;
                        bool vFgAsignarValor = true;

                        Control vControl = vPageView.FindControl(vIdControl);

                        List<KeyValuePair<string, string>> vLstAtributos = new List<KeyValuePair<string, string>>();

                        switch (vClTipoControl)
                        {
                            case "TEXTBOX":
                                vNbValor = ((RadTextBox)vControl).Text;
                                vNbTexto = vNbValor;
                                vLstAtributos.Add(new KeyValuePair<string, string>("NB_VALOR", vNbValor));
                                vLstAtributos.Add(new KeyValuePair<string, string>("NB_TEXTO", vNbTexto));
                                vFgTieneValor = !String.IsNullOrWhiteSpace(vNbValor);
                                break;
                            case "TEXTBOXCP":
                                vNbValor = ((RadTextBox)vControl).Text;
                                vNbTexto = vNbValor;
                                vLstAtributos.Add(new KeyValuePair<string, string>("NB_VALOR", vNbValor));
                                vLstAtributos.Add(new KeyValuePair<string, string>("NB_TEXTO", vNbTexto));
                                vFgTieneValor = !String.IsNullOrWhiteSpace(vNbValor);
                                break;
                            case "MASKBOX":
                                vNbValor = ((RadMaskedTextBox)vControl).Text;
                                vNbTexto = vNbValor;
                                vLstAtributos.Add(new KeyValuePair<string, string>("NB_VALOR", vNbValor));
                                vLstAtributos.Add(new KeyValuePair<string, string>("NB_TEXTO", vNbTexto));
                                vFgTieneValor = !String.IsNullOrWhiteSpace(vNbValor);
                                break;
                            case "CHECKBOX":
                                vNbValor = ((RadButton)vControl).Checked ? "1" : "0";
                                vNbTexto = ((RadButton)vControl).Checked ? "Sí" : "No";
                                vLstAtributos.Add(new KeyValuePair<string, string>("NB_VALOR", vNbValor));
                                vLstAtributos.Add(new KeyValuePair<string, string>("NB_TEXTO", vNbTexto));
                                vFgTieneValor = !String.IsNullOrWhiteSpace(vNbValor);
                                break;
                            case "NUMERICBOX":
                                vNbValor = ((RadNumericTextBox)vControl).Text;
                                vNbTexto = vNbValor;
                                vLstAtributos.Add(new KeyValuePair<string, string>("NB_VALOR", vNbValor));
                                vLstAtributos.Add(new KeyValuePair<string, string>("NB_TEXTO", vNbTexto));
                                vFgTieneValor = !String.IsNullOrWhiteSpace(vNbValor);
                                break;
                            case "DATEPICKER":
                            case "DATEAGE":
                                RadDatePicker vControlF = new RadDatePicker();
                                vControlF = (RadDatePicker)vControl;
                                string value = vControlF.DateInput.InvalidTextBoxValue;
                                DateTime vFecha;

                                if (value == string.Empty)
                                {
                                    vFecha = ((RadDatePicker)vControl).SelectedDate ?? DateTime.Now;

                                    if (vControlF.SelectedDate < vControlF.MinDate)
                                    {
                                        vFecha = DateTime.Now;
                                    }

                                    vNbValor = vFecha.ToString("dd/MM/yyyy");
                                    vNbTexto = vNbValor;
                                    vLstAtributos.Add(new KeyValuePair<string, string>("NB_VALOR", vNbValor));
                                    vLstAtributos.Add(new KeyValuePair<string, string>("NB_TEXTO", vNbTexto));
                                    vFgTieneValor = !String.IsNullOrWhiteSpace(vNbValor);
                                }


                                break;
                            case "COMBOBOX":
                                RadComboBox vComboBox = (RadComboBox)vControl;
                                vNbValor = vComboBox.SelectedValue;
                                if (vComboBox.SelectedItem != null && !String.IsNullOrWhiteSpace((vComboBox.SelectedItem.Text)))
                                    vNbTexto = ((RadComboBox)vControl).SelectedItem.Text;
                                else
                                    vNbTexto = vNbValor;

                                vLstAtributos.Add(new KeyValuePair<string, string>("NB_VALOR", vNbValor));
                                vLstAtributos.Add(new KeyValuePair<string, string>("NB_TEXTO", vNbTexto));
                                vFgTieneValor = !String.IsNullOrWhiteSpace(vNbValor);
                                break;
                            case "LISTBOX":
                                RadListBox vRadListBox = ((RadListBox)vControl);
                                string vClValor = String.Empty;

                                foreach (RadListBoxItem item in vRadListBox.SelectedItems)
                                {
                                    vNbValor = item.Text;
                                    vClValor = item.Value;
                                    vNbTexto = vNbValor;
                                }

                                vLstAtributos.Add(new KeyValuePair<string, string>("NB_VALOR", vNbValor));
                                vLstAtributos.Add(new KeyValuePair<string, string>("CL_VALOR", vClValor));
                                vLstAtributos.Add(new KeyValuePair<string, string>("NB_TEXTO", vNbTexto));
                                vFgTieneValor = !String.IsNullOrWhiteSpace(vClValor) && vClValor != "NA";
                                break;
                            default:
                                vFgAsignarValor = false;
                                break;
                        }

                        if (vFgRequerido)
                        {
                            vFgAsignarValor = vFgTieneValor;
                            vFgAsignarXML = vFgAsignarXML && vFgAsignarValor;
                            if (!vFgAsignarValor)
                            {
                                vXmlMensajes.Add(new XElement("MENSAJE", String.Format("El campo {0} es requerido.<br />", vNbControl)));
                                vMensajes += String.Format("El campo {0} es requerido.<br />", vNbControl);
                                vNoMensajes++;
                            }
                        }

                        if (vFgAsignarValor)
                            foreach (KeyValuePair<string, string> vKey in vLstAtributos)
                                UtilXML.AsignarValorAtributo(vXmlControl, vKey.Key, vKey.Value);

                    }
                }
            }
            return new XElement("RESPUESTA", new XAttribute("FG_VALIDO", vFgAsignarXML), vXmlMensajes, vXmlPlantilla);
        }

        public void LimpiarControles(string vIdControlGrid)
        {
            XElement vXmlPlantilla = XElement.Parse(xmlPlantilla);
            XElement vXmlGrid = null;
            RadPageView vPageView = null;

            foreach (XElement vXmlContenedor in vXmlPlantilla.Element("CONTENEDORES").Elements("CONTENEDOR"))
            {
                vPageView = (RadPageView)FormularioDinamico.ObtenerContenedor(UtilXML.ValorAtributo<string>(vXmlContenedor.Attribute("ID_CONTENEDOR")), lstContenedores);
                vXmlGrid = vXmlContenedor.Elements("CAMPO").FirstOrDefault(f => UtilXML.ValorAtributo<string>(f.Attribute("ID_CAMPO")).Equals(vIdControlGrid));
                if (vXmlGrid != null)
                    break;
            }

            if (vXmlGrid != null)
            {
                if (vPageView.Page is Empleado)
                {
                    if (((Empleado)vPageView.Page).vDatosModificar.Exists(t => t.Key.Equals(vIdControlGrid)))
                    {
                        string vIdItem = ((Empleado)vPageView.Page).vDatosModificar.Where(t => t.Key.Equals(vIdControlGrid)).FirstOrDefault().Value;
                        vXmlGrid.Element("GRID").Element("DATOS").Elements("ITEM").Where(w => UtilXML.ValorAtributo<string>(w.Attribute("ID_ITEM")) == vIdItem).Remove();
                    }
                }

                if (vPageView.Page is Solicitud)
                {
                    if (((Solicitud)vPageView.Page).vDatosModificar.Exists(t => t.Key.Equals(vIdControlGrid)))
                    {
                        string vIdItem = ((Solicitud)vPageView.Page).vDatosModificar.Where(t => t.Key.Equals(vIdControlGrid)).FirstOrDefault().Value;
                        vXmlGrid.Element("GRID").Element("DATOS").Elements("ITEM").Where(w => UtilXML.ValorAtributo<string>(w.Attribute("ID_ITEM")) == vIdItem).Remove();
                    }
                }

                foreach (XElement vXmlControl in vXmlGrid.Element("FORMULARIO").Elements("CAMPO"))
                {
                    string vIdControl = UtilXML.ValorAtributo<string>(vXmlControl.Attribute("ID_CAMPO"));
                    switch (UtilXML.ValorAtributo<string>(vXmlControl.Attribute("CL_TIPO")))
                    {
                        case "TEXTBOX":
                            RadTextBox vRadTextBox = (RadTextBox)vPageView.FindControl(vIdControl);
                             vRadTextBox.Text = String.Empty;
                            break;
                        case "MASKBOX":
                            RadMaskedTextBox vRadMaskedTextBox = (RadMaskedTextBox)vPageView.FindControl(vIdControl);
                              vRadMaskedTextBox.Text = String.Empty;
                            break;
                        case "NUMERICBOX":
                            RadNumericTextBox vRadNumericTextBox = (RadNumericTextBox)vPageView.FindControl(vIdControl);
                             vRadNumericTextBox.Value = 0;
                            break;
                        case "COMBOBOX":
                            RadComboBox vRadComboBox = (RadComboBox)vPageView.FindControl(vIdControl);
                            RadComboBoxItem vRadComboBoxItem = vRadComboBox.SelectedItem;
                              vRadComboBox.ClearSelection();
                            break;
                        case "DATEPICKER":
                            RadDatePicker vRadDatePicker = (RadDatePicker)vPageView.FindControl(vIdControl);
                             vRadDatePicker.SelectedDate = null;
                            break;
                        case "DATEAGE":
                            RadDatePicker vRadDateAgePicker = (RadDatePicker)vPageView.FindControl(vIdControl);
                            RadTextBox vTxtEdad = (RadTextBox)vPageView.FindControl(string.Format("txt{0}", vIdControl));
                              vTxtEdad.Text = string.Empty;
                            break;
                        case "CHECKBOX":
                            RadButton vRadCheckBox = (RadButton)vPageView.FindControl(vIdControl);
                            vRadCheckBox.Checked = false;
                            break;
                    }
                }
            }
        }
    }
}