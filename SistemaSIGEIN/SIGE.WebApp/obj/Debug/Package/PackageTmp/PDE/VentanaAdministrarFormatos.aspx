<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaAdministrarFormatos.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaAdministrarFormatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
      </style>
    <script>

        function OnClientLoad(editor) {
            var tree = $find("<%= rlbFormato.ClientID %>");
            makeUnselectable(tree.get_element());
        }

        function OnClientNodeDragStart() {
            setOverlayVisible(true);
        }

        function OnClientNodeDropping(sender, args) {
            var editor = $find("<%=reFormato.ClientID%>");
            var event = args.get_domEvent();

            document.body.style.cursor = "default";

            var result = isMouseOverEditor(editor, event);
            if (result) {
                var itemValue = "{" + sender.get_selectedItem().get_value() + "}"
                editor.setFocus();
                editor.pasteHtml(itemValue);

            }
            setOverlayVisible(false);
        }
        function OnClientNodeDroppingFecha(sender, args) {
            var editor = $find("<%=reFormato.ClientID%>");
                 var event = args.get_domEvent();

                 document.body.style.cursor = "default";

                 var result = isMouseOverEditor(editor, event);
                 if (result) {
                     var itemValue = "'" + sender.get_selectedItem().get_value() + "'";
                     editor.setFocus();
                     editor.pasteHtml(itemValue);

                 }
                 setOverlayVisible(false);
             }

        function OnClientNodeDragging(sender, args) {
            var editor = editor = $find("<%=reFormato.ClientID%>");
            var event = args.get_domEvent();

            if (isMouseOverEditor(editor, event)) {
                document.body.style.cursor = "hand";
            }
            else {
                document.body.style.cursor = "hand";
            }
        }


        /* ================== Utility methods needed for the Drag/Drop ===============================*/

        //Make all treeview nodes unselectable to prevent selection in editor being lost
        function makeUnselectable(element) {
            var nodes = element.getElementsByTagName("*");
            for (var index = 0; index < nodes.length; index++) {
                var elem = nodes[index];
                elem.setAttribute("unselectable", "");
            }
        }

        //Create and display an overlay to prevent the editor content area from capturing mouse events
        var shimId = null;
        function setOverlayVisible(toShow) {
            if (!shimId) {
                var div = document.createElement("DIV");
                document.body.appendChild(div);
                shimId = new Telerik.Web.UI.ModalExtender(div);
            }

            if (toShow) shimId.show();
            else shimId.hide();
        }


        //Check if the image is over the editor or not
        function isMouseOverEditor(editor, events) {
            var editorFrame = editor.get_contentAreaElement();
            var editorRect = $telerik.getBounds(editorFrame);

            var mouseX = events.clientX;
            var mouseY = events.clientY;

            if (mouseX < (editorRect.x + editorRect.width) &&
             mouseX > editorRect.x &&
                mouseY < (editorRect.y + editorRect.height) &&
             mouseY > editorRect.y) {
                return true;
            }
            return false;
        }

        function closeWindow() {

            GetRadWindow().close();

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <%--<div style="clear: both; height: 10px;"></div>--%>
    <div style="height: calc(100% - 60px);">



        <div style="height: 100%; width: 25%; float: left;">
            <div style="">
                <div style="position: relative; cursor: pointer; overflow: hidden;">
                    <div class="" style="margin-top: 15px; width: 95%">
                        <div style="background-color: #DE1648; width: 100%; text-align: left; color: #FFF;">Selecciona</div>

                    </div>
                </div>
            </div>

            <div style="width: 100%; height: 100%; float: left;">
                <telerik:RadListBox runat="server" ID="rlbFormato"
                    EnableDragAndDrop="true"
                    OnClientDragging="OnClientNodeDragging"
                    OnClientDropping="OnClientNodeDropping"
                    OnClientDragStart="OnClientNodeDragStart"
                    AllowTransfer="true" 
                    Height="90%"
                     Width="90%" 
                    AutoPostBackOnTransfer="true" >
                    <ButtonSettings AreaWidth="35px" />
                </telerik:RadListBox>
            </div>
         

        </div>

        <div style="height: 100%; width: 50%; float: left; padding-left: 15px;">
            <div>
                <div style="position: relative; cursor: pointer; overflow: hidden;">
                    <div class="" style="margin-top: 15px; width: 100%">
                        <div style="background-color: #DE1648; width: 100%; text-align: left; color: #FFF;">Texto</div>

                    </div>
                </div>

            </div>
            <div style="clear: both; height: 10px;"></div>
            <div>
                <telerik:RadTextBox EmptyMessage="Nombre del formato" ID="txtNombre" Width="100%" Height="30" runat="server"></telerik:RadTextBox>

                <div style="clear: both; height: 20px;"></div>
                <telerik:RadTextBox EmptyMessage="Descripción" ID="txtDescripcion" TextMode="MultiLine" Enabled="true" InputType="Text" Width="100%" Height="50" runat="server"></telerik:RadTextBox>
                <div style="clear: both; height: 20px;"></div>

                <telerik:RadEditor Height="300" Width="100%" ToolbarMode="Default" ToolsWidth="600" EditModes="Design" ID="reFormato" runat="server" OnClientLoad="OnClientLoad" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
            </div>
            <div style="clear: both; height: 10px;"></div>
        </div>

        <div style="height: 100%; width: 25%; float: left; padding-left: 25px;">
            <div style="">
                <div style="position: relative; cursor: pointer; overflow: hidden;">
                    <div class="" style="margin-top: 15px; width: 100%">
                        <div style="background-color: #DE1648; width: 100%; text-align: left; color: #FFF;">Selecciona</div>

                    </div>
                </div>

            </div>
              <div style="width: 100%; height:100%; float: left;">
                <telerik:RadListBox runat="server" ID="rlbFecha"
                    EnableDragAndDrop="true"
                    OnClientDragging="OnClientNodeDragging"
                    OnClientDropping="OnClientNodeDroppingFecha"
                    OnClientDragStart="OnClientNodeDragStart"
                    AllowTransfer="true" 
                    Height="90%"
                     Width="90%" 
                    AutoPostBackOnTransfer="true" >
                    <ButtonSettings AreaWidth="35px" />
                </telerik:RadListBox>
            </div>
        </div>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton CssClass="btnGuardar" ID="btnGuardar" AutoPostBack="true" runat="server" Text="Guardar" Width="100" OnClick="btnGuardar_Click"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwmEdicionFormatos" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow
                ID="rwFormatos"
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
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
