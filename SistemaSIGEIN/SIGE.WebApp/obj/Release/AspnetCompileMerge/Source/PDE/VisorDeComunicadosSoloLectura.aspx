<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PDE/MenuPDESoloLectura.Master" CodeBehind="VisorDeComunicadosSoloLectura.aspx.cs" Inherits="SIGE.WebApp.PDE.VisorDeComunicadosSoloLectura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <style>
            .RadListViewContainer {
                border: 1px solid lightgray;
                margin: 6px;
                padding: 2px;
                border-radius: 5px;
            }

            h4 {
            }
        </style>
        <script type="text/javascript">
            var idComunicado = "";
            function obtenerIdFila() {
                var grid = $find("<%=grdComunicados.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idComunicado = SelectDataItem.getDataKeyValue("ID_COMUNICADO");
                }
            }

            function AbrirVentanaComentario() {
                obtenerIdFila();
                if (idComunicado != "") {
                    var oWnd = radopen("VentanaNuevoComentario.aspx?&ID_COMUNICADO=" + idComunicado + "&TIPO=Agregar", "winComentario");
                    oWnd.set_title("Agregar Comentario");
                }
                else {
                    radalert("No has seleccionado un comunicado.", 400, 150, "");
                }
            }

            function onCloseWindow(oWnd, args) {
                var vLista = $find("<%= rlvComentarios.ClientID %>");
                vLista.rebind();
            }

            function AbrirVentanaVideo() {
                var idArchivo;
                var nbArchivo;
                var grid = $find("<%=grdComunicados.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idArchivo = SelectDataItem.getDataKeyValue("ID_ARCHIVO_PDE");
                    nbArchivo = SelectDataItem.getDataKeyValue("NB_COMUNICADO");
                }
                var vURL = "VisorVideos.aspx?IdArchivo=" + idArchivo;
                var Title = nbArchivo;
                var oWin = window.radopen(vURL, "rwVerVideo");
                oWin.set_title(Title);
            }
            function AbrirVentanaArchivo() {
                var idArchivo;
                var nbArchivo;
                var grid = $find("<%=grdComunicados.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idArchivo = SelectDataItem.getDataKeyValue("ID_ARCHIVO_PDE");
                    nbArchivo = SelectDataItem.getDataKeyValue("NB_COMUNICADO");
                }
                var vURL = "VisorDeArchivos.aspx?IdArchivo=" + idArchivo;
                var Title = nbArchivo;
                var oWin = window.radopen(vURL, "rwVerArchivo", document.documentElement.clientWidth - 10, document.documentElement.clientHeight - 10);
                oWin.set_title(Title);
            }
            function AbrirVentanaInformacionEmpleado() {
                var idArchivo;
                var nbArchivo;
                var vTipoComunicado;
                var grid = $find("<%=grdComunicados.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idArchivo = SelectDataItem.getDataKeyValue("ID_COMUNICADO");
                    nbArchivo = SelectDataItem.getDataKeyValue("NB_COMUNICADO");
                    vTipoComunicado = SelectDataItem.getDataKeyValue("TIPO_COMUNICADO");
                }
                var vURL = "VentanaListaPorRevisarInformacion.aspx?IdComunicado=" + idArchivo + "&TipoComunicado=" + vTipoComunicado;
                var Title = nbArchivo;
                var oWin = window.radopen(vURL, "rwVerInformacion", document.documentElement.clientWidth - 100, document.documentElement.clientHeight - 100);
                oWin.set_title(Title);
            }

            window.onload = function () {
                var idArchivo = ('<%= idArchivo %>');
                if (idArchivo != "") {
                    var vURL = "VisorDeArchivos.aspx?IdArchivo=" + idArchivo;
                    var Title = "Archivo Adjunto";
                    var oWin = window.radopen(vURL, "rwVerArchivo", document.documentElement.clientWidth - 10, document.documentElement.clientHeight - 10);
                    oWin.set_title(Title);
                }
            }



        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdComunicados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rtbTitulo" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="content" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rcbleido" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rlvComentarios" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ArchivosAdjuntos" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="Informacion" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="VideoAdjunto" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rbnComentario" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rlMensajePrivado" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rcbLeido">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdComunicados" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
          <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxLoadingPanel1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rlMensajePrivado" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 10px);">
        <telerik:RadSplitter ID="RSContenedor" runat="server" Height="100%" Width="100%" BorderSize="0">
            <telerik:RadPane ID="RPComunicados" runat="server" Width="50%">
                <div style="margin-right: 20px;">
                    <div style="clear: both; height: 10px;"></div>
                    <%-- <telerik:RadButton runat="server"   OnClientLoad="VentanaAbrirArchivo" Visible="true" ID="btnVentana"></telerik:RadButton>--%>
                    <label class="labelTitulo">Comunicados</label>
                    <telerik:RadGrid
                        ID="grdComunicados"
                        ShowHeader="true"
                        runat="server"
                        AllowPaging="true"
                        AllowSorting="true"
                        Width="100%"
                        OnSelectedIndexChanged="grdComunicados_SelectedIndexChanged"
                        OnNeedDataSource="grdComunicados_NeedDataSource">
                        <ClientSettings EnablePostBackOnRowClick="true">
                            <Selecting AllowRowSelect="true" />
                            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView ClientDataKeyNames="ID_COMUNICADO , ID_ARCHIVO_PDE, NB_COMUNICADO, TIPO_COMUNICADO" DataKeyNames="ID_COMUNICADO, ID_ARCHIVO_PDE, NB_COMUNICADO, TIPO_COMUNICADO" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="50"
                            HorizontalAlign="NotSet" PagerStyle-AlwaysVisible="true" AllowFilteringByColumn="true">
                            <Columns>
                                <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha"
                                    DataField="FE_COMUNICADO" UniqueName="FE_COMUNICADO" HeaderStyle-Width="100" FilterControlWidth="150">
                                </telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Título"
                                    DataField="NB_COMUNICADO" UniqueName="NB_COMUNICADO" HeaderStyle-Width="220" FilterControlWidth="150">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Leído"
                                    DataField="FG_LEIDO" UniqueName="FG_LEIDO" HeaderStyle-Width="80" FilterControlWidth="10">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </telerik:RadPane>
            <telerik:RadSplitBar ID="RSBarra" runat="server" CollapseMode="Forward">
            </telerik:RadSplitBar>
            <telerik:RadPane ID="RPComentarios" runat="server" Width="50%">
                <div style="margin-left: 20px;">
                    <div style="clear: both; height: 40px;"></div>
                    <div class="divControlDerecha" style="cursor: pointer;">
                        <img id="VideoAdjunto" visible="false" runat="server" src="../Assets/images/PDE/play.png" class="img-responsive" width="19" height="19" onclick="AbrirVentanaVideo ();" />
                        <img id="ArchivosAdjuntos" visible="false" runat="server" src="../Assets/images/PDE/Adjunto.png" class="img-responsive" width="17" height="17" onclick="AbrirVentanaArchivo()" />

                    </div>
                    <div class="divControlIzquierda"  style="cursor: pointer;">
                        <img id="Informacion" visible="false" runat="server" src="../Assets/images/PDE/information.png" class="img-responsive" onclick="AbrirVentanaInformacionEmpleado()"/>

                    </div>
                    <div style="clear: both; height: 10px;"></div>
                    <telerik:RadTextBox runat="server" ID="rtbTitulo" TextMode="MultiLine" Resize="None" Width="100%" Height="40px" ReadOnly="true"></telerik:RadTextBox>
                    <div style="clear: both; height: 20px;"></div>
                    <div style="height: 140px; overflow: scroll">
                        <label runat="server" id="content"></label>
                    </div>
                    <div style="clear: both; height: 10px;"></div>
                    <div>
                        <telerik:RadCheckBox runat="server" ID="rcbleido" Checked="True" Text="Marcar como leído" Enabled="false" OnCheckedChanged="rcbleido_CheckedChanged">
                        </telerik:RadCheckBox>
                        <div class="divControlDerecha">
                            <telerik:RadButton ID="rbnComentario"  runat="server" Primary="true" Text="Agregar Comentario" AutoPostBack="false" Enabled="false" OnClientClicked="AbrirVentanaComentario">
                            </telerik:RadButton>

                        </div>
                        &nbsp&nbsp&nbsp&nbsp&nbsp
                       
                    </div>
                    <div style="clear: both; height: 10px;"></div>
                    <label class="labelTitulo">Comentarios:</label>
                    <telerik:RadLabel runat="server" ID="rlMensajePrivado" Text="No se permiten agregar comentarios y marcar los comunicados como leído porque es solo de lectura." Visible="true"></telerik:RadLabel>
                    <telerik:RadListView ID="rlvComentarios" runat="server" DataKeyNames="ID_COMENTARIO_COMUNICADO" OnNeedDataSource="rlvComentarios_NeedDataSource"
                        ClientDataKeyNames="ID_COMENTARIO_COMUNICADO" ItemPlaceholderID="EventosHolder">
                        <LayoutTemplate>
                            <div style="overflow: auto; overflow-y: auto; max-height: 120px;">
                                <asp:Panel ID="EventosHolder" runat="server"></asp:Panel>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div class="RadListViewContainer">
                                <div style='background-color: <%# (int)Eval("ID_COMENTARIO_COMUNICADO") % 2 == 0 ? "#E3E3E3" : "#FBB7CE" %>'>
                                    <div style="text-align: left">
                                        <h4 style="font-weight: bold"><%# Eval("NOMBRE") %> <%# Eval("FE_COMENTARIO") %></h4>
                                    </div>
                                    <div>
                                        <%# Eval("DS_COMENTARIO") %>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </telerik:RadListView>
                </div>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow
                ID="winComentario"
                Width="450px"
                Height="300px"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="false"
                OnClientClose="onCloseWindow"
                Modal="true"
                Behaviors="close"
                Animation="Fade">
            </telerik:RadWindow>
            <telerik:RadWindow
                ID="rwVerVideo"
                Height="370px"
                Width="560px"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="true"
                Behaviors="Close"
                Modal="true"
                ReloadOnShow="false">
            </telerik:RadWindow>
            <telerik:RadWindow
                ID="rwVerArchivo"
                runat="server"
                Title="Archivo adjunto"
                VisibleOnPageLoad="false"
                Behaviors="Minimize, Move, Maximize, Close"
                EnableShadow="false"
                VisibleStatusbar="false"
                ShowContentDuringLoad="true">
            </telerik:RadWindow>
            <telerik:RadWindow
                ID="rwVentanaEditarNotificaciones"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="true"
                Behaviors="Close"
                Modal="true"
                ReloadOnShow="false"
                AutoSize="false"
                OnClientClose="onCloseWindow">
            </telerik:RadWindow>
            <telerik:RadWindow
                ID="rwVerInformacion"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="true"
                Behaviors="Close"
                Modal="true"
                ReloadOnShow="false"
                AutoSize="false"
                OnClientClose="onCloseWindow">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
