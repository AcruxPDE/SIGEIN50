<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaDarBajaEmpleado.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaDarBajaEmpleado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .RadButton.rbSkinnedButton.uncheckedYes {
            background-color: #eee !important;
        }

            .RadButton.rbSkinnedButton.uncheckedYes > .rbDecorated {
                color: #eee !important;
            }

        .RadButton.rbSkinnedButton.checkedNo {
            background-color: #eee !important;
        }

            .RadButton.rbSkinnedButton.checkedNo > .rbDecorated {
                color: #333 !important;
            }

        .RadButton.rbSkinnedButton.uncheckedNo {
            background-color: #eee !important;
        }

            .RadButton.rbSkinnedButton.uncheckedNo > .rbDecorated {
                color: #eee !important;
            }

        .checkContainer {
            border-radius: 5px;
            border: 1px solid lightgray;
            background: #eee;
        }
    </style>
    <script id="modal" type="text/javascript">
        function closeWindow() {
            GetRadWindow().close();
        }

        function GetWindowProperties() {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            return {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };
        }

        function GetSelectionCausaWindow() {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Selección de causa de baja";
            wnd.vURL = "../Comunes/SeleccionCatalogos.aspx?ID_CATALOGO=3&mulSel=0";
            wnd.vRadWindowId = "WinSeleccionCausa";
            return wnd;
        }

        function OpenSelectionCausaWindow() {
            OpenWindow(GetSelectionCausaWindow());
        }

        function OpenWindow(pWindowProperties) {
            openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                var arr = [];
                var vSelectedData = pDato[0];
                switch (pDato[0].clTipoCatalogo) {
                    case "CATALOGO":
                        list = $find("<%=lstCausaBaja.ClientID %>");
                         txtList = $find("<%=txtDsCausa.ClientID %>");
                         if (list != undefined) {
                             list.trackChanges();
                             var items = list.get_items();
                             items.clear();
                             var item = new Telerik.Web.UI.RadListBoxItem();
                             item.set_text(vSelectedData.nbCatalogoValor);
                             item.set_value(vSelectedData.idCatalogo);
                             item.set_selected(true);
                             items.add(item);
                             list.commitChanges();
                             document.getElementById("<%=txtDsCausa.ClientID %>").value = vSelectedData.dsCatalogoValor;
                         }
                         break;
                 }
             }
        }

        function ConfirmarDasBaja(sender, args) {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                            if (shouldSubmit) {
                                this.click();
                            }
                        });
                        radconfirm('¿Estas seguro que deseas dar de baja a este empleado?', callBackFunction, 400, 170, null, "Aviso");
                        args.set_cancel(true);
                    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpBaja" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramBaja" runat="server" DefaultLoadingPanelID="ralpBaja">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnProgramarTrue">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rdpFechaBaja" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline"/>
                    <telerik:AjaxUpdatedControl ControlID="lbFechaBaja" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="btnProgramarFalse">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rdpFechaBaja" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline"/>
                    <telerik:AjaxUpdatedControl ControlID="lbFechaBaja" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
    <div style="height: calc(100% - 60px);">
        <div style="height: 10px;"></div>
        <div class="ctrlBasico">
            <table class="ctrlTableForm">
                <tr>
                    <td class="ctrlTableDataContext">
                        <label id="lbClaveEmpleado" class="divControlDerecha" name="lbClaveEmpleado" runat="server">Clave empleado:</label>
                    </td>
                    <td colspan="2" class="ctrlTableDataBorderContext">
                        <div id="txtClEmpleado" runat="server"></div>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <label id="LbNombre" class="divControlDerecha" name="LbNombre" runat="server">Nombre:</label>
                    </td>
                    <td colspan="2" class="ctrlTableDataBorderContext">
                        <div id="txtNombre" runat="server"></div>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <label id="LbClavePuesto" class="divControlDerecha" name="LbClavePuesto" runat="server">Clave puesto:</label>
                    </td>
                    <td colspan="2" class="ctrlTableDataBorderContext">
                        <div id="txtClPuesto" runat="server"></div>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <label id="lbPuesto" class="divControlDerecha" name="lbPuesto" runat="server">Puesto:</label>
                    </td>
                    <td colspan="2" class="ctrlTableDataBorderContext">
                        <div id="txtPuesto" runat="server"></div>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <div class="divControlDerecha">
                            <label id="Label2" name="lblCausaBaja" runat="server">Causa de baja:</label>
                        </div>
                    </td>
                    <td colspan="2" class="ctrlTableDataContext">
                        <div class="ctrlBasico" style="width: 100%;">
                            <div class="divControlDerecha" style="width: 5%;">
                                <telerik:RadButton ID="btnCausaBaja" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgEstado" OnClientClicked="OpenSelectionCausaWindow"></telerik:RadButton>
                            </div>
                            <div class="divControlDerecha" style="width: 95%;">
                                <telerik:RadListBox ID="lstCausaBaja" Width="100%" runat="server" Height="35px" ValidationGroup="vgEstado"></telerik:RadListBox>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="ctrlTableDataContext">
                        <div class="divControlDerecha" style="width: 85%;">
                            <telerik:RadTextBox ID="txtDsCausa" InputType="Text" Width="100%" Height="100px" TextMode="MultiLine" Enabled="false" runat="server"></telerik:RadTextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <div class="divControlIzquierda">
                            <label id="lbFechaBaja" name="lbFechaBaja" runat="server" visible="true" Text=""></label>
                        </div>
                    </td>
                    <td colspan="2" class="ctrlTableDataContext">
                     <%--   <div class="divControlIzquierda">
                             <div class="checkContainer">
                                <telerik:RadButton ID="btnProgramarTrue" runat="server" ToggleType="Radio" OnClick="btnProgramarTrue_Click" ButtonType="StandardButton" GroupName="grpOtros" AutoPostBack="true" Width="55">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="btnProgramarFalse" runat="server" ToggleType="Radio" OnClick="btnProgramarFalse_Click" ButtonType="StandardButton" GroupName="grpOtros" AutoPostBack="true" Width="55">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                        </div>--%>
                        <div class="divControlIzquierda">
                            <telerik:RadDatePicker ID="rdpFechaBaja" Enabled="true" runat="server" Visible="true">
                            </telerik:RadDatePicker>
                            </div>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <div class="divControlDerecha">
                            <label id="Label3">Comentario:</label>
                        </div>
                    </td>
                    <td colspan="2" class="ctrlTableDataContext">
                        <div class="divControlDerecha">
                            <telerik:RadEditor Height="100" Width="100%" ToolsWidth="400" EditModes="Design" ID="reComentarios" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
                        </div>
                    </td>
                </tr>

            </table>
        </div>

        <%--    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblNbEmpleado">Nombre:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtNombre" InputType="Text" Width="400"  runat="server"></telerik:RadTextBox>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblClPuesto">Clave puesto:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtClPuesto" InputType="Text" Width="100"  runat="server"></telerik:RadTextBox>
        </div>
    </div>
    <div style="clear: both;"></div>
     <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblPuesto">Puesto:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtPuesto" InputType="Text" Width="400"  runat="server"></telerik:RadTextBox>
        </div>
    </div>--%>
        <%-- <div style="clear: both;"></div>
    <div class="ctrlBasico">
    <div class="divControlIzquierda">
            <label id="lblCausaBaja" name="lblCausaBaja" runat="server"> Causa de baja:</label>
        </div>
     <div class="divControlDerecha">
            <telerik:RadListBox ID="lstCausaBaja" Width="365" runat="server" Height="35px" ValidationGroup="vgEstado"></telerik:RadListBox>
            <telerik:RadButton ID="btnCausaBaja" runat="server" Text="B" AutoPostBack="false"  ValidationGroup="vgEstado" OnClientClicked="OpenSelectionCausaWindow"></telerik:RadButton>
        </div>
    </div>--%>
        <%-- <div style="clear: both;"></div>
    <div class="ctrlBasico" style="margin-left:135px">
            <telerik:RadTextBox ID="txtDsCausa" InputType="Text" Width="400" Height="100px"  TextMode="MultiLine" Enabled="false" runat="server"></telerik:RadTextBox>
    </div>--%>
        <%-- <div style="clear: both;"></div>
     <div class="ctrlBasico">--%>
        <%-- <div class="divControlIzquierda">
            <label id="lbFechaBaja" name="lbFechaBaja" runat="server">Fecha efectiva de la baja:</label>
        </div>--%>
        <%--<div class="divControlDerecha">--%>
        <%-- <telerik:RadDatePicker ID="rdpFechaBaja" Enabled="true" runat="server" Width="140px" >
            </telerik:RadDatePicker>--%>
        <%--  </div>--%>
        <%--</div>--%>
        <%-- <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblComentarios">Comentario:</label>
        </div>
        <div class="divControlDerecha">
           <%-- <telerik:RadEditor Height="100" Width="400" ToolsWidth="400" EditModes="Design" ID="reComentarios" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>--%>
        <%-- </div>
    </div>--%>
        <div style="clear: both; height: 10px"></div>
        <div class="divControlesBoton">
            <telerik:RadButton ID="btnGuardar"
                runat="server"
                Width="100px"
                Text="Guardar"
                AutoPostBack="true"
                OnClientClicking="ConfirmarDasBaja"
                OnClick="btnGuardar_Click">
            </telerik:RadButton>

            <telerik:RadButton ID="btnCancelar"
                runat="server"
                Width="100px"
                Text="Cancelar"
                AutoPostBack="true"
                OnClientClicking="closeWindow">
            </telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true">
    </telerik:RadWindowManager>
</asp:Content>
