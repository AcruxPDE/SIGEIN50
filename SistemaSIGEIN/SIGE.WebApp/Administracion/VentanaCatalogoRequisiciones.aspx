<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaCatalogoRequisiciones.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaCatalogoRequisiciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <script id="modal" type="text/javascript">
        var fe_requerimiento = "";
        var fe_solicitud = "";
        function GetRadWindow() {
            var oWindow = null;
            if
                (window.radWindow) oWindow = window.radWindow;
            else
                if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function closeWindow() {
            GetRadWindow().close();
        }


        function OnDateSelectedFe_solicitud(sender, eventArgs) {

            var date1 = sender.get_selectedDate();
            date1.setDate(date1.getDate() + 31);
            var datepicker = $find("<%= Fe_solicitud.ClientID %>");
            datepicker.set_maxDate(date1);
        }

        function OnDateSelectedFe_requerimiento(sender, eventArgs) {
            var date1 = sender.get_selectedDate();
            date1.setDate(date1.getDate() + 31);
            var datepicker = $find("<%= Fe_Requerimiento.ClientID %>");
            datepicker.set_maxDate(date1);
        }

        function OpenNotificateWindow()
          {
                  openChildDialog("NotificarRequisicion.aspx", "winNotificarRequisicion", "Notificación al área de RR HH");
          }



        function OpenEmployeeSelectionWindow() {
            var combo = $find("<%= cmbPuestos.ClientID %>");//COMBO PUESTOS
            var id_puesto = combo.get_selectedItem().get_value();//
            var nb_puesto = combo.get_selectedItem().get_text();
            
            var cmbAutoriza = $find("<%= cmbAutoriza.ClientID %>");//COMBO DE EMPLEADO QUE PUEDEN AUTORIZAR LA REQUISICION
            var sueldo = $find("<%= txtSueldo.ClientID %>");//TEXTBOX DE SUELDO
            var No_requisicion = $find("<%= txtNo_requisicion.ClientID %>");// FOLIO DE REQUISICION
            var vfe_requerimiento = $find("<%= Fe_Requerimiento.ClientID %>");//FECHA EN LA QUE SE REQUIERE
            var vfe_solicitud = $find("<%= Fe_solicitud.ClientID %>");//FECHA DE SOLICITUD
            var p_fe_requerimiento = vfe_requerimiento.get_selectedDate();//
            var p_fe_solicitud = vfe_solicitud.get_selectedDate();//FECHA DE SOLICITUD
         
            var nb_autoriza = cmbAutoriza.get_selectedItem().get_text(); //NB_PERSONA QUE VA A AUTORIZAR
            var Id_Empleado = cmbAutoriza.get_selectedItem().get_value();//OBTIENE EL ID_EMPLEADO 
            var NoRequisicion = $find("<%= txtNo_requisicion.ClientID %>");//
            var NB_solicita = $find("<%= txtSolicitado.ClientID %>");//

            var Notificar = {
                solicita : NB_solicita.get_value(),
                no_requisicion: NoRequisicion.get_value(),
                idPuesto: id_puesto,
                sueldo: sueldo.get_value(),
                fe_req: p_fe_requerimiento,
                fe_sol: p_fe_solicitud,
                id_empleado: Id_Empleado,
                nbpuesto : nb_puesto
            };
            //console.info(Notificar);
            //sessionStorage.setItem("ObjetoEnviar", JSON.stringify(Notificar));
            if (sueldo.get_value() != "")
            {
                openChildDialog("NotificarRequisicion.aspx", "winNotificarRequisicion", "Notificación al área de RR HH");
            } 

        }


        function AbrirAutorizarModal() {
            openChildDialog("AutorizarRequisicion.aspx", "winAutorizarRequisicion", "Notificación de autorización de requisición");
        }

        function useDataFromChild(pEmpleados) {
            if (pEmpleados != null) {
                var vEmpleadoSeleccionado = pEmpleados[0];
                $find("<%=txtNo_requisicion.ClientID %>").set_value(vEmpleadoSeleccionado.nbEmpleado);
            }
        }
    </script>


    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">

        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cmbPuestos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtClPuesto" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtArea" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtSueldo" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>

                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="cmbCausas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtEspecifique" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

                 <telerik:AjaxSetting AjaxControlID="txtNotificarRH">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="btnNotificarAutoriza">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
         

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div style="height: calc(100% - 150px);">
        <telerik:RadPanelBar ID="rad" runat="server" Height="400" Width="100%" ExpandMode="FullExpandedItem">
            <Items>
                <telerik:RadPanelItem Text="Información del área" Expanded="true">
                    <ContentTemplate>
                        <br />
                        <div style="text-align: center; align-content: center;">
                            <telerik:RadTextBox ID="txtmensaje" runat="server" Width="80%" Height="75px"
                                Text="Te informamos que si el puesto a cubrir no está en el catálogo de puestos, deberás dar click en el botón Notificar a RRHH, para que el área de Recursos Humanos cree el nuevo puesto con las características necesarias."
                                TextMode="MultiLine" ReadOnly="true">
                            </telerik:RadTextBox>
                        </div>

                        <br />

                        <table>
                            <tr>
                                <td>

                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <label id="Label18" name="lblNbIdioma" runat="server">Puesto a cubrir:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadComboBox
                                                Filter="Contains"
                                                runat="server"
                                                ID="cmbPuestos"
                                                Width="200"
                                                MarkFirstMatch="true"
                                                AutoPostBack="true"
                                                EmptyMessage="Seleccione..."
                                                DropDownWidth="340"
                                                Height="300"
                                                OnSelectedIndexChanged="cmbPuestos_SelectedIndexChanged"
                                                ValidationGroup="ValcmbPuestos">
                                                <HeaderTemplate>
                                                    <table>
                                                        <tr>
                                                            <td><asp:Label ID="lblCL_PUESTO" Text="Clave" runat="server" Width="150px"></asp:Label></td>
                                                            <td><asp:Label ID="lblNB_PUESTO" Text="Nombre" runat="server" Width="150px"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td><asp:Label ID="lblCL_PUESTO" Text='<%# DataBinder.Eval(Container.DataItem, "CL_PUESTO") %>' runat="server" Width="150px"></asp:Label></td>
                                                            <td><asp:Label ID="lblNB_PUESTO" Text='<%# DataBinder.Eval(Container.DataItem, "NB_PUESTO") %>' runat="server" Width="150px"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator CausesValidation="True" Display="Dynamic" CssClass="validacion" ID="ReqcmbPuestos" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="cmbPuestos" ErrorMessage="Campo Obligatorio" ValidationGroup="ValidacionPage"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                </td>


                                <td>
                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <label id="Label2" name="lblNbIdioma" runat="server">Clave del puesto:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox ID="txtClPuesto" runat="server" Width="200px" MaxLength="1000" ReadOnly="true"></telerik:RadTextBox>
                                            <asp:RequiredFieldValidator CausesValidation="True" Display="Dynamic" CssClass="validacion" ID="RFtxtClPuesto" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtClPuesto" ErrorMessage="Campo Obligatorio" validationgroup="InformacionArea"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </td>

                            </tr>
                          
                             <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <label id="Label4" name="lblNbIdioma" runat="server">Sueldo actual:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox ID="txtSueldo" runat="server" Width="200px" MaxLength="1000" Type="Currency"></telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="ValidacionPage" CausesValidation="True" Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator1" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtSueldo" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <label id="Label5" name="lblNbIdioma" runat="server">Fecha en la que se requiere:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadDatePicker ID="Fe_Requerimiento"   Width="200px" ClientEvents-OnDateSelected="OnDateSelectedFe_requerimiento" runat="server" >
                                                <Calendar ID="Calendar1" runat="server"	 >
                                                </Calendar>
                                            </telerik:RadDatePicker>

                                            <asp:RequiredFieldValidator CausesValidation="True" Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator6" ValidationGroup="ValidacionPage" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="Fe_Requerimiento" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <label id="Label3" name="lblNbIdioma" runat="server">Área:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox ID="txtArea" runat="server" Width="200px" MaxLength="1000" ReadOnly="true"></telerik:RadTextBox>
                                            <asp:RequiredFieldValidator CausesValidation="True" Display="Dynamic" validationgroup="InformacionArea" CssClass="validacion" ID="RFtxtArea" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtArea" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </td>

                                <td>

                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <label id="Label7" name="lblNbIdioma" runat="server"></label>
                                        </div>
                                        <div class="divControlDerecha">
                                        <telerik:RadButton  validationgroup="btnNotificarRH" ID="btnNotificarRH" runat="server" Width="150" Text="Notificar a RRHH" OnClientClicked="OpenNotificateWindow" AutoPostBack="false"></telerik:RadButton>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>



                    </ContentTemplate>
                </telerik:RadPanelItem>




                <telerik:RadPanelItem Text="Causa de la vacante" Expanded="false">

                    <ContentTemplate>
                        <br />

                        <div style="clear: both;" />
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="Label6" name="lblNbIdioma" runat="server">Causas:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadComboBox
                                    Filter="Contains"
                                    runat="server"
                                    ID="cmbCausas"
                                    Width="200"
                                    MarkFirstMatch="true"
                                    AutoPostBack="true"
                                    EmptyMessage="Seleccione..."
                                    DropDownWidth="330"
                                    ValidationGroup="ValcmbCausas"
                                    OnSelectedIndexChanged="cmbCausas_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <table>
                                            <tr>
                                                <td><asp:Label ID="lblCL_CATALOGO_VALOR" Text="Clave" runat="server" Width="200px"></asp:Label></td>
                                                <td><asp:Label ID="lblNB_CATALOGO_VALOR" Text="Nombre" runat="server" Width="200px"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td><asp:Label ID="lblCL_CATALOGO_VALOR" Text='<%# DataBinder.Eval(Container.DataItem, "CL_CATALOGO_VALOR") %>' runat="server" Width="150px"></asp:Label></td>
                                                <td><asp:Label ID="lblNB_CATALOGO_VALOR" Text='<%# DataBinder.Eval(Container.DataItem, "NB_CATALOGO_VALOR") %>' runat="server" Width="150px"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator CausesValidation="True" Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator4" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="cmbCausas" ErrorMessage="Campo Obligatorio" ValidationGroup="ValidacionPage"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="Label14" name="lblNbIdioma" runat="server">Especifíque:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtEspecifique" runat="server" Width="250px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>

                    </ContentTemplate>
                </telerik:RadPanelItem>


                <telerik:RadPanelItem Text="Autorizaciones" Expanded="false">
                    <ContentTemplate>
                        <br />

                        <table>
                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <label id="Label15" name="lblNbIdioma" runat="server">Solicitado por:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox ID="txtSolicitado" runat="server" Width="300px" MaxLength="1000"></telerik:RadTextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator8" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtSolicitado" ErrorMessage="Campo Obligatorio" ValidationGroup="ValidacionPage"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </td>
                            </tr>



                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <label id="Label8" name="lblNbIdioma" runat="server">Autoriza:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadComboBox
                                                Filter="Contains"
                                                runat="server"
                                                ID="cmbAutoriza"
                                                Width="300"
                                                MarkFirstMatch="true"
                                                AutoPostBack="false"
                                                EmptyMessage="Seleccione..."
                                                DropDownWidth="340"
                                                ValidationGroup="ValcmbAutoriza"
                                                Height="300">
                                                <HeaderTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblNB_PUESTO" Text="Nombre" runat="server" Width="300px"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblNB_PUESTO" Text='<%# DataBinder.Eval(Container.DataItem, "NB_EMPLEADO_COMPLETO") %>' runat="server" Width="300px"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator3" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="cmbAutoriza" ErrorMessage="Campo Obligatorio" ValidationGroup="ValidacionPage"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                </td>


                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton  ValidationGroup="ValidacionPage" ID="btnNotificarAutorizar" runat="server" Width="100" Text="Notificar" OnClick="btnNotificar_Click" AutoPostBack="true"></telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <label id="Label17" name="lblNbIdioma" runat="server">Vo. Bo. de RR HH:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadTextBox ID="txtVistoBueno" runat="server" Width="300" MaxLength="1000"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </td>

                                <td>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ValidationGroup="ValidacionPage" ID="btnAutorizar" runat="server" Width="100" Text="Autorizar" OnClick="btnNotificar_Click" AutoPostBack="false"></telerik:RadButton>
                                    </div>
                                </td>
                            </tr>

                        </table>
                    </ContentTemplate>
                </telerik:RadPanelItem>


            </Items>

        </telerik:RadPanelBar>

    </div>

    

    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblNbIdioma" name="lblNbIdioma" runat="server">Número de requisición:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtNo_requisicion" runat="server" Width="150px" MaxLength="1000"></telerik:RadTextBox>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator2" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtNo_requisicion" ErrorMessage="Campo Obligatorio" ValidationGroup="ValidacionPage"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="Label1" name="lblNbIdioma" runat="server">Fecha de la requisición:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadDatePicker ID="Fe_solicitud" Width="150px" runat="server" ClientEvents-OnDateSelected="OnDateSelectedFe_solicitud"></telerik:RadDatePicker>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RFFe_solicitud" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="Fe_solicitud" ErrorMessage="Campo Obligatorio" ValidationGroup="ValidacionPage"></asp:RequiredFieldValidator>
        </div>
    </div>


    <div style="clear: both; height: 10px;"></div>
     <div class="divControlDerecha">
    <div class="ctrlBasico">
        <telerik:RadButton ValidationGroup="ValidacionPage" ID="btnGuardarCatalogo" runat="server" Width="100" Text="Guardar" OnClick="btnSave_click" AutoPostBack="true"></telerik:RadButton>
    </div>
    
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnCancelarCatalogo" runat="server" Width="100" Text="Cancelar" AutoPostBack="false" OnClientClicking="closeWindow"></telerik:RadButton>
    </div>
    </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
