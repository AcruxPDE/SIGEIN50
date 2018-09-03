<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaRadDock.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaRadDock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">


    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">

        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardarCatalogo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rad" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <%--<telerik:AjaxUpdatedControl ControlID="btnGuardarCatalogo"  UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>--%>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rad">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rad" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="cmbCategoria">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbCategoria" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="cmbClasificaciones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbClasificaciones" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>


    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="modal" type="text/javascript">
            function GetRadWindow() {
                var oWindow = null;
                if
                (window.radWindow) oWindow = window.radWindow;
                else
                    if (window.frameElement.radWindow)
                        oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function closeWindow() {
                GetRadWindow().close();
            }

        </script>
    </telerik:RadCodeBlock>


    <div style="clear: both;"></div>
    <div style="height: calc(100% - 70px);">
        <telerik:RadPanelBar ID="rad" runat="server" Height="470" Width="100%"  ExpandMode="FullExpandedItem">
            <Items>
                <telerik:RadPanelItem Text="Generales" PostBack="false" Expanded="true">
                    <ContentTemplate>

                        <div style="clear: both; height: 10px;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="Label3" runat="server">Categoría:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadComboBox Filter="Contains" runat="server" ID="cmbCategoria" Width="300" MarkFirstMatch="true" EnableLoadOnDemand="true" 
                                    AutoPostBack="true" HighlightTemplatedItems="true"  DropDownWidth="315" ValidationGroup="VGcmbCategoria" OnSelectedIndexChanged="cmbCategoria_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <br />


                        <br />
                        <div style="clear: both;" />
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="Label2" runat="server">Clasificación:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadComboBox Filter="Contains" runat="server" ID="cmbClasificaciones" Width="300" MaxHeight="200" MarkFirstMatch="true" EnableLoadOnDemand="true" 
                                    AutoPostBack="true" HighlightTemplatedItems="true"  DropDownWidth="310" ValidationGroup="VGcmbClasificaciones">
                                </telerik:RadComboBox>                                
                            </div>
                        </div>
                        <br />

                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="Label4" runat="server">Clave:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtClave" runat="server" Width="300px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;" />
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="lblNbIdioma" runat="server">Competencia:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtNbCompetencia" runat="server" Width="300px" MaxLength="1000"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;" />
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="Label1" runat="server">Descripción:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadTextBox ID="txtDescripcion" runat="server" Width="300px" Height="130px" MaxLength="1000" CssClass="textarea" TextMode="MultiLine"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;" />
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="Label5" name="lblDsTipo" runat="server">Activo:&nbsp;</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadButton ID="chkActivo" runat="server" ToggleType="CheckBox" AutoPostBack="false">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                        </div>

                    </ContentTemplate>
                </telerik:RadPanelItem>

                <telerik:RadPanelItem Text="Definición de niveles" PostBack="false" Expanded="false">
                    <ContentTemplate>
                        <div style="clear: both;"></div>
                        <table style="width: 98%;">
                            <thead>
                                <tr>
                                    <th style="width: 10%;">
                                        <div></div>
                                    </th>
                                    <th style="width: 45%;">
                                        <div>Por Puesto</div>
                                    </th>
                                    <th style="width: 45%;">
                                        <div>Por Persona</div>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td style="text-align: center;">Nivel 0</td>
                                    <td>
                                        <telerik:RadEditor
                                            Height="150px"
                                            Width="100%"
                                            ToolsWidth="310px"
                                            EditModes="Design"
                                            ID="radEditorPuesto0"
                                            runat="server"
                                            ToolbarMode="Default"
                                            ToolsFile="~/Assets/BasicTools.xml">
                                        </telerik:RadEditor>
                                        <%--<asp:RequiredFieldValidator ValidationGroup="save" Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator5" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="radEditorPuesto0" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>--%>

                                    </td>
                                    <td>
                                        <telerik:RadEditor
                                            Height="150px"
                                            Width="100%"
                                            ToolsWidth="310px"
                                            EditModes="Design"
                                            ID="radEditorPersona0"
                                            runat="server"
                                            ToolbarMode="Default"
                                            ToolsFile="~/Assets/BasicTools.xml">
                                        </telerik:RadEditor>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">Nivel 1</td>
                                    <td>
                                        <telerik:RadEditor
                                            Height="150px"
                                            Width="100%"
                                            ToolsWidth="310px"
                                            EditModes="Design"
                                            ID="radEditorPuesto1"
                                            runat="server"
                                            ToolbarMode="Default"
                                            ToolsFile="~/Assets/BasicTools.xml">
                                        </telerik:RadEditor>
                                        <%--<asp:RequiredFieldValidator ValidationGroup="save" Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator7" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="radEditorPuesto1" Text="Campo obligatorio" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>

                                    </td>
                                    <td>
                                        <telerik:RadEditor
                                            Height="150px"
                                            Width="100%"
                                            ToolsWidth="310px"
                                            EditModes="Design"
                                            ID="radEditorPersona1"
                                            runat="server"
                                            ToolbarMode="Default"
                                            ToolsFile="~/Assets/BasicTools.xml">
                                        </telerik:RadEditor>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">Nivel 2</td>
                                    <td>
                                        <telerik:RadEditor
                                            Height="150px"
                                            Width="100%"
                                            ToolsWidth="310px"
                                            EditModes="Design"
                                            ID="radEditorPuesto2"
                                            runat="server"
                                            ToolbarMode="Default"
                                            ToolsFile="~/Assets/BasicTools.xml">
                                        </telerik:RadEditor>

                                    </td>
                                    <td>
                                        <telerik:RadEditor
                                            Height="150px"
                                            Width="100%"
                                            ToolsWidth="310px"
                                            EditModes="Design"
                                            ID="radEditorPersona2"
                                            runat="server"
                                            ToolbarMode="Default"
                                            ToolsFile="~/Assets/BasicTools.xml">
                                        </telerik:RadEditor>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">Nivel 3</td>
                                    <td>
                                        <telerik:RadEditor
                                            Height="150px"
                                            Width="100%"
                                            ToolsWidth="310px"
                                            EditModes="Design"
                                            ID="radEditorPuesto3"
                                            runat="server"
                                            ToolbarMode="Default"
                                            ToolsFile="~/Assets/BasicTools.xml">
                                        </telerik:RadEditor>

                                    </td>
                                    <td>
                                        <telerik:RadEditor
                                            Height="150px"
                                            Width="100%"
                                            ToolsWidth="310px"
                                            EditModes="Design"
                                            ID="radEditorPersona3"
                                            runat="server"
                                            ToolbarMode="Default"
                                            ToolsFile="~/Assets/BasicTools.xml">
                                        </telerik:RadEditor>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">Nivel 4</td>
                                    <td>
                                        <telerik:RadEditor
                                            Height="150px"
                                            Width="100%"
                                            ToolsWidth="310px"
                                            EditModes="Design"
                                            ID="radEditorPuesto4"
                                            runat="server"
                                            ToolbarMode="Default"
                                            ToolsFile="~/Assets/BasicTools.xml">
                                        </telerik:RadEditor>

                                    </td>
                                    <td>
                                        <telerik:RadEditor
                                            Height="150px"
                                            Width="100%"
                                            ToolsWidth="310px"
                                            EditModes="Design"
                                            ID="radEditorPersona4"
                                            runat="server"
                                            ToolbarMode="Default"
                                            ToolsFile="~/Assets/BasicTools.xml">
                                        </telerik:RadEditor>


                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">Nivel 5</td>
                                    <td>
                                        <telerik:RadEditor
                                            Height="150px"
                                            Width="100%"
                                            ToolsWidth="310px"
                                            EditModes="Design"
                                            ID="radEditorPuesto5"
                                            runat="server"
                                            ToolbarMode="Default"
                                            ToolsFile="~/Assets/BasicTools.xml">
                                        </telerik:RadEditor>

                                    </td>
                                    <td>
                                        <telerik:RadEditor
                                            Height="150px"
                                            Width="100%"
                                            ToolsWidth="310px"
                                            EditModes="Design"
                                            ID="radEditorPersona5"
                                            runat="server"
                                            ToolbarMode="Default"
                                            ToolsFile="~/Assets/BasicTools.xml">
                                        </telerik:RadEditor>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </ContentTemplate>
                </telerik:RadPanelItem>
            </Items>

        </telerik:RadPanelBar>

    </div>

    <div style="clear: both; height: 10px;"></div>
    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton ValidationGroup="save" ID="btnGuardarCatalogo" UseSubmitBehavior="false" runat="server" Width="100" Text="Guardar" OnClick="btnSave_click" AutoPostBack="true"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnCancelarCatalogo" runat="server" Width="100" Text="Cancelar" AutoPostBack="false" OnClientClicking="closeWindow"></telerik:RadButton>
        </div>
    </div>


    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
