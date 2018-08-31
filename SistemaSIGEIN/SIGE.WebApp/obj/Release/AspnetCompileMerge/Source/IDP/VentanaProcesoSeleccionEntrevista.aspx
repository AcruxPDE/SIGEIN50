<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaProcesoSeleccionEntrevista.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaProcesoSeleccionEntrevista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <script type="text/javascript">

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.radWindow;
            else
                if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function closeWindow() {
            GetRadWindow().close();
            //sendDataToParent();
        }

        function generateDataForParent() {
            var vCampos = [];

            var vCampo = {
                clTipoCatalogo: "ENTREVISTA"
            };
            vCampos.push(vCampo);

            sendDataToParent(vCampos);
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle, pWindowProperties) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            if (pWindowProperties)
                windowProperties = pWindowProperties;

            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }


        function OpenSelectionEntrevistadorWindow(sender, args) {
            OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx?mulSel=0", "winSeleccion", "Selección de entrevistador")
        }

        function useDataFromChild(pData) {
            if (pData != null) {

                var texto = "";
                var txtEntrevistador = $find("<%# txtEntrevistador.ClientID %>");
                var txtPuesto = $find("<%# txtPuesto.ClientID %>");
                var txtCorreo = $find("<%# txtCorreoEntrevistador.ClientID %>");

                txtEntrevistador.set_value(pData[0].nbEmpleado);
                txtPuesto.set_value(pData[0].nbPuesto);
                txtCorreo.set_value(pData[0].nbCorreoElectronico);
                InsertDato(pData[0].idEmpleado);
                //SetListBoxItem(listaEvaluador, texto, pData[0].idEmpleado);
            }
        }

        //function SetListBoxItem(list, text, value) {
        //    if (list != undefined) {
        //        list.trackChanges();

        //        var items = list.get_items();
        //        items.clear();

        //        var item = new Telerik.Web.UI.RadListBoxItem();
        //        item.set_text(text);
        //        item.set_value(value);
        //        item.set_selected(true);
        //        items.add(item);

        //        list.commitChanges();
        //    }
        //}

        function EliminarSeleccionEntrevistador() {

            //var list = $find("<%=txtEntrevistador.ClientID %>");
            //SetListBoxItem(list, "Ninguno", "");

            var txtPuesto = $find("<%# txtPuesto.ClientID %>");
            var txtCorreo = $find("<%# txtCorreoEntrevistador.ClientID %>");
            var txtEntrevistador = $find("<%# txtEntrevistador.ClientID %>");
            txtEntrevistador.set_value("");
            txtPuesto.set_value("");
            txtCorreo.set_value("");

            //IdEntrevistador.value ="";


            // var vListBox = $find("<%=txtEntrevistador.ClientID %>");
            // var vSelectedItems = vListBox.get_selectedItems();
            // vListBox.trackChanges();
            //if (vSelectedItems)
            //  vSelectedItems.forEach(function (item) {
            //    vListBox.get_items().remove(item);
            // });
            //vListBox.commitChanges();
        }
        function InsertDato(pDato) {
            var ajaxManager = $find('<%= ramEntrevista.ClientID%>');
            ajaxManager.ajaxRequest(pDato);
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <telerik:RadAjaxLoadingPanel runat="server" ID="ralpEntrevista"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxManager runat="server" ID="ramEntrevista" DefaultLoadingPanelID="ralpEntrevista" OnAjaxRequest="ramEntrevista_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbTipoEntrevista" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtEntrevistador" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPuesto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtCorreoEntrevistador" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />

                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ramEntrevista">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtEntrevistador" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPuesto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />

                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarEntrevistador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtEntrevistador" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtPuesto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />

                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
  <div style="height: calc(100% - 20px);">
    <div style="clear: both; height: 10px;"></div>

    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label>Tipo de entrevista:</label>
        </div>

        <div class="divControlDerecha">
            <telerik:RadComboBox runat="server" ID="cmbTipoEntrevista" Width="300px" DataTextField="NB_ENTREVISTA_TIPO" DataValueField="ID_ENTREVISTA_TIPO"></telerik:RadComboBox>
        </div>
    </div>

    <div style="clear: both; height: 10px;"></div>

    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label>Entrevistador:</label>
        </div>
        <div class="divControlDerecha">
            <%--<telerik:RadListBox ID="lstEntrevistador" Width="300px" Height="35px" runat="server"></telerik:RadListBox>--%>
            <telerik:RadTextBox ID="txtEntrevistador" Width="300px" Height="35px" runat="server" Enabled="true"></telerik:RadTextBox>
            <telerik:RadButton ID="btnSeleccionarentrevistador" runat="server" Text="B" AutoPostBack="false" OnClientClicked="OpenSelectionEntrevistadorWindow"></telerik:RadButton>
            <telerik:RadButton ID="btnEliminarEntrevistador" runat="server" Text="X" AutoPostBack="true" OnClientClicked="EliminarSeleccionEntrevistador" OnClick="btnEliminarEntrevistador_Click"></telerik:RadButton>
        </div>
    </div>

    <div style="clear: both; height: 10px;"></div>

    <div class="ctrlBasico">

        <div class="divControlIzquierda">
            <label>Puesto del Entrevistador: </label>
        </div>

        <div class="divControlDerecha">
            <telerik:RadTextBox runat="server" ID="txtPuesto" Width="300px" Enabled="true"></telerik:RadTextBox>
        </div>
    </div>

    <div class="ctrlBasico">

        <div class="divControlIzquierda">
            <label>Correo electrónico: </label>
        </div>

        <div class="divControlDerecha">
            <telerik:RadTextBox runat="server" ID="txtCorreoEntrevistador" Width="300px"></telerik:RadTextBox>
        </div>
    </div>

    <div style="clear: both; height: 10px;"></div>

    <div class="ctrlBasico">
        <label>Comentarios de la entrevista: </label>
    </div>

    <div class="ctrlBasico">
        <telerik:RadEditor Height="200px" Width="100%" ToolsWidth="500" EditModes="Design" ID="txtDsNotas" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
    </div>

    <div style="clear: both; height: 10px;"></div>

    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton runat="server" ID="btnGuardar" UseSubmitBehavior="false" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
        </div>

        <div class="ctrlBasico">
            <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" OnClientClicked="generateDataForParent"></telerik:RadButton>
        </div>
    </div>
      </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
