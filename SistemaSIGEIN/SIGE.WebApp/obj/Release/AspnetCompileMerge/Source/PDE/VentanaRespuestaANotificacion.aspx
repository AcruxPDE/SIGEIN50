<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaRespuestaANotificacion.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaRespuestaANotificacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script>
 
    </script>
    <style>
        html {
            clear: inherit;
            overflow: hidden;
        }

        .Instrucciones {
            padding-left: 5px;
        }

        .Notificacion {
            padding-left: 5px;
            font-size :12px;
        }

        .Uploadfiles {
            float: right;
        }

        .RadListViewContainer {
            width: calc(100%-10px);
            height: calc(100%-10px);
            border: 1px solid lightgray;
            margin: 5px;
            border-radius: 5px;
            text-decoration-color: black;
        }
       
    </style>
    <script>
        function OpenObservacionResponder(sender, args) {

            var vURL = "VentanaObservacionNotificacion.aspx?IdNotificacion=" + '<%= vIdNotificacion %>' + "&ClAutorizacion=" + '<%= vBotonesVisibles%>';
            var vTitulo = "Atendida";
            var oWin = window.radopen(vURL, "rwObservacioNotificacion", document.documentElement.clientWidth - 90, document.documentElement.clientHeight - 140);
            oWin.set_title(vTitulo);
        }
        function OpenObservacionAutorizar(sender, args) {

            var vURL = "VentanaObservacionNotificacion.aspx?IdNotificacion=" + '<%= vIdNotificacion %>' + "&ClAutorizacion=Autorizada";
            var vTitulo = "Autorizar";
            var oWin = window.radopen(vURL, "rwObservacioNotificacion", document.documentElement.clientWidth - 90, document.documentElement.clientHeight - 140);
            oWin.set_title(vTitulo);
        }

        function OpenObservacionRechazar(sender, args) {

            var vURL = "VentanaObservacionNotificacion.aspx?IdNotificacion=" + '<%= vIdNotificacion %>' + "&ClAutorizacion=Rechazada";
            var vTitulo = "Rechazar";
            var oWin = window.radopen(vURL, "rwObservacioNotificacion", document.documentElement.clientWidth - 90, document.documentElement.clientHeight - 140);
            oWin.set_title(vTitulo);

        }
        function OpenVisorArchivos() {
            var RespuestaNotificacion = true;
            var vTitulo = "Archivo adjunto";
            var vURL = "VisorDeArchivos.aspx?IdArchivo=" + '<%= vIdArchivo %>';
            var oWin = window.radopen(vURL, "rwVerArchivos", document.documentElement.clientWidth - 5, document.documentElement.clientHeight - 5);
           oWin.set_title(vTitulo);
        }

           function closeWindow() {
            GetRadWindow().close();
           }
           function onCloseWindow(oWnd, args) {
               var vLista = $find("<%= rlvComentarios.ClientID %>");
               vLista.rebind();
               closeWindow();
                    }

           
        function useDataFromChild() {

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxManager ID="ramEnviar" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnEnviar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtDsNotas" LoadingPanelID="ralpEnviar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rauArchivo" LoadingPanelID="ralpEnviar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtAsunto" LoadingPanelID="ralpEnviar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>

    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="ralpEnviar" runat="server"></telerik:RadAjaxLoadingPanel>

    <div style="height: calc(100% - 50px);">
        <telerik:RadSplitter ID="rsNorificacionesRRHH" BorderSize="0" Width="100%" Height="100%" runat="server">
            <telerik:RadPane ID="rpEdicionTramites" runat="server">
                <div style="padding-top: 2%; width: 90%;">
                    <label class="labelTitulo" id="Label1">Asunto:</label>
                    <telerik:RadTextBox EmptyMessage="" ID="txtAsunto" Enabled="false" Width="100%" Height="30" runat="server"></telerik:RadTextBox>
                    <div style="clear: both; height: 10px;"></div>
                    <div style="padding-left: 10px; height: 70px; Width: 100%; overflow: auto; background: #F2F2F2; font-size:small;">
                        <label id="lblNotificacion" runat="server" class="Notificacion"></label>
                    </div>

                    <div style="float: left; cursor: pointer;">
                        <img id="ArchivoAdjunto" runat="server" class="img-responsive" width="17" height="17" src="../Assets/images/PDE/Adjunto.png"
                            onclick="OpenVisorArchivos()" />
                    </div>
                </div>
                                <div style="clear: both; height: 10px;"></div>
                <div>
                    <label class="labelTitulo">Comentarios:</label>
                    <div style="width: 90%;  overflow: auto;">
                        <telerik:RadListView ID="rlvComentarios" runat="server" DataKeyNames="ID_COMENTARIO_NOTIFICACION" OnNeedDataSource="rlvComentarios_NeedDataSource"
                            ClientDataKeyNames="ID_COMENTARIO_NOTIFICACION" ItemPlaceholderID="EventosHolder">
                            <LayoutTemplate>
                                <div style="overflow: auto; max-height:270px;">
                                    <asp:Panel ID="EventosHolder" runat="server"></asp:Panel>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div class="RadListViewContainer">
                                    <div style='background-color: <%# (int)Eval("ID_COMENTARIO_NOTIFICACION") % 2 == 0 ? "#E3E3E3" : "#FBB7CE" %>'>

                                        <div style="text-align: left;">
                                            <h4 style="font-weight: bold"><%# Eval("NB_EMPLEADO") %> <%# Eval("FE_COMENTARIO") %></h4>
                                        </div>
                                        <div>
                                            <%# Eval("DS_COMENTARIO") %>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </telerik:RadListView>
                    </div>
                </div>

                <div style="clear: both; height: 20px;"></div>
                <telerik:RadLabel runat="server" Font-Bold="true" ForeColor="Gray" ID="lblFinComentario" Visible="false"></telerik:RadLabel>


            </telerik:RadPane>
            <telerik:RadPane ID="rpAyudaEdicionDeTramites" runat="server" Scrolling="None" Width="20px">
                <%--DockOnOpen="true" --%>
                <telerik:RadSlidingZone ID="rszAyudaEdicionDeTramites" runat="server" SlideDirection="Left" ExpandedPaneId="rsEdicionDeTramites" Width="20px">
                    <telerik:RadSlidingPane ID="rspAyudaEdicionDeTramites" runat="server" Title="Ayuda" Width="240px" RenderMode="Mobile" Height="90%">
                         <div style="text-align: left;">
                            <telerik:RadLabel  id="pIns" runat="server"  ></telerik:RadLabel>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton AutoPostBack="false" ID="btnRerponder" runat="server" Text="Responder" Width="100" OnClientClicked="OpenObservacionResponder"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton AutoPostBack="false" ID="btnAutorizar" runat="server" Text="Autorizar" Width="100" OnClientClicked="OpenObservacionAutorizar"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton AutoPostBack="false" ID="btnRechazar" runat="server" Text="Rechazar" Width="100" OnClientClicked="OpenObservacionRechazar"></telerik:RadButton>
    </div>
        <div class="ctrlBasico">
        <telerik:RadButton AutoPostBack="false" ID="btnCerrar" runat="server" Text="Cerrar" Width="100" OnClientClicked="closeWindow"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadCheckBox runat="server" ID="rcbleido" Checked="False" Text="marcar como leído" Enabled="true" AutoPostBack="true" OnCheckedChanged="rcbleido_CheckedChanged">
        </telerik:RadCheckBox>
    </div>
    <telerik:RadWindowManager ID="rwmEdicionFormatos" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow
                ID="rwObservacioNotificacion"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="true"
                Behaviors="None"
                Modal="true"
                ReloadOnShow="false"
                AutoSize="false"
                OnClientClose="onCloseWindow">
            </telerik:RadWindow>

            <telerik:RadWindow
                ID="rwVerArchivos"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="true"
                Behaviors="Close"
                Modal="true"
                ReloadOnShow="false"
                AutoSize="false">
            </telerik:RadWindow>
        </Windows>

    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
