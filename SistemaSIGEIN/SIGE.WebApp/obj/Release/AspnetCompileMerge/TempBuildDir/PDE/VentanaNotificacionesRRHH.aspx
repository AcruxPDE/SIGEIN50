<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaNotificacionesRRHH.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaNotificacionesRRHH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
     <style>
        .reTool.reInsertSpecialLink {
    background-image: url(Custom.gif) !important;
    background-repeat:no-repeat;
}
.reTool.rePrintPreview {
    background-image: url(CustomDialog.gif) !important;
    background-repeat:no-repeat;
}
.reTool.reInsertEmoticon {
    background-image: url(Emoticons/1.gif) !important;
    background-repeat:no-repeat;
}
 
    .reTool.reInsertSpecialLink:before,
    .reTool.rePrintPreview:before,
    .reTool.reInsertEmoticon:before {
        display:none;
    }
    </style>
    <script>
        function closeWindow() {
            GetRadWindow().close();
        }
        (function (global, undefined) {
            var EditorCommandList;

            var registerCustomCommands = function () {
                EditorCommandList["InsertSpecialLink"] = function (commandName, editor, args) {
                    var elem = editor.getSelectedElement(); //returns the selected element.

                    if (elem && elem.tagName == "A") {
                        editor.selectElement(elem);
                        argument = elem.cloneNode(true);
                    }
                    else {
                        var content = editor.getSelectionHtml();
                        var link = editor.get_document().createElement("A");
                        link.innerHTML = content;
                        argument = link;
                    }

                    var myCallbackFunction = function (sender, args) {
                        editor.pasteHyperLink(args, "Insert Link");
                    };

                    editor.showExternalDialog(
                    "InsertLink.aspx",
                    argument,
                    270,
                    200,
                    myCallbackFunction,
                    null,
                    "Insert Link",
                    true,
                    Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move,
                    false,
                    true);
                };

                EditorCommandList["PrintPreview"] = function (commandName, editor, args) {
                    var args = editor.get_html(true); //returns the HTML of the selection.  

                    editor.showExternalDialog(
                    "PrintPreview.aspx",
                    args,
                    570,
                    420,
                    null,
                    null,
                    "Print Preview",
                    true,
                    Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move,
                    true,
                    true);
                };

                EditorCommandList["InsertEmoticon"] = function (commandName, editor, args) {
                    var myCallbackFunction = function (sender, args) {
                        editor.pasteHtml(String.format("<img src='{0}' border='0' align='middle' alt='emoticon' /> ", args.image));
                    }

                    editor.showExternalDialog(
                    "InsertEmoticon.aspx",
                    {},
                    400,
                    310,
                    myCallbackFunction,
                    null,
                    "Insert Emoticon",
                    true,
                    Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move,
                    false,
                    true);
                };
            };

            var TelerikDemo = global.TelerikDemo = {
                editor_onClientLoad: function (editor, args) {
                    EditorCommandList = Telerik.Web.UI.Editor.CommandList;
                    registerCustomCommands();
                }
            };
        })(window);
        function OpenSeleccion() {
            var vURL = "VentanaSeleccionEnvioNotificacion.aspx"
            var vTitulo = "Selección envío de notificaciones";
            var oWin = window.radopen(vURL, "wNotificaciones", document.documentElement.clientWidth - 20, document.documentElement.clientHeight - 20);
            oWin.set_title(vTitulo);
        }
   
    </script>
    <style>
        .Instrucciones {
            padding-left: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <%--    <telerik:RadAjaxManager ID="ramEnviar" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnEnviar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtDsNotas" LoadingPanelID="ralpEnviar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rauArchivo" LoadingPanelID="ralpEnviar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtAsunto" LoadingPanelID="ralpEnviar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rlvComentarios" LoadingPanelID="ralpEnviar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rcbCerrar" LoadingPanelID="ralpEnviar" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>

    </telerik:RadAjaxManager>--%>
    <div style="height: calc(100% - 60px);">
        <telerik:RadSplitter ID="rsNorificacionesRRHH" BorderSize="0" Width="100%" Height="100%" runat="server">
            <telerik:RadPane ID="rpEdicionTramites" runat="server">
                <div style="clear: both; height: 20px;"></div>
                <div style="width: 90%;">
                    <label id="lblAsunto">Asunto:</label>
                    <telerik:RadTextBox EmptyMessage="" ID="txtAsunto" Enabled="true" InputType="Text" Width="100%" Height="30" runat="server"></telerik:RadTextBox>

                    <div style="clear: both; height: 10px;"></div>
                    <telerik:RadEditor Width="100%" Height="300px" OnClientLoad="TelerikDemo.editor_onClientLoad"  EditModes="Design" ToolbarMode="Default" ID="txtDsNotas" runat="server" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
                  
                </div>

            </telerik:RadPane>
            <telerik:RadPane ID="rpAyudaEdicionDeTramites" runat="server" Scrolling="None" Width="20px">
              
                <telerik:RadSlidingZone  ID="rszAyudaEdicionDeTramites" runat="server" SlideDirection="Left" ExpandedPaneId="rsEdicionDeTramites" Width="20px">
                    <telerik:RadSlidingPane DockOnOpen="false"  ID="rsEdicionDeTramites" runat="server" Title="Ayuda" Width="240px" RenderMode="Mobile" Height="90%">
                        <div style="text-align: left;">
                               <telerik:RadLabel  id="pIns" runat="server"  ></telerik:RadLabel>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </div>
    <div>
        <div class="divControlIzquierda">

            <telerik:RadAsyncUpload CssClass="Uploadfiles" ID="rauArchivo" runat="server" Width="250px" MaxFileInputsCount="1" Localization-Cancel="Cancelar"
                Localization-Remove="Quitar" Localization-Select="Adjuntar" Font-Size="12px"
      >
            </telerik:RadAsyncUpload>
        </div>
        <div class="divControlIzquierda" style="padding-left: 140px;">
 <telerik:RadButton ID="btnSeleccionar" runat="server" Text="Seleccionar a" Width="145" AutoPostBack="false" OnClientClicked="OpenSeleccion"></telerik:RadButton>

        </div>
        <div class="ctrlBasico" style="padding-left: 140px;">
            <telerik:RadCheckBox runat="server" ID="rcbCerrar" Checked="False" Text="Enviar otra notificación" Enabled="true" AutoPostBack="false" OnCheckedChanged="rcbCerrar_CheckedChanged">
            </telerik:RadCheckBox>
        </div>
    </div>
            <telerik:RadButton ID="btnEnviar" runat="server" Text="Enviar" Width="100" OnClick="bntEnviar_Click"></telerik:RadButton>

      
    <telerik:RadAjaxLoadingPanel ID="ralpEnviar" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow
                ID="wNotificaciones"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="true"
                Behaviors="Close"
                Modal="true"
                ReloadOnShow="false"
                AutoSize="false"
                               >
            </telerik:RadWindow>

        </Windows>

    </telerik:RadWindowManager>
        
</asp:Content>
