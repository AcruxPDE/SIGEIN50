<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="Grupos.aspx.cs" Inherits="SIGE.WebApp.Administracion.Grupos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function OpenGruposWindows() {
            var vURl = "VentanaGrupos.aspx";
            var vTitulo = "Agregar grupo";
            var vWindowsPropierties = {
                width: document.documentElement.clientWidth - 500,
                height: document.documentElement.clientHeight - 20
            };

            openChildDialog(vURl, "winGrupos", vTitulo, vWindowsPropierties);
        }

        function OpenEditGruposWindows() {
            var masterTable = $find("<%= rgGrupos.ClientID %>").get_masterTableView();
            var vSelectedItems = masterTable.get_selectedItems()[0];
            if (vSelectedItems != undefined) {
                var vIdGrupo = vSelectedItems.getDataKeyValue("ID_GRUPO");
                var vURl = "VentanaGrupos.aspx?pIdGrupo=" + vIdGrupo;
                var vTitulo = "Editar grupo";
                var vWindowsPropierties = {
                    width: document.documentElement.clientWidth - 500,
                    height: document.documentElement.clientHeight - 20
                };

                openChildDialog(vURl, "winGrupos", vTitulo, vWindowsPropierties);
            }
            else
                radalert("Selecciona un grupo.", 400, 150);
        }


        function OpenConfirmEliminar(sender, args) {
            var masterTable = $find("<%= rgGrupos.ClientID %>").get_masterTableView();
            var selectedItem = masterTable.get_selectedItems()[0];
            if (selectedItem != undefined) {
                var vNombre = masterTable.getCellByColumnUniqueName(selectedItem, "NB_GRUPO").innerHTML;
                confirmAction(sender, args, "¿Deseas eliminar el grupo " + vNombre + "?, este proceso no podrá revertirse");
            }
            else {
                radalert("Selecciona un grupo.", 400, 150);
            }
        }


        function useDataFromChild(pDato) {
            $find('<%= rgGrupos.ClientID %>').get_masterTableView().rebind();;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxLoadingPanel ID="rlpConfiguracion" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnAgregarGrupo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgGrupos" UpdatePanelHeight="100%" LoadingPanelID="rlpConfiguracion" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgGrupos" UpdatePanelHeight="100%" LoadingPanelID="rlpConfiguracion" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgGrupos" UpdatePanelHeight="100%" LoadingPanelID="rlpConfiguracion" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="clear: both;"></div>
    <div style="height: calc(100% - 60px); width: 100%;">
        <telerik:RadGrid
            ID="rgGrupos"
            runat="server"
            Width="900"
            Height="100%"
            AllowPaging="true"
            AutoGenerateColumns="false"
            HeaderStyle-Font-Bold="true"
            EnableHeaderContextMenu="true"
            AllowMultiRowSelection="false"
            OnNeedDataSource="rgGrupos_NeedDataSource"
            OnItemDataBound="rgGrupos_ItemDataBound">
            <GroupingSettings CaseSensitive="False" />
            <ClientSettings>
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView DataKeyNames="ID_GRUPO" ClientDataKeyNames="ID_GRUPO" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                <Columns>
                    <telerik:GridBoundColumn UniqueName="CL_GRUPO" DataField="CL_GRUPO" HeaderText="Clave" AutoPostBackOnFilter="true" HeaderStyle-Width="150" FilterControlWidth="120" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="NB_GRUPO" DataField="NB_GRUPO" HeaderText="Grupo" AutoPostBackOnFilter="true" HeaderStyle-Width="250" FilterControlWidth="220" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="CL_USUARIO_MODIFICA" DataField="CL_USUARIO_MODIFICA" HeaderText="Último usuario que modifica" AutoPostBackOnFilter="true" HeaderStyle-Width="100" FilterControlWidth="50" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="FE_MODIFICACION" DataField="FE_MODIFICACION" HeaderText="Última fecha de modificación" AutoPostBackOnFilter="true" HeaderStyle-Width="100" FilterControlWidth="50" CurrentFilterFunction="EqualTo" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="height: 10px; clear: both;"></div>
    <telerik:RadButton ID="btnAgregarGrupo" runat="server" Text="Agregar" Width="100" AutoPostBack="false" OnClientClicked="OpenGruposWindows"></telerik:RadButton>
    <telerik:RadButton ID="btnEditar" runat="server" Text="Editar" Width="100" AutoPostBack="false" OnClientClicked="OpenEditGruposWindows"></telerik:RadButton>
    <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" Width="100" AutoPostBack="true" OnClientClicking="OpenConfirmEliminar" OnClick="btnEliminar_Click"></telerik:RadButton>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="winGrupos" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
