<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaPeriodoDesempenoReplica.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaPeriodoDesempenoReplica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function closeWindow() {
            GetRadWindow().close();
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };
            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <telerik:RadAjaxLoadingPanel ID="ralpReplicarPeriodo" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxManager ID="ramReplicaPeriodo" runat="server" DefaultLoadingPanelID="ralpReplicarPeriodo">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnReplicarPeriodo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPeriodosReplica" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="clear: both; height: 10px;"></div>
    <table class="ctrlTableForm">
        <tr>
            <td class="ctrlTableDataContext">
                <label id="lblPeriodo">Período:</label></td>
            <td colspan="2" class="ctrlTableDataBorderContext">
                <div id="txtPeriodo" runat="server"></div>
            </td>
            <td class="ctrlTableDataContext">
                <label id="lblDescripcion">Descripción:</label></td>
            <td colspan="2" class="ctrlTableDataBorderContext">
                <div id="txtDescripcion" runat="server"></div>
            </td>
            <td class="ctrlTableDataContext">
                <label id="lblInicio">Inicio:</label></td>
            <td colspan="2" class="ctrlTableDataBorderContext">
                <div id="txtInicio" runat="server"></div>
            </td>
            <td class="ctrlTableDataContext">
                <label id="lblFin">Término:</label></td>
            <td colspan="2" class="ctrlTableDataBorderContext">
                <div id="txtFin" runat="server"></div>
            </td>

        </tr>
    </table>
    <%-- <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico" style="padding-left: 10px;">
        <label id="Label2" name="lblFeInicio" runat="server">Fecha de inicio:</label>
        <br />
        <div class="divControlDerecha">
            <telerik:RadDatePicker runat="server" ID="feInicio" Width="150" AutoPostBack="false"></telerik:RadDatePicker>
        </div>
    </div>
    <div class="ctrlBasico" style="padding-left: 10px;">
        <label id="Label1" name="lblFeFin" runat="server">Fecha de fin:</label>
        <br />
        <div class="divControlDerecha">
            <telerik:RadDatePicker runat="server" ID="feFin" Width="150" AutoPostBack="false"></telerik:RadDatePicker>
        </div>
    </div>--%>
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico" style="padding-left: 10px;">
        <label id="Label2" name="lblReplicas" runat="server">¿Cuántas veces deseas repetir el mismo periodo?</label>
        <div class="divControlDerecha" style="padding-left: 5px;">
            <telerik:RadNumericTextBox runat="server" ID="txtNumero" Width="50" NumberFormat-DecimalDigits="0" MinValue="1" AutoPostBack="false"></telerik:RadNumericTextBox>
        </div>
    </div>
    <div class="ctrlBasico" style="padding-left: 10px;">
        <telerik:RadButton ID="btnReplicarPeriodo" runat="server" Width="200px" Text="Replicar período" AutoPostBack="true" OnClick="btnReplicarPeriodo_Click"></telerik:RadButton>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div style="height: calc(100%-60px);">
        <telerik:RadGrid ID="grdPeriodosReplica" 
            ShowHeader="true" 
            runat="server" 
            AllowPaging="false"
            AllowSorting="true" Width="100%" Height="350" HeaderStyle-Font-Bold="true"
            OnNeedDataSource="grdPeriodosReplica_NeedDataSource"
            OnDeleteCommand="grdPeriodosReplica_DeleteCommand">
            <ClientSettings EnablePostBackOnRowClick="false">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView ClientDataKeyNames="ID_PERIODO,ID_PERIODO_REPLICA, CL_PERIODO" DataKeyNames="ID_PERIODO,ID_PERIODO_REPLICA, CL_PERIODO" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false"
                 PagerStyle-AlwaysVisible="true" AllowFilteringByColumn="false">
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Período" DataField="CL_PERIODO" UniqueName="CL_PERIODO" HeaderStyle-Width="150" FilterControlWidth="130" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Descripción" DataField="DS_PERIODO" UniqueName="DS_PERIODO" HeaderStyle-Width="250" FilterControlWidth="240" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn AutoPostBackOnFilter="false" HeaderText="Fecha de inicio" UniqueName="FE_INICIO" DataField="FE_INICIO" HeaderStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadDatePicker ID="rdpFechaInicio" runat="server" Width="120" AutoPostBack="false" SelectedDate='<%# Eval("FE_INICIO") %>'>
                            </telerik:RadDatePicker>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn AutoPostBackOnFilter="false" HeaderText="Fecha de fin" UniqueName="FE_TERMINO" DataField="FE_TERMINO" HeaderStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadDatePicker ID="rdpFechaFin" runat="server" Width="120" AutoPostBack="false" SelectedDate='<%# Eval("FE_TERMINO") %>'>
                            </telerik:RadDatePicker>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <%--  <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Fecha de inicio" DataField="FE_INICIO" UniqueName="FE_INICIO" HeaderStyle-Width="120" FilterControlWidth="100" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Fecha de fin" DataField="FE_TERMINO" UniqueName="FE_TERMINO" HeaderStyle-Width="100" FilterControlWidth="80" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>--%>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estatus" DataField="CL_ESTADO_PERIODO" UniqueName="CL_ESTADO_PERIODO" HeaderStyle-Width="100" FilterControlWidth="80" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                    <telerik:GridButtonColumn CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn" ButtonType="ImageButton" HeaderStyle-Width="30" ConfirmTextFields="CL_PERIODO" ConfirmTextFormatString="¿Desea eliminar el período {0}?, este proceso no podrá revertirse." ConfirmDialogWidth="450" ConfirmDialogHeight="175" ConfirmDialogType="RadWindow" />
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="divControlesBoton">
        <telerik:RadButton runat="server" ID="btnAceptar" AutoPostBack="true" Text="Aceptar" OnClick="btnAceptar_Click"></telerik:RadButton>
        <telerik:RadButton runat="server" ID="btnCancelar" AutoPostBack="false" Text="Cancelar" OnClientClicked="closeWindow"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
