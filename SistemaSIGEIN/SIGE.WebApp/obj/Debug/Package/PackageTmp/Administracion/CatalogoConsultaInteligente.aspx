<%@ Page Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="CatalogoConsultaInteligente.aspx.cs" Inherits="SIGE.WebApp.Administracion.CatalogoConsultaInteligente" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            var vIdCubo = '';
            var vIArchivo = '';
            function MostrarPopUpInsertaCVI() {
                vIdCubo = '';
                vIArchivo = '';
                AbrirPopUp(ObtenerPropiedadesPopUp());
            }
            function MostrarPopUpModificaCVI() {
                obtenerFila();
                AbrirPopUp(ObtenerPropiedadesPopUp());
            }
            function obtenerFila(fila) {
                var indice = 0;
                var selectedItem;
                if (fila != null) {
                    indice = fila[0].arguments[0]._element.parentNode.parentNode.rowIndex - 1;
                    if (indice == null)
                        indice = fila[0].arguments[0]._element.parentNode.parentNode.parentNode.rowIndex - 1;
                    selectedItem = $find("<%=rdgConsultasInteligentes.ClientID %>").get_masterTableView().get_dataItems()[indice];
                } else
                    selectedItem = $find("<%=rdgConsultasInteligentes.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined) {
                    vIdCubo = selectedItem.getDataKeyValue("ID_CUBO");
                    vIArchivo = selectedItem.getDataKeyValue("ID_ARCHIVO");
                    document.getElementById("<%:hdnId.ClientID%>").value = vIArchivo;
                }
                else {
                    vIdEmpleado = "";
                    vNbEmpleado = "";
                    vClEstatus = "";
                }
            }
            function AbrirPopUp(PropiedadesPopUp) {
                openChildDialog(PropiedadesPopUp.vURL, PropiedadesPopUp.vRadWindowId, PropiedadesPopUp.vTitulo, PropiedadesPopUp);
            }
            function ObtenerPropiedadesPopUp() {
                var wnd = ObtenerPropiedadesVentana();
                wnd.vTitulo = "Agregar vista inteligente";
                wnd.vURL = "VentanaConsultaInteligente.aspx";
                wnd.vRadWindowId = "rwPopUpVistaInteligente";

                if (vIdCubo != "") {
                    wnd.vURL = wnd.vURL + "?idCubo=" + vIdCubo;
                    wnd.vTitulo = "Editar vista inteligente";
                }

                return wnd;
            }
            function ObtenerPropiedadesVentana() {
                return {
                    width: 700,//750,
                    height:  250//15
                };
            }
            function onClose() {
                if ($find("<%=rdgConsultasInteligentes.ClientID%>").get_masterTableView() != null)
                    $find("<%=rdgConsultasInteligentes.ClientID%>").get_masterTableView().rebind();
            }
            function HabilitaBotones() {
                var btnModificar = $find("<%=btnEditar.ClientID%>");
                var btnEliminar = $find("<%=btnEliminar.ClientID%>");

                if('<%= vEditar%>' == "True")
                    btnModificar.set_enabled(true);

                if ('<%= vEliminar%>' == "True")
                btnEliminar.set_enabled(true);
            }
            function Eliminar() {
                obtenerFila()
                if (vIArchivo != null)
                    document.getElementById("<%:hdnId.ClientID%>").value = vIArchivo;
            }
            function closeWindow() {
                if (GetRadWindow() != null)
                    GetRadWindow().close();
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">            
    <asp:HiddenField runat="server" ID="hdnId"/>
    <label class="labelTitulo">Consultas Inteligentes</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadGrid ID="rdgConsultasInteligentes" runat="server"
            AutoGenerateColumns="false"
            EnableHeaderContextMenu="true"
            ShowGroupPanel="true"
            AllowSorting="true"
            OnNeedDataSource="rdgConsultasInteligentes_NeedDataSource" HeaderStyle-Font-Bold="true">
            <ClientSettings>
                <ClientEvents OnRowClick="HabilitaBotones"  />
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
            <ExportSettings ExportOnlyData="true" FileName="InventarioDePersonal" Excel-Format="Xlsx" IgnorePaging="true"></ExportSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView ClientDataKeyNames="ID_CUBO,ID_ARCHIVO,ID_ITEM">
                <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="200" HeaderText="Nombre vista inteligente" DataField="NB_CATALOGO" HeaderStyle-Font-Bold="true" ></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="200" HeaderText="Archivo" DataField="NB_ARCHIVO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>                    
                    <telerik:GridTemplateColumn HeaderStyle-Width="20">
                        <ItemTemplate>
                            <telerik:RadImageButton ID="imgBtnArchivo" OnClientClicked="function () { obtenerFila(this); }" OnClick="imgBtnArchivo_Click" ToolTip="Mostrar tablero"  Height="20" runat="server" Image-Url="~/Assets/images/StatusPDE.png"></telerik:RadImageButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="clear: both; height: 10px;"></div>        
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnAgregar" runat="server" Text="Agregar" OnClientClicked="MostrarPopUpInsertaCVI" AutoPostBack="false" ></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditar" runat="server" Enabled="false" Text="Editar" OnClientClicked="MostrarPopUpModificaCVI"  AutoPostBack="false"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminar" runat="server" Enabled="false" Text="Eliminar" OnClientClicked="Eliminar" OnClick="btnEliminar_Click" ></telerik:RadButton>
    </div>    
    <telerik:RadWindowManager ID="rdwmPopUps" runat="server">
        <Windows>
            <telerik:RadWindow ID="rwPopUpVistaInteligente" runat="server" Modal="true" VisibleStatusbar="false" Behaviors="Close" OnClientClose="onClose"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
</asp:Content>