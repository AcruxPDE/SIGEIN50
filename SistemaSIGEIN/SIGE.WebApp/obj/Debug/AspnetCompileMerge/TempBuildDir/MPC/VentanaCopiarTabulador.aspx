<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="VentanaCopiarTabulador.aspx.cs" Inherits="SIGE.WebApp.MPC.VentanaCopiarTabulador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //function closeWindow() {
            //    GetRadWindow().close();
            //}


            function closeWindow() {
                var pDatos = [{
                    accion: "ACTUALIZARLISTA"

                }];
                cerrarVentana(pDatos);
            }

            function cerrarVentana(recargarList) {
                sendDataToParent(recargarList);
            }

            </script>
         </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="clear:both; height:10px;"> </div>
    <div style="height:calc(100%-60px); clear:both;">
<div class="ctrlBasico">
        <div class="divControlIzquierda" style="width:150px">
            <label id="lbCrear" 
                name="lbCrear"
                runat="server">Crear a partir de:</label>
        </div>
        <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtCrearAPartir"  
                    runat="server" 
                    Width="180px" 
                    MaxLength="800"
                    Enabled="false">
                </telerik:RadTextBox>
        </div>
    </div>
    <div style="clear:both";> </div>
<div class="ctrlBasico">
        <div class="divControlIzquierda" style="width:150px">
            <label id="lbVersionTabuladorCopiar" 
                name="lbVersionTabuladorCopiar" 
                runat="server">Versión de tabulador:</label>
        </div>
        <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtVersionTabuladorCopiar"  
                    runat="server" 
                    Width="180px" 
                    MaxLength="800">
                </telerik:RadTextBox>
             <asp:RequiredFieldValidator      
                 Display="Dynamic"  
                 CssClass="validacion"    
                 ID="RequiredFieldValidator2" 
                 runat="server"  
                 Font-Names="Arial" 
                 Font-Size="Small" 
                 ControlToValidate="txtVersionTabuladorCopiar" 
                 ErrorMessage="El campo es obligatorio">
             </asp:RequiredFieldValidator>
        </div>
    </div>
    <div style="clear:both;"> </div>
<div class="ctrlBasico">
        <div class="divControlIzquierda" style="width:150px">
            <label id="lbNombre" 
                name="lbNombre" 
                runat="server">Descripción:</label>
        </div>
    <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtNombreTabulador"  
                    runat="server" 
                    Width="180px" 
                    >
                </telerik:RadTextBox>
        <asp:RequiredFieldValidator      
                 Display="Dynamic"  
                 CssClass="validacion"    
                 ID="RequiredFieldValidator3" 
                 runat="server"  
                 Font-Names="Arial" 
                 Font-Size="Small" 
                 ControlToValidate="txtNombreTabulador" 
                 ErrorMessage="El campo es obligatorio">
             </asp:RequiredFieldValidator>
        </div>
    </div>
    <div style="clear:both;"> </div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda" style="width:150px">
            <label id="lbDescripcionCopiar" 
                name="lbDescripcionCopiar" 
                runat="server">Notas:</label>
        </div>
        <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtDescripcion"  
                    runat="server" 
                    Width="290px" 
                    MaxLength="800">
                </telerik:RadTextBox>
            <%-- <asp:RequiredFieldValidator      
                 Display="Dynamic"  
                 CssClass="validacion"    
                 ID="RequiredFieldValidator1" 
                 runat="server"  
                 Font-Names="Arial" 
                 Font-Size="Small" 
                 ControlToValidate="txtDescripcion" 
                 ErrorMessage="El campo es obligatorio">
             </asp:RequiredFieldValidator>--%>
        </div>
    </div>
    <div style="clear:both;"></div>
     <div class="ctrlBasico">
         <div class="divControlIzquierda" style="width:150px">
          <label id="lbFechaCreacion" 
                name="lbFechaCreacion" 
                runat="server">Fecha de Creacion:</label>
             </div>
         <div class="divControlDerecha">
                 <telerik:RadDatePicker ID="rdpCreacion"  Enabled="true" runat="server"  Width="140px" DateInput-DateFormat="dd-MM-yyyy" DateInput-DateInput-DisplayDateFormat="dd-MM-yyyy">
                        </telerik:RadDatePicker>
             </div>
    </div>
    <div style="clear:both;"> </div>
   <div class="ctrlBasico">
       <div class="divControlIzquierda" style="width:150px">
        <label id="lbVigencia" 
                name="lbVigencia" 
                runat="server">Vigencia:</label>
           </div>
       <div class="divControlDerecha">
             <telerik:RadDatePicker ID="rdpVigencia"  runat="server" Width="140px" DateInput-DateFormat="dd-MM-yyyy" DateInput-DateInput-DisplayDateFormat="dd-MM-yyyy">
             <Calendar Height="100px" runat="server" CalendarTableStyle-BorderColor="YellowGreen" FastNavigationStyle-HorizontalAlign="Left" CalendarTableStyle-HorizontalAlign="Left"></Calendar>           
             </telerik:RadDatePicker>
                 <asp:RequiredFieldValidator  
                 Display="Dynamic"  
                 CssClass="validacion"    
                 ID="RequiredFieldValidator" 
                 runat="server"  
                 Font-Names="Arial" 
                 Font-Size="Small" 
                 ControlToValidate="rdpVigencia" 
                 ErrorMessage="El campo es obligatorio"></asp:RequiredFieldValidator>
           </div>
       </div>
        </div>
      <div style="clear:both;">
        <div class="divControlesBoton">    
            <telerik:RadButton ID="btnGuardarCopiar"
                 runat="server" 
                Width="100px" 
                Text="Guardar"  
                AutoPostBack="true" 
                OnClick="btnGuardarCopiar_Click"
                ></telerik:RadButton>
            <telerik:RadButton ID="btnCancelarCopiar" 
                runat="server" 
                Width="100px" 
                Text="Cancelar" 
                AutoPostBack="true" 
                OnClientClicking="closeWindow"></telerik:RadButton>
        </div>
    </div>
     <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
