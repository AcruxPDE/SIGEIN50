<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="EnvioCorreosPruebas.aspx.cs" Inherits="SIGE.WebApp.IDP.EnvioCorreosPruebas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function closeWindow() {
            GetRadWindow().close();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 60px); width: 100%;">
                                <telerik:RadGrid runat="server" 
                                    ID="rgCandidatosBateria" 
                                    HeaderStyle-Font-Bold="true" 
                                    AutoGenerateColumns="false" 
                                    OnItemDataBound="rgCandidatosBateria_ItemDataBound" 
                                    OnNeedDataSource="rgCandidatosBateria_NeedDataSource" 
                                    Height="100%" 
                                    Width="100%" 
                                    AllowSorting="true" 
                                    AllowMultiRowSelection="true">
                                    <ClientSettings EnablePostBackOnRowClick="false">
                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView ShowHeadersWhenNoRecords="true" DataKeyNames="ID_BATERIA, CL_TOKEN, FL_BATERIA, ID_CANDIDATO">
                                        <Columns>
                                          <%-- <telerik:GridBoundColumn UniqueName="CL_SOLICITUD" DataField="CL_SOLICITUD" HeaderText="Folio de solicitud" HeaderStyle-Width="120" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn UniqueName="NB_CANDIDATO" DataField="NB_CANDIDATO" HeaderText="Nombre completo" HeaderStyle-Width="250" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn UniqueName="CL_CORREO_ELECTRONICO" DataField="CL_CORREO_ELECTRONICO" HeaderText="Correo electrónico" HeaderStyle-Width="300" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox runat="server" ID="txtCorreo" Width="100%" Text='<%# Bind("CL_CORREO_ELECTRONICO") %>' AutoPostBack="false"></telerik:RadTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>                            
                            </div>
    <div style="height:10px; clear:both;"></div>
           <div class="divControlDerecha">
               <div class="ctrlBasico">
            <telerik:RadButton runat="server" Text="Enviar" ID="btnEnviar" OnClick="btnEnviar_Click" AutoPostBack="true" />
                   </div>
               <div class="ctrlBasico">
            <telerik:RadButton runat="server" Text="Cancelar" ID="btnCancelar" AutoPostBack="false" OnClientClicked="closeWindow" />
                   </div>
        </div>
    <telerik:RadWindowManager ID="rwmAlertas"  runat="server" OnClientClose="returnDataToParentPopup"></telerik:RadWindowManager>
</asp:Content>
