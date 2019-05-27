<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaConfiguraFechaEnvioReplicas.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaConfiguraFechaEnvioReplicas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
 <script>
     function closeWindow() {
         GetRadWindow().close();
     }

     function OpenSelectionWindows(pURL, pVentana, pTitle) {
         var currentWnd = GetRadWindow();
         var browserWnd = window;
         if (currentWnd)
             browserWnd = currentWnd.BrowserWindow;
         var WindowsProperties = {
             width: browserWnd.innerWidth - 20,
             height: browserWnd.innerHeight - 20
         };
         openChildDialog(pURL, pVentana, pTitle, WindowsProperties);
     }

     function OpenSendMessage(pIdPeriodo) {
             if (pIdPeriodo != null) {
                 OpenSelectionWindows("../EO/VentanaSolicitudesReplicas.aspx?PeriodoId=" + pIdPeriodo, "WinEnvioSolicitudReplica", "Envío de solicitudes");
             }
     }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 10px);">
        <telerik:RadSplitter runat="server" ID="rsAyuda" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpDatos" runat="server">
                <div style="clear: both; height: 10px;"></div>
                <div style="height: calc(100% - 60px); width: 100%;">
                    <telerik:RadGrid runat="server" HeaderStyle-Font-Bold="true" ID="rgFechas" AutoGenerateColumns="false" OnNeedDataSource="rgFechas_NeedDataSource" Height="100%" Width="100%" AllowSorting="true" AllowMultiRowSelection="true">
                       <GroupingSettings CaseSensitive="false" />
                         <ClientSettings EnablePostBackOnRowClick="false">
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <MasterTableView ShowHeadersWhenNoRecords="true" DataKeyNames="ID_PERIODO">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="NB_PERIODO" DataField="NB_PERIODO" HeaderText="Periodo" HeaderStyle-Width="250"></telerik:GridBoundColumn>
                           <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" UniqueName="rdtFechaEnvio">
                           <ItemTemplate>
                           <telerik:RadDatePicker ID="rdtFeEnvio" AutoPostBack="false" runat="server" Width="150px" DateInput-DateFormat="dd/MM/yyyy" DateInput-EmptyMessage="dd/MM/yyyy"  MinDate="<%# DateTime.Today.Date %>">
                           </telerik:RadDatePicker>
                           </ItemTemplate>
                   </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="clear: both; height: 15px;"></div>
                <div class="divControlDerecha">
                    <telerik:RadButton runat="server" ID="btnGuardar" Text="Asignar fechas" AutoPostBack="true" OnClick="btnGuardar_Click"></telerik:RadButton>
                    <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
                </div>
            </telerik:RadPane>
            <telerik:RadPane ID="rpAyuda" runat="server" Width="30">
                <telerik:RadSlidingZone ID="rszAyuda" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                    <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Ayuda" Width="200" MinWidth="200" Height="100%">
                        <div style="padding: 10px; text-align: justify;">
                        Este proceso asigna fechas de envío de solicitudes correspondientes a los períodos réplica y al periodo original.<br /><br />
                        No puedes asignar fechas previas al día de hoy.
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>


