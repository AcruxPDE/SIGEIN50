<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaCentroAdministrativo.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaCentroAdministrativo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    
    <script id="modal" type="text/javascript">

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

        function OpenSelectionWindow() {
            openChildDialog("../Comunes/SeleccionLocalizacion/SeleccionEstado.aspx", "winSeleccion", "Selección de Estado")
        }

        function OpenSelectionWindow1() {
            var listEstado = $find("<%=lstEstado.ClientID %>");
            var clEstado = listEstado.get_selectedItem().get_value();
                openChildDialog("../Comunes/SeleccionLocalizacion/SeleccionMunicipio.aspx?CLEstado=" + clEstado, "winSeleccion", "Selección de municipio");
        }

        function OpenSelectionWindow2() {
            var listEstado = $find("<%=lstEstado.ClientID %>");
            var clEstado = listEstado.get_selectedItem().get_value();
            var listMunicipio = $find("<%=lstMunicipio.ClientID %>");
            var clMunicipio = listMunicipio.get_selectedItem().get_value();
            openChildDialog("../Comunes/SeleccionLocalizacion/SeleccionColonia.aspx?CLEstado"+ clEstado + "&ClMunicipio=" + clMunicipio, "winSeleccion", "Selección de Colonia")
        }

   function useDataFromChild(pData) {
            if (pData != null) {
                var vSelectedData = pData[0];
                var list;
                switch (vSelectedData.clTipoCatalogo) {
                    case "ESTADO":
                        list = $find("<%=lstEstado.ClientID %>");
                        listMunicipio = $find("<%=lstMunicipio.ClientID %>");
                        listColonia = $find("<%=lstColonia.ClientID %>");

                        var itemEstado = list.getItem(0);

                        if (vSelectedData.nbDato != itemEstado.get_text()) {
                            var item = listMunicipio.getItem(0);
                            listMunicipio.get_items().remove(item);
                            var item = new Telerik.Web.UI.RadListBoxItem();
                            item.set_text("No Seleccionado");
                            listMunicipio.get_items().add(item)

                            var item = listColonia.getItem(0);
                            listColonia.get_items().remove(item);
                            var item = new Telerik.Web.UI.RadListBoxItem();
                            item.set_text("No Seleccionado");
                            listColonia.get_items().add(item)
                        }

                        break;
                    case "MUNICIPIO":
                        list = $find("<%=lstMunicipio.ClientID %>");
                        listColonia = $find("<%=lstColonia.ClientID %>");

                        var itemMunicipio = list.getItem(0);

                        if (vSelectedData.nbDato != itemMunicipio.get_text()) {
                            var item = listColonia.getItem(0);
                            listColonia.get_items().remove(item);
                            var item = new Telerik.Web.UI.RadListBoxItem();
                            item.set_text("No Seleccionado");
                            listColonia.get_items().add(item)
                            item.select();
                        }

                        break;

                    case "COLONIA":
                        list = $find("<%=lstColonia.ClientID %>");
                        break;
                }

                if (list != undefined) {
                    list.trackChanges();

                    var items = list.get_items();
                    items.clear();

                    var item = new Telerik.Web.UI.RadListBoxItem();
                    item.set_text(vSelectedData.nbDato);
                    item.set_value(vSelectedData.clDato);
                    item.set_selected(true);
                    items.add(item);

                    list.commitChanges();
                }
            }
        }
       
    
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">

       
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cmbEstados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtNBEstado" UpdatePanelHeight="100%"  LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>

    </telerik:RadAjaxManager>


    <div style="clear:both; height:10px;"/> 
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lbClave" 
                name="lbClave" 
                runat="server">Clave:</label>
        </div>
        <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtClave"  
                    runat="server" 
                    Width="100px" 
                    MaxLength="800">
                </telerik:RadTextBox>
             <asp:RequiredFieldValidator      
                 Display="Dynamic"  
                 CssClass="validacion"    
                 ID="RequiredFieldValidator2" 
                 runat="server"  
                 Font-Names="Arial" 
                 Font-Size="Small" 
                 ControlToValidate="txtClave" 
                 ErrorMessage="Campo Obligatorio">
             </asp:RequiredFieldValidator>
        </div>
    </div>

    <div style="clear:both;"/>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblNombre"
                 name="lbNombre" 
                runat="server">Nombre:</label>
        </div>
        <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtNombre"
                     runat="server" 
                    Width="250px" 
                    MaxLength="1000">
                </telerik:RadTextBox>
            <asp:RequiredFieldValidator      
                 Display="Dynamic"  
                 CssClass="validacion"    
                 ID="RequiredFieldValidator1" 
                 runat="server"  
                 Font-Names="Arial" 
                 Font-Size="Small" 
                 ControlToValidate="txtNombre" 
                 ErrorMessage="Campo Obligatorio">
             </asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="ctrlBasico">
    <div class="divControlIzquierda">
            <label id="lbEstado" name="lblEstado" runat="server">Estado:</label>
        </div>
     <div class="divControlDerecha">
                                          
                                                    
            <telerik:RadListBox ID="lstEstado" Width="300" runat ="server" OnClientItemDoubleClicking="OpenSelectionWindow" ValidationGroup="vgEstado"  ></telerik:RadListBox>
            <telerik:RadButton ID="btnBuscarEstado" runat="server" Text="B" OnClientClicked="OpenSelectionWindow" AutoPostBack="false" ValidationGroup="vgEstado"></telerik:RadButton>
      
        </div>
    </div>

     <div class="ctrlBasico">
    <div class="divControlIzquierda">
            <label id="lbMunicipio" name="lbMunicipio" runat="server">Municipio:</label>
        </div>
     <div class="divControlDerecha">
            <telerik:RadListBox ID="lstMunicipio" Width="300" runat="server" OnClientItemDoubleClicking="OpenSelectionWindow1" ValidationGroup="vgMunicipio" ></telerik:RadListBox>
            <telerik:RadButton ID="btnBuscarMunicipio" runat="server" Text="B" OnClientClicked="OpenSelectionWindow1" AutoPostBack="false" ValidationGroup="vgMunicipio"></telerik:RadButton>
      
        </div>
    </div>

     <div class="ctrlBasico">
    <div class="divControlIzquierda">
            <label id="lbColonia" name="lbColonia" runat="server">Colonia:</label>
        </div>
     <div class="divControlDerecha">
            <telerik:RadListBox ID="lstColonia" Width="300" runat="server" OnClientItemDoubleClicking="OpenSelectionWindow2" ValidationGroup="vgColonia" ></telerik:RadListBox>
            <telerik:RadButton ID="btnBuscarColonia" runat="server" Text="B" OnClientClicked="OpenSelectionWindow2" AutoPostBack="false" ValidationGroup="vgColonia"></telerik:RadButton>
       
        </div>
    </div>

    
      <div style="clear:both;"/>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblCalle" 
                name="lblCalle" 
                runat="server">Calle:</label>
        </div>
        <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtCalle" 
                    runat="server" 
                    Width="335px" 
                    MaxLength="1000">
                </telerik:RadTextBox>
            <asp:RequiredFieldValidator      
                 Display="Dynamic"  
                 CssClass="validacion"    
                 ID="RequiredFieldValidator3" 
                 runat="server"  
                 Font-Names="Arial" 
                 Font-Size="Small" 
                 ControlToValidate="txtCalle" 
                 ErrorMessage="Campo Obligatorio">
             </asp:RequiredFieldValidator>
        </div>
    </div>


    <div style="clear:both;"/>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblNoExt" 
                name="lblNoExt"
                 runat="server">No. Exterior:</label>
        </div>
        <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtNoExt" 
                    runat="server" 
                    Width="100px" 
                    MaxLength="1000">
                </telerik:RadTextBox>
            <asp:RequiredFieldValidator      
                 Display="Dynamic"  
                 CssClass="validacion"    
                 ID="RequiredFieldValidator4" 
                 runat="server"  
                 Font-Names="Arial" 
                 Font-Size="Small" 
                 ControlToValidate="txtNoExt" 
                 ErrorMessage="Campo Obligatorio">
             </asp:RequiredFieldValidator>
        </div>
    </div>


     <div style="clear:both;"/>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblNoInt" 
                name="lbNoInt"
                 runat="server">No. Interior:</label>
        </div>
        <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtNoInt" 
                    runat="server"
                     Width="100px" 
                    MaxLength="1000">
                </telerik:RadTextBox>
        </div>
    </div>

  
     <div style="clear:both;"/>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblCP" 
                name="lblCP" 
                runat="server">Código Postal:</label>
        </div>
        
        <div class="divControlDerecha">
                <telerik:RadTextBox 
                    ID="txtCP" 
                    runat="server" 
                    Width="130px" 
                    MaxLength="1000">
                </telerik:RadTextBox>
            <asp:RequiredFieldValidator      
                 Display="Dynamic"  
                 CssClass="validacion"    
                 ID="RequiredFieldValidator8" 
                 runat="server"  
                 Font-Names="Arial" 
                 Font-Size="Small" 
                 ControlToValidate="txtCP" 
                 ErrorMessage="Campo Obligatorio">
             </asp:RequiredFieldValidator>
        </div>
    </div>
     
    <div style="clear:both;">
        <div class="divControlesBoton">    
            <telerik:RadButton ID="btnGuardarCentroAdmvo"
                 runat="server" 
                Width="100px" 
                Text="Guardar"  
                AutoPostBack="true" 
                OnClick="btnGuardarCentroAdmvo_Click"
                ></telerik:RadButton>
                
            <telerik:RadButton ID="btnCancelarCentroAdmvo" 
                runat="server" 
                Width="100px" 
                Text="Cancelar" 
                AutoPostBack="true"  
                OnClientClicking="closeWindow" 
                OnClick="btnCancelarCentroAdmvo_Click" ></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
