﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="SIGE.WebApp.PDE.Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function ShowInsertForm() {
                OpenWindow(null);
                return false;
            }

            function ShowEditForm() {
                var selectedItem = $find("<%=grdUsuarios.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined)
                    OpenWindow(selectedItem.getDataKeyValue("CL_USUARIO"), selectedItem.getDataKeyValue("FG_ACTIVO"));
                else
                    radalert("Selecciona un usuario.", 400, 150);
            }


            function OpenWindow(pClUsuario, pActivo) {
               
                var vTitulo = "Agregar Usuario";
                var vEditar = "false";
                var vURL = "VentanaUsuarios.aspx?Editar=" + vEditar;
                if (pClUsuario != null) {
                    vEditar = "true";
                    vURL = "VentanaUsuarios.aspx?UsuarioId=" + pClUsuario + "&Activo=" + pActivo + "&Editar=" + vEditar;
                    vTitulo = "Editar Usuario"
                   
                }
                var oWin = window.radopen(vURL, "winUsuarios2");
                oWin.set_title(vTitulo);
               
            }

            function onCloseWindow(oWnd, args) {
                $find("<%= grdUsuarios.ClientID%>").get_masterTableView().rebind();
            }

            function confirmarEliminar(sender, args) {
                var masterTable = $find("<%= grdUsuarios.ClientID %>").get_masterTableView();
                var selectedItem = masterTable.get_selectedItems()[0];
                if (selectedItem != undefined) {
                    var vNombre = masterTable.getCellByColumnUniqueName(selectedItem, "CL_USUARIO").innerHTML;
                    confirmar(sender, args, "¿Deseas eliminar el usuario " + vNombre + "?, este proceso no podrá revertirse");
                }
                else {
                    radalert("Selecciona un usuario.", 400, 150);
                    args.set_cancel(true);
                }
            }

            function confirmar(sender, args, text) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });

                radconfirm(text, callBackFunction, 400, 150, null, "Confirmar");
                //always prevent the original postback so the RadConfirm can work, it will be initiated again with code in the callback function
                args.set_cancel(true);
            }

            function useDataFromChild(pEmpleados) {
                $find("<%= grdUsuarios.ClientID%>").get_masterTableView().rebind();
            }

          
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
      <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdUsuarios" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rwmAlertas" />
                    <telerik:AjaxUpdatedControl ControlID="grdUsuarios" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <label class="labelTitulo">Usuarios</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadGrid ID="grdUsuarios" runat="server" OnNeedDataSource="grdUsuarios_NeedDataSource" 
            Height="100%"   AutoGenerateColumns="false" ShowGroupPanel="True" AllowPaging="true"
             AllowSorting="true" HeaderStyle-Font-Bold="true">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView ClientDataKeyNames="CL_USUARIO, FG_ACTIVO,FG_ENVIO" DataKeyNames="CL_USUARIO, FG_ACTIVO,FG_ENVIO" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Usuario" DataField="CL_USUARIO" UniqueName="CL_USUARIO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="160" FilterControlWidth="120" HeaderText="Nombre" DataField="NB_USUARIO" UniqueName="NB_USUARIO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  HeaderStyle-Width="170" FilterControlWidth="100" HeaderText="Correo electrónico" DataField="NB_CORREO_ELECTRONICO" UniqueName="NB_CORREO_ELECTRONICO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  HeaderStyle-Width="150" FilterControlWidth="100" HeaderText="Área" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="60" HeaderStyle-Width="120"  HeaderText="Rol" DataField="NB_ROL" UniqueName="NB_ROL"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="20" HeaderStyle-Width="70 " HeaderText="Activo" DataField="FG_ACTIVO" UniqueName="FG_ACTIVO"></telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true"  HeaderText="Fecha de envío " DataField="FE_ENVIO" UniqueName="FE_ENVIO" HeaderStyle-Width="100" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridDateTimeColumn>
                    <telerik:GridTemplateColumn HeaderText="Correo enviado" DataField="FG_ENVIO" UniqueName="FG_ENVIO" AllowFiltering="false">
                                       <HeaderStyle Width="100" />
                                            <ItemTemplate>
                                                <div style="width: 80%; text-align: center;">
                                                    <img src='<%# Eval("FG_ENVIO").ToString().Equals("True") ? "../Assets/images/Aceptar.png" : "../Assets/images/Cancelar.png"  %>' />
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnAgregar" runat="server" name="btnAgregar" AutoPostBack="false" Text="Agregar" OnClientClicked="ShowInsertForm" Width="100"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditar" runat="server" name="btnEditar" AutoPostBack="false" Text="Editar" OnClientClicked="ShowEditForm" Width="100"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminar" runat="server" name="btnEliminar" AutoPostBack="true" Text="Eliminar" OnClientClicking="confirmarEliminar" Width="100" OnClick="btnEliminar_Click"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow ID="winSeleccionEmpleados" runat="server" Title="Seleccionar empleado" Height="500px" Width="1000px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            
            <telerik:RadWindow ID="winUsuarios2" runat="server" Title="Agregar/Editar Usuarios" Height="500px" Width="1000px"  ReloadOnShow="true" VisibleStatusbar="false" 
                ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="onCloseWindow" ></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>