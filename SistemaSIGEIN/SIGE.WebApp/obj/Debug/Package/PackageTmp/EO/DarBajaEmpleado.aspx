<%@ Page Title="" Language="C#" MasterPageFile="~/EO/MenuEO.master" AutoEventWireup="true" CodeBehind="DarBajaEmpleado.aspx.cs" Inherits="SIGE.WebApp.EO.DarBajaEmpleado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

      <script id="modal" type="text/javascript">

          var idEmpleado = "";

          function obtenerIdFila() {
              var grid = $find("<%=rgEmpleadosBaja.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    console.info(SelectDataItem);
                    idEmpleado = SelectDataItem.getDataKeyValue("M_EMPLEADO_ID_EMPLEADO");
                }
          }

          function OpenWindow(pWindowProperties) {
              openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
          }

          function GetWindowProperties() {
              return {
                  width: document.documentElement.clientWidth - 700,
                  height: document.documentElement.clientHeight - 15
              };
          }

          function OpenWindowDarBaja() {
              obtenerIdFila();
              if (idEmpleado != "")
                  OpenWindow(GetDarBajaWindowProperties(idEmpleado));
              else
                  radalert("Selecciona un empleado.", 400, 150);
          }

          function GetDarBajaWindowProperties(pIdEmpleado) {
              var wnd = GetWindowProperties();
              wnd.vTitulo = "Dar de baja a un empleado";
              wnd.vURL = "VentanaDarBajaEmpleado.aspx?ID=" + pIdEmpleado;
              wnd.vRadWindowId = "WinDarBaja";
              return wnd;
          }

          function CloseWindow() {
              $find("<%= rgEmpleadosBaja.ClientID %>").get_masterTableView().rebind();
          }
          
      </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <telerik:RadAjaxLoadingPanel ID="ralpBajas" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramBajas" runat="server" DefaultLoadingPanelID="ralpBajas">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgEmpleadosBaja">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgEmpleadosBaja" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        </telerik:RadAjaxManager>
     <label class="labelTitulo">Dar de baja un empleado</label>
    <div style="height: calc(100% - 100px);">
     <telerik:RadGrid ID="rgEmpleadosBaja" runat="server" HeaderStyle-Font-Bold="true" OnNeedDataSource="rgEmpleadosBaja_NeedDataSource" AutoGenerateColumns="false" Height="100%" OnItemDataBound="rgEmpleadosBaja_ItemDataBound">
                            <ClientSettings>
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <PagerStyle AlwaysVisible="true" />
                            <GroupingSettings CaseSensitive="false" />
                             <MasterTableView DataKeyNames="M_EMPLEADO_ID_EMPLEADO" ClientDataKeyNames="M_EMPLEADO_ID_EMPLEADO"  AllowFilteringByColumn="true" AllowSorting="true" AllowPaging="true">
                                <Columns>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="80" FilterControlWidth="80" HeaderText="No. de Empleado" DataField="M_EMPLEADO_CL_EMPLEADO" UniqueName="M_EMPLEADO_CL_EMPLEADO">
                                        <HeaderStyle Font-Bold="true" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150"  FilterControlWidth="120"  HeaderText="Nombre completo" DataField="M_EMPLEADO_NB_EMPLEADO_COMPLETO" UniqueName="M_EMPLEADO_NB_EMPLEADO_COMPLETO">
                                        <HeaderStyle Font-Bold="true" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="80" FilterControlWidth="80" HeaderText="Clave puesto" DataField="M_PUESTO_CL_PUESTO" UniqueName="M_PUESTO_CL_PUESTO">
                                        <HeaderStyle Font-Bold="true" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="120" HeaderText="Puesto" DataField="M_PUESTO_NB_PUESTO" UniqueName="M_PUESTO_NB_PUESTO">
                                        <HeaderStyle Font-Bold="true" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="80" FilterControlWidth="80" HeaderText="Área/Departamento" DataField="M_DEPARTAMENTO_NB_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_NB_DEPARTAMENTO">
                                        <HeaderStyle Font-Bold="true" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="80" FilterControlWidth="80" HeaderText="Empresa" DataField="C_EMPRESA_NB_EMPRESA" UniqueName="C_EMPRESA_NB_EMPRESA">
                                        <HeaderStyle Font-Bold="true" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
     <div style="height: 10px;"></div>
     <div class="ctrlBasico">
             <telerik:RadButton ID="btnBaja" runat="server" Text="Dar de baja" OnClientClicking="OpenWindowDarBaja"  AutoPostBack="false"></telerik:RadButton>         
         </div>
     <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup" >
         <Windows>
              <telerik:RadWindow ID="WinDarBaja" runat="server" Width="1050px" Height="580px" VisibleStatusbar="false" ShowContentDuringLoad="false" OnClientClose="CloseWindow" Behaviors="Close" Modal="true" Animation="Fade"> </telerik:RadWindow>
         </Windows>
         <Windows>
              <telerik:RadWindow ID="WinSeleccionCausa" runat="server" Width="1050px" Height="580px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade">
            </telerik:RadWindow>
         </Windows>
     </telerik:RadWindowManager>
</asp:Content>
